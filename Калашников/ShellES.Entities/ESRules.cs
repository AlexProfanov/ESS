using System;
using System.Collections.Generic;

namespace ShellES.Entities
{
	[Serializable]
	public class ESRules
	{
		private string name;

		private List<ESFact> premises;

		private List<ESFact> consequences;

		private string reason;

		private int ruleCF;

		private int priority;

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

		public List<ESFact> Premises
		{
			get
			{
				return this.premises;
			}
			set
			{
				this.premises = value;
			}
		}

		public List<ESFact> Consequences
		{
			get
			{
				return this.consequences;
			}
			set
			{
				this.consequences = value;
			}
		}

		public string Reason
		{
			get
			{
				return this.reason;
			}
			set
			{
				this.reason = value;
			}
		}

		public int RuleCF
		{
			get
			{
				return this.ruleCF;
			}
			set
			{
				this.ruleCF = ((value < 0) ? 0 : value);
				this.ruleCF = ((value > 100) ? 100 : value);
			}
		}

		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		public string Presentation
		{
			get
			{
				string text = "";
				string text2 = "";
				string str = "<пусто>";
				for (int i = 0; i < this.premises.Count; i++)
				{
					text += ((i > 0) ? " AND " : "");
					if (this.premises[i].Variable != null)
					{
						text += this.premises[i].Variable.Name;
					}
					else
					{
						text += str;
					}
					text += " = ";
					if (this.premises[i].Value != null)
					{
						text += this.premises[i].Value.Value;
					}
					else
					{
						text += str;
					}
					text += ((this.premises[i].FactCF < 100) ? (" [" + this.premises[i].FactCF.ToString() + "]") : "");
				}
				for (int j = 0; j < this.consequences.Count; j++)
				{
					text2 += ((j > 0) ? "; " : "");
					if (this.consequences[j].Variable != null)
					{
						text2 += this.consequences[j].Variable.Name;
					}
					else
					{
						text2 += str;
					}
					text2 += " = ";
					if (this.consequences[j].Value != null)
					{
						text2 += this.consequences[j].Value.Value;
					}
					else
					{
						text2 += str;
					}
					text2 += ((this.consequences[j].FactCF != 100) ? (" [" + this.consequences[j].FactCF.ToString() + "]") : "");
				}
				return string.Concat(new string[]
				{
					"if (",
					text,
					") then (",
					text2,
					")"
				});
			}
		}

		public ESRules()
		{
			this.name = "";
			this.premises = new List<ESFact>();
			this.consequences = new List<ESFact>();
			this.reason = "";
			this.ruleCF = 100;
			this.priority = 0;
		}

		public ESRules(string Name)
		{
			this.name = Name;
			this.premises = new List<ESFact>();
			this.consequences = new List<ESFact>();
			this.reason = "";
			this.ruleCF = 100;
			this.priority = 0;
		}

		public ESRules(string NewName, string NewReason, int newCF, int newPriority)
		{
			this.name = NewName;
			this.premises = new List<ESFact>();
			this.consequences = new List<ESFact>();
			this.reason = NewReason;
			this.ruleCF = newCF;
			this.priority = newPriority;
		}

		public ESRules(string NewName, List<ESFact> NewPrem, List<ESFact> NewCons, string NewReason, int newCF, int newPriority)
		{
			this.name = NewName;
			this.premises = NewPrem;
			this.consequences = NewCons;
			this.reason = NewReason;
			this.ruleCF = newCF;
			this.priority = newPriority;
		}

		public bool isReadyToFire(int EDGE, bool All)
		{
			foreach (ESFact current in this.premises)
			{
				if (current.Variable.GetValueCF(current.Value.Value) <= EDGE && (current.Variable.IsDefine(EDGE) || current.Variable.VarType != ESVars.VAR_TYPE.Запрашиваемая) && (!All || current.Variable.IsDefine(EDGE) || current.Variable.VarType != ESVars.VAR_TYPE.Выводимо_Запрашиваемая))
				{
					return false;
				}
			}
			return true;
		}

		public int HowMuchPremsIsUnknown(int EDGE)
		{
			int num = 0;
			foreach (ESFact current in this.premises)
			{
				if (!current.Variable.IsDefine(EDGE))
				{
					num++;
				}
			}
			return num;
		}

		public bool HaveFactInPrem(ESVars variable)
		{
			foreach (ESFact current in this.premises)
			{
				if (current.Variable == variable)
				{
					return true;
				}
			}
			return false;
		}

		public bool HaveFactInCons(ESVars variable)
		{
			foreach (ESFact current in this.consequences)
			{
				if (current.Variable == variable)
				{
					return true;
				}
			}
			return false;
		}

		public bool DeleteDomainValue(ESDomains d, ESDomainValue domVal)
		{
			bool result = false;
			for (int i = 0; i < this.premises.Count; i++)
			{
				if (this.premises[i].Value.Equals(domVal))
				{
					this.premises.RemoveAt(i);
					result = true;
					i--;
				}
			}
			for (int j = 0; j < this.consequences.Count; j++)
			{
				if (this.consequences[j].Value.Equals(domVal))
				{
					this.consequences.RemoveAt(j);
					result = true;
					j--;
				}
			}
			return result;
		}

		public bool DeleteVarFromRule(ESVars variable)
		{
			bool result = false;
			for (int i = 0; i < this.premises.Count; i++)
			{
				if (this.premises[i].Variable == variable)
				{
					this.premises.RemoveAt(i);
					result = true;
					i--;
				}
			}
			for (int j = 0; j < this.consequences.Count; j++)
			{
				if (this.consequences[j].Variable == variable)
				{
					this.consequences.RemoveAt(j);
					result = true;
					j--;
				}
			}
			return result;
		}
	}
}
