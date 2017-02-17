using System;

namespace Expert_system_shell_Ladyzhets2015
{
	[Serializable]
	public class Value
	{
		public readonly Type type;

		private object data;

		public object Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		public Value(Type type, object date)
		{
			this.type = type;
			this.Data = date;
		}

		public override string ToString()
		{
			string result;
			if (this.data != null)
			{
				result = this.data.ToString();
			}
			else
			{
				result = "";
			}
			return result;
		}
	}
}
