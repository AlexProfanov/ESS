using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class RuleForm : Form
	{
		private IContainer components;

		private Panel panel1;

		private Label label1;

		private TextBox textBoxName;

		private SplitContainer splitContainer1;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private Panel panel2;

		private Button buttonOK;

		private Panel panel3;

		private Button buttonRemoveCondition;

		private Button buttonEditCondition;

		private Button buttonAddCondition;

		private Panel panel4;

		private Button buttonRemoveResolution;

		private Button buttonEditResolution;

		private Button buttonAddResolution;

		private Button buttonCancel;

		private ListView listViewCondition;

		private ListView listViewResolution;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ToolTip toolTip;

		private Rule ruleForEditing;

		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

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
			this.panel1 = new Panel();
			this.label1 = new Label();
			this.textBoxName = new TextBox();
			this.splitContainer1 = new SplitContainer();
			this.groupBox1 = new GroupBox();
			this.listViewCondition = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.panel3 = new Panel();
			this.buttonRemoveCondition = new Button();
			this.buttonEditCondition = new Button();
			this.buttonAddCondition = new Button();
			this.groupBox2 = new GroupBox();
			this.listViewResolution = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.panel4 = new Panel();
			this.buttonRemoveResolution = new Button();
			this.buttonEditResolution = new Button();
			this.buttonAddResolution = new Button();
			this.panel2 = new Panel();
			this.buttonCancel = new Button();
			this.buttonOK = new Button();
			this.toolTip = new ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textBoxName);
			this.panel1.Dock = 1;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(396, 53);
			this.panel1.TabIndex = 6;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(74, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Имя правила";
			this.textBoxName.Anchor = 13;
			this.textBoxName.BorderStyle = 1;
			this.textBoxName.Location = new Point(3, 25);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(390, 20);
			this.textBoxName.TabIndex = 4;
			this.textBoxName.Validating += new CancelEventHandler(this.textBoxName_Validating);
			this.splitContainer1.Dock = 5;
			this.splitContainer1.Location = new Point(0, 53);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = 0;
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel1MinSize = 150;
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Panel2MinSize = 150;
			this.splitContainer1.Size = new Size(396, 455);
			this.splitContainer1.SplitterDistance = 194;
			this.splitContainer1.TabIndex = 7;
			this.groupBox1.Controls.Add(this.listViewCondition);
			this.groupBox1.Controls.Add(this.panel3);
			this.groupBox1.Dock = 5;
			this.groupBox1.Location = new Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(396, 194);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Условия правила";
			this.listViewCondition.AllowDrop = true;
			this.listViewCondition.BorderStyle = 1;
			this.listViewCondition.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listViewCondition.Dock = 5;
			this.listViewCondition.ForeColor = Color.DarkBlue;
			this.listViewCondition.FullRowSelect = true;
			this.listViewCondition.GridLines = true;
			this.listViewCondition.HeaderStyle = 1;
			this.listViewCondition.HideSelection = false;
			this.listViewCondition.Location = new Point(3, 16);
			this.listViewCondition.MultiSelect = false;
			this.listViewCondition.Name = "listViewCondition";
			this.listViewCondition.Size = new Size(390, 106);
			this.listViewCondition.TabIndex = 11;
			this.toolTip.SetToolTip(this.listViewCondition, "Drag&Drop - вставка условия на новое место\r\nDrag&Drop + Shift - перестановка условий местами");
			this.listViewCondition.UseCompatibleStateImageBehavior = false;
			this.listViewCondition.View = 1;
			this.listViewCondition.DragDrop += new DragEventHandler(this.listViewCondition_DragDrop);
			this.listViewCondition.DoubleClick += new EventHandler(this.listViewCondition_DoubleClick);
			this.listViewCondition.DragOver += new DragEventHandler(this.listViewCondition_DragOver);
			this.listViewCondition.ItemDrag += new ItemDragEventHandler(this.listViewCondition_ItemDrag);
			this.columnHeader1.Text = "Условие";
			this.columnHeader1.Width = 365;
			this.panel3.Controls.Add(this.buttonRemoveCondition);
			this.panel3.Controls.Add(this.buttonEditCondition);
			this.panel3.Controls.Add(this.buttonAddCondition);
			this.panel3.Dock = 2;
			this.panel3.Location = new Point(3, 122);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(390, 69);
			this.panel3.TabIndex = 10;
			this.buttonRemoveCondition.Anchor = 13;
			this.buttonRemoveCondition.FlatStyle = 0;
			this.buttonRemoveCondition.Location = new Point(2, 46);
			this.buttonRemoveCondition.Name = "buttonRemoveCondition";
			this.buttonRemoveCondition.Size = new Size(388, 23);
			this.buttonRemoveCondition.TabIndex = 11;
			this.buttonRemoveCondition.Text = "Удалить";
			this.buttonRemoveCondition.UseVisualStyleBackColor = true;
			this.buttonRemoveCondition.Click += new EventHandler(this.buttonRemoveCondition_Click);
			this.buttonEditCondition.Anchor = 13;
			this.buttonEditCondition.FlatStyle = 0;
			this.buttonEditCondition.Location = new Point(2, 24);
			this.buttonEditCondition.Name = "buttonEditCondition";
			this.buttonEditCondition.Size = new Size(388, 23);
			this.buttonEditCondition.TabIndex = 10;
			this.buttonEditCondition.Text = "Изменить";
			this.buttonEditCondition.UseVisualStyleBackColor = true;
			this.buttonEditCondition.Click += new EventHandler(this.buttonEditCondition_Click);
			this.buttonAddCondition.Anchor = 13;
			this.buttonAddCondition.FlatStyle = 0;
			this.buttonAddCondition.Location = new Point(2, 2);
			this.buttonAddCondition.Name = "buttonAddCondition";
			this.buttonAddCondition.Size = new Size(388, 23);
			this.buttonAddCondition.TabIndex = 9;
			this.buttonAddCondition.Text = "Добавить";
			this.buttonAddCondition.UseVisualStyleBackColor = true;
			this.buttonAddCondition.Click += new EventHandler(this.buttonAddCondition_Click);
			this.groupBox2.Controls.Add(this.listViewResolution);
			this.groupBox2.Controls.Add(this.panel4);
			this.groupBox2.Dock = 5;
			this.groupBox2.Location = new Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(396, 257);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Заключения правила";
			this.listViewResolution.AllowDrop = true;
			this.listViewResolution.BorderStyle = 1;
			this.listViewResolution.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader2
			});
			this.listViewResolution.Dock = 5;
			this.listViewResolution.ForeColor = Color.DarkBlue;
			this.listViewResolution.FullRowSelect = true;
			this.listViewResolution.GridLines = true;
			this.listViewResolution.HeaderStyle = 1;
			this.listViewResolution.HideSelection = false;
			this.listViewResolution.Location = new Point(3, 16);
			this.listViewResolution.MultiSelect = false;
			this.listViewResolution.Name = "listViewResolution";
			this.listViewResolution.Size = new Size(390, 141);
			this.listViewResolution.TabIndex = 14;
			this.toolTip.SetToolTip(this.listViewResolution, "Drag&Drop - вставка заключения на новое место\r\nDrag&Drop + Shift - перестановка заключений местами\r\n");
			this.listViewResolution.UseCompatibleStateImageBehavior = false;
			this.listViewResolution.View = 1;
			this.listViewResolution.DragDrop += new DragEventHandler(this.listViewResolution_DragDrop);
			this.listViewResolution.DoubleClick += new EventHandler(this.listViewResolution_DoubleClick);
			this.listViewResolution.DragOver += new DragEventHandler(this.listViewResolution_DragOver);
			this.listViewResolution.ItemDrag += new ItemDragEventHandler(this.listViewResolution_ItemDrag);
			this.columnHeader2.Text = "Заключение";
			this.columnHeader2.Width = 365;
			this.panel4.Controls.Add(this.buttonRemoveResolution);
			this.panel4.Controls.Add(this.buttonEditResolution);
			this.panel4.Controls.Add(this.buttonAddResolution);
			this.panel4.Dock = 2;
			this.panel4.Location = new Point(3, 157);
			this.panel4.Name = "panel4";
			this.panel4.Size = new Size(390, 97);
			this.panel4.TabIndex = 13;
			this.buttonRemoveResolution.Anchor = 13;
			this.buttonRemoveResolution.FlatStyle = 0;
			this.buttonRemoveResolution.Location = new Point(2, 47);
			this.buttonRemoveResolution.Name = "buttonRemoveResolution";
			this.buttonRemoveResolution.Size = new Size(388, 23);
			this.buttonRemoveResolution.TabIndex = 14;
			this.buttonRemoveResolution.Text = "Удалить";
			this.buttonRemoveResolution.UseVisualStyleBackColor = true;
			this.buttonRemoveResolution.Click += new EventHandler(this.buttonRemoveResolution_Click);
			this.buttonEditResolution.Anchor = 13;
			this.buttonEditResolution.FlatStyle = 0;
			this.buttonEditResolution.Location = new Point(2, 25);
			this.buttonEditResolution.Name = "buttonEditResolution";
			this.buttonEditResolution.Size = new Size(388, 23);
			this.buttonEditResolution.TabIndex = 13;
			this.buttonEditResolution.Text = "Изменить";
			this.buttonEditResolution.UseVisualStyleBackColor = true;
			this.buttonEditResolution.Click += new EventHandler(this.buttonEditResolution_Click);
			this.buttonAddResolution.Anchor = 13;
			this.buttonAddResolution.FlatStyle = 0;
			this.buttonAddResolution.Location = new Point(2, 3);
			this.buttonAddResolution.Name = "buttonAddResolution";
			this.buttonAddResolution.Size = new Size(388, 23);
			this.buttonAddResolution.TabIndex = 12;
			this.buttonAddResolution.Text = "Добавить";
			this.buttonAddResolution.UseVisualStyleBackColor = true;
			this.buttonAddResolution.Click += new EventHandler(this.buttonAddResolution_Click);
			this.panel2.Controls.Add(this.buttonCancel);
			this.panel2.Controls.Add(this.buttonOK);
			this.panel2.Dock = 2;
			this.panel2.Location = new Point(0, 479);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(396, 29);
			this.panel2.TabIndex = 8;
			this.buttonCancel.Anchor = 9;
			this.buttonCancel.FlatStyle = 0;
			this.buttonCancel.Location = new Point(293, 3);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(100, 23);
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "Отмена";
			this.toolTip.SetToolTip(this.buttonCancel, "Вызов через - Escape");
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			this.buttonOK.Anchor = 9;
			this.buttonOK.FlatStyle = 0;
			this.buttonOK.Location = new Point(187, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(100, 23);
			this.buttonOK.TabIndex = 6;
			this.buttonOK.Text = "OK";
			this.toolTip.SetToolTip(this.buttonOK, "Вызов через - Enter");
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(396, 508);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.splitContainer1);
			base.Controls.Add(this.panel1);
			base.KeyPreview = true;
			this.MinimumSize = new Size(220, 400);
			base.Name = "RuleForm";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			base.KeyDown += new KeyEventHandler(this.RuleForm_KeyDown);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public RuleForm()
		{
			this.InitializeComponent();
		}

		public void createNewRule()
		{
			this.Text = "Добавление правила";
			this.ruleForEditing = new Rule(Global.knowledgeBase.UniqueRuleName);
			this.printRuleForEditing();
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				try
				{
					Global.knowledgeBase.addRule(this.ruleForEditing);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show("Добавление невозможно:\n" + ex.Message, "Ошибка", 0, 16);
				}
			}
		}

		public void editRule(Rule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("item");
			}
			this.Text = "Редактирование правила";
			this.ruleForEditing = new Rule(rule);
			this.printRuleForEditing();
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				rule.makeSameAs(this.ruleForEditing);
			}
		}

		private void printRuleForEditing()
		{
			this.textBoxName.Text = this.ruleForEditing.Name;
			this.printConditionList();
			this.printResolutionList();
		}

		private void printResolutionList()
		{
			int indexToSelect = 0;
			if (this.listViewResolution.SelectedIndices.Count > 0)
			{
				indexToSelect = this.listViewResolution.SelectedIndices[0];
			}
			this.listViewResolution.Items.Clear();
			IEnumerator<Resolution> enumeratorForResolutionList = this.ruleForEditing.getEnumeratorForResolutionList();
			while (enumeratorForResolutionList.MoveNext())
			{
				this.listViewResolution.Items.Add(enumeratorForResolutionList.Current.ToString());
			}
			DialogFuncs.selectListViewItem(this.listViewResolution, indexToSelect);
		}

		private void printConditionList()
		{
			int indexToSelect = 0;
			if (this.listViewCondition.SelectedIndices.Count > 0)
			{
				indexToSelect = this.listViewCondition.SelectedIndices[0];
			}
			this.listViewCondition.Items.Clear();
			IEnumerator<Condition> enumeratorForConditionList = this.ruleForEditing.getEnumeratorForConditionList();
			while (enumeratorForConditionList.MoveNext())
			{
				this.listViewCondition.Items.Add(enumeratorForConditionList.Current.ToString());
			}
			DialogFuncs.selectListViewItem(this.listViewCondition, indexToSelect);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.OK;
			base.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.CANCEL;
			base.Close();
		}

		private void textBoxName_Leave(object sender, EventArgs e)
		{
			string text = this.textBoxName.Text;
			if (!Global.knowledgeBase.containsRuleName(text))
			{
				this.ruleForEditing.Name = text;
				return;
			}
			MessageBox.Show("Имя правила не может быть изменено.\nПравило с таким именем уже есть в базе знаний.", "Ошибка", 0, 16);
		}

		private void buttonAddCondition_Click(object sender, EventArgs e)
		{
			FormAddEditCondition formAddEditCondition = new FormAddEditCondition();
			formAddEditCondition.AddCondition(this.ruleForEditing);
			this.printConditionList();
			DialogFuncs.selectListViewItem(this.listViewCondition, this.listViewCondition.Items.Count - 1);
		}

		private void buttonEditCondition_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewCondition.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int conditionIndex = (int)enumerator.Current;
					FormAddEditCondition formAddEditCondition = new FormAddEditCondition();
					formAddEditCondition.editCondition(this.ruleForEditing.getConditionAt(conditionIndex));
					this.printConditionList();
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

		private void buttonRemoveCondition_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewCondition.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int conditionIndex = (int)enumerator.Current;
					if (MessageBox.Show("Вы действительно хотите удалить условие " + this.ruleForEditing.getConditionAt(conditionIndex).ToString() + " ?", "Внимание", 1, 48) == 1)
					{
						this.ruleForEditing.removeCondition(this.ruleForEditing.getConditionAt(conditionIndex));
						this.printConditionList();
						DialogFuncs.selectListViewItem(this.listViewCondition, 0);
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

		private void buttonAddResolution_Click(object sender, EventArgs e)
		{
			FormAddEditResolution formAddEditResolution = new FormAddEditResolution();
			formAddEditResolution.addResolution(this.ruleForEditing);
			this.printResolutionList();
			DialogFuncs.selectListViewItem(this.listViewResolution, this.listViewResolution.Items.Count - 1);
		}

		private void buttonEditResolution_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewResolution.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int resolutionIndex = (int)enumerator.Current;
					FormAddEditResolution formAddEditResolution = new FormAddEditResolution();
					formAddEditResolution.editResolution(this.ruleForEditing.getResolutionAt(resolutionIndex));
					this.printResolutionList();
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

		private void buttonRemoveResolution_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewResolution.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int resolutionIndex = (int)enumerator.Current;
					if (MessageBox.Show("Вы действительно хотите удалить закдючение " + this.ruleForEditing.getResolutionAt(resolutionIndex).ToString() + " ?", "Внимание", 1, 48) == 1)
					{
						this.ruleForEditing.removeResolution(this.ruleForEditing.getResolutionAt(resolutionIndex));
						this.printResolutionList();
						DialogFuncs.selectListViewItem(this.listViewResolution, 0);
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

		private void textBoxName_Validating(object sender, CancelEventArgs e)
		{
			this.ruleForEditing.Name = this.textBoxName.Text;
		}

		private void listViewCondition_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewCondition.AllowDrop = true;
			this.listViewResolution.AllowDrop = false;
			DialogFuncs.doDragBeginning(this.listViewCondition);
		}

		private void listViewCondition_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewCondition, e);
		}

		private void listViewCondition_DragDrop(object sender, DragEventArgs e)
		{
			ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
			Point point = new Point(e.X, e.Y);
			Point point2 = this.listViewCondition.PointToClient(point);
			ListViewItem itemAt = this.listViewCondition.GetItemAt(point2.X, point2.Y);
			if (itemAt == null)
			{
				return;
			}
			if (e.Effect == 2)
			{
				this.ruleForEditing.insertConditionInto(listViewItem.Index, itemAt.Index);
			}
			else if (e.Effect == 1)
			{
				this.ruleForEditing.switchConditions(listViewItem.Index, itemAt.Index);
			}
			this.printRuleForEditing();
		}

		private void listViewResolution_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewCondition.AllowDrop = false;
			this.listViewResolution.AllowDrop = true;
			DialogFuncs.doDragBeginning(this.listViewResolution);
		}

		private void listViewResolution_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewResolution, e);
		}

		private void listViewResolution_DragDrop(object sender, DragEventArgs e)
		{
			ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
			Point point = new Point(e.X, e.Y);
			Point point2 = this.listViewResolution.PointToClient(point);
			ListViewItem itemAt = this.listViewResolution.GetItemAt(point2.X, point2.Y);
			if (itemAt == null)
			{
				return;
			}
			if (e.Effect == 2)
			{
				this.ruleForEditing.insertResolutionInto(listViewItem.Index, itemAt.Index);
			}
			else if (e.Effect == 1)
			{
				this.ruleForEditing.switchResolutions(listViewItem.Index, itemAt.Index);
			}
			this.printRuleForEditing();
		}

		private void RuleForm_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == 13)
			{
				this.buttonOK_Click(this, new EventArgs());
				return;
			}
			if (keyCode != 27)
			{
				return;
			}
			this.buttonCancel_Click(this, new EventArgs());
		}

		private void listViewCondition_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditCondition_Click(this, new EventArgs());
		}

		private void listViewResolution_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditResolution_Click(this, new EventArgs());
		}
	}
}
