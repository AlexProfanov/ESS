using System;

namespace Оболочка_Буряк
{
	[Serializable]
	public class VariableAndValueCondition : Condition
	{
		private Value value;

		public Variable ConditionVariable
		{
			get
			{
				return this.usedVariables[0];
			}
		}

		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		public VariableAndValueCondition(Variable variable, ConditionComparer comparer, Value value) : base(comparer)
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
				throw new ArgumentException("Типы переменной и значения не совпадают");
			}
			this.comparer = comparer;
			this.value = value;
			this.usedVariables.Add(variable);
			this.tryToRegisterValueInDomain();
		}

		private void tryToRegisterValueInDomain()
		{
			if (this.usedVariables[0].Domain != null)
			{
				this.value = this.usedVariables[0].Domain.registerValueFromCondition(this);
			}
		}

		public override bool doCheck()
		{
			bool result;
			if (!this.usedVariables[0].tryToGetValue())
			{
				result = false;
			}
			else
			{
				IComparable comparable = this.usedVariables[0].Value.Data as IComparable;
				if (comparable == null)
				{
					throw new ArgumentException("Значение переменной " + this.usedVariables[0].ToString() + " не поддерживает интерфейс ICompareble");
				}
				result = this.comparer.doCheck(comparable, this.value.Data as IComparable);
			}
			return result;
		}

		public override string ToString()
		{
			return this.usedVariables[0].ToString() + this.comparer.ToString() + this.value.ToString();
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
				throw new ArgumentException("Тип переменной " + variable.ToString() + " не совпадает со значением " + value.ToString());
			}
			this.usedVariables[0] = variable;
			this.value = value;
			this.tryToRegisterValueInDomain();
		}
	}
}
