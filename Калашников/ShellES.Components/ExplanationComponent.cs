using ShellES.Entities;
using System;
using System.Windows.Forms;

namespace ShellES.Components
{
	public class ExplanationComponent : ComponentPrototype
	{
		private delegate int dlgAddNodeTo(TreeNode Place);

		private delegate void dlgInsertNode(int place, TreeNode What);

		private delegate void dlgDeleteNode(TreeNode What);

		private delegate void dlgSimple();

		private TreeView explTreeFull;

		private TreeView explTreeVars;

		private TreeView explTreeRules;

		private RichTextBox explLog;

		private TreeNode currentNodeFull;

		private TreeNode currentNodeVar;

		private TreeNode currentNodeRule;

		public ExplanationComponent(ExpertSystemShell ess) : base(ess)
		{
		}

		public void BindingControls(TreeView TreeFull, TreeView TreeVars, TreeView TreeRules, RichTextBox Rich)
		{
			this.explTreeFull = TreeFull;
			this.explTreeRules = TreeRules;
			this.explTreeVars = TreeVars;
			this.explLog = Rich;
		}

		public void ClearResults()
		{
			StaticHelper.RunMethodAsDelegate(this.explTreeVars, new ExplanationComponent.dlgSimple(this.explTreeVars.Nodes.Clear), new object[0]);
			StaticHelper.RunMethodAsDelegate(this.explTreeFull, new ExplanationComponent.dlgSimple(this.explTreeFull.Nodes.Clear), new object[0]);
			StaticHelper.RunMethodAsDelegate(this.explTreeRules, new ExplanationComponent.dlgSimple(this.explTreeRules.Nodes.Clear), new object[0]);
		}

		public void ExpandConsultation(bool Collapse, bool ExpandLast)
		{
			if (Collapse)
			{
				this.CollapseTree(this.explTreeFull);
				this.CollapseTree(this.explTreeRules);
				this.CollapseTree(this.explTreeVars);
			}
			this.ExpandTree(this.explTreeFull, ExpandLast);
			this.ExpandTree(this.explTreeRules, ExpandLast);
			this.ExpandTree(this.explTreeVars, ExpandLast);
		}

		private void CollapseTree(TreeView Tree)
		{
			if (Tree.InvokeRequired)
			{
				Tree.Invoke(new ExplanationComponent.dlgSimple(Tree.CollapseAll));
				return;
			}
			Tree.CollapseAll();
		}

		private void ExpandTree(TreeView Tree, bool ExpandAll)
		{
			if (Tree.Nodes.Count > 0)
			{
				if (Tree.InvokeRequired)
				{
					if (ExpandAll)
					{
						Tree.Invoke(new ExplanationComponent.dlgSimple(Tree.Nodes[0].ExpandAll));
						return;
					}
					Tree.Invoke(new ExplanationComponent.dlgSimple(Tree.Nodes[0].Expand));
					return;
				}
				else
				{
					if (ExpandAll)
					{
						Tree.Nodes[0].ExpandAll();
						return;
					}
					Tree.Nodes[0].Expand();
				}
			}
		}

		private void AddNode(TreeView Tree, TreeNode What, int place)
		{
			if (Tree.InvokeRequired)
			{
				Tree.Invoke(new ExplanationComponent.dlgInsertNode(Tree.Nodes.Insert), new object[]
				{
					place,
					What
				});
				return;
			}
			Tree.Nodes.Insert(place, What);
		}

		private void AddNodeTo(TreeView Tree, TreeNode What, TreeNode Place)
		{
			if (Tree.InvokeRequired)
			{
				Tree.Invoke(new ExplanationComponent.dlgAddNodeTo(Place.Nodes.Add), new object[]
				{
					What
				});
				return;
			}
			Place.Nodes.Add(What);
		}

		private void AddOrReplace(TreeView Tree, TreeNode What, TreeNode Place)
		{
			string text = What.Text;
			TreeNode treeNode = null;
			foreach (TreeNode treeNode2 in Place.Nodes)
			{
				if (treeNode2.Text == text)
				{
					treeNode = treeNode2;
					break;
				}
			}
			if (treeNode != null)
			{
				this.DeleteNode(Tree, treeNode);
			}
			this.AddNodeTo(Tree, What, Place);
		}

