using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixOperations;

namespace TransportationProblems
{
	class Program
	{

		//from inet. res = 25
		static void Example0()
		{
			List<double> a = new List<double>(new double[] { 2, 4, 6 });
			List<double> b = new List<double>(new double[] { 6, 3, 3 });
			Matrix c = MatrixGenerator.From(new double[,]
			                                	{
			                                		{2, 3, 5},
			                                		{3, 2, 4},
													{5, 1, 2}
			                                	});
			var trProblem = new MatrixTransportationProblem(a, b, c);

			Dictionary<Tuple<int, int>, double> sol;
			bool flag = trProblem.Solve(out sol);
			PrintRes(flag, sol, c);
		}

		static void Example1()
		{
			List<double> a = new List<double>(new double[] { 5, 9, 12, 5, 16 });
			List<double> b = new List<double>(new double[] { 6, 3, 11, 7, 8, 8, 4 });
			Matrix c = MatrixGenerator.From(new double[,]
			                                	{
			                                		{1, 2, 3, 4, 0, 1, 1},
			                                		{3, 0, 3, 0, 4, 1, 1},
			                                		{3, 2, 0, 1, 4, 0, 1},
			                                		{3, 2, 0, 1, 4, 9, 1},
			                                		{0, 0, 0, 1, 0, 43, 0},
			                                	});
			var trProblem = new MatrixTransportationProblem(a, b, c);

			Dictionary<Tuple<int, int>, double> sol;
			bool flag = trProblem.Solve(out sol);
			PrintRes(flag, sol, c);
		}

		static void Example2()
		{
			List<double> a = new List<double>(new double[] { 180, 55, 80, 65, 135 });
			List<double> b = new List<double>(new double[] { 130, 75, 65, 60, 75, 60 });
			Matrix c = MatrixGenerator.From(new double[,]
			                                	{
			                                		{ 6, 6, 8, 5, 4, 3 }, 
													{ 2, 4, 3, 9, 8, 5 },
													{ 3, 5, 7, 9, 6, 11 }, 
													{ 3, 5, 4, 4, 2, 1 },
													{ 2, 5, 6, 3, 2, 8 },
			                                	});
			var trProblem = new MatrixTransportationProblem(a, b, c);

			Dictionary<Tuple<int, int>, double> sol;
			bool flag = trProblem.Solve(out sol);
			PrintRes(flag, sol, c);
		}

		//from inet. res 78
		static void Example3()
		{
			List<double> a = new List<double>(new double[] { 6,8,10, 2 });
			List<double> b = new List<double>(new double[] { 4,6,8,8 });
			Matrix c = MatrixGenerator.From(new double[,]
			                                	{
			                                		{ 1,2,4,3 }, 
													{ 4,3,8,5 },
													{ 2,7,6,3 }, 
													{ 0,0,0,0 }, 
			                                	});
			var trProblem = new MatrixTransportationProblem(a, b, c);

			Dictionary<Tuple<int, int>, double> sol;
			bool flag = trProblem.Solve(out sol);
			PrintRes(flag, sol, c);
		}

		//on pt task1
		static void Example4()
		{
			List<double> a = new List<double>(new double[] { 1,1,1,1,1,1 });
			List<double> b = new List<double>(new double[] { 1, 1, 1, 1, 1, 1 });
			Matrix c = MatrixGenerator.From(new double[,]
			                                	{
			                                		{ 2, 4, 8,-3, 6,-3 }, 
													{ 8, 5, 4, 3, 2, 0 },
													{ 1, 1, 1, 1, 2, 3 }, 
													{ 0, 1, 5, 6,12, 3 }, 
													{-5, 3,-5, 2, 4, 3 }, 
													{ 2, 0, 1, 1, 2, 9 }, 
			                                	});
			var trProblem = new MatrixTransportationProblem(a, b, c);

			Dictionary<Tuple<int, int>, double> sol;
			bool flag = trProblem.Solve(out sol);
			PrintRes(flag, sol, c);
		}

		//on pt task2
		static void Example5()
		{
			List<double> a = new List<double>(new double[] { 20, 40, 30, 15 });
			List<double> b = new List<double>(new double[] { 10, 20, 15, 5, 30, 15, 10 });
			Matrix c = MatrixGenerator.From(new double[,]
			                                	{
			                                		{ 2, 4, 8,-3, 6,-3, 1 }, 
													{ 8, 5, 4, 3, 2, 0, 4 },
													{ 2, 0, 1, 1, 2, 9, -1 }, 
													{ 0, 1, 5, 6,12, 3, -3 },
			                                	});
			var trProblem = new MatrixTransportationProblem(a, b, c);

			Dictionary<Tuple<int, int>, double> sol;
			bool flag = trProblem.Solve(out sol);
			PrintRes(flag, sol, c);
		}

		static void Main(string[] args)
		{
			Example5();
		}


		static void PrintRes(bool isSol, Dictionary<Tuple<int, int>, double> sol, Matrix c)
		{
			Console.WriteLine("Is Solutin: {0}", isSol);
			double res = 0;
			string resPath = "";
			foreach (var d in sol)
			{
				res = res + c[d.Key.Item1, d.Key.Item2]*d.Value;
				resPath += String.Format("({0}; {1}); ", d.Key.Item1, d.Key.Item2);
			}

			Console.WriteLine("Optimum plan:\n {0}\nTarget func: {1}\n", resPath, res);
		}
	}
}
