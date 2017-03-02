using System;

namespace ShellES.Entities
{
	[Serializable]
	public class ESDomainValue
	{
		private string value;

		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		public ESDomainValue Self
		{
			get
			{
				return this;
			}
		}

		public ESDomainValue()
		{
			this.value = "";
		}

		public ESDomainValue(string str)
		{
			this.value = str;
		}
	}
}
