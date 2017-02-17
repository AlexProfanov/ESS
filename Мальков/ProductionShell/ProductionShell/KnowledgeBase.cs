using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProductionShell
{
	[Serializable]
	public class KnowledgeBase
	{
		private Container<Enumeration> enumerationList = new Container<Enumeration>();

		private Container<Rule> ruleList = new Container<Rule>();

		private Container<Variable> variableList = new Container<Variable>();

		private Variable goal;

		public Variable Goal
		{
			get
			{
				return this.goal;
			}
			set
			{
				if (value == null)
				{
					this.goal = null;
					return;
				}
				if (!this.variableList.Contains(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value is AskedVariable)
				{
					throw new ArgumentException("Целью консультации не может быть запрашиваемая переменная");
				}
				this.goal = value;
			}
		}

		public string UniqueDomainName
		{
			get
			{
				string text = "Domain";
				int num = this.enumerationList.Count + 1;
				while (this.enumerationList.Contains(text + num.ToString()))
				{
					num++;
				}
				return text + num.ToString();
			}
		}

		public string UniqueVarName
		{
			get
			{
				string text = "Var";
				int num = this.variableList.Count + 1;
				while (this.variableList.Contains(text + num.ToString()))
				{
					num++;
				}
				return text + num.ToString();
			}
		}

		public string UniqueRuleName
		{
			get
			{
				string text = "Rule";
				int num = this.ruleList.Count + 1;
				while (this.ruleList.Contains(text + num.ToString()))
				{
					num++;
				}
				return text + num.ToString();
			}
		}

		public static KnowledgeBase openKnowledgeBase(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			KnowledgeBase result = new KnowledgeBase();
			try
			{
				using (Stream stream = File.Open(fileName, 3))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					try
					{
						result = (binaryFormatter.Deserialize(stream) as KnowledgeBase);
					}
					catch (SerializationException ex)
					{
						throw new ArgumentException("Возможно указанный файл имеет неверный формат:\n" + ex.Message, ex);
					}
				}
			}
			catch (IOException ex2)
			{
				throw new ArgumentException("Указанный файл прочитать не удалось", ex2);
			}
			return result;
		}

		public static void saveKnowledgeBase(string fileName, KnowledgeBase knowledgeBase)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (knowledgeBase == null)
			{
				throw new ArgumentNullException("knowledgeBase");
			}
			try
			{
				using (Stream stream = File.Open(fileName, 4))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(stream, knowledgeBase);
				}
				using (Stream stream2 = File.Open(fileName, 3))
				{
					BinaryFormatter binaryFormatter2 = new BinaryFormatter();
					try
					{
						binaryFormatter2.Deserialize(stream2);
					}
					catch (SerializationException ex)
					{
						throw new ArgumentException("Сохранение прошло с ошибками:\n" + ex.Message, ex);
					}
				}
			}
			catch (SerializationException ex2)
			{
				throw new ArgumentException(ex2.Message, ex2);
			}
			catch (IOException ex3)
			{
				throw new ArgumentException("В указанный файл ничего записать не удалось", ex3);
			}
		}

		public void addRule(Rule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.containsRuleName(rule.Name))
			{
				throw new ArgumentException("Правило с именем " + rule.Name + " уже зарегестрировано в базе знаний");
			}
			this.ruleList.Add(rule);
		}

		public IEnumerator<Rule> getEnumeratorForRules()
		{
			return this.ruleList.GetEnumerator();
		}

		public void removeRuleAt(int ruleIndex)
		{
			this.ruleList.RemoveAt(ruleIndex);
		}

		public Rule getRuleAt(int ruleIndex)
		{
			return this.ruleList[ruleIndex];
		}

		public int getRuleCount()
		{
			return this.ruleList.Count;
		}

		public bool containsRuleName(string ruleName)
		{
			return this.ruleList.Contains(ruleName);
		}

		public void addVariable(Variable variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.containsVariableName(variable.Name))
			{
				throw new ArgumentException("Переменная с именем " + variable.Name + " уже зарегестрирована в базе знаний");
			}
			this.variableList.Add(variable);
		}

		public void insertVariableInto(int oldVarIndex, int newVarIndex)
		{
			this.variableList.Insert(oldVarIndex, newVarIndex);
		}

		public void switchVariables(int firstVarIndex, int secondVarIndex)
		{
			this.variableList.Switch(firstVarIndex, secondVarIndex);
		}

		public IEnumerator<Variable> getEnumeratorForVariables()
		{
			return this.variableList.GetEnumerator();
		}

		public bool hasAnyVariables()
		{
			return this.variableList.Count != 0;
		}

		public void removeVariableAt(int varIndex)
		{
			if (varIndex < 0 || varIndex > this.variableList.Count)
			{
				throw new ArgumentOutOfRangeException("varIndex");
			}
			Variable variable = this.variableList[varIndex];
			using (IEnumerator<Rule> enumerator = this.ruleList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					if (current.containsVariableInConditionList(variable))
					{
						current.removeAllConditionsWithVariable(variable);
					}
					if (current.containsVariableInResolutionList(variable))
					{
						current.removeAllResolutionsWithVariable(variable);
					}
				}
			}
			if (this.goal == variable)
			{
				this.goal = null;
			}
			this.variableList.RemoveAt(varIndex);
		}

		public void replaceVariableWith(Variable oldVar, Variable newVar)
		{
			if (!this.variableList.Contains(oldVar))
			{
				throw new ArgumentException("В базе знаний нет такой переменной");
			}
			if (oldVar.VarType != newVar.VarType)
			{
				throw new ArgumentException("Типы новой и старой переменной не совпадают");
			}
			using (IEnumerator<Rule> enumerator = this.ruleList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					current.replaceVariableInConditionResolutionList(oldVar, newVar);
				}
			}
			this.variableList[oldVar] = newVar;
			if (this.goal == oldVar)
			{
				if (newVar is DeducibleVariable || newVar is DeducibleAskedVariable)
				{
					this.goal = newVar;
					return;
				}
				this.Goal = null;
			}
		}

		public void removeConditionAndResolutionWith(Variable variable)
		{
			if (!this.variableList.Contains(variable))
			{
				throw new ArgumentException("Эта переменная не принадлежит базе знаний");
			}
			List<Rule> list = new List<Rule>();
			using (IEnumerator<Rule> enumerator = this.ruleList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					List<Condition> list2 = new List<Condition>();
					IEnumerator<Condition> enumeratorForConditionList = current.getEnumeratorForConditionList();
					while (enumeratorForConditionList.MoveNext())
					{
						if (enumeratorForConditionList.Current.usedVariables.Contains(variable) && enumeratorForConditionList.Current is VariableAndValueCondition)
						{
							list2.Add(enumeratorForConditionList.Current);
						}
					}
					using (List<Condition>.Enumerator enumerator2 = list2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Condition current2 = enumerator2.Current;
							current.removeCondition(current2);
						}
					}
					if (current.containsVariableInResolutionList(variable))
					{
						current.removeAllResolutionsWithVariable(variable);
					}
					if (current.getConditionCount() == 0 && current.getResolutionCount() == 0)
					{
						list.Add(current);
					}
				}
			}
			using (List<Rule>.Enumerator enumerator3 = list.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					Rule current3 = enumerator3.Current;
					this.removeRuleAt(this.ruleList.IndexOf(current3));
				}
			}
		}

		public Variable getVariableAt(int variableIndex)
		{
			return this.variableList[variableIndex];
		}

		public Variable getVariable(string variableName)
		{
			return this.variableList[variableName];
		}

		public bool containsVariableName(string varName)
		{
			return this.variableList.Contains(varName);
		}

		public void addEnumeration(Enumeration domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("enumeration");
			}
			if (this.enumerationList.Contains(domain.Name))
			{
				throw new ArgumentException("База знаний уже содержит домен с именем " + domain.Name);
			}
			this.enumerationList.Add(domain);
		}

		public void removeEnumerationAt(int enumerationIndex)
		{
			if (enumerationIndex < 0 || enumerationIndex > this.enumerationList.Count)
			{
				throw new ArgumentOutOfRangeException("emumerationIndex");
			}
			Enumeration enumeration = this.enumerationList[enumerationIndex];
			using (IEnumerator<Variable> enumerator = this.variableList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Variable current = enumerator.Current;
					if (current.Domain == enumeration)
					{
						current.removeDomain();
					}
				}
			}
			enumeration.removeAllValues();
			this.enumerationList.RemoveAt(enumerationIndex);
		}

		public IEnumerator<Enumeration> getEnumeratorForEnumerations()
		{
			return this.enumerationList.GetEnumerator();
		}

		public void switchEnumerations(int firstDomainIndex, int secondDomainIndex)
		{
			this.enumerationList.Switch(firstDomainIndex, secondDomainIndex);
		}

		public void insertDomainInto(int domainToInsertIndex, int newDomainPositionIndex)
		{
			this.enumerationList.Insert(newDomainPositionIndex, newDomainPositionIndex);
		}

		public Enumeration getEnumerationAt(int enumerationIndex)
		{
			return this.enumerationList[enumerationIndex];
		}

		public Enumeration getEnumeration(string enumerationName)
		{
			if (enumerationName == null)
			{
				throw new ArgumentNullException("enumerationName");
			}
			if (!this.containsEnumerationName(enumerationName))
			{
				throw new ArgumentException("Домена с именем " + enumerationName + " нет в базе знаний");
			}
			return this.enumerationList[enumerationName];
		}

		public bool containsEnumerationName(string enumerationName)
		{
			return this.enumerationList.Contains(enumerationName);
		}

		public void switchRules(int firstRuleIndex, int secondRuleIndex)
		{
			this.ruleList.Switch(firstRuleIndex, secondRuleIndex);
		}

		public void insertRuleInto(int ruleToInsertIndex, int newRulePositionIndex)
		{
			this.ruleList.Insert(ruleToInsertIndex, newRulePositionIndex);
		}

		public void setAllVariablesToBeUnknown()
		{
			using (IEnumerator<Variable> enumerator = this.variableList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Variable current = enumerator.Current;
					current.prepareForNewConsultation();
				}
			}
		}

		public bool tryToGetValue(Variable variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (!this.variableList.Contains(variable))
			{
				throw new ArgumentOutOfRangeException("variable");
			}
			bool result = false;
			using (IEnumerator<Rule> enumerator = this.ruleList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					if (current.containsVariableInResolutionList(variable) && current.tryToFire())
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public void removeConditionInAnyRule(Condition condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			List<int> list = new List<int>();
			using (IEnumerator<Rule> enumerator = this.ruleList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					if (current.containsCondition(condition))
					{
						current.removeCondition(condition);
						if (current.getConditionCount() == 0 && current.getResolutionCount() == 0)
						{
							list.Add(this.ruleList.IndexOf(current));
						}
					}
				}
			}
			using (List<int>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int current2 = enumerator2.Current;
					this.removeRuleAt(current2);
				}
			}
		}

		public void removeResolutionInAnyRule(Resolution resolution)
		{
			if (resolution == null)
			{
				throw new ArgumentNullException("resolution");
			}
			List<int> list = new List<int>();
			using (IEnumerator<Rule> enumerator = this.ruleList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					if (current.containsResolution(resolution))
					{
						current.removeResolution(resolution);
						if (current.getConditionCount() == 0 && current.getResolutionCount() == 0)
						{
							list.Add(this.ruleList.IndexOf(current));
						}
					}
				}
			}
			using (List<int>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int current2 = enumerator2.Current;
					this.removeRuleAt(current2);
				}
			}
		}
	}
}