		private void DeleteNode(TreeView Tree, TreeNode What)
		{
			if (Tree.InvokeRequired)
			{
				Tree.Invoke(new ExplanationComponent.dlgDeleteNode(Tree.Nodes.Remove), new object[]
				{
					What
				});
				return;
			}
			Tree.Nodes.Remove(What);
		}

		public void ProcessEvent(eTypeEvent eType, string AdditionalInfo)
		{
			switch (eType)
			{
			case eTypeEvent.START_CONSULTATION:
				this.currentNodeFull = new TreeNode("Консультация от " + AdditionalInfo + ":");
				this.AddNode(this.explTreeFull, this.currentNodeFull, 0);
				this.currentNodeRule = new TreeNode("Консультация от " + AdditionalInfo + ":");
				this.AddNode(this.explTreeRules, this.currentNodeRule, 0);
				this.currentNodeVar = new TreeNode("Консультация от " + AdditionalInfo + ":");
				this.AddNode(this.explTreeVars, this.currentNodeVar, 0);
				return;
			case eTypeEvent.GET_SETTINGS:
			{
				TreeNode treeNode = new TreeNode("Установки консультации:");
				treeNode.Nodes.Add(new TreeNode("Цель консультации: " + this.ESshell.Settings.Goal.Name.ToString()));
				treeNode.Nodes.Add(new TreeNode("Направление МЛВ: " + this.ESshell.Settings.MLV_Direction.ToString()));
				treeNode.Nodes.Add(new TreeNode("Граница CF.Unknkown = " + this.ESshell.Settings.EDGE_UNKNOWN.ToString()));
				treeNode.Nodes.Add(new TreeNode("Стратегия выборки правила: " + this.ESshell.Settings.SelStrategy.ToString()));
				TreeNode treeNode2 = treeNode.Nodes.Add("Стратегии пересчета");
				treeNode2.Nodes.Add("CF ф. в посылке: " + this.ESshell.Settings.CFFact.ToString());
				treeNode2.Nodes.Add("CF посылки: " + this.ESshell.Settings.CFPrem.ToString());
				treeNode2.Nodes.Add("CF заключения: " + this.ESshell.Settings.CFConcl.ToString());
				treeNode2.Nodes.Add("CF ф. в закл.: " + this.ESshell.Settings.CFFactConcl.ToString());
				treeNode2.Nodes.Add("CFVA: " + this.ESshell.Settings.CFVA.ToString());
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			}
			case eTypeEvent.END_CONSULTATION_BREAK:
			{
				TreeNode treeNode = new TreeNode("Консультация прервана!");
				this.AddNode(this.explTreeFull, treeNode, 1);
				this.currentNodeFull = treeNode;
				return;
			}
			case eTypeEvent.END_CONSULTATION_GOOD:
			{
				TreeNode treeNode = new TreeNode("Консультация завершилась успешно!");
				this.AddNode(this.explTreeFull, treeNode, 1);
				this.currentNodeFull = treeNode;
				return;
			}
			case eTypeEvent.END_CONSULTATION_BAD:
			{
				TreeNode treeNode = new TreeNode("Консультация завершилась неудачно! Обратитесь к другой ЭС");
				this.AddNode(this.explTreeFull, treeNode, 1);
				this.currentNodeFull = treeNode;
				return;
			}
			default:
				return;
			}
		}

