using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Оболочка_Буряк
{
	public class FormVarList1 : Form
	{
		private IContainer components = null;

		private ListView listViewVar;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private Button buttonClose;

		private GroupBox groupBox2;

		private GroupBox groupBox1;

		private Button buttonRemoveVar;

		private Button buttonEditVar;

		private Button buttonAddVar;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private Label label3;

		private Label label2;

		private Label label1;

		private RichTextBox richTextBoxQuestion;

		private TextBox textBoxVarType;

		private TextBox textBoxDomain;

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
			this.listViewVar = new ListView();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.buttonClose = new Button();
			this.groupBox2 = new GroupBox();
			this.textBoxVarType = new TextBox();
			this.textBoxDomain = new TextBox();
			this.richTextBoxQuestion = new RichTextBox();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.groupBox1 = new GroupBox();
			this.buttonRemoveVar = new Button();
			this.buttonEditVar = new Button();
			this.buttonAddVar = new Button();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.listViewVar.AllowDrop = true;
			this.listViewVar.Anchor = 15;
			this.listViewVar.BorderStyle = 1;
			this.listViewVar.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader3,
				this.columnHeader4,
				this.columnHeader5,
				this.columnHeader6,
				this.columnHeader7
			});
			this.listViewVar.FullRowSelect = true;
			this.listViewVar.GridLines = true;
			this.listViewVar.HeaderStyle = 1;
			this.listViewVar.ImeMode = 0;
			this.listViewVar.Location = new Point(5, 3);
			this.listViewVar.MultiSelect = false;
			this.listViewVar.Name = "listViewVar";
			this.listViewVar.Size = new Size(632, 353);
			this.listViewVar.TabIndex = 8;
			this.listViewVar.UseCompatibleStateImageBehavior = false;
			this.listViewVar.View = 1;
			this.listViewVar.add_ItemDrag(new ItemDragEventHandler(this.listViewVar_ItemDrag));
			this.listViewVar.add_SelectedIndexChanged(new EventHandler(this.listViewVar_SelectedIndexChanged));
			this.listViewVar.add_DragDrop(new DragEventHandler(this.listViewVar_DragDrop));
			this.listViewVar.add_DragOver(new DragEventHandler(this.listViewVar_DragOver));
			this.listViewVar.add_DoubleClick(new EventHandler(this.listViewVar_DoubleClick));
			this.columnHeader3.Text = "№";
			this.columnHeader4.Text = "Имя";
			this.columnHeader4.Width = 200;
			this.columnHeader5.Text = "Означивание";
			this.columnHeader5.Width = 200;
			this.columnHeader6.Text = "Домен";
			this.columnHeader6.Width = 160;
			this.columnHeader7.Text = "Тип";
			this.columnHeader7.Width = 0;
			this.buttonClose.Anchor = 10;
			this.buttonClose.AutoSize = true;
			this.buttonClose.Location = new Point(646, 333);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new Size(204, 23);
			this.buttonClose.TabIndex = 7;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.add_Click(new EventHandler(this.buttonClose_Click));
			this.groupBox2.Anchor = 11;
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.textBoxVarType);
			this.groupBox2.Controls.Add(this.textBoxDomain);
			this.groupBox2.Controls.Add(this.richTextBoxQuestion);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new Point(646, 129);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(210, 197);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Текущая переменная";
			this.textBoxVarType.Location = new Point(13, 87);
			this.textBoxVarType.Name = "textBoxVarType";
			this.textBoxVarType.ReadOnly = true;
			this.textBoxVarType.Size = new Size(189, 20);
			this.textBoxVarType.TabIndex = 5;
			this.textBoxDomain.Location = new Point(13, 37);
			this.textBoxDomain.Name = "textBoxDomain";
			this.textBoxDomain.ReadOnly = true;
			this.textBoxDomain.Size = new Size(189, 20);
			this.textBoxDomain.TabIndex = 4;
			this.richTextBoxQuestion.Anchor = 7;
			this.richTextBoxQuestion.Location = new Point(13, 126);
			this.richTextBoxQuestion.Name = "richTextBoxQuestion";
			this.richTextBoxQuestion.ReadOnly = true;
			this.richTextBoxQuestion.Size = new Size(189, 52);
			this.richTextBoxQuestion.TabIndex = 3;
			this.richTextBoxQuestion.Text = "";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(10, 110);
			this.label3.Name = "label3";
			this.label3.Size = new Size(82, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Текст вопроса";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(10, 62);
			this.label2.Name = "label2";
			this.label2.Size = new Size(112, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Способ означивания";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(7, 20);
			this.label1.Name = "label1";
			this.label1.Size = new Size(42, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Домен";
			this.groupBox1.Anchor = 11;
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.buttonRemoveVar);
			this.groupBox1.Controls.Add(this.buttonEditVar);
			this.groupBox1.Controls.Add(this.buttonAddVar);
			this.groupBox1.Location = new Point(643, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(213, 121);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Действия";
			this.buttonRemoveVar.AutoSize = true;
			this.buttonRemoveVar.Location = new Point(3, 79);
			this.buttonRemoveVar.Name = "buttonRemoveVar";
			this.buttonRemoveVar.Size = new Size(204, 23);
			this.buttonRemoveVar.TabIndex = 2;
			this.buttonRemoveVar.Text = "Удалить";
			this.buttonRemoveVar.UseVisualStyleBackColor = true;
			this.buttonRemoveVar.add_Click(new EventHandler(this.buttonRemoveVar_Click));
			this.buttonEditVar.AutoSize = true;
			this.buttonEditVar.Location = new Point(3, 50);
			this.buttonEditVar.Name = "buttonEditVar";
			this.buttonEditVar.Size = new Size(204, 23);
			this.buttonEditVar.TabIndex = 1;
			this.buttonEditVar.Text = "Изменить";
			this.buttonEditVar.UseVisualStyleBackColor = true;
			this.buttonEditVar.add_Click(new EventHandler(this.buttonEditVar_Click));
			this.buttonAddVar.AutoSize = true;
			this.buttonAddVar.Location = new Point(3, 21);
			this.buttonAddVar.Name = "buttonAddVar";
			this.buttonAddVar.Size = new Size(204, 23);
			this.buttonAddVar.TabIndex = 0;
			this.buttonAddVar.Text = "Добавить";
			this.buttonAddVar.UseVisualStyleBackColor = true;
			this.buttonAddVar.add_Click(new EventHandler(this.buttonAddVar_Click));
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = SystemColors.Highlight;
			base.ClientSize = new Size(860, 358);
			base.Controls.Add(this.listViewVar);
			base.Controls.Add(this.buttonClose);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Name = "FormVarList1";
			this.Text = "Список переменных";
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormVarList1()
		{
			this.InitializeComponent();
			this.printVariableList();
		}

		private void printVariableList()
		{
			int indexToSelect = 0;
			if (this.listViewVar.SelectedIndices.Count > 0)
			{
				indexToSelect = this.listViewVar.SelectedIndices[0];
			}
			this.listViewVar.Items.Clear();
			IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			int num = 1;
			while (enumeratorForVariables.MoveNext())
			{
				ListViewItem listViewItem = new ListViewItem(num.ToString());
				listViewItem.SubItems.Add(enumeratorForVariables.Current.Name);
				if (enumeratorForVariables.Current is DeducibleVariable)
				{
					listViewItem.SubItems.Add("Выводимая");
				}
				else if (enumeratorForVariables.Current is AskedVariable)
				{
					listViewItem.SubItems.Add("Запрашиваемая");
				}
				else if (enumeratorForVariables.Current is DeducibleAskedVariable)
				{
					listViewItem.SubItems.Add("Вывод.-запр.");
				}
				if (enumeratorForVariables.Current.Domain != null)
				{
					listViewItem.SubItems.Add(enumeratorForVariables.Current.Domain.ToString());
				}
				else
				{
					listViewItem.SubItems.Add("-");
				}
				listViewItem.SubItems.Add(DialogFuncs.printType(enumeratorForVariables.Current.VarType));
				this.listViewVar.Items.Add(listViewItem);
				num++;
			}
			DialogFuncs.selectListViewItem(this.listViewVar, indexToSelect);
		}

		private void showVarInfo(int varIndex)
		{
			if (0 <= varIndex && varIndex < this.listViewVar.Items.Count)
			{
				Variable variableAt = Global.knowledgeBase.getVariableAt(varIndex);
				if (variableAt.Domain != null)
				{
					this.textBoxDomain.Text = variableAt.Domain.Name;
				}
				else
				{
					this.textBoxDomain.Text = "Отсутствует";
				}
				this.richTextBoxQuestion.Text = "";
				if (variableAt is DeducibleVariable)
				{
					this.textBoxVarType.Text = "Выводимая";
					this.richTextBoxQuestion.Enabled = false;
				}
				else if (variableAt is DeducibleAskedVariable)
				{
					this.textBoxVarType.Text = "Выводимо-запрашиваемая";
					this.richTextBoxQuestion.Text = (variableAt as DeducibleAskedVariable).Question;
				}
				else if (variableAt is AskedVariable)
				{
					this.textBoxVarType.Text = "Запрашиваемая";
					this.richTextBoxQuestion.Text = (variableAt as AskedVariable).Question;
				}
			}
		}

		private void buttonAddVar_Click(object sender, EventArgs e)
		{
			new FormAddEditVar1().addVariable();
			this.printVariableList();
			DialogFuncs.selectListViewItem(this.listViewVar, this.listViewVar.Items.Count - 1);
		}

		private void buttonEditVar_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewVar.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int variableIndex = (int)enumerator.Current;
					new FormAddEditVar1().editVariable(Global.knowledgeBase.getVariableAt(variableIndex));
					this.printVariableList();
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

		private void buttonRemoveVar_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewVar.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int num = (int)enumerator.Current;
					if (MessageBox.Show("Вы действительно хотите удалить переменную с именем " + Global.knowledgeBase.getVariableAt(num).Name + " ? Удаление переменной приведет к удалению посылок и заключений, которые ее используют.", "Внимание", 4, 32) == 6)
					{
						Global.knowledgeBase.removeVariableAt(num);
						this.printVariableList();
						DialogFuncs.selectListViewItem(this.listViewVar, 0);
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

		private void buttonClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void listViewVar_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewVar.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int varIndex = (int)enumerator.Current;
					this.showVarInfo(varIndex);
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

		private void listViewVar_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DialogFuncs.doDragBeginning(this.listViewVar);
		}

		private void listViewVar_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewVar, e);
		}

		private void listViewVar_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = this.listViewVar.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewVar.GetItemAt(point.X, point.Y);
				if (itemAt != null)
				{
					if (e.Effect == 2)
					{
						Global.knowledgeBase.insertVariableInto(listViewItem.Index, itemAt.Index);
					}
					else if (e.Effect == 1)
					{
						Global.knowledgeBase.switchVariables(listViewItem.Index, itemAt.Index);
					}
					this.printVariableList();
				}
			}
		}

		private void FormVarList_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode <= 27)
			{
				if (keyCode != 13)
				{
					if (keyCode == 27)
					{
						this.buttonClose_Click(this, new EventArgs());
					}
				}
				else
				{
					this.buttonEditVar_Click(this, new EventArgs());
				}
			}
			else if (keyCode != 32)
			{
				if (keyCode == 46)
				{
					this.buttonRemoveVar_Click(this, new EventArgs());
				}
			}
			else
			{
				this.buttonAddVar_Click(this, new EventArgs());
			}
		}

		private void listViewVar_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditVar_Click(this, new EventArgs());
		}
	}
}
