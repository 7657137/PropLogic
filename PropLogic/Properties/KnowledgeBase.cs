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


		public Variable VarByName(string name){
			foreach (Variable v in _vars) {
				if (v.Name == name) {
					return v;
				}
			}
			return null;
		}

		public void AddVar(string name){
			if ((VarByName (name) == null)) {
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

			StreamReader reader = new StreamReader (_filename);

			try{
				while(!reader.EndOfStream){
					line = reader.ReadLine();
					if (line == "TELL"){
						line = reader.ReadLine();
						relsplitline = line.Split(reldelimiters);
					}
					if (line == "ASK"){
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
			for (int i = 0; i < relsplitline.Length; i++) {				//Splits statements into right and left hand sides
				string[] temp;
				relsplitline[i] = relsplitline[i].Replace (" ", string.Empty);
				temp = relsplitline [i].Split (new string[]{"=>"},StringSplitOptions.None);
				if (temp.Length > 1) {
					lhs.Add (temp [0]);
					rhs.Add (temp [1]);
				} else {
					AddVar (temp [0]);
					VarByName (temp [0]).Truth = true;
				}
					
			}
			foreach (string s in rhs) {									//Right hand side variables listed
				AddVar(s);
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
					AddVar (s);
					tempvar.Add (VarByName(s));
				}
				_relations.Add(new Relationship(tempvar,type,VarByName(rhs[i])));

			}
			foreach (Relationship r in _relations) {
				r.Print ();
			}
			Console.WriteLine ("---");
			foreach (Variable v in _vars) {
				Console.WriteLine (v.Name);
			}


			if (_goal != null) {
				Console.WriteLine ("Goal : " + _goal.Name);
			} else
				Console.WriteLine ("No goal found");
		}

		public void ForwardLink(){
			while (true) {			//Placeholder loop
				foreach (Relationship r in _relations) {
					if (r.LeftCheck ()) {
						r.right.Truth = true;
					}
				}
			}
		}
	}
}

