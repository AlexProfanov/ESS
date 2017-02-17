using MyEs.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyEs
{
	public class MainForm : Form
	{
		public KnowledgeBase KB = new KnowledgeBase();

		private OpenSave Disp = new OpenSave();

		private bool saved;

		public static string GoalVar;

		private IContainer components = null;

		private MenuStrip menuStrip1;

		private OpenFileDialog openFileDialog1;

		private SaveFileDialog saveFileDialog1;

		private SplitContainer splitContainer1;

		private TabControl tabControl1;

		private TabPage TabVariables;

		private TabPage TabRules;

		private ToolStripMenuItem FileMenuItem;

		private ToolStripMenuItem ViewMenuItem;

		private ToolStripMenuItem SettingsMenuItem;

		private ToolStripMenuItem ConsultationMenuItem;

		private ToolStripMenuItem FileCreate;

		private ToolStripMenuItem FileOpen;

		private ToolStripMenuItem FileSave;

		private ToolStripMenuItem FileSaveAs;

		private ToolStripMenuItem FileExit;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem ViewShowTreeSaved;

		private ToolTip toolTip1;

		private ToolStripMenuItem CascadeRemoving;

		private ToolStripMenuItem GoalItem;

		private ToolStripMenuItem RunItem;

		private ToolStripMenuItem ReasonItem;

		private GroupBox groupBox4;

		public MyDGV GridVars;

		private GroupBox groupBox5;

		private Button BtnVarChange;

		private Button BtnVarRemove;

		private Button BtnVarAdd;

		private GroupBox groupBox6;

		private MyDGV GridRules;

		private GroupBox groupBox8;

		private Button BtnRuleChange;

		private Button BtnRuleRemove;

		private Button BtnRuleAdd;

		private TabPage TabDomains;

		private GroupBox groupBox2;

		private GroupBox groupBox9;

		private Button BtnDomainChange;

		private Button BtnDomainRemove;

		private Button BtnDomainAdd;

		public MyDGV GridDomains;

		private Button BtnDomainPaste;

		private Button BtnDomainCopy;

		private Button BtnVarPaste;

		private Button BtnVarCopy;

		private Button BtnRulePaste;

		private Button BtnRuleCopy;

		private ToolStripMenuItem OrderRules;

		private SplitContainer splitContainer2;

		private Button BtnCollapseCurrent;

		private Button BtnExpandCurrent;

		public TreeView TreeCurrent;

		private Button BtnCollapseSaved;

		private Button BtnExpandSaved;

		private TreeView TreeSaved;

		private GroupBox groupBox1;

		private Button BtnUndo;

		private Button BtnSave;

		private ToolStripMenuItem ViewShowTreeCurrent;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn GridVarsName;

		private DataGridViewTextBoxColumn GridVarsType;

		private DataGridViewTextBoxColumn GridVarsDomain;

		private DataGridViewTextBoxColumn GridVarsQuestion;

		private DataGridViewTextBoxColumn GridVarsReason;

		private DataGridViewTextBoxColumn Номер;

		private DataGridViewTextBoxColumn GridRulesName;

		private DataGridViewTextBoxColumn GridRulePartRule;

		private DataGridViewTextBoxColumn GridRulesVar;

		private DataGridViewTextBoxColumn GridRulesVarValue;

		public MainForm()
		{
			this.InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.KB.SetTree(this.TreeSaved);
			this.KB.SetTree(this.TreeCurrent);
			this.TreeCurrent.Nodes["root"].Text = "Current KB";
			this.saved = false;
			this.splitContainer1.Panel2MinSize = 600;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.saved = false;
			DialogResult dialogResult = MessageBox.Show("Save changes?", "", 3);
			if (dialogResult == 6)
			{
				this.FileSave_Click(new object(), new EventArgs());
			}
			if (dialogResult == 2 || !this.saved)
			{
				e.Cancel = true;
			}
			if (dialogResult == 7)
			{
				e.Cancel = false;
			}
		}

		private void TreeSaved_ClientSizeChanged(object sender, EventArgs e)
		{
			this.BtnExpandSaved.Location = new Point(this.TreeSaved.ClientSize.Width - 29, this.BtnExpandSaved.Location.Y);
			this.BtnCollapseSaved.Location = new Point(this.TreeSaved.ClientSize.Width - 29, this.BtnCollapseSaved.Location.Y);
		}

		private void BtnPlus_Click(object sender, EventArgs e)
		{
			switch (this.tabControl1.SelectedIndex)
			{
			case 0:
				this.TreeSaved.Nodes["root"].Nodes["domains"].ExpandAll();
				break;
			case 1:
				this.TreeSaved.Nodes["root"].Nodes["variables"].ExpandAll();
				break;
			case 2:
				this.TreeSaved.Nodes["root"].Nodes["rules"].ExpandAll();
				break;
			}
		}

		private void BtnMinus_Click(object sender, EventArgs e)
		{
			TreeNode treeNode = new TreeNode();
			switch (this.tabControl1.SelectedIndex)
			{
			case 0:
				treeNode = this.TreeSaved.Nodes["root"].Nodes["domains"];
				break;
			case 1:
				treeNode = this.TreeSaved.Nodes["root"].Nodes["variables"];
				break;
			case 2:
				treeNode = this.TreeSaved.Nodes["root"].Nodes["rules"];
				break;
			}
			IEnumerator enumerator = treeNode.Nodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TreeNode treeNode2 = (TreeNode)enumerator.Current;
					treeNode2.Collapse();
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

		private void BtnSave_Click(object sender, EventArgs e)
		{
			this.KB.SaveDomains(this.TreeSaved);
			this.KB.SaveVars(this.TreeSaved);
			this.KB.SaveRules(this.TreeSaved);
			this.BtnUndo.Enabled = true;
		}

		private void BtnUndo_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вы уверены, что хотите откатить БЗ к сохраненной версии?", "", 4, 0, 256) == 6)
			{
				this.LoadBase();
			}
		}

		private void TreeCurrent_ClientSizeChanged(object sender, EventArgs e)
		{
			this.BtnExpandCurrent.Location = new Point(this.TreeCurrent.ClientSize.Width - 29, this.BtnExpandCurrent.Location.Y);
			this.BtnCollapseCurrent.Location = new Point(this.TreeCurrent.ClientSize.Width - 29, this.BtnCollapseCurrent.Location.Y);
		}

		private void BtnExpandCurrent_Click(object sender, EventArgs e)
		{
			switch (this.tabControl1.SelectedIndex)
			{
			case 0:
				this.TreeCurrent.Nodes["root"].Nodes["domains"].ExpandAll();
				break;
			case 1:
				this.TreeCurrent.Nodes["root"].Nodes["variables"].ExpandAll();
				break;
			case 2:
				this.TreeCurrent.Nodes["root"].Nodes["rules"].ExpandAll();
				break;
			}
		}

		private void BtnCollapseCurrent_Click(object sender, EventArgs e)
		{
			TreeNode treeNode = new TreeNode();
			switch (this.tabControl1.SelectedIndex)
			{
			case 0:
				treeNode = this.TreeCurrent.Nodes["root"].Nodes["domains"];
				break;
			case 1:
				treeNode = this.TreeCurrent.Nodes["root"].Nodes["variables"];
				break;
			case 2:
				treeNode = this.TreeCurrent.Nodes["root"].Nodes["rules"];
				break;
			}
			IEnumerator enumerator = treeNode.Nodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TreeNode treeNode2 = (TreeNode)enumerator.Current;
					treeNode2.Collapse();
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

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			TreeNode treeNode = this.TreeCurrent.Nodes["root"];
			switch (this.tabControl1.SelectedIndex)
			{
			case 0:
				treeNode.Nodes["domains"].Expand();
				treeNode.Nodes["variables"].Collapse();
				treeNode.Nodes["rules"].Collapse();
				break;
			case 1:
				treeNode.Nodes["domains"].Collapse();
				treeNode.Nodes["variables"].Expand();
				treeNode.Nodes["rules"].Collapse();
				break;
			case 2:
				treeNode.Nodes["domains"].Collapse();
				treeNode.Nodes["variables"].Collapse();
				treeNode.Nodes["rules"].Expand();
				break;
			}
			treeNode = this.TreeSaved.Nodes["root"];
			switch (this.tabControl1.SelectedIndex)
			{
			case 0:
				treeNode.Nodes["domains"].Expand();
				treeNode.Nodes["variables"].Collapse();
				treeNode.Nodes["rules"].Collapse();
				break;
			case 1:
				treeNode.Nodes["domains"].Collapse();
				treeNode.Nodes["variables"].Expand();
				treeNode.Nodes["rules"].Collapse();
				break;
			case 2:
				treeNode.Nodes["domains"].Collapse();
				treeNode.Nodes["variables"].Collapse();
				treeNode.Nodes["rules"].Expand();
				break;
			}
		}

		public void AddToTree(int mode, int id, int index)
		{
			switch (mode)
			{
			case 1:
			{
				TreeNode treeNode = this.TreeCurrent.Nodes["root"].Nodes["domains"].Nodes.Insert(index, this.KB.Domains.GetDomainById(id).Name);
				using (Dictionary<int, string>.ValueCollection.Enumerator enumerator = this.KB.Domains.GetDomainValues(id).Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						treeNode.Nodes.Add("'" + current + "'");
					}
				}
				break;
			}
			case 2:
			{
				Variable varById = this.KB.Vars.GetVarById(id);
				TreeNode treeNode = this.TreeCurrent.Nodes["root"].Nodes["variables"].Nodes.Insert(index, varById.Name);
				treeNode.Nodes.Add("Type: '" + varById.type + "'");
				treeNode.Nodes.Add("Domain: '" + this.KB.Domains.GetNameById(varById.domain) + "'");
				break;
			}
			case 3:
			{
				Rule ruleById = this.KB.Rules.GetRuleById(id);
				TreeNode treeNode = this.TreeCurrent.Nodes["root"].Nodes["rules"].Nodes.Insert(index, ruleById.Name);
				treeNode.Nodes.Add("IF");
				using (List<RulePair>.Enumerator enumerator2 = ruleById.IfVars.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						RulePair current2 = enumerator2.Current;
						treeNode.Nodes.Add(string.Concat(new string[]
						{
							"'",
							this.KB.Vars.GetVarById(current2.Variable).Name,
							"'=='",
							this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current2.Variable).domain, current2.Value),
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
							this.KB.Vars.GetVarById(current2.Variable).Name,
							"'='",
							this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current2.Variable).domain, current2.Value),
							"'"
						}));
					}
				}
				treeNode.Nodes.Add("Reasoning: '" + ruleById.Reason + "'");
				break;
			}
			}
		}

		private void FileOpen_Click(object sender, EventArgs e)
		{
			if (this.openFileDialog1.ShowDialog() == 1)
			{
				this.Disp.FileName = this.openFileDialog1.FileName;
				KnowledgeBase knowledgeBase = new KnowledgeBase();
				knowledgeBase = this.Disp.OpenKB();
				if (knowledgeBase != null)
				{
					this.Text = this.Disp.FileName;
					this.KB = knowledgeBase;
					this.LoadBase();
				}
			}
		}

		private void LoadBase()
		{
			this.KB.UndoDomains();
			this.GridDomains.Rows.Clear();
			List<string> domainsNames = this.KB.Domains.GetDomainsNames();
			using (List<string>.Enumerator enumerator = domainsNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					int num = this.GridDomains.Rows.Add();
					this.GridDomains.Rows[num].Tag = this.KB.Domains.GetIdByName(current);
					this.WriteDomainToGrid(num);
				}
			}
			this.BtnDomainPaste.Enabled = this.KB.Domains.HasSavedDomain;
			this.KB.UndoVars();
			this.GridVars.Rows.Clear();
			using (List<int>.Enumerator enumerator2 = Enumerable.ToList<int>(this.KB.Vars.Vars.Keys).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int current2 = enumerator2.Current;
					int num = this.GridVars.Rows.Add();
					this.GridVars.Rows[num].Tag = current2;
					this.WriteVarToGrid(num);
				}
			}
			this.BtnVarPaste.Enabled = this.KB.Vars.HasSavedVar;
			this.KB.UndoRules();
			this.GridRules.Rows.Clear();
			using (List<int>.Enumerator enumerator2 = Enumerable.ToList<int>(this.KB.Rules.Rules.Keys).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int current3 = enumerator2.Current;
					int num = this.GridRules.Rows.Add();
					this.GridRules.Rows[num].Tag = current3;
					this.WriteRuleToGrid(num);
				}
			}
			this.BtnRulePaste.Enabled = this.KB.Rules.HasSavedRule;
			this.KB.InitTree(this.TreeCurrent);
			this.BtnSave_Click(new object(), new EventArgs());
		}

		private void FileSaveAs_Click(object sender, EventArgs e)
		{
			if (this.saveFileDialog1.ShowDialog() == 1)
			{
				this.Disp.FileName = this.saveFileDialog1.FileName;
				this.Text = this.Disp.FileName;
				this.KB.SaveDomains(this.TreeSaved);
				this.KB.SaveVars(this.TreeSaved);
				this.KB.SaveRules(this.TreeSaved);
				this.Disp.SaveKB(this.KB);
				this.saved = true;
			}
			else
			{
				this.saved = false;
			}
		}

		private void FileCreate_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Save changes?", "", 3);
			if (dialogResult == 6)
			{
				this.FileSave_Click(new object(), new EventArgs());
			}
			if (dialogResult == 7 || this.saved)
			{
				this.Text = "";
				this.GridRules.Rows.Clear();
				this.GridVars.Rows.Clear();
				this.GridDomains.Rows.Clear();
				this.KB = new KnowledgeBase();
				this.KB.SetTree(this.TreeSaved);
				this.KB.SetTree(this.TreeCurrent);
			}
		}

		private void FileSave_Click(object sender, EventArgs e)
		{
			if (this.Disp.FileName != null)
			{
				this.KB.SaveDomains(this.TreeSaved);
				this.KB.SaveVars(this.TreeSaved);
				this.KB.SaveRules(this.TreeSaved);
				this.Disp.SaveKB(this.KB);
				this.saved = true;
			}
			else
			{
				this.FileSaveAs_Click(new object(), new EventArgs());
			}
		}

		private void FileExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void ViewShowTreeSaved_Click(object sender, EventArgs e)
		{
			this.splitContainer2.Panel2Collapsed = !this.ViewShowTreeSaved.Checked;
			this.splitContainer2.Panel1Collapsed = !this.ViewShowTreeCurrent.Checked;
			if (!this.ViewShowTreeSaved.Checked && !this.ViewShowTreeCurrent.Checked)
			{
				this.splitContainer1.Panel1Collapsed = true;
			}
			else
			{
				this.splitContainer1.Panel1Collapsed = false;
			}
		}

		private void ViewShowTreeCurrent_Click(object sender, EventArgs e)
		{
			this.splitContainer2.Panel1Collapsed = !this.ViewShowTreeCurrent.Checked;
			this.splitContainer2.Panel2Collapsed = !this.ViewShowTreeSaved.Checked;
			if (!this.ViewShowTreeSaved.Checked && !this.ViewShowTreeCurrent.Checked)
			{
				this.splitContainer1.Panel1Collapsed = true;
			}
			else
			{
				this.splitContainer1.Panel1Collapsed = false;
			}
		}

		private void CascadeRemoving_Click(object sender, EventArgs e)
		{
			this.KB.CascadeRemoving = this.CascadeRemoving.Checked;
		}

		private void GoalItem_Click(object sender, EventArgs e)
		{
			SelectGoal.Varlist = this.KB.Vars.GetVarsNames();
			SelectGoal selectGoal = new SelectGoal();
			selectGoal.ShowDialog(this);
			if (selectGoal.DialogResult == 1)
			{
				this.KB.GoalVariable = this.KB.Vars.GetIdVarByName(MainForm.GoalVar);
				this.KB.SaveGoal(this.TreeCurrent);
			}
		}

		private void ConsultationMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			if (this.KB.GoalVariable != -1 && this.GridRules.Rows.Count > 0)
			{
				this.RunItem.Enabled = true;
			}
			else
			{
				this.RunItem.Enabled = false;
			}
			if (this.GridVars.RowCount > 0)
			{
				this.GoalItem.Enabled = true;
			}
			else
			{
				this.GoalItem.Enabled = false;
			}
		}

		private void RunItem_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			this.menuStrip1.Focus();
			WorkingMemory.InitMemory(this.KB.Domains, this.KB.Vars, this.KB.Rules, this.KB.GoalVariable, this.OrderRules.Checked);
			WorkingMemory.StartFinding();
			if (WorkingMemory.success)
			{
				this.ReasonItem.Enabled = true;
				if (WorkingMemory.KnownVars.ContainsKey(this.KB.GoalVariable))
				{
					MessageBox.Show(string.Concat(new string[]
					{
						"Success: '",
						this.KB.Vars.GetVarById(this.KB.GoalVariable).Name,
						"'='",
						this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(this.KB.GoalVariable).domain, WorkingMemory.KnownVars[this.KB.GoalVariable]),
						"'"
					}));
				}
				else
				{
					MessageBox.Show("Не удалось вывести цель");
				}
			}
			else
			{
				this.ReasonItem.Enabled = false;
			}
			base.Enabled = true;
		}

		private void ReasonItem_Click(object sender, EventArgs e)
		{
			ReasonForm reasonForm = new ReasonForm(this.KB);
			reasonForm.ShowDialog(this);
		}

		private void BtnDomainAdd_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.GridDomains.CurrentRow != null)
			{
				num = this.GridDomains.CurrentRow.Index + 1;
			}
			DialogResult dialogResult;
			do
			{
				DomainsForm domainsForm = new DomainsForm();
				domainsForm.index = num;
				domainsForm.KB = this.KB;
				dialogResult = domainsForm.ShowDialog(this);
				if (dialogResult != 2)
				{
					this.AddToTree(1, domainsForm.output, num);
					this.TreeCurrent.Nodes["root"].Nodes["domains"].Expand();
					this.GridDomains.Rows.Insert(num, 1);
					this.GridDomains.Rows[num].Tag = domainsForm.output;
					this.GridDomains.CurrentCell = this.GridDomains[0, num];
					this.WriteDomainToGrid(num);
				}
				num++;
			}
			while (dialogResult == 4);
		}

		private void BtnDomainChange_Click(object sender, EventArgs e)
		{
			DomainsForm domainsForm = new DomainsForm();
			domainsForm.input = Convert.ToInt32(this.GridDomains.CurrentRow.Tag);
			domainsForm.index = this.GridDomains.CurrentRow.Index;
			domainsForm.KB = this.KB;
			domainsForm.ShowDialog(this);
			if (domainsForm.DialogResult == 1)
			{
				this.TreeCurrent.Nodes["root"].Nodes["domains"].Nodes.RemoveAt(this.GridDomains.CurrentRow.Index);
				this.AddToTree(1, domainsForm.input, this.GridDomains.CurrentRow.Index);
				this.WriteDomainToGrid(this.GridDomains.CurrentRow.Index);
				this.RefreshVarRowByDomain(domainsForm.input);
				this.RefreshRuleRow(0, domainsForm.input);
			}
		}

		private void BtnDomainRemove_Click(object sender, EventArgs e)
		{
			string text = "";
			if (this.CascadeRemoving.Checked)
			{
				text = "Внимание, включен режим контекстного удаления! ";
			}
			if (MessageBox.Show(string.Concat(new object[]
			{
				text,
				"Вы уверены,что хотите удалить домен ",
				this.GridDomains.CurrentRow.Cells[1].Value,
				"?"
			}), "", 4, 0, 256) == 6)
			{
				int num = Convert.ToInt32(this.GridDomains.CurrentRow.Tag);
				if (this.KB.Domains.RemoveDomain(num))
				{
					this.TreeCurrent.Nodes["root"].Nodes["domains"].Nodes.RemoveAt(this.GridDomains.CurrentRow.Index);
					this.GridDomains.Rows.Remove(this.GridDomains.CurrentRow);
					List<int> list = new List<int>();
					if (this.GridVars.RowCount > 0)
					{
						for (int i = this.GridVars.RowCount - 1; i >= 0; i--)
						{
							if (this.KB.Vars.GetVarById(Convert.ToInt32(this.GridVars.Rows[i].Tag)).domain == num)
							{
								list.AddRange(this.KB.Rules.GetRulesByVar(Convert.ToInt32(this.GridVars.Rows[i].Tag)));
								if (this.KB.GoalVariable == Convert.ToInt32(this.GridVars.Rows[i].Tag))
								{
									this.KB.GoalVariable = -1;
									this.TreeCurrent.Nodes["root"].Nodes["goal"].Nodes.Clear();
								}
								this.TreeCurrent.Nodes["root"].Nodes["variables"].Nodes.RemoveAt(i);
								this.GridVars.Rows.RemoveAt(i);
							}
						}
					}
					this.KB.Vars.RemoveVarByDomains(num);
					list = Enumerable.ToList<int>(Enumerable.Distinct<int>(list));
					for (int i = list.Count - 1; i >= 0; i--)
					{
						this.RemoveRule(list[i]);
					}
				}
				else
				{
					MessageBox.Show("Удаление невозможно,так как данный домен используется в переменных");
				}
			}
		}

		private void BtnDomainCopy_Click(object sender, EventArgs e)
		{
			this.KB.SavedDomain = (Domain)this.KB.Domains.GetDomainById(Convert.ToInt32(this.GridDomains.CurrentRow.Tag)).Clone();
			this.BtnDomainPaste.Enabled = true;
		}

		private void BtnDomainPaste_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.GridDomains.CurrentRow != null)
			{
				num = this.GridDomains.CurrentRow.Index + 1;
			}
			this.GridDomains.Rows.Insert(num, 1);
			Domain savedDomain = this.KB.SavedDomain;
			this.GridDomains.Rows[num].Tag = this.KB.Domains.PasteDomain(num, -1, (Domain)savedDomain.Clone());
			this.AddToTree(1, Convert.ToInt32(this.GridDomains.Rows[num].Tag), num);
			this.WriteDomainToGrid(num);
		}

		private void GridDomains_DragDrop(object sender, DragEventArgs e)
		{
			if (this.GridDomains.DragDropTargetIndex > -1)
			{
				int dragDropTargetIndex = this.GridDomains.DragDropTargetIndex;
				int dragDropSourceIndex = this.GridDomains.DragDropSourceIndex;
				this.KB.Domains.ReplaceDomains(dragDropSourceIndex, dragDropTargetIndex);
				TreeNode treeNode = this.TreeCurrent.Nodes["root"].Nodes["domains"].Nodes[dragDropSourceIndex];
				this.TreeCurrent.Nodes["root"].Nodes["domains"].Nodes.RemoveAt(dragDropSourceIndex);
				this.TreeCurrent.Nodes["root"].Nodes["domains"].Nodes.Insert(dragDropTargetIndex, treeNode);
				this.GridDomains.CurrentCell = this.GridDomains.Rows[dragDropTargetIndex].Cells[0];
			}
		}

		public void WriteDomainToGrid(int index)
		{
			Domain domainById = this.KB.Domains.GetDomainById(Convert.ToInt32(this.GridDomains.Rows[index].Tag));
			this.GridDomains.Rows[index].Cells[1].Value = domainById.Name;
			string text = "";
			using (List<string>.Enumerator enumerator = Enumerable.ToList<string>(domainById.DomValues.Values).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					text = text + current + "; ";
				}
			}
			this.GridDomains.Rows[index].Cells[2].Value = text.Substring(0, text.Length - 2);
		}

		private void GridDomains_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (this.GridDomains.CurrentRow != null)
			{
				this.BtnDomainChange_Click(sender, new EventArgs());
			}
		}

		private void GridDomains_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			this.BtnDomainChange.Enabled = true;
			this.BtnDomainRemove.Enabled = true;
			this.BtnDomainCopy.Enabled = true;
			IEnumerator enumerator = this.GridDomains.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					dataGridViewRow.Cells[0].Value = dataGridViewRow.Index + 1;
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

		private void GridDomains_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (this.GridDomains.RowCount == 0)
			{
				this.BtnDomainChange.Enabled = false;
				this.BtnDomainRemove.Enabled = false;
				this.BtnDomainCopy.Enabled = false;
			}
			IEnumerator enumerator = this.GridDomains.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					dataGridViewRow.Cells[0].Value = dataGridViewRow.Index + 1;
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

		private void GridDomains_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.GridDomains.CurrentRow != null)
			{
				if (e.KeyData == 46)
				{
					this.BtnDomainRemove_Click(sender, new EventArgs());
				}
				else if (e.KeyData == 13)
				{
					this.BtnDomainChange_Click(sender, new EventArgs());
				}
			}
		}

		private void GridDomains_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.ConvertFromUtf32(3) == e.KeyChar.ToString() && this.BtnDomainCopy.Enabled)
			{
				this.BtnDomainCopy_Click(sender, new EventArgs());
			}
			if (char.ConvertFromUtf32(22) == e.KeyChar.ToString() && this.BtnDomainPaste.Enabled)
			{
				this.BtnDomainPaste_Click(sender, new EventArgs());
			}
		}

		public void RefreshDomainRows()
		{
			IEnumerator enumerator = this.GridDomains.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					this.WriteDomainToGrid(dataGridViewRow.Index);
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

		private void BtnVarAdd_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.GridVars.CurrentRow != null)
			{
				num = this.GridVars.CurrentRow.Index + 1;
			}
			DialogResult dialogResult;
			do
			{
				VariablesForm variablesForm = new VariablesForm();
				variablesForm.index = num;
				variablesForm.KB = this.KB;
				variablesForm.MForm = this;
				dialogResult = variablesForm.ShowDialog(this);
				if (dialogResult != 2)
				{
					this.AddToTree(2, variablesForm.output, num);
					this.TreeCurrent.Nodes["root"].Nodes["variables"].Expand();
					this.GridVars.Rows.Insert(num, 1);
					this.GridVars.Rows[num].Tag = variablesForm.output;
					this.GridVars.CurrentCell = this.GridVars[0, num];
					this.WriteVarToGrid(num);
				}
				num++;
			}
			while (dialogResult == 4);
		}

		private void BtnVarChange_Click(object sender, EventArgs e)
		{
			VariablesForm variablesForm = new VariablesForm();
			variablesForm.input = Convert.ToInt32(this.GridVars.CurrentRow.Tag);
			variablesForm.KB = this.KB;
			variablesForm.index = this.GridVars.CurrentRow.Index;
			variablesForm.MForm = this;
			variablesForm.ShowDialog(this);
			if (variablesForm.DialogResult == 1)
			{
				this.TreeCurrent.Nodes["root"].Nodes["variables"].Nodes.RemoveAt(this.GridVars.CurrentRow.Index);
				this.AddToTree(2, variablesForm.input, this.GridVars.CurrentRow.Index);
				this.WriteVarToGrid(this.GridVars.CurrentRow.Index);
				this.RefreshRuleRow(variablesForm.input, 0);
			}
		}

		private void BtnVarRemove_Click(object sender, EventArgs e)
		{
			string text = "";
			if (this.CascadeRemoving.Checked)
			{
				text = "Внимание, включен режим контекстного удаления! ";
			}
			int num = Convert.ToInt32(this.GridVars.CurrentRow.Tag);
			if (MessageBox.Show(text + "Вы уверены,что хотите удалить переменную " + Convert.ToString(this.GridVars.CurrentRow.Cells[1].Value) + "?", "", 4, 0, 256) == 6)
			{
				if (this.KB.Vars.RemoveVar(num))
				{
					List<int> list = new List<int>();
					list.AddRange(this.KB.Rules.GetRulesByVar(num));
					if (this.KB.GoalVariable == num)
					{
						this.KB.GoalVariable = -1;
						this.TreeCurrent.Nodes["root"].Nodes["goal"].Nodes.Clear();
					}
					this.TreeCurrent.Nodes["root"].Nodes["variables"].Nodes.RemoveAt(this.GridVars.CurrentRow.Index);
					this.GridVars.Rows.Remove(this.GridVars.CurrentRow);
					for (int i = list.Count - 1; i >= 0; i--)
					{
						this.RemoveRule(list[i]);
					}
					this.RefreshRuleRow(num, 0);
				}
				else
				{
					MessageBox.Show("Удаление невозможно,так как эта переменная используется в правилах");
				}
			}
		}

		private void BtnVarCopy_Click(object sender, EventArgs e)
		{
			this.KB.SavedVar = (Variable)this.KB.Vars.GetVarById(Convert.ToInt32(this.GridVars.CurrentRow.Tag)).Clone();
			this.BtnVarPaste.Enabled = true;
		}

		private void BtnVarPaste_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.GridVars.CurrentRow != null)
			{
				num = this.GridVars.CurrentRow.Index + 1;
			}
			this.GridVars.Rows.Insert(num, 1);
			this.GridVars.Rows[num].Tag = this.KB.Vars.PasteVar(num, -1, (Variable)this.KB.SavedVar.Clone());
			this.AddToTree(2, Convert.ToInt32(this.GridVars.Rows[num].Tag), num);
			this.WriteVarToGrid(num);
		}

		private void GridVars_DragDrop(object sender, DragEventArgs e)
		{
			if (this.GridVars.DragDropTargetIndex > -1)
			{
				int dragDropSourceIndex = this.GridVars.DragDropSourceIndex;
				int dragDropTargetIndex = this.GridVars.DragDropTargetIndex;
				this.KB.Vars.ReplaceVariables(dragDropSourceIndex, dragDropTargetIndex);
				TreeNode treeNode = this.TreeCurrent.Nodes["root"].Nodes["variables"].Nodes[dragDropSourceIndex];
				this.TreeCurrent.Nodes["root"].Nodes["variables"].Nodes.RemoveAt(dragDropSourceIndex);
				this.TreeCurrent.Nodes["root"].Nodes["variables"].Nodes.Insert(dragDropTargetIndex, treeNode);
				this.GridVars.CurrentCell = this.GridVars.Rows[dragDropTargetIndex].Cells[0];
			}
		}

		private void GridVars_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (this.GridVars.CurrentRow != null)
			{
				this.BtnVarChange_Click(sender, new EventArgs());
			}
		}

		public void WriteVarToGrid(int index)
		{
			Variable varById = this.KB.Vars.GetVarById(Convert.ToInt32(this.GridVars.Rows[index].Tag));
			this.GridVars.Rows[index].Cells[1].Value = varById.Name;
			this.GridVars.Rows[index].Cells[2].Value = varById.type;
			this.GridVars.Rows[index].Cells[3].Value = this.KB.Domains.GetNameById(varById.domain);
			this.GridVars.Rows[index].Cells[4].Value = varById.Question;
			this.GridVars.Rows[index].Cells[5].Value = varById.Reason;
		}

		private void RefreshVarRowByDomain(int IdDomain)
		{
			IEnumerator enumerator = this.GridVars.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					if (this.KB.Vars.GetVarById(Convert.ToInt32(dataGridViewRow.Tag)).domain == IdDomain)
					{
						dataGridViewRow.Cells[3].Value = this.KB.Domains.GetNameById(IdDomain);
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

		private void GridVars_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			this.BtnVarChange.Enabled = true;
			this.BtnVarRemove.Enabled = true;
			this.BtnVarCopy.Enabled = true;
			IEnumerator enumerator = this.GridVars.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					dataGridViewRow.Cells[0].Value = dataGridViewRow.Index + 1;
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

		private void GridVars_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (this.GridVars.RowCount == 0)
			{
				this.BtnVarChange.Enabled = false;
				this.BtnVarRemove.Enabled = false;
				this.BtnVarCopy.Enabled = false;
			}
			IEnumerator enumerator = this.GridVars.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					dataGridViewRow.Cells[0].Value = dataGridViewRow.Index + 1;
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

		private void GridVars_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.GridVars.CurrentRow != null)
			{
				if (e.KeyData == 46)
				{
					this.BtnVarRemove_Click(sender, new EventArgs());
				}
				else if (e.KeyData == 13)
				{
					this.BtnVarChange_Click(sender, new EventArgs());
				}
			}
		}

		private void GridVars_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.ConvertFromUtf32(3) == e.KeyChar.ToString() && this.BtnVarCopy.Enabled)
			{
				this.BtnVarCopy_Click(sender, new EventArgs());
			}
			if (char.ConvertFromUtf32(22) == e.KeyChar.ToString() && this.BtnVarPaste.Enabled)
			{
				this.BtnVarPaste_Click(sender, new EventArgs());
			}
		}

		private void BtnRuleAdd_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.GridRules.CurrentRow != null)
			{
				num = this.GridRules.CurrentRow.Index + 1;
			}
			DialogResult dialogResult;
			do
			{
				RulesForm rulesForm = new RulesForm();
				rulesForm.index = num;
				rulesForm.KB = this.KB;
				dialogResult = rulesForm.ShowDialog(this);
				if (dialogResult != 2)
				{
					this.AddToTree(3, rulesForm.output, num);
					this.TreeCurrent.Nodes["root"].Nodes["rules"].Expand();
					this.GridRules.Rows.Insert(num, 1);
					this.GridRules.Rows[num].Tag = rulesForm.output;
					this.GridRules.CurrentCell = this.GridRules[0, num];
					this.WriteRuleToGrid(num);
				}
				num++;
			}
			while (dialogResult == 4);
		}

		private void BtnRuleChange_Click(object sender, EventArgs e)
		{
			RulesForm rulesForm = new RulesForm();
			rulesForm.input = Convert.ToInt32(this.GridRules.CurrentRow.Tag);
			rulesForm.KB = this.KB;
			rulesForm.index = this.GridRules.CurrentRow.Index;
			rulesForm.ShowDialog(this);
			if (rulesForm.DialogResult == 1)
			{
				this.TreeCurrent.Nodes["root"].Nodes["rules"].Nodes.RemoveAt(this.GridRules.CurrentRow.Index);
				this.AddToTree(3, Convert.ToInt32(this.GridRules.CurrentRow.Tag), this.GridRules.CurrentRow.Index);
				this.WriteRuleToGrid(this.GridRules.CurrentRow.Index);
			}
		}

		private void BtnRuleRemove_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вы уверены,что хотите удалить правило " + this.GridRules.CurrentRow.Cells[1].Value.ToString() + " ?", "", 4, 0, 256) == 6)
			{
				this.RemoveRule(Convert.ToInt32(this.GridRules.CurrentRow.Tag));
			}
		}

		private void BtnRuleCopy_Click(object sender, EventArgs e)
		{
			this.KB.SavedRule = (Rule)this.KB.Rules.GetRuleById(Convert.ToInt32(this.GridRules.CurrentRow.Tag)).Clone();
			this.BtnRulePaste.Enabled = true;
		}

		private void BtnRulePaste_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.GridRules.CurrentRow != null)
			{
				num = this.GridRules.CurrentRow.Index + 1;
			}
			this.GridRules.Rows.Insert(num, 1);
			this.GridRules.Rows[num].Tag = this.KB.Rules.PasteRule(num, -1, (Rule)this.KB.SavedRule.Clone());
			this.AddToTree(3, Convert.ToInt32(this.GridRules.Rows[num].Tag), num);
			this.WriteRuleToGrid(num);
		}

		private void RefreshRuleRow(int IdVar, int IdDomain)
		{
			if (IdDomain == 0)
			{
				List<int> list = this.KB.Rules.GetRulesByVar(IdVar);
				IEnumerator enumerator = this.GridRules.Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
						if (list.Contains(Convert.ToInt32(dataGridViewRow.Tag)))
						{
							this.WriteRuleToGrid(dataGridViewRow.Index);
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
			else
			{
				List<int> list = this.KB.Rules.GetRulesByDomain(IdDomain);
				IEnumerator enumerator = this.GridRules.Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
						if (list.Contains(Convert.ToInt32(dataGridViewRow.Tag)))
						{
							this.WriteRuleToGrid(dataGridViewRow.Index);
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
		}

		private void WriteRuleToGrid(int index)
		{
			int idRule = Convert.ToInt32(this.GridRules.Rows[index].Tag);
			Rule ruleById = this.KB.Rules.GetRuleById(idRule);
			this.GridRules.Rows[index].Cells[1].Value = ruleById.Name;
			string text = "";
			using (List<RulePair>.Enumerator enumerator = ruleById.IfVars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RulePair current = enumerator.Current;
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"'",
						this.KB.Vars.GetVarById(current.Variable).Name,
						"'=='",
						this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current.Variable).domain, current.Value),
						"' И "
					});
				}
			}
			this.GridRules.Rows[index].Cells[2].Value = text.Substring(0, text.Length - 3);
			text = "";
			using (List<RulePair>.Enumerator enumerator = ruleById.ThenVars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RulePair current = enumerator.Current;
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"'",
						this.KB.Vars.GetVarById(current.Variable).Name,
						"'='",
						this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current.Variable).domain, current.Value),
						"' И "
					});
				}
			}
			this.GridRules.Rows[index].Cells[3].Value = text.Substring(0, text.Length - 3);
			this.GridRules.Rows[index].Cells[4].Value = ruleById.Reason;
		}

		private void RemoveRule(int IdRule)
		{
			this.KB.Rules.RemoveRule(IdRule);
			IEnumerator enumerator = this.GridRules.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					if (Convert.ToInt32(dataGridViewRow.Tag) == IdRule)
					{
						this.TreeCurrent.Nodes["root"].Nodes["rules"].Nodes.RemoveAt(dataGridViewRow.Index);
						this.GridRules.Rows.Remove(dataGridViewRow);
						break;
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

		private void GridRules_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (this.GridRules.CurrentRow != null)
			{
				this.BtnRuleChange_Click(sender, new EventArgs());
			}
		}

		private void GridRules_DragDrop(object sender, DragEventArgs e)
		{
			if (this.GridRules.DragDropTargetIndex > -1)
			{
				int dragDropSourceIndex = this.GridRules.DragDropSourceIndex;
				int dragDropTargetIndex = this.GridRules.DragDropTargetIndex;
				this.KB.Rules.ReplaceRules(dragDropSourceIndex, dragDropTargetIndex);
				TreeNode treeNode = this.TreeCurrent.Nodes["root"].Nodes["rules"].Nodes[dragDropSourceIndex];
				this.TreeCurrent.Nodes["root"].Nodes["rules"].Nodes.RemoveAt(dragDropSourceIndex);
				this.TreeCurrent.Nodes["root"].Nodes["rules"].Nodes.Insert(dragDropTargetIndex, treeNode);
				this.GridRules.CurrentCell = this.GridRules.Rows[dragDropTargetIndex].Cells[0];
			}
		}

		private void GridRules_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			this.BtnRuleChange.Enabled = true;
			this.BtnRuleRemove.Enabled = true;
			this.BtnRuleCopy.Enabled = true;
			IEnumerator enumerator = this.GridRules.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					dataGridViewRow.Cells[0].Value = dataGridViewRow.Index + 1;
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

		private void GridRules_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (this.GridRules.RowCount == 0)
			{
				this.BtnRuleChange.Enabled = false;
				this.BtnRuleRemove.Enabled = false;
				this.BtnRuleCopy.Enabled = false;
			}
			IEnumerator enumerator = this.GridRules.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					dataGridViewRow.Cells[0].Value = dataGridViewRow.Index + 1;
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

		private void GridRules_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.GridRules.CurrentRow != null)
			{
				if (e.KeyData == 46)
				{
					this.BtnRuleRemove_Click(sender, new EventArgs());
				}
				else if (e.KeyData == 13)
				{
					this.BtnRuleChange_Click(sender, new EventArgs());
				}
			}
		}

		private void GridRules_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.ConvertFromUtf32(3) == e.KeyChar.ToString() && this.BtnRuleCopy.Enabled)
			{
				this.BtnRuleCopy_Click(sender, new EventArgs());
			}
			if (char.ConvertFromUtf32(22) == e.KeyChar.ToString() && this.BtnRulePaste.Enabled)
			{
				this.BtnRulePaste_Click(sender, new EventArgs());
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new MenuStrip();
			this.FileMenuItem = new ToolStripMenuItem();
			this.FileCreate = new ToolStripMenuItem();
			this.FileOpen = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.FileSave = new ToolStripMenuItem();
			this.FileSaveAs = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.FileExit = new ToolStripMenuItem();
			this.ViewMenuItem = new ToolStripMenuItem();
			this.ViewShowTreeSaved = new ToolStripMenuItem();
			this.ViewShowTreeCurrent = new ToolStripMenuItem();
			this.SettingsMenuItem = new ToolStripMenuItem();
			this.CascadeRemoving = new ToolStripMenuItem();
			this.OrderRules = new ToolStripMenuItem();
			this.ConsultationMenuItem = new ToolStripMenuItem();
			this.GoalItem = new ToolStripMenuItem();
			this.RunItem = new ToolStripMenuItem();
			this.ReasonItem = new ToolStripMenuItem();
			this.openFileDialog1 = new OpenFileDialog();
			this.saveFileDialog1 = new SaveFileDialog();
			this.splitContainer1 = new SplitContainer();
			this.splitContainer2 = new SplitContainer();
			this.BtnCollapseCurrent = new Button();
			this.BtnExpandCurrent = new Button();
			this.TreeCurrent = new TreeView();
			this.BtnCollapseSaved = new Button();
			this.BtnExpandSaved = new Button();
			this.TreeSaved = new TreeView();
			this.groupBox1 = new GroupBox();
			this.BtnUndo = new Button();
			this.BtnSave = new Button();
			this.tabControl1 = new TabControl();
			this.TabDomains = new TabPage();
			this.groupBox2 = new GroupBox();
			this.BtnDomainPaste = new Button();
			this.BtnDomainCopy = new Button();
			this.groupBox9 = new GroupBox();
			this.BtnDomainChange = new Button();
			this.BtnDomainRemove = new Button();
			this.BtnDomainAdd = new Button();
			this.GridDomains = new MyDGV();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.TabVariables = new TabPage();
			this.groupBox4 = new GroupBox();
			this.BtnVarPaste = new Button();
			this.BtnVarCopy = new Button();
			this.groupBox5 = new GroupBox();
			this.BtnVarChange = new Button();
			this.BtnVarRemove = new Button();
			this.BtnVarAdd = new Button();
			this.GridVars = new MyDGV();
			this.Column4 = new DataGridViewTextBoxColumn();
			this.GridVarsName = new DataGridViewTextBoxColumn();
			this.GridVarsType = new DataGridViewTextBoxColumn();
			this.GridVarsDomain = new DataGridViewTextBoxColumn();
			this.GridVarsQuestion = new DataGridViewTextBoxColumn();
			this.GridVarsReason = new DataGridViewTextBoxColumn();
			this.TabRules = new TabPage();
			this.groupBox6 = new GroupBox();
			this.BtnRulePaste = new Button();
			this.BtnRuleCopy = new Button();
			this.groupBox8 = new GroupBox();
			this.BtnRuleChange = new Button();
			this.BtnRuleRemove = new Button();
			this.BtnRuleAdd = new Button();
			this.GridRules = new MyDGV();
			this.Номер = new DataGridViewTextBoxColumn();
			this.GridRulesName = new DataGridViewTextBoxColumn();
			this.GridRulePartRule = new DataGridViewTextBoxColumn();
			this.GridRulesVar = new DataGridViewTextBoxColumn();
			this.GridRulesVarValue = new DataGridViewTextBoxColumn();
			this.toolTip1 = new ToolTip(this.components);
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.TabDomains.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.GridDomains.BeginInit();
			this.TabVariables.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.GridVars.BeginInit();
			this.TabRules.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.GridRules.BeginInit();
			base.SuspendLayout();
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.FileMenuItem,
				this.ViewMenuItem,
				this.SettingsMenuItem,
				this.ConsultationMenuItem
			});
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(860, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.FileMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.FileCreate,
				this.FileOpen,
				this.toolStripSeparator1,
				this.FileSave,
				this.FileSaveAs,
				this.toolStripSeparator2,
				this.FileExit
			});
			this.FileMenuItem.Name = "FileMenuItem";
			this.FileMenuItem.Size = new Size(37, 20);
			this.FileMenuItem.Text = "File";
			this.FileCreate.Image = Resources.Document;
			this.FileCreate.Name = "FileCreate";
			this.FileCreate.ShortcutKeys = 131150;
			this.FileCreate.Size = new Size(193, 22);
			this.FileCreate.Text = "Create";
			this.FileCreate.ToolTipText = "Создать новую ЭС";
			this.FileCreate.Click += new EventHandler(this.FileCreate_Click);
			this.FileOpen.Image = Resources.Open;
			this.FileOpen.Name = "FileOpen";
			this.FileOpen.ShortcutKeys = 131151;
			this.FileOpen.Size = new Size(193, 22);
			this.FileOpen.Text = "Open...";
			this.FileOpen.ToolTipText = "Диалог открытия ЭС";
			this.FileOpen.Click += new EventHandler(this.FileOpen_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(190, 6);
			this.FileSave.Image = Resources.Save;
			this.FileSave.Name = "FileSave";
			this.FileSave.ShortcutKeys = 131155;
			this.FileSave.Size = new Size(193, 22);
			this.FileSave.Text = "Save";
			this.FileSave.ToolTipText = "Сохранение ЭС";
			this.FileSave.Click += new EventHandler(this.FileSave_Click);
			this.FileSaveAs.Image = Resources.SaveAll;
			this.FileSaveAs.Name = "FileSaveAs";
			this.FileSaveAs.ShortcutKeys = 196691;
			this.FileSaveAs.Size = new Size(193, 22);
			this.FileSaveAs.Text = "Save as...";
			this.FileSaveAs.ToolTipText = "Диалог сохранения ЭС";
			this.FileSaveAs.Click += new EventHandler(this.FileSaveAs_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(190, 6);
			this.FileExit.Image = Resources.Exit;
			this.FileExit.Name = "FileExit";
			this.FileExit.Size = new Size(193, 22);
			this.FileExit.Text = "Exit";
			this.FileExit.ToolTipText = "Выйти";
			this.FileExit.Click += new EventHandler(this.FileExit_Click);
			this.ViewMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.ViewShowTreeSaved,
				this.ViewShowTreeCurrent
			});
			this.ViewMenuItem.Name = "ViewMenuItem";
			this.ViewMenuItem.Size = new Size(44, 20);
			this.ViewMenuItem.Text = "View";
			this.ViewShowTreeSaved.Checked = true;
			this.ViewShowTreeSaved.CheckOnClick = true;
			this.ViewShowTreeSaved.CheckState = 1;
			this.ViewShowTreeSaved.Name = "ViewShowTreeSaved";
			this.ViewShowTreeSaved.Size = new Size(154, 22);
			this.ViewShowTreeSaved.Text = "Saved KB tree";
			this.ViewShowTreeSaved.ToolTipText = "Показать/скрыть дерево сохраненной БЗ";
			this.ViewShowTreeSaved.Click += new EventHandler(this.ViewShowTreeSaved_Click);
			this.ViewShowTreeCurrent.Checked = true;
			this.ViewShowTreeCurrent.CheckOnClick = true;
			this.ViewShowTreeCurrent.CheckState = 1;
			this.ViewShowTreeCurrent.Name = "ViewShowTreeCurrent";
			this.ViewShowTreeCurrent.Size = new Size(154, 22);
			this.ViewShowTreeCurrent.Text = "Current KB tree";
			this.ViewShowTreeCurrent.ToolTipText = "Показать/скрыть дерево текущей БЗ";
			this.ViewShowTreeCurrent.Click += new EventHandler(this.ViewShowTreeCurrent_Click);
			this.SettingsMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.CascadeRemoving,
				this.OrderRules
			});
			this.SettingsMenuItem.Name = "SettingsMenuItem";
			this.SettingsMenuItem.Size = new Size(61, 20);
			this.SettingsMenuItem.Text = "Settings";
			this.CascadeRemoving.Checked = true;
			this.CascadeRemoving.CheckOnClick = true;
			this.CascadeRemoving.CheckState = 1;
			this.CascadeRemoving.Name = "CascadeRemoving";
			this.CascadeRemoving.Size = new Size(173, 22);
			this.CascadeRemoving.Text = "Cascade delete";
			this.CascadeRemoving.ToolTipText = "Каскадное удаление/запрет на удаление";
			this.CascadeRemoving.Click += new EventHandler(this.CascadeRemoving_Click);
			this.OrderRules.CheckOnClick = true;
			this.OrderRules.Name = "OrderRules";
			this.OrderRules.Size = new Size(173, 22);
			this.OrderRules.Text = "Regard rule's order";
			this.ConsultationMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.GoalItem,
				this.RunItem,
				this.ReasonItem
			});
			this.ConsultationMenuItem.Name = "ConsultationMenuItem";
			this.ConsultationMenuItem.Size = new Size(60, 20);
			this.ConsultationMenuItem.Text = "Consult";
			this.ConsultationMenuItem.DropDownOpening += new EventHandler(this.ConsultationMenuItem_DropDownOpening);
			this.GoalItem.Enabled = false;
			this.GoalItem.Image = Resources.Aim;
			this.GoalItem.Name = "GoalItem";
			this.GoalItem.Size = new Size(129, 22);
			this.GoalItem.Text = "Goal";
			this.GoalItem.ToolTipText = "Выбор целевой переменной";
			this.GoalItem.Click += new EventHandler(this.GoalItem_Click);
			this.RunItem.Enabled = false;
			this.RunItem.Name = "RunItem";
			this.RunItem.ShortcutKeys = 116;
			this.RunItem.Size = new Size(129, 22);
			this.RunItem.Text = "Run";
			this.RunItem.ToolTipText = "Начать консультацию";
			this.RunItem.Click += new EventHandler(this.RunItem_Click);
			this.ReasonItem.Enabled = false;
			this.ReasonItem.Image = Resources.dialog_question;
			this.ReasonItem.Name = "ReasonItem";
			this.ReasonItem.Size = new Size(129, 22);
			this.ReasonItem.Text = "Reasoning";
			this.ReasonItem.ToolTipText = "Показать ход логического вывода";
			this.ReasonItem.Click += new EventHandler(this.ReasonItem_Click);
			this.openFileDialog1.Filter = "Файлы проектов (*.es)|*.es";
			this.saveFileDialog1.Filter = "Файлы проектов (*.es)|*.es";
			this.splitContainer1.Dock = 5;
			this.splitContainer1.Location = new Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel1MinSize = 172;
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new Size(860, 472);
			this.splitContainer1.SplitterDistance = 174;
			this.splitContainer1.TabIndex = 1;
			this.splitContainer2.Dock = 5;
			this.splitContainer2.Location = new Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = 0;
			this.splitContainer2.Panel1.Controls.Add(this.BtnCollapseCurrent);
			this.splitContainer2.Panel1.Controls.Add(this.BtnExpandCurrent);
			this.splitContainer2.Panel1.Controls.Add(this.TreeCurrent);
			this.splitContainer2.Panel1MinSize = 100;
			this.splitContainer2.Panel2.Controls.Add(this.BtnCollapseSaved);
			this.splitContainer2.Panel2.Controls.Add(this.BtnExpandSaved);
			this.splitContainer2.Panel2.Controls.Add(this.TreeSaved);
			this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer2.Panel2MinSize = 100;
			this.splitContainer2.Size = new Size(174, 472);
			this.splitContainer2.SplitterDistance = 223;
			this.splitContainer2.TabIndex = 0;
			this.BtnCollapseCurrent.Anchor = 9;
			this.BtnCollapseCurrent.Location = new Point(141, 22);
			this.BtnCollapseCurrent.Name = "BtnCollapseCurrent";
			this.BtnCollapseCurrent.Size = new Size(31, 21);
			this.BtnCollapseCurrent.TabIndex = 14;
			this.BtnCollapseCurrent.Text = "-";
			this.toolTip1.SetToolTip(this.BtnCollapseCurrent, "Свернуть дерево");
			this.BtnCollapseCurrent.UseVisualStyleBackColor = true;
			this.BtnCollapseCurrent.Click += new EventHandler(this.BtnCollapseCurrent_Click);
			this.BtnExpandCurrent.Anchor = 9;
			this.BtnExpandCurrent.Location = new Point(141, 1);
			this.BtnExpandCurrent.Name = "BtnExpandCurrent";
			this.BtnExpandCurrent.Size = new Size(31, 21);
			this.BtnExpandCurrent.TabIndex = 13;
			this.BtnExpandCurrent.Text = "+";
			this.toolTip1.SetToolTip(this.BtnExpandCurrent, "Развернуть дерево");
			this.BtnExpandCurrent.UseVisualStyleBackColor = true;
			this.BtnExpandCurrent.Click += new EventHandler(this.BtnExpandCurrent_Click);
			this.TreeCurrent.Dock = 5;
			this.TreeCurrent.Location = new Point(0, 0);
			this.TreeCurrent.Name = "TreeCurrent";
			this.TreeCurrent.Size = new Size(174, 223);
			this.TreeCurrent.TabIndex = 12;
			this.TreeCurrent.ClientSizeChanged += new EventHandler(this.TreeCurrent_ClientSizeChanged);
			this.BtnCollapseSaved.Anchor = 9;
			this.BtnCollapseSaved.Location = new Point(141, 22);
			this.BtnCollapseSaved.Name = "BtnCollapseSaved";
			this.BtnCollapseSaved.Size = new Size(31, 21);
			this.BtnCollapseSaved.TabIndex = 11;
			this.BtnCollapseSaved.Text = "-";
			this.toolTip1.SetToolTip(this.BtnCollapseSaved, "Свернуть дерево");
			this.BtnCollapseSaved.UseVisualStyleBackColor = true;
			this.BtnCollapseSaved.Click += new EventHandler(this.BtnMinus_Click);
			this.BtnExpandSaved.Anchor = 9;
			this.BtnExpandSaved.Location = new Point(141, 1);
			this.BtnExpandSaved.Name = "BtnExpandSaved";
			this.BtnExpandSaved.Size = new Size(31, 21);
			this.BtnExpandSaved.TabIndex = 10;
			this.BtnExpandSaved.Text = "+";
			this.toolTip1.SetToolTip(this.BtnExpandSaved, "Развернуть дерево");
			this.BtnExpandSaved.UseVisualStyleBackColor = true;
			this.BtnExpandSaved.Click += new EventHandler(this.BtnPlus_Click);
			this.TreeSaved.Dock = 5;
			this.TreeSaved.Location = new Point(0, 0);
			this.TreeSaved.Name = "TreeSaved";
			this.TreeSaved.Size = new Size(174, 209);
			this.TreeSaved.TabIndex = 9;
			this.TreeSaved.ClientSizeChanged += new EventHandler(this.TreeSaved_ClientSizeChanged);
			this.groupBox1.Controls.Add(this.BtnUndo);
			this.groupBox1.Controls.Add(this.BtnSave);
			this.groupBox1.Dock = 2;
			this.groupBox1.Location = new Point(0, 209);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(174, 36);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.BtnUndo.Anchor = 2;
			this.BtnUndo.Enabled = false;
			this.BtnUndo.Location = new Point(90, 10);
			this.BtnUndo.Name = "BtnUndo";
			this.BtnUndo.Size = new Size(69, 20);
			this.BtnUndo.TabIndex = 4;
			this.BtnUndo.Text = "Cancel";
			this.toolTip1.SetToolTip(this.BtnUndo, "Откат к сохраненной версии БЗ");
			this.BtnUndo.UseVisualStyleBackColor = true;
			this.BtnUndo.Click += new EventHandler(this.BtnUndo_Click);
			this.BtnSave.Anchor = 2;
			this.BtnSave.Location = new Point(16, 10);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Size = new Size(70, 20);
			this.BtnSave.TabIndex = 3;
			this.BtnSave.Text = "Save";
			this.toolTip1.SetToolTip(this.BtnSave, "Сохранение текущей БЗ");
			this.BtnSave.UseVisualStyleBackColor = true;
			this.BtnSave.Click += new EventHandler(this.BtnSave_Click);
			this.tabControl1.Controls.Add(this.TabDomains);
			this.tabControl1.Controls.Add(this.TabVariables);
			this.tabControl1.Controls.Add(this.TabRules);
			this.tabControl1.Dock = 5;
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(682, 472);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
			this.TabDomains.Controls.Add(this.groupBox2);
			this.TabDomains.Location = new Point(4, 22);
			this.TabDomains.Name = "TabDomains";
			this.TabDomains.Padding = new Padding(3);
			this.TabDomains.Size = new Size(674, 446);
			this.TabDomains.TabIndex = 0;
			this.TabDomains.Text = "Domains";
			this.TabDomains.UseVisualStyleBackColor = true;
			this.groupBox2.Anchor = 15;
			this.groupBox2.Controls.Add(this.BtnDomainPaste);
			this.groupBox2.Controls.Add(this.BtnDomainCopy);
			this.groupBox2.Controls.Add(this.groupBox9);
			this.groupBox2.Controls.Add(this.GridDomains);
			this.groupBox2.Location = new Point(12, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(649, 436);
			this.groupBox2.TabIndex = 51;
			this.groupBox2.TabStop = false;
			this.BtnDomainPaste.Anchor = 10;
			this.BtnDomainPaste.Enabled = false;
			this.BtnDomainPaste.Location = new Point(563, 413);
			this.BtnDomainPaste.Name = "BtnDomainPaste";
			this.BtnDomainPaste.Size = new Size(75, 20);
			this.BtnDomainPaste.TabIndex = 57;
			this.BtnDomainPaste.Text = "Paste";
			this.BtnDomainPaste.UseVisualStyleBackColor = true;
			this.BtnDomainPaste.Click += new EventHandler(this.BtnDomainPaste_Click);
			this.BtnDomainCopy.Anchor = 10;
			this.BtnDomainCopy.Enabled = false;
			this.BtnDomainCopy.Location = new Point(563, 393);
			this.BtnDomainCopy.Name = "BtnDomainCopy";
			this.BtnDomainCopy.Size = new Size(75, 20);
			this.BtnDomainCopy.TabIndex = 56;
			this.BtnDomainCopy.Text = "Copy";
			this.BtnDomainCopy.UseVisualStyleBackColor = true;
			this.BtnDomainCopy.Click += new EventHandler(this.BtnDomainCopy_Click);
			this.groupBox9.Anchor = 2;
			this.groupBox9.Controls.Add(this.BtnDomainChange);
			this.groupBox9.Controls.Add(this.BtnDomainRemove);
			this.groupBox9.Controls.Add(this.BtnDomainAdd);
			this.groupBox9.Location = new Point(82, 391);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new Size(467, 38);
			this.groupBox9.TabIndex = 55;
			this.groupBox9.TabStop = false;
			this.BtnDomainChange.Anchor = 10;
			this.BtnDomainChange.Enabled = false;
			this.BtnDomainChange.Location = new Point(190, 10);
			this.BtnDomainChange.Name = "BtnDomainChange";
			this.BtnDomainChange.Size = new Size(80, 25);
			this.BtnDomainChange.TabIndex = 55;
			this.BtnDomainChange.Text = "Change";
			this.BtnDomainChange.UseVisualStyleBackColor = true;
			this.BtnDomainChange.Click += new EventHandler(this.BtnDomainChange_Click);
			this.BtnDomainRemove.Anchor = 10;
			this.BtnDomainRemove.Enabled = false;
			this.BtnDomainRemove.Location = new Point(326, 10);
			this.BtnDomainRemove.Name = "BtnDomainRemove";
			this.BtnDomainRemove.Size = new Size(80, 25);
			this.BtnDomainRemove.TabIndex = 56;
			this.BtnDomainRemove.Text = "Remove";
			this.toolTip1.SetToolTip(this.BtnDomainRemove, "Удалить текущий домен");
			this.BtnDomainRemove.UseVisualStyleBackColor = true;
			this.BtnDomainRemove.Click += new EventHandler(this.BtnDomainRemove_Click);
			this.BtnDomainAdd.Anchor = 6;
			this.BtnDomainAdd.Location = new Point(60, 10);
			this.BtnDomainAdd.Name = "BtnDomainAdd";
			this.BtnDomainAdd.Size = new Size(80, 25);
			this.BtnDomainAdd.TabIndex = 54;
			this.BtnDomainAdd.Text = "Add";
			this.toolTip1.SetToolTip(this.BtnDomainAdd, "Добавить новый домен");
			this.BtnDomainAdd.UseVisualStyleBackColor = true;
			this.BtnDomainAdd.Click += new EventHandler(this.BtnDomainAdd_Click);
			this.GridDomains.AllowDrop = true;
			this.GridDomains.AllowUserToAddRows = false;
			this.GridDomains.AllowUserToDeleteRows = false;
			this.GridDomains.AllowUserToResizeRows = false;
			this.GridDomains.Anchor = 15;
			this.GridDomains.AutoSizeColumnsMode = 6;
			this.GridDomains.AutoSizeRowsMode = 7;
			this.GridDomains.ColumnHeadersHeightSizeMode = 2;
			this.GridDomains.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column3,
				this.Column1,
				this.Column2
			});
			dataGridViewCellStyle.Alignment = 16;
			dataGridViewCellStyle.BackColor = SystemColors.Window;
			dataGridViewCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, 0, 3, 204);
			dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = 1;
			this.GridDomains.DefaultCellStyle = dataGridViewCellStyle;
			this.GridDomains.Location = new Point(12, 16);
			this.GridDomains.MultiSelect = false;
			this.GridDomains.Name = "GridDomains";
			this.GridDomains.ReadOnly = true;
			this.GridDomains.RowHeadersWidthSizeMode = 1;
			this.GridDomains.SelectionMode = 1;
			this.GridDomains.Size = new Size(626, 375);
			this.GridDomains.TabIndex = 52;
			this.GridDomains.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.GridDomains_CellMouseDoubleClick);
			this.GridDomains.RowsAdded += new DataGridViewRowsAddedEventHandler(this.GridDomains_RowsAdded);
			this.GridDomains.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.GridDomains_RowsRemoved);
			this.GridDomains.DragDrop += new DragEventHandler(this.GridDomains_DragDrop);
			this.GridDomains.KeyDown += new KeyEventHandler(this.GridDomains_KeyDown);
			this.GridDomains.KeyPress += new KeyPressEventHandler(this.GridDomains_KeyPress);
			this.Column3.AutoSizeMode = 6;
			this.Column3.HeaderText = "Number";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			this.Column3.Resizable = 2;
			this.Column3.SortMode = 0;
			this.Column3.Width = 50;
			this.Column1.AutoSizeMode = 6;
			this.Column1.HeaderText = "Name";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.SortMode = 0;
			this.Column1.Width = 41;
			this.Column2.AutoSizeMode = 16;
			this.Column2.HeaderText = "Domain's values";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.SortMode = 0;
			this.TabVariables.Controls.Add(this.groupBox4);
			this.TabVariables.Location = new Point(4, 22);
			this.TabVariables.Name = "TabVariables";
			this.TabVariables.Padding = new Padding(3);
			this.TabVariables.Size = new Size(674, 446);
			this.TabVariables.TabIndex = 1;
			this.TabVariables.Text = "Variables";
			this.TabVariables.UseVisualStyleBackColor = true;
			this.groupBox4.Anchor = 15;
			this.groupBox4.Controls.Add(this.BtnVarPaste);
			this.groupBox4.Controls.Add(this.BtnVarCopy);
			this.groupBox4.Controls.Add(this.groupBox5);
			this.groupBox4.Controls.Add(this.GridVars);
			this.groupBox4.Location = new Point(12, 3);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(649, 436);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.BtnVarPaste.Anchor = 10;
			this.BtnVarPaste.Enabled = false;
			this.BtnVarPaste.Location = new Point(563, 413);
			this.BtnVarPaste.Name = "BtnVarPaste";
			this.BtnVarPaste.Size = new Size(75, 20);
			this.BtnVarPaste.TabIndex = 59;
			this.BtnVarPaste.Text = "Paste";
			this.BtnVarPaste.UseVisualStyleBackColor = true;
			this.BtnVarPaste.Click += new EventHandler(this.BtnVarPaste_Click);
			this.BtnVarCopy.Anchor = 10;
			this.BtnVarCopy.Enabled = false;
			this.BtnVarCopy.Location = new Point(563, 393);
			this.BtnVarCopy.Name = "BtnVarCopy";
			this.BtnVarCopy.Size = new Size(75, 20);
			this.BtnVarCopy.TabIndex = 58;
			this.BtnVarCopy.Text = "Copy";
			this.BtnVarCopy.UseVisualStyleBackColor = true;
			this.BtnVarCopy.Click += new EventHandler(this.BtnVarCopy_Click);
			this.groupBox5.Anchor = 2;
			this.groupBox5.Controls.Add(this.BtnVarChange);
			this.groupBox5.Controls.Add(this.BtnVarRemove);
			this.groupBox5.Controls.Add(this.BtnVarAdd);
			this.groupBox5.Location = new Point(82, 391);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new Size(467, 38);
			this.groupBox5.TabIndex = 54;
			this.groupBox5.TabStop = false;
			this.BtnVarChange.Anchor = 2;
			this.BtnVarChange.Enabled = false;
			this.BtnVarChange.Location = new Point(190, 10);
			this.BtnVarChange.Name = "BtnVarChange";
			this.BtnVarChange.Size = new Size(80, 25);
			this.BtnVarChange.TabIndex = 55;
			this.BtnVarChange.Text = "Change";
			this.toolTip1.SetToolTip(this.BtnVarChange, "Редактировать текущую переменную");
			this.BtnVarChange.UseVisualStyleBackColor = true;
			this.BtnVarChange.Click += new EventHandler(this.BtnVarChange_Click);
			this.BtnVarRemove.Anchor = 10;
			this.BtnVarRemove.Enabled = false;
			this.BtnVarRemove.Location = new Point(326, 10);
			this.BtnVarRemove.Name = "BtnVarRemove";
			this.BtnVarRemove.Size = new Size(80, 25);
			this.BtnVarRemove.TabIndex = 56;
			this.BtnVarRemove.Text = "Remove";
			this.toolTip1.SetToolTip(this.BtnVarRemove, "Удалить текущую переменную");
			this.BtnVarRemove.UseVisualStyleBackColor = true;
			this.BtnVarRemove.Click += new EventHandler(this.BtnVarRemove_Click);
			this.BtnVarAdd.Anchor = 6;
			this.BtnVarAdd.Location = new Point(60, 10);
			this.BtnVarAdd.Name = "BtnVarAdd";
			this.BtnVarAdd.Size = new Size(80, 25);
			this.BtnVarAdd.TabIndex = 54;
			this.BtnVarAdd.Text = "Add";
			this.toolTip1.SetToolTip(this.BtnVarAdd, "Добавить новую переменную");
			this.BtnVarAdd.UseVisualStyleBackColor = true;
			this.BtnVarAdd.Click += new EventHandler(this.BtnVarAdd_Click);
			this.GridVars.AllowDrop = true;
			this.GridVars.AllowUserToAddRows = false;
			this.GridVars.AllowUserToDeleteRows = false;
			this.GridVars.AllowUserToResizeRows = false;
			this.GridVars.Anchor = 15;
			this.GridVars.AutoSizeColumnsMode = 6;
			this.GridVars.AutoSizeRowsMode = 7;
			this.GridVars.ColumnHeadersHeightSizeMode = 2;
			this.GridVars.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column4,
				this.GridVarsName,
				this.GridVarsType,
				this.GridVarsDomain,
				this.GridVarsQuestion,
				this.GridVarsReason
			});
			dataGridViewCellStyle2.Alignment = 16;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, 0, 3, 204);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = 1;
			this.GridVars.DefaultCellStyle = dataGridViewCellStyle2;
			this.GridVars.EditMode = 0;
			this.GridVars.Location = new Point(12, 16);
			this.GridVars.MultiSelect = false;
			this.GridVars.Name = "GridVars";
			this.GridVars.ReadOnly = true;
			this.GridVars.RowHeadersWidthSizeMode = 1;
			this.GridVars.SelectionMode = 1;
			this.GridVars.Size = new Size(626, 375);
			this.GridVars.TabIndex = 52;
			this.GridVars.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.GridVars_CellMouseDoubleClick);
			this.GridVars.RowsAdded += new DataGridViewRowsAddedEventHandler(this.GridVars_RowsAdded);
			this.GridVars.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.GridVars_RowsRemoved);
			this.GridVars.DragDrop += new DragEventHandler(this.GridVars_DragDrop);
			this.GridVars.KeyDown += new KeyEventHandler(this.GridVars_KeyDown);
			this.GridVars.KeyPress += new KeyPressEventHandler(this.GridVars_KeyPress);
			this.Column4.AutoSizeMode = 6;
			this.Column4.HeaderText = "Number";
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			this.Column4.Resizable = 2;
			this.Column4.SortMode = 0;
			this.Column4.Width = 50;
			this.GridVarsName.AutoSizeMode = 16;
			this.GridVarsName.HeaderText = "Name";
			this.GridVarsName.Name = "GridVarsName";
			this.GridVarsName.ReadOnly = true;
			this.GridVarsName.SortMode = 0;
			this.GridVarsType.AutoSizeMode = 6;
			this.GridVarsType.HeaderText = "Type";
			this.GridVarsType.Name = "GridVarsType";
			this.GridVarsType.ReadOnly = true;
			this.GridVarsType.Resizable = 1;
			this.GridVarsType.SortMode = 0;
			this.GridVarsType.Width = 37;
			this.GridVarsDomain.AutoSizeMode = 6;
			this.GridVarsDomain.HeaderText = "Domain";
			this.GridVarsDomain.Name = "GridVarsDomain";
			this.GridVarsDomain.ReadOnly = true;
			this.GridVarsDomain.Resizable = 1;
			this.GridVarsDomain.SortMode = 0;
			this.GridVarsDomain.Width = 49;
			this.GridVarsQuestion.AutoSizeMode = 16;
			this.GridVarsQuestion.HeaderText = "Question";
			this.GridVarsQuestion.Name = "GridVarsQuestion";
			this.GridVarsQuestion.ReadOnly = true;
			this.GridVarsQuestion.SortMode = 0;
			this.GridVarsReason.AutoSizeMode = 16;
			this.GridVarsReason.HeaderText = "Reasoning";
			this.GridVarsReason.Name = "GridVarsReason";
			this.GridVarsReason.ReadOnly = true;
			this.GridVarsReason.SortMode = 0;
			this.TabRules.Controls.Add(this.groupBox6);
			this.TabRules.Location = new Point(4, 22);
			this.TabRules.Name = "TabRules";
			this.TabRules.Padding = new Padding(3);
			this.TabRules.Size = new Size(674, 446);
			this.TabRules.TabIndex = 2;
			this.TabRules.Text = "Rules";
			this.TabRules.UseVisualStyleBackColor = true;
			this.groupBox6.Anchor = 15;
			this.groupBox6.Controls.Add(this.BtnRulePaste);
			this.groupBox6.Controls.Add(this.BtnRuleCopy);
			this.groupBox6.Controls.Add(this.groupBox8);
			this.groupBox6.Controls.Add(this.GridRules);
			this.groupBox6.Location = new Point(12, 3);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new Size(649, 436);
			this.groupBox6.TabIndex = 5;
			this.groupBox6.TabStop = false;
			this.BtnRulePaste.Anchor = 10;
			this.BtnRulePaste.Enabled = false;
			this.BtnRulePaste.Location = new Point(563, 413);
			this.BtnRulePaste.Name = "BtnRulePaste";
			this.BtnRulePaste.Size = new Size(75, 20);
			this.BtnRulePaste.TabIndex = 66;
			this.BtnRulePaste.Text = "Paste";
			this.BtnRulePaste.UseVisualStyleBackColor = true;
			this.BtnRulePaste.Click += new EventHandler(this.BtnRulePaste_Click);
			this.BtnRuleCopy.Anchor = 10;
			this.BtnRuleCopy.Enabled = false;
			this.BtnRuleCopy.Location = new Point(563, 393);
			this.BtnRuleCopy.Name = "BtnRuleCopy";
			this.BtnRuleCopy.Size = new Size(75, 20);
			this.BtnRuleCopy.TabIndex = 65;
			this.BtnRuleCopy.Text = "Copy";
			this.BtnRuleCopy.UseVisualStyleBackColor = true;
			this.BtnRuleCopy.Click += new EventHandler(this.BtnRuleCopy_Click);
			this.groupBox8.Anchor = 2;
			this.groupBox8.Controls.Add(this.BtnRuleChange);
			this.groupBox8.Controls.Add(this.BtnRuleRemove);
			this.groupBox8.Controls.Add(this.BtnRuleAdd);
			this.groupBox8.Location = new Point(82, 391);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new Size(467, 38);
			this.groupBox8.TabIndex = 64;
			this.groupBox8.TabStop = false;
			this.BtnRuleChange.Anchor = 2;
			this.BtnRuleChange.Enabled = false;
			this.BtnRuleChange.Location = new Point(190, 10);
			this.BtnRuleChange.Name = "BtnRuleChange";
			this.BtnRuleChange.Size = new Size(80, 25);
			this.BtnRuleChange.TabIndex = 66;
			this.BtnRuleChange.Text = "Change";
			this.toolTip1.SetToolTip(this.BtnRuleChange, "Редактировать текущее правило");
			this.BtnRuleChange.UseVisualStyleBackColor = true;
			this.BtnRuleChange.Click += new EventHandler(this.BtnRuleChange_Click);
			this.BtnRuleRemove.Anchor = 2;
			this.BtnRuleRemove.Enabled = false;
			this.BtnRuleRemove.Location = new Point(326, 10);
			this.BtnRuleRemove.Name = "BtnRuleRemove";
			this.BtnRuleRemove.Size = new Size(80, 25);
			this.BtnRuleRemove.TabIndex = 67;
			this.BtnRuleRemove.Text = "Remove";
			this.toolTip1.SetToolTip(this.BtnRuleRemove, "Удалить текущее правило");
			this.BtnRuleRemove.UseVisualStyleBackColor = true;
			this.BtnRuleRemove.Click += new EventHandler(this.BtnRuleRemove_Click);
			this.BtnRuleAdd.Anchor = 2;
			this.BtnRuleAdd.Location = new Point(60, 10);
			this.BtnRuleAdd.Name = "BtnRuleAdd";
			this.BtnRuleAdd.Size = new Size(80, 25);
			this.BtnRuleAdd.TabIndex = 65;
			this.BtnRuleAdd.Text = "Add";
			this.toolTip1.SetToolTip(this.BtnRuleAdd, "Добавить новое правило");
			this.BtnRuleAdd.UseVisualStyleBackColor = true;
			this.BtnRuleAdd.Click += new EventHandler(this.BtnRuleAdd_Click);
			this.GridRules.AllowDrop = true;
			this.GridRules.AllowUserToAddRows = false;
			this.GridRules.AllowUserToDeleteRows = false;
			this.GridRules.AllowUserToResizeRows = false;
			this.GridRules.Anchor = 15;
			this.GridRules.AutoSizeColumnsMode = 6;
			this.GridRules.AutoSizeRowsMode = 7;
			this.GridRules.ColumnHeadersHeightSizeMode = 2;
			this.GridRules.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Номер,
				this.GridRulesName,
				this.GridRulePartRule,
				this.GridRulesVar,
				this.GridRulesVarValue
			});
			dataGridViewCellStyle3.Alignment = 16;
			dataGridViewCellStyle3.BackColor = SystemColors.Window;
			dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, 0, 3, 204);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = 1;
			this.GridRules.DefaultCellStyle = dataGridViewCellStyle3;
			this.GridRules.EditMode = 0;
			this.GridRules.Location = new Point(12, 16);
			this.GridRules.MultiSelect = false;
			this.GridRules.Name = "GridRules";
			this.GridRules.ReadOnly = true;
			this.GridRules.RowHeadersWidthSizeMode = 1;
			this.GridRules.SelectionMode = 1;
			this.GridRules.Size = new Size(626, 375);
			this.GridRules.TabIndex = 61;
			this.GridRules.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.GridRules_CellMouseDoubleClick);
			this.GridRules.RowsAdded += new DataGridViewRowsAddedEventHandler(this.GridRules_RowsAdded);
			this.GridRules.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.GridRules_RowsRemoved);
			this.GridRules.DragDrop += new DragEventHandler(this.GridRules_DragDrop);
			this.GridRules.KeyDown += new KeyEventHandler(this.GridRules_KeyDown);
			this.GridRules.KeyPress += new KeyPressEventHandler(this.GridRules_KeyPress);
			this.Номер.AutoSizeMode = 6;
			this.Номер.HeaderText = "Number";
			this.Номер.Name = "Номер";
			this.Номер.ReadOnly = true;
			this.Номер.Resizable = 2;
			this.Номер.SortMode = 0;
			this.Номер.Width = 50;
			this.GridRulesName.AutoSizeMode = 6;
			this.GridRulesName.HeaderText = "Name";
			this.GridRulesName.Name = "GridRulesName";
			this.GridRulesName.ReadOnly = true;
			this.GridRulesName.SortMode = 0;
			this.GridRulesName.Width = 41;
			this.GridRulePartRule.AutoSizeMode = 16;
			this.GridRulePartRule.HeaderText = "Hypothesis";
			this.GridRulePartRule.Name = "GridRulePartRule";
			this.GridRulePartRule.ReadOnly = true;
			this.GridRulePartRule.SortMode = 0;
			this.GridRulesVar.AutoSizeMode = 16;
			this.GridRulesVar.HeaderText = "Conclusion";
			this.GridRulesVar.Name = "GridRulesVar";
			this.GridRulesVar.ReadOnly = true;
			this.GridRulesVar.Resizable = 1;
			this.GridRulesVar.SortMode = 0;
			this.GridRulesVarValue.AutoSizeMode = 16;
			this.GridRulesVarValue.HeaderText = "Reasoning";
			this.GridRulesVarValue.Name = "GridRulesVarValue";
			this.GridRulesVarValue.ReadOnly = true;
			this.GridRulesVarValue.SortMode = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(860, 496);
			base.Controls.Add(this.splitContainer1);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new Size(770, 235);
			base.Name = "MainForm";
			base.StartPosition = 1;
			this.Text = "End User Expert System Shell";
			base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
			base.Load += new EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.TabDomains.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			this.GridDomains.EndInit();
			this.TabVariables.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.GridVars.EndInit();
			this.TabRules.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.GridRules.EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
