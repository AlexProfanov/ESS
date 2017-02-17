using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ZShellCore
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
				}
				else
				{
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

		public string UniqueVariableName
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
			KnowledgeBase result;
			try
			{
				using (Stream stream = File.Open(fileName, (FileMode)3))
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
				using (Stream stream = File.Open(fileName, (FileMode)4))
				{
					new BinaryFormatter().Serialize(stream, knowledgeBase);
				}
				using (Stream stream = File.Open(fileName, (FileMode)3))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					try
					{
						binaryFormatter.Deserialize(stream);
					}
					catch (SerializationException ex)
					{
						throw new ArgumentException("Сохранение прошло с ошибками:\n" + ex.Message, ex);
					}
				}
			}
			catch (SerializationException ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}
			catch (IOException ex2)
			{
				throw new ArgumentException("В указанный файл ничего записать не удалось", ex2);
			}
		}

		public void addRule(Rule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
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
			if (variable.Domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			if (this.containsVariableName(variable.Name))
			{
				throw new ArgumentException("Переменная с именем " + variable.Name + " уже зарегестрирована в базе знаний");
			}
			this.variableList.Add(variable);
		}

		public void insertVariableInto(int oldVariableIndex, int newVariableIndex)
		{
			this.variableList.Insert(oldVariableIndex, newVariableIndex);
		}

		public void switchVariables(int firstVariableIndex, int secondVariableIndex)
		{
			this.variableList.Switch(firstVariableIndex, secondVariableIndex);
		}

		public IEnumerator<Variable> getEnumeratorForVariables()
		{
			return this.variableList.GetEnumerator();
		}

		public bool hasAnyVariables()
		{
			return this.variableList.Count != 0;
		}

		public void removeVariableAt(int variableIndex)
		{
			if (variableIndex < 0 || variableIndex > this.variableList.Count)
			{
				throw new ArgumentOutOfRangeException("varIndex");
			}
			Variable variable = this.variableList[variableIndex];
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
			this.variableList.RemoveAt(variableIndex);
		}

		public void replaceVariableWith(Variable oldVariable, Variable newVariable)
		{
			if (!this.variableList.Contains(oldVariable))
			{
				throw new ArgumentException("В базе знаний нет такой переменной");
			}
			if (oldVariable.Type != newVariable.Type)
			{
				throw new ArgumentException("Типы новой и старой переменной не совпадают");
			}
			using (IEnumerator<Rule> enumerator = this.ruleList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					current.replaceVariableInConditionResolutionList(oldVariable, newVariable);
				}
			}
			this.variableList[oldVariable] = newVariable;
			if (this.goal == oldVariable)
			{
				if (newVariable is DeducibleVariable || newVariable is DeducibleAskedVariable)
				{
					this.goal = newVariable;
				}
				else
				{
					this.Goal = null;
				}
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
					Rule current = enumerator3.Current;
					this.removeRuleAt(this.ruleList.IndexOf(current));
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

		public int getVariableCount()
		{
			return this.variableList.Count;
		}

		public int getEnumerationCount()
		{
			return this.enumerationList.Count;
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
			this.enumerationList.Insert(domainToInsertIndex, newDomainPositionIndex);
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
			bool result;
			using (IEnumerator<Rule> enumerator = this.ruleList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					if (current.containsVariableInResolutionList(variable) && current.tryToFire())
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
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
