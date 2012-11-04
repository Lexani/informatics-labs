using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixOperations;

namespace QuadraticProgramming
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
			Matrix d = new Matrix(4, 4);
			Matrix xBase = new Matrix(4, 1);
			xBase[0, 0] = 0;
			xBase[1, 0] = 0;
			xBase[2, 0] = 1;
			xBase[3, 0] = 1;
			List<int> baseIndexes = new List<int>(new[] { 2, 3 });
			var sm = new QuadraticProgrammingProblem(a, c, d, xBase, baseIndexes, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			PrintAns(isSolved, ans, c, d);
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
			List<int> baseIndexes = new List<int>(new[] { 0, 2 });
			Matrix d = new Matrix(4,4);
			var sm = new QuadraticProgrammingProblem(a, c, d, xBase, baseIndexes, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}", isSolved, isSolved ? ans.ToString() : "");
		}

		//on pt
		public static void Example1()
		{
			Matrix a = MatrixGenerator.From(new double[,]
			                                	{
													{1, 2, -1,  4, 0, 1,  3, -6, 0},
													{0, 3,  2, -3, 1, 0,  2,  1, 0},
													{0, 1, -2,  0, 0, 1, -4,  5, 1}
			                                	});
			Matrix bHelp = MatrixGenerator.From(new double[,]
			                                	{
													{ 1, 1, 1,  1, 1, 1,  1,  1, 1},
													{-2, 1, 2, -1, 3, 2, -2, -3, 1},
													{ 0, 4, 0, -4, 4, 2  ,1, -2, 0}
			                                	});
			Matrix aHelp = MatrixGenerator.From(new double[,]
			                                	{
													{9,1,5}
			                                	}).Transpose();
			Matrix b = MatrixGenerator.From(new double[,]
			                                	{
			                                		{4, 6, 2},
			                                	}).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,]
			                                	{
			                                		{4, 0, 0, 0, 6, 0, 0, 0,2},
			                                	}).Transpose();
			Matrix c = aHelp.Copy().Transpose().Multiply(bHelp).Transpose().Multiply(-1);
			Matrix d = bHelp.Copy().Transpose().Multiply(bHelp);
			List<int> baseIndexes = new List<int>(new[] { 0,4,8 });
			var sm = new QuadraticProgrammingProblem(a, c, d, xBase, baseIndexes, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			PrintAns(isSolved, ans, c, d);
			Console.WriteLine("A*x - b:\n{0}", a.Copy().Multiply(ans).Add(b.Copy().Multiply(-1)));
		}

		//on pt
		public static void Example2()
		{
			Matrix a = MatrixGenerator.From(new double[,]
			                                	{
													{0,2,-3,4,1,7,1,0},
													{0,1,1,8,0,-8,1,1},
													{-1,4,-3,5,0,-11,1,0}
			                                	});
			Matrix bHelp = MatrixGenerator.From(new double[,]
			                                	{
													{ 1,2,1,2,-2,-2,1,3},
													{0,1,0,1,0,1,0,1},
													{3,2,1,4,5,-6,-7,2}
			                                	});
			Matrix aHelp = MatrixGenerator.From(new double[,]
			                                	{
													{6,4,4}
			                                	}).Transpose();
			Matrix b = MatrixGenerator.From(new double[,]
			                                	{
			                                		{12,4,-5},
			                                	}).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,]
			                                	{
			                                		{5,0,0,0,12,0,0,4},
			                                	}).Transpose();
			Matrix c = aHelp.Copy().Transpose().Multiply(bHelp).Transpose().Multiply(-1);
			Matrix d = bHelp.Copy().Transpose().Multiply(bHelp);
			List<int> baseIndexes = new List<int>(new[] { 0,4,7 });
			var sm = new QuadraticProgrammingProblem(a, c, d, xBase, baseIndexes, baseIndexes);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			PrintAns(isSolved, ans, c, d);
			Console.WriteLine("A*x - b:\n{0}", a.Copy().Multiply(ans).Add(b.Copy().Multiply(-1)));
		}

		//on pt
		public static void Example3()
		{
			Matrix a = MatrixGenerator.From(new double[,]
			                                	{
													{1,0,2,0,1,2,-4,1},
													{0,-1,-8,0,-1,0,3,2},
													{0,0,3,1,-1,1,-3,1}
			                                	});
			Matrix bHelp = MatrixGenerator.From(new double[,]
			                                	{
													{1,3,0,-8,1,1,2,0},
													{2,-1,1,-5,0,1,-1,0},
													{4,0,0,1,0,1,-4,0}
			                                	});
			Matrix aHelp = MatrixGenerator.From(new double[,]
			                                	{
													{0,-3,2}
			                                	}).Transpose();
			Matrix b = MatrixGenerator.From(new double[,]
			                                	{
			                                		{3,-5,2},
			                                	}).Transpose();
			Matrix xBase = MatrixGenerator.From(new double[,]
			                                	{
			                                		{.329141993484944, .047415684987319, .593510086504827, 0, .377058616418933, .510250860182017, 0, .086277496722435}
			                                		//{3,5,0,2,0,0,0,0},
			                                	}).Transpose();
			Matrix c = aHelp.Copy().Transpose().Multiply(bHelp).Transpose().Multiply(-1);
			Matrix d = bHelp.Copy().Transpose().Multiply(bHelp);

			//Matrix c = MatrixGenerator.From(new double[,]
			//                                    {
			//                                        {0,0,0,0,0,0,0,0}
			//                                    }).Transpose();
			//Matrix d = Matrix.UnityMatrixE(8);

			List<int> baseIndexes = new List<int>(new[] { 0, 2, 5 });
			List<int> baseStar = new List<int>(new[] { 0, 1, 2, 4, 5,7 });
			var sm = new QuadraticProgrammingProblem(a, c, d, xBase, baseIndexes, baseStar);
			Matrix ans = null;
			bool isSolved = sm.Solve(out ans);
			PrintAns(isSolved, ans, c, d);
			Console.WriteLine("Ax:\n{0}", a.Copy().Multiply(ans));
			Console.WriteLine("Bx:\n{0}", bHelp.Copy().Multiply(ans));
		}

		static void PrintAns(bool isSol, Matrix x, Matrix c, Matrix d)
		{
			Console.WriteLine("IsSolved: {0}\nSolution:\n{1}Targ. func:\n{2}", isSol, x, CalculateTargetFunc(x, c, d));
		}

		static double CalculateTargetFunc(Matrix x, Matrix c, Matrix d)
		{
			return c.Copy().Transpose().Multiply(x).Add(x.Copy().Transpose().Multiply(d).Multiply(x).Multiply(0.5))[0, 0];
		}

		static void Main(string[] args)
		{
			Example3();
			//Example1();
			//Example2();
		}
	}
}
