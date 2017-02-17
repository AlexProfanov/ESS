using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Оболочка_Буряк
{
	public class FormAddEditRule : Form
	{
		private IContainer components = null;

		private Label label1;

		private TextBox textBoxName;

		private GroupBox groupBox2;

		private ListView listViewCondition;

		private ColumnHeader columnHeader1;

		private GroupBox groupBox1;

		private ListView listViewResolution;

		private ColumnHeader columnHeader3;

		private Button buttonRemoveCondition;

		private Button buttonEditCondition;

		private Button buttonAddCondition;

		private Button buttonRemoveResolution;

		private Button buttonEditResolution;

		private Button buttonAddResolution;

		private Panel panel1;

		private Button buttonCancel;

		private Button buttonOK;

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
			this.label1 = new Label();
			this.textBoxName = new TextBox();
			this.groupBox2 = new GroupBox();
			this.listViewCondition = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.groupBox1 = new GroupBox();
			this.listViewResolution = new ListView();
			this.columnHeader3 = new ColumnHeader();
			this.buttonRemoveCondition = new Button();
			this.buttonEditCondition = new Button();
			this.buttonAddCondition = new Button();
			this.buttonRemoveResolution = new Button();
			this.buttonEditResolution = new Button();
			this.buttonAddResolution = new Button();
			this.panel1 = new Panel();
			this.buttonCancel = new Button();
			this.buttonOK = new Button();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(74, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Имя правила";
			this.textBoxName.Anchor = 12;
			this.textBoxName.Location = new Point(16, 34);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(352, 20);
			this.textBoxName.TabIndex = 1;
			this.textBoxName.add_Leave(new EventHandler(this.textBoxName_Leave));
			this.textBoxName.add_Validating(new CancelEventHandler(this.textBoxName_Validating));
			this.groupBox2.Anchor = 15;
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.listViewCondition);
			this.groupBox2.Location = new Point(10, 51);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(358, 150);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Условие правила";
			this.listViewCondition.Anchor = 15;
			this.listViewCondition.BorderStyle = 1;
			this.listViewCondition.CausesValidation = false;
			this.listViewCondition.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listViewCondition.FullRowSelect = true;
			this.listViewCondition.GridLines = true;
			this.listViewCondition.HeaderStyle = 1;
			this.listViewCondition.Location = new Point(7, 20);
			this.listViewCondition.MultiSelect = false;
			this.listViewCondition.Name = "listViewCondition";
			this.listViewCondition.Size = new Size(344, 111);
			this.listViewCondition.TabIndex = 0;
			this.listViewCondition.UseCompatibleStateImageBehavior = false;
			this.listViewCondition.View = 1;
			this.listViewCondition.add_ItemDrag(new ItemDragEventHandler(this.listViewCondition_ItemDrag));
			this.listViewCondition.add_DragDrop(new DragEventHandler(this.listViewCondition_DragDrop));
			this.listViewCondition.add_DragOver(new DragEventHandler(this.listViewCondition_DragOver));
			this.listViewCondition.add_DoubleClick(new EventHandler(this.listViewCondition_DoubleClick));
			this.columnHeader1.Text = "Условие";
			this.columnHeader1.Width = 500;
			this.groupBox1.Anchor = 14;
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.listViewResolution);
			this.groupBox1.Location = new Point(10, 266);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(358, 164);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Заключение правила";
			this.listViewResolution.Anchor = 12;
			this.listViewResolution.BorderStyle = 1;
			this.listViewResolution.CausesValidation = false;
			this.listViewResolution.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader3
			});
			this.listViewResolution.FullRowSelect = true;
			this.listViewResolution.GridLines = true;
			this.listViewResolution.HeaderStyle = 1;
			this.listViewResolution.Location = new Point(6, 29);
			this.listViewResolution.MultiSelect = false;
			this.listViewResolution.Name = "listViewResolution";
			this.listViewResolution.Size = new Size(345, 116);
			this.listViewResolution.TabIndex = 0;
			this.listViewResolution.UseCompatibleStateImageBehavior = false;
			this.listViewResolution.View = 1;
			this.listViewResolution.add_ItemDrag(new ItemDragEventHandler(this.listViewResolution_ItemDrag));
			this.listViewResolution.add_DragDrop(new DragEventHandler(this.listViewResolution_DragDrop));
			this.listViewResolution.add_DragOver(new DragEventHandler(this.listViewResolution_DragOver));
			this.listViewResolution.add_DoubleClick(new EventHandler(this.listViewResolution_DoubleClick));
			this.columnHeader3.Text = "Значение";
			this.columnHeader3.Width = 500;
			this.buttonRemoveCondition.Anchor = 14;
			this.buttonRemoveCondition.AutoSize = true;
			this.buttonRemoveCondition.Location = new Point(10, 244);
			this.buttonRemoveCondition.Name = "buttonRemoveCondition";
			this.buttonRemoveCondition.Size = new Size(358, 23);
			this.buttonRemoveCondition.TabIndex = 7;
			this.buttonRemoveCondition.Text = "Удалить";
			this.buttonRemoveCondition.UseVisualStyleBackColor = true;
			this.buttonRemoveCondition.add_Click(new EventHandler(this.buttonRemoveCondition_Click));
			this.buttonEditCondition.Anchor = 14;
			this.buttonEditCondition.AutoSize = true;
			this.buttonEditCondition.Location = new Point(10, 223);
			this.buttonEditCondition.Name = "buttonEditCondition";
			this.buttonEditCondition.Size = new Size(358, 23);
			this.buttonEditCondition.TabIndex = 6;
			this.buttonEditCondition.Text = "Изменить";
			this.buttonEditCondition.UseVisualStyleBackColor = true;
			this.buttonEditCondition.add_Click(new EventHandler(this.buttonEditCondition_Click));
			this.buttonAddCondition.Anchor = 14;
			this.buttonAddCondition.AutoSize = true;
			this.buttonAddCondition.Location = new Point(10, 202);
			this.buttonAddCondition.Name = "buttonAddCondition";
			this.buttonAddCondition.Size = new Size(358, 23);
			this.buttonAddCondition.TabIndex = 5;
			this.buttonAddCondition.Text = "Добавить";
			this.buttonAddCondition.UseVisualStyleBackColor = true;
			this.buttonAddCondition.add_Click(new EventHandler(this.buttonAddCondition_Click));
			this.buttonRemoveResolution.Anchor = 14;
			this.buttonRemoveResolution.AutoSize = true;
			this.buttonRemoveResolution.Location = new Point(10, 460);
			this.buttonRemoveResolution.Name = "buttonRemoveResolution";
			this.buttonRemoveResolution.Size = new Size(358, 23);
			this.buttonRemoveResolution.TabIndex = 10;
			this.buttonRemoveResolution.Text = "Удалить";
			this.buttonRemoveResolution.UseVisualStyleBackColor = true;
			this.buttonRemoveResolution.add_Click(new EventHandler(this.buttonRemoveResolution_Click));
			this.buttonEditResolution.Anchor = 14;
			this.buttonEditResolution.AutoSize = true;
			this.buttonEditResolution.Location = new Point(10, 439);
			this.buttonEditResolution.Name = "buttonEditResolution";
			this.buttonEditResolution.Size = new Size(358, 23);
			this.buttonEditResolution.TabIndex = 9;
			this.buttonEditResolution.Text = "Изменить";
			this.buttonEditResolution.UseVisualStyleBackColor = true;
			this.buttonEditResolution.add_Click(new EventHandler(this.buttonEditResolution_Click));
			this.buttonAddResolution.Anchor = 14;
			this.buttonAddResolution.AutoSize = true;
			this.buttonAddResolution.Location = new Point(10, 418);
			this.buttonAddResolution.Name = "buttonAddResolution";
			this.buttonAddResolution.Size = new Size(358, 23);
			this.buttonAddResolution.TabIndex = 8;
			this.buttonAddResolution.Text = "Добавить";
			this.buttonAddResolution.UseVisualStyleBackColor = true;
			this.buttonAddResolution.add_Click(new EventHandler(this.buttonAddResolution_Click));
			this.panel1.Controls.Add(this.buttonCancel);
			this.panel1.Controls.Add(this.buttonOK);
			this.panel1.Dock = 2;
			this.panel1.Location = new Point(0, 495);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(380, 32);
			this.panel1.TabIndex = 11;
			this.buttonCancel.Location = new Point(286, 3);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.add_Click(new EventHandler(this.buttonCancel_Click));
			this.buttonOK.Location = new Point(196, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(75, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "ОК";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.add_Click(new EventHandler(this.buttonOK_Click));
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = SystemColors.Highlight;
			base.ClientSize = new Size(380, 527);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.buttonRemoveResolution);
			base.Controls.Add(this.buttonEditResolution);
			base.Controls.Add(this.buttonAddResolution);
			base.Controls.Add(this.buttonRemoveCondition);
			base.Controls.Add(this.buttonEditCondition);
			base.Controls.Add(this.buttonAddCondition);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.textBoxName);
			base.Controls.Add(this.label1);
			base.Name = "FormAddEditRule";
			this.Text = "FormAddEditRule";
			base.add_Load(new EventHandler(this.FormAddEditRule_Load));
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormAddEditRule()
		{
			this.InitializeComponent();
		}

		public void createNewRule()
		{
			this.Text = "Добавление правила";
			this.ruleForEditing = new Rule(Global.knowledgeBase.UniqueRuleName);
			this.printRuleForEditing();
			int num = base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				try
				{
					Global.knowledgeBase.addRule(this.ruleForEditing);
				}
				catch (ArgumentException ex)
				{
					int num2 = MessageBox.Show("Добавление невозможно:\n" + ex.Message, "Ошибка", 0, 16);
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
			int num = base.ShowDialog();
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
			string text = this.textBoxName.Text;
			if (!Global.knowledgeBase.containsRuleName(text))
			{
				this.ruleForEditing.Name = text;
				this.formClosingMethod = FormClosingMethod.OK;
				base.Close();
			}
			else
			{
				int num = MessageBox.Show("Имя правила не может быть изменено.\nПравило с таким именем уже есть в базе знаний.", "Ошибка", 0, 16);
			}
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
		}

		private void buttonAddCondition_Click(object sender, EventArgs e)
		{
			new FormAddEditCondition1().AddCondition(this.ruleForEditing);
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
					new FormAddEditCondition1().editCondition(this.ruleForEditing.getConditionAt(conditionIndex));
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
			new FormAddEditResolution1().addResolution(this.ruleForEditing);
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
					new FormAddEditResolution1().editResolution(this.ruleForEditing.getResolutionAt(resolutionIndex));
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
			Point point = this.listViewCondition.PointToClient(new Point(e.X, e.Y));
			ListViewItem itemAt = this.listViewCondition.GetItemAt(point.X, point.Y);
			if (itemAt != null)
			{
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
			Point point = this.listViewResolution.PointToClient(new Point(e.X, e.Y));
			ListViewItem itemAt = this.listViewResolution.GetItemAt(point.X, point.Y);
			if (itemAt != null)
			{
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
		}

		private void RuleForm_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != 13)
			{
				if (keyCode == 27)
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

		private void FormAddEditRule_Load(object sender, EventArgs e)
		{
		}
	}
}
