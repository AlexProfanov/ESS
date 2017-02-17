using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEs
{
	[Serializable]
	public class Domain : cc
	{
		private Dictionary<int, string> DomainValues;

		private string name;

		public Dictionary<int, string> DomValues
		{
			get
			{
				return this.DomainValues;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public object Clone()
		{
			Domain domain = new Domain();
			domain.Name = this.name;
			using (List<KeyValuePair<int, string>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, string>>(this.DomValues).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, string> current = enumerator.Current;
					domain.DomValues.Add(current.Key, current.Value);
				}
			}
			return domain;
		}

		public Domain()
		{
			this.name = "";
			this.DomainValues = new Dictionary<int, string>();
		}

		public bool ChangeDomainValue(int id, string DomainValue)
		{
			bool result;
			if (!Enumerable.Contains<string>(this.DomainValues.Values, DomainValue))
			{
				this.DomainValues[id] = DomainValue;
				result = true;
			}
			else
			{
				result = (this.DomainValues[id] == DomainValue);
			}
			return result;
		}

		public void ChangeIndex(int SourceIndex, int TargetIndex)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			if (TargetIndex < SourceIndex)
			{
				for (int i = 0; i < this.DomainValues.Count; i++)
				{
					if ((i < SourceIndex && i < TargetIndex) || (i > SourceIndex && i > TargetIndex))
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i].Key, Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i].Value);
					}
					else if (i == TargetIndex)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[SourceIndex].Key, Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[SourceIndex].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i - 1].Value);
					}
				}
			}
			else
			{
				for (int i = 0; i < this.DomainValues.Count; i++)
				{
					if ((i < SourceIndex && i < TargetIndex) || (i > SourceIndex && i > TargetIndex))
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i].Key, Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i].Value);
					}
					else if (i != TargetIndex)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i + 1].Key, Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i + 1].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[SourceIndex].Key, Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[SourceIndex].Value);
					}
				}
			}
			this.DomainValues = dictionary;
		}

		public int AddDomainValue(int Index)
		{
			int result;
			if (this.DomainValues.Count > 0)
			{
				Dictionary<int, string> dictionary = new Dictionary<int, string>();
				for (int i = 0; i <= this.DomainValues.Count; i++)
				{
					if (i == Index)
					{
						dictionary.Add(Enumerable.Max(this.DomainValues.Keys) + 1, "");
					}
					else if (i < Index)
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i].Key, Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i].Value);
					}
					else
					{
						dictionary.Add(Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i - 1].Key, Enumerable.ToList<KeyValuePair<int, string>>(this.DomainValues)[i - 1].Value);
					}
				}
				this.DomainValues = dictionary;
				result = Enumerable.Max(this.DomainValues.Keys);
			}
			else
			{
				this.DomainValues.Add(1, "");
				result = 1;
			}
			return result;
		}

		public void RemoveDomainValue(int id)
		{
			this.DomainValues.Remove(id);
		}

		public int GetIdValueByName(string DomainValueName)
		{
			int result;
			using (Dictionary<int, string>.Enumerator enumerator = this.DomainValues.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, string> current = enumerator.Current;
					if (current.Value.Trim().ToLower() == DomainValueName.Trim().ToLower())
					{
						result = current.Key;
						return result;
					}
				}
			}
			result = -1;
			return result;
		}
	}
}
