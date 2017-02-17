using System;

namespace ZShellCore
{
	[Serializable]
	public class ConditionEqualComparer : ConditionComparer
	{
		public override bool compare(IComparable a, IComparable b)
		{
			return a.CompareTo(b) == 0;
		}

		public override string ToString()
		{
			return " == ";
		}
	}
}
