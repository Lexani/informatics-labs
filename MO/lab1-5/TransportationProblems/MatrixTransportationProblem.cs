using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixOperations;

namespace TransportationProblems
{
	public enum EMoveDirection
	{
		Horizontal,
		Vertical
	}

	public class MatrixTransportationProblem
	{
		#region Constructor

		public MatrixTransportationProblem(List<double> a, List<double> b, Matrix c)
		{
			_a = new List<double>(a);
			_b = new List<double>(b);
			_c = c.Copy();
		}

		#endregion

		#region Public methods

		public bool Solve(out Dictionary<Tuple<int, int>, double> sol)
		{
			_basePlanValues = CalculateBasePlan();
			_basePlan = _basePlanValues.Keys.ToList();
			//Console.WriteLine("Base plan:\n{0}\n", _basePlan.Aggregate("", (acc, x) => String.Format("{0} ({1}, {2};)", acc, x.Item1, x.Item2)));

			for(int i =0; i < _iterationsCount; i++)
			{
				Console.WriteLine("Iteration #{0}\n", i);
				Console.WriteLine("Base plan:\n{0}\n", _basePlan.Aggregate("", (acc, x) => String.Format("{0} ({1}, {2};)", acc, x.Item1, x.Item2)));
				Step1CalculatePotencials();
				Console.WriteLine("Potencials A:\n {0}\n", _aPotencials.Aggregate("", (a, x) => a + x + "; "));
				Console.WriteLine("Potencials B:\n {0}\n", _bPotencials.Aggregate("", (a, x) => a + x + "; "));

				Step2CalculateEstimations();
				Console.WriteLine("Estimations:\n{0}\n", _estimations.ToString());
				WriteCostMatrix();
				if (Step3CheckForOptimum())
				{
					sol = _basePlanValues;
					return true;
				}
				Step4GetPositiveEstimation();
				double tet0;
				Tuple<int, int> tet0Point;
				Step5BuildCycle(out tet0, out tet0Point);
				Step6BuildNewBasePlanValues(tet0);
				Step7BuilNewBasePlan(tet0, tet0Point);
			}

			throw new Exception("iterations limit");
		}

		#endregion

		#region Private methods

		private void WriteCostMatrix()
		{
			Matrix m = new Matrix(_a.Count, _b.Count);
			foreach(var el in _basePlanValues)
			{
				m[el.Key.Item1, el.Key.Item2] = el.Value;
			}

			Console.WriteLine("Value matrix X:\n{0}", m);
		}

		//TODO: this methods must calculate base blan that is NOT a cycle. But currently it doesn't check this restriction.
		private Dictionary<Tuple<int, int>, double> CalculateBasePlan()
		{
			List<int> excludedRows = new List<int>();
			List<int> excludedCols = new List<int>();
			List<double> aNew = new List<double>(_a);
			List<double> bNew = new List<double>(_b);
			Dictionary<Tuple<int, int>, double> basePlanValues = new Dictionary<Tuple<int, int>, double>();
			while (basePlanValues.Count != _a.Count + _b.Count - 1)
			{
				int iMin = -1, jMin = -1;
				double cMin = Double.MaxValue;
				for (int i = 0; i < _c.RowsCount; i++)
				{
					if (excludedRows.Contains(i))
					{
						continue;
					}
					for (int j = 0; j < _c.ColumnsCount; j++)
					{
						if (excludedCols.Contains(j))
						{
							continue;
						}
						if (_c[i, j] < cMin)
						{
							cMin = _c[i, j];
							iMin = i;
							jMin = j;
						}
					}
				}
				double xIJValue = 0;
				if (iMin < 0 || jMin < 0)
				{
					for (int i = 0; i < _c.RowsCount; i++)
					{
						for (int j = 0; j < _c.ColumnsCount; j++)
						{
							if (!basePlanValues.Keys.Contains(new Tuple<int, int>(i, j)))
							{
								iMin = i;
								jMin = j;
								break;
							}
						}
						if (iMin >= 0 && jMin >= 0)
						{
							break;
						}
					}
				}
				else
				{
					if (aNew[iMin] < bNew[jMin])
					{
						xIJValue = aNew[iMin];
						excludedRows.Add(iMin);
						bNew[jMin] -= aNew[iMin];
						aNew[iMin] = 0;
					}
					else
					{
						xIJValue = bNew[jMin];
						excludedCols.Add(jMin);
						aNew[iMin] -= bNew[jMin];
						bNew[jMin] = 0;
					}
				}
				basePlanValues.Add(new Tuple<int, int>(iMin, jMin), xIJValue);
			}
			
			return basePlanValues;
		}

		private void Step1CalculatePotencials()
		{
			_aPotencials = Enumerable.Repeat(Double.NaN, _a.Count).ToList();
			_bPotencials = Enumerable.Repeat(Double.NaN, _b.Count).ToList();
			Queue<int> aKnownPotenc = new Queue<int>();
			Queue<int> bKnownPotenc = new Queue<int>();

			_aPotencials[0] = 0;
			aKnownPotenc.Enqueue(0);

			while (aKnownPotenc.Count != 0 || bKnownPotenc.Count != 0)
			{
				if (aKnownPotenc.Count != 0)
				{
					int i = aKnownPotenc.Dequeue();
					foreach (Tuple<int, int> x in _basePlan.Where(y => y.Item1 == i))
					{
						int j = x.Item2;
						if (Double.IsNaN(_bPotencials[j]))
						{
							_bPotencials[j] = _c[i, j] - _aPotencials[i];
							bKnownPotenc.Enqueue(j);
						}
					}
				}
				if (bKnownPotenc.Count != 0)
				{
					int j = bKnownPotenc.Dequeue();
					foreach (Tuple<int, int> x in _basePlan.Where(y => y.Item2 == j))
					{
						int i = x.Item1;
						if (Double.IsNaN(_aPotencials[i]))
						{
							_aPotencials[i] = _c[i, j] - _bPotencials[j];
							aKnownPotenc.Enqueue(i);
						}
					}
				}
			}

			if (_aPotencials.Contains(Double.NaN) || _bPotencials.Contains(Double.NaN))
			{
				throw new Exception("Error during potencials calculating");
			}
		}

