using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixOperations;

namespace SimplexMethods
{
	public enum ESolutionResult
	{
		Unique,
		Empty,
		Nosolution
	}

	public class SimplexMethodGeneralIteration
	{
		#region Constructors

		public SimplexMethodGeneralIteration(Matrix a, Matrix c, Matrix xBase, List<int> baseIndexes) : this(a,null,c,xBase,baseIndexes)
		{
			
		}

		public SimplexMethodGeneralIteration(Matrix a,  Matrix b, Matrix c, Matrix xBase, List<int> baseIndexes)
		{
			m_matrixA = a.Copy();
			if (b != null)
			{
				m_matrixB = b.Copy();
			}
			m_matrixC = c.Copy();
			m_xBaseVector = xBase.Copy();
			m_baseIndexes = new List<int>(baseIndexes);
			m_baseIndexes.Sort();
		}

		#endregion

		#region Public methods

		public bool Solve(out Matrix x)
		{
			x = null;
			int iterationsCount = 0;
			while (iterationsCount++ < m_maxIterationsCount)
			{
				Console.WriteLine("{0} iteration:", iterationsCount);
				Console.WriteLine("Matrix A:\n{0}", m_matrixA);
				Console.WriteLine("X0:\n{0}", m_xBaseVector);
				Step6CalculateNewAbInverse();
				Console.WriteLine("Matrix B:\n{0}", m_matrixAbRev.ToString());
				var estimations = Step1GetEstimations();
				if (Step2IsPlanOptimal(estimations))
				{
					x = m_xBaseVector.Copy();
					return true;
				}
				Console.WriteLine("Estimations:\n");
				foreach (var kp in estimations)
				{
					Console.WriteLine("j = {0}, delta{0}={1}", kp.Key, kp.Value);
				}
				var step3Res = Step3GetZAndJ0(estimations);
				if (step3Res == null)
				{
					return false;
				}
				Matrix z = step3Res.Item1;
				Console.WriteLine("Z vector:\n{0}", z);
				int j0 = step3Res.Item2;
				Console.WriteLine("J0={0}", j0);
				var step4Res = Step4GetNewBaseIndex(z);
				int s = step4Res.Item1;
				double tet0 = step4Res.Item2;
				Console.WriteLine("s = {0}", s);
				Console.WriteLine("tet0 = {0}", tet0);
				Step5RecalculateBasePlanAndBasis(j0, z, s, tet0);
				Console.WriteLine("x0 =\n{0}", m_xBaseVector);
				string baseInd = "";
				foreach (var bi in m_baseIndexes)
				{
					baseInd += bi + ", ";
				}
				Console.WriteLine("Jb = {0}", baseInd);
			}
			return false;
		}

		public bool SolveWithoutXBase(out Matrix x)
		{
			int n = m_matrixA.ColumnsCount;
			int m = m_matrixA.RowsCount;
			Matrix cNew = new Matrix(n +m, 1);
			for (int i = 0; i < n + m; i++)
			{
				cNew[i, 0] = i < n ? m_matrixC[i, 0] : -100000000;
			}

			Matrix aNew = new Matrix(m, n + m);
			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j <  n; j++)
				{
					aNew[i, j] = m_matrixA[i, j];
				}
			}
			for (int j=0; j < m; j++)
			{
				aNew[j, j + n] = m_matrixB[j, 0] < 0 ? -1 : 1;
			}

			Matrix xBaseNew = new Matrix(n + m, 1);
			for (int i = 0; i < m_matrixB.RowsCount; i++)
			{
				xBaseNew[i + n, 0] = Math.Abs(m_matrixB[i, 0]);
			}
			List<int> jBNew = Enumerable.Range(n, m).ToList();


			var sm = new SimplexMethodGeneralIteration(aNew, cNew, xBaseNew, jBNew);
			Matrix ansNew = null;
			x = null;
			if (!sm.Solve(out ansNew))
			{
				return false;
			}

			for (int i = n; i < m + n; i++)
			{
				if (!IsEqualToZero(ansNew[i, 0]))
				{
					return false;
				}
			}

