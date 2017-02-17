using System;

namespace Оболочка_Буряк
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
