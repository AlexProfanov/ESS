using System;

namespace Оболочка_Буряк
{
	[Serializable]
	public class ConditionALaterBComparer : ConditionComparer
	{
		public override bool doCheck(IComparable a, IComparable b)
		{
			return a.CompareTo(b) < 0;
		}

		public override string ToString()
		{
			return " < ";
		}
	}
}
