using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEs
{
	[Serializable]
	public class VariablesList
	{
		public Dictionary<int, Variable> Vars;

		public bool Cascade;

		public bool HasSavedVar;

		private DomainList ToDomains;

		private RulesList ToRules;

		public VariablesList()
		{
			this.Vars = new Dictionary<int, Variable>();
			this.Cascade = true;
			this.HasSavedVar = false;
		}

		public int AddVar(int Index)
		{
			int result;
			if (this.Vars.Count > 0)
			{
				Dictionary<int, Variable> dictionary = new Dictionary<int, Variable>();
				for (int i = 0; i <= this.Vars.Count; i++)
				{
					if (i == Index)
					{
						dictionary.Add(Enumerable.Max(this.Vars.Keys) + 1, new Variable());
					}
					else if (i < Index)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i - 1].Value);
					}
				}
				this.Vars = dictionary;
				result = Enumerable.Max(this.Vars.Keys);
			}
			else
			{
				this.Vars.Add(1, new Variable());
				result = 1;
			}
			return result;
		}

		public void SetDomains(DomainList dom)
		{
			this.ToDomains = dom;
		}

		public void SetRules(RulesList ruls)
		{
			this.ToRules = ruls;
		}

		public bool ChangeVar(int IdVar, string VarName, string VarType, string DomainName, string VarQuestion, string VarReason)
		{
			this.Vars[IdVar].Name = VarName;
			this.Vars[IdVar].type = VarType;
			this.Vars[IdVar].domain = this.ToDomains.GetIdByName(DomainName);
			if (VarType != "Выводимая" && VarQuestion.Trim() == "")
			{
				this.Vars[IdVar].Question = VarName + "?";
			}
			else
			{
				this.Vars[IdVar].Question = VarQuestion;
			}
			if (VarReason.Trim() == "")
			{
				this.Vars[IdVar].Reason = VarName;
			}
			else
			{
				this.Vars[IdVar].Reason = VarReason;
			}
			return true;
		}

		public bool RemoveVar(int IdVar)
		{
			bool result;
			if (!this.Cascade)
			{
				if (this.ToRules.GetRulesByVar(IdVar).Count == 0)
				{
					this.Vars.Remove(IdVar);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				this.Vars.Remove(IdVar);
				result = true;
			}
			return result;
		}

		public void RemoveVarByDomains(int IdDomain)
		{
			for (int i = this.Vars.Count - 1; i >= 0; i--)
			{
				if (Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Value.domain == IdDomain)
				{
					this.Vars.Remove(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Key);
				}
			}
		}

		public void ReplaceVariables(int SourceIndex, int TargetIndex)
		{
			Dictionary<int, Variable> dictionary = new Dictionary<int, Variable>();
			if (TargetIndex < SourceIndex)
			{
				for (int i = 0; i < this.Vars.Count; i++)
				{
					if ((i < SourceIndex && i < TargetIndex) || (i > SourceIndex && i > TargetIndex))
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Value);
					}
					else if (i == TargetIndex)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[SourceIndex].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[SourceIndex].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i - 1].Value);
					}
				}
			}
			else
			{
				for (int i = 0; i < this.Vars.Count; i++)
				{
					if ((i < SourceIndex && i < TargetIndex) || (i > SourceIndex && i > TargetIndex))
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Value);
					}
					else if (i != TargetIndex)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i + 1].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i + 1].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[SourceIndex].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[SourceIndex].Value);
					}
				}
			}
			this.Vars = dictionary;
		}

		public Variable GetVarById(int IdVar)
		{
			return this.Vars[IdVar];
		}

		public int GetIdVarByName(string VarName)
		{
			int result;
			using (Dictionary<int, Variable>.Enumerator enumerator = this.Vars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Variable> current = enumerator.Current;
					if (current.Value.Name.Trim().ToLower() == VarName.Trim().ToLower())
					{
						result = current.Key;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}

		public bool ContainsDomain(int IdDomain)
		{
			bool result;
			using (List<Variable>.Enumerator enumerator = Enumerable.ToList<Variable>(this.Vars.Values).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Variable current = enumerator.Current;
					if (current.domain == IdDomain)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public List<string> GetVarsNames()
		{
			List<string> list = new List<string>();
			using (List<Variable>.Enumerator enumerator = Enumerable.ToList<Variable>(this.Vars.Values).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Variable current = enumerator.Current;
					if (current.Name != "")
					{
						list.Add(current.Name);
					}
				}
			}
			return list;
		}

		public int GetIdDomainByName(string VarName)
		{
			int result;
			using (Dictionary<int, Variable>.Enumerator enumerator = this.Vars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Variable> current = enumerator.Current;
					if (current.Value.Name == VarName)
					{
						result = current.Value.domain;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}

		public int CloneVar(int IdVar)
		{
			this.Vars.Add(Enumerable.Max(this.Vars.Keys) + 1, (Variable)this.Vars[IdVar].Clone());
			return Enumerable.Max(this.Vars.Keys);
		}

		public int PasteVar(int Index, int IdVar, Variable var)
		{
			Dictionary<int, Variable> dictionary = new Dictionary<int, Variable>();
			if (IdVar == -1)
			{
				if (this.Vars.Count > 0)
				{
					IdVar = Enumerable.Max(this.Vars.Keys) + 1;
				}
				else
				{
					IdVar = 1;
				}
			}
			for (int i = 0; i <= this.Vars.Count; i++)
			{
				if (i == Index)
				{
					dictionary.Add(IdVar, var);
				}
				else if (i < Index)
				{
					dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i].Value);
				}
				else
				{
					dictionary.Add(Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, Variable>>(this.Vars)[i - 1].Value);
				}
			}
			this.Vars = dictionary;
			return IdVar;
		}
	}
}
