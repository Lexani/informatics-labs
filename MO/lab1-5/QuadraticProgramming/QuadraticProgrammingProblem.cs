using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixOperations;

namespace QuadraticProgramming
{
	public class QuadraticProgrammingProblem
	{
		#region Constructor

		public QuadraticProgrammingProblem(Matrix a, /*Matrix b, */Matrix c, Matrix d, Matrix x, IEnumerable<int> jBasis, IEnumerable<int> jStar)
		{
			_a = a.Copy();
			//_b = b.Copy();
			_c = c.Copy();
			_d = d.Copy();
			_x = x.Copy();
			_jBasis = new List<int>(jBasis);
			_jStar = new List<int>(jStar);
		}

		#endregion

		#region Public methods

		public bool Solve(out Matrix sol)
		{
			sol = null;
			bool is2StepMustBeSkiped = false;
			RecalculateBasisAndStarVariables();
			for(int i = 0; i < _iterCount; i++)
			{
				Console.WriteLine("Iteration #{0}", i);

				if (!is2StepMustBeSkiped)
				{
					Step1BuildPotencialsAndEstimations();

					if (Step2CheckForOptimumAndCalculateJ0())
					{
						sol = _x.Copy();
						return true;
					}
				}
				Console.WriteLine("Estimations:\n{0}", _estimation.Aggregate("", (acc, x) => String.Format("{0}det{1} = {2}\n", acc, x.Key, x.Value)));

				Step3BuildLDirection();
				Console.WriteLine("Change direction:\n{0}", _l);
				Step4CalculateTet0();
				Console.WriteLine("Tet{0} = {1}", _tet0.Key, _tet0.Value);
				if (!IsTargetFunctionHasBottomBondary())
				{
					return false;
				}
				Step5BuildNewPlan();
				Console.WriteLine("New base plan:\n{0}", _x);
				is2StepMustBeSkiped = Step6RecalculateJbAndJStar();
				Console.WriteLine("New Jbasis:\n[{0}]", _jBasis.Aggregate("", (a, x) => a + x.ToString() + ", "));
				Console.WriteLine("New J*:\n[{0}]", _jStar.Aggregate("", (a, x) => a + x.ToString() + ", "));
				RecalculateBasisAndStarVariables();
			}

			return false;
		}

		#endregion


		#region Private methods

		private void RecalculateBasisAndStarVariables()
		{
			int m = _a.RowsCount;
			int n = _a.ColumnsCount;
			_aBasis = new Matrix(m, _jBasis.Count);
			_aStar = new Matrix(m, _jStar.Count);
			_cNewBasis = new Matrix(_jBasis.Count, 1);
			_dBasis = new Matrix(_jBasis.Count, _jBasis.Count);
			_dStar = new Matrix(_jStar.Count, _jStar.Count);
			_xBasis = new Matrix(_jBasis.Count, 1);
			_jNotBasis = new List<int>();
			_cNew = _c.Copy().Add(_d.Copy().Multiply(_x));

			
			for(int i = 0; i < m; i++)
			{
				for(int j = 0; j < _jStar.Count; j++)
				{
					_aStar[i, j] = _a[i, _jStar[j]];
				}
			}

			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < _jBasis.Count; j++)
				{
					_aBasis[i, j] = _a[i, _jBasis[j]];
				}
			}

			for (int i = 0; i < _jBasis.Count; i++)
			{
				for (int j = 0; j < _jBasis.Count; j++)
				{
					_dBasis[i, j] = _d[_jBasis[i], _jBasis[j]];
				}
			}

			for (int i = 0; i < _jStar.Count; i++)
			{
				for (int j = 0; j < _jStar.Count; j++)
				{
					_dStar[i, j] = _d[_jStar[i], _jStar[j]];
				}
			}

			
			for (int i = 0; i < _jBasis.Count; i++)
			{
				_cNewBasis[i, 0] = _cNew[_jBasis[i], 0];
				_xBasis[i, 0] = _x[_jBasis[i], 0];
			}

