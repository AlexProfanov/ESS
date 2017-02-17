using System;
using System.Collections.Generic;

namespace Expert_system_shell_Ladyzhets2015
{
	[Serializable]
	public abstract class Condition : IComparable
	{
		public readonly List<Variable> usedVariables = new List<Variable>();

		protected ConditionComparer comparer;

		public ConditionComparer Comparer
		{
			get
			{
				return this.comparer;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value==null");
				}
				this.comparer = value;
			}
		}

		public Condition(ConditionComparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("");
			}
			this.comparer = comparer;
		}

		public virtual bool doCheck()
		{
			return false;
		}

		public void replaceVariable(Variable oldVar, Variable newVar)
		{
			if (this.usedVariables.Contains(oldVar))
			{
				this.usedVariables[this.usedVariables.IndexOf(oldVar)] = newVar;
			}
		}

		public int CompareTo(object other)
		{
			Condition condition = other as Condition;
			return (condition == null || this != condition) ? 1 : 0;
		}
	}
}