		private void Step2CalculateEstimations()
		{
			_estimations = new Matrix(_a.Count, _b.Count);
			for (int i = 0; i < _estimations.RowsCount; i++)
			{
				for (int j = 0; j < _estimations.ColumnsCount; j++)
				{
					_estimations[i, j] = _basePlan.Contains(new Tuple<int, int>(i, j)) 
															? 0 
															: _aPotencials[i] + _bPotencials[j] - _c[i, j];
				}
			}
		}

		private bool Step3CheckForOptimum()
		{
			for (int i = 0; i < _estimations.RowsCount; i++)
			{
				for (int j = 0; j < _estimations.ColumnsCount; j++)
				{
					if (!_basePlan.Contains(new Tuple<int, int>(i, j)) && _estimations[i, j].IsGreaterZero())
					{
						return false;
					}
				}
			}
			return true;
		}

		//return (i0, j0)
		private void Step4GetPositiveEstimation()
		{
			for (int i = 0; i < _estimations.RowsCount; i++)
			{
				for (int j = 0; j < _estimations.ColumnsCount; j++)
				{
					if (_estimations[i, j].IsGreaterZero())
					{
						_positiveEstimationPoint = new Tuple<int, int>(i, j);
						return;
					}
				}
			}

			throw new Exception("Positive estimation nof founded");
		}

		private bool IsMoveDirectionCorrect(EMoveDirection direction, Tuple<int, int> startPoint, Tuple<int, int> nextPoint)
		{
			return direction == EMoveDirection.Horizontal
			       	? startPoint.Item1 == nextPoint.Item1
			       	: startPoint.Item2 == nextPoint.Item2;
		}

		private EMoveDirection InverseMoveDirection(EMoveDirection direction)
		{
			return direction == EMoveDirection.Horizontal
			       	? EMoveDirection.Vertical
			       	: EMoveDirection.Horizontal;
		}

		private bool BuildCycleRecursively(Tuple<int, int> startPoint, EMoveDirection moveDirection)
		{
			if (startPoint != _positiveEstimationPoint && IsMoveDirectionCorrect(moveDirection, startPoint, _positiveEstimationPoint))
			{
				return true;
			}

			for (int i = 0; i < _basePlan.Count; i++)
			{
				if (!_basePlanUsingFlags[i])
				{
					var nextPoint = _basePlan[i];
					if (IsMoveDirectionCorrect(moveDirection, startPoint, nextPoint))
					{
						_basePlanUsingFlags[i] = true;
						_cyclePointsOrdered.Push(nextPoint);
						if (BuildCycleRecursively(nextPoint, InverseMoveDirection(moveDirection)))
						{
							return true;
						}
						_basePlanUsingFlags[i] = false;
						_cyclePointsOrdered.Pop();
					}
				}
			}

			return false;
		}

		//returns tet0, (i*, j*)
		private void Step5BuildCycle(out double tet0, out Tuple<int, int> tet0Point)
		{
			_basePlanUsingFlags = Enumerable.Repeat(false, _basePlan.Count).ToList();
			_cyclePointsOrdered = new Stack<Tuple<int, int>>();

			tet0 = Double.MaxValue;
			tet0Point = new Tuple<int, int>(-1, -1);

			if (BuildCycleRecursively(_positiveEstimationPoint, EMoveDirection.Horizontal))
			{
				bool flag = true;
				foreach (var point in _cyclePointsOrdered)
				{
					if (flag)
					{
						double tet = _basePlanValues[point];
						if (tet < tet0)
						{
							tet0 = tet;
							tet0Point = point;
						}
					}
					flag = !flag;
				}
			}
			if (tet0 == Double.MaxValue)
			{
				throw new Exception("Tet0 not founded");
			}
		}

		private void Step6BuildNewBasePlanValues(double tet0)
		{
			bool flag = false;
			foreach (var point in _cyclePointsOrdered)
			{
				_basePlanValues[point] += (flag ? 1 : -1) * tet0;
				flag = !flag;
			}
		}

		private void Step7BuilNewBasePlan(double tet0, Tuple<int, int> tet0Point)
		{
			if (!_basePlanValues.Remove(tet0Point))
			{
				throw new Exception("Point not founded in base plan");
			}
			_basePlanValues.Add(_positiveEstimationPoint, tet0);
			_basePlan = _basePlanValues.Keys.ToList();
		}

		#endregion

		#region Private fields

		private List<double> _a;
		private List<double> _b;
		private Matrix _c;
		private List<Tuple<int, int>> _basePlan;
		private Dictionary<Tuple<int, int>, double> _basePlanValues;
		private List<double> _aPotencials;
		private List<double> _bPotencials;
		private Matrix _estimations;
		private Tuple<int, int> _positiveEstimationPoint;
		private const int _iterationsCount = 1000;
		private Stack<Tuple<int, int>> _cyclePointsOrdered;
		
		
		//step 5 rec parameters
		private List<bool> _basePlanUsingFlags;

		#endregion
	}
}
