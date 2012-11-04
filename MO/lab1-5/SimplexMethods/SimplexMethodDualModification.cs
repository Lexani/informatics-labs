using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixOperations;

namespace SimplexMethods
{
	public class SimplexMethodDualModification
	{
		#region Constructors

		public SimplexMethodDualModification(Matrix a, Matrix b, Matrix c, List<int> baseIndexes, Matrix dMin, Matrix dMax)
		{
			m_matrixA = a.Copy();
			m_matrixB = b.Copy();
			m_matrixC = c.Copy();
			m_baseIndexes = new List<int>(baseIndexes);
			m_baseIndexes.Sort();
			m_dMin = dMin.Copy();
			m_dMax = dMax.Copy();
		}

		#endregion

		#region Public methods

		public bool Solve(out Matrix pseudoPlan)
		{
			pseudoPlan = null;

			Step6CalculateNewAbInverse();
			m_yBaseVector = GetFirstBasePlan();
			CalculateBaseEstimations();

			int iterationsCount = 0;
			while (iterationsCount++ < m_maxIterationsCount)
			{
				Console.WriteLine("Iter. no:{0}", iterationsCount);
				Console.WriteLine("Base vector:\n{0}", m_yBaseVector);
				Console.WriteLine("Jb:\n{0}", m_baseIndexes.Aggregate("", (acc, ind) => acc + ind + ", "));

				Step2CalculatePseudoPlan();
				Console.WriteLine("Pseudo plan_b:\n{0}", m_pseudoPlanB);
				if (Step3CheckForOptimum())
				{
					pseudoPlan = BuildFullPlan(m_pseudoPlanB);
					return true;
				}

				int k = Step4FindK();
				int jK = m_baseIndexes[k];
				int mJk;
				Matrix deltaY;
				Dictionary<int, double> estimations;
				Step5GetMVectorAndEstimations(k, out deltaY, out estimations, out mJk);

				KeyValuePair<double, int> step4Res = Step6GetMinimum(estimations);
				double sigma0 = step4Res.Key;
				int j0 = step4Res.Value;
				//step7
				if (sigma0 == Double.MaxValue)
				{
					return false;
				}

				
				Console.WriteLine("Sigma0:\n{0}", sigma0);
				Console.WriteLine("j0:\n{0}", j0);
				Step8RecalculateCoplan(k, jK, estimations, sigma0);
				Step9RecalculatePlan(k, j0);
				Step10(mJk, j0, jK);
				Step6CalculateNewAbInverse();
			}

			return false;
		}

		#endregion

		#region Private methods

		private void CalculateBaseEstimations()
		{
			Matrix yBasePlanT = m_yBaseVector.Transpose();
			m_baseEstimations = new List<double>(m_matrixA.ColumnsCount);
			m_jNotBPlus = new List<int>();
			m_jNotBMinus = new List<int>();
			for (int i = 0; i < m_matrixA.ColumnsCount; i++)
			{
				m_baseEstimations.Add(yBasePlanT.Copy().Multiply(m_matrixA.GetVectorColulmn(i))[0, 0] - m_matrixC[i, 0]);
				if (!m_baseIndexes.Contains(i))
				{
					if (m_baseEstimations[i].IsZero() || m_baseEstimations[i] > 0)
					{
						m_jNotBPlus.Add(i);
					}
					else
					{
						m_jNotBMinus.Add(i);
					}
				}
			}

			

		}

		private Matrix GetFirstBasePlan()
		{
			Matrix cB = new Matrix(m_baseIndexes.Count, 1);
			for (int i = 0; i < m_baseIndexes.Count; i++)
			{
				cB[i, 0] = m_matrixC[m_baseIndexes[i], 0];
			}

			Matrix yBasePlanT = cB.Transpose().Multiply(m_matrixAbRev);
			

			return yBasePlanT.Transpose();
		}

		private Matrix BuildFullPlan(Matrix basePlan)
		{
			Matrix fullPlan = new Matrix(m_matrixA.ColumnsCount, 1);
			for (int i = 0; i < basePlan.RowsCount; i++)
			{
				fullPlan[m_baseIndexes[i], 0] = basePlan[i, 0];
				
			}
			for (int i = 0; i < fullPlan.RowsCount; i++ )
			{
				if (!m_baseIndexes.Contains(i))
				{
					if (m_jNotBMinus.Contains(i))
					{
						fullPlan[i, 0] = m_dMax[i, 0];
					}
					else
					{
						fullPlan[i, 0] = m_dMin[i, 0];
					}
				}
			}

				return fullPlan;
		}

		private void Step2CalculatePseudoPlan()
		{
			Matrix newB = m_matrixB.Copy();
			for (int i = 0; i < m_matrixA.ColumnsCount; i++ )
			{
				if (!m_baseIndexes.Contains(i))
				{
					double tmp = m_jNotBPlus.Contains(i) ? m_dMin[i, 0] : m_dMax[i, 0];
					newB = newB.Add(m_matrixA.GetVectorColulmn(i).Copy().Multiply(tmp).Multiply(-1));
				}
			}

			m_pseudoPlanB = m_matrixAbRev.Copy().Multiply(newB);
		}

