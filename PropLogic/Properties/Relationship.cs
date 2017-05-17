using System;
using System.Collections.Generic;

namespace PropLogic
{
	public class Relationship
	{
		public enum RelType{	//Idenfies if left side is a lone var, AND or OR
			AND,
			OR,
			SINGLE
		};
		private RelType _type;
		private List<Variable> _left = new List<Variable>();	//Vars on left side of horn
		private Variable _right;								//Implied var on right of horn
		private Relationship _parent;							//For giving logic sequence when backchaining

		public Variable right{
			get{
				return _right;
			}
		}

		public List<Variable> Left{
			get{
				return _left;
			}
		}

		public Relationship Parent{
			get{
				return _parent;
			}
			set{
				_parent = value;
			}
		}

		public Relationship (List<Variable> left, RelType type, Variable right)
		{
			foreach (Variable v in left) {
				v.AddRel (this);
			}
			right.AddRel (this);
			_left = left;
			_type = type;
			_right = right;
			_parent = null;
		}

		public void Print(){
			foreach (Variable v in _left) {
				Console.Write (v.Name + " ");
			}
			Console.Write (_type + " ");
			Console.WriteLine ("=> " + _right.Name);
		}

		public bool LeftCheck(){		//Checks if left side of statement is true, sets right true if so
			switch (_type) {
			case RelType.SINGLE:
				if (_left [0].Truth == true) {
					_right.Truth = true;
					return true;
				}
				break;

			case RelType.AND:
				foreach (Variable v in _left) {
					if (v.Truth == false)
						return false;
				}
					_right.Truth = true;
					return true;
			case RelType.OR:
				foreach (Variable v in _left) {
					if (v.Truth == true) {
						_right.Truth = true;
						return true;
					}
				}
				break;
			default:
				Console.WriteLine ("You're staring into the void lad");
				break;
			}
			return false;
		}

		public bool SoftLeftCheck(bool a, bool b, bool c){ //Relationship check for truth table which doesn't alter vars
			switch (_type) {
			case RelType.SINGLE:
				if (a & c)
					return true;
				break;

			case RelType.AND:
				if ((a && b) && c)
					return true;
				break;
			case RelType.OR:
				if ((a || b) && c)
					return true;
				break;
			default:
				return false;
			}
			return false;
		}

		public String BackLogicChain(){		//Follows list of parents to get the logic path in BC
			Relationship r = this;
			String Chain = "";
			foreach (Variable v in _left) {
				Chain = Chain + (v.Name + ", ");
			}
			List<Relationship> result = new List<Relationship> ();
			result.Add (r);
			while (r.Parent != null){
				r = r.Parent;
				result.Add (r);
			}
			foreach (Relationship c in result) {
				Chain = Chain + (c.right.Name + ", ");
			}
			Chain = Chain.Remove (Chain.Length - 2, 2);
			return  Chain;
		}
	}
}

