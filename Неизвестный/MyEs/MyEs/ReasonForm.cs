using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyEs
{
	public class ReasonForm : Form
	{
		private KnowledgeBase KB;

		private IContainer components = null;

		private TabPage TabReasonAllRules;

		private TabPage TabReasonChain;

		private TabPage TabReasonVars;

		private TreeView TreeRules;

		private TreeView TreeChain;

		private DataGridView GridReasonVars;

		public TabControl TabReason;

		private Button button2;

		private ToolTip toolTip1;

		private Button button1;

		private Button button3;

		private Button button4;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column2;

		public ReasonForm(KnowledgeBase _kb)
		{
			this.InitializeComponent();
			this.KB = _kb;
		}

		private void ReasonForm_Load(object sender, EventArgs e)
		{
			this.TreeRules.ClientSizeChanged += new EventHandler(this.TreeRules_ClientSizeChanged);
			this.TreeChain.ClientSizeChanged += new EventHandler(this.TreeChain_ClientSizeChanged);
			this.GridReasonVars.Rows.Clear();
			using (List<KeyValuePair<int, int>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, int>>(WorkingMemory.KnownVars).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, int> current = enumerator.Current;
					int num = this.GridReasonVars.Rows.Add();
					this.GridReasonVars[0, num].Value = this.KB.Vars.GetVarById(current.Key).Reason;
					this.GridReasonVars[1, num].Value = this.KB.Vars.GetVarById(current.Key).type;
					this.GridReasonVars[2, num].Value = this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current.Key).domain, current.Value);
				}
			}
			using (List<int>.Enumerator enumerator2 = Enumerable.ToList<int>(this.KB.Vars.Vars.Keys).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int current2 = enumerator2.Current;
					if (!WorkingMemory.KnownVars.ContainsKey(current2))
					{
						int num = this.GridReasonVars.Rows.Add();
						this.GridReasonVars[0, num].Value = this.KB.Vars.GetVarById(current2).Name;
						this.GridReasonVars[1, num].Value = this.KB.Vars.GetVarById(current2).type;
						this.GridReasonVars[2, num].Value = "not designated";
					}
				}
			}
			using (List<int>.Enumerator enumerator2 = WorkingMemory.Chain.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int current3 = enumerator2.Current;
					Rule ruleById = this.KB.Rules.GetRuleById(current3);
					TreeNode treeNode = this.TreeChain.Nodes["root"].Nodes.Add(ruleById.Name);
					treeNode.Nodes.Add("IF");
					using (List<RulePair>.Enumerator enumerator3 = ruleById.IfVars.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							RulePair current4 = enumerator3.Current;
							treeNode.Nodes.Add(string.Concat(new string[]
							{
								"'",
								this.KB.Vars.GetVarById(current4.Variable).Name,
								"'=='",
								this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current4.Variable).domain, current4.Value),
								"'"
							}));
						}
					}
					treeNode.Nodes.Add("THEN");
					using (List<RulePair>.Enumerator enumerator3 = ruleById.ThenVars.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							RulePair current4 = enumerator3.Current;
							treeNode.Nodes.Add(string.Concat(new string[]
							{
								"'",
								this.KB.Vars.GetVarById(current4.Variable).Name,
								"'='",
								this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current4.Variable).domain, current4.Value),
								"'"
							}));
						}
					}
					treeNode.Nodes.Add("Reasoning: '" + ruleById.Reason + "'");
				}
			}
			if (WorkingMemory.Chain.Count == 0)
			{
				this.TreeChain.Nodes["root"].Nodes.Add("List is empty");
			}
			this.TreeChain.Nodes["root"].Expand();
			int num2 = 0;
			using (List<int>.Enumerator enumerator2 = WorkingMemory.AllRules.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int current3 = enumerator2.Current;
					List<RulePair> list = new List<RulePair>();
					Rule ruleById = this.KB.Rules.GetRuleById(current3);
					TreeNode treeNode = this.TreeRules.Nodes["root"].Nodes.Add(ruleById.Name);
					TreeNode treeNode2 = new TreeNode();
					treeNode2 = treeNode;
					if (WorkingMemory.Chain.Contains(current3))
					{
						treeNode.ForeColor = Color.Green;
					}
					else
					{
						treeNode.ForeColor = Color.Red;
						treeNode = treeNode.Nodes.Add("Rule");
					}
					treeNode.Nodes.Add("IF");
					using (List<RulePair>.Enumerator enumerator3 = ruleById.IfVars.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							RulePair current4 = enumerator3.Current;
							TreeNode treeNode3 = treeNode.Nodes.Add(string.Concat(new string[]
							{
								"'",
								this.KB.Vars.GetVarById(current4.Variable).Name,
								"'=='",
								this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current4.Variable).domain, current4.Value),
								"'"
							}));
							if (WorkingMemory.ForAll.Count > ruleById.IfVars.IndexOf(current4) && WorkingMemory.ForAll[num2][ruleById.IfVars.IndexOf(current4)].val != current4.Value)
							{
								treeNode3.ForeColor = Color.Red;
							}
						}
					}
					treeNode.Nodes.Add("THEN");
					using (List<RulePair>.Enumerator enumerator3 = ruleById.ThenVars.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							RulePair current4 = enumerator3.Current;
							treeNode.Nodes.Add(string.Concat(new string[]
							{
								"'",
								this.KB.Vars.GetVarById(current4.Variable).Name,
								"'='",
								this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current4.Variable).domain, current4.Value),
								"'"
							}));
						}
					}
					treeNode.Nodes.Add("Reasoning: '" + ruleById.Reason + "'");
					if (treeNode2.ForeColor == Color.Red)
					{
						TreeNode treeNode4 = treeNode2.Nodes.Add("Hypothesis value");
						using (List<WorkingMemory.pair>.Enumerator enumerator4 = WorkingMemory.ForAll[num2].GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								WorkingMemory.pair current5 = enumerator4.Current;
								if (current5.val > -1)
								{
									treeNode4.Nodes.Add(string.Concat(new string[]
									{
										"'",
										this.KB.Vars.GetVarById(current5.var).Name,
										"'='",
										this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current5.var).domain, current5.val),
										"'"
									}));
								}
								else
								{
									treeNode4.Nodes.Add("'" + this.KB.Vars.GetVarById(current5.var).Name + "'=not designated'");
								}
							}
						}
						treeNode4.Expand();
						treeNode.Expand();
					}
					num2++;
				}
			}
			if (WorkingMemory.AllRules.Count == 0)
			{
				this.TreeRules.Nodes["root"].Nodes.Add("List is empty");
			}
			this.TreeRules.Nodes["root"].Expand();
		}

		private void TreeChain_ClientSizeChanged(object sender, EventArgs e)
		{
			this.button3.Location = new Point(this.TreeChain.ClientSize.Width - 40, this.button3.Location.Y);
			this.button4.Location = new Point(this.TreeChain.ClientSize.Width - 40, this.button4.Location.Y);
		}

		private void TreeRules_ClientSizeChanged(object sender, EventArgs e)
		{
			this.button1.Location = new Point(this.TreeRules.ClientSize.Width - 40, this.button1.Location.Y);
			this.button2.Location = new Point(this.TreeRules.ClientSize.Width - 40, this.button2.Location.Y);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.TreeRules.ExpandAll();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.TreeRules.Nodes["root"].Nodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TreeNode treeNode = (TreeNode)enumerator.Current;
					treeNode.Collapse();
					if (treeNode.ForeColor == Color.Red)
					{
						treeNode.Nodes[0].Expand();
						treeNode.Nodes[1].Expand();
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			this.TreeChain.ExpandAll();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.TreeChain.Nodes["root"].Nodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TreeNode treeNode = (TreeNode)enumerator.Current;
					treeNode.Collapse();
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			TreeNode treeNode = new TreeNode("Rule's list");
			TreeNode treeNode2 = new TreeNode("Used rules");
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.TabReason = new TabControl();
			this.TabReasonAllRules = new TabPage();
			this.button2 = new Button();
			this.button1 = new Button();
			this.TreeRules = new TreeView();
			this.TabReasonChain = new TabPage();
			this.button3 = new Button();
			this.button4 = new Button();
			this.TreeChain = new TreeView();
			this.TabReasonVars = new TabPage();
			this.GridReasonVars = new DataGridView();
			this.toolTip1 = new ToolTip(this.components);
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.TabReason.SuspendLayout();
			this.TabReasonAllRules.SuspendLayout();
			this.TabReasonChain.SuspendLayout();
			this.TabReasonVars.SuspendLayout();
			this.GridReasonVars.BeginInit();
			base.SuspendLayout();
			this.TabReason.Controls.Add(this.TabReasonAllRules);
			this.TabReason.Controls.Add(this.TabReasonChain);
			this.TabReason.Controls.Add(this.TabReasonVars);
			this.TabReason.Dock = 5;
			this.TabReason.Location = new Point(0, 0);
			this.TabReason.Name = "TabReason";
			this.TabReason.SelectedIndex = 0;
			this.TabReason.Size = new Size(464, 362);
			this.TabReason.TabIndex = 0;
			this.TabReasonAllRules.Controls.Add(this.button2);
			this.TabReasonAllRules.Controls.Add(this.button1);
			this.TabReasonAllRules.Controls.Add(this.TreeRules);
			this.TabReasonAllRules.Location = new Point(4, 22);
			this.TabReasonAllRules.Name = "TabReasonAllRules";
			this.TabReasonAllRules.Padding = new Padding(3);
			this.TabReasonAllRules.Size = new Size(456, 336);
			this.TabReasonAllRules.TabIndex = 0;
			this.TabReasonAllRules.Text = "Rules";
			this.TabReasonAllRules.UseVisualStyleBackColor = true;
			this.button2.Anchor = 9;
			this.button2.Location = new Point(408, 35);
			this.button2.Name = "button2";
			this.button2.Size = new Size(40, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "-";
			this.toolTip1.SetToolTip(this.button2, "Свернуть дерево");
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.button1.Anchor = 9;
			this.button1.Location = new Point(408, 6);
			this.button1.Name = "button1";
			this.button1.Size = new Size(40, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "+";
			this.toolTip1.SetToolTip(this.button1, "Развернуть дерево");
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.TreeRules.Dock = 5;
			this.TreeRules.Location = new Point(3, 3);
			this.TreeRules.Name = "TreeRules";
			treeNode.Name = "root";
			treeNode.Text = "Rule's list";
			this.TreeRules.Nodes.AddRange(new TreeNode[]
			{
				treeNode
			});
			this.TreeRules.Size = new Size(450, 330);
			this.TreeRules.TabIndex = 0;
			this.TabReasonChain.Controls.Add(this.button3);
			this.TabReasonChain.Controls.Add(this.button4);
			this.TabReasonChain.Controls.Add(this.TreeChain);
			this.TabReasonChain.Location = new Point(4, 22);
			this.TabReasonChain.Name = "TabReasonChain";
			this.TabReasonChain.Padding = new Padding(3);
			this.TabReasonChain.Size = new Size(456, 336);
			this.TabReasonChain.TabIndex = 1;
			this.TabReasonChain.Text = "Used rules";
			this.TabReasonChain.UseVisualStyleBackColor = true;
			this.button3.Anchor = 9;
			this.button3.Location = new Point(406, 35);
			this.button3.Name = "button3";
			this.button3.Size = new Size(40, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "-";
			this.toolTip1.SetToolTip(this.button3, "Свернуть дерево");
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.button4.Anchor = 9;
			this.button4.Location = new Point(406, 6);
			this.button4.Name = "button4";
			this.button4.Size = new Size(40, 23);
			this.button4.TabIndex = 3;
			this.button4.Text = "+";
			this.toolTip1.SetToolTip(this.button4, "Развернуть дерево");
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.TreeChain.Dock = 5;
			this.TreeChain.Location = new Point(3, 3);
			this.TreeChain.Name = "TreeChain";
			treeNode2.Name = "root";
			treeNode2.Text = "Used rules";
			this.TreeChain.Nodes.AddRange(new TreeNode[]
			{
				treeNode2
			});
			this.TreeChain.Size = new Size(450, 330);
			this.TreeChain.TabIndex = 0;
			this.TabReasonVars.Controls.Add(this.GridReasonVars);
			this.TabReasonVars.Location = new Point(4, 22);
			this.TabReasonVars.Name = "TabReasonVars";
			this.TabReasonVars.Size = new Size(456, 336);
			this.TabReasonVars.TabIndex = 2;
			this.TabReasonVars.Text = "Variables";
			this.TabReasonVars.UseVisualStyleBackColor = true;
			this.GridReasonVars.AllowUserToAddRows = false;
			this.GridReasonVars.AllowUserToDeleteRows = false;
			this.GridReasonVars.AllowUserToResizeRows = false;
			this.GridReasonVars.AutoSizeColumnsMode = 6;
			this.GridReasonVars.AutoSizeRowsMode = 7;
			this.GridReasonVars.ColumnHeadersHeightSizeMode = 2;
			this.GridReasonVars.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column1,
				this.Column3,
				this.Column2
			});
			dataGridViewCellStyle.Alignment = 16;
			dataGridViewCellStyle.BackColor = SystemColors.Window;
			dataGridViewCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, 0, 3, 204);
			dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = 1;
			this.GridReasonVars.DefaultCellStyle = dataGridViewCellStyle;
			this.GridReasonVars.Dock = 5;
			this.GridReasonVars.Location = new Point(0, 0);
			this.GridReasonVars.Name = "GridReasonVars";
			this.GridReasonVars.ReadOnly = true;
			this.GridReasonVars.RowHeadersVisible = false;
			this.GridReasonVars.SelectionMode = 1;
			this.GridReasonVars.Size = new Size(456, 336);
			this.GridReasonVars.TabIndex = 0;
			this.Column1.AutoSizeMode = 16;
			this.Column1.HeaderText = "Variable";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column3.HeaderText = "Type";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			this.Column3.Width = 56;
			this.Column2.AutoSizeMode = 16;
			this.Column2.HeaderText = "Value";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(464, 362);
			base.Controls.Add(this.TabReason);
			this.MinimumSize = new Size(290, 160);
			base.Name = "ReasonForm";
			base.StartPosition = 4;
			this.Text = "Consultation reasoning";
			base.Load += new EventHandler(this.ReasonForm_Load);
			this.TabReason.ResumeLayout(false);
			this.TabReasonAllRules.ResumeLayout(false);
			this.TabReasonChain.ResumeLayout(false);
			this.TabReasonVars.ResumeLayout(false);
			this.GridReasonVars.EndInit();
			base.ResumeLayout(false);
		}
	}
}
