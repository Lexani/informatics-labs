using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixOperations;
using SimplexMethods;

namespace SimplexMethod
{
	class Program
	{
		//page 13
		//ans = [0, 1, 0, 3]
		public static void ExampleGenIt1()
		{
			Matrix a = new Matrix(2, 4);
			a[0, 0] = 3; a[0, 1] = 1; a[0, 2] = 1; a[0, 3] = 0;
			a[1, 0] = 1; a[1, 1] = -2; a[1, 2] = 0; a[1, 3] = 1;
			Matrix c = new Matrix(4, 1);
			c[0, 0] = 1;
			c[1, 0] = 4;
			c[2, 0] = 1;
			c[3, 0] = -1;
			Matrix xBase = new Matrix(4, 1);
			xBase[0, 0] = 0;
			xBase[1, 0] = 0;
			xBase[2, 0] = 1;
			xBase[3, 0] = 1;
			List<int> baseIndexes = new List<int>(new[] { 2, 3 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, ans.ToString());
		}

		//by S.Majonov
		public static void ExampleGenIt2()
		{
			//ans = (5,1,3,0,0)
			Matrix a = MatrixGenerator.From(
				new double[,]{{1, 7, -1, 7, -8},
							  {0, 1, 8, 9, 7},
							  {0, 0, 1, 1, 1}});
			Matrix c = MatrixGenerator.From(new double[,] {{1, 2, 1, 3, -2}}).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] {{3, 0, 0, 2, 1}}).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 3, 4 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}
		
