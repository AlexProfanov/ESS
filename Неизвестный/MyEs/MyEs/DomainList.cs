using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEs
{
	[Serializable]
	public class DomainList
	{
		public Dictionary<int, Domain> Domains;

		public bool Cascade;

		public bool HasSavedDomain;

		private VariablesList ToVars;

		public DomainList(VariablesList _vars)
		{
			this.Domains = new Dictionary<int, Domain>();
			this.Cascade = true;
			this.HasSavedDomain = false;
			this.ToVars = _vars;
		}

		public List<string> GetDomainsNames()
		{
			List<string> list = new List<string>();
			using (List<Domain>.Enumerator enumerator = Enumerable.ToList<Domain>(this.Domains.Values).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Domain current = enumerator.Current;
					if (current.DomValues.Values.Count > 0 && current.Name != "")
					{
						list.Add(current.Name);
					}
				}
			}
			return list;
		}

		public int GetIdByName(string DomainName)
		{
			int result;
			using (Dictionary<int, Domain>.Enumerator enumerator = this.Domains.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Domain> current = enumerator.Current;
					if (current.Value.Name.Trim().ToLower() == DomainName.Trim().ToLower())
					{
						result = current.Key;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}

		public int GetIdDomainValueByName(int IdDomain, string DomainValue)
		{
			return this.Domains[IdDomain].GetIdValueByName(DomainValue);
		}

		public string GetNameById(int IdDomain)
		{
			return this.Domains[IdDomain].Name;
		}

		public Domain GetDomainById(int IdDomain)
		{
			return this.Domains[IdDomain];
		}

		public string GetDomainValueNameById(int IdDomain, int IDDomainValue)
		{
			return this.Domains[IdDomain].DomValues[IDDomainValue];
		}

		public int AddDomain(int Index)
		{
			int result;
			if (this.Domains.Count > 0)
			{
				Dictionary<int, Domain> dictionary = new Dictionary<int, Domain>();
				for (int i = 0; i <= this.Domains.Count; i++)
				{
					if (i == Index)
					{
						dictionary.Add(Enumerable.Max(this.Domains.Keys) + 1, new Domain());
					}
					else if (i < Index)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i - 1].Value);
					}
				}
				this.Domains = dictionary;
				result = Enumerable.Max(this.Domains.Keys);
			}
			else
			{
				this.Domains.Add(1, new Domain());
				result = 1;
			}
			return result;
		}

		public bool RemoveDomain(int IdDomain)
		{
			bool result;
			if (!this.Cascade)
			{
				if (!this.ToVars.ContainsDomain(IdDomain))
				{
					this.Domains.Remove(IdDomain);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				this.Domains.Remove(IdDomain);
				result = true;
			}
			return result;
		}

		public bool ChangeDomainName(int IdDomain, string NewDomainName)
		{
			bool result;
			using (List<KeyValuePair<int, Domain>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Domain> current = enumerator.Current;
					if (current.Value.Name.Trim().ToLower() == NewDomainName.Trim().ToLower() && current.Key != IdDomain)
					{
						result = false;
						return result;
					}
				}
			}
			this.Domains[IdDomain].Name = NewDomainName;
			result = true;
			return result;
		}

		public bool ChangeDomainValue(int IdDomain, int IdDomainValue, string DomainValue)
		{
			return this.Domains[IdDomain].ChangeDomainValue(IdDomainValue, DomainValue);
		}

		public void ReplaceDomainValues(int IdDomain, int SourceIndex, int TargetIndex)
		{
			this.Domains[IdDomain].ChangeIndex(SourceIndex, TargetIndex);
		}

		public void ReplaceDomains(int SourceIndex, int TargetIndex)
		{
			Dictionary<int, Domain> dictionary = new Dictionary<int, Domain>();
			if (TargetIndex < SourceIndex)
			{
				for (int i = 0; i < this.Domains.Count; i++)
				{
					if ((i < SourceIndex && i < TargetIndex) || (i > SourceIndex && i > TargetIndex))
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i].Value);
					}
					else if (i == TargetIndex)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[SourceIndex].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[SourceIndex].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i - 1].Value);
					}
				}
			}
			else
			{
				for (int i = 0; i < this.Domains.Count; i++)
				{
					if ((i < SourceIndex && i < TargetIndex) || (i > SourceIndex && i > TargetIndex))
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i].Value);
					}
					else if (i != TargetIndex)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i + 1].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i + 1].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[SourceIndex].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[SourceIndex].Value);
					}
				}
			}
			this.Domains = dictionary;
		}

		public int AddDomainValue(int IdDomain, int Index)
		{
			return this.Domains[IdDomain].AddDomainValue(Index);
		}

		public void RemoveDomainValue(int IdDomain, int IdDomainValue)
		{
			this.Domains[IdDomain].RemoveDomainValue(IdDomainValue);
		}

		public Dictionary<int, string> GetDomainValues(int IdDomain)
		{
			return this.Domains[IdDomain].DomValues;
		}

		public int CloneDomain(int IdDomain)
		{
			this.Domains.Add(Enumerable.Max(this.Domains.Keys) + 1, (Domain)this.Domains[IdDomain].Clone());
			return Enumerable.Max(this.Domains.Keys);
		}

		public int PasteDomain(int Index, int IdDomain, Domain Dom)
		{
			Dictionary<int, Domain> dictionary = new Dictionary<int, Domain>();
			if (IdDomain == -1)
			{
				if (this.Domains.Count > 0)
				{
					IdDomain = Enumerable.Max(this.Domains.Keys) + 1;
				}
				else
				{
					IdDomain = 1;
				}
			}
			for (int i = 0; i <= this.Domains.Count; i++)
			{
				if (i == Index)
				{
					dictionary.Add(IdDomain, Dom);
				}
				else if (i < Index)
				{
					dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i].Value);
				}
				else
				{
					dictionary.Add(Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, Domain>>(this.Domains)[i - 1].Value);
				}
			}
			this.Domains = dictionary;
			return IdDomain;
		}
	}
}
