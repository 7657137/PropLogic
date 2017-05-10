﻿using System;
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
		private List<Variable> _left = new List<Variable>();	//Vars on left side of eqn
		private Variable _right;								//Implied var on right

		public Variable right{
			get{
				return _right;
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
		}

		public void Print(){
			foreach (Variable v in _left) {
				Console.Write (v.Name + " ");
			}
			Console.WriteLine ("=> " + _right.Name);
		}

		public bool LeftCheck(){
			switch (_type) {
			case RelType.SINGLE:
				if (_left [0].Truth == true)
					return true;
				break;

			case RelType.AND:
				if ((_left [0].Truth && _left [1].Truth) == true)
					return true;
				break;
			case RelType.OR:
				if ((_left [0].Truth || _left [1].Truth) == true)
					return true;
				break;
			default:
				Console.WriteLine ("You're staring into the void lad");
				break;
			}
			return false;
		}
	}
}
