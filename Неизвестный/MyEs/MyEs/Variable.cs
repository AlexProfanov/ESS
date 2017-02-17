using System;

namespace MyEs
{
	[Serializable]
	public class Variable : cc
	{
		public string Name;

		public string Question;

		public string Reason;

		public string type;

		public int domain;

		public Variable()
		{
			this.Name = "";
		}

		public object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
