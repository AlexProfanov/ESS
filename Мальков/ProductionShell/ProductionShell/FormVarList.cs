using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormVarList : Form
	{
		private IContainer components;

		private Panel panel1;

		private ListView listViewVar;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private GroupBox groupBox1;

		private Button buttonRemoveVar;

		private Button buttonEditVar;

		private Button buttonAddVar;

		private GroupBox groupBox2;

		private Label label1;

		private TextBox textBoxVarType;

		private TextBox textBoxDomain;

		private Label label2;

		private RichTextBox richTextBoxQuestion;

		private Button buttonClose;

		private Label label3;

		private ToolTip toolTip;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

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
			this.buttonClose = new Button();
			this.groupBox2 = new GroupBox();
			this.label3 = new Label();
			this.richTextBoxQuestion = new RichTextBox();
			this.label2 = new Label();
			this.textBoxDomain = new TextBox();
			this.label1 = new Label();
			this.textBoxVarType = new TextBox();
			this.groupBox1 = new GroupBox();
			this.buttonRemoveVar = new Button();
			this.buttonEditVar = new Button();
			this.buttonAddVar = new Button();
			this.listViewVar = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.toolTip = new ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = 4;
			this.panel1.Location = new Point(694, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(270, 419);
			this.panel1.TabIndex = 2;
			this.buttonClose.Anchor = 2;
			this.buttonClose.FlatStyle = 0;
			this.buttonClose.Location = new Point(10, 390);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new Size(250, 23);
			this.buttonClose.TabIndex = 4;
			this.buttonClose.Text = "Закрыть";
			this.toolTip.SetToolTip(this.buttonClose, "Вызов через - Esc");
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new EventHandler(this.buttonClose_Click);
			this.groupBox2.Anchor = 3;
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.richTextBoxQuestion);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.textBoxDomain);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textBoxVarType);
			this.groupBox2.Location = new Point(10, 122);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(250, 262);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Текущая переменная";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(9, 106);
			this.label3.Name = "label3";
			this.label3.Size = new Size(171, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Текст вопроса для означивания";
			this.richTextBoxQuestion.Anchor = 3;
			this.richTextBoxQuestion.BorderStyle = 1;
			this.richTextBoxQuestion.Location = new Point(6, 122);
			this.richTextBoxQuestion.Name = "richTextBoxQuestion";
			this.richTextBoxQuestion.ReadOnly = true;
			this.richTextBoxQuestion.Size = new Size(238, 134);
			this.richTextBoxQuestion.TabIndex = 4;
			this.richTextBoxQuestion.TabStop = false;
			this.richTextBoxQuestion.Text = "";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(6, 24);
			this.label2.Name = "label2";
			this.label2.Size = new Size(42, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Домен";
			this.textBoxDomain.BorderStyle = 1;
			this.textBoxDomain.Location = new Point(6, 40);
			this.textBoxDomain.Name = "textBoxDomain";
			this.textBoxDomain.ReadOnly = true;
			this.textBoxDomain.Size = new Size(238, 20);
			this.textBoxDomain.TabIndex = 2;
			this.textBoxDomain.TabStop = false;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(6, 63);
			this.label1.Name = "label1";
			this.label1.Size = new Size(112, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Способ означивания";
			this.textBoxVarType.BorderStyle = 1;
			this.textBoxVarType.Location = new Point(6, 79);
			this.textBoxVarType.Name = "textBoxVarType";
			this.textBoxVarType.ReadOnly = true;
			this.textBoxVarType.Size = new Size(238, 20);
			this.textBoxVarType.TabIndex = 0;
			this.textBoxVarType.TabStop = false;
			this.groupBox1.Controls.Add(this.buttonRemoveVar);
			this.groupBox1.Controls.Add(this.buttonEditVar);
			this.groupBox1.Controls.Add(this.buttonAddVar);
			this.groupBox1.Location = new Point(10, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(250, 113);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Действия";
			this.buttonRemoveVar.FlatStyle = 0;
			this.buttonRemoveVar.Location = new Point(6, 79);
			this.buttonRemoveVar.Name = "buttonRemoveVar";
			this.buttonRemoveVar.Size = new Size(238, 23);
			this.buttonRemoveVar.TabIndex = 3;
			this.buttonRemoveVar.Text = "Удалить";
			this.toolTip.SetToolTip(this.buttonRemoveVar, "Вызов через - Del");
			this.buttonRemoveVar.UseVisualStyleBackColor = true;
			this.buttonRemoveVar.Click += new EventHandler(this.buttonRemoveVar_Click);
			this.buttonEditVar.FlatStyle = 0;
			this.buttonEditVar.Location = new Point(6, 50);
			this.buttonEditVar.Name = "buttonEditVar";
			this.buttonEditVar.Size = new Size(238, 23);
			this.buttonEditVar.TabIndex = 2;
			this.buttonEditVar.Text = "Редактировать";
			this.toolTip.SetToolTip(this.buttonEditVar, "Вызов через - Enter/DoubleClick");
			this.buttonEditVar.UseVisualStyleBackColor = true;
			this.buttonEditVar.Click += new EventHandler(this.buttonEditVar_Click);
			this.buttonAddVar.FlatStyle = 0;
			this.buttonAddVar.Location = new Point(6, 21);
			this.buttonAddVar.Name = "buttonAddVar";
			this.buttonAddVar.Size = new Size(238, 23);
			this.buttonAddVar.TabIndex = 1;
			this.buttonAddVar.Text = "Добавить";
			this.toolTip.SetToolTip(this.buttonAddVar, "Вызов через - Space");
			this.buttonAddVar.UseVisualStyleBackColor = true;
			this.buttonAddVar.Click += new EventHandler(this.buttonAddVar_Click);
			this.listViewVar.AllowDrop = true;
			this.listViewVar.BorderStyle = 1;
			this.listViewVar.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3,
				this.columnHeader4,
				this.columnHeader5
			});
			this.listViewVar.Dock = 5;
			this.listViewVar.ForeColor = Color.DarkBlue;
			this.listViewVar.FullRowSelect = true;
			this.listViewVar.GridLines = true;
			this.listViewVar.HeaderStyle = 1;
			this.listViewVar.HideSelection = false;
			this.listViewVar.Location = new Point(0, 0);
			this.listViewVar.MultiSelect = false;
			this.listViewVar.Name = "listViewVar";
			this.listViewVar.Size = new Size(694, 419);
			this.listViewVar.TabIndex = 0;
			this.toolTip.SetToolTip(this.listViewVar, "Drag&Drop - вставка переменной на новое место\r\nDrag&Drop + Shift - перестановка переменных местами");
			this.listViewVar.UseCompatibleStateImageBehavior = false;
			this.listViewVar.View = 1;
			this.listViewVar.DragDrop += new DragEventHandler(this.listViewVar_DragDrop);
			this.listViewVar.DoubleClick += new EventHandler(this.listViewVar_DoubleClick);
			this.listViewVar.DragOver += new DragEventHandler(this.listViewVar_DragOver);
			this.listViewVar.SelectedIndexChanged += new EventHandler(this.listViewVar_SelectedIndexChanged);
			this.listViewVar.ItemDrag += new ItemDragEventHandler(this.listViewVar_ItemDrag);
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 40;
			this.columnHeader2.Text = "Имя";
			this.columnHeader2.Width = 270;
			this.columnHeader3.Text = "Означивание";
			this.columnHeader3.Width = 120;
			this.columnHeader4.Text = "Домен";
			this.columnHeader4.Width = 120;
			this.columnHeader5.Text = "Тип";
			this.columnHeader5.Width = 120;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(964, 419);
			base.Controls.Add(this.listViewVar);
			base.Controls.Add(this.panel1);
			base.KeyPreview = true;
			this.MinimumSize = new Size(350, 350);
			base.Name = "FormVarList";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			this.Text = "Список переменных";
			base.KeyDown += new KeyEventHandler(this.FormVarList_KeyDown);
			this.panel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public FormVarList()
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
					return;
				}
				if (variableAt is DeducibleAskedVariable)
				{
					this.textBoxVarType.Text = "Выводимо-запрашиваемая";
					this.richTextBoxQuestion.Text = (variableAt as DeducibleAskedVariable).Question;
					return;
				}
				if (variableAt is AskedVariable)
				{
					this.textBoxVarType.Text = "Запрашиваемая";
					this.richTextBoxQuestion.Text = (variableAt as AskedVariable).Question;
				}
			}
		}

		private void buttonAddVar_Click(object sender, EventArgs e)
		{
			FormAddEditVar formAddEditVar = new FormAddEditVar();
			formAddEditVar.addVariable();
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
					FormAddEditVar formAddEditVar = new FormAddEditVar();
					formAddEditVar.editVariable(Global.knowledgeBase.getVariableAt(variableIndex));
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
				Point point = new Point(e.X, e.Y);
				Point point2 = this.listViewVar.PointToClient(point);
				ListViewItem itemAt = this.listViewVar.GetItemAt(point2.X, point2.Y);
				if (itemAt == null)
				{
					return;
				}
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

		private void FormVarList_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode <= 27)
			{
				if (keyCode == 13)
				{
					this.buttonEditVar_Click(this, new EventArgs());
					return;
				}
				if (keyCode != 27)
				{
					return;
				}
				this.buttonClose_Click(this, new EventArgs());
				return;
			}
			else
			{
				if (keyCode == 32)
				{
					this.buttonAddVar_Click(this, new EventArgs());
					return;
				}
				if (keyCode != 46)
				{
					return;
				}
				this.buttonRemoveVar_Click(this, new EventArgs());
				return;
			}
		}

		private void listViewVar_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditVar_Click(this, new EventArgs());
		}
	}
}
