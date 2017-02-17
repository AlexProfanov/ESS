using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class F_variable_add : Form
	{
		private IContainer components = null;

		private Label label1;

		private TextBox textBoxName;

		private ComboBox comboBoxType;

		private GroupBox groupBox1;

		private Button buttonClearDomai;

		private Button buttonEditDomain;

		private ListView listViewValues;

		private ComboBox comboBoxDomain;

		private GroupBox groupBox2;

		private RadioButton radioButtonDeducibleAsked;

		private RadioButton radioButtonAsked;

		private RadioButton radioButtonDeducible;

		private Label label3;

		private RichTextBox richTextBoxQuestion;

		private Button buttonOK;

		private Button buttonCancel;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private int teg = 0;

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
			this.label1 = new Label();
			this.textBoxName = new TextBox();
			this.comboBoxType = new ComboBox();
			this.groupBox1 = new GroupBox();
			this.buttonEditDomain = new Button();
			this.listViewValues = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.comboBoxDomain = new ComboBox();
			this.buttonClearDomai = new Button();
			this.groupBox2 = new GroupBox();
			this.radioButtonDeducibleAsked = new RadioButton();
			this.radioButtonAsked = new RadioButton();
			this.radioButtonDeducible = new RadioButton();
			this.label3 = new Label();
			this.richTextBoxQuestion = new RichTextBox();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(97, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Имя переменной:";
			this.textBoxName.Location = new Point(12, 25);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(355, 20);
			this.textBoxName.TabIndex = 1;
			this.textBoxName.TextChanged += new EventHandler(this.textBoxName_TextChanged);
			this.comboBoxType.Enabled = false;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.Items.AddRange(new object[]
			{
				"Целый",
				"Строковый",
				"Вещественный"
			});
			this.comboBoxType.Location = new Point(237, 285);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new Size(20, 21);
			this.comboBoxType.TabIndex = 3;
			this.comboBoxType.Visible = false;
			this.comboBoxType.SelectedIndexChanged += new EventHandler(this.comboBoxType_SelectedIndexChanged);
			this.comboBoxType.Validated += new EventHandler(this.comboBoxType_Validated);
			this.groupBox1.Controls.Add(this.buttonEditDomain);
			this.groupBox1.Controls.Add(this.listViewValues);
			this.groupBox1.Controls.Add(this.comboBoxDomain);
			this.groupBox1.Location = new Point(11, 51);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(355, 178);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Домен:";
			this.buttonEditDomain.Location = new Point(204, 149);
			this.buttonEditDomain.Name = "buttonEditDomain";
			this.buttonEditDomain.Size = new Size(142, 23);
			this.buttonEditDomain.TabIndex = 2;
			this.buttonEditDomain.Text = "Новый домен";
			this.buttonEditDomain.UseVisualStyleBackColor = true;
			this.buttonEditDomain.Click += new EventHandler(this.buttonEditDomain_Click);
			this.listViewValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader2,
				this.columnHeader3
			});
			this.listViewValues.FullRowSelect = true;
			this.listViewValues.GridLines = true;
			this.listViewValues.Location = new Point(7, 48);
			this.listViewValues.MultiSelect = false;
			this.listViewValues.Name = "listViewValues";
			this.listViewValues.Size = new Size(342, 97);
			this.listViewValues.TabIndex = 1;
			this.listViewValues.UseCompatibleStateImageBehavior = false;
			this.listViewValues.View = 1;
			this.listViewValues.ItemDrag += new ItemDragEventHandler(this.listViewValues_ItemDrag);
			this.listViewValues.DragDrop += new DragEventHandler(this.listViewValues_DragDrop);
			this.listViewValues.DragOver += new DragEventHandler(this.listViewValues_DragOver);
			this.listViewValues.Validated += new EventHandler(this.listViewValues_Validated);
			this.columnHeader2.Text = "№";
			this.columnHeader2.Width = 47;
			this.columnHeader3.Text = "Имя";
			this.columnHeader3.Width = 290;
			this.comboBoxDomain.DropDownStyle = 2;
			this.comboBoxDomain.FormattingEnabled = true;
			this.comboBoxDomain.Location = new Point(7, 20);
			this.comboBoxDomain.Name = "comboBoxDomain";
			this.comboBoxDomain.Size = new Size(342, 21);
			this.comboBoxDomain.TabIndex = 0;
			this.comboBoxDomain.SelectedIndexChanged += new EventHandler(this.comboBoxDomain_SelectedIndexChanged);
			this.buttonClearDomai.Enabled = false;
			this.buttonClearDomai.Location = new Point(221, 285);
			this.buttonClearDomai.Name = "buttonClearDomai";
			this.buttonClearDomai.Size = new Size(10, 13);
			this.buttonClearDomai.TabIndex = 3;
			this.buttonClearDomai.Text = "Очистить окно";
			this.buttonClearDomai.UseVisualStyleBackColor = true;
			this.buttonClearDomai.Visible = false;
			this.buttonClearDomai.Click += new EventHandler(this.buttonClearDomai_Click);
			this.groupBox2.Controls.Add(this.radioButtonDeducibleAsked);
			this.groupBox2.Controls.Add(this.radioButtonAsked);
			this.groupBox2.Controls.Add(this.radioButtonDeducible);
			this.groupBox2.Location = new Point(12, 235);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(200, 100);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Вид переменной:";
			this.radioButtonDeducibleAsked.AutoSize = true;
			this.radioButtonDeducibleAsked.Location = new Point(7, 68);
			this.radioButtonDeducibleAsked.Name = "radioButtonDeducibleAsked";
			this.radioButtonDeducibleAsked.Size = new Size(163, 17);
			this.radioButtonDeducibleAsked.TabIndex = 2;
			this.radioButtonDeducibleAsked.TabStop = true;
			this.radioButtonDeducibleAsked.Text = "Выводимо-запрашиваемая";
			this.radioButtonDeducibleAsked.UseVisualStyleBackColor = true;
			this.radioButtonDeducibleAsked.CheckedChanged += new EventHandler(this.radioButtonDeducibleAsked_CheckedChanged);
			this.radioButtonDeducibleAsked.Click += new EventHandler(this.radioButtonDeducibleAsked_Click);
			this.radioButtonAsked.AutoSize = true;
			this.radioButtonAsked.Checked = true;
			this.radioButtonAsked.Location = new Point(7, 44);
			this.radioButtonAsked.Name = "radioButtonAsked";
			this.radioButtonAsked.Size = new Size(108, 17);
			this.radioButtonAsked.TabIndex = 1;
			this.radioButtonAsked.TabStop = true;
			this.radioButtonAsked.Text = "Запрашиваемая";
			this.radioButtonAsked.UseVisualStyleBackColor = true;
			this.radioButtonAsked.CheckedChanged += new EventHandler(this.radioButtonAsked_CheckedChanged);
			this.radioButtonAsked.Click += new EventHandler(this.radioButtonAsked_Click);
			this.radioButtonDeducible.AutoSize = true;
			this.radioButtonDeducible.Location = new Point(7, 20);
			this.radioButtonDeducible.Name = "radioButtonDeducible";
			this.radioButtonDeducible.Size = new Size(84, 17);
			this.radioButtonDeducible.TabIndex = 0;
			this.radioButtonDeducible.TabStop = true;
			this.radioButtonDeducible.Text = "Выводимая";
			this.radioButtonDeducible.UseVisualStyleBackColor = true;
			this.radioButtonDeducible.CheckedChanged += new EventHandler(this.radioButtonDeducible_CheckedChanged);
			this.radioButtonDeducible.Click += new EventHandler(this.radioButtonDeducible_Click);
			this.label3.AutoSize = true;
			this.label3.Location = new Point(16, 338);
			this.label3.Name = "label3";
			this.label3.Size = new Size(47, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Вопрос:";
			this.richTextBoxQuestion.Location = new Point(15, 354);
			this.richTextBoxQuestion.Name = "richTextBoxQuestion";
			this.richTextBoxQuestion.Size = new Size(342, 63);
			this.richTextBoxQuestion.TabIndex = 7;
			this.richTextBoxQuestion.Text = "";
			this.richTextBoxQuestion.Validated += new EventHandler(this.richTextBoxQuestion_Validated);
			this.buttonOK.Location = new Point(44, 423);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(158, 23);
			this.buttonOK.TabIndex = 8;
			this.buttonOK.Text = "Выход с сохранением";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(208, 423);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(158, 23);
			this.buttonCancel.TabIndex = 9;
			this.buttonCancel.Text = "Выход без сохранения";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(378, 458);
			base.Controls.Add(this.buttonClearDomai);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.richTextBoxQuestion);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.comboBoxType);
			base.Controls.Add(this.textBoxName);
			base.Controls.Add(this.label1);
			base.Name = "F_variable_add";
			base.StartPosition = 1;
			this.Text = "Переменная";
			base.Load += new EventHandler(this.F_variable_add_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public F_variable_add()
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
				this.richTextBoxQuestion.Enabled = false;
			}
			else if (variable is AskedVariable)
			{
				this.varToEdit = new AskedVariable(variable.Name, variable.VarType, (variable as AskedVariable).Question);
				this.radioButtonAsked.Checked = true;
				this.richTextBoxQuestion.Enabled = true;
				this.richTextBoxQuestion.Text = (variable as AskedVariable).Question;
			}
			else if (variable is DeducibleAskedVariable)
			{
				this.varToEdit = new DeducibleAskedVariable(variable.Name, variable.VarType, (variable as DeducibleAskedVariable).Question);
				this.radioButtonDeducibleAsked.Checked = true;
				this.richTextBoxQuestion.Enabled = true;
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
					int num2 = MessageBox.Show("Переменная с таким именем уже существует", "Ошибка", 0, 16);
				}
				else
				{
					if (variable.Domain != this.varToEdit.Domain)
					{
						if (MessageBox.Show("Домен переменной был изменен. Это может привести к удалению всех условий и заключений, связанных с данной переменной. Хотите продолжить?", "Внимание", 4, 32) != 6)
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
				else if (this.varToEdit is DeducibleVariable)
				{
					this.radioButtonDeducible.Checked = true;
				}
				else if (this.varToEdit is AskedVariable)
				{
					this.radioButtonAsked.Checked = true;
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

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.OK;
			base.Close();
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

		private void radioButtonDeducible_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonAsked_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonDeducibleAsked_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonAsked_Click(object sender, EventArgs e)
		{
			this.richTextBoxQuestion.Enabled = true;
			Variable variable = new AskedVariable(this.varToEdit.Name, this.varToEdit.VarType, this.richTextBoxQuestion.Text);
			if (this.varToEdit.Domain != null)
			{
				variable.Domain = this.varToEdit.Domain;
			}
			this.varToEdit = variable;
		}

		private void radioButtonDeducible_Click(object sender, EventArgs e)
		{
			this.richTextBoxQuestion.Enabled = false;
			Variable variable = new DeducibleVariable(this.varToEdit.Name, this.varToEdit.VarType);
			if (this.varToEdit.Domain != null)
			{
				variable.Domain = this.varToEdit.Domain;
			}
			this.varToEdit = variable;
		}

		private void radioButtonDeducibleAsked_Click(object sender, EventArgs e)
		{
			this.richTextBoxQuestion.Enabled = true;
			Variable variable = new DeducibleAskedVariable(this.varToEdit.Name, this.varToEdit.VarType, this.richTextBoxQuestion.Text);
			if (this.varToEdit.Domain != null)
			{
				variable.Domain = this.varToEdit.Domain;
			}
			this.varToEdit = variable;
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

		private void listViewValues_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewValues, e);
		}

		private void listViewValues_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DialogFuncs.doDragBeginning(this.listViewValues);
		}

		private void buttonEditDomain_Click(object sender, EventArgs e)
		{
			new F_domain_add().addDomain();
			this.showAllowedDomains();
			this.comboBoxDomain.SelectedIndex = this.comboBoxDomain.Items.Count - 1;
			this.setSelectedDomain();
		}

		private void buttonClearDomai_Click(object sender, EventArgs e)
		{
		}

		private void comboBoxType_Validated(object sender, EventArgs e)
		{
			this.showAllowedDomains();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.CANCEL;
			base.Close();
		}

		private void listViewValues_Validated(object sender, EventArgs e)
		{
		}

		private void richTextBoxQuestion_Validated(object sender, EventArgs e)
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

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
		}

		private void F_variable_add_Load(object sender, EventArgs e)
		{
		}
	}
}
