using System;

namespace ProductionShell
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
				if (!this.type.Equals(value.GetType()))
				{
					throw new ArgumentException("Тип значения и его содержимого не совпадают");
				}
				if (!(value is IComparable))
				{
					throw new ArgumentException("Тип значения не поддерживает операции сравнения");
				}
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
