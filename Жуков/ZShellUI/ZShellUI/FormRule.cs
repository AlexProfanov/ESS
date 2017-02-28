using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZShellCore;

namespace ZShellUI
{
	public class FormRule : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private Rule ruleForEditing;

		private IContainer components = null;

		private Panel panel1;

		private Label label1;

		private TextBox textBoxName;

		private SplitContainer splitContainer1;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private Button buttonOK;

		private Button buttonRemoveCondition;

		private Button buttonEditCondition;

		private Button buttonAddCondition;

		private Button buttonCancel;

		private ListView listViewCondition;

		private ListView listViewResolution;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ToolTip toolTip;

		private Button buttonRemoveResolution;

		private Button buttonEditResolution;

		private Button buttonAddResolution;

		private Button buttonAddVar;

		public FormRule()
		{
			this.InitializeComponent();
		}

		public void createNewRule(int indexToInsert = -1)
		{
			this.Text = "Добавление правила";
			this.ruleForEditing = new Rule(Global.knowledgeBase.UniqueRuleName);
			this.printRuleForEditing();
			base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				try
				{
					Global.knowledgeBase.addRule(this.ruleForEditing);
					if (indexToInsert != -1)
					{
						Global.knowledgeBase.insertRuleInto(Global.knowledgeBase.getRuleCount() - 1, indexToInsert);
					}
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show("Добавление невозможно:\n" + ex.Message, "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
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
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
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
			}
			else
			{
				MessageBox.Show("Имя правила не может быть изменено.\nПравило с таким именем уже есть в базе знаний.", "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
			}
		}

		private void buttonAddCondition_Click(object sender, EventArgs e)
		{
			new FormAddEditCondition().AddCondition(this.ruleForEditing);
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
					new FormAddEditCondition().editCondition(this.ruleForEditing.getConditionAt(conditionIndex));
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
					if (MessageBox.Show("Вы действительно хотите удалить условие " + this.ruleForEditing.getConditionAt(conditionIndex).ToString() + " ?", "Внимание", (System.Windows.Forms.MessageBoxButtons)1, (System.Windows.Forms.MessageBoxIcon)48) == (DialogResult)1)
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
			new FormAddEditResolution().addResolution(this.ruleForEditing);
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
					new FormAddEditResolution().editResolution(this.ruleForEditing.getResolutionAt(resolutionIndex));
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
					if (MessageBox.Show("Вы действительно хотите удалить закдючение " + this.ruleForEditing.getResolutionAt(resolutionIndex).ToString() + " ?", "Внимание", (System.Windows.Forms.MessageBoxButtons)1, (System.Windows.Forms.MessageBoxIcon)48) == (DialogResult)1)
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
			if (MessageBox.Show("Вы действительно хотите переместить элемент ?", "Внимание", (System.Windows.Forms.MessageBoxButtons)4, (System.Windows.Forms.MessageBoxIcon)48) == (DialogResult)6)
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = this.listViewCondition.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewCondition.GetItemAt(point.X, point.Y);
				if (itemAt != null)
				{
					if (e.Effect == (DragDropEffects)2)
					{
						this.ruleForEditing.insertConditionInto(listViewItem.Index, itemAt.Index);
					}
					this.printRuleForEditing();
				}
			}
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
			if (MessageBox.Show("Вы действительно хотите переместить элемент ?", "Внимание", (System.Windows.Forms.MessageBoxButtons)4, (System.Windows.Forms.MessageBoxIcon)48) == (DialogResult)6)
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = this.listViewResolution.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewResolution.GetItemAt(point.X, point.Y);
				if (itemAt != null)
				{
					if (e.Effect == (DragDropEffects)2)
					{
						this.ruleForEditing.insertResolutionInto(listViewItem.Index, itemAt.Index);
					}
					this.printRuleForEditing();
				}
			}
		}

