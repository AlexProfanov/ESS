using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyEs
{
	[Serializable]
	public class KnowledgeBase
	{
		private DomainList DomainsCurrent;

		private DomainList DomainsSaved;

		private VariablesList VarsCurrent;

		private VariablesList VarsSaved;

		private RulesList RulesCurrent;

		private RulesList RulesSaved;

		private bool _CascadeRemoving;

		private int Goal;

		private Domain _SavedDomain;

		private int SavedDomainIndex;

		private Variable _SavedVar;

		private int SavedVarIndex;

		private Rule _SavedRule;

		private int SavedRuleIndex;

		public DomainList Domains
		{
			get
			{
				return this.DomainsCurrent;
			}
		}

		public VariablesList Vars
		{
			get
			{
				return this.VarsCurrent;
			}
		}

		public RulesList Rules
		{
			get
			{
				return this.RulesCurrent;
			}
		}

		public int GoalVariable
		{
			get
			{
				return this.Goal;
			}
			set
			{
				this.Goal = value;
			}
		}

		public bool CascadeRemoving
		{
			get
			{
				return this._CascadeRemoving;
			}
			set
			{
				this._CascadeRemoving = value;
				this.Domains.Cascade = value;
				this.Vars.Cascade = value;
			}
		}

		public Domain SavedDomain
		{
			get
			{
				while (this.Domains.GetDomainsNames().Contains(this._SavedDomain.Name + this.SavedDomainIndex.ToString()))
				{
					this.SavedDomainIndex++;
				}
				Domain domain = new Domain();
				domain = (Domain)this._SavedDomain.Clone();
				Domain expr_58 = domain;
				expr_58.Name += this.SavedDomainIndex.ToString();
				return domain;
			}
			set
			{
				this._SavedDomain = new Domain();
				this._SavedDomain = value;
				this.SavedDomainIndex = 1;
			}
		}

		public Variable SavedVar
		{
			get
			{
				while (this.Vars.GetVarsNames().Contains(this._SavedVar.Name + this.SavedVarIndex.ToString()))
				{
					this.SavedVarIndex++;
				}
				Variable variable = new Variable();
				variable = (Variable)this._SavedVar.Clone();
				Variable expr_58 = variable;
				expr_58.Name += this.SavedVarIndex.ToString();
				return variable;
			}
			set
			{
				this._SavedVar = new Variable();
				this._SavedVar = value;
				this.SavedVarIndex = 1;
			}
		}

		public Rule SavedRule
		{
			get
			{
				while (this.Rules.GetRulesNames().Contains(this._SavedRule.Name + this.SavedRuleIndex.ToString()))
				{
					this.SavedRuleIndex++;
				}
				Rule rule = new Rule();
				rule = (Rule)this._SavedRule.Clone();
				Rule expr_58 = rule;
				expr_58.Name += this.SavedRuleIndex.ToString();
				return rule;
			}
			set
			{
				this._SavedRule = new Rule();
				this._SavedRule = value;
				this.SavedRuleIndex = 1;
			}
		}

		public KnowledgeBase()
		{
			this.VarsCurrent = new VariablesList();
			this.VarsSaved = new VariablesList();
			this.DomainsCurrent = new DomainList(this.Vars);
			this.DomainsSaved = new DomainList(this.Vars);
			this.VarsCurrent.SetDomains(this.Domains);
			this.VarsSaved.SetDomains(this.Domains);
			this.RulesCurrent = new RulesList();
			this.RulesSaved = new RulesList();
			this.RulesCurrent.SetDomains(this.Domains);
			this.RulesCurrent.SetVariables(this.Vars);
			this.RulesSaved.SetDomains(this.Domains);
			this.RulesSaved.SetVariables(this.Vars);
			this.VarsCurrent.SetRules(this.Rules);
			this.VarsSaved.SetRules(this.Rules);
			this.CascadeRemoving = true;
			this.Goal = -1;
			this.SavedDomainIndex = (this.SavedVarIndex = (this.SavedRuleIndex = 1));
		}

		public void SetTree(TreeView tree)
		{
			tree.Nodes.Clear();
			tree.Nodes.Add("root", "Saved KB");
			tree.Nodes["root"].Nodes.Add("goal", "Goal");
			tree.Nodes["root"].Nodes.Add("domains", "Domains");
			tree.Nodes["root"].Nodes.Add("variables", "Variables");
			tree.Nodes["root"].Nodes.Add("rules", "Rules");
			tree.ExpandAll();
		}

		public void SaveDomains(TreeView tree)
		{
			this.DomainsSaved.Domains.Clear();
			using (List<KeyValuePair<int, Domain>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Domain>>(this.DomainsCurrent.Domains).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Domain> current = enumerator.Current;
					this.DomainsSaved.Domains.Add(current.Key, (Domain)current.Value.Clone());
				}
			}
			this.DomainsSaved.Cascade = this.DomainsCurrent.Cascade;
			this.DomainsSaved.HasSavedDomain = this.DomainsCurrent.HasSavedDomain;
			this.FullTree("domains", 0, tree);
		}

		public void UndoDomains()
		{
			this.DomainsCurrent.Domains.Clear();
			using (List<KeyValuePair<int, Domain>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Domain>>(this.DomainsSaved.Domains).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Domain> current = enumerator.Current;
					this.DomainsCurrent.Domains.Add(current.Key, (Domain)current.Value.Clone());
				}
			}
			this.DomainsCurrent.Cascade = this.DomainsSaved.Cascade;
			this.DomainsCurrent.HasSavedDomain = this.DomainsSaved.HasSavedDomain;
		}

		public void SaveVars(TreeView tree)
		{
			this.VarsSaved.Vars.Clear();
			using (List<KeyValuePair<int, Variable>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Variable>>(this.VarsCurrent.Vars).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Variable> current = enumerator.Current;
					this.VarsSaved.Vars.Add(current.Key, (Variable)current.Value.Clone());
				}
			}
			this.FullTree("variables", 1, tree);
			this.VarsSaved.Cascade = this.VarsCurrent.Cascade;
			this.VarsSaved.HasSavedVar = this.VarsCurrent.HasSavedVar;
		}

		public void UndoVars()
		{
			this.VarsCurrent.Vars.Clear();
			using (List<KeyValuePair<int, Variable>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Variable>>(this.VarsSaved.Vars).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Variable> current = enumerator.Current;
					this.VarsCurrent.Vars.Add(current.Key, (Variable)current.Value.Clone());
				}
			}
			this.VarsCurrent.Cascade = this.VarsSaved.Cascade;
			this.VarsCurrent.HasSavedVar = this.VarsSaved.HasSavedVar;
		}

		public void SaveRules(TreeView tree)
		{
			this.RulesSaved.Rules.Clear();
			using (List<KeyValuePair<int, Rule>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Rule>>(this.RulesCurrent.Rules).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Rule> current = enumerator.Current;
					this.RulesSaved.Rules.Add(current.Key, (Rule)current.Value.Clone());
				}
			}
			this.FullTree("rules", 2, tree);
			this.RulesSaved.Cascade = this.RulesCurrent.Cascade;
			this.RulesSaved.HasSavedRule = this.RulesCurrent.HasSavedRule;
		}

		public void UndoRules()
		{
			this.RulesCurrent.Rules.Clear();
			using (List<KeyValuePair<int, Rule>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, Rule>>(this.RulesSaved.Rules).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Rule> current = enumerator.Current;
					this.RulesCurrent.Rules.Add(current.Key, (Rule)current.Value.Clone());
				}
			}
			this.RulesCurrent.Cascade = this.RulesSaved.Cascade;
			this.RulesCurrent.HasSavedRule = this.RulesSaved.HasSavedRule;
		}

		public void SaveGoal(TreeView tree)
		{
			if (this.Goal > -1)
			{
				tree.Nodes["root"].Nodes["goal"].Nodes.Clear();
				tree.Nodes["root"].Nodes["goal"].Nodes.Add(this.Vars.GetVarById(this.Goal).Name);
			}
			tree.Nodes["root"].Expand();
			tree.Nodes["root"].Nodes["goal"].Expand();
		}

		private void FullTree(string leaf, int id, TreeView tree)
		{
			List<string> list = new List<string>();
			if (this.Goal > -1)
			{
				tree.Nodes["root"].Nodes["goal"].Nodes.Clear();
				tree.Nodes["root"].Nodes["goal"].Nodes.Add(this.Vars.GetVarById(this.Goal).Name);
			}
			tree.Nodes["root"].Nodes[leaf].Nodes.Clear();
			switch (id)
			{
			case 0:
				list = this.DomainsSaved.GetDomainsNames();
				break;
			case 1:
				list = this.VarsSaved.GetVarsNames();
				break;
			case 2:
				list = this.RulesSaved.GetRulesNames();
				break;
			}
			using (List<string>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					TreeNode treeNode = tree.Nodes["root"].Nodes[leaf].Nodes.Add(current);
					if (leaf != null)
					{
						if (!(leaf == "domains"))
						{
							if (!(leaf == "variables"))
							{
								if (leaf == "rules")
								{
									Rule ruleById = this.RulesCurrent.GetRuleById(this.RulesCurrent.GetIdRuleByName(current));
									treeNode.Nodes.Add("IF");
									using (List<RulePair>.Enumerator enumerator2 = ruleById.IfVars.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											RulePair current2 = enumerator2.Current;
											treeNode.Nodes.Add(string.Concat(new string[]
											{
												"'",
												this.VarsCurrent.GetVarById(current2.Variable).Name,
												"'=='",
												this.DomainsCurrent.GetDomainValueNameById(this.VarsCurrent.GetVarById(current2.Variable).domain, current2.Value),
												"'"
											}));
										}
									}
									treeNode.Nodes.Add("THEN");
									using (List<RulePair>.Enumerator enumerator2 = ruleById.ThenVars.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											RulePair current2 = enumerator2.Current;
											treeNode.Nodes.Add(string.Concat(new string[]
											{
												"'",
												this.VarsCurrent.GetVarById(current2.Variable).Name,
												"'='",
												this.DomainsCurrent.GetDomainValueNameById(this.VarsCurrent.GetVarById(current2.Variable).domain, current2.Value),
												"'"
											}));
										}
									}
									treeNode.Nodes.Add("Объяснение: '" + ruleById.Reason + "'");
								}
							}
							else
							{
								Variable varById = this.VarsCurrent.GetVarById(this.VarsCurrent.GetIdVarByName(current));
								treeNode.Nodes.Add("Тип: '" + varById.type + "'");
								treeNode.Nodes.Add("Домен: '" + this.DomainsCurrent.GetNameById(varById.domain) + "'");
							}
						}
						else
						{
							Dictionary<int, string> domainValues = this.DomainsCurrent.GetDomainValues(this.DomainsCurrent.GetIdByName(current));
							using (List<string>.Enumerator enumerator3 = Enumerable.ToList<string>(domainValues.Values).GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									string current3 = enumerator3.Current;
									treeNode.Nodes.Add("'" + current3 + "'");
								}
							}
						}
					}
				}
			}
			tree.Nodes["root"].Expand();
			tree.Nodes["root"].Nodes[leaf].Expand();
		}

		public void InitTree(TreeView tree)
		{
			List<string> list = new List<string>();
			tree.Nodes["root"].Nodes["domains"].Nodes.Clear();
			tree.Nodes["root"].Nodes["variables"].Nodes.Clear();
			tree.Nodes["root"].Nodes["rules"].Nodes.Clear();
			tree.Nodes["root"].Nodes["goal"].Nodes.Clear();
			if (this.Goal > -1)
			{
				tree.Nodes["root"].Nodes["goal"].Nodes.Add(this.Vars.GetVarById(this.Goal).Name);
			}
			this.FullTree("domains", 0, tree);
			this.FullTree("variables", 1, tree);
			this.FullTree("rules", 2, tree);
		}
	}
}
