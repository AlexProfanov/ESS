using System;

namespace ZShellCore
{
	[Serializable]
	public abstract class Variable : NamedItem, IComparable
	{
		private Type type = typeof(Value);

		protected bool isValueSet;

		protected Value value;

		protected Enumeration variableEnumeration;

		public Enumeration Domain
		{
			get
			{
				return this.variableEnumeration;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("variableEnumeration");
				}
				if (this.type != value.DomainType)
				{
					throw new ArgumentException("Ошибка приведения типов. Тип аргумента должен быть " + this.type.Name);
				}
				this.variableEnumeration = value;
			}
		}

		public Value Value
		{
			get
			{
				Value result;
				if (this.isValueSet)
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
				if (this.variableEnumeration != null && !this.variableEnumeration.containsData(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.value = value;
				this.isValueSet = true;
			}
		}

		public Type Type
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

		public void removeDomain()
		{
			this.variableEnumeration = null;
		}

		public virtual void prepareForNewConsultation()
		{
			this.isValueSet = false;
			this.value = null;
		}

		public virtual bool tryToGetValue()
		{
			return false;
		}

		public override string ToString()
		{
			return this.name;
		}

		public int CompareTo(object other)
		{
			return (this == other) ? 0 : 1;
		}
	}
}
