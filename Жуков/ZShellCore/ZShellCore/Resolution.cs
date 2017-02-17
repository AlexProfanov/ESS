using System;

namespace ZShellCore
{
	[Serializable]
	public class Resolution : IComparable
	{
		private Variable variable;

		private Value value;

		public Variable ResolutionVariable
		{
			get
			{
				return this.variable;
			}
		}

		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		public Resolution(Variable variable, Value value)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (variable.Type != value.type)
			{
				throw new ArgumentException("Тип переменной и значения не совпадают");
			}
			this.value = value;
			this.variable = variable;
			this.tryToRegisterValueInDomain();
		}

		public int CompareTo(object other)
		{
			Resolution resolution = other as Resolution;
			return (resolution == null || this != resolution) ? 1 : 0;
		}

		private void tryToRegisterValueInDomain()
		{
			if (this.variable.Domain != null)
			{
				this.value = this.variable.Domain.registerValueFromResolution(this);
			}
		}

		public void doResolution()
		{
			this.variable.Value = this.value;
		}

		public override string ToString()
		{
			return this.variable.ToString() + " := " + this.value.ToString();
		}

		public void setVariableAndValue(Variable variable, Value value)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (variable.Type != value.type)
			{
				throw new ArgumentException("Тип переменной " + variable.ToString() + " не совпадает со значением " + value.ToString());
			}
			this.variable = variable;
			this.value = value;
			this.tryToRegisterValueInDomain();
		}

		public void replaceVariable(Variable oldVariable, Variable newVariable)
		{
			if (this.variable == oldVariable)
			{
				this.variable = newVariable;
			}
		}
	}
}