		private void RuleForm_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != (Keys)13)
			{
				if (keyCode == (Keys)27)
				{
					this.buttonCancel_Click(this, new EventArgs());
				}
			}
			else
			{
				this.buttonOK_Click(this, new EventArgs());
			}
		}

		private void listViewCondition_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditCondition_Click(this, new EventArgs());
		}

		private void listViewResolution_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditResolution_Click(this, new EventArgs());
		}

		private void buttonAddVar_Click(object sender, EventArgs e)
		{
			new FormAddEditVariable().addVariable(-1);
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
			this.panel1 = new Panel();
			this.label1 = new Label();
			this.textBoxName = new TextBox();
			this.splitContainer1 = new SplitContainer();
			this.groupBox1 = new GroupBox();
			this.buttonRemoveCondition = new Button();
			this.listViewCondition = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.buttonEditCondition = new Button();
			this.buttonAddCondition = new Button();
			this.groupBox2 = new GroupBox();
			this.buttonCancel = new Button();
			this.buttonRemoveResolution = new Button();
			this.buttonOK = new Button();
			this.listViewResolution = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.buttonEditResolution = new Button();
			this.buttonAddResolution = new Button();
			this.buttonAddVar = new Button();
			this.panel1.SuspendLayout();
			this.splitContainer1.BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textBoxName);
			this.panel1.Dock = (System.Windows.Forms.DockStyle)1;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(399, 53);
			this.panel1.TabIndex = 6;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(74, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Имя правила";
			this.textBoxName.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.textBoxName.Location = new Point(3, 25);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(393, 20);
			this.textBoxName.TabIndex = 4;
			this.textBoxName.Validating += new CancelEventHandler(this.textBoxName_Validating);
			this.splitContainer1.Dock = (System.Windows.Forms.DockStyle)5;
			this.splitContainer1.Location = new Point(0, 53);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = 0;
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel1MinSize = 250;
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Panel2MinSize = 150;
			this.splitContainer1.Size = new Size(399, 459);
			this.splitContainer1.SplitterDistance = 250;
			this.splitContainer1.TabIndex = 7;
			this.groupBox1.Controls.Add(this.buttonAddVar);
			this.groupBox1.Controls.Add(this.buttonRemoveCondition);
			this.groupBox1.Controls.Add(this.listViewCondition);
			this.groupBox1.Controls.Add(this.buttonEditCondition);
			this.groupBox1.Controls.Add(this.buttonAddCondition);
			this.groupBox1.Dock = (System.Windows.Forms.DockStyle)5;
			this.groupBox1.Location = new Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(399, 250);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Условия правила";
			this.buttonRemoveCondition.Anchor = (System.Windows.Forms.AnchorStyles)9;
			this.buttonRemoveCondition.Location = new Point(293, 74);
			this.buttonRemoveCondition.Name = "buttonRemoveCondition";
			this.buttonRemoveCondition.Size = new Size(100, 23);
			this.buttonRemoveCondition.TabIndex = 11;
			this.buttonRemoveCondition.Text = "Удалить";
			this.buttonRemoveCondition.UseVisualStyleBackColor = true;
			this.buttonRemoveCondition.Click += new EventHandler(this.buttonRemoveCondition_Click);
			this.listViewCondition.AllowDrop = true;
			this.listViewCondition.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.listViewCondition.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listViewCondition.ForeColor = Color.DarkBlue;
			this.listViewCondition.FullRowSelect = true;
			this.listViewCondition.GridLines = true;
			this.listViewCondition.HeaderStyle = (System.Windows.Forms.ColumnHeaderStyle)1;
			this.listViewCondition.HideSelection = false;
			this.listViewCondition.Location = new Point(3, 16);
			this.listViewCondition.MultiSelect = false;
			this.listViewCondition.Name = "listViewCondition";
			this.listViewCondition.Size = new Size(284, 232);
			this.listViewCondition.TabIndex = 11;
			this.listViewCondition.UseCompatibleStateImageBehavior = false;
			this.listViewCondition.View = (System.Windows.Forms.View)1;
			this.listViewCondition.ItemDrag += new ItemDragEventHandler(this.listViewCondition_ItemDrag);
			this.listViewCondition.DragDrop += new DragEventHandler(this.listViewCondition_DragDrop);
			this.listViewCondition.DragOver += new DragEventHandler(this.listViewCondition_DragOver);
			this.listViewCondition.DoubleClick += new EventHandler(this.listViewCondition_DoubleClick);
			this.columnHeader1.Text = "Условие";
			this.columnHeader1.Width = 365;
			this.buttonEditCondition.Anchor = (System.Windows.Forms.AnchorStyles)9;
			this.buttonEditCondition.Location = new Point(293, 45);
			this.buttonEditCondition.Name = "buttonEditCondition";
			this.buttonEditCondition.Size = new Size(100, 23);
			this.buttonEditCondition.TabIndex = 10;
			this.buttonEditCondition.Text = "Изменить";
			this.buttonEditCondition.UseVisualStyleBackColor = true;
			this.buttonEditCondition.Click += new EventHandler(this.buttonEditCondition_Click);
			this.buttonAddCondition.Anchor = (System.Windows.Forms.AnchorStyles)9;
			this.buttonAddCondition.Location = new Point(293, 16);
			this.buttonAddCondition.Name = "buttonAddCondition";
			this.buttonAddCondition.Size = new Size(100, 23);
			this.buttonAddCondition.TabIndex = 9;
			this.buttonAddCondition.Text = "Добавить";
			this.buttonAddCondition.UseVisualStyleBackColor = true;
			this.buttonAddCondition.Click += new EventHandler(this.buttonAddCondition_Click);
			this.groupBox2.Controls.Add(this.buttonCancel);
			this.groupBox2.Controls.Add(this.buttonRemoveResolution);
			this.groupBox2.Controls.Add(this.buttonOK);
			this.groupBox2.Controls.Add(this.listViewResolution);
			this.groupBox2.Controls.Add(this.buttonEditResolution);
			this.groupBox2.Controls.Add(this.buttonAddResolution);
			this.groupBox2.Dock = (System.Windows.Forms.DockStyle)5;
			this.groupBox2.Location = new Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(399, 205);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Заключения правила";
			this.buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles)9;
			this.buttonCancel.Location = new Point(292, 172);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(101, 23);
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			this.buttonRemoveResolution.Anchor = (System.Windows.Forms.AnchorStyles)9;
			this.buttonRemoveResolution.Location = new Point(293, 74);
			this.buttonRemoveResolution.Name = "buttonRemoveResolution";
			this.buttonRemoveResolution.Size = new Size(101, 23);
			this.buttonRemoveResolution.TabIndex = 14;
			this.buttonRemoveResolution.Text = "Удалить";
			this.buttonRemoveResolution.UseVisualStyleBackColor = true;
			this.buttonRemoveResolution.Click += new EventHandler(this.buttonRemoveResolution_Click);
			this.buttonOK.Anchor = (System.Windows.Forms.AnchorStyles)9;
			this.buttonOK.Location = new Point(292, 143);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(100, 23);
			this.buttonOK.TabIndex = 6;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.listViewResolution.AllowDrop = true;
			this.listViewResolution.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.listViewResolution.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader2
			});
			this.listViewResolution.ForeColor = Color.DarkBlue;
			this.listViewResolution.FullRowSelect = true;
			this.listViewResolution.GridLines = true;
			this.listViewResolution.HeaderStyle = (System.Windows.Forms.ColumnHeaderStyle)1;
			this.listViewResolution.HideSelection = false;
			this.listViewResolution.Location = new Point(3, 16);
			this.listViewResolution.MultiSelect = false;
			this.listViewResolution.Name = "listViewResolution";
			this.listViewResolution.Size = new Size(283, 186);
			this.listViewResolution.TabIndex = 14;
			this.listViewResolution.UseCompatibleStateImageBehavior = false;
			this.listViewResolution.View = (System.Windows.Forms.View)1;
			this.listViewResolution.ItemDrag += new ItemDragEventHandler(this.listViewResolution_ItemDrag);
			this.listViewResolution.DragDrop += new DragEventHandler(this.listViewResolution_DragDrop);
			this.listViewResolution.DragOver += new DragEventHandler(this.listViewResolution_DragOver);
			this.listViewResolution.DoubleClick += new EventHandler(this.listViewResolution_DoubleClick);
			this.columnHeader2.Text = "Заключение";
			this.columnHeader2.Width = 365;
			this.buttonEditResolution.Anchor = (System.Windows.Forms.AnchorStyles)9;
			this.buttonEditResolution.Location = new Point(292, 45);
			this.buttonEditResolution.Name = "buttonEditResolution";
			this.buttonEditResolution.Size = new Size(101, 23);
			this.buttonEditResolution.TabIndex = 13;
			this.buttonEditResolution.Text = "Изменить";
			this.buttonEditResolution.UseVisualStyleBackColor = true;
			this.buttonEditResolution.Click += new EventHandler(this.buttonEditResolution_Click);
			this.buttonAddResolution.Anchor = (System.Windows.Forms.AnchorStyles)9;
			this.buttonAddResolution.Location = new Point(293, 16);
			this.buttonAddResolution.Name = "buttonAddResolution";
			this.buttonAddResolution.Size = new Size(100, 23);
			this.buttonAddResolution.TabIndex = 12;
			this.buttonAddResolution.Text = "Добавить";
			this.buttonAddResolution.UseVisualStyleBackColor = true;
			this.buttonAddResolution.Click += new EventHandler(this.buttonAddResolution_Click);
			this.buttonAddVar.Anchor = (System.Windows.Forms.AnchorStyles)9;
			this.buttonAddVar.Location = new Point(292, 192);
			this.buttonAddVar.Name = "buttonAddVar";
			this.buttonAddVar.Size = new Size(100, 52);
			this.buttonAddVar.TabIndex = 12;
			this.buttonAddVar.Text = "Добавить переменную";
			this.buttonAddVar.UseVisualStyleBackColor = true;
			this.buttonAddVar.Click += new EventHandler(this.buttonAddVar_Click);
			base.ClientSize = new Size(399, 512);
			base.Controls.Add(this.splitContainer1);
			base.Controls.Add(this.panel1);
			base.KeyPreview = true;
			this.MinimumSize = new Size(415, 550);
			base.Name = "FormRule";
			base.ShowInTaskbar = false;
			base.StartPosition = (System.Windows.Forms.FormStartPosition)4;
			base.KeyDown += new KeyEventHandler(this.RuleForm_KeyDown);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
