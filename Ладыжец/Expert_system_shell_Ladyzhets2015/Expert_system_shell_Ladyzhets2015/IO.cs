using System;

namespace Expert_system_shell_Ladyzhets2015
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
