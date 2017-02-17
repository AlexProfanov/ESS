using System;

namespace ProductionShell
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
