using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixOperations
{
	public class Matrix
	{
		#region Contructors

		public Matrix(int rows, int columns)
		{
			ChangeDimension(rows, columns);
		}

		#endregion

		#region Public properties

		public string LastError { get; set; }

		public int RowsCount
		{
			get { return m_data.GetLength(0); }
			private set { }
		}

		public int ColumnsCount
		{
			get { return m_data.GetLength(1); }
			private set { }
		}

		public double this[int row, int col]
		{
			get
			{
				CheckIndexWithThrowException(row, col);
				return m_data[row, col];
			}
			set
			{
				CheckIndexWithThrowException(row, col);
				m_data[row, col] = value;
			}
		}

		#endregion

		#region Public methods

		public Matrix Add(Matrix m)
		{
			if (!(m.RowsCount == RowsCount && m.ColumnsCount == ColumnsCount))
			{
				throw new ArgumentException("Matrix dimension must be the same");
			}
			for (int i = 0; i < RowsCount; i++)
			{
				for (int j = 0; j < ColumnsCount; j++)
				{
					m_data[i, j] += m[i, j];
				}
			}
			return this;
		}

		public Matrix Multiply(double d)
		{
			for (int i = 0; i < RowsCount; i++)
			{
				for (int j = 0; j < ColumnsCount; j++)
				{
					m_data[i, j] *= d;
				}
			}
			return this;
		}

		public Matrix Multiply(Matrix m)
		{
			if (ColumnsCount != m.RowsCount)
			{
				throw new ArgumentException("Matrix dimension wrong");
			}
			var result = new double[RowsCount,m.ColumnsCount];
			for (int i = 0; i < RowsCount; i++)
			{
				for (int j = 0; j < m.ColumnsCount; j++)
				{
					double tmp = 0;
					for (int z = 0; z < ColumnsCount; z++)
					{
						tmp += m_data[i, z]*m[z, j];
					}
					result[i, j] = tmp;
				}
			}
			m_data = result;
			return this;
		}

		public Matrix Transpose()
		{
			var trMatrix = new double[ColumnsCount,RowsCount];
			for (int i = 0; i < RowsCount; i++)
			{
				for(int j = 0; j < ColumnsCount; j++)
				{
					trMatrix[j, i] = m_data[i, j];
				}
			}
			m_data = trMatrix;
			return this;
		}

		public Matrix Invert()
		{
			if (RowsCount != ColumnsCount)
			{
				throw new ArgumentException("Dimension invalid");
			}
			Matrix bLast = Matrix.UnityMatrixE(RowsCount);
			List<int> usedColumns = new List<int>();
			for (int i = 0; i < RowsCount; i++)
			{
				int curCol = -1;
				double alpha = 0;
				do
				{
					curCol = GetNextUnusedColumn(usedColumns, curCol);
					if (curCol < 0)
					{
						break;
					}
					Matrix ek = Matrix.UnityVectorColumn(RowsCount, i).Transpose();
					//Matrix ek = Matrix.UnityVectorColumn(RowsCount, curCol).Transpose();
					Matrix ck = GetVectorColulmn(curCol);
					alpha = ek.Multiply(bLast).Multiply(ck)[0, 0];

				} while (IsEqualZero(alpha) && curCol >= 0);
				if (IsEqualZero(alpha))
				{
					LastError = "Det=0";
					throw new ArgumentException(LastError);
				}
				usedColumns.Add(curCol);
				//Calc D(k,z) | z = B*ck
				Matrix dHelpMatr = Matrix.UnityMatrixE(RowsCount);
				Matrix zHelpMatr = bLast.Copy().Multiply(GetVectorColulmn(curCol));
				double tmp = zHelpMatr[i, 0];
				//double tmp = zHelpMatr[curCol, 0];
				//zHelpMatr[curCol, 0] = 1;
				zHelpMatr[i, 0] = 1;
				zHelpMatr.Multiply(-1/tmp);
				for (int j = 0; j < RowsCount; j++)
				{
					dHelpMatr[j, i] = zHelpMatr[j, 0];
					//dHelpMatr[j, curCol] = zHelpMatr[j, 0];
				}
				bLast = dHelpMatr.Multiply(bLast);
			}

			for (int i = 0; i < RowsCount; i++ )
			{
				int newCol = usedColumns[i];
				for (int j = 0; j < ColumnsCount; j++)
				{
					m_data[newCol, j] = bLast[i, j];
				}
			}
			return this.Multiply(-1);
		}

		public Matrix GetVectorRow(int row)
		{
			CheckIndexWithThrowException(row, 0);
			var vec = Matrix.UnityVectorColumn(ColumnsCount, 0).Transpose();
			for (int i = 0; i < ColumnsCount; i++)
			{
				vec[0, i] = m_data[row, i];
			}
			return vec;
		}

		public Matrix GetVectorColulmn(int col)
		{
			CheckIndexWithThrowException(0, col);
			var vec = Matrix.UnityVectorColumn(RowsCount, 0);
			for (int i = 0; i < RowsCount; i++)
			{
				vec[i, 0] = m_data[i, col];
			}
			return vec;
		}

		public override string ToString()
		{
			var sb = new StringBuilder(RowsCount*ColumnsCount*5);
			for (int i = 0; i < RowsCount; i++)
			{
				for (int j = 0; j < ColumnsCount; j++)
				{
					sb.AppendFormat("{0,7}|", Math.Round(m_data[i, j], 7));
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		public Matrix Copy()
		{
			var copy = new Matrix(RowsCount, ColumnsCount);
			for (int i = 0; i < RowsCount; i++)
			{
				for (int j = 0; j < ColumnsCount; j++)
				{
					copy[i, j] = m_data[i, j];
				}
			}
			return copy;
		}

		#endregion

		#region Public static methods

		public static Matrix UnityMatrixE(int dim)
		{
			var e = new Matrix(dim, dim);
			for (int i = 0; i < dim; i++)
			{
				e[i, i] = 1;
			}
			return e;
		}

		public static Matrix UnityVectorColumn(int dim, int k)
		{
			var e = new Matrix(dim, 1);
			e[k, 0] = 1;
			return e;
		}

		#endregion

		#region Private methods

		private void ChangeDimension(int rowsCount, int colsCount)
		{
			var newData = new double[rowsCount, colsCount];
			for (int i = 0; i < Math.Min(RowsCount, rowsCount); i++)
			{
				for (int j = 0; j < Math.Min(ColumnsCount, colsCount); j++)
				{
					newData[i, j] = m_data[i, j];
				}
			}
			m_data = newData;
		}

		private void CheckIndexWithThrowException(int row, int col)
		{
			if (RowsCount - 1 < row || ColumnsCount - 1 < col)
			{
				throw new ArgumentException("invalid [i,j]");
			}
		}

		private int GetNextUnusedColumn(List<int> used, int offset = -1)
		{
			int next = offset + 1;
			if (next >= ColumnsCount)
			{
				return -1;
			}
			if (next == 0 && !used.Contains(next))
			{
				return next;
			}
			next = Enumerable.Range(next, ColumnsCount).Where(x => !used.Contains(x)).FirstOrDefault();
			return next != 0 ? next : -1;
		}

		private bool IsEqualZero(double x)
		{
			return Math.Abs(x) <= m_eps;
		}

		#endregion

		#region Private fields

		private readonly double m_eps = 0.0000000001;
		private double[,] m_data = new double[10, 10];

		#endregion

	}
}
