using System;
using System.Collections.Generic;

namespace ProductionShell
{
	[Serializable]
	public class Enumeration : NamedItem
	{
		private Type type = typeof(Value);

		private List<Value> allowedValues = new List<Value>();

		private SortedList<VariableAndValueCondition, Value> conditionList = new SortedList<VariableAndValueCondition, Value>();

		private SortedList<Resolution, Value> resolutionList = new SortedList<Resolution, Value>();

		public Value this[int valueIndex]
		{
			get
			{
				if (valueIndex < 0 || valueIndex > this.allowedValues.Count)
				{
					throw new ArgumentOutOfRangeException("valueIndex");
				}
				return this.allowedValues[valueIndex];
			}
		}

		public Type DomainType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		public Enumeration(string name, Type type)
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

		public Enumeration(Enumeration domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("enumeration");
			}
			this.type = domain.type;
			this.name = domain.name;
			using (List<Value>.Enumerator enumerator = domain.allowedValues.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Value current = enumerator.Current;
					this.allowedValues.Add(current);
				}
			}
			using (IEnumerator<KeyValuePair<VariableAndValueCondition, Value>> enumerator2 = domain.conditionList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<VariableAndValueCondition, Value> current2 = enumerator2.Current;
					this.conditionList.Add(current2.Key, current2.Value);
				}
			}
			using (IEnumerator<KeyValuePair<Resolution, Value>> enumerator3 = domain.resolutionList.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					KeyValuePair<Resolution, Value> current3 = enumerator3.Current;
					this.resolutionList.Add(current3.Key, current3.Value);
				}
			}
		}

		public bool containsData(Value value)
		{
			if (this.type != value.type)
			{
				throw new ArgumentException("Ошибка приведения типов. Тип аргумента должен быть " + this.type.Name);
			}
			using (List<Value>.Enumerator enumerator = this.allowedValues.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Value current = enumerator.Current;
					if (current.Data.Equals(value.Data))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void addValue(Value value)
		{
			if (this.type != value.type)
			{
				throw new ArgumentException("Ошибка приведения типов. Тип аргумента должен быть " + this.type.Name);
			}
			if (this.containsData(value))
			{
				throw new ArgumentException("Значение " + value.ToString() + " уже добавлено в перечисление");
			}
			this.allowedValues.Add(value);
		}

		public void replaceValue(int oldValueIndex, Value newValue)
		{
			if (oldValueIndex < 0 || oldValueIndex > this.allowedValues.Count)
			{
				throw new ArgumentOutOfRangeException("oldValueIndex");
			}
			Value value = this.allowedValues[oldValueIndex];
			if (value.type != newValue.type)
			{
				throw new ArgumentException("Тип старого и нового значения не совпадают");
			}
			this.allowedValues[oldValueIndex].Data = newValue.Data;
		}

		public int getAllowedValueCount()
		{
			return this.allowedValues.Count;
		}

		public void removeValueAt(int valueIndex)
		{
			if (valueIndex < 0 || valueIndex > this.allowedValues.Count)
			{
				throw new ArgumentOutOfRangeException("valueIndex");
			}
			Value value = this.allowedValues[valueIndex];
			using (IEnumerator<KeyValuePair<VariableAndValueCondition, Value>> enumerator = this.conditionList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<VariableAndValueCondition, Value> current = enumerator.Current;
					if (current.Value == value)
					{
						Global.knowledgeBase.removeConditionInAnyRule(current.Key);
					}
				}
			}
			using (IEnumerator<KeyValuePair<Resolution, Value>> enumerator2 = this.resolutionList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<Resolution, Value> current2 = enumerator2.Current;
					if (current2.Value == value)
					{
						Global.knowledgeBase.removeResolutionInAnyRule(current2.Key);
					}
				}
			}
			this.allowedValues.Remove(value);
		}

		public void removeAllValues()
		{
			while (this.allowedValues.Count != 0)
			{
				this.removeValueAt(0);
			}
		}

		public IEnumerator<Value> getEnumeratorForValues()
		{
			return this.allowedValues.GetEnumerator();
		}

		public void switchValues(int firstValueIndex, int secondValueIndex)
		{
			if (firstValueIndex < 0 || firstValueIndex > this.allowedValues.Count)
			{
				throw new ArgumentOutOfRangeException("firstRuleIndex");
			}
			if (secondValueIndex < 0 || secondValueIndex > this.allowedValues.Count)
			{
				throw new ArgumentOutOfRangeException("secondValueIndex");
			}
			Value value = this.allowedValues[firstValueIndex];
			this.allowedValues[firstValueIndex] = this.allowedValues[secondValueIndex];
			this.allowedValues[secondValueIndex] = value;
		}

		public void insertValueInto(int valueToInsertIndex, int newValuePositionIndex)
		{
			if (valueToInsertIndex < 0 || valueToInsertIndex > this.allowedValues.Count)
			{
				throw new ArgumentOutOfRangeException("valueToInsertIndex");
			}
			if (newValuePositionIndex < 0 || newValuePositionIndex > this.allowedValues.Count)
			{
				throw new ArgumentOutOfRangeException("newValuePositionIndex");
			}
			Value value = this.allowedValues[valueToInsertIndex];
			this.allowedValues.RemoveAt(valueToInsertIndex);
			this.allowedValues.Insert(newValuePositionIndex, value);
		}

		public Value registerValueFromCondition(VariableAndValueCondition condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (!this.containsData(condition.Value))
			{
				throw new ArgumentException("Домен " + base.Name + " не содержит значения " + condition.Value.ToString());
			}
			Value value = null;
			using (List<Value>.Enumerator enumerator = this.allowedValues.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Value current = enumerator.Current;
					if (current.Data.Equals(condition.Value.Data))
					{
						value = current;
					}
				}
			}
			if (this.conditionList.ContainsKey(condition))
			{
				this.conditionList[condition] = condition.Value;
			}
			else
			{
				this.conditionList.Add(condition, value);
			}
			return value;
		}

		public Value registerValueFromResolution(Resolution resolution)
		{
			if (resolution == null)
			{
				throw new ArgumentNullException("resolution");
			}
			if (!this.containsData(resolution.Value))
			{
				throw new ArgumentException("Домен " + base.Name + " не содержит значения " + resolution.Value.ToString());
			}
			Value value = null;
			using (List<Value>.Enumerator enumerator = this.allowedValues.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Value current = enumerator.Current;
					if (current.Data.Equals(resolution.Value.Data))
					{
						value = current;
					}
				}
			}
			if (this.resolutionList.ContainsKey(resolution))
			{
				this.resolutionList[resolution] = resolution.Value;
			}
			else
			{
				this.resolutionList.Add(resolution, value);
			}
			return value;
		}

		public bool hasTheSameValues(Enumeration otherDomain)
		{
			if (otherDomain == null)
			{
				throw new ArgumentNullException("otherDomain");
			}
			if (this.getAllowedValueCount() != otherDomain.getAllowedValueCount())
			{
				return false;
			}
			using (List<Value>.Enumerator enumerator = this.allowedValues.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Value current = enumerator.Current;
					if (!otherDomain.containsData(current))
					{
						return false;
					}
				}
			}
			return true;
		}

		public void removeAllConditioResolutionDependences()
		{
			this.conditionList.Clear();
			this.resolutionList.Clear();
		}

		public void setEqualAllowedValuesWith(Enumeration otherDomain)
		{
			if (otherDomain == null)
			{
				throw new ArgumentNullException("otherDomain");
			}
			List<int> list = new List<int>();
			using (List<Value>.Enumerator enumerator = this.allowedValues.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Value current = enumerator.Current;
					if (!otherDomain.containsData(current))
					{
						list.Add(this.allowedValues.IndexOf(current));
					}
				}
			}
			using (List<int>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int current2 = enumerator2.Current;
					this.removeValueAt(current2);
				}
			}
			this.allowedValues.Clear();
			using (List<Value>.Enumerator enumerator3 = otherDomain.allowedValues.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					Value current3 = enumerator3.Current;
					this.addValue(current3);
				}
			}
		}

		public override string ToString()
		{
			return this.name;
		}
	}
}
