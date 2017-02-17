using System;

namespace Expert_system_shell_Ladyzhets2015
{
	[Serializable]
	public abstract class ConditionComparer
	{
		public virtual bool doCheck(IComparable a, IComparable b)
		{
			return false;
		}
	}
}
