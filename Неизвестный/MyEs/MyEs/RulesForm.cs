using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyEs
{
	public class RulesForm : Form
	{
		public int input;

		public int output;

		public int index;

		public KnowledgeBase KB;

		private int IdRule;

		private IContainer components = null;

		private Button BtnOkContinue;

		private Button BtnCancel;

		private Label label1;

		private TextBox TxtName;

		private TextBox TxtReason;

		private Label label2;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private MyDGV GridIF;

		private Button BtnIfRemove;

		private Button BtnIfChange;

		private Button BtnIfAdd;

		private Button BtnThenRemove;

		private Button BtnThenChange;

		private Button BtnThenAdd;

		private MyDGV GridThen;

		private Button BtnOk;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		public RulesForm()
		{
			this.InitializeComponent();
			this.input = -1;
		}

		private void RulesForm_Load(object sender, EventArgs e)
		{
			if (this.input == -1)
			{
				this.BtnOkContinue.Enabled = true;
				this.BtnOkContinue.Visible = true;
				this.Text = "Creating new rule";
				this.IdRule = this.KB.Rules.AddRule(this.index);
			}
			else
			{
				this.IdRule = this.KB.Rules.CloneRule(this.input);
				Rule ruleById = this.KB.Rules.GetRuleById(this.IdRule);
				this.TxtName.Text = ruleById.Name;
				this.TxtReason.Text = ruleById.Reason;
				this.Text = this.Text + " " + ruleById.Name;
				using (List<RulePair>.Enumerator enumerator = ruleById.IfVars.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RulePair current = enumerator.Current;
						int num = this.GridIF.Rows.Add();
						this.GridIF.Rows[num].Cells[0].set_Value(string.Concat(new string[]
						{
							"'",
							this.KB.Vars.GetVarById(current.Variable).Name,
							"'='",
							this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current.Variable).domain, current.Value),
							"'"
						}));
					}
				}
				using (List<RulePair>.Enumerator enumerator = ruleById.ThenVars.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RulePair current = enumerator.Current;
						int num = this.GridThen.Rows.Add();
						this.GridThen.Rows[num].Cells[0].set_Value(string.Concat(new string[]
						{
							"'",
							this.KB.Vars.GetVarById(current.Variable).Name,
							"'='",
							this.KB.Domains.GetDomainValueNameById(this.KB.Vars.GetVarById(current.Variable).domain, current.Value),
							"'"
						}));
					}
				}
				this.BtnIfChange.Enabled = true;
				this.BtnIfRemove.Enabled = true;
				this.BtnThenChange.Enabled = true;
				this.BtnThenRemove.Enabled = true;
			}
		}

		private void RulesForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == 2)
			{
				this.KB.Rules.RemoveRule(this.IdRule);
			}
		}

		private bool TestRule()
		{
			bool result;
			if (this.TxtName.Text.Trim() == "")
			{
				MessageBox.Show("У правила должно быть имя");
				this.TxtName.Select();
				result = false;
			}
			else
			{
				int idRuleByName = this.KB.Rules.GetIdRuleByName(this.TxtName.Text);
				if (this.input == -1 && idRuleByName > -1)
				{
					MessageBox.Show("Правило с таким именем уже есть");
					this.TxtName.Select();
					result = false;
				}
				else if (this.input > -1 && idRuleByName > -1 && idRuleByName != this.IdRule && idRuleByName != this.input)
				{
					MessageBox.Show("Правило с таким именем уже есть");
					this.TxtName.Select();
					result = false;
				}
				else if (this.GridIF.RowCount == 0)
				{
					MessageBox.Show("В посылке должны быть факты");
					result = false;
				}
				else if (this.GridThen.RowCount == 0)
				{
					MessageBox.Show("В заключении должны быть факты");
					result = false;
				}
				else
				{
					List<RulePair> thenVars = this.KB.Rules.GetRuleById(this.IdRule).ThenVars;
					using (List<RulePair>.Enumerator enumerator = thenVars.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							RulePair current = enumerator.Current;
							if (this.KB.Rules.GetVarsInRule(this.IdRule).Contains(current.Variable))
							{
								MessageBox.Show("Переменная и тут и там");
								result = false;
								return result;
							}
						}
					}
					result = true;
				}
			}
			return result;
		}

		private void BtnOkContinue_Click(object sender, EventArgs e)
		{
			if (this.TestRule())
			{
				this.KB.Rules.ChangeRuleName(this.IdRule, this.TxtName.Text);
				this.KB.Rules.ChangeRuleReason(this.IdRule, this.TxtReason.Text);
				this.output = this.IdRule;
				base.DialogResult = 4;
				base.Close();
			}
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			if (this.TestRule())
			{
				if (this.input == -1)
				{
					this.KB.Rules.ChangeRuleName(this.IdRule, this.TxtName.Text);
					this.KB.Rules.ChangeRuleReason(this.IdRule, this.TxtReason.Text);
					this.output = this.IdRule;
				}
				else
				{
					this.KB.Rules.RemoveRule(this.input);
					this.KB.Rules.ChangeRuleName(this.IdRule, this.TxtName.Text);
					this.KB.Rules.ChangeRuleReason(this.IdRule, this.TxtReason.Text);
					this.KB.Rules.PasteRule(this.index, this.input, (Rule)this.KB.Rules.Rules[this.IdRule].Clone());
					this.KB.Rules.RemoveRule(this.IdRule);
				}
				base.DialogResult = 1;
				base.Close();
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void BtnIfAdd_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.GridIF.CurrentRow != null)
			{
				num = this.GridIF.CurrentRow.Index + 1;
			}
			DialogResult dialogResult;
			do
			{
				ChangeFact changeFact = new ChangeFact();
				changeFact.index = num;
				changeFact.IdRule = this.IdRule;
				changeFact.mode = 0;
				changeFact.KB = this.KB;
				changeFact.MForm = (MainForm)base.Owner;
				dialogResult = changeFact.ShowDialog(this);
				if (dialogResult != 2)
				{
					this.GridIF.Rows.Insert(num, 1);
					this.GridIF[0, num].Value = changeFact.output;
					this.GridIF.CurrentCell = this.GridIF[0, num];
				}
				num++;
			}
			while (dialogResult == 4);
		}

		private void BtnIfChange_Click(object sender, EventArgs e)
		{
			ChangeFact changeFact = new ChangeFact();
			changeFact.index = this.GridIF.CurrentRow.Index;
			changeFact.IdRule = this.IdRule;
			changeFact.mode = 2;
			changeFact.KB = this.KB;
			changeFact.MForm = (MainForm)base.Owner;
			changeFact.ShowDialog(this);
			if (changeFact.DialogResult == 1)
			{
				this.GridIF.CurrentRow.Cells[0].Value = changeFact.output;
			}
		}

		private void BtnIfRemove_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вы уверены, что хотите удалить факт" + this.GridIF.CurrentRow.Cells[0].Value.ToString() + "?", "", 4) == 6)
			{
				this.KB.Rules.RemoveIfFact(this.IdRule, this.GridIF.CurrentRow.Index);
				this.GridIF.Rows.Remove(this.GridIF.CurrentRow);
			}
		}

		private void GridIF_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			this.BtnIfChange.Enabled = true;
			this.BtnIfRemove.Enabled = true;
		}

		private void GridIF_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (this.GridIF.RowCount == 0)
			{
				this.BtnIfChange.Enabled = false;
				this.BtnIfRemove.Enabled = false;
			}
		}

		private void GridIF_DragDrop(object sender, DragEventArgs e)
		{
			int dragDropSourceIndex = this.GridIF.DragDropSourceIndex;
			int dragDropTargetIndex = this.GridIF.DragDropTargetIndex;
			if (dragDropTargetIndex > -1)
			{
				if (dragDropSourceIndex > -1)
				{
					this.KB.Rules.ReplaceIfFacts(this.IdRule, dragDropSourceIndex, dragDropTargetIndex);
					this.GridIF.CurrentCell = this.GridIF.Rows[dragDropTargetIndex].Cells[0];
				}
				else
				{
					this.KB.Rules.RemoveThenFact(this.IdRule, this.GridThen.CurrentRow.Index);
					string text = this.GridThen.CurrentRow.Cells[0].Value.ToString();
					this.GridThen.Rows.Remove(this.GridThen.CurrentRow);
					this.KB.Rules.AddIfFact(this.IdRule, dragDropTargetIndex);
					this.KB.Rules.ChangeIfFact(this.IdRule, dragDropTargetIndex, text.Split(new char[]
					{
						'='
					})[0].Substring(1, text.Split(new char[]
					{
						'='
					})[0].Length - 2), text.Split(new char[]
					{
						'='
					})[1].Substring(1, text.Split(new char[]
					{
						'='
					})[1].Length - 2));
					this.GridIF.Rows.Insert(dragDropTargetIndex, 1);
					this.GridIF[0, dragDropTargetIndex].Value = text;
					this.GridIF.CurrentCell = this.GridIF.Rows[dragDropTargetIndex].Cells[0];
				}
			}
		}

		private void GridIF_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (this.GridIF.CurrentRow != null)
			{
				this.BtnIfChange_Click(sender, new EventArgs());
			}
		}

		private void BtnThenAdd_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.GridThen.CurrentRow != null)
			{
				num = this.GridThen.CurrentRow.Index + 1;
			}
			DialogResult dialogResult;
			do
			{
				ChangeFact changeFact = new ChangeFact();
				changeFact.index = num;
				changeFact.IdRule = this.IdRule;
				changeFact.mode = 1;
				changeFact.KB = this.KB;
				changeFact.MForm = (MainForm)base.Owner;
				dialogResult = changeFact.ShowDialog(this);
				if (dialogResult != 2)
				{
					this.GridThen.Rows.Insert(num, 1);
					this.GridThen[0, num].Value = changeFact.output;
					this.GridThen.CurrentCell = this.GridThen[0, num];
				}
				num++;
			}
			while (dialogResult == 4);
		}

		private void BtnThenChange_Click(object sender, EventArgs e)
		{
			ChangeFact changeFact = new ChangeFact();
			changeFact.index = this.GridThen.CurrentRow.Index;
			changeFact.IdRule = this.IdRule;
			changeFact.mode = 3;
			changeFact.KB = this.KB;
			changeFact.MForm = (MainForm)base.Owner;
			changeFact.ShowDialog(this);
			if (changeFact.DialogResult == 1)
			{
				this.GridThen.CurrentRow.Cells[0].Value = changeFact.output;
			}
		}

		private void BtnThenRemove_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вы уверены, что хотите удалить факт" + this.GridThen.CurrentRow.Cells[0].Value.ToString() + "?", "", 4) == 6)
			{
				this.KB.Rules.RemoveThenFact(this.IdRule, this.GridThen.CurrentRow.Index);
				this.GridThen.Rows.Remove(this.GridThen.CurrentRow);
			}
		}

		private void GridThen_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			this.BtnThenChange.Enabled = true;
			this.BtnThenRemove.Enabled = true;
		}

		private void GridThen_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (this.GridThen.RowCount == 0)
			{
				this.BtnThenChange.Enabled = false;
				this.BtnThenRemove.Enabled = false;
			}
		}

		private void GridThen_DragDrop(object sender, DragEventArgs e)
		{
			int dragDropSourceIndex = this.GridThen.DragDropSourceIndex;
			int dragDropTargetIndex = this.GridThen.DragDropTargetIndex;
			if (dragDropTargetIndex > -1)
			{
				if (dragDropSourceIndex > -1)
				{
					this.KB.Rules.ReplaceThenFacts(this.IdRule, dragDropSourceIndex, dragDropTargetIndex);
					this.GridThen.CurrentCell = this.GridThen.Rows[dragDropTargetIndex].Cells[0];
				}
				else
				{
					this.KB.Rules.RemoveIfFact(this.IdRule, this.GridIF.CurrentRow.Index);
					string text = this.GridIF.CurrentRow.Cells[0].Value.ToString();
					this.GridIF.Rows.Remove(this.GridIF.CurrentRow);
					this.KB.Rules.AddThenFact(this.IdRule, dragDropTargetIndex);
					this.KB.Rules.ChangeThenFact(this.IdRule, dragDropTargetIndex, text.Split(new char[]
					{
						'='
					})[0].Substring(1, text.Split(new char[]
					{
						'='
					})[0].Length - 2), text.Split(new char[]
					{
						'='
					})[1].Substring(1, text.Split(new char[]
					{
						'='
					})[1].Length - 2));
					this.GridThen.Rows.Insert(dragDropTargetIndex, 1);
					this.GridThen[0, dragDropTargetIndex].Value = text;
					this.GridThen.CurrentCell = this.GridThen.Rows[dragDropTargetIndex].Cells[0];
				}
			}
		}

		private void GridThen_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (this.GridThen.CurrentRow != null)
			{
				this.BtnThenChange_Click(sender, new EventArgs());
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.BtnOkContinue = new Button();
			this.BtnCancel = new Button();
			this.label1 = new Label();
			this.TxtName = new TextBox();
			this.TxtReason = new TextBox();
			this.label2 = new Label();
			this.groupBox1 = new GroupBox();
			this.BtnIfRemove = new Button();
			this.BtnIfChange = new Button();
			this.BtnIfAdd = new Button();
			this.GridIF = new MyDGV();
			this.groupBox2 = new GroupBox();
			this.GridThen = new MyDGV();
			this.BtnThenRemove = new Button();
			this.BtnThenChange = new Button();
			this.BtnThenAdd = new Button();
			this.BtnOk = new Button();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.groupBox1.SuspendLayout();
			this.GridIF.BeginInit();
			this.groupBox2.SuspendLayout();
			this.GridThen.BeginInit();
			base.SuspendLayout();
			this.BtnOkContinue.Enabled = false;
			this.BtnOkContinue.Location = new Point(243, 359);
			this.BtnOkContinue.Name = "BtnOkContinue";
			this.BtnOkContinue.Size = new Size(152, 23);
			this.BtnOkContinue.TabIndex = 8;
			this.BtnOkContinue.Text = "Apply and continue";
			this.BtnOkContinue.UseVisualStyleBackColor = true;
			this.BtnOkContinue.Visible = false;
			this.BtnOkContinue.Click += new EventHandler(this.BtnOkContinue_Click);
			this.BtnCancel.DialogResult = 2;
			this.BtnCancel.Location = new Point(558, 359);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new Size(75, 23);
			this.BtnCancel.TabIndex = 10;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new EventHandler(this.BtnCancel_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(68, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Rule's name:";
			this.TxtName.Location = new Point(103, 10);
			this.TxtName.Name = "TxtName";
			this.TxtName.Size = new Size(383, 20);
			this.TxtName.TabIndex = 0;
			this.TxtReason.Location = new Point(103, 36);
			this.TxtReason.Name = "TxtReason";
			this.TxtReason.Size = new Size(383, 20);
			this.TxtReason.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(13, 39);
			this.label2.Name = "label2";
			this.label2.Size = new Size(61, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Reasoning:";
			this.groupBox1.Controls.Add(this.BtnIfRemove);
			this.groupBox1.Controls.Add(this.BtnIfChange);
			this.groupBox1.Controls.Add(this.BtnIfAdd);
			this.groupBox1.Controls.Add(this.GridIF);
			this.groupBox1.Location = new Point(16, 62);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(297, 278);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Hypothesis facts";
			this.BtnIfRemove.Enabled = false;
			this.BtnIfRemove.Location = new Point(208, 245);
			this.BtnIfRemove.Name = "BtnIfRemove";
			this.BtnIfRemove.Size = new Size(75, 23);
			this.BtnIfRemove.TabIndex = 4;
			this.BtnIfRemove.Text = "Remove";
			this.BtnIfRemove.UseVisualStyleBackColor = true;
			this.BtnIfRemove.Click += new EventHandler(this.BtnIfRemove_Click);
			this.BtnIfChange.Enabled = false;
			this.BtnIfChange.Location = new Point(114, 245);
			this.BtnIfChange.Name = "BtnIfChange";
			this.BtnIfChange.Size = new Size(75, 23);
			this.BtnIfChange.TabIndex = 3;
			this.BtnIfChange.Text = "Change";
			this.BtnIfChange.UseVisualStyleBackColor = true;
			this.BtnIfChange.Click += new EventHandler(this.BtnIfChange_Click);
			this.BtnIfAdd.Location = new Point(17, 245);
			this.BtnIfAdd.Name = "BtnIfAdd";
			this.BtnIfAdd.Size = new Size(75, 23);
			this.BtnIfAdd.TabIndex = 2;
			this.BtnIfAdd.Text = "Add";
			this.BtnIfAdd.UseVisualStyleBackColor = true;
			this.BtnIfAdd.Click += new EventHandler(this.BtnIfAdd_Click);
			this.GridIF.AllowDrop = true;
			this.GridIF.AllowUserToAddRows = false;
			this.GridIF.AllowUserToDeleteRows = false;
			this.GridIF.AllowUserToResizeRows = false;
			this.GridIF.ColumnHeadersHeightSizeMode = 2;
			this.GridIF.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column1
			});
			this.GridIF.Location = new Point(17, 20);
			this.GridIF.MultiSelect = false;
			this.GridIF.Name = "GridIF";
			this.GridIF.ReadOnly = true;
			this.GridIF.RowHeadersWidthSizeMode = 1;
			this.GridIF.SelectionMode = 1;
			this.GridIF.Size = new Size(266, 214);
			this.GridIF.TabIndex = 0;
			this.GridIF.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.GridIF_CellMouseDoubleClick);
			this.GridIF.RowsAdded += new DataGridViewRowsAddedEventHandler(this.GridIF_RowsAdded);
			this.GridIF.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.GridIF_RowsRemoved);
			this.GridIF.DragDrop += new DragEventHandler(this.GridIF_DragDrop);
			this.groupBox2.Controls.Add(this.GridThen);
			this.groupBox2.Controls.Add(this.BtnThenRemove);
			this.groupBox2.Controls.Add(this.BtnThenChange);
			this.groupBox2.Controls.Add(this.BtnThenAdd);
			this.groupBox2.Location = new Point(336, 62);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(297, 278);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Conclusion facts";
			this.GridThen.AllowDrop = true;
			this.GridThen.AllowUserToAddRows = false;
			this.GridThen.AllowUserToDeleteRows = false;
			this.GridThen.AllowUserToResizeRows = false;
			this.GridThen.ColumnHeadersHeightSizeMode = 2;
			this.GridThen.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1
			});
			this.GridThen.Location = new Point(17, 19);
			this.GridThen.MultiSelect = false;
			this.GridThen.Name = "GridThen";
			this.GridThen.ReadOnly = true;
			this.GridThen.RowHeadersWidthSizeMode = 1;
			this.GridThen.SelectionMode = 1;
			this.GridThen.Size = new Size(266, 214);
			this.GridThen.TabIndex = 21;
			this.GridThen.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.GridThen_CellMouseDoubleClick);
			this.GridThen.RowsAdded += new DataGridViewRowsAddedEventHandler(this.GridThen_RowsAdded);
			this.GridThen.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.GridThen_RowsRemoved);
			this.GridThen.DragDrop += new DragEventHandler(this.GridThen_DragDrop);
			this.BtnThenRemove.Enabled = false;
			this.BtnThenRemove.Location = new Point(208, 245);
			this.BtnThenRemove.Name = "BtnThenRemove";
			this.BtnThenRemove.Size = new Size(75, 23);
			this.BtnThenRemove.TabIndex = 7;
			this.BtnThenRemove.Text = "Remove";
			this.BtnThenRemove.UseVisualStyleBackColor = true;
			this.BtnThenRemove.Click += new EventHandler(this.BtnThenRemove_Click);
			this.BtnThenChange.Enabled = false;
			this.BtnThenChange.Location = new Point(114, 245);
			this.BtnThenChange.Name = "BtnThenChange";
			this.BtnThenChange.Size = new Size(75, 23);
			this.BtnThenChange.TabIndex = 6;
			this.BtnThenChange.Text = "Change";
			this.BtnThenChange.UseVisualStyleBackColor = true;
			this.BtnThenChange.Click += new EventHandler(this.BtnThenChange_Click);
			this.BtnThenAdd.Location = new Point(17, 245);
			this.BtnThenAdd.Name = "BtnThenAdd";
			this.BtnThenAdd.Size = new Size(75, 23);
			this.BtnThenAdd.TabIndex = 5;
			this.BtnThenAdd.Text = "Add";
			this.BtnThenAdd.UseVisualStyleBackColor = true;
			this.BtnThenAdd.Click += new EventHandler(this.BtnThenAdd_Click);
			this.BtnOk.Location = new Point(408, 359);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new Size(139, 23);
			this.BtnOk.TabIndex = 9;
			this.BtnOk.Text = "Apply and exit";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new EventHandler(this.BtnOk_Click);
			this.Column1.AutoSizeMode = 16;
			this.Column1.HeaderText = "Fact";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.SortMode = 0;
			this.dataGridViewTextBoxColumn1.AutoSizeMode = 16;
			dataGridViewCellStyle.WrapMode = 1;
			this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle;
			this.dataGridViewTextBoxColumn1.HeaderText = "Fact";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.SortMode = 0;
			base.AcceptButton = this.BtnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.CancelButton = this.BtnCancel;
			base.ClientSize = new Size(650, 394);
			base.Controls.Add(this.BtnOk);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.TxtReason);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.TxtName);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.BtnOkContinue);
			base.Controls.Add(this.BtnCancel);
			base.FormBorderStyle = 2;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RulesForm";
			base.StartPosition = 4;
			this.Text = "Rule editing";
			base.FormClosing += new FormClosingEventHandler(this.RulesForm_FormClosing);
			base.Load += new EventHandler(this.RulesForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.GridIF.EndInit();
			this.groupBox2.ResumeLayout(false);
			this.GridThen.EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
