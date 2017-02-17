using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZShellCore;

namespace ZShellUI
{
	public class FormAddEditVariable : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private Variable varToEdit;

		private IContainer components = null;

		private TextBox textBoxName;

		private Label label1;

		private Button buttonOK;

		private GroupBox groupBoxVarType;

		private RadioButton radioButtonDeducible;

		private RadioButton radioButtonAsked;

		private RadioButton radioButtonDeducibleAsked;

		private GroupBox groupBox1;

		private ComboBox comboBoxDomain;

		private GroupBox groupBoxQuestion;

		private RichTextBox richTextBoxQuestion;

		private Button buttonCancel;

		private ListView listViewValues;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private Button buttonAddDomain;

		public FormAddEditVariable()
		{
			this.InitializeComponent();
		}

		public void addVariable(int indexToInsert = -1)
		{
			this.Text = "Добавление переменной";
			this.varToEdit = new DeducibleVariable(Global.knowledgeBase.UniqueVariableName, typeof(string));
			this.showVarToEdit();
			base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				this.varToEdit.Name = this.textBoxName.Text;
				try
				{
					Global.knowledgeBase.addVariable(this.varToEdit);
					if (indexToInsert != -1)
					{
						Global.knowledgeBase.insertVariableInto(Global.knowledgeBase.getVariableCount() - 1, indexToInsert);
					}
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
			if (variable is DeducibleVariable)
			{
				this.varToEdit = new DeducibleVariable(variable.Name, variable.Type);
				this.radioButtonDeducible.Checked = true;
				this.groupBoxQuestion.Enabled = false;
			}
			else if (variable is AskedVariable)
			{
				this.varToEdit = new AskedVariable(variable.Name, variable.Type, (variable as AskedVariable).Question);
				this.radioButtonAsked.Checked = true;
				this.groupBoxQuestion.Enabled = true;
				this.richTextBoxQuestion.Text = (variable as AskedVariable).Question;
			}
			else if (variable is DeducibleAskedVariable)
			{
				this.varToEdit = new DeducibleAskedVariable(variable.Name, variable.Type, (variable as DeducibleAskedVariable).Question);
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
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				this.varToEdit.Name = this.textBoxName.Text;
				if (this.varToEdit.Name != variable.Name && Global.knowledgeBase.containsVariableName(this.varToEdit.Name))
				{
					MessageBox.Show("Переменная с таким именем уже есть в базе знаний", "Ошибка", 0, 16);
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
				this.showAllowedDomains();
				this.setSelectedDomain();
				this.showSelectedDomainAllowedList();
				this.setSelectedVarType();
			}
		}

		private Type getSelectedType()
		{
			return typeof(string);
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
			if (this.varToEdit != null && !(this.varToEdit.Type == this.getSelectedType()))
			{
				this.comboBoxDomain.SelectedIndex = -1;
				this.varToEdit.Type = this.getSelectedType();
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
			Variable variable = new AskedVariable(this.varToEdit.Name, this.varToEdit.Type, this.richTextBoxQuestion.Text);
			if (this.varToEdit.Domain != null)
			{
				variable.Domain = this.varToEdit.Domain;
			}
			this.varToEdit = variable;
		}

		private void radioButtonDeducible_Click(object sender, EventArgs e)
		{
			this.groupBoxQuestion.Enabled = false;
			Variable variable = new DeducibleVariable(this.varToEdit.Name, this.varToEdit.Type);
			if (this.varToEdit.Domain != null)
			{
				variable.Domain = this.varToEdit.Domain;
			}
			this.varToEdit = variable;
		}

		private void radioButtonDeducibleAsked_Click(object sender, EventArgs e)
		{
			this.groupBoxQuestion.Enabled = true;
			Variable variable = new DeducibleAskedVariable(this.varToEdit.Name, this.varToEdit.Type, this.richTextBoxQuestion.Text);
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
			if (MessageBox.Show("Вы действительно хотите переместить элемент ?", "Внимание", 4, 48) == 6)
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
						this.showSelectedDomainAllowedList();
					}
				}
			}
		}