			for (int i = 0; i < n; i++)
			{
				if (!_jStar.Contains(i))
				{
					_jNotBasis.Add(i);
				}
			}
		}

		private void Step1BuildPotencialsAndEstimations()
		{
			Matrix potencialsT = _cNewBasis.Copy().Transpose().Multiply(_aBasis.Copy().Invert()).Multiply(-1);
			_estimation = new Dictionary<int, double>();
			foreach(int j in _jNotBasis)
			{
				double delta = potencialsT.Copy().Multiply(_a.GetVectorColulmn(j))[0, 0] + _cNew[j, 0];
				_estimation.Add(j, delta);
			}

		}

		private bool Step2CheckForOptimumAndCalculateJ0()
		{
			_j0 = -1;
			foreach(KeyValuePair<int, double> pair in _estimation)
			{
				if (pair.Value.IsLessZero())
				{
					_j0 = pair.Key;
					return false;
				}
			}
			return true;
		}

		private void BuildHStarMatrix()
		{
			int m = _a.RowsCount;
			int k = _jStar.Count;
			_hStar = new Matrix(k + m, k + m);
			//D*
			for (int i = 0; i < k; i++)
			{
				for (int j = 0; j < k; j++)
				{
					_hStar[i, j] = _dStar[i, j];
				}
			}
			//A*
			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < k; j++)
				{
					_hStar[i + k, j] = _aStar[i, j];
				}
			}
			//A*'
			Matrix aStarT = _aStar.Copy().Transpose();
			for (int i = 0; i < k; i++)
			{
				for (int j = 0; j < m; j++)
				{
					_hStar[i, j + k] = aStarT[i, j];
				}
			}
		}

		private void Step3BuildLDirection()
		{
			BuildHStarMatrix();
			int k = _jStar.Count;
			int m = _a.RowsCount;
			Matrix hJ0 = new Matrix(k + m, 1);
			//at D* indexes
			int j0 = _jNotBasis.IndexOf(_j0);
			if (j0 < 0)
			{
				throw new Exception("j0 not in JNotBasis");
			}
			//D*j0
			for (int i = 0; i < k; i++)
			{
				hJ0[i, 0] = _d[_jStar[i], _j0];
			}
			for (int i = 0; i < m; i++)
			{
				hJ0[i + k, 0] = _a[i, _j0];
			}

			Matrix lStarY = _hStar.Copy().Invert().Multiply(hJ0).Multiply(-1);

			_l = new Matrix(_a.ColumnsCount, 1);
			for (int i = 0; i < k; i++)
			{
				_l[_jStar[i], 0] = lStarY[i, 0];
			}
			_l[_j0, 0] = 1;
		}

		private void Step4CalculateTet0()
		{
			Dictionary<int, double> tetS = new Dictionary<int, double>();
			foreach(int j in _jStar)
			{
				if(_l[j, 0].IsGreaterOrEqualZero())
				{
					tetS.Add(j, Double.MaxValue);
				}
				else
				{
					tetS.Add(j, -_x[j, 0]/_l[j, 0]);
				}
			}
			_sigma = _l.Copy().Transpose().Multiply(_d).Multiply(_l)[0, 0];
			if (!_jStar.Contains(_j0))
			{
				if (_sigma.IsZero())
				{
					tetS.Add(_j0, Double.MaxValue);
				}
				else if (_sigma.IsGreaterZero())
				{
					tetS.Add(_j0, Math.Abs(_estimation[_j0])/_sigma);
				}
				else
				{
                    // TODO: разобраться
                    //throw new Exception("Can't determine condition path in Step 4");
				}
			}
			_tet0 = new KeyValuePair<int, double>(-1, Double.MaxValue);
			foreach(var pair in tetS)
			{
				if (pair.Value < _tet0.Value)
				{
					_tet0 = pair;
				}
			}
		}

		private bool IsTargetFunctionHasBottomBondary()
		{
			return _tet0.Value != Double.MaxValue;
		}

		private void Step5BuildNewPlan()
		{
			_x = _x.Add(_l.Copy().Multiply(_tet0.Value));
		}

		//return true if first 2 steps must be skipped
		private bool Step6RecalculateJbAndJStar()
		{
			if (_tet0.Key == _j0)
			{
				_jStar.Add(_j0);
				//_jStar.Sort();
				return false;
			}
			if (_jStar.Contains(_tet0.Key) && !_jBasis.Contains(_tet0.Key))
			{
				_jStar.Remove(_tet0.Key);
				//_jStar.Sort();
				_estimation[_j0] = _estimation[_j0] + _tet0.Value*_sigma;
				return true;
			}
			bool is3dStep = false;
			int jPlus = -1;
			if (_jBasis.Contains(_tet0.Key))
			{
				Matrix aBasisInv = _aBasis.Copy().Invert();
				for (int i = 0; i < _jBasis.Count; i++ )
				{
					if (_jStar.Contains(i) && !_jBasis.Contains(i))
					{
						Matrix eS = new Matrix(_aBasis.RowsCount, 1);
						eS[_jBasis.IndexOf(_tet0.Key), 0] = 1;
						if (!eS.Copy().Transpose().Multiply(aBasisInv).Multiply(_a.GetVectorColulmn(i))[0, 0].IsZero())
						{
							jPlus = i;
							is3dStep = true;
							break;
						}
					}
				}

			}
			if (is3dStep)
			{
				_jBasis.Remove(_tet0.Key);
				_jBasis.Add(jPlus);
				//_jBasis.Sort();

				_jStar.Remove(_tet0.Key);
				//_jStar.Sort();

				_estimation[_j0] = _estimation[_j0] + _tet0.Value * _sigma;
				return true;
			}

			_jBasis.Remove(_tet0.Key);
			_jBasis.Add(_j0);
			//_jBasis.Sort();

			_jStar.Remove(_tet0.Key);
			_jStar.Add(_j0);
			//_jStar.Sort();

			return false;
		}

		#endregion

		#region Private fields

		private Matrix _a;
		private Matrix _aBasis;
		private Matrix _aStar;
		private Matrix _b;
		private Matrix _c;
		private Matrix _cNew;
		private Matrix _cNewBasis;
		private Matrix _d;
		private Matrix _dBasis;
		private Matrix _dStar;
		private Matrix _x;
		private Matrix _xBasis;
		private Matrix _hStar;
		private Matrix _l;
		private List<int> _jBasis;
		private List<int> _jNotBasis;
		private List<int> _jStar;

		Dictionary<int, double> _estimation = new Dictionary<int, double>();
		private int _j0;
		private double _sigma;
		private KeyValuePair<int, double> _tet0;

		private const int _iterCount = 10000;

		#endregion

	}
}
