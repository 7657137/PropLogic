using System;

namespace PropLogic
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			LogicEngine Thinker;
			if (args.Length > 1) {
				try {
					Thinker = new LogicEngine (new KnowledgeBase (args [1]));
				} catch {
					Thinker = new LogicEngine (new KnowledgeBase ("test1.txt"));
				}

				switch (args [0].ToLower ()) {
				case "tt":
					Thinker.TruthTableMethod ();
					break;
				case "fc":
					Thinker.ForwardChain ();
					break;
				case "bc":
					Thinker.BackChain ();
					break;
				default:
					Thinker.TruthTableMethod ();
					break;
				}
			} else
				Console.WriteLine ("Insufficient arguments");
		}
	}
}
