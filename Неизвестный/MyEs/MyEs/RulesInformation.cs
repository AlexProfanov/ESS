using System;

namespace MyEs
{
	internal class RulesInformation : cc
	{
		public int IndexIf;

		public int IndexThen;

		public int CountIf;

		public int CountThen;

		public RulesInformation(int _if)
		{
			this.IndexIf = _if;
			this.IndexThen = this.IndexIf + 1;
			this.CountIf = 0;
			this.CountThen = 0;
		}

		public object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
