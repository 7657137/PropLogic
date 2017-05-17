using System;
using System.Collections.Generic;
using System.IO;

namespace PropLogic
{
	public class KnowledgeBase
	{
		private List<Variable> _vars = new List<Variable> ();
		private Variable _goal;
		private List<Relationship> _relations = new List<Relationship>();
		private String _filename;

		public List<Variable> Vars{
			get{
				return _vars;
			}
			set{
				_vars = value;
			}
		}

		public Variable Goal{
			get{
				return _goal;
			}
		}

		public List<Relationship> Rels{
			get{
				return _relations;
			}
		}
			

		public Variable VarByName(string name){		//returns a variable with a specific name
			foreach (Variable v in _vars) {
				if (v.Name.Equals(name)) {
					return v;
				}
			}
			return null;
		}

		public void AddVar(string name){			//Variable adding with duplicate prevention
			if ((VarByName (name) == null) && (!name.Equals(""))) {
				_vars.Add(new Variable(name));
			}
		}

		public KnowledgeBase (string filename)
		{
			_goal = null;
			_filename = filename;
			string line;
			string[] relsplitline = {""};

			char[] reldelimiters = {';' };
			char[] vardelimiters = { '&', '|' };

			List<string> rhs = new List<string> ();
			List<string> lhs = new List<string> ();
			List<string> givens = new List<string> ();

			StreamReader reader = new StreamReader (_filename);

			try{
				while(!reader.EndOfStream){				//Line immediately under TELL is read as given information
					line = reader.ReadLine();
					if (line == "TELL"){
						line = reader.ReadLine().ToLower();			//case sensitivity removed
						relsplitline = line.Split(reldelimiters);	//Instructions split into statements by ';'
					}
					if (line == "ASK"){					//Line immediately below ASK is taken as the goal
						line = reader.ReadLine();
						if(VarByName(line) != null){
							_goal = VarByName(line);
						}else{
							_goal = new Variable(line);
							_vars.Add(_goal);
						}
					}
				}
			}
			finally{
				reader.Close ();
			}
			for (int i = 0; i < relsplitline.Length - 1; i++) {				//Splits statements into right and left hand
				string[] temp;
				relsplitline[i] = relsplitline[i].Replace (" ", string.Empty);
				temp = relsplitline [i].Split (new string[]{"=>"},StringSplitOptions.None);
				if (temp.Length > 1) {									//Left and right hand strings seperated for processing
					lhs.Add (temp [0]);
					rhs.Add (temp [1]);
				} else {
					givens.Add (temp [0]);								//vars named alone are serperated for processing
				}
			}
			foreach (string s in rhs) {									//Right hand side variables made stright into vars
				AddVar(s);
			}
			foreach (string s in givens) {								//Lone statements made into vars and set true
				AddVar(s);
				VarByName (s).Truth = true;
			}
			for (int i = 0; i < lhs.Count; i++) {
				Relationship.RelType type;								//Identifying if this is AND, OR, or standalone
				if (lhs[i].Contains ("&")) {
					type = Relationship.RelType.AND;
				} else if (lhs[i].Contains ("|")) {
					type = Relationship.RelType.OR;
				} else
					type = Relationship.RelType.SINGLE;
				string[] temp = lhs[i].Split (vardelimiters);			//Splitting left side into variables and operators
				List<Variable> tempvar = new List<Variable>();
				tempvar.Clear ();
				foreach (string s in temp) {
					AddVar (s);											//Adding variables from left hand side
					tempvar.Add (VarByName(s));
				}
				_relations.Add(new Relationship(tempvar,type,VarByName(rhs[i])));

			}
				
			if (_goal != null) {
				Console.WriteLine ("Goal : " + _goal.Name);
			} else
				Console.WriteLine ("No goal found");
		}
			
	}
}