		public void ProcessEvent(eTypeEvent eType, ESVars variable, string AdditionalInfo)
		{
			TreeNode treeNode;
			switch (eType)
			{
			case eTypeEvent.SET_GOAL:
				treeNode = new TreeNode("Установлена новая цель: <" + variable.Name + ">");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				treeNode = new TreeNode("Попытка доказательства цели:");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				this.currentNodeFull = treeNode;
				return;
			case eTypeEvent.GOAL_HAVE_VALUE:
				treeNode = new TreeNode("Переменная уже доказана!");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				break;
			case eTypeEvent.GOAL_ASK:
				treeNode = new TreeNode("Цель - " + ESVars.Str_VarType[(int)variable.VarType].ToLower() + " переменная... запросим!");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			case eTypeEvent.GOAL_MEET:
				treeNode = new TreeNode("Встретилась доказываемая цель со значением(ями) " + variable.GetResults());
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			case eTypeEvent.ERROR_GOAL_ALREADY_PROVING:
				treeNode = new TreeNode("Цель уже доказывается! Во избежании циклической ссылки считаем, что цель не доказана!");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			case eTypeEvent.GET_RULES_SET:
				treeNode = new TreeNode(string.Concat(new string[]
				{
					"Цель - ",
					ESVars.Str_VarType[(int)variable.VarType].ToLower(),
					" переменная... получили конфликтный набор правил ",
					AdditionalInfo,
					"!"
				}));
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			case eTypeEvent.GET_RULES_SET2:
				treeNode = new TreeNode("Получаем текущий конфликтный набор правил, готовых к исполнению и еще не сработавших " + AdditionalInfo + "!");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			case eTypeEvent.HAVE_NO_ANY_RULE:
				treeNode = new TreeNode("В конфликтном наборе нет ни одного правила!");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			case eTypeEvent.SOLVED_GOAL:
				break;
			case eTypeEvent.GOAL_HAS_VALUE:
			{
				string text = string.Format("<{0}>", variable.Name);
				treeNode = new TreeNode(text);
				treeNode.Nodes.Add(string.Format("Значения: {0}", variable.GetResults()));
				this.AddOrReplace(this.explTreeVars, treeNode, this.currentNodeVar);
				return;
			}
			case eTypeEvent.GOAL_ASK_AFTER_TRYING_PROVE:
				treeNode = new TreeNode("С помощью правил переменную <" + variable.Name + "> установить не удалось. Попробуем запросить: ");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			case eTypeEvent.CANNOT_SOLVE_GOAL:
				treeNode = new TreeNode("Цель доказать не удалось.");
				this.currentNodeFull = this.currentNodeFull.Parent;
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			default:
				return;
			}
			treeNode = new TreeNode("Цель доказана! Значение(я): " + variable.GetResults());
			this.currentNodeFull = this.currentNodeFull.Parent;
			this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
		}

		public void ProcessEvent(eTypeEvent eType, ESRules rule, string AdditionalInfo)
		{
			switch (eType)
			{
			case eTypeEvent.CHOOSE_RULE:
			{
				TreeNode treeNode = new TreeNode("Выбрали правило <" + rule.Name + ">: " + rule.Presentation);
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			}
			case eTypeEvent.TRY_PROVE_RULE:
			{
				TreeNode treeNode = new TreeNode("Попытка доказательства правила:");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				this.currentNodeFull = treeNode;
				return;
			}
			case eTypeEvent.CANNOT_PROVE_RULE:
			{
				TreeNode treeNode = new TreeNode("Правило не доказано!");
				this.currentNodeFull = this.currentNodeFull.Parent;
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			}
			case eTypeEvent.DEAD_RULE:
			{
				TreeNode treeNode = new TreeNode("Правило помечено как несработавшее - считаем его таковым");
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				return;
			}
			case eTypeEvent.PROVED_RULE:
			{
				TreeNode treeNode = new TreeNode("Правило доказано! Вычисляем результирующий CF к заключению = " + AdditionalInfo + " и получаем результаты:");
				this.currentNodeFull = this.currentNodeFull.Parent;
				this.AddNodeTo(this.explTreeFull, treeNode, this.currentNodeFull);
				this.currentNodeFull = treeNode;
				treeNode = new TreeNode(string.Format("Правило <{0}>", rule.Name));
				treeNode.Nodes.Add(string.Format("Текст: {0}", rule.Presentation));
				TreeNode treeNode2 = treeNode.Nodes.Add("IF (Посылки):");
				foreach (ESFact current in rule.Premises)
				{
					treeNode2.Nodes.Add(current.GetPresentation(true));
				}
				treeNode2 = treeNode.Nodes.Add("THEN (Заключения):");
				foreach (ESFact current2 in rule.Consequences)
				{
					treeNode2.Nodes.Add(current2.GetPresentation(true));
				}
				treeNode2 = treeNode.Nodes.Add("REASON (Объяснение):");
				treeNode2.Nodes.Add(rule.Reason);
				this.AddNodeTo(this.explTreeRules, treeNode, this.currentNodeRule);
				return;
			}
			case eTypeEvent.PROVED_RULE_THATS_ALL:
				this.currentNodeFull = this.currentNodeFull.Parent;
				return;
			default:
				return;
			}
		}

