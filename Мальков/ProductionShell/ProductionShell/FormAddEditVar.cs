using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormAddEditVar : Form
	{
		private IContainer components;

		private TextBox textBoxName;

		private Label label1;

		private ComboBox comboBoxType;

		private Label label2;

		private Button buttonOK;

		private GroupBox groupBoxVarType;

		private RadioButton radioButtonDeducible;

		private RadioButton radioButtonAsked;

		private RadioButton radioButtonDeducibleAsked;

		private GroupBox groupBox1;

		private Button buttonClearDomain;

		private ComboBox comboBoxDomain;

		private GroupBox groupBoxQuestion;

		private RichTextBox richTextBoxQuestion;

		private Button buttonCancel;

		private Button buttonEditDomain;

		private ListView listViewValues;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ToolTip toolTip;

		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private Variable varToEdit;

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
			this.textBoxName = new TextBox();
			this.label1 = new Label();
			this.comboBoxType = new ComboBox();
			this.label2 = new Label();
			this.buttonOK = new Button();
			this.groupBoxVarType = new GroupBox();
			this.radioButtonDeducibleAsked = new RadioButton();
			this.radioButtonAsked = new RadioButton();
			this.radioButtonDeducible = new RadioButton();
			this.groupBox1 = new GroupBox();
			this.listViewValues = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.buttonEditDomain = new Button();
			this.buttonClearDomain = new Button();
			this.comboBoxDomain = new ComboBox();
			this.groupBoxQuestion = new GroupBox();
			this.richTextBoxQuestion = new RichTextBox();
			this.buttonCancel = new Button();
			this.toolTip = new ToolTip(this.components);
			this.groupBoxVarType.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBoxQuestion.SuspendLayout();
			base.SuspendLayout();
			this.textBoxName.BorderStyle = 1;
			this.textBoxName.Location = new Point(12, 22);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(310, 20);
			this.textBoxName.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(9, 6);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Имя";
			this.comboBoxType.DropDownHeight = 400;
			this.comboBoxType.DropDownStyle = 2;
			this.comboBoxType.FlatStyle = 0;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.IntegralHeight = false;
			this.comboBoxType.Items.AddRange(new object[]
			{
				"Целый",
				"Строковый",
				"Логический",
				"Вещественный"
			});
			this.comboBoxType.Location = new Point(12, 60);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new Size(310, 21);
			this.comboBoxType.TabIndex = 1;
			this.comboBoxType.Validated += new EventHandler(this.comboBoxType_Validated);
			this.comboBoxType.SelectedIndexChanged += new EventHandler(this.comboBoxType_SelectedIndexChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 44);
			this.label2.Name = "label2";
			this.label2.Size = new Size(26, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Тип";
			this.buttonOK.FlatStyle = 0;
			this.buttonOK.Location = new Point(12, 534);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(150, 23);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.toolTip.SetToolTip(this.buttonOK, "Вызов через - Enter");
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.groupBoxVarType.Controls.Add(this.radioButtonDeducibleAsked);
			this.groupBoxVarType.Controls.Add(this.radioButtonAsked);
			this.groupBoxVarType.Controls.Add(this.radioButtonDeducible);
			this.groupBoxVarType.Location = new Point(12, 324);
			this.groupBoxVarType.Name = "groupBoxVarType";
			this.groupBoxVarType.Size = new Size(310, 98);
			this.groupBoxVarType.TabIndex = 7;
			this.groupBoxVarType.TabStop = false;
			this.groupBoxVarType.Text = "Вид переменной";
			this.radioButtonDeducibleAsked.AutoSize = true;
			this.radioButtonDeducibleAsked.FlatStyle = 0;
			this.radioButtonDeducibleAsked.Location = new Point(23, 68);
			this.radioButtonDeducibleAsked.Name = "radioButtonDeducibleAsked";
			this.radioButtonDeducibleAsked.Size = new Size(162, 17);
			this.radioButtonDeducibleAsked.TabIndex = 10;
			this.radioButtonDeducibleAsked.Text = "Выводимо-запрашиваемая";
			this.radioButtonDeducibleAsked.UseVisualStyleBackColor = true;
			this.radioButtonDeducibleAsked.Click += new EventHandler(this.radioButtonDeducibleAsked_Click);
			this.radioButtonAsked.AutoSize = true;
			this.radioButtonAsked.FlatStyle = 0;
			this.radioButtonAsked.Location = new Point(23, 45);
			this.radioButtonAsked.Name = "radioButtonAsked";
			this.radioButtonAsked.Size = new Size(107, 17);
			this.radioButtonAsked.TabIndex = 9;
			this.radioButtonAsked.Text = "Запрашиваемая";
			this.radioButtonAsked.UseVisualStyleBackColor = true;
			this.radioButtonAsked.Click += new EventHandler(this.radioButtonAsked_Click);
			this.radioButtonDeducible.AutoSize = true;
			this.radioButtonDeducible.Checked = true;
			this.radioButtonDeducible.FlatStyle = 0;
			this.radioButtonDeducible.Location = new Point(23, 22);
			this.radioButtonDeducible.Name = "radioButtonDeducible";
			this.radioButtonDeducible.Size = new Size(83, 17);
			this.radioButtonDeducible.TabIndex = 8;
			this.radioButtonDeducible.TabStop = true;
			this.radioButtonDeducible.Text = "Выводимая";
			this.radioButtonDeducible.UseVisualStyleBackColor = true;
			this.radioButtonDeducible.Click += new EventHandler(this.radioButtonDeducible_Click);
			this.groupBox1.Controls.Add(this.listViewValues);
			this.groupBox1.Controls.Add(this.buttonEditDomain);
			this.groupBox1.Controls.Add(this.buttonClearDomain);
			this.groupBox1.Controls.Add(this.comboBoxDomain);
			this.groupBox1.Location = new Point(12, 87);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(310, 231);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Домен";
			this.listViewValues.AllowDrop = true;
			this.listViewValues.BorderStyle = 1;
			this.listViewValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewValues.ForeColor = Color.DarkBlue;
			this.listViewValues.FullRowSelect = true;
			this.listViewValues.GridLines = true;
			this.listViewValues.HeaderStyle = 1;
			this.listViewValues.HideSelection = false;
			this.listViewValues.Location = new Point(6, 51);
			this.listViewValues.MultiSelect = false;
			this.listViewValues.Name = "listViewValues";
			this.listViewValues.Size = new Size(297, 106);
			this.listViewValues.TabIndex = 5;
			this.toolTip.SetToolTip(this.listViewValues, "Drag&Drop - вставка значения на новое место\r\nDrag&Drop + Shift - перестановка значений местами");
			this.listViewValues.UseCompatibleStateImageBehavior = false;
			this.listViewValues.View = 1;
			this.listViewValues.DragDrop += new DragEventHandler(this.listViewValues_DragDrop);
			this.listViewValues.DragOver += new DragEventHandler(this.listViewValues_DragOver);
			this.listViewValues.ItemDrag += new ItemDragEventHandler(this.listViewValues_ItemDrag);
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 40;
			this.columnHeader2.Text = "Имя";
			this.columnHeader2.Width = 230;
			this.buttonEditDomain.FlatStyle = 0;
			this.buttonEditDomain.Location = new Point(6, 197);
			this.buttonEditDomain.Name = "buttonEditDomain";
			this.buttonEditDomain.Size = new Size(297, 23);
			this.buttonEditDomain.TabIndex = 7;
			this.buttonEditDomain.Text = "Редактировать домен";
			this.buttonEditDomain.UseVisualStyleBackColor = true;
			this.buttonEditDomain.Click += new EventHandler(this.buttonEditDomain_Click);
			this.buttonClearDomain.FlatStyle = 0;
			this.buttonClearDomain.Location = new Point(6, 168);
			this.buttonClearDomain.Name = "buttonClearDomain";
			this.buttonClearDomain.Size = new Size(297, 23);
			this.buttonClearDomain.TabIndex = 6;
			this.buttonClearDomain.Text = "Убрать домен";
			this.buttonClearDomain.UseVisualStyleBackColor = true;
			this.buttonClearDomain.Click += new EventHandler(this.buttonClearDomain_Click);
			this.comboBoxDomain.DropDownHeight = 400;
			this.comboBoxDomain.DropDownStyle = 2;
			this.comboBoxDomain.FlatStyle = 0;
			this.comboBoxDomain.FormattingEnabled = true;
			this.comboBoxDomain.IntegralHeight = false;
			this.comboBoxDomain.Location = new Point(6, 26);
			this.comboBoxDomain.Name = "comboBoxDomain";
			this.comboBoxDomain.Size = new Size(297, 21);
			this.comboBoxDomain.TabIndex = 2;
			this.comboBoxDomain.SelectedIndexChanged += new EventHandler(this.comboBoxDomain_SelectedIndexChanged);
			this.groupBoxQuestion.Controls.Add(this.richTextBoxQuestion);
			this.groupBoxQuestion.Enabled = false;
			this.groupBoxQuestion.Location = new Point(12, 428);
			this.groupBoxQuestion.Name = "groupBoxQuestion";
			this.groupBoxQuestion.Size = new Size(310, 100);
			this.groupBoxQuestion.TabIndex = 10;
			this.groupBoxQuestion.TabStop = false;
			this.groupBoxQuestion.Text = "Текст запроса";
			this.richTextBoxQuestion.BorderStyle = 1;
			this.richTextBoxQuestion.Dock = 5;
			this.richTextBoxQuestion.Location = new Point(3, 16);
			this.richTextBoxQuestion.Name = "richTextBoxQuestion";
			this.richTextBoxQuestion.Size = new Size(304, 81);
			this.richTextBoxQuestion.TabIndex = 11;
			this.richTextBoxQuestion.Text = "";
			this.richTextBoxQuestion.Validating += new CancelEventHandler(this.richTextBoxQuestion_Validating);
			this.buttonCancel.FlatStyle = 0;
			this.buttonCancel.Location = new Point(172, 534);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(150, 23);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Отмена";
			this.toolTip.SetToolTip(this.buttonCancel, "Вызов через - Escape");
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(334, 566);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.groupBoxQuestion);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBoxVarType);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.comboBoxType);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBoxName);
			base.FormBorderStyle = 3;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAddEditVar";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			base.KeyDown += new KeyEventHandler(this.FormAddEditVar_KeyDown);
			this.groupBoxVarType.ResumeLayout(false);
			this.groupBoxVarType.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBoxQuestion.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormAddEditVar()
		{
			this.InitializeComponent();
		}

		public void addVariable()
		{
			this.Text = "Добавление переменной";
			this.varToEdit = new DeducibleVariable(Global.knowledgeBase.UniqueVarName, typeof(string));
			this.showVarToEdit();
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				this.varToEdit.Name = this.textBoxName.Text;
				try
				{
					Global.knowledgeBase.addVariable(this.varToEdit);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show("Добавление переменной невозможно.\n" + ex.Message, "Ошибка", 0, 16);
				}
			}
		}

		public void editVariable(Variable variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			this.Text = "Редактирование переменной";
			this.comboBoxType.Enabled = false;
			if (variable is DeducibleVariable)
			{
				this.varToEdit = new DeducibleVariable(variable.Name, variable.VarType);
				this.radioButtonDeducible.Checked = true;
				this.groupBoxQuestion.Enabled = false;
			}
			else if (variable is AskedVariable)
			{
				this.varToEdit = new AskedVariable(variable.Name, variable.VarType, (variable as AskedVariable).Question);
				this.radioButtonAsked.Checked = true;
				this.groupBoxQuestion.Enabled = true;
				this.richTextBoxQuestion.Text = (variable as AskedVariable).Question;
			}
			else if (variable is DeducibleAskedVariable)
			{
				this.varToEdit = new DeducibleAskedVariable(variable.Name, variable.VarType, (variable as DeducibleAskedVariable).Question);
				this.radioButtonDeducibleAsked.Checked = true;
				this.groupBoxQuestion.Enabled = true;
				this.richTextBoxQuestion.Text = (variable as DeducibleAskedVariable).Question;
			}
			if (variable.Domain != null)
			{
				this.varToEdit.Domain = variable.Domain;
			}
			this.showVarToEdit();
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				this.varToEdit.Name = this.textBoxName.Text;
				if (this.varToEdit.Name != variable.Name && Global.knowledgeBase.containsVariableName(this.varToEdit.Name))
				{
					MessageBox.Show("Переменная с таким именем уже есть в базе знаний", "Ошибка", 0, 16);
					return;
				}
				if (variable.Domain != this.varToEdit.Domain)
				{
					if (MessageBox.Show("Домен переменной изменился. Это может привести к удалению всех условий (вида перем-ая - значение) и заключений, где использовалась эта переменная. Хотите продолжить?", "Внимание", 4, 32) != 6)
					{
						return;
					}
					Global.knowledgeBase.removeConditionAndResolutionWith(variable);
				}
				Global.knowledgeBase.replaceVariableWith(variable, this.varToEdit);
			}
		}

		private void showVarToEdit()
		{
			if (this.varToEdit == null)
			{
				return;
			}
			this.textBoxName.Text = this.varToEdit.Name;
			this.setSelectedType(this.varToEdit.VarType);
			this.showAllowedDomains();
			this.setSelectedDomain();
			this.showSelectedDomainAllowedList();
			this.setSelectedVarType();
		}

		public void setSelectedType(Type type)
		{
			if (type == typeof(int))
			{
				this.comboBoxType.SelectedIndex = 0;
				return;
			}
			if (type == typeof(string))
			{
				this.comboBoxType.SelectedIndex = 1;
				return;
			}
			if (type == typeof(bool))
			{
				this.comboBoxType.SelectedIndex = 2;
				return;
			}
			if (type == typeof(double))
			{
				this.comboBoxType.SelectedIndex = 3;
			}
		}

		private Type getSelectedType()
		{
			Type result = null;
			if (this.comboBoxType.SelectedIndex == 0)
			{
				result = typeof(int);
			}
			else if (this.comboBoxType.SelectedIndex == 1)
			{
				result = typeof(string);
			}
			else if (this.comboBoxType.SelectedIndex == 2)
			{
				result = typeof(bool);
			}
			else if (this.comboBoxType.SelectedIndex == 3)
			{
				result = typeof(double);
			}
			return result;
		}

		private void setSelectedDomain()
		{
			if (this.varToEdit.Domain != null)
			{
				this.comboBoxDomain.SelectedIndex = this.comboBoxDomain.Items.IndexOf(this.varToEdit.Domain.ToString());
			}
		}

		private Enumeration getSelectedDomain()
		{
			Enumeration result = null;
			if (this.comboBoxDomain.SelectedIndex != -1)
			{
				string enumerationName = this.comboBoxDomain.Items[this.comboBoxDomain.SelectedIndex].ToString();
				result = Global.knowledgeBase.getEnumeration(enumerationName);
			}
			return result;
		}

		private void setSelectedVarType()
		{
			if (this.varToEdit == null)
			{
				return;
			}
			if (this.varToEdit is DeducibleAskedVariable)
			{
				this.radioButtonDeducibleAsked.Checked = true;
				return;
			}
			if (this.varToEdit is AskedVariable)
			{
				this.radioButtonAsked.Checked = true;
				return;
			}
			if (this.varToEdit is DeducibleVariable)
			{
				this.radioButtonDeducible.Checked = true;
			}
		}

		private void showAllowedDomains()
		{
			Type selectedType = this.getSelectedType();
			IEnumerator<Enumeration> enumeratorForEnumerations = Global.knowledgeBase.getEnumeratorForEnumerations();
			this.comboBoxDomain.Items.Clear();
			while (enumeratorForEnumerations.MoveNext())
			{
				if (enumeratorForEnumerations.Current.DomainType == selectedType)
				{
					this.comboBoxDomain.Items.Add(enumeratorForEnumerations.Current.ToString());
				}
			}
		}

		private void showSelectedDomainAllowedList()
		{
			if (this.varToEdit.Domain == null)
			{
				this.listViewValues.Items.Clear();
				return;
			}
			int indexToSelect = 0;
			if (this.listViewValues.SelectedIndices.Count > 0)
			{
				indexToSelect = this.listViewValues.SelectedIndices[0];
			}
			this.listViewValues.Items.Clear();
			IEnumerator<Value> enumeratorForValues = this.varToEdit.Domain.getEnumeratorForValues();
			int num = 1;
			while (enumeratorForValues.MoveNext())
			{
				ListViewItem listViewItem = new ListViewItem(num.ToString());
				listViewItem.SubItems.Add(enumeratorForValues.Current.ToString());
				this.listViewValues.Items.Add(listViewItem);
				num++;
			}
			DialogFuncs.selectListViewItem(this.listViewValues, indexToSelect);
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.varToEdit == null)
			{
				return;
			}
			if (this.varToEdit.VarType != this.getSelectedType())
			{
				this.comboBoxDomain.SelectedIndex = -1;
				this.varToEdit.VarType = this.getSelectedType();
				this.showAllowedDomains();
				this.showSelectedDomainAllowedList();
			}
		}

		private void comboBoxDomain_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboBoxDomain.SelectedIndex == -1)
			{
				this.varToEdit.removeDomain();
			}
			else
			{
				this.varToEdit.Domain = this.getSelectedDomain();
			}
			this.showSelectedDomainAllowedList();
		}

		private void radioButtonAsked_Click(object sender, EventArgs e)
		{
			this.groupBoxQuestion.Enabled = true;
			Variable variable = new AskedVariable(this.varToEdit.Name, this.varToEdit.VarType, this.richTextBoxQuestion.Text);
			if (this.varToEdit.Domain != null)
			{
				variable.Domain = this.varToEdit.Domain;
			}
			this.varToEdit = variable;
		}

		private void radioButtonDeducible_Click(object sender, EventArgs e)
		{
			this.groupBoxQuestion.Enabled = false;
			Variable variable = new DeducibleVariable(this.varToEdit.Name, this.varToEdit.VarType);
			if (this.varToEdit.Domain != null)
			{
				variable.Domain = this.varToEdit.Domain;
			}
			this.varToEdit = variable;
		}

		private void radioButtonDeducibleAsked_Click(object sender, EventArgs e)
		{
			this.groupBoxQuestion.Enabled = true;
			Variable variable = new DeducibleAskedVariable(this.varToEdit.Name, this.varToEdit.VarType, this.richTextBoxQuestion.Text);
			if (this.varToEdit.Domain != null)
			{
				variable.Domain = this.varToEdit.Domain;
			}
			this.varToEdit = variable;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.OK;
			base.Close();
		}

		private void comboBoxType_Validated(object sender, EventArgs e)
		{
			this.showAllowedDomains();
		}

		private void buttonClearDomain_Click(object sender, EventArgs e)
		{
			this.comboBoxDomain.SelectedIndex = -1;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.CANCEL;
			base.Close();
		}

		private void buttonEditDomain_Click(object sender, EventArgs e)
		{
			FormAddEditDomain formAddEditDomain = new FormAddEditDomain();
			if (this.varToEdit.Domain != null)
			{
				formAddEditDomain.editDomain(this.varToEdit.Domain);
			}
			this.showAllowedDomains();
			this.setSelectedDomain();
			this.showSelectedDomainAllowedList();
		}

		private void richTextBoxQuestion_Validating(object sender, CancelEventArgs e)
		{
			if (this.varToEdit == null)
			{
				return;
			}
			if (this.varToEdit is AskedVariable)
			{
				(this.varToEdit as AskedVariable).Question = this.richTextBoxQuestion.Text;
				return;
			}
			if (this.varToEdit is DeducibleAskedVariable)
			{
				(this.varToEdit as DeducibleAskedVariable).Question = this.richTextBoxQuestion.Text;
			}
		}

		private void FormAddEditVar_KeyDown(object sender, KeyEventArgs e)
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

		private void listViewValues_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DialogFuncs.doDragBeginning(this.listViewValues);
		}

		private void listViewValues_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewValues, e);
		}

		private void listViewValues_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = new Point(e.X, e.Y);
				Point point2 = this.listViewValues.PointToClient(point);
				ListViewItem itemAt = this.listViewValues.GetItemAt(point2.X, point2.Y);
				if (itemAt == null)
				{
					return;
				}
				Enumeration selectedDomain = this.getSelectedDomain();
				if (e.Effect == 2)
				{
					selectedDomain.insertValueInto(listViewItem.Index, itemAt.Index);
				}
				else if (e.Effect == 1)
				{
					selectedDomain.switchValues(listViewItem.Index, itemAt.Index);
				}
				this.showSelectedDomainAllowedList();
			}
		}
	}
}
