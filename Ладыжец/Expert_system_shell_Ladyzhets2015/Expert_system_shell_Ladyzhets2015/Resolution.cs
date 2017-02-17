using System;

namespace Expert_system_shell_Ladyzhets2015
{
	[Serializable]
	public class Resolution : IComparable
	{
		private Variable variable;

		private Value value;

		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		public Variable ResolutionVariable
		{
			get
			{
				return this.variable;
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
			if (variable.VarType != value.type)
			{
				throw new ArgumentException("Тип переменной и значения не совпадают");
			}
			this.value = value;
			this.variable = variable;
			this.tryToRegisterInDomain();
		}

		public int CompareTo(object other)
		{
			Resolution resolution = other as Resolution;
			return (resolution == null || this != resolution) ? 1 : 0;
		}

		private void tryToRegisterInDomain()
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

		public void setVarAndValue(Variable variable, Value value)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (variable.VarType != value.type)
			{
				throw new ArgumentException("Тип переменной и значения не совпадают");
			}
			this.variable = variable;
			this.value = value;
			this.tryToRegisterInDomain();
		}

		public void replaceVariable(Variable oldVar, Variable newVar)
		{
			if (this.variable == oldVar)
			{
				this.variable = newVar;
			}
		}
	}
}
