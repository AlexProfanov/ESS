using System;

namespace Expert_system_shell_Ladyzhets2015
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
