using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixOperations
{
	public static class MathHelper
	{
		static MathHelper()
		{
			Eps = 0.0000000000001;
		}

		public static double Eps { get; set; }

		public static bool IsZero(this double d)
		{
			return -Eps < d && d < Eps;
		}

		public static bool IsGreaterOrEqualZero(this double d)
		{
			return d.IsZero() || d.IsGreaterZero();
		}

		public static bool IsLessOrEqualZero(this double d)
		{
			return d.IsZero() || d.IsLessZero();
		}

		public static bool IsGreaterZero(this double d)
		{
			return d > Eps;
		}

		public static bool IsLessZero(this double d)
		{
			return d < -Eps;
		}
	}
}
