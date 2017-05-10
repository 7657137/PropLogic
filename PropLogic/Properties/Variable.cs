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


		public Variable (String name)
		{
			_name = name;
			_truth = false;		//To be set true if named in txt file
	//		_checkflag = false;
		}

		public void AddRel(Relationship rel){
			_relationships.Add (rel);
		}
	}
}

