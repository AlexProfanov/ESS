using System;

namespace ProductionShell
{
	[Serializable]
	public abstract class IO
	{
		public virtual Value askQuestion(string question, Variable variable)
		{
			return null;
		}
	}
}