		private bool Step3CheckForOptimum()
		{
			bool isOptimum = true;
			for (int i = 0; i < m_pseudoPlanB.RowsCount; i++)
			{
				isOptimum &= (m_dMin[m_baseIndexes[i] ,0] - m_pseudoPlanB[i,0]).IsLessOrEqualZero()
							&& (m_dMax[m_baseIndexes[i], 0] - m_pseudoPlanB[i, 0]).IsGreaterOrEqualZero();
			}
			return isOptimum;
		}

		//return k. 0 <= k < |Jb|. Jb[k] = jK
		private int Step4FindK()
		{
			for (int i = 0; i < m_pseudoPlanB.RowsCount; i++)
			{
				if (!((m_dMin[m_baseIndexes[i] ,0] - m_pseudoPlanB[i,0]).IsLessOrEqualZero()
							&& (m_dMax[m_baseIndexes[i], 0] - m_pseudoPlanB[i, 0]).IsGreaterOrEqualZero()))
				{
					return i;
				}
			}
			throw new Exception("Step 4 failed");
		}

		private void Step5GetMVectorAndEstimations(int k, out Matrix deltaY, out Dictionary<int, double> estimations, out int mJk)
		{
			mJk = (m_pseudoPlanB[k, 0] - m_dMin[m_baseIndexes[k], 0]).IsLessZero() ? 1 : -1;

			estimations = new Dictionary<int, double>();
			deltaY = null;
			//s = -1;
			//int i = 0;
			//while (i < m_pseudoPlanB.RowsCount && s < 0)
			//{
			//    if (m_pseudoPlanB[i, 0] < 0)
			//    {
			//        s = i;
			//    }
			//    i++;
			//}

			//if (s < 0)
			//{
			//    return false;
			//}

			Matrix eS = new Matrix(m_baseIndexes.Count, 1);
			eS[k, 0] = 1;
			deltaY = eS.Copy().Transpose().Multiply(m_matrixAbRev).Transpose().Multiply(mJk);

			bool isCompatible = false;
			for (int j = 0; j < m_matrixA.ColumnsCount; j++)
			{
				if (!m_baseIndexes.Contains(j))
				{
					double mJ = deltaY.Copy().Transpose().Multiply(m_matrixA.GetVectorColulmn(j))[0, 0];
					estimations.Add(j, mJ);
					//isCompatible |= !mJ.IsZero() && mJ < 0;
				}
			}

			//return isCompatible;
		}

		//return (sigma0, j0)
		private KeyValuePair<double, int> Step6GetMinimum(Dictionary<int, double> estimations)
		{
			double sigma0 = Double.MaxValue;
			int j0 = -1;
			foreach (KeyValuePair<int, double> mJ in estimations)
			{
				if ((m_jNotBPlus.Contains(mJ.Key) && mJ.Value.IsLessZero()) 
					|| (m_jNotBMinus.Contains(mJ.Key) && mJ.Value.IsGreaterZero()))
				{
					double sigma = -m_baseEstimations[mJ.Key]/mJ.Value;
					if (sigma < sigma0)
					{
						sigma0 = sigma;
						j0 = mJ.Key;
					}
				}
			}

			return new KeyValuePair<double, int>(sigma0, j0);
		}

		private void Step8RecalculateCoplan(int k, int mJk, Dictionary<int, double> estimations, double sigma0)
		{
			//int mJk = (m_pseudoPlanB[k, 0] - m_dMin[m_baseIndexes[k], 0]).IsLessZero() ? 1 : -1;
			for (int i = 0; i < m_matrixA.ColumnsCount; i++)
			{
				if (m_baseIndexes.Contains(i) && m_baseIndexes[k] != i)
				{
					m_baseEstimations[i] = 0;
				}
				else
				{
					double tmp;
					if (m_baseIndexes[k] == i)
					{
						tmp = sigma0*mJk;
					}
					else
					{
						tmp = sigma0*estimations[i];
					}
					m_baseEstimations[i] = m_baseEstimations[i] + tmp;
				}
			}
				
			//m_yBaseVector.Add(deltaY.Copy().Multiply(sigma0));

			//m_baseIndexes[s] = j0;
			//m_baseIndexes.Sort();
		}

		private void Step9RecalculatePlan(int k, int j0)
		{
			m_baseIndexes[k] = j0;
			m_baseIndexes.Sort();
		}

		private void Step10(int mJk, int j0, int jK)
		{
			if (mJk == 1)
			{
				if (m_jNotBPlus.Contains(j0))
				{
					m_jNotBPlus.Remove(j0);
					m_jNotBPlus.Add(jK);
				}
				else
				{
					m_jNotBPlus.Add(jK);
				}
			}
			else
			{
				if (m_jNotBPlus.Contains(j0))
				{
					m_jNotBPlus.Remove(j0);
				}
			}
			m_jNotBMinus.Clear();
			for (int i=0; i < m_matrixA.ColumnsCount; i++)
			{
				if (!m_baseIndexes.Contains(i) && !m_jNotBPlus.Contains(i))
				{
					m_jNotBMinus.Add(i);
				}
			}
		}

		private void Step6CalculateNewAbInverse()
		{
			Matrix aB = new Matrix(m_matrixA.RowsCount, m_baseIndexes.Count);
			int tmp = 0;

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
		private List<double> m_baseEstimations;
		private Matrix m_dMin;
		private Matrix m_dMax;
		private List<int> m_jNotBPlus;
		private List<int> m_jNotBMinus;

		private const int m_maxIterationsCount = 10000;

		#endregion
	}
}
