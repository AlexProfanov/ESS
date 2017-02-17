using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ExpertSystem
{
	public class Expert
	{
		public static class ExpertEvents
		{
			public delegate void vD(Expert.Domain d, List<string> ow, List<string> nw);

			public delegate void vv();

			public static event Expert.ExpertEvents.vD DomainChangedEv;

			public static event Expert.ExpertEvents.vv ExpertChangedEvent;

			public static void DomainChanged(Expert.Domain d, List<string> ow, List<string> nw)
			{
				if (Expert.ExpertEvents.DomainChangedEv != null)
				{
					Expert.ExpertEvents.DomainChangedEv(d, ow, nw);
				}
			}

			public static void ExpertChanged()
			{
				if (Expert.ExpertEvents.ExpertChangedEvent != null)
				{
					Expert.ExpertEvents.ExpertChangedEvent();
				}
			}
		}

		private class Counter
		{
			private static int pos = -1;

			public static int Pos
			{
				get
				{
					Expert.Counter.pos++;
					return Expert.Counter.pos;
				}
			}

			public static void reset()
			{
				Expert.Counter.pos = -1;
			}
		}

		public class Domain
		{
			private string name;

			private List<Expert.Variable> vars = new List<Expert.Variable>();

			private List<string> values = new List<string>();

			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					if (this.name != "")
					{
						if (value != this.name && this.vars.Count != 0)
						{
							throw new Exception("Невозможно изменить используемы домен!");
						}
					}
					if (value != "")
					{
						if (this.name != value)
						{
							Expert.Cor.makedirty();
						}
						if (value != null)
						{
							value = value.ToUpper().Trim();
						}
						this.name = value;
					}
				}
			}

			public List<Expert.Variable> Vars
			{
				get
				{
					return this.vars;
				}
			}

			public List<string> Values
			{
				get
				{
					return this.values;
				}
				set
				{
					this.values = value;
				}
			}

			public Domain(string _name, List<string> vals)
			{
				this.name = _name;
				this.values = vals;
			}

			public void AddVar(Expert.Variable v)
			{
				if (v != null && !(v.Name == ""))
				{
					this.vars.Add(v);
					Expert.Cor.makedirty();
				}
			}

			public void RemoveVar(Expert.Variable v)
			{
				if (v != null && !(v.Name == ""))
				{
					this.vars.Remove(v);
					Expert.Cor.makedirty();
				}
			}

			public bool AddVal(string val)
			{
				val = val.Trim().ToUpper();
				bool result;
				if (!this.values.Contains(val))
				{
					result = false;
				}
				else
				{
					if (this.inRealUse())
					{
						throw new Exception("Невозможно изменить используемый домен!");
					}
					this.values.Add(val);
					Expert.Cor.makedirty();
					result = true;
				}
				return result;
			}

			public bool DeleteVal(string val)
			{
				val = val.Trim().ToUpper();
				bool result;
				if (!this.values.Contains(val))
				{
					result = false;
				}
				else
				{
					if (this.inRealUse())
					{
						throw new Exception("Невозможно изменить используемый домен!");
					}
					this.values.Remove(val);
					Expert.Cor.makedirty();
					result = true;
				}
				return result;
			}

			public void assignNewVals(List<string> nv)
			{
				if (nv.Count != 0)
				{
					bool flag = false;
					using (List<string>.Enumerator enumerator = this.values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string current = enumerator.Current;
							flag |= !nv.Contains(current);
						}
					}
					flag |= (nv.Count != this.values.Count);
					if (this.inRealUse() && flag)
					{
						throw new Exception("Невозможно изменить значения в используемом домене!");
					}
					Expert.ExpertEvents.DomainChanged(this, this.values, nv);
					this.values = nv;
					Expert.ExpertEvents.ExpertChanged();
					Expert.Cor.makedirty();
				}
			}

			public bool inRealUse()
			{
				bool flag = false;
				string text = this.name.ToUpper().Trim();
				using (List<Expert.Variable>.Enumerator enumerator = Expert.Cor.variables.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Expert.Variable current = enumerator.Current;
						flag |= (current.Dom.Name.Trim().ToUpper() == text);
					}
				}
				return flag;
			}
		}

		public enum VarType
		{
			vtVivod = 0,
			vtZapr = 1,
			vtVivZapr = 2
		}

		public class Variable
		{
			private Expert.VarType __vt = Expert.VarType.vtZapr;

			private string question = "";

			private string name = "";

			private Expert.Domain dom;

			private List<Expert.Fact> facts = new List<Expert.Fact>();

			public string Question
			{
				get
				{
					return this.question;
				}
				set
				{
					if (this.inRealUse())
					{
						throw new Exception("Переменная используется");
					}
					if (value != "" && value != null)
					{
						this.question = value;
					}
				}
			}

			public Expert.VarType vt
			{
				get
				{
					return this.__vt;
				}
				set
				{
					this.__vt = value;
					if (this.__vt != Expert.VarType.vtVivod && this.question == "")
					{
						this.question = this.name + "?";
					}
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
					if (this.inRealUse())
					{
						throw new Exception("Переменная используется");
					}
					if (this.dom != null)
					{
						this.dom.RemoveVar(this);
					}
					if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
					{
						this.name = value.Trim().ToUpper();
						Expert.Cor.makedirty();
						if (this.Question == "")
						{
							this.Question = value + "?";
						}
					}
					else if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
					{
						this.name = "НЕ_ЗАДАННОЕ_ИМЯ";
						this.Question = this.name + "?";
						Expert.Cor.makedirty();
					}
					if (this.dom != null)
					{
						this.dom.AddVar(this);
					}
				}
			}

			public Expert.Domain Dom
			{
				get
				{
					return this.dom;
				}
				set
				{
					if (this.inRealUse())
					{
						throw new Exception("Перемнная используется");
					}
					if (this.dom != null)
					{
						this.dom.RemoveVar(this);
					}
					this.dom = value;
					if (this.dom != null)
					{
						this.dom.AddVar(this);
					}
					Expert.Cor.makedirty();
				}
			}

			public List<Expert.Fact> Facts
			{
				get
				{
					return this.facts;
				}
			}

			public Variable(string _name, string q, Expert.Domain d, Expert.VarType _vt)
			{
				if (_name != null)
				{
					_name = _name.ToUpper().Trim();
				}
				this.Question = q;
				this.Dom = d;
				this.Name = _name;
				this.vt = _vt;
			}

			~Variable()
			{
				try
				{
					this.Dom = null;
				}
				catch (Exception)
				{
					this.dom = null;
				}
			}

			public void addFact(Expert.Fact f)
			{
				if (!(f == null))
				{
					if (!this.facts.Contains(f))
					{
						this.facts.Add(f);
						Expert.Cor.makedirty();
					}
				}
			}

			public void removeFact(Expert.Fact f)
			{
				if (!(f == null))
				{
					this.facts.Remove(f);
					Expert.Cor.makedirty();
				}
			}

			public bool ChangeThis(string nm, string qs, string dn, Expert.VarType _vt)
			{
				bool result;
				if (this.dom == null)
				{
					result = false;
				}
				else if (this.name == nm && this.question == qs && this.dom.Name == dn && this.vt == _vt)
				{
					result = false;
				}
				else if (this.inRealUse())
				{
					MessageBox.Show("Переменная используется");
					result = false;
				}
				else
				{
					this.vt = _vt;
					if (qs != this.question)
					{
						this.Question = qs;
					}
					if (this.name != nm)
					{
						this.Name = nm;
					}
					Expert.Domain domain = Expert.cor.DomainByName(dn);
					if (domain != this.dom)
					{
						this.Dom = domain;
					}
					Expert.ExpertEvents.ExpertChanged();
					result = true;
				}
				return result;
			}

			public bool containFact(Expert.Fact f)
			{
				return this.facts.Contains(f);
			}

			public bool inRealUse()
			{
				bool result;
				try
				{
					bool flag = false;
					string text = this.name.ToUpper().Trim();
					using (Dictionary<int, Expert.Rule>.ValueCollection.Enumerator enumerator = Expert.cor.rules.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Expert.Rule current = enumerator.Current;
							flag |= (text == current.RuleResult.Var.name.ToUpper().Trim());
							using (Dictionary<int, Expert.Fact>.ValueCollection.Enumerator enumerator2 = current.Facts.Values.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									Expert.Fact current2 = enumerator2.Current;
									flag |= (text == current2.Var.name.ToUpper().Trim());
								}
							}
						}
					}
					result = flag;
				}
				catch (Exception)
				{
					result = true;
				}
				return result;
			}
		}

		public class Fact
		{
			private Expert.Variable varb = null;

			private Expert.Domain dom = null;

			private int posAtDom = 0;

			public int posAtRule = -1;

			public Expert.Variable Var
			{
				get
				{
					return this.varb;
				}
				set
				{
					if (this.varb != null)
					{
						this.varb.removeFact(this);
					}
					this.varb = value;
					if (this.varb != null)
					{
						this.dom = this.varb.Dom;
						this.varb.addFact(this);
					}
					else
					{
						this.dom = null;
					}
					Expert.Cor.makedirty();
				}
			}

			public Expert.Domain Dom
			{
				get
				{
					return this.dom;
				}
				set
				{
					this.dom = value;
				}
			}

			public int PosAtDom
			{
				get
				{
					return this.posAtDom;
				}
				set
				{
					this.posAtDom = value;
				}
			}

			public string DomStrValue()
			{
				return this.Dom.Values[this.posAtDom];
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"(",
					(this.varb != null) ? this.varb.Name : "UNKNOW VAR",
					" = \"",
					(this.dom != null) ? this.dom.Values[this.posAtDom] : "UNKNOW DOMEN VAL",
					"\")"
				});
			}

			public override bool Equals(object obj)
			{
				Expert.Fact fact = (Expert.Fact)obj;
				bool result;
				if (fact != null)
				{
					result = (this == fact);
				}
				else
				{
					result = base.Equals(obj);
				}
				return result;
			}

			public static bool operator ==(Expert.Fact f1, Expert.Fact f2)
			{
				bool result;
				try
				{
					if (object.ReferenceEquals(f1, null))
					{
						result = object.ReferenceEquals(f2, null);
					}
					else if (object.ReferenceEquals(f2, null))
					{
						result = false;
					}
					else
					{
						result = (f1.Dom.Name == f2.Dom.Name && f1.posAtDom == f2.posAtDom && f1.Var.Name == f2.Var.Name);
					}
				}
				catch (Exception)
				{
					result = false;
				}
				return result;
			}

			public static bool operator !=(Expert.Fact f1, Expert.Fact f2)
			{
				return !(f1 == f2);
			}

			public Fact()
			{
				Expert.ExpertEvents.DomainChangedEv += new Expert.ExpertEvents.vD(this.onDomChng);
			}

			~Fact()
			{
				Expert.ExpertEvents.DomainChangedEv -= new Expert.ExpertEvents.vD(this.onDomChng);
				this.Var = null;
			}

			private void onDomChng(Expert.Domain d, List<string> ow, List<string> nw)
			{
				if (this.dom == d)
				{
					string text = ow[this.posAtDom];
					if (!nw.Contains(text))
					{
						throw new Exception("Некорректное изменения в факте с переменной " + this.Var.Name);
					}
					this.posAtDom = nw.IndexOf(text);
					Expert.Cor.makedirty();
				}
			}
		}

		public class Rule
		{
			private Dictionary<int, Expert.Fact> facts = new Dictionary<int, Expert.Fact>();

			private Expert.Fact ruleResult = null;

			private string name;

			private bool namereuild = true;

			private int posAtRuleSet = -1;

			public string Reason = "";

			public string Name
			{
				get
				{
					if (this.namereuild && string.IsNullOrEmpty(this.name))
					{
						this.namereuild = false;
						this.name = Expert.cor.GetNextRuleName();
					}
					return this.name;
				}
				set
				{
					if (!string.IsNullOrEmpty(value))
					{
						this.name = value;
					}
					else if (string.IsNullOrEmpty(this.name))
					{
						this.name = Expert.cor.GetNextRuleName();
					}
				}
			}

			public Expert.Fact RuleResult
			{
				get
				{
					return this.ruleResult;
				}
				set
				{
					this.ruleResult = value;
				}
			}

			public int PosAtRuleSet
			{
				get
				{
					return this.posAtRuleSet;
				}
				set
				{
					this.posAtRuleSet = value;
				}
			}

			public Dictionary<int, Expert.Fact> Facts
			{
				get
				{
					return this.facts;
				}
			}

			public string ReadName()
			{
				return this.name;
			}

			public string RuleResDomValue()
			{
				return this.ruleResult.DomStrValue();
			}

			public override string ToString()
			{
				string result;
				if (this.facts.Count == 0)
				{
					if (this.ruleResult != null)
					{
						result = "___Нет_фактов___" + this.ruleResult.ToString();
					}
					else
					{
						result = "___Нет_фактов___";
					}
				}
				else
				{
					bool flag = false;
					string text = "";
					using (Dictionary<int, Expert.Fact>.ValueCollection.Enumerator enumerator = this.facts.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Expert.Fact current = enumerator.Current;
							if (flag)
							{
								text += " AND";
							}
							else
							{
								flag = true;
							}
							text = text + " " + current.ToString();
						}
					}
					result = text;
				}
				return result;
			}

			private int lastFreePos()
			{
				int num = -1;
				using (Dictionary<int, Expert.Fact>.Enumerator enumerator = this.facts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Expert.Fact> current = enumerator.Current;
						if (current.Key > num)
						{
							num = current.Key;
						}
					}
				}
				num++;
				return num;
			}

			public bool addFact(Expert.Fact f, int pos = -1)
			{
				bool result;
				if (f == null)
				{
					result = false;
				}
				else
				{
					bool flag = false;
					using (Dictionary<int, Expert.Fact>.ValueCollection.Enumerator enumerator = this.facts.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Expert.Fact current = enumerator.Current;
							bool flag2 = current.Var.Name == f.Var.Name && current.Dom.Name == f.Dom.Name && current.posAtRule == f.posAtRule;
							flag |= flag2;
						}
					}
					if (flag)
					{
						result = false;
					}
					else
					{
						if (pos == -1 || pos > this.facts.Keys.Count)
						{
							pos = this.lastFreePos();
						}
						this.facts[pos] = f;
						f.posAtRule = pos;
						Expert.Cor.makedirty();
						result = true;
					}
				}
				return result;
			}

			public bool removeFact(int pos)
			{
				bool result;
				if (!this.facts.ContainsKey(pos))
				{
					result = false;
				}
				else
				{
					Expert.Fact fact = this.facts[pos];
					if (pos == -1)
					{
						result = false;
					}
					else
					{
						for (int i = pos; i < this.facts.Count - 1; i++)
						{
							this.facts[i] = this.facts[i + 1];
						}
						this.facts.Remove(this.facts.Count - 1);
						Expert.Cor.makedirty();
						result = true;
					}
				}
				return result;
			}

			public bool swapFactsAt(int i, int j)
			{
				bool result;
				if (!this.facts.ContainsKey(i) || !this.facts.ContainsKey(j))
				{
					result = false;
				}
				else
				{
					Expert.Fact fact = this.facts[i];
					Expert.Fact fact2 = this.facts[j];
					if (fact == null || fact2 == null || fact == fact2)
					{
						result = false;
					}
					else
					{
						this.facts[i] = fact2;
						this.facts[j] = fact;
						Expert.Cor.makedirty();
						result = true;
					}
				}
				return result;
			}
		}

		private bool isDirty = false;

		private static Expert cor = null;

		private List<Expert.Domain> domains = new List<Expert.Domain>();

		private List<Expert.Variable> variables = new List<Expert.Variable>();

		private Dictionary<int, Expert.Rule> rules = new Dictionary<int, Expert.Rule>();

		private int lastind = 0;

		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		public static Expert Cor
		{
			get
			{
				if (Expert.cor == null)
				{
					Expert.cor = new Expert();
					Expert.ExpertEvents.ExpertChanged();
				}
				return Expert.cor;
			}
		}

		public List<Expert.Domain> Domains
		{
			get
			{
				return this.domains;
			}
		}

		public List<Expert.Variable> Variables
		{
			get
			{
				return this.variables;
			}
		}

		public Dictionary<int, Expert.Rule> Rules
		{
			get
			{
				return this.rules;
			}
		}

		private Expert()
		{
		}

		public void makedirty()
		{
			this.isDirty = true;
		}

		public static void CreateNewExpert()
		{
			Expert.cor = new Expert();
			Expert.ExpertEvents.ExpertChanged();
			Expert.cor.isDirty = false;
		}

		private string serealise()
		{
			string text = "";
			text = text + this.domains.Count.ToString() + "\n";
			using (List<Expert.Domain>.Enumerator enumerator = this.domains.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Expert.Domain current = enumerator.Current;
					text = text + current.Name + "\n";
					text = text + current.Values.Count.ToString() + "\n";
					using (List<string>.Enumerator enumerator2 = current.Values.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string current2 = enumerator2.Current;
							text = text + current2 + "\n";
						}
					}
				}
			}
			text = text + this.variables.Count.ToString() + "\n";
			using (List<Expert.Variable>.Enumerator enumerator3 = this.variables.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					Expert.Variable current3 = enumerator3.Current;
					text = text + current3.Name + "\n";
					text = text + current3.Question + "\n";
					text = text + ((int)current3.vt).ToString() + "\n";
					text = text + current3.Dom.Name + "\n";
				}
			}
			text = text + this.rules.Count.ToString() + "\n";
			using (Dictionary<int, Expert.Rule>.ValueCollection.Enumerator enumerator4 = this.rules.Values.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					Expert.Rule current4 = enumerator4.Current;
					text = text + current4.Name + "\n";
					text = text + current4.Reason + "\n";
					text += ((current4.RuleResult == null) ? "n" : "y\n");
					if (current4.RuleResult != null)
					{
						text = text + current4.RuleResult.Var.Name + "\n";
						text = text + current4.RuleResult.PosAtDom.ToString() + "\n";
					}
					text = text + current4.Facts.Count.ToString() + "\n";
					using (Dictionary<int, Expert.Fact>.ValueCollection.Enumerator enumerator5 = current4.Facts.Values.GetEnumerator())
					{
						while (enumerator5.MoveNext())
						{
							Expert.Fact current5 = enumerator5.Current;
							text = text + current5.Var.Name + "\n";
							text = text + current5.PosAtDom.ToString() + "\n";
						}
					}
				}
			}
			return text;
		}

		private Expert deseriallize(string des)
		{
			Expert result;
			try
			{
				string[] array = des.Split(new char[]
				{
					'\n'
				});
				Expert expert = new Expert();
				if (array.Length == 0)
				{
					result = expert;
				}
				else
				{
					Expert.Counter.reset();
					int num = int.Parse(array[Expert.Counter.Pos]);
					for (int i = 0; i < num; i++)
					{
						string name = array[Expert.Counter.Pos];
						List<string> list = new List<string>();
						int num2 = int.Parse(array[Expert.Counter.Pos]);
						for (int j = 0; j < num2; j++)
						{
							list.Add(array[Expert.Counter.Pos]);
						}
						Expert.Domain domain = new Expert.Domain(name, list);
						expert.domains.Add(domain);
					}
					int num3 = int.Parse(array[Expert.Counter.Pos]);
					for (int i = 0; i < num3; i++)
					{
						string name = array[Expert.Counter.Pos];
						string q = array[Expert.Counter.Pos];
						Expert.VarType vt = (Expert.VarType)int.Parse(array[Expert.Counter.Pos]);
						Expert.Domain domain = expert.DomainByName(array[Expert.Counter.Pos]);
						Expert.Variable variable = new Expert.Variable(name, q, domain, vt);
						expert.variables.Add(variable);
					}
					int num4 = int.Parse(array[Expert.Counter.Pos]);
					for (int i = 0; i < num4; i++)
					{
						Expert.Rule rule = new Expert.Rule();
						string name = array[Expert.Counter.Pos];
						rule.Name = name;
						string reason = array[Expert.Counter.Pos];
						rule.Reason = reason;
						bool flag = array[Expert.Counter.Pos][0] == 'y';
						if (flag)
						{
							rule.RuleResult = new Expert.Fact
							{
								Var = expert.varByName(array[Expert.Counter.Pos]),
								PosAtDom = int.Parse(array[Expert.Counter.Pos])
							};
						}
						int num5 = int.Parse(array[Expert.Counter.Pos]);
						for (int j = 0; j < num5; j++)
						{
							rule.addFact(new Expert.Fact
							{
								Var = expert.varByName(array[Expert.Counter.Pos]),
								PosAtDom = int.Parse(array[Expert.Counter.Pos])
							}, -1);
						}
						expert.AcceptRule(rule, -1);
					}
					result = expert;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при открытии файла! " + ex.Message);
				result = null;
			}
			return result;
		}

		public bool save(string path, bool silent = true)
		{
			bool result;
			try
			{
				string text = this.serealise();
				if (File.Exists(path))
				{
					if (!silent && MessageBox.Show("Старый файл будет удален. Продолжить?", "", (System.Windows.Forms.MessageBoxButtons)1) != (DialogResult)1)
					{
						result = false;
						return result;
					}
					File.Delete(path);
					FileStream fileStream = File.Create(path);
					fileStream.Close();
				}
				File.WriteAllText(path, text);
				this.isDirty = false;
				result = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				result = false;
			}
			return result;
		}

		public bool open(string path)
		{
			bool result;
			try
			{
				if (this.isDirty && MessageBox.Show("Изменения не были сохранены, продолжить открытие файла?", "", (System.Windows.Forms.MessageBoxButtons)1) != (DialogResult)1)
				{
					result = false;
				}
				else
				{
					string des = File.ReadAllText(path);
					Expert expert = this.deseriallize(des);
					Expert.cor = expert;
					this.isDirty = false;
					Expert.ExpertEvents.ExpertChanged();
					result = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				result = false;
			}
			return result;
		}

		public string GetNextDomName()
		{
			string text = "";
			int num = 0;
			bool flag = true;
			while (flag)
			{
				flag = false;
				num++;
				text = "DOMAIN " + num.ToString();
				using (List<Expert.Domain>.Enumerator enumerator = this.domains.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Expert.Domain current = enumerator.Current;
						flag |= (current.Name == text);
						if (flag)
						{
							break;
						}
					}
				}
			}
			return text;
		}

		public Expert.Domain AddNewDomain(string name, List<string> vals, int ind = -1)
		{
			name = name.Trim().ToUpper();
			Expert.Domain result;
			if (name == "")
			{
				result = null;
			}
			else
			{
				using (List<Expert.Domain>.Enumerator enumerator = this.domains.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Expert.Domain current = enumerator.Current;
						if (current.Name == name)
						{
							result = null;
							return result;
						}
					}
				}
				for (int i = vals.Count - 1; i >= 0; i--)
				{
					if (vals.IndexOf(vals[i]) < i)
					{
						vals.RemoveAt(i);
					}
				}
				Expert.Domain domain = new Expert.Domain(name, vals);
				if (ind == -1 || ind >= this.domains.Count)
				{
					this.domains.Add(domain);
				}
				else
				{
					this.domains.Insert(ind, domain);
				}
				Expert.ExpertEvents.ExpertChanged();
				Expert.Cor.makedirty();
				result = domain;
			}
			return result;
		}

		public bool DeleteDomain(string name, bool raiseEv = false)
		{
			name = name.ToUpper().Trim();
			Expert.Domain domain = this.DomainByName(name);
			bool result;
			if (domain == null)
			{
				result = false;
			}
			else if (domain.inRealUse())
			{
				MessageBox.Show("Домен используется в ЭС!");
				result = false;
			}
			else
			{
				this.domains.Remove(domain);
				Expert.Cor.makedirty();
				if (raiseEv)
				{
					Expert.ExpertEvents.ExpertChanged();
				}
				result = true;
			}
			return result;
		}

		public Expert.Domain DomainByName(string name)
		{
			name = name.Trim().ToUpper();
			Expert.Domain result = null;
			using (List<Expert.Domain>.Enumerator enumerator = this.domains.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Expert.Domain current = enumerator.Current;
					if (current.Name == name)
					{
						result = current;
					}
				}
			}
			return result;
		}

		public string GetNextVarName()
		{
			string text = "";
			int num = 0;
			bool flag = true;
			while (flag)
			{
				flag = false;
				num++;
				text = "VAR " + num.ToString();
				using (List<Expert.Variable>.Enumerator enumerator = this.variables.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Expert.Variable current = enumerator.Current;
						flag |= (current.Name == text);
						if (flag)
						{
							break;
						}
					}
				}
			}
			return text;
		}

		public Expert.Variable addVar(string name, Expert.VarType vt, Expert.Domain d, string quest = "", int ind = -1)
		{
			name = name.Trim().ToUpper();
			Expert.Variable result;
			if (name == "")
			{
				result = null;
			}
			else
			{
				using (List<Expert.Variable>.Enumerator enumerator = this.variables.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Expert.Variable current = enumerator.Current;
						if (current.Name == name)
						{
							result = null;
							return result;
						}
					}
				}
				Expert.Variable variable = new Expert.Variable(name, quest, d, vt);
				if (ind == -1 || ind >= this.domains.Count)
				{
					this.variables.Add(variable);
				}
				else
				{
					this.variables.Insert(ind, variable);
				}
				Expert.ExpertEvents.ExpertChanged();
				Expert.Cor.makedirty();
				result = variable;
			}
			return result;
		}

		public Expert.Variable varByName(string name)
		{
			name = name.ToUpper().Trim();
			Expert.Variable result = null;
			using (List<Expert.Variable>.Enumerator enumerator = this.variables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Expert.Variable current = enumerator.Current;
					if (current.Name == name)
					{
						result = current;
						break;
					}
				}
			}
			return result;
		}

		public bool deleteVar(string name)
		{
			Expert.Variable variable = this.varByName(name);
			bool result;
			if (variable == null)
			{
				result = false;
			}
			else if (variable.inRealUse())
			{
				MessageBox.Show("used in fact");
				result = false;
			}
			else
			{
				variable.Dom.RemoveVar(variable);
				this.variables.Remove(variable);
				result = true;
			}
			return result;
		}

		public string GetNextRuleName()
		{
			string text = "";
			int num = this.lastind;
			bool flag = true;
			while (flag)
			{
				flag = false;
				num++;
				text = "R" + num.ToString();
				using (Dictionary<int, Expert.Rule>.ValueCollection.Enumerator enumerator = this.rules.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Expert.Rule current = enumerator.Current;
						flag |= (!string.IsNullOrEmpty(current.ReadName()) && current.ReadName() == text);
						if (flag)
						{
							break;
						}
					}
				}
			}
			this.lastind = num;
			return text;
		}

		public bool AddRule(Expert.Rule newRule, int pos = -1)
		{
			bool result;
			if (pos == -1)
			{
				this.rules[this.rules.Keys.Count] = newRule;
				Expert.ExpertEvents.ExpertChanged();
				Expert.Cor.makedirty();
				result = true;
			}
			else if (!this.rules.ContainsKey(pos))
			{
				this.rules[pos] = newRule;
				Expert.ExpertEvents.ExpertChanged();
				Expert.Cor.makedirty();
				result = true;
			}
			else
			{
				for (int i = this.rules.Keys.Count; i > pos; i--)
				{
					this.rules[i] = this.rules[i - 1];
				}
				this.rules[pos] = newRule;
				Expert.ExpertEvents.ExpertChanged();
				Expert.Cor.makedirty();
				result = true;
			}
			return result;
		}

		public bool AcceptRule(Expert.Rule newRule, int pos = -1)
		{
			if (pos == -1)
			{
				using (Dictionary<int, Expert.Rule>.KeyCollection.Enumerator enumerator = this.Rules.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int current = enumerator.Current;
						if (pos < current)
						{
							pos = current;
						}
					}
				}
				pos++;
			}
			this.rules[pos] = newRule;
			newRule.PosAtRuleSet = pos;
			Expert.ExpertEvents.ExpertChanged();
			Expert.Cor.makedirty();
			return true;
		}

		public Expert.Rule LoadRule(int pos)
		{
			Expert.Rule result;
			if (!this.rules.ContainsKey(pos) || pos < 0 || pos >= this.rules.Keys.Count)
			{
				result = null;
			}
			else
			{
				result = this.rules[pos];
			}
			return result;
		}

		public bool removeRule(int pos)
		{
			bool result;
			if (!this.rules.ContainsKey(pos))
			{
				result = false;
			}
			else
			{
				Expert.Rule rule = this.rules[pos];
				for (int i = pos; i < this.rules.Count - 1; i++)
				{
					this.rules[i] = this.rules[i + 1];
				}
				this.rules.Remove(this.rules.Count - 1);
				Expert.Cor.makedirty();
				Expert.ExpertEvents.ExpertChanged();
				result = true;
			}
			return result;
		}

		public bool swapRulesAt(int i, int j)
		{
			bool result;
			if (!this.rules.ContainsKey(i) || !this.rules.ContainsKey(j))
			{
				result = false;
			}
			else
			{
				Expert.Rule rule = this.rules[i];
				Expert.Rule rule2 = this.rules[j];
				if (rule == null || rule2 == null || rule == rule2)
				{
					result = false;
				}
				else
				{
					this.rules[i] = rule2;
					this.rules[j] = rule;
					Expert.Cor.makedirty();
					result = true;
				}
			}
			return result;
		}

		public bool setNewDomainPos(int opos, int npos)
		{
			bool result;
			if (!this.rules.ContainsKey(opos) || !this.rules.ContainsKey(npos))
			{
				result = false;
			}
			else if (opos == npos)
			{
				result = false;
			}
			else
			{
				int num = (opos < npos) ? 1 : -1;
				Expert.Rule rule = this.rules[opos];
				for (int num2 = opos; num2 != npos; num2 += num)
				{
					this.rules[num2] = this.rules[num2 + num];
				}
				this.rules[npos] = rule;
				Expert.ExpertEvents.ExpertChanged();
				this.makedirty();
				result = true;
			}
			return result;
		}
	}
}
