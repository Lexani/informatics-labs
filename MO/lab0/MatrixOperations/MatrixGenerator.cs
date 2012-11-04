using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixOperations
{
	public class MatrixGenerator
	{
		public static Matrix From(double[,] matrix)
		{
			var m = new Matrix(matrix.GetLength(0), matrix.GetLength(1));
			for (int i = 0; i < m.RowsCount; i++)
			{
				for (int j = 0; j < m.ColumnsCount; j++)
				{
					m[i, j] = matrix[i, j];
				}
			}

			return m;
		}
	}
}
