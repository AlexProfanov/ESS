using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Оболочка_Буряк
{
	public class FormAddEditVar1 : Form
	{
		private IContainer components = null;

		private Button buttonCancel;

		private Button buttonOK;

		private ComboBox comboBoxType;

		private Label label2;

		private TextBox textBoxName;

		private Label label1;

		private GroupBox groupBox1;

		private ListView listViewValues;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private Button buttonEditDomain;

		private Button buttonClearDomai;

		private GroupBox groupBoxQuestion;

		private RadioButton radioButtonAsked;

		private RadioButton radioButtonDeducibleAsked;

		private RadioButton radioButtonDeducible;

		private RichTextBox richTextBox1;

		private GroupBox groupBox3;

		private RichTextBox richTextBoxQuestion;

		private ComboBox comboBoxDomain;

		private Button button1;

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
			this.buttonCancel = new Button();
			this.buttonOK = new Button();
			this.comboBoxType = new ComboBox();
			this.label2 = new Label();
			this.textBoxName = new TextBox();
			this.label1 = new Label();
			this.groupBox1 = new GroupBox();
			this.comboBoxDomain = new ComboBox();
			this.buttonEditDomain = new Button();
			this.buttonClearDomai = new Button();
			this.listViewValues = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.groupBoxQuestion = new GroupBox();
			this.richTextBox1 = new RichTextBox();
			this.radioButtonAsked = new RadioButton();
			this.radioButtonDeducibleAsked = new RadioButton();
			this.radioButtonDeducible = new RadioButton();
			this.groupBox3 = new GroupBox();
			this.richTextBoxQuestion = new RichTextBox();
			this.button1 = new Button();
			this.groupBox1.SuspendLayout();
			this.groupBoxQuestion.SuspendLayout();
			this.groupBox3.SuspendLayout();
			base.SuspendLayout();
			this.buttonCancel.Location = new Point(193, 469);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(75, 23);
			this.buttonCancel.TabIndex = 24;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.add_Click(new EventHandler(this.buttonCancel_Click));
			this.buttonOK.Location = new Point(112, 469);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(75, 23);
			this.buttonOK.TabIndex = 23;
			this.buttonOK.Text = "ОК";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.add_Click(new EventHandler(this.buttonOK_Click));
			this.comboBoxType.DropDownStyle = 2;
			this.comboBoxType.Enabled = false;
			this.comboBoxType.FlatStyle = 1;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.Items.AddRange(new object[]
			{
				"Целый",
				"Строковый",
				"Вещественный"
			});
			this.comboBoxType.Location = new Point(11, 64);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new Size(258, 21);
			this.comboBoxType.TabIndex = 22;
			this.comboBoxType.Visible = false;
			this.comboBoxType.add_SelectedIndexChanged(new EventHandler(this.comboBoxType_SelectedIndexChanged));
			this.comboBoxType.add_Validated(new EventHandler(this.comboBoxType_Validated));
			this.label2.AutoSize = true;
			this.label2.Enabled = false;
			this.label2.Location = new Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new Size(26, 13);
			this.label2.TabIndex = 21;
			this.label2.Text = "Тип";
			this.label2.Visible = false;
			this.textBoxName.Anchor = 12;
			this.textBoxName.Location = new Point(11, 25);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(257, 20);
			this.textBoxName.TabIndex = 19;
			this.textBoxName.add_TextChanged(new EventHandler(this.textBoxName_TextChanged));
			this.label1.AutoSize = true;
			this.label1.Location = new Point(8, 5);
			this.label1.Name = "label1";
			this.label1.Size = new Size(94, 13);
			this.label1.TabIndex = 18;
			this.label1.Text = "Имя переменной";
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.comboBoxDomain);
			this.groupBox1.Controls.Add(this.buttonEditDomain);
			this.groupBox1.Controls.Add(this.buttonClearDomai);
			this.groupBox1.Controls.Add(this.listViewValues);
			this.groupBox1.Location = new Point(10, 91);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(262, 200);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Домен";
			this.comboBoxDomain.DropDownStyle = 2;
			this.comboBoxDomain.FlatStyle = 1;
			this.comboBoxDomain.FormattingEnabled = true;
			this.comboBoxDomain.Location = new Point(2, 16);
			this.comboBoxDomain.Name = "comboBoxDomain";
			this.comboBoxDomain.Size = new Size(254, 21);
			this.comboBoxDomain.TabIndex = 29;
			this.comboBoxDomain.add_SelectedIndexChanged(new EventHandler(this.comboBoxDomain_SelectedIndexChanged));
			this.buttonEditDomain.Enabled = false;
			this.buttonEditDomain.Location = new Point(4, 169);
			this.buttonEditDomain.Name = "buttonEditDomain";
			this.buttonEditDomain.Size = new Size(257, 23);
			this.buttonEditDomain.TabIndex = 28;
			this.buttonEditDomain.Text = "Редактировать домен";
			this.buttonEditDomain.UseVisualStyleBackColor = true;
			this.buttonEditDomain.Visible = false;
			this.buttonEditDomain.add_Click(new EventHandler(this.buttonEditDomain_Click));
			this.buttonClearDomai.Enabled = false;
			this.buttonClearDomai.Location = new Point(4, 169);
			this.buttonClearDomai.Name = "buttonClearDomai";
			this.buttonClearDomai.Size = new Size(258, 23);
			this.buttonClearDomai.TabIndex = 27;
			this.buttonClearDomai.Text = "Убрать домен";
			this.buttonClearDomai.UseVisualStyleBackColor = true;
			this.buttonClearDomai.Visible = false;
			this.buttonClearDomai.add_Click(new EventHandler(this.buttonClearDomain_Click));
			this.listViewValues.Anchor = 0;
			this.listViewValues.BorderStyle = 1;
			this.listViewValues.CausesValidation = false;
			this.listViewValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader2,
				this.columnHeader3
			});
			this.listViewValues.FullRowSelect = true;
			this.listViewValues.GridLines = true;
			this.listViewValues.HeaderStyle = 1;
			this.listViewValues.Location = new Point(2, 43);
			this.listViewValues.MultiSelect = false;
			this.listViewValues.Name = "listViewValues";
			this.listViewValues.Size = new Size(256, 90);
			this.listViewValues.TabIndex = 26;
			this.listViewValues.UseCompatibleStateImageBehavior = false;
			this.listViewValues.View = 1;
			this.listViewValues.add_ItemDrag(new ItemDragEventHandler(this.listViewValues_ItemDrag));
			this.listViewValues.add_DragDrop(new DragEventHandler(this.listViewValues_DragDrop));
			this.listViewValues.add_DragOver(new DragEventHandler(this.listViewValues_DragOver));
			this.columnHeader2.Text = "№";
			this.columnHeader2.Width = 40;
			this.columnHeader3.Text = "Имя";
			this.columnHeader3.Width = 210;
			this.groupBoxQuestion.Controls.Add(this.richTextBox1);
			this.groupBoxQuestion.Controls.Add(this.radioButtonAsked);
			this.groupBoxQuestion.Controls.Add(this.radioButtonDeducibleAsked);
			this.groupBoxQuestion.Controls.Add(this.radioButtonDeducible);
			this.groupBoxQuestion.Location = new Point(10, 297);
			this.groupBoxQuestion.Name = "groupBoxQuestion";
			this.groupBoxQuestion.Size = new Size(256, 88);
			this.groupBoxQuestion.TabIndex = 26;
			this.groupBoxQuestion.TabStop = false;
			this.groupBoxQuestion.Text = "Вид переменной";
			this.richTextBox1.Location = new Point(0, 87);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new Size(256, 55);
			this.richTextBox1.TabIndex = 27;
			this.richTextBox1.Text = "";
			this.radioButtonAsked.AutoSize = true;
			this.radioButtonAsked.Location = new Point(7, 41);
			this.radioButtonAsked.Name = "radioButtonAsked";
			this.radioButtonAsked.Size = new Size(108, 17);
			this.radioButtonAsked.TabIndex = 2;
			this.radioButtonAsked.TabStop = true;
			this.radioButtonAsked.Text = "Запрашиваемая";
			this.radioButtonAsked.UseVisualStyleBackColor = true;
			this.radioButtonAsked.add_Click(new EventHandler(this.radioButtonAsked_Click));
			this.radioButtonDeducibleAsked.AutoSize = true;
			this.radioButtonDeducibleAsked.Location = new Point(7, 64);
			this.radioButtonDeducibleAsked.Name = "radioButtonDeducibleAsked";
			this.radioButtonDeducibleAsked.Size = new Size(163, 17);
			this.radioButtonDeducibleAsked.TabIndex = 1;
			this.radioButtonDeducibleAsked.TabStop = true;
			this.radioButtonDeducibleAsked.Text = "Выводимо-запрашиваемая";
			this.radioButtonDeducibleAsked.UseVisualStyleBackColor = true;
			this.radioButtonDeducibleAsked.add_Click(new EventHandler(this.radioButtonDeducibleAsked_Click));
			this.radioButtonDeducible.AutoSize = true;
			this.radioButtonDeducible.Location = new Point(7, 20);
			this.radioButtonDeducible.Name = "radioButtonDeducible";
			this.radioButtonDeducible.Size = new Size(84, 17);
			this.radioButtonDeducible.TabIndex = 0;
			this.radioButtonDeducible.TabStop = true;
			this.radioButtonDeducible.Text = "Выводимая";
			this.radioButtonDeducible.UseVisualStyleBackColor = true;
			this.radioButtonDeducible.add_Click(new EventHandler(this.radioButtonDeducible_Click));
			this.groupBox3.Controls.Add(this.richTextBoxQuestion);
			this.groupBox3.Location = new Point(10, 391);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(256, 70);
			this.groupBox3.TabIndex = 27;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Текст вопроса";
			this.richTextBoxQuestion.Location = new Point(7, 19);
			this.richTextBoxQuestion.Name = "richTextBoxQuestion";
			this.richTextBoxQuestion.Size = new Size(243, 45);
			this.richTextBoxQuestion.TabIndex = 0;
			this.richTextBoxQuestion.Text = "";
			this.richTextBoxQuestion.add_Validating(new CancelEventHandler(this.richTextBoxQuestion_Validating));
			this.button1.Location = new Point(6, 141);
			this.button1.Name = "button1";
			this.button1.Size = new Size(250, 22);
			this.button1.TabIndex = 30;
			this.button1.Text = "Добавить новый домен";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.add_Click(new EventHandler(this.button1_Click));
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = SystemColors.Highlight;
			base.ClientSize = new Size(284, 493);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBoxQuestion);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.comboBoxType);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBoxName);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = 3;
			base.Name = "FormAddEditVar1";
			this.Text = "FormAddEditVar1";
			this.groupBox1.ResumeLayout(false);
			this.groupBoxQuestion.ResumeLayout(false);
			this.groupBoxQuestion.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormAddEditVar1()
		{
			this.InitializeComponent();
		}

		public void addVariable()
		{
			this.Text = "Добавление переменной";
			this.varToEdit = new DeducibleVariable(Global.knowledgeBase.UniqueVarName, typeof(string));
			this.showVarToEdit();
			int num = base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				this.varToEdit.Name = this.textBoxName.Text;
				try
				{
					Global.knowledgeBase.addVariable(this.varToEdit);
				}
				catch (ArgumentException ex)
				{
					int num2 = MessageBox.Show("Добавление переменной невозможно.\n" + ex.Message, "Ошибка", 0, 16);
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
			}
			else if (variable is AskedVariable)
			{
				this.varToEdit = new AskedVariable(variable.Name, variable.VarType, (variable as AskedVariable).Question);
				this.radioButtonAsked.Checked = true;
				this.richTextBoxQuestion.Text = (variable as AskedVariable).Question;
			}
			else if (variable is DeducibleAskedVariable)
			{
				this.varToEdit = new DeducibleAskedVariable(variable.Name, variable.VarType, (variable as DeducibleAskedVariable).Question);
				this.radioButtonDeducibleAsked.Checked = true;
				this.richTextBoxQuestion.Text = (variable as DeducibleAskedVariable).Question;
			}
			if (variable.Domain != null)
			{
				this.varToEdit.Domain = variable.Domain;
			}
			this.showVarToEdit();
			int num = base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				this.varToEdit.Name = this.textBoxName.Text;
				if (this.varToEdit.Name != variable.Name && Global.knowledgeBase.containsVariableName(this.varToEdit.Name))
				{
					int num2 = MessageBox.Show("Переменная с таким именем уже есть в базе знаний", "Ошибка", 0, 16);
				}
				else
				{
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
		}

		private void showVarToEdit()
		{
			if (this.varToEdit != null)
			{
				this.textBoxName.Text = this.varToEdit.Name;
				this.setSelectedType(this.varToEdit.VarType);
				this.showAllowedDomains();
				this.setSelectedDomain();
				this.showSelectedDomainAllowedList();
				this.setSelectedVarType();
			}
		}

		public void setSelectedType(Type type)
		{
			if (type == typeof(int))
			{
				this.comboBoxType.SelectedIndex = 0;
			}
			else if (type == typeof(string))
			{
				this.comboBoxType.SelectedIndex = 1;
			}
			else if (type == typeof(bool))
			{
				this.comboBoxType.SelectedIndex = 2;
			}
			else if (!(type != typeof(double)))
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
			if (this.varToEdit != null)
			{
				if (this.varToEdit is DeducibleAskedVariable)
				{
					this.radioButtonDeducibleAsked.Checked = true;
				}
				else if (this.varToEdit is AskedVariable)
				{
					this.radioButtonAsked.Checked = true;
				}
				else if (this.varToEdit is DeducibleVariable)
				{
					this.radioButtonDeducible.Checked = true;
				}
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
			}
			else
			{
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
					ListView.ListViewItemCollection arg_B8_0 = this.listViewValues.Items;
					ListViewItem listViewItem = new ListViewItem(num.ToString());
					listViewItem.SubItems.Add(enumeratorForValues.Current.ToString());
					arg_B8_0.Add(listViewItem);
					num++;
				}
				DialogFuncs.selectListViewItem(this.listViewValues, indexToSelect);
			}
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.varToEdit != null && !(this.varToEdit.VarType == this.getSelectedType()))
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
			FormAddEditDomain1 formAddEditDomain = new FormAddEditDomain1();
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
			if (this.varToEdit != null)
			{
				if (this.varToEdit is AskedVariable)
				{
					(this.varToEdit as AskedVariable).Question = this.richTextBoxQuestion.Text;
				}
				else if (this.varToEdit is DeducibleAskedVariable)
				{
					(this.varToEdit as DeducibleAskedVariable).Question = this.richTextBoxQuestion.Text;
				}
			}
		}

		private void FormAddEditVar_KeyDown(object sender, KeyEventArgs e)
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
				Point point = this.listViewValues.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewValues.GetItemAt(point.X, point.Y);
				if (itemAt != null)
				{
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

		private void button1_Click(object sender, EventArgs e)
		{
			new FormAddEditDomain1().addDomain();
			this.showAllowedDomains();
			this.comboBoxDomain.SelectedIndex = this.comboBoxDomain.Items.Count - 1;
			this.setSelectedDomain();
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			this.richTextBoxQuestion.Text = this.textBoxName.Text + "?";
		}
	}
}