		private void buttonAddDomain_Click(object sender, EventArgs e)
		{
			int count = this.comboBoxDomain.Items.Count;
			int selectedIndex = this.comboBoxDomain.SelectedIndex;
			new FormAddEditDomain().addDomain(-1);
			this.showAllowedDomains();
			if (count != this.comboBoxDomain.Items.Count)
			{
				this.comboBoxDomain.SelectedIndex = this.comboBoxDomain.Items.Count - 1;
			}
			else
			{
				this.comboBoxDomain.SelectedIndex = selectedIndex;
			}
			this.showSelectedDomainAllowedList();
			this.setSelectedVarType();
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
			this.textBoxName = new TextBox();
			this.label1 = new Label();
			this.buttonOK = new Button();
			this.groupBoxVarType = new GroupBox();
			this.radioButtonDeducibleAsked = new RadioButton();
			this.radioButtonAsked = new RadioButton();
			this.radioButtonDeducible = new RadioButton();
			this.groupBox1 = new GroupBox();
			this.listViewValues = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.comboBoxDomain = new ComboBox();
			this.groupBoxQuestion = new GroupBox();
			this.richTextBoxQuestion = new RichTextBox();
			this.buttonCancel = new Button();
			this.buttonAddDomain = new Button();
			this.groupBoxVarType.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBoxQuestion.SuspendLayout();
			base.SuspendLayout();
			this.textBoxName.Location = new Point(12, 22);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(382, 20);
			this.textBoxName.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(9, 6);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Имя";
			this.buttonOK.Location = new Point(12, 495);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(107, 23);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.groupBoxVarType.Controls.Add(this.radioButtonDeducibleAsked);
			this.groupBoxVarType.Controls.Add(this.radioButtonAsked);
			this.groupBoxVarType.Controls.Add(this.radioButtonDeducible);
			this.groupBoxVarType.Location = new Point(12, 285);
			this.groupBoxVarType.Name = "groupBoxVarType";
			this.groupBoxVarType.Size = new Size(382, 98);
			this.groupBoxVarType.TabIndex = 7;
			this.groupBoxVarType.TabStop = false;
			this.groupBoxVarType.Text = "Вид переменной";
			this.radioButtonDeducibleAsked.AutoSize = true;
			this.radioButtonDeducibleAsked.Location = new Point(23, 68);
			this.radioButtonDeducibleAsked.Name = "radioButtonDeducibleAsked";
			this.radioButtonDeducibleAsked.Size = new Size(163, 17);
			this.radioButtonDeducibleAsked.TabIndex = 10;
			this.radioButtonDeducibleAsked.Text = "Выводимо-запрашиваемая";
			this.radioButtonDeducibleAsked.UseVisualStyleBackColor = true;
			this.radioButtonDeducibleAsked.Click += new EventHandler(this.radioButtonDeducibleAsked_Click);
			this.radioButtonAsked.AutoSize = true;
			this.radioButtonAsked.Location = new Point(23, 45);
			this.radioButtonAsked.Name = "radioButtonAsked";
			this.radioButtonAsked.Size = new Size(108, 17);
			this.radioButtonAsked.TabIndex = 9;
			this.radioButtonAsked.Text = "Запрашиваемая";
			this.radioButtonAsked.UseVisualStyleBackColor = true;
			this.radioButtonAsked.Click += new EventHandler(this.radioButtonAsked_Click);
			this.radioButtonDeducible.AutoSize = true;
			this.radioButtonDeducible.Checked = true;
			this.radioButtonDeducible.Location = new Point(23, 22);
			this.radioButtonDeducible.Name = "radioButtonDeducible";
			this.radioButtonDeducible.Size = new Size(84, 17);
			this.radioButtonDeducible.TabIndex = 8;
			this.radioButtonDeducible.TabStop = true;
			this.radioButtonDeducible.Text = "Выводимая";
			this.radioButtonDeducible.UseVisualStyleBackColor = true;
			this.radioButtonDeducible.Click += new EventHandler(this.radioButtonDeducible_Click);
			this.groupBox1.Controls.Add(this.buttonAddDomain);
			this.groupBox1.Controls.Add(this.listViewValues);
			this.groupBox1.Controls.Add(this.comboBoxDomain);
			this.groupBox1.Location = new Point(12, 48);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(382, 231);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Домен";
			this.listViewValues.AllowDrop = true;
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
			this.listViewValues.Location = new Point(3, 53);
			this.listViewValues.MultiSelect = false;
			this.listViewValues.Name = "listViewValues";
			this.listViewValues.Size = new Size(373, 172);
			this.listViewValues.TabIndex = 5;
			this.listViewValues.UseCompatibleStateImageBehavior = false;
			this.listViewValues.View = 1;
			this.listViewValues.ItemDrag += new ItemDragEventHandler(this.listViewValues_ItemDrag);
			this.listViewValues.DragDrop += new DragEventHandler(this.listViewValues_DragDrop);
			this.listViewValues.DragOver += new DragEventHandler(this.listViewValues_DragOver);
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 40;
			this.columnHeader2.Text = "Имя";
			this.columnHeader2.Width = 230;
			this.comboBoxDomain.DropDownHeight = 400;
			this.comboBoxDomain.DropDownStyle = 2;
			this.comboBoxDomain.FormattingEnabled = true;
			this.comboBoxDomain.IntegralHeight = false;
			this.comboBoxDomain.Location = new Point(6, 26);
			this.comboBoxDomain.Name = "comboBoxDomain";
			this.comboBoxDomain.Size = new Size(212, 21);
			this.comboBoxDomain.TabIndex = 2;
			this.comboBoxDomain.SelectedIndexChanged += new EventHandler(this.comboBoxDomain_SelectedIndexChanged);
			this.groupBoxQuestion.Controls.Add(this.richTextBoxQuestion);
			this.groupBoxQuestion.Enabled = false;
			this.groupBoxQuestion.Location = new Point(12, 389);
			this.groupBoxQuestion.Name = "groupBoxQuestion";
			this.groupBoxQuestion.Size = new Size(382, 100);
			this.groupBoxQuestion.TabIndex = 10;
			this.groupBoxQuestion.TabStop = false;
			this.groupBoxQuestion.Text = "Текст запроса";
			this.richTextBoxQuestion.Dock = 5;
			this.richTextBoxQuestion.Location = new Point(3, 16);
			this.richTextBoxQuestion.Name = "richTextBoxQuestion";
			this.richTextBoxQuestion.Size = new Size(376, 81);
			this.richTextBoxQuestion.TabIndex = 11;
			this.richTextBoxQuestion.Text = "";
			this.richTextBoxQuestion.Validating += new CancelEventHandler(this.richTextBoxQuestion_Validating);
			this.buttonCancel.Location = new Point(287, 495);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(107, 23);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			this.buttonAddDomain.Location = new Point(224, 26);
			this.buttonAddDomain.Name = "buttonAddDomain";
			this.buttonAddDomain.Size = new Size(152, 23);
			this.buttonAddDomain.TabIndex = 11;
			this.buttonAddDomain.Text = "Добавить домен";
			this.buttonAddDomain.UseVisualStyleBackColor = true;
			this.buttonAddDomain.Click += new EventHandler(this.buttonAddDomain_Click);
			base.ClientSize = new Size(406, 526);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.groupBoxQuestion);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBoxVarType);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBoxName);
			base.KeyPreview = true;
			base.MaximizeBox = false;
			this.MaximumSize = new Size(422, 564);
			base.MinimizeBox = false;
			this.MinimumSize = new Size(422, 564);
			base.Name = "FormAddEditVariable";
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
	}
}
