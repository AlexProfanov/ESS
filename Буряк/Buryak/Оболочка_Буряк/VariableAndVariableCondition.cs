using System;

namespace Оболочка_Буряк
{
	[Serializable]
	public class VariableAndVariableCondition : Condition
	{
		private Variable variableFirst;

		private Variable variableSecond;

		public Variable VariableFirst
		{
			get
			{
				return this.variableFirst;
			}
		}

		public Variable VariableSecond
		{
			get
			{
				return this.variableSecond;
			}
		}

		public VariableAndVariableCondition(Variable variableFirst, ConditionComparer comparer, Variable variableSecond) : base(comparer)
		{
			if (variableFirst == null)
			{
				throw new ArgumentNullException("variableFirst");
			}
			if (variableSecond == null)
			{
				throw new ArgumentNullException("variableSecond");
			}
			if (variableFirst.VarType != variableSecond.VarType)
			{
				throw new ArgumentException("Типы переменных не совпадают");
			}
			this.variableFirst = variableFirst;
			this.comparer = comparer;
			this.variableSecond = variableSecond;
			this.usedVariables.Add(this.variableFirst);
			this.usedVariables.Add(this.variableSecond);
		}

		public override bool doCheck()
		{
			bool result;
			if (!this.variableFirst.tryToGetValue() || !this.variableSecond.tryToGetValue())
			{
				result = false;
			}
			else
			{
				IComparable comparable = this.variableFirst.Value.Data as IComparable;
				if (comparable == null)
				{
					throw new ArgumentException("Значение переменной " + this.variableFirst.ToString() + " не поддерживает интерфейс ICompareble");
				}
				IComparable comparable2 = this.variableSecond.Value.Data as IComparable;
				if (comparable2 == null)
				{
					throw new ArgumentException("Значение переменной " + this.variableSecond.ToString() + " не поддерживает интерфейс ICompareble");
				}
				result = this.comparer.doCheck(comparable, comparable2);
			}
			return result;
		}

		public override string ToString()
		{
			return this.variableFirst.ToString() + this.comparer.ToString() + this.variableSecond.ToString();
		}

		public void setVariables(Variable variableFirst, Variable variableSecond)
		{
			if (variableFirst == null)
			{
				throw new ArgumentNullException("variableFirst");
			}
			if (variableSecond == null)
			{
				throw new ArgumentNullException("variableSecond");
			}
			if (variableFirst.VarType != variableSecond.VarType)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Типы переменной ",
					variableFirst.ToString(),
					" и ",
					variableSecond.ToString(),
					" не совпадают."
				}));
			}
			this.variableFirst = variableFirst;
			this.variableSecond = variableSecond;
		}
	}
}
