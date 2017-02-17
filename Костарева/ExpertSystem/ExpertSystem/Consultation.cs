using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExpertSystem
{
	internal class Consultation
	{
		private Expert core = null;

		private Dictionary<string, string> values = new Dictionary<string, string>();

		private TreeView tvFULL = new TreeView();

		private TreeView tvACC = new TreeView();

		private mainForm mf;

		private bool abort = false;

		public static void Execute(Expert _core, mainForm _mf = null)
		{
			Consultation consultation = new Consultation(_core, _mf);
			consultation.Process();
		}

		private Consultation(Expert _core, mainForm _mf = null)
		{
			this.core = _core;
			this.mf = _mf;
		}

		private void Process()
		{
			string text = varSelectForm.exec(this.core);
			if (!(text == ""))
			{
				Expert.Variable variable = this.core.varByName(text);
				if (variable != null)
				{
					TreeNode treeNode = new TreeNode("root");
					TreeNode treeNode2 = new TreeNode("root");
					if (this.grph(variable, ref treeNode, ref treeNode2))
					{
						MessageBox.Show(text + ": " + this.values[variable.Name]);
					}
					this.tvFULL.Nodes.Add(treeNode);
					this.tvACC.Nodes.Add(treeNode2);
					if (!this.abort)
					{
						consForm.Execute(this.core, this.tvFULL, this.tvACC, text, this.values, this.mf);
					}
				}
			}
		}

		private bool grph(Expert.Variable v, ref TreeNode parentF, ref TreeNode parentAc)
		{
			bool result;
			if (this.abort)
			{
				result = false;
			}
			else if (this.values.ContainsKey(v.Name))
			{
				result = true;
			}
			else if (v.vt == Expert.VarType.vtZapr)
			{
				if (this.abort)
				{
					result = false;
				}
				else
				{
					string text = askForm.Execute(v);
					if (text == "")
					{
						this.abort = true;
						result = false;
					}
					else
					{
						TreeNode treeNode = new TreeNode(v.Name + ": " + text);
						treeNode.BackColor = Color.Yellow;
						parentF.Nodes.Add(treeNode);
						this.values.Add(v.Name, text);
						result = true;
					}
				}
			}
			else
			{
				using (Dictionary<int, Expert.Rule>.Enumerator enumerator = this.core.Rules.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Expert.Rule> current = enumerator.Current;
						if (!(current.Value.RuleResult == null))
						{
							if (current.Value.RuleResult.Var == v)
							{
								bool flag = true;
								TreeNode treeNode2 = new TreeNode(string.Concat(new string[]
								{
									current.Value.Reason,
									" Rule: ",
									current.Value.Name,
									" IF ",
									current.Value.ToString(),
									" THEN ",
									current.Value.RuleResult.ToString()
								}));
								TreeNode treeNode3 = new TreeNode(string.Concat(new string[]
								{
									current.Value.Reason,
									" Rule: ",
									current.Value.Name,
									" IF ",
									current.Value.ToString(),
									" THEN ",
									current.Value.RuleResult.ToString()
								}));
								parentF.Nodes.Add(treeNode2);
								using (Dictionary<int, Expert.Fact>.Enumerator enumerator2 = current.Value.Facts.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										KeyValuePair<int, Expert.Fact> current2 = enumerator2.Current;
										Expert.Fact value = current2.Value;
										if (!this.grph(value.Var, ref treeNode2, ref treeNode3))
										{
											flag = false;
											treeNode2.BackColor = Color.Red;
											break;
										}
										if (this.values[value.Var.Name] != value.DomStrValue())
										{
											treeNode2.BackColor = Color.Red;
											flag = false;
											break;
										}
									}
								}
								if (flag || treeNode3.Nodes.Count > 0)
								{
									parentAc.Nodes.Add(treeNode3);
								}
								if (flag)
								{
									treeNode2.BackColor = Color.Green;
									this.values.Add(v.Name, current.Value.RuleResDomValue());
									result = true;
									return result;
								}
							}
						}
					}
				}
				if (v.vt == Expert.VarType.vtVivZapr)
				{
					if (this.abort)
					{
						result = false;
					}
					else
					{
						string text = askForm.Execute(v);
						if (text == "")
						{
							this.abort = true;
							result = false;
						}
						else
						{
							TreeNode treeNode = new TreeNode(v.Name + ": " + text);
							treeNode.BackColor = Color.Yellow;
							parentF.Nodes.Add(treeNode);
							this.values.Add(v.Name, text);
							result = true;
						}
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}
	}
}
