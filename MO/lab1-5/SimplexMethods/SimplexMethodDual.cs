using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixOperations;

namespace SimplexMethods
{
	public class SimplexMethodDual
	{
		#region Constructors

		public SimplexMethodDual(Matrix a, Matrix b, Matrix c, List<int> baseIndexes)
		{
			m_matrixA = a.Copy();
			m_matrixB = b.Copy();
			m_matrixC = c.Copy();
			m_baseIndexes = new List<int>(baseIndexes);
			m_baseIndexes.Sort();
		}

		#endregion

		#region Public methods

		public bool Solve(out Matrix y0, out Matrix pseudoPlan)
		{
			y0 = null;
			pseudoPlan = null;

			Step6CalculateNewAbInverse();
			m_yBaseVector = GetFirstBasePlan();

			int iterationsCount = 0;
			while (iterationsCount++ < m_maxIterationsCount)
			{
				Console.WriteLine("Iter. no:{0}", iterationsCount);
				Console.WriteLine("Base vector:\n{0}", m_yBaseVector);
				Console.WriteLine("Jb:\n{0}", m_baseIndexes.Aggregate("", (acc, ind) => acc + ind + ", "));

				Step1CalculatePseudoPlan();
				Console.WriteLine("Pseudo plan_b:\n{0}", m_pseudoPlanB);
				if (Step2CheckForOptimum())
				{
					y0 = BuildFullPlan(m_yBaseVector);
					pseudoPlan = BuildFullPlan(m_pseudoPlanB);
					return true;
				}

				int s;
				Matrix deltaY;
				Dictionary<int, double> estimations;
				if (!Step3CheckForCompatibility(out s, out deltaY, out estimations))
				{
					return false;
				}

				KeyValuePair<double, int> step4Res = Step4GetMinimum(estimations);
				double sigma0 = step4Res.Key;
				int j0 = step4Res.Value;

				Console.WriteLine("S:\n{0}", s);
				Console.WriteLine("DeltaY:\n{0}", deltaY);
				Console.WriteLine("Sigma0:\n{0}", sigma0);
				Console.WriteLine("j0:\n{0}", j0);
				Step5RecalculatePlan(s, deltaY, sigma0, j0);
				Step6CalculateNewAbInverse();
			}

			return false;
		}

		#endregion

		#region Private methods

		private Matrix GetFirstBasePlan()
		{
			Matrix cB = new Matrix(m_baseIndexes.Count, 1);
			for (int i = 0; i < m_baseIndexes.Count; i++)
			{
				cB[i, 0] = m_matrixC[m_baseIndexes[i], 0];
			}

			return cB.Transpose().Multiply(m_matrixAbRev).Transpose();
		}

		private Matrix BuildFullPlan(Matrix basePlan)
		{
			Matrix fullPlan = new Matrix(m_matrixA.ColumnsCount, 1);
			for (int i = 0; i < basePlan.RowsCount; i++)
			{
				fullPlan[m_baseIndexes[i], 0] = basePlan[i, 0];
			}

			return fullPlan;
		}

		private void Step1CalculatePseudoPlan()
		{
			m_pseudoPlanB = m_matrixAbRev.Copy().Multiply(m_matrixB);
		}

		private bool Step2CheckForOptimum()
		{
			bool isOptimum = true;
			for (int i = 0; i < m_pseudoPlanB.RowsCount; i++)
			{
				isOptimum &= m_pseudoPlanB[i, 0].IsZero() || m_pseudoPlanB[i, 0] > 0;
			}
			return isOptimum;
		}

		private bool Step3CheckForCompatibility(out int s, out Matrix deltaY, out Dictionary<int, double> estimations)
		{
			estimations = new Dictionary<int, double>();
			deltaY = null;
			s = -1;
			int i = 0;
			while (i < m_pseudoPlanB.RowsCount && s < 0)
			{
				if (m_pseudoPlanB[i, 0] < 0)
				{
					s = i;
				}
				i++;
			}
			
			if (s < 0)
			{
				return false;
			}

			Matrix eS = new Matrix(m_baseIndexes.Count, 1);
			eS[s, 0] = 1;
			deltaY = eS.Copy().Transpose().Multiply(m_matrixAbRev).Transpose();

			bool isCompatible = false;
			for (int j = 0; j < m_matrixA.ColumnsCount; j++)
			{
				if (!m_baseIndexes.Contains(j))
				{
					double mJ = deltaY.Copy().Transpose().Multiply(m_matrixA.GetVectorColulmn(j))[0, 0];
					estimations.Add(j, mJ);
					isCompatible |= !mJ.IsZero() && mJ < 0;
				}
			}

			return isCompatible;
		}

		//return (sigma0, j0)
		private KeyValuePair<double, int> Step4GetMinimum(Dictionary<int, double> estimations)
		{
			double sigma0 = Double.MaxValue;
			int j0 = -1;
			foreach(KeyValuePair<int, double> mJ in estimations)
			{
				if (mJ.Value < 0)
				{
					double sigma = (m_matrixC[mJ.Key, 0] -
					                m_matrixA.GetVectorColulmn(mJ.Key).Copy().Transpose().Multiply(m_yBaseVector)[0, 0])/mJ.Value;
					if (sigma < sigma0)
					{
						sigma0 = sigma;
						j0 = mJ.Key;
					}
				}
			}

			if (j0 < 0)
			{
				throw new Exception("Step 4 failed. Can't find j0");
			}

			return new KeyValuePair<double, int>(sigma0, j0);
		}

		private void Step5RecalculatePlan(int s, Matrix deltaY, double sigma0, int j0)
		{
			m_yBaseVector.Add(deltaY.Copy().Multiply(sigma0));

			m_baseIndexes[s] = j0;
			m_baseIndexes.Sort();
		}

		private void Step6CalculateNewAbInverse()
		{
			Matrix aB = new Matrix(m_matrixA.RowsCount, m_baseIndexes.Count);
			int tmp = 0;

			//for (int i = 0; i < m_baseIndexes.Count; i++)
			//{
			//    for (int j = 0; j < m_matrixA.RowsCount; j++)
			//    {
			//        aB[j, tmp] = m_matrixA[j, m_baseIndexes[i]];
			//    }
			//    tmp++;
			//}

			//sequence indifferent
			for (int i = 0; i < m_matrixA.ColumnsCount; i++)
			{
				if (m_baseIndexes.Contains(i))
				{
					for (int j = 0; j < m_matrixA.RowsCount; j++)
					{
						aB[j, tmp] = m_matrixA[j, i];
					}
					tmp++;
				}
			}
			m_matrixAbRev = aB.Invert();
		}

		#endregion

		#region Private fields

		private Matrix m_matrixA;
		private Matrix m_matrixAbRev;
		private Matrix m_matrixB;
		private Matrix m_matrixC;
		private Matrix m_yBaseVector;
		private Matrix m_pseudoPlanB;
		private List<int> m_baseIndexes;

		private const int m_maxIterationsCount = 10000;

		#endregion
	}
}
