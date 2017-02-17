using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEs
{
	[Serializable]
	public class RulesList
	{
		public Dictionary<int, Rule> Rules;

		public bool Cascade;

		public bool HasSavedRule;

		private DomainList ToDomains;

		public VariablesList ToVars;

		public RulesList()
		{
			this.Rules = new Dictionary<int, Rule>();
			this.Cascade = true;
			this.HasSavedRule = false;
		}

		public int AddRule(int Index)
		{
			int result;
			if (this.Rules.Count > 0)
			{
				Dictionary<int, Rule> dictionary = new Dictionary<int, Rule>();
				for (int i = 0; i <= this.Rules.Count; i++)
				{
					if (i == Index)
					{
						dictionary.Add(Enumerable.Max(this.Rules.Keys) + 1, new Rule());
					}
					else if (i < Index)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i - 1].Value);
					}
				}
				this.Rules = dictionary;
				result = Enumerable.Max(this.Rules.Keys);
			}
			else
			{
				this.Rules.Add(1, new Rule());
				result = 1;
			}
			return result;
		}

		public bool ChangeRuleName(int IdRule, string RuleName)
		{
			bool result;
			using (List<KeyValuePair<int, Rule>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Rule> current = enumerator.Current;
					if (current.Value.Name.Trim().ToLower() == RuleName.Trim().ToLower() && current.Key != IdRule)
					{
						result = false;
						return result;
					}
				}
			}
			this.Rules[IdRule].Name = RuleName;
			result = true;
			return result;
		}

		public void ChangeRuleReason(int IdRule, string RuleReason)
		{
			this.Rules[IdRule].Reason = RuleReason;
		}

		public void RemoveRule(int IdRule)
		{
			this.Rules.Remove(IdRule);
		}

		public Rule GetRuleById(int IdRule)
		{
			return this.Rules[IdRule];
		}

		public void AddIfFact(int IdRule, int Index)
		{
			this.Rules[IdRule].IfVars.Insert(Index, new RulePair());
		}

		public void ChangeIfFact(int IdRule, int Index, string VarName, string DomainValueName)
		{
			int idVarByName = this.ToVars.GetIdVarByName(VarName);
			this.Rules[IdRule].IfVars[Index].Variable = idVarByName;
			this.Rules[IdRule].IfVars[Index].Value = this.ToDomains.GetIdDomainValueByName(this.ToVars.GetVarById(idVarByName).domain, DomainValueName);
		}

		public void RemoveIfFact(int IdRule, int Index)
		{
			this.Rules[IdRule].IfVars.RemoveAt(Index);
		}

		public void AddThenFact(int IdRule, int Index)
		{
			this.Rules[IdRule].ThenVars.Insert(Index, new RulePair());
		}

		public void ChangeThenFact(int IdRule, int Index, string VarName, string DomainValueName)
		{
			int idVarByName = this.ToVars.GetIdVarByName(VarName);
			this.Rules[IdRule].ThenVars[Index].Variable = idVarByName;
			this.Rules[IdRule].ThenVars[Index].Value = this.ToDomains.GetIdDomainValueByName(this.ToVars.GetVarById(idVarByName).domain, DomainValueName);
		}

		public void RemoveThenFact(int IdRule, int Index)
		{
			this.Rules[IdRule].ThenVars.RemoveAt(Index);
		}

		public void ReplaceIfFacts(int IdRule, int SourceIndex, int TargetIndex)
		{
			RulePair rulePair = this.Rules[IdRule].IfVars[SourceIndex];
			this.Rules[IdRule].IfVars.RemoveAt(SourceIndex);
			this.Rules[IdRule].IfVars.Insert(TargetIndex, rulePair);
		}

		public void ReplaceThenFacts(int IdRule, int SourceIndex, int TargetIndex)
		{
			RulePair rulePair = this.Rules[IdRule].ThenVars[SourceIndex];
			this.Rules[IdRule].ThenVars.RemoveAt(SourceIndex);
			this.Rules[IdRule].ThenVars.Insert(TargetIndex, rulePair);
		}

		public void ReplaceRules(int SourceIndex, int TargetIndex)
		{
			Dictionary<int, Rule> dictionary = new Dictionary<int, Rule>();
			if (TargetIndex < SourceIndex)
			{
				for (int i = 0; i < this.Rules.Count; i++)
				{
					if ((i < SourceIndex && i < TargetIndex) || (i > SourceIndex && i > TargetIndex))
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Value);
					}
					else if (i == TargetIndex)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[SourceIndex].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[SourceIndex].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i - 1].Value);
					}
				}
			}
			else
			{
				for (int i = 0; i < this.Rules.Count; i++)
				{
					if ((i < SourceIndex && i < TargetIndex) || (i > SourceIndex && i > TargetIndex))
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Value);
					}
					else if (i != TargetIndex)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i + 1].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i + 1].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[SourceIndex].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[SourceIndex].Value);
					}
				}
			}
			this.Rules = dictionary;
		}

		public int GetIdRuleByName(string RuleName)
		{
			int result;
			using (Dictionary<int, Rule>.Enumerator enumerator = this.Rules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Rule> current = enumerator.Current;
					if (current.Value.Name.Trim().ToLower() == RuleName.Trim().ToLower())
					{
						result = current.Key;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}

		public int GetRuleIndex(int IdRule)
		{
			return Enumerable.ToList<Rule>(this.Rules.Values).IndexOf(this.Rules[IdRule]);
		}

		public List<int> GetRulesByVar(int IdVar)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.Rules.Count; i++)
			{
				using (List<RulePair>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Value.IfVars.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RulePair current = enumerator.Current;
						if (current.Variable == IdVar)
						{
							list.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key);
							break;
						}
					}
				}
				if (!list.Contains(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key))
				{
					using (List<RulePair>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Value.ThenVars.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							RulePair current = enumerator.Current;
							if (current.Variable == IdVar)
							{
								list.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key);
								break;
							}
						}
					}
				}
			}
			return list;
		}

		public List<int> GetRulesForVar(int IdVar, int Index)
		{
			List<int> list = new List<int>();
			for (int i = Index; i < this.Rules.Count; i++)
			{
				using (List<RulePair>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Value.ThenVars.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RulePair current = enumerator.Current;
						if (current.Variable == IdVar)
						{
							list.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key);
							break;
						}
					}
				}
			}
			return list;
		}

		public List<int> GetRulesByDomain(int IdDomain)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.Rules.Count; i++)
			{
				using (List<RulePair>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Value.IfVars.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RulePair current = enumerator.Current;
						if (this.ToVars.GetVarById(current.Variable).domain == IdDomain)
						{
							list.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key);
							break;
						}
					}
				}
				if (!list.Contains(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key))
				{
					using (List<RulePair>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Value.ThenVars.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							RulePair current = enumerator.Current;
							if (this.ToVars.GetVarById(current.Variable).domain == IdDomain)
							{
								list.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key);
								break;
							}
						}
					}
				}
			}
			return list;
		}

		public void SetDomains(DomainList dom)
		{
			this.ToDomains = dom;
		}

		public void SetVariables(VariablesList dom)
		{
			this.ToVars = dom;
		}

		public List<string> GetRulesNames()
		{
			List<string> list = new List<string>();
			using (List<Rule>.Enumerator enumerator = Enumerable.ToList<Rule>(this.Rules.Values).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					list.Add(current.Name);
				}
			}
			return list;
		}

		public List<int> GetVarsInRule(int IdRule)
		{
			List<int> list = new List<int>();
			using (List<RulePair>.Enumerator enumerator = this.Rules[IdRule].IfVars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RulePair current = enumerator.Current;
					list.Add(current.Variable);
				}
			}
			return list;
		}

		public bool TestForValue(int IdDomain, int IdDomainValue)
		{
			bool result;
			using (List<Rule>.Enumerator enumerator = Enumerable.ToList<Rule>(this.Rules.Values).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Rule current = enumerator.Current;
					using (List<RulePair>.Enumerator enumerator2 = current.IfVars.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							RulePair current2 = enumerator2.Current;
							if (this.ToVars.GetVarById(current2.Variable).domain == IdDomain && current2.Value == IdDomainValue)
							{
								result = false;
								return result;
							}
						}
					}
					using (List<RulePair>.Enumerator enumerator2 = current.ThenVars.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							RulePair current2 = enumerator2.Current;
							if (this.ToVars.GetVarById(current2.Variable).domain == IdDomain && current2.Value == IdDomainValue)
							{
								result = false;
								return result;
							}
						}
					}
				}
			}
			result = true;
			return result;
		}

		public int CloneRule(int IdRule)
		{
			this.Rules.Add(Enumerable.Max(this.Rules.Keys) + 1, (Rule)this.Rules[IdRule].Clone());
			return Enumerable.Max(this.Rules.Keys);
		}

		public int PasteRule(int Index, int IdRule, Rule rul)
		{
			Dictionary<int, Rule> dictionary = new Dictionary<int, Rule>();
			if (IdRule == -1)
			{
				if (this.Rules.Count > 0)
				{
					IdRule = Enumerable.Max(this.Rules.Keys) + 1;
				}
				else
				{
					IdRule = 1;
				}
			}
			for (int i = 0; i <= this.Rules.Count; i++)
			{
				if (i == Index)
				{
					dictionary.Add(IdRule, rul);
				}
				else if (i < Index)
				{
					dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i].Value);
				}
				else
				{
					dictionary.Add(Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, Rule>>(this.Rules)[i - 1].Value);
				}
			}
			this.Rules = dictionary;
			return IdRule;
		}
	}
}
