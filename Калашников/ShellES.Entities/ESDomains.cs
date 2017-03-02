using System;
using System.Collections.Generic;

namespace ShellES.Entities
{
	[Serializable]
	public class ESDomains
	{
		private string name;

		private List<ESDomainValue> elements;

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

		public ESDomains Self
		{
			get
			{
				return this;
			}
		}

		public List<ESDomainValue> Elements
		{
			get
			{
				return this.elements;
			}
		}

		public ESDomains()
		{
			this.name = "";
			this.elements = new List<ESDomainValue>();
		}

		public ESDomains(string DomainName)
		{
			this.name = DomainName;
			this.elements = new List<ESDomainValue>();
		}

		public ESDomains(string DomainName, params string[] domainList)
		{
			this.name = DomainName;
			this.elements = new List<ESDomainValue>();
			for (int i = 0; i < domainList.Length; i++)
			{
				string str = domainList[i];
				this.elements.Add(new ESDomainValue(str));
			}
		}

		public List<string> StringElements()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this.Elements.Count; i++)
			{
				list.Add(this.Elements[i].Value);
			}
			return list;
		}

		public bool ExistValue(string strVal)
		{
			strVal = strVal.Trim().ToUpper();
			foreach (ESDomainValue current in this.elements)
			{
				if (strVal == current.Value.ToUpper())
				{
					return true;
				}
			}
			return false;
		}

		public bool AddValue(string strVal)
		{
			if (!this.ExistValue(strVal))
			{
				this.elements.Add(new ESDomainValue(strVal));
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			return this.name;
		}
	}
}
