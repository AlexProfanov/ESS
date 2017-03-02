using ShellES.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShellES.Components
{
	[Serializable]
	public class ESDataBase : ComponentPrototype
	{
		private List<ESVars> vars;

		private List<ESDomains> domains;

		private List<ESRules> rules;

		private Settings settings;

		[NonSerialized]
		private BindingSource bindingDomainNames;

		[NonSerialized]
		private BindingSource bindingDomainValues;

		[NonSerialized]
		private BindingSource bindingVars;

		[NonSerialized]
		private BindingSource bindingRules;

		[NonSerialized]
		private BindingSource bindingPrems;

		[NonSerialized]
		private BindingSource bindingCons;

		[NonSerialized]
		private BindingSource bindingPremValues;

		[NonSerialized]
		private BindingSource bindingConsValues;

		[NonSerialized]
		private string dbFilePath = "";

		[NonSerialized]
		private bool changed;

		public List<ESDomains> Domains
		{
			get
			{
				return this.domains;
			}
		}

		public List<ESVars> Vars
		{
			get
			{
				return this.vars;
			}
		}

		public List<ESRules> Rules
		{
			get
			{
				return this.rules;
			}
		}

		public Settings Setting
		{
			get
			{
				return this.settings;
			}
		}

		public string BasePath
		{
			get
			{
				return this.dbFilePath;
			}
			set
			{
				this.dbFilePath = value;
			}
		}

		public bool Changed
		{
			get
			{
				return this.changed;
			}
			set
			{
				this.changed = value;
			}
		}

		public ESDataBase(ExpertSystemShell ess) : base(ess)
		{
			this.InitializeDataBase(ess, null, null, null);
		}

		public ESDataBase(ExpertSystemShell ess, List<ESDomains> Ldom, List<ESVars> Lvar, List<ESRules> Lrul) : base(ess)
		{
			this.InitializeDataBase(ess, Ldom, Lvar, Lrul);
		}

		private void InitializeDataBase(ExpertSystemShell ess, List<ESDomains> Ldom, List<ESVars> Lvar, List<ESRules> Lrul)
		{
			this.domains = ((Ldom != null) ? Ldom : new List<ESDomains>());
			this.vars = ((Lvar != null) ? Lvar : new List<ESVars>());
			this.rules = ((Lrul != null) ? Lrul : new List<ESRules>());
			this.settings = new Settings(ess);
		}

		public void SetOwnerES(ExpertSystemShell ess)
		{
			this.ESshell = ess;
		}

		public void ClearVariableValues()
		{
			foreach (ESVars current in this.vars)
			{
				current.ClearValues();
			}
		}

		public void ResetDefaults()
		{
			this.settings.ResetDefaults();
			this.changed = true;
		}

		public void ClearAll(bool ToClearPath, bool ToClearSettings)
		{
			this.rules.Clear();
			this.vars.Clear();
			this.domains.Clear();
			if (ToClearPath)
			{
				this.dbFilePath = "";
			}
			if (ToClearSettings)
			{
				this.settings.ResetDefaults();
			}
			this.changed = false;
		}

		public void AddDomain(string domainName)
		{
			this.domains.Add(new ESDomains(domainName));
			this.changed = true;
		}

		public void AddDomainValue(ESDomains d, string domainValue)
		{
			d.Elements.Add(new ESDomainValue(domainValue));
			this.changed = true;
		}

		public void AddVar()
		{
			this.vars.Add(new ESVars());
			this.changed = true;
		}

		public void AddRule(string name)
		{
			this.rules.Add(new ESRules(name));
			this.changed = true;
		}

		public void AddFactInPrem(ESRules r)
		{
			this.AddFact(r.Premises);
			this.changed = true;
		}

		public void AddFactInCons(ESRules r)
		{
			this.AddFact(r.Consequences);
			this.changed = true;
		}

		private void AddFact(List<ESFact> factList)
		{
			factList.Add(new ESFact());
		}

		public bool DeleteDomain(ESDomains d, bool isCascade, bool ask)
		{
			string str = isCascade ? "[Каскадно]: переменные, использующие этот домен, также будут удалены" : "[Не каскадно]: переменные, использующие этот домен, не будут удалены";
			if (ask && MessageBox.Show("Вы действительно хотите удалить домен " + d.Name + "?\n" + str, "Удаление домена", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return false;
			}
			for (int i = 0; i < this.vars.Count; i++)
			{
				if (this.vars[i].Domain == d)
				{
					if (isCascade)
					{
						this.DeleteVar(this.vars[i], true, false);
						i--;
					}
					else
					{
						this.vars[i].Domain = null;
					}
				}
			}
			this.changed = true;
			return this.domains.Remove(d);
		}

		public bool DeleteDomainValue(ESDomains d, ESDomainValue domVal, bool isCascade, bool ask)
		{
			string text = isCascade ? "[Каскадно]: факты, содержащие это значение, также будут удалены" : "[Не каскадно]: факты, содержащие это значение, не будут удалены";
			if (ask && MessageBox.Show(string.Concat(new object[]
			{
				"Вы действительно хотите удалить значение домена ",
				domVal,
				"?\n",
				text
			}), "Удаление значения домена", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return false;
			}
			if (isCascade)
			{
				foreach (ESRules current in this.rules)
				{
					current.DeleteDomainValue(d, domVal);
				}
			}
			this.changed = true;
			return d.Elements.Remove(domVal);
		}

		public bool DeleteVar(ESVars v, bool isCascade, bool ask)
		{
			string str = isCascade ? "[Каскадно]: правила, содержащие эту переменную, также будут удалены" : "[Не каскадно]: правила, содержащие эту переменную, также будут удалены";
			if (ask && MessageBox.Show("Вы действительно хотите удалить переменную " + v.Name + "?\n" + str, "Удаление переменной", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return false;
			}
			for (int i = 0; i < this.rules.Count; i++)
			{
				if (this.rules[i].HaveFactInPrem(v) || this.rules[i].HaveFactInCons(v))
				{
					if (isCascade)
					{
						this.rules.RemoveAt(i);
						i--;
					}
					else
					{
						this.rules[i].DeleteVarFromRule(v);
					}
				}
			}
			this.changed = true;
			return this.vars.Remove(v);
		}

		public bool DeleteRule(ESRules r, bool ask)
		{
			if (ask && MessageBox.Show("Вы действительно хотите удалить правило " + r.Name + "?", "Удаление правила", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return false;
			}
			this.changed = true;
			return this.rules.Remove(r);
		}

		public bool DeleteFactInPrem(ESRules r, ESFact f, bool ask)
		{
			return this.DeleteFact(r.Premises, f, ask);
		}

		public bool DeleteFactInCons(ESRules r, ESFact f, bool ask)
		{
			return this.DeleteFact(r.Consequences, f, ask);
		}

		private bool DeleteFact(List<ESFact> factList, ESFact f, bool ask)
		{
			if (ask && MessageBox.Show("Вы действительно хотите удалить данный факт из правила?", "Удаление факта", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return false;
			}
			this.changed = true;
			return factList.Remove(f);
		}

		public ESDomains GetDomainByName(object FormattedValue)
		{
			if (!(FormattedValue is string))
			{
				return null;
			}
			string b = Convert.ToString(FormattedValue).Trim().ToUpper();
			foreach (ESDomains current in this.domains)
			{
				if (current.Name.ToUpper() == b)
				{
					return current;
				}
			}
			return null;
		}

		public int CheckWhileChangingDomain(ESVars var, ESDomains newDom)
		{
			if (var == null)
			{
				return 0;
			}
			if (var.Domain == newDom)
			{
				return 0;
			}
			bool flag = false;
			int num = 0;
			foreach (ESRules current in this.rules)
			{
				flag = false;
				foreach (ESFact current2 in current.Premises)
				{
					if (current2.Variable == var)
					{
						current2.Value = null;
						flag = true;
					}
				}
				foreach (ESFact current3 in current.Consequences)
				{
					if (current3.Variable == var)
					{
						current3.Value = null;
						flag = true;
					}
				}
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		public DialogResult ValidateESDB(RichTextBox TxtBox, bool askSmth)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 3;
			TxtBox.Text += "\n";
			StaticHelper.AddSpecLine(TxtBox, "### -------------------- Валидация ЭС -------------------- ###", 0);
			TxtBox.Text += "\n";
			bool flag = true;
			StaticHelper.AddSpecLine(TxtBox, "1. Проверка доменов...", num3);
			for (int i = 0; i < this.domains.Count; i++)
			{
				if (this.domains[i].Elements.Count == 0)
				{
					StaticHelper.AddSpecLine(TxtBox, "Домен <" + this.domains[i].Name + "> не имеет значений.", 2 * num3, eExplainType.Warning);
					flag = false;
					num2++;
				}
			}
			if (flag)
			{
				TxtBox.Text += " ok";
			}
			TxtBox.Text += "\n";
			flag = true;
			StaticHelper.AddSpecLine(TxtBox, "2. Проверка переменных...", num3);
			for (int j = 0; j < this.vars.Count; j++)
			{
				if (this.vars[j].Domain == null)
				{
					StaticHelper.AddSpecLine(TxtBox, "Переменная <" + this.vars[j].Name + "> не имеет домена!", 2 * num3, eExplainType.Error);
					flag = false;
					num++;
				}
				if (this.vars[j].VarType != ESVars.VAR_TYPE.Выводимая && this.vars[j].Question.Length == 0)
				{
					StaticHelper.AddSpecLine(TxtBox, ESVars.Str_VarType[(int)this.vars[j].VarType] + " переменная <" + this.vars[j].Name + "> должна иметь вопрос!", 2 * num3, eExplainType.Error);
					flag = false;
					num++;
				}
			}
			if (flag)
			{
				TxtBox.Text += " ok";
			}
			TxtBox.Text += "\n";
			StaticHelper.AddSpecLine(TxtBox, "3. Проверка правил...", num3);
			for (int k = 0; k < this.rules.Count; k++)
			{
				flag = true;
				StaticHelper.AddSpecLine(TxtBox, string.Concat(new string[]
				{
					"Проверка правила <",
					this.rules[k].Name,
					"> #",
					(k + 1).ToString(),
					"..."
				}), 2 * num3);
				if (this.rules[k].RuleCF <= this.Setting.EDGE_UNKNOWN)
				{
					StaticHelper.AddSpecLine(TxtBox, "Правило имеeт коэффициент доверия ниже, чем граница CF_UNKNOWN [" + this.Setting.EDGE_UNKNOWN.ToString() + "]", 3 * num3, eExplainType.Warning);
					flag = false;
					num2++;
				}
				if (this.rules[k].Premises.Count == 0)
				{
					StaticHelper.AddSpecLine(TxtBox, "Правило не содержит ни одной посылки", 3 * num3, eExplainType.Warning);
					flag = false;
					num2++;
				}
				for (int l = 0; l < this.rules[k].Premises.Count; l++)
				{
					if (this.rules[k].Premises[l].Variable == null)
					{
						StaticHelper.AddSpecLine(TxtBox, "Посылка #" + (l + 1).ToString() + " должна содержать переменную!", 3 * num3, eExplainType.Error);
						flag = false;
						num++;
					}
					if (this.rules[k].Premises[l].Value == null)
					{
						StaticHelper.AddSpecLine(TxtBox, "Посылка #" + (l + 1).ToString() + " должна иметь значение!", 3 * num3, eExplainType.Error);
						flag = false;
						num++;
					}
					if (this.rules[k].Premises[l].FactCF <= this.Setting.EDGE_UNKNOWN)
					{
						StaticHelper.AddSpecLine(TxtBox, string.Concat(new string[]
						{
							"Посылка #",
							(l + 1).ToString(),
							" имеeт коэффициент доверия нижк, чем граница CF_UNKNOWN [",
							this.Setting.EDGE_UNKNOWN.ToString(),
							"]"
						}), 3 * num3, eExplainType.Warning);
						flag = false;
						num2++;
					}
				}
				if (this.rules[k].Consequences.Count == 0)
				{
					StaticHelper.AddSpecLine(TxtBox, "Правило не содержит ни одного заключения", 3 * num3, eExplainType.Warning);
					flag = false;
					num2++;
				}
				for (int m = 0; m < this.rules[k].Consequences.Count; m++)
				{
					if (this.rules[k].Consequences[m].Variable == null)
					{
						StaticHelper.AddSpecLine(TxtBox, "Заключение #" + (m + 1).ToString() + " должно содержать переменную!", 3 * num3, eExplainType.Error);
						flag = false;
						num++;
					}
					else
					{
						if (this.rules[k].Consequences[m].Variable.VarType == ESVars.VAR_TYPE.Запрашиваемая)
						{
							StaticHelper.AddSpecLine(TxtBox, string.Concat(new string[]
							{
								"Заключение #",
								(m + 1).ToString(),
								" содержит запрашиваемую переменную <",
								this.rules[k].Consequences[m].Variable.Name,
								">!"
							}), 3 * num3, eExplainType.Error);
							flag = false;
							num++;
						}
						if (this.rules[k].HaveFactInPrem(this.rules[k].Consequences[m].Variable))
						{
							StaticHelper.AddSpecLine(TxtBox, string.Concat(new string[]
							{
								"Заключение #",
								(m + 1).ToString(),
								" содержит циклическую ссылку по переменной <",
								this.rules[k].Consequences[m].Variable.Name,
								">!"
							}), 3 * num3, eExplainType.Error);
							flag = false;
							num++;
						}
					}
					if (this.rules[k].Consequences[m].Value == null)
					{
						StaticHelper.AddSpecLine(TxtBox, "Заключение #" + (m + 1).ToString() + " должна иметь значение!", 3 * num3, eExplainType.Error);
						flag = false;
						num++;
					}
					if (this.rules[k].Consequences[m].FactCF <= this.Setting.EDGE_UNKNOWN)
					{
						StaticHelper.AddSpecLine(TxtBox, string.Concat(new string[]
						{
							"Заключение #",
							(m + 1).ToString(),
							" имеeт коэффициент доверия нижк, чем граница CF_UNKNOWN [",
							this.Setting.EDGE_UNKNOWN.ToString(),
							"]"
						}), 3 * num3, eExplainType.Warning);
						flag = false;
						num2++;
					}
				}
				if (flag)
				{
					TxtBox.Text += " ok";
				}
			}
			flag = true;
			TxtBox.Text += "\n";
			StaticHelper.AddSpecLine(TxtBox, "4. Проверка установок...", num3);
			if (this.Setting.EDGE_UNKNOWN > 50)
			{
				StaticHelper.AddSpecLine(TxtBox, "Установка EDGE_UNKNOWN чрезчур велика есть (" + this.Setting.EDGE_UNKNOWN + ").", 2 * num3, eExplainType.Warning);
				flag = false;
				num2++;
			}
			if (this.Setting.Goal == null)
			{
				StaticHelper.AddSpecLine(TxtBox, "Целевая переменная не определена!", 2 * num3, eExplainType.Error);
				flag = false;
				num++;
			}
			else if (this.Setting.Goal.VarType == ESVars.VAR_TYPE.Запрашиваемая)
			{
				StaticHelper.AddSpecLine(TxtBox, "Целевая переменная является запрашиваемой.", 2 * num3, eExplainType.Warning);
				flag = false;
				num2++;
			}
			if (flag)
			{
				TxtBox.Text += " ok";
			}
			TxtBox.Text += "\n";
			StaticHelper.AddSpecLine(TxtBox, string.Concat(new string[]
			{
				"Итого: ",
				num.ToString(),
				" ошибок, ",
				num2.ToString(),
				" опасностей."
			}), num3);
			if (!askSmth)
			{
				return DialogResult.OK;
			}
			if (num != 0)
			{
				MessageBox.Show("В ЭС найдены ошибки! Подробности в логе", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				TxtBox.Focus();
				return DialogResult.Cancel;
			}
			if (num2 > 0)
			{
				return MessageBox.Show("Во время валидации были выявлены некоторые предупреждения.\nВы хотите продолжить консультацию?", "Валидация", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
			}
			return DialogResult.OK;
		}

		public bool IsUniqueDomainName(string name, ESDomains except)
		{
			string b = name.Trim().ToUpper();
			foreach (ESDomains current in this.domains)
			{
				if (current.Name.ToUpper() == b && current != except)
				{
					return false;
				}
			}
			return true;
		}

		public bool IsUniqueDomainValue(ESDomains d, string name, int except)
		{
			string b = name.Trim().ToUpper();
			for (int i = 0; i < d.Elements.Count; i++)
			{
				if (d.Elements[i].Value.ToUpper() == b && i != except)
				{
					return false;
				}
			}
			return true;
		}

		public bool IsUniqueVarName(string name, ESVars except)
		{
			string b = name.Trim().ToUpper();
			foreach (ESVars current in this.vars)
			{
				if (current.Name.ToUpper() == b && current != except)
				{
					return false;
				}
			}
			return true;
		}

		public bool IsUniqueRuleName(string name, ESRules except)
		{
			string b = name.Trim().ToUpper();
			foreach (ESRules current in this.rules)
			{
				if (current.Name.ToUpper() == b && current != except)
				{
					return false;
				}
			}
			return true;
		}

		public void ConnectBindings(BindingSource bDN, BindingSource bDV, BindingSource bVa, BindingSource bRu, BindingSource bPr, BindingSource bCo, BindingSource bPV, BindingSource bCV)
		{
			this.bindingDomainNames = bDN;
			this.bindingDomainValues = bDV;
			this.bindingVars = bVa;
			this.bindingRules = bRu;
			this.bindingPrems = bPr;
			this.bindingCons = bCo;
			this.bindingPremValues = bPV;
			this.bindingConsValues = bCV;
		}

		public void SetMainBindings()
		{
			this.bindingDomainNames.DataSource = this.domains;
			this.bindingVars.DataSource = this.vars;
			this.bindingRules.DataSource = this.rules;
		}

		public void BindingsRefresh_DomainName()
		{
			if (this.bindingVars.Current == null)
			{
				return;
			}
			ESDomains domain = (this.bindingVars.Current as ESVars).Domain;
			if (domain == null)
			{
				return;
			}
			int num = this.bindingDomainNames.IndexOf(domain);
			if (num >= 0)
			{
				this.bindingDomainNames.Position = num;
			}
		}

		public void BindingsRefresh_DomainValue()
		{
			if (this.bindingDomainNames.Current != null)
			{
				this.bindingDomainValues.DataSource = (this.bindingDomainNames.Current as ESDomains).Elements;
				return;
			}
			this.bindingDomainValues.DataSource = null;
		}

		public void BindingRefresh_RulePremCons()
		{
			this.BindingRefresh_PremValues();
			this.BindingRefresh_ConsValues();
			if (this.bindingRules != null)
			{
				ESRules eSRules = this.bindingRules.Current as ESRules;
				if (eSRules != null)
				{
					this.bindingPrems.DataSource = eSRules.Premises;
					this.bindingCons.DataSource = eSRules.Consequences;
					return;
				}
				this.bindingPrems.DataSource = null;
				this.bindingCons.DataSource = null;
			}
		}

		private void BindingRefresh_PremValues()
		{
			if (this.bindingRules != null)
			{
				ESRules eSRules = this.bindingRules.Current as ESRules;
				this.bindingPremValues.Clear();
				if (eSRules != null)
				{
					for (int i = 0; i < eSRules.Premises.Count; i++)
					{
						if (eSRules.Premises[i].Value != null)
						{
							this.bindingPremValues.Add(eSRules.Premises[i].Value);
						}
					}
				}

			}
		}

		private void BindingRefresh_ConsValues()
		{
			if (this.bindingRules != null)
			{
				ESRules eSRules = this.bindingRules.Current as ESRules;
				this.bindingConsValues.Clear();
				if (eSRules != null)
				{
					for (int i = 0; i < eSRules.Consequences.Count; i++)
					{
						if (eSRules.Consequences[i].Value != null)
						{
							this.bindingConsValues.Add(eSRules.Consequences[i].Value);
						}
					}
				}
			}
		}
	}
}
