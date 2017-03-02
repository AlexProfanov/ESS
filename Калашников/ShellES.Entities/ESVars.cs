using System;
using System.Collections.Generic;

namespace ShellES.Entities
{
	[Serializable]
	public class ESVars
	{
		public enum VAR_TYPE
		{
			Выводимая,
			Запрашиваемая,
			Выводимо_Запрашиваемая
		}

		private const ESVars.VAR_TYPE VTYPE = ESVars.VAR_TYPE.Выводимая;

		public static string[] Str_VarType = new string[]
		{
			"Выводимая",
			"Запрашиваемая",
			"Выводимо-запрашиваемая"
		};

		private string name;

		private ESDomains domain;

		private ESVars.VAR_TYPE varType;

		private string question;

		private string description;

		private Dictionary<string, int> value;

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

		public ESDomains Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				this.domain = value;
			}
		}

		public ESVars.VAR_TYPE VarType
		{
			get
			{
				return this.varType;
			}
			set
			{
				this.varType = value;
			}
		}

		public string Question
		{
			get
			{
				return this.question;
			}
			set
			{
				this.question = value;
			}
		}

		public Dictionary<string, int> Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		public ESVars Self
		{
			get
			{
				return this;
			}
		}

		public ESVars()
		{
			this.name = "";
			this.domain = null;
			this.varType = ESVars.VAR_TYPE.Выводимая;
			this.question = "";
			this.description = "";
			this.value = new Dictionary<string, int>();
		}

		public ESVars(string newName, ESDomains newDomain, ESVars.VAR_TYPE newVarType)
		{
			this.name = newName;
			this.domain = newDomain;
			this.varType = newVarType;
			this.question = "";
			this.description = "";
			this.value = new Dictionary<string, int>();
		}

		public ESVars(string newName, ESDomains newDomain, ESVars.VAR_TYPE newVarType, string newQuestion, string newDescription)
		{
			this.name = newName;
			this.domain = newDomain;
			this.varType = newVarType;
			this.question = newQuestion;
			this.description = newDescription;
			this.value = new Dictionary<string, int>();
		}

		public void AddValue(string strVal, int CF)
		{
			this.value[strVal] = CF;
		}

		public int GetValueCF(string strVal)
		{
			if (this.value.ContainsKey(strVal))
			{
				return this.value[strVal];
			}
			return 0;
		}

		public bool ExistValue(string strVal)
		{
			return this.value.ContainsKey(strVal);
		}

		public bool IsDefine(int UNKN)
		{
			foreach (KeyValuePair<string, int> current in this.value)
			{
				if (current.Value > UNKN)
				{
					return true;
				}
			}
			return false;
		}

		public void ClearValues()
		{
			this.value.Clear();
		}

		public string GetResults()
		{
			string text = "";
			string text2 = "";
			SortedDictionary<string, int> sortedDictionary = new SortedDictionary<string, int>();
			foreach (KeyValuePair<string, int> current in this.value)
			{
				sortedDictionary.Add(current.Key, current.Value);
			}
			bool flag = true;
			KeyValuePair<string, int> maxInStringDictionary = StaticHelper.GetMaxInStringDictionary(sortedDictionary);
			while (maxInStringDictionary.Key != "")
			{
				if (flag)
				{
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						maxInStringDictionary.Key,
						" [",
						maxInStringDictionary.Value.ToString(),
						"]"
					});
					flag = !flag;
				}
				else
				{
					string text4 = text2;
					text2 = string.Concat(new string[]
					{
						text4,
						maxInStringDictionary.Key,
						" [",
						maxInStringDictionary.Value.ToString(),
						"], "
					});
				}
				sortedDictionary.Remove(maxInStringDictionary.Key);
				maxInStringDictionary = StaticHelper.GetMaxInStringDictionary(sortedDictionary);
			}
			if (text2 != "")
			{
				text2 = " (" + text2.TrimEnd(new char[]
				{
					',',
					' '
				}) + ")";
			}
			if (!(text != ""))
			{
				return "<Переменная не определена>";
			}
			return text + text2;
		}

		public string GenerateQuestion()
		{
			string text = (this.domain != null) ? this.domain.Name.ToLower() : "";
			text = ((text == "") ? this.name.ToLower() : text);
			return string.Format("Выберите {0}:", text);
		}
	}
}
