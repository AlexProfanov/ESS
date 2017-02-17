using System;
using System.Collections.Generic;

namespace ProductionShell
{
	[Serializable]
	public class Rule : NamedItem
	{
		private List<Resolution> resolutionList = new List<Resolution>();

		private List<Condition> conditionList = new List<Condition>();

		public Rule()
		{
		}

		public Rule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		public Rule(Rule rule)
		{
			this.makeSameAs(rule);
		}

		public void makeSameAs(Rule rule)
		{
			this.name = rule.name;
			this.conditionList.Clear();
			using (List<Condition>.Enumerator enumerator = rule.conditionList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Condition current = enumerator.Current;
					this.addCondition(current);
				}
			}
			this.resolutionList.Clear();
			using (List<Resolution>.Enumerator enumerator2 = rule.resolutionList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Resolution current2 = enumerator2.Current;
					this.addResolution(current2);
				}
			}
		}

		public void addResolution(Resolution resolution)
		{
			if (resolution == null)
			{
				throw new ArgumentNullException("resolution");
			}
			this.resolutionList.Add(resolution);
		}

		public bool containsResolution(Resolution resolution)
		{
			if (resolution == null)
			{
				throw new ArgumentNullException("resolution");
			}
			return this.resolutionList.Contains(resolution);
		}

		public void removeResolution(Resolution resolution)
		{
			if (resolution == null)
			{
				throw new ArgumentNullException("resolution");
			}
			if (!this.resolutionList.Contains(resolution))
			{
				throw new ArgumentException("В правиле нет такого заключения");
			}
			this.resolutionList.Remove(resolution);
		}

		public void removeAllResolutionsWithVariable(Variable variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			List<Resolution> list = new List<Resolution>();
			using (List<Resolution>.Enumerator enumerator = this.resolutionList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Resolution current = enumerator.Current;
					if (current.ResolutionVariable == variable)
					{
						list.Add(current);
					}
				}
			}
			using (List<Resolution>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Resolution current = enumerator.Current;
					this.resolutionList.Remove(current);
				}
			}
		}

		public IEnumerator<Resolution> getEnumeratorForResolutionList()
		{
			return this.resolutionList.GetEnumerator();
		}

		public int getResolutionCount()
		{
			return this.resolutionList.Count;
		}

		public Resolution getResolutionAt(int resolutionIndex)
		{
			if (resolutionIndex < 0 || resolutionIndex > this.resolutionList.Count)
			{
				throw new ArgumentOutOfRangeException("resolutionIndex");
			}
			return this.resolutionList[resolutionIndex];
		}

		public void addCondition(Condition condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (this.conditionList.Contains(condition))
			{
				throw new ArgumentException("В правиле уже есть такое условие");
			}
			this.conditionList.Add(condition);
		}

		public bool containsCondition(Condition condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			return this.conditionList.Contains(condition);
		}

		public void removeCondition(Condition condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (!this.conditionList.Contains(condition))
			{
				throw new ArgumentException("В правиле нет такого условия");
			}
			this.conditionList.Remove(condition);
		}

		public void removeAllConditionsWithVariable(Variable variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			List<Condition> list = new List<Condition>();
			using (List<Condition>.Enumerator enumerator = this.conditionList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Condition current = enumerator.Current;
					if (current.usedVariables.Contains(variable))
					{
						list.Add(current);
					}
				}
			}
			using (List<Condition>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Condition current = enumerator.Current;
					this.conditionList.Remove(current);
				}
			}
		}

		public IEnumerator<Condition> getEnumeratorForConditionList()
		{
			return this.conditionList.GetEnumerator();
		}

		public int getConditionCount()
		{
			return this.conditionList.Count;
		}

		public Condition getConditionAt(int conditionIndex)
		{
			if (conditionIndex < 0 || conditionIndex > this.conditionList.Count)
			{
				throw new ArgumentOutOfRangeException("conditionIndex");
			}
			return this.conditionList[conditionIndex];
		}

		public bool tryToFire()
		{
			Global.exposition.printText(base.Name + " : " + this.ToString());
			Global.exposition.goToLowLevel();
			bool flag = true;
			using (List<Condition>.Enumerator enumerator = this.conditionList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Condition current = enumerator.Current;
					if (!current.doCheck())
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				using (List<Resolution>.Enumerator enumerator2 = this.resolutionList.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Resolution current2 = enumerator2.Current;
						current2.doResolution();
					}
				}
			}
			Global.exposition.goToHighLevel();
			if (flag)
			{
				Global.exposition.printText(base.Name + " СРАБОТАЛО");
			}
			else
			{
				Global.exposition.deleteNode("ff");
			}
			return flag;
		}

		public bool containsVariableInConditionList(Variable variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			bool result;
			using (List<Condition>.Enumerator enumerator = this.conditionList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Condition current = enumerator.Current;
					if (current.usedVariables.Contains(variable))
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public void replaceVariableInConditionResolutionList(Variable oldVar, Variable newVar)
		{
			using (List<Condition>.Enumerator enumerator = this.conditionList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Condition current = enumerator.Current;
					current.replaceVariable(oldVar, newVar);
				}
			}
			using (List<Resolution>.Enumerator enumerator2 = this.resolutionList.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Resolution current2 = enumerator2.Current;
					current2.replaceVariable(oldVar, newVar);
				}
			}
		}

		public bool containsVariableInResolutionList(Variable variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			bool result = false;
			using (List<Resolution>.Enumerator enumerator = this.resolutionList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Resolution current = enumerator.Current;
					if (current.ResolutionVariable == variable)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public void insertConditionInto(int conditionIndex, int newConditionIndex)
		{
			if (conditionIndex < 0 || conditionIndex > this.conditionList.Count)
			{
				throw new ArgumentOutOfRangeException("conditionIndex");
			}
			if (newConditionIndex < 0 || newConditionIndex > this.conditionList.Count)
			{
				throw new ArgumentOutOfRangeException("newConditionIndex");
			}
			Condition condition = this.conditionList[conditionIndex];
			this.conditionList.Remove(condition);
			this.conditionList.Insert(newConditionIndex, condition);
		}

		public void insertResolutionInto(int resolutionIndex, int newResolutionIndex)
		{
			if (resolutionIndex < 0 || resolutionIndex > this.resolutionList.Count)
			{
				throw new ArgumentOutOfRangeException("resolutionIndex");
			}
			if (newResolutionIndex < 0 || newResolutionIndex > this.resolutionList.Count)
			{
				throw new ArgumentOutOfRangeException("newResolutionIndex");
			}
			Resolution resolution = this.resolutionList[resolutionIndex];
			this.resolutionList.Remove(resolution);
			this.resolutionList.Insert(newResolutionIndex, resolution);
		}

		public void switchConditions(int firstConditionIndex, int secondConditionIndex)
		{
			if (firstConditionIndex < 0 || firstConditionIndex > this.conditionList.Count)
			{
				throw new ArgumentOutOfRangeException("firstConditionIndex");
			}
			if (secondConditionIndex < 0 || secondConditionIndex > this.conditionList.Count)
			{
				throw new ArgumentOutOfRangeException("secondConditionIndex");
			}
			Condition condition = this.conditionList[firstConditionIndex];
			this.conditionList[firstConditionIndex] = this.conditionList[secondConditionIndex];
			this.conditionList[secondConditionIndex] = condition;
		}

		public void switchResolutions(int firstResolutionIndex, int secondResolutionIndex)
		{
			if (firstResolutionIndex < 0 || secondResolutionIndex > this.resolutionList.Count)
			{
				throw new ArgumentOutOfRangeException("firstResolutionIndex");
			}
			if (secondResolutionIndex < 0 || secondResolutionIndex > this.resolutionList.Count)
			{
				throw new ArgumentOutOfRangeException("secondResolutionIndex");
			}
			Resolution resolution = this.resolutionList[firstResolutionIndex];
			this.resolutionList[firstResolutionIndex] = this.resolutionList[secondResolutionIndex];
			this.resolutionList[secondResolutionIndex] = resolution;
		}

		public override string ToString()
		{
			string text = "IF ";
			IEnumerator<Condition> enumerator = this.conditionList.GetEnumerator();
			if (enumerator.MoveNext())
			{
				text += enumerator.Current.ToString();
			}
			while (enumerator.MoveNext())
			{
				text = text + " AND " + enumerator.Current.ToString();
			}
			string text2 = text + " THEN ";
			IEnumerator<Resolution> enumerator2 = this.resolutionList.GetEnumerator();
			if (enumerator2.MoveNext())
			{
				text2 += enumerator2.Current.ToString();
			}
			while (enumerator2.MoveNext())
			{
				text2 = text2 + ", " + enumerator2.Current.ToString();
			}
			return text2;
		}
	}
}
