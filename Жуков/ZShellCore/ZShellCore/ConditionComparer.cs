using System;

namespace ZShellCore
{
	[Serializable]
	public abstract class ConditionComparer
	{
		public virtual bool compare(IComparable a, IComparable b)
		{
			return false;
		}
	}
}
