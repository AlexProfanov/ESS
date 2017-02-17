using System;

namespace ZShellCore
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