			x = new Matrix(n, 1);
			for (int i = 0; i < n; i++)
			{
				x[i, 0] = ansNew[i, 0];
			}
			return true;
		}

		#endregion

		#region Private methods

		private Dictionary<int, double> Step1GetEstimations()
		{
			Matrix cB = new Matrix(m_baseIndexes.Count, 1);
			int tmp = 0;
			for(int i = 0; i < m_matrixC.RowsCount; i++)
			{
				if (m_baseIndexes.Contains(i))
				{
					cB[tmp++, 0] = m_matrixC[i, 0];
				}
			}


			Matrix u = cB.Copy().Transpose().Multiply(m_matrixAbRev).Transpose();

			Console.WriteLine("Vector U:\n{0}", u);

			var estimations = new Dictionary<int, double>();
			for (int i = 0; i < m_matrixC.RowsCount; i++)
			{
				if (!m_baseIndexes.Contains(i))
				{
					double nextEst = u.Copy().Transpose().Multiply(m_matrixA.GetVectorColulmn(i))[0, 0] - m_matrixC[i, 0];
					estimations.Add(i, nextEst);
				}
			}

			return estimations;
		}
		
		private bool Step2IsPlanOptimal(Dictionary<int, double> estimations)
		{
			for (int i = 0; i < m_matrixA.ColumnsCount; i++)
			{
				if (!m_baseIndexes.Contains(i))
				{
					double tmp;
					if (estimations.TryGetValue(i, out tmp) && (!IsEqualToZero(tmp) && tmp < 0))
					{
						return false;
					}
				}
			}
			return true;
		}

		//return (z, j0)
		//return null if no elements matching to condition
		private Tuple<Matrix, int> Step3GetZAndJ0(Dictionary<int, double> estimations)
		{
			for (int i = 0; i < m_matrixA.ColumnsCount; i++)
			{
				if (!m_baseIndexes.Contains(i))
				{
					double tmp;
					if (estimations.TryGetValue(i, out tmp) && (!IsEqualToZero(tmp) && tmp < 0))
					{
						Matrix z = m_matrixAbRev.Copy().Multiply(m_matrixA.GetVectorColulmn(i));
						bool hasPositiveEl = false;
						for (int j = 0; j < m_matrixAbRev.RowsCount; j++)
						{
							hasPositiveEl |= !IsEqualToZero(z[j, 0]) && z[j, 0] > 0;
						}
						if (hasPositiveEl)
						{
							return new Tuple<Matrix, int>(z, i);
						}
					}
				}
			}
			return null;
		}

		//return (s, tet0)
		private Tuple<int, double> Step4GetNewBaseIndex(Matrix z)
		{
			double min = Double.MaxValue;
			int s = -1;
			for (int i = 0; i < z.RowsCount; i++)
			{
				if (!IsEqualToZero(z[i,0]) && z[i,0] > 0 )
				{
					int jI = m_baseIndexes[i];
					double tet0 = m_xBaseVector[jI, 0]/z[i, 0];
					if (tet0 < min)
					{
						min = tet0;
						s = i;
					}
				}
			}
			if (s == -1)
			{
				throw new Exception("Step4: s not founded");
			}
			return new Tuple<int, double>(s, min);
		}

		private void Step5RecalculateBasePlanAndBasis(int j0, Matrix z, int s, double tet0)
		{
			for (int i = 0; i < m_xBaseVector.RowsCount; i++)
			{
				if (!m_baseIndexes.Contains(i) && i != j0)
				{
					m_xBaseVector[i, 0] = 0;
				}
			}
			m_xBaseVector[j0, 0] = tet0;
			for (int i = 0; i < z.RowsCount; i++)
			{
				int jI = m_baseIndexes[i];
				m_xBaseVector[jI, 0] = jI == j0 ? 0 : m_xBaseVector[jI, 0] - tet0*z[i, 0];
			}
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

		private bool IsEqualToZero(double d)
		{
			return -m_eps < d && d < m_eps;
		}

		#endregion

		#region Private fields

		private Matrix m_matrixA;
		private Matrix m_matrixAbRev;
		private Matrix m_matrixB;
		private Matrix m_matrixC;
		private Matrix m_xBaseVector;
		private List<int> m_baseIndexes;

		private const double m_eps = 0.000000001;
		private const int m_maxIterationsCount = 10000;

		#endregion
	}
}