		//from pt
		public static void ExampleGenIt3()
		{
			Matrix a = MatrixGenerator.From(
				new double[,]{{1, 2, -1, 3, 1, 0, 0},
							  {1, -1, 1, 1, 0, 1, 0},
							  {0, 1, 0, 2, 0, 0, 1}});
			Matrix c = MatrixGenerator.From(new double[,] { { 0, 0, 0, 0, -1, -1, -1 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 0, 0, 0, 0, 1, 0, 1 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 4,5,6 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//from p.15/task5/ex1
		public static void ExampleGenIt4()
		{
			//1 0 0 4
			Matrix a = MatrixGenerator.From(
				new double[,]{
							  {1, 4, 4, 1},
							  {1, 7, 8, 2}});
			Matrix c = MatrixGenerator.From(new double[,] { { 1, -3, -5, -1 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 1, 0, 1, 0 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0,2 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//from p.15/task5/ex2
		public static void ExampleGenIt5()
		{
			//2 0 3 0
			Matrix a = MatrixGenerator.From(
				new double[,]{
							  {1, 3, 1, 2},
							  {2, 0,-1, 1}});
			Matrix c = MatrixGenerator.From(new double[,] { { 1,  1,  1,  1 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 0, 1, 0, 1 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 1, 3 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//from p.15/task5/ex3
		public static void ExampleGenIt6()
		{
			//det = 0
			Matrix a = MatrixGenerator.From(
				new double[,]{
							  {3,1,2,6,9,3},
							  {1,2,-1,2,3,1}});
			Matrix c = MatrixGenerator.From(new double[,] { {-2,1,1,-1,4,1 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { {1,0,0,0,0,4 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0,5 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//by Ftkvyn
		public static void ExampleGenIt7()
		{
			//ans =  [0 4/3 6 13/3 0 0]
			Matrix a = MatrixGenerator.From(
				new double[,]{{5, 2,  3, 1,  0, 0},
							  {1,6,2,0,1,0},
							  {4,0,3,0,0,1}});
			Matrix c = MatrixGenerator.From(new double[,] { { 6,5,9,0,0,0} }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 0,0,0,25,20,18 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 3,4,5 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//by Ftkvyn
		public static void ExampleGenIt8()
		{
			//ans = [0 80 200 0 0]
			Matrix a = MatrixGenerator.From(
				new double[,]{{5, 10, 6, 1, 0},
							  {4, 5, 8, 0, 1}});
			Matrix c = MatrixGenerator.From(new double[,] { { 1, 3, 3, 0, 0 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 0, 0, 0, 2000, 2000 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 3, 4 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//by Ftkvyn
		public static void ExampleGenIt9()
		{
			//ans = [0 0 0 5 0.5 4.5]
			Matrix a = MatrixGenerator.From(
				new double[,]{{0, 2, 0, 1, -2, 0},
							  {0, 3, 2, 0, -3, 1},
							  {1, 4, 1, 0, 4, 0}});
			Matrix c = MatrixGenerator.From(new double[,] { { -3, 2, 4, 5, 1, 6 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { {2, 0, 0, 4, 0, 3 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 3, 5 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//by Ftkvyn
		public static void ExampleGenIt10()
		{
			//ans = [0 0 0 3,(5) 14,(4) 2,(3)]
			//by ftkvyn ans=[]
			Matrix a = MatrixGenerator.From(
				new double[,]{{1, 0, 0, 2, -1, 4},
							  {0, 1, 0, -3, 0, 5},
							  {0, 0, 1, 1, 1, -6}});
			Matrix c = MatrixGenerator.From(new double[,] { { 1, 1, 1, 2, 4, -2 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 2, 1, 4, 0, 0, 0 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 1, 2 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//on Lab Ex. 5.172
		public static void ExampleGenIt11()
		{
			//no ans
			Matrix a = MatrixGenerator.From(
				new double[,]{{0, 1, 1,-2, 7},
							  {1, 0, 1,-2,-6},
							  {1, 1, 0,-2, 7}});
			Matrix c = MatrixGenerator.From(new double[,] { { 1,2,3,4,5 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 1,1,1, 0, 0 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 1, 2 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//on Lab Ex. 5.139
		public static void ExampleGenIt12()
		{
			//[3 0 7 0 5 0 5 0]
			Matrix a = MatrixGenerator.From(
				new double[,]{{0,3,1,0,1,3,1,5},
							  {1,6,0,0,2,3,2,5},
							  {1,4,2,-2,0,5,2,11},
							  {1,2,2,2,2,7,0,7}});
			Matrix c = MatrixGenerator.From(new double[,] { { 1, 2, 1, -2, 1, 2, 1, -2 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 0,2,0,1,0,2,0,1} }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 1,3,5,7 });
			var sm = new SimplexMethodGeneralIteration(a, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//on Lab Ex. 5.139 with modification
		public static void ExampleGenIt13()
		{
			Matrix a = MatrixGenerator.From(
				new double[,]{{0,3,1,0,1,3,1,5},
							  {1,6,0,0,2,3,2,5},
							  {1,4,2,-2,0,5,2,11},
							  {1,2,2,2,2,7,0,7}});
			Matrix b = MatrixGenerator.From(new double[,] { { 17, 23, 27, 27 } }).Transpose();
			Matrix c = MatrixGenerator.From(new double[,] { { 1, 2, 1, -2, 1, 2, 1, -2 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 0, 2, 0, 1, 0, 2, 0, 1 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 1, 3, 5, 7 });
			var sm = new SimplexMethodGeneralIteration(a, b, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.SolveWithoutXBase(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//on Lab Ex. 5.139 with modification
		public static void ExampleGenIt14()
		{
			//[3 0 7 0 5 0 5 0]
			Matrix a = MatrixGenerator.From(
				new double[,]{{1,1,-1,1},
							  {1,14,10,-10}});
			Matrix b = MatrixGenerator.From(new double[,] { { 2, 24 } }).Transpose();
			Matrix c = MatrixGenerator.From(new double[,] { { 1, 2, 3, - 4 } }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 0, 0, 0, 0 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 0 });
			var sm = new SimplexMethodGeneralIteration(a, b, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.SolveWithoutXBase(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		public static void ExampleGenIt15()
		{
			//[3 0 7 0 5 0 5 0]
			Matrix a = MatrixGenerator.From(
				new double[,]{{4,3,2,-1},
							  {1,-2,-5,-3}});
			Matrix b = MatrixGenerator.From(new double[,] { { 7, -12 } }).Transpose();
			Matrix c = MatrixGenerator.From(new double[,] { { 3,7,6,5} }).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,] { { 0, 0, 0, 0 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 0 });
			var sm = new SimplexMethodGeneralIteration(a, b, c, xBase, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.SolveWithoutXBase(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		// #7.40
		public static void ExampleDual1()
		{

			var a = MatrixGenerator.From(new double[,] {{1, 1, -3}, {1, -2, 1}});
			var b = MatrixGenerator.From(new double[,] { { 0, 3 } }).Transpose();
			var c = MatrixGenerator.From(new double[,] {{ 1, 1, -4 }}).Transpose();
			List<int> baseIndexes = new List<int>(new[] {0, 1});

			var sm = new SimplexMethodDual(a, b, c, baseIndexes);
			Matrix y0 = null;
			Matrix pseudo = null;
			bool isSolved = sm.Solve(out y0, out pseudo);
			Console.WriteLine("IsSolved: {0}\nSolution:\nY0\n{1}\nPseudo:\n{2}\n Targ func:{3}", isSolved, isSolved ? y0.ToString() : "", isSolved ? pseudo.ToString() : "", isSolved ? CalculateTargetFunc(c, pseudo).ToString() : "");
		}

		// #7.41
		public static void ExampleDual2()
		{

			var a = MatrixGenerator.From(new double[,] { { 2, 6, -8, -30 }, { 1, -1, 1, 1 } });
			var b = MatrixGenerator.From(new double[,] { { 6, -5 } }).Transpose();
			var c = MatrixGenerator.From(new double[,] { { 1, -7, -5, -35 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 1 });

			var sm = new SimplexMethodDual(a, b, c, baseIndexes);
			Matrix y0 = null;
			Matrix pseudo = null;
			bool isSolved = sm.Solve(out y0, out pseudo);
			Console.WriteLine("IsSolved: {0}\nSolution:\nY0\n{1}\nPseudo:\n{2}\n Targ func:{3}", isSolved, isSolved ? y0.ToString() : "", isSolved ? pseudo.ToString() : "", isSolved ? CalculateTargetFunc(c, pseudo).ToString() : "");
		}
		// #7.42
		public static void ExampleDual3()
		{

			var a = MatrixGenerator.From(new double[,] { { 1, 1, 0, -4, 2 }, { 0, 1, 1, 1, -2 }, { 1, 0, 1, 3, -2 } });
			var b = MatrixGenerator.From(new double[,] { { -2, -2, -2 } }).Transpose();
			var c = MatrixGenerator.From(new double[,] { { 1, 1, 1, 0, -1 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 1, 2 });

			var sm = new SimplexMethodDual(a, b, c, baseIndexes);
			Matrix y0 = null;
			Matrix pseudo = null;
			bool isSolved = sm.Solve(out y0, out pseudo);
			Console.WriteLine("IsSolved: {0}\nSolution:\nY0\n{1}\nPseudo:\n{2}\n Targ func:{3}", isSolved, isSolved ? y0.ToString() : "", isSolved ? pseudo.ToString() : "", isSolved ? CalculateTargetFunc(c, pseudo).ToString() : "");
		}
		// #7.43
		public static void ExampleDual4()
		{

			var a = MatrixGenerator.From(new double[,] { { 1, 8, -1, 10, 14, 24 }, { 0, 1, -7, 8, -5, 3 }, { 0, 0, 1, -1, 1, 0 } });
			var b = MatrixGenerator.From(new double[,] { { 4, 28, -4 } }).Transpose();
			var c = MatrixGenerator.From(new double[,] { { 3, 2, 1, 1, -5, -10 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 1, 2 });

			var sm = new SimplexMethodDual(a, b, c, baseIndexes);
			Matrix y0 = null;
			Matrix pseudo = null;
			bool isSolved = sm.Solve(out y0, out pseudo);
			Console.WriteLine("IsSolved: {0}\nSolution:\nY0\n{1}\nPseudo:\n{2}\n Targ func:{3}", isSolved, isSolved ? y0.ToString() : "", isSolved ? pseudo.ToString() : "", isSolved ? CalculateTargetFunc(c, pseudo).ToString() : "");
		}

		public static void ExampleDual1Mod()
		{

			var a = MatrixGenerator.From(new double[,] { { 1, 1, -3 }, { 1, -2, 1 } });
			var b = MatrixGenerator.From(new double[,] { { 0, 3 } }).Transpose();
			var c = MatrixGenerator.From(new double[,] { { 1, 1, -4 } }).Transpose();

			var dMin = MatrixGenerator.From(new double[,] {{-50, -50, -50}}).Transpose();
			var dMax = MatrixGenerator.From(new double[,] {{10, 10, 10}}).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0,1 });

			var sm = new SimplexMethodDualModification(a, b, c, baseIndexes, dMin, dMax);
			Matrix pseudo = null;
			bool isSolved = sm.Solve(out pseudo);
			PrintSolution(isSolved, c, pseudo);
		}

		public static void ExampleDual4Mod()
		{

			var a = MatrixGenerator.From(new double[,] { { 1, 8, -1, 10, 14, 24 }, { 0, 1, -7, 8, -5, 3 }, { 0, 0, 1, -1, 1, 0 } });
			var b = MatrixGenerator.From(new double[,] { { 4, 28, -4 } }).Transpose();
			var c = MatrixGenerator.From(new double[,] { { 3, 2, 1, 1, -5, -10 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 1, 2 });
			var dMin = MatrixGenerator.From(new double[,] { { -5, -5, -5, -5, -5, -5 } }).Transpose();
			var dMax = MatrixGenerator.From(new double[,] { { 10, 10, 10, 10, 10, 10 } }).Transpose();

			var sm = new SimplexMethodDualModification(a, b, c, baseIndexes, dMin, dMax);
			Matrix pseudo = null;
			bool isSolved = sm.Solve(out pseudo);
			PrintSolution(isSolved, c, pseudo);
		}

		public static void ExampleDual5Mod()
		{

			var a = MatrixGenerator.From(new double[,] { { 1,0,1,1,3,1,4 }, 
														 { 0, 1, 1, 0, 2, 0, 5 }, 
														 {  2,0,1,-4,1,1,-1} });
			//var x = MatrixGenerator.From(new double[,] {{1,-1,-2,3.6,2,4,1.6}}).Transpose();
			var x = MatrixGenerator.From(new double[,] {{3,-1,-2,4,1,3,2}}).Transpose();
			var b = a.Copy().Multiply(x);
			//var b = MatrixGenerator.From(new double[,] { { 4, 28, -4 } }).Transpose();
			var c = MatrixGenerator.From(new double[,] { { 10, 0, -1, 6, 6, 2, 8 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 1,2,3});
			//var dMin = MatrixGenerator.From(new double[,] { { -1, -1, -2, -3, -1, -2, -1 } }).Transpose().Multiply(10);
			var dMin = MatrixGenerator.From(new double[,] { { -1, -1, -2, -3, -1, 2, 1 } }).Transpose();
			var dMax = MatrixGenerator.From(new double[,] { { 3,4,5,4,2,4,5 } }).Transpose();

			var sm = new SimplexMethodDualModification(a, b, c, baseIndexes, dMin, dMax);
			Matrix pseudo = null;
			bool isSolved = sm.Solve(out pseudo);
			PrintSolution(isSolved, c, pseudo);
		}

		public static void ExampleDual6Mod()
		{

			var a = MatrixGenerator.From(new double[,] { { -0.0114, -0.00741, -0.02541, 0, 1, 0 }, 
														 { 0.00138, 0.000375, -0.001625, -1, 0, 0 }, 
														 {  -0.0018, -0.00275, -0.00475, 0, 0, 1} });
			//var x = MatrixGenerator.From(new double[,] {{1,-1,-2,3.6,2,4,1.6}}).Transpose();
			var x = MatrixGenerator.From(new double[,] { { 1, 1, 1, 0, 0, 0 } }).Transpose();
			var b = a.Copy().Multiply(x);
			//var b = MatrixGenerator.From(new double[,] { { 4, 28, -4 } }).Transpose();
			var c = MatrixGenerator.From(new double[,] { { 7080, 5100, 4010,0,0,0 } }).Transpose();
			List<int> baseIndexes = new List<int>(new[] { 0, 1, 2 });
			//var dMin = MatrixGenerator.From(new double[,] { { -1, -1, -2, -3, -1, -2, -1 } }).Transpose().Multiply(10);
			var dMin = MatrixGenerator.From(new double[,] { { 0,0,0,0,0,0} }).Transpose();
			var dMax = MatrixGenerator.From(new double[,] { { 9,9,9,9,9,9 } }).Transpose();

			var sm = new SimplexMethodDualModification(a, b, c, baseIndexes, dMin, dMax);
			Matrix pseudo = null;
			bool isSolved = sm.Solve(out pseudo);
			PrintSolution(isSolved, c, pseudo);
		}

		static void Main(string[] args)
		{
			try
			{
                Console.WriteLine("========== ЛР №1. Симплекс-метод ==========");
                ExampleGenIt1();
                Console.WriteLine("\n\n========== ЛР №2. Двойственный симплекс-метод ==========");
                ExampleDual1();
                Console.WriteLine("\n\n========== ЛР №3. Двойственный симплекс-метод (модификация) ==========");
				ExampleDual6Mod();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: " + e.Message);
			}
		}

		static void PrintSolution(bool isSolved, Matrix c, Matrix sol)
		{
			Console.WriteLine("IsSolved: {0}\nSolution:\nPseudo:\n{1}\n Targ func:{2}", isSolved, isSolved ? sol.ToString() : "", isSolved ? CalculateTargetFunc(c, sol).ToString() : "");
		}

		static double CalculateTargetFunc(Matrix c, Matrix x)
		{
			double ans = 0;
			for (int i = 0; i < c.RowsCount; i++)
			{
				ans += c[i, 0] * x[i, 0];
			}
			return ans;
		}
	}
}
