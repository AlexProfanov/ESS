using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class F_rule_add : Form
	{
		private IContainer components = null;

		private Label label1;

		private TextBox textBoxName;

		private GroupBox groupBox1;

		private Button buttonRemoveCondition;

		private Button buttonEditCondition;

		private Button buttonAddCondition;

		private ListView listViewCondition;

		private GroupBox groupBox2;

		private Button buttonRemoveResolution;

		private Button buttonEditResolution;

		private Button buttonAddResolution;

		private ListView listViewResolution;

		private Button buttonOK;

		private Button buttonCancel;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader3;

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
			this.groupBox1 = new GroupBox();
			this.buttonRemoveCondition = new Button();
			this.buttonEditCondition = new Button();
			this.buttonAddCondition = new Button();
			this.listViewCondition = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.groupBox2 = new GroupBox();
			this.buttonRemoveResolution = new Button();
			this.buttonEditResolution = new Button();
			this.buttonAddResolution = new Button();
			this.listViewResolution = new ListView();
			this.columnHeader3 = new ColumnHeader();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(77, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Имя правила:";
			this.textBoxName.Location = new Point(16, 30);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(408, 20);
			this.textBoxName.TabIndex = 1;
			this.textBoxName.Leave += new EventHandler(this.textBoxName_Leave);
			this.textBoxName.Validating += new CancelEventHandler(this.textBoxName_Validating);
			this.groupBox1.Controls.Add(this.buttonRemoveCondition);
			this.groupBox1.Controls.Add(this.buttonEditCondition);
			this.groupBox1.Controls.Add(this.buttonAddCondition);
			this.groupBox1.Controls.Add(this.listViewCondition);
			this.groupBox1.Location = new Point(16, 57);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(408, 156);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Условие:";
			this.buttonRemoveCondition.Location = new Point(325, 122);
			this.buttonRemoveCondition.Name = "buttonRemoveCondition";
			this.buttonRemoveCondition.Size = new Size(75, 23);
			this.buttonRemoveCondition.TabIndex = 3;
			this.buttonRemoveCondition.Text = "Удалить";
			this.buttonRemoveCondition.UseVisualStyleBackColor = true;
			this.buttonRemoveCondition.Click += new EventHandler(this.buttonRemoveCondition_Click);
			this.buttonEditCondition.Location = new Point(244, 122);
			this.buttonEditCondition.Name = "buttonEditCondition";
			this.buttonEditCondition.Size = new Size(75, 23);
			this.buttonEditCondition.TabIndex = 2;
			this.buttonEditCondition.Text = "Изменить";
			this.buttonEditCondition.UseVisualStyleBackColor = true;
			this.buttonEditCondition.Click += new EventHandler(this.buttonEditCondition_Click);
			this.buttonAddCondition.Location = new Point(163, 122);
			this.buttonAddCondition.Name = "buttonAddCondition";
			this.buttonAddCondition.Size = new Size(75, 23);
			this.buttonAddCondition.TabIndex = 1;
			this.buttonAddCondition.Text = "Добавить";
			this.buttonAddCondition.UseVisualStyleBackColor = true;
			this.buttonAddCondition.Click += new EventHandler(this.buttonAddCondition_Click);
			this.listViewCondition.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listViewCondition.FullRowSelect = true;
			this.listViewCondition.GridLines = true;
			this.listViewCondition.Location = new Point(6, 19);
			this.listViewCondition.MultiSelect = false;
			this.listViewCondition.Name = "listViewCondition";
			this.listViewCondition.Size = new Size(394, 97);
			this.listViewCondition.TabIndex = 0;
			this.listViewCondition.UseCompatibleStateImageBehavior = false;
			this.listViewCondition.View = 1;
			this.listViewCondition.ItemDrag += new ItemDragEventHandler(this.listViewCondition_ItemDrag);
			this.listViewCondition.DragDrop += new DragEventHandler(this.listViewCondition_DragDrop);
			this.listViewCondition.DragOver += new DragEventHandler(this.listViewCondition_DragOver);
			this.columnHeader1.Text = "Условие";
			this.columnHeader1.Width = 393;
			this.groupBox2.Controls.Add(this.buttonRemoveResolution);
			this.groupBox2.Controls.Add(this.buttonEditResolution);
			this.groupBox2.Controls.Add(this.buttonAddResolution);
			this.groupBox2.Controls.Add(this.listViewResolution);
			this.groupBox2.Location = new Point(16, 229);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(408, 160);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Заключение:";
			this.buttonRemoveResolution.Location = new Point(327, 122);
			this.buttonRemoveResolution.Name = "buttonRemoveResolution";
			this.buttonRemoveResolution.Size = new Size(75, 23);
			this.buttonRemoveResolution.TabIndex = 3;
			this.buttonRemoveResolution.Text = "Удалить";
			this.buttonRemoveResolution.UseVisualStyleBackColor = true;
			this.buttonRemoveResolution.Click += new EventHandler(this.buttonRemoveResolution_Click);
			this.buttonEditResolution.Location = new Point(246, 122);
			this.buttonEditResolution.Name = "buttonEditResolution";
			this.buttonEditResolution.Size = new Size(75, 23);
			this.buttonEditResolution.TabIndex = 2;
			this.buttonEditResolution.Text = "Изменить";
			this.buttonEditResolution.UseVisualStyleBackColor = true;
			this.buttonEditResolution.Click += new EventHandler(this.buttonEditResolution_Click);
			this.buttonAddResolution.Location = new Point(165, 122);
			this.buttonAddResolution.Name = "buttonAddResolution";
			this.buttonAddResolution.Size = new Size(75, 23);
			this.buttonAddResolution.TabIndex = 1;
			this.buttonAddResolution.Text = "Добавить";
			this.buttonAddResolution.UseVisualStyleBackColor = true;
			this.buttonAddResolution.Click += new EventHandler(this.buttonAddResolution_Click);
			this.listViewResolution.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader3
			});
			this.listViewResolution.FullRowSelect = true;
			this.listViewResolution.GridLines = true;
			this.listViewResolution.Location = new Point(6, 19);
			this.listViewResolution.MultiSelect = false;
			this.listViewResolution.Name = "listViewResolution";
			this.listViewResolution.Size = new Size(394, 97);
			this.listViewResolution.TabIndex = 0;
			this.listViewResolution.UseCompatibleStateImageBehavior = false;
			this.listViewResolution.View = 1;
			this.listViewResolution.ItemDrag += new ItemDragEventHandler(this.listViewResolution_ItemDrag);
			this.listViewResolution.DragDrop += new DragEventHandler(this.listViewResolution_DragDrop);
			this.listViewResolution.DragOver += new DragEventHandler(this.listViewResolution_DragOver);
			this.columnHeader3.Text = "Значение";
			this.columnHeader3.Width = 389;
			this.buttonOK.Location = new Point(130, 398);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(144, 23);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "Выход с сохранием";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(280, 398);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(144, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Выход без сохранения";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(443, 433);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.textBoxName);
			base.Controls.Add(this.label1);
			base.Name = "F_rule_add";
			base.StartPosition = 1;
			this.Text = "Правило";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public F_rule_add()
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
			try
			{
				this.formClosingMethod = FormClosingMethod.OK;
				base.Close();
			}
			catch
			{
				throw new ArgumentNullException("Ошибка приложения");
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

		private void textBoxName_Validating(object sender, CancelEventArgs e)
		{
			this.ruleForEditing.Name = this.textBoxName.Text;
		}

		private void buttonAddCondition_Click(object sender, EventArgs e)
		{
			new F_AddCondition().AddCondition(this.ruleForEditing);
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
					new F_AddCondition().editCondition(this.ruleForEditing.getConditionAt(conditionIndex));
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
					if (MessageBox.Show("Вы действительно хотите удалить условие  ?", "Внимание", 1, 48) == 1)
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
			new F_AddResolution1().addResolution(this.ruleForEditing);
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
					new F_AddResolution1().editResolution(this.ruleForEditing.getResolutionAt(resolutionIndex));
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
					if (MessageBox.Show("Вы действительно хотите удалить заключение  ?", "Внимание", 1, 48) == 1)
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
	}
}
