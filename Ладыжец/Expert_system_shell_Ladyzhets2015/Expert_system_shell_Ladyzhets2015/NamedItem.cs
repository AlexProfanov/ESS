using System;

namespace Expert_system_shell_Ladyzhets2015
{
	[Serializable]
	public class NamedItem
	{
		protected string name = "";

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}
	}
}
