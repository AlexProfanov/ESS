using System;

namespace MyEs
{
	[Serializable]
	public class RulePair : cc
	{
		public int Variable;

		public int Value;

		public object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
