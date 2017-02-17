using System;

namespace ProductionShell
{
	[Serializable]
	public class ConditionEqualComparer : ConditionComparer
	{
		public override bool doCheck(IComparable a, IComparable b)
		{
			return a.CompareTo(b) == 0;
		}

		public override string ToString()
		{
			return " == ";
		}
	}
}