		public void ProcessEvent(eTypeEvent eType, ESFact fact, string AdditionalInfo)
		{
			switch (eType)
			{
			case eTypeEvent.TRY_PROVE_PREM:
			{
				TreeNode what = new TreeNode("Доказательство посыли #" + AdditionalInfo + ": " + fact.GetPresentation(true));
				this.AddNodeTo(this.explTreeFull, what, this.currentNodeFull);
				this.currentNodeFull = what;
				return;
			}
			case eTypeEvent.PROVED_PREM:
			{
				TreeNode what = new TreeNode("Посылка доказана!");
				if (AdditionalInfo == "")
				{
					this.currentNodeFull = this.currentNodeFull.Parent;
				}
				this.AddNodeTo(this.explTreeFull, what, this.currentNodeFull);
				return;
			}
			case eTypeEvent.PREM_DEAD:
			{
				TreeNode what = new TreeNode("Этот факт - уже отвергнутая гипотеза!");
				this.AddNodeTo(this.explTreeFull, what, this.currentNodeFull);
				this.currentNodeFull = this.currentNodeFull.Parent;
				what = new TreeNode("Посылка не доказана - выведено ранее");
				this.AddNodeTo(this.explTreeFull, what, this.currentNodeFull);
				return;
			}
			case eTypeEvent.CANNOT_PROVE_PREM_BECAUSE_OF_VAR:
			{
				TreeNode what = new TreeNode("Посылка не доказана - не определено значение переменной <" + fact.Variable.Name + ">");
				this.currentNodeFull = this.currentNodeFull.Parent;
				this.AddNodeTo(this.explTreeFull, what, this.currentNodeFull);
				return;
			}
			case eTypeEvent.CANNOT_PROVE_PREM_BECAUSE_OF_CF:
			{
				TreeNode what = new TreeNode(string.Concat(new string[]
				{
					"Посылка не доказана - переменная <",
					fact.Variable.Name,
					"> не обладает значением <",
					fact.Value.Value,
					"> или его cf < CF_UNKNOWN (",
					this.ESshell.Settings.EDGE_UNKNOWN.ToString(),
					")"
				}));
				if (AdditionalInfo == "")
				{
					this.currentNodeFull = this.currentNodeFull.Parent;
				}
				this.AddNodeTo(this.explTreeFull, what, this.currentNodeFull);
				return;
			}
			case eTypeEvent.PREM_ASK:
			{
				TreeNode what = new TreeNode(string.Concat(new string[]
				{
					"Запрашиваем переменную <",
					fact.Variable.Name,
					"> в посылке ",
					fact.GetPresentation(true),
					"."
				}));
				this.AddNodeTo(this.explTreeFull, what, this.currentNodeFull);
				this.currentNodeFull = what;
				return;
			}
			case eTypeEvent.PREM_ASK_RESULT:
			{
				TreeNode what = new TreeNode("Переменная получила значение " + fact.Variable.GetResults());
				this.AddNodeTo(this.explTreeFull, what, this.currentNodeFull);
				this.currentNodeFull = this.currentNodeFull.Parent;
				return;
			}
			case eTypeEvent.SOLVED_CONCLUSION_FACT:
			{
				TreeNode what = new TreeNode("Установлен факт " + fact.GetPresentation(false) + ": до объединения " + AdditionalInfo);
				this.AddNodeTo(this.explTreeFull, what, this.currentNodeFull);
				return;
			}
			default:
				return;
			}
		}

		public void ClearEvents()
		{
		}
	}
}
