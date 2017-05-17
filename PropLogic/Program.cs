using System;

namespace PropLogic
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			LogicEngine Thinker = new LogicEngine( new KnowledgeBase ("test1.txt"));
			//Thinker.ForwardChain ();
			//Thinker.BackChain ();
			Thinker.TruthTableMethod();
		}
	}
}
