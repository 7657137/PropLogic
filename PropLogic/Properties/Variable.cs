using System;
using System.Collections.Generic;

namespace PropLogic
{
	public class Variable
	{
		private string _name;
		private bool _truth;
	//	private bool _checkflag;		//Flag for if the variable has been checked
		private List<Relationship> _relationships = new List<Relationship>();

		public string Name{
			get{
				return _name;
			}
		}

		public bool Truth{
			get{
				return _truth;
			}
			set{
				_truth = value;
			}
		}

		public List<Relationship> Rels{
			get{
				return _relationships;
			}
		}


		public Variable (String name)
		{
			_name = name;
			_truth = false;		//To be set true if named in txt file
		}

		public void AddRel(Relationship rel){
			_relationships.Add (rel);
		}
		public bool Is(String name){
			if (_name == name)
				return true;
			else
				return false;
		}
	}
}

