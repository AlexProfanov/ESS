using System;

namespace Expert_system_shell_Ladyzhets2015
{
	[Serializable]
	public abstract class Variable : NamedItem, IComparable
	{
		private Type type = typeof(Value);

		protected Enumeration varEnumeration;

		protected bool valueWasSet;

		protected Value value;

		public Type VarType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
				this.removeDomain();
			}
		}

		public Value Value
		{
			get
			{
				Value result;
				if (this.valueWasSet)
				{
					result = this.value;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.type != value.type)
				{
					throw new ArgumentException("Ошибка приведения типов. Тип аргумента должен быть " + this.type.Name);
				}
				if (this.varEnumeration != null && !this.varEnumeration.containsData(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.value = value;
				this.valueWasSet = true;
			}
		}

		public Enumeration Domain
		{
			get
			{
				return this.varEnumeration;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("varEnumeration");
				}
				if (this.type != value.DomainType)
				{
					throw new ArgumentException("Ошибка приведения типов. Тип аргумента должен быть " + this.type.Name);
				}
				this.varEnumeration = value;
			}
		}

		public Variable(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.name = name;
			this.type = type;
		}

		public int CompareTo(object other)
		{
			return (this == other) ? 0 : 1;
		}

		public virtual bool tryToGetValue()
		{
			return false;
		}

		public void removeDomain()
		{
			this.varEnumeration = null;
		}

		public override string ToString()
		{
			return this.name;
		}

		public virtual void prepareForNewConsultation()
		{
			this.valueWasSet = false;
			this.value = null;
		}
	}
}
