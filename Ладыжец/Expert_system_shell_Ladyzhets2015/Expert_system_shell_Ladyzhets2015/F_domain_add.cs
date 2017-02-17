using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class F_domain_add : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private Enumeration domainToEdit;

		private IContainer components = null;

		private Label label1;

		private TextBox textBoxName;

		private ComboBox comboBoxType;

		private GroupBox groupBox1;

		private Button buttonRemoveValue;

		private Button buttonReplaceValue;

		private Button buttonAddValue;

		private Button buttonOK;

		private Button buttonCancel;

		private ListView listViewAllowedValues;

		private Label label4;

		private TextBox textBoxValue;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		public F_domain_add()
		{
			this.InitializeComponent();
		}

		public void editDomain(Enumeration domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("enumeration");
			}
			this.Text = "Редактирование домена";
			this.showDomainToEdit(domain);
			this.domainToEdit = new Enumeration(domain);
			this.domainToEdit.removeAllConditioResolutionDependences();
			this.comboBoxType.Enabled = false;
			int num = base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				this.domainToEdit.Name = this.textBoxName.Text;
				if (domain.Name != this.domainToEdit.Name && Global.knowledgeBase.containsEnumerationName(this.domainToEdit.Name))
				{
					int num2 = MessageBox.Show("Домен с таким именем уже существует", "Ошибка", 0, 16);
				}
				else if (this.domainToEdit.getAllowedValueCount() == 0)
				{
					int num3 = MessageBox.Show("Домен не содержит значений", "Ошибка", 0, 16);
				}
				else if (!domain.hasTheSameValues(this.domainToEdit) && MessageBox.Show("Множество  значений домена изменилось. Хотите ли вы создать новый домен,чтобы избежать возможных изменений в условии/заключении правил, использующих данный домен?", "Внимание", 4, 32) == 6)
				{
					this.domainToEdit.Name = Global.knowledgeBase.UniqueDomainName;
					this.addDomainWithSavingContext();
				}
				else
				{
					domain.Name = this.domainToEdit.Name;
					domain.setEqualAllowedValuesWith(this.domainToEdit);
				}
			}
		}

		private void printAllowedValueList(Enumeration domain, ListView listView)
		{
			if (domain != null)
			{
				int indexToSelect = 0;
				if (listView.SelectedIndices.Count > 0)
				{
					indexToSelect = listView.SelectedIndices[0];
				}
				listView.Items.Clear();
				int num = 1;
				IEnumerator<Value> enumeratorForValues = domain.getEnumeratorForValues();
				while (enumeratorForValues.MoveNext())
				{
					ListView.ListViewItemCollection arg_7C_0 = listView.Items;
					ListViewItem listViewItem = new ListViewItem(num.ToString());
					listViewItem.SubItems.Add(enumeratorForValues.Current.ToString());
					arg_7C_0.Add(listViewItem);
					num++;
				}
				DialogFuncs.selectListViewItem(listView, indexToSelect);
			}
		}

		private void setSelectedType(Enumeration domain)
		{
			if (domain.DomainType == typeof(int))
			{
				this.comboBoxType.SelectedIndex = 0;
			}
			else if (domain.DomainType == typeof(string))
			{
				this.comboBoxType.SelectedIndex = 1;
			}
			else
			{
				if (domain.DomainType != typeof(double))
				{
					throw new OperationCanceledException("Неизвестный тип домена");
				}
				this.comboBoxType.SelectedIndex = 2;
			}
		}

		private Type getSelectedType()
		{
			Type typeFromHandle;
			switch (this.comboBoxType.SelectedIndex)
			{
				case 0:
					typeFromHandle = typeof(int);
					break;
				case 1:
					typeFromHandle = typeof(string);
					break;
				case 2:
					typeFromHandle = typeof(double);
					break;
				default:
					throw new OperationCanceledException("Неизвестный тип домена");
			}
			return typeFromHandle;
		}

		public void addDomain()
		{
			this.comboBoxType.Enabled = true;
			this.comboBoxType.SelectedIndex = 1;
			this.domainToEdit = new Enumeration(Global.knowledgeBase.UniqueDomainName, this.getSelectedType());
			this.addDomainWithSavingContext();
		}

		private void addDomainWithSavingContext()
		{
			this.Text = "Добавление домена";
			this.showDomainToEdit(this.domainToEdit);
			int num = base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				this.domainToEdit.Name = this.textBoxName.Text;
				this.domainToEdit.DomainType = this.getSelectedType();
				if (this.domainToEdit.getAllowedValueCount() == 0)
				{
					int num2 = MessageBox.Show("Домен не содержит значений", "Ошибка", 0, 16);
				}
				else
				{
					try
					{
						Global.knowledgeBase.addEnumeration(this.domainToEdit);
					}
					catch (ArgumentException ex)
					{
						int num3 = MessageBox.Show("Добавление домена невозможно:\n" + ex.Message, "Ошибка", 0, 16);
					}
				}
			}
		}

		private void showDomainToEdit(Enumeration domain)
		{
			this.textBoxName.Text = domain.Name;
			this.setSelectedType(domain);
			this.printAllowedValueList(domain, this.listViewAllowedValues);
		}

		private void b_domain_value_add_Click(object sender, EventArgs e)
		{
			this.domainToEdit.DomainType = this.getSelectedType();
			Type selectedType = this.getSelectedType();
			string text = this.textBoxValue.Text;
			try
			{
				Value value;
				if (selectedType == typeof(int))
				{
					value = new Value(typeof(int), int.Parse(text));
				}
				else if (selectedType == typeof(string))
				{
					value = new Value(typeof(string), text);
				}
				else
				{
					value = new Value(typeof(double), double.Parse(text));
				}
				this.domainToEdit.addValue(value);
				this.printAllowedValueList(this.domainToEdit, this.listViewAllowedValues);
			}
			catch (FormatException var_3_CB)
			{
			}
			catch (ArgumentException ex)
			{
				int num = MessageBox.Show("Добавление невозможно.\n" + ex.Message, "Ошибка", 0, 16);
			}
			this.textBoxValue.Text = "";
			this.textBoxValue.Focus();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.OK;
			base.Close();
		}

		private void b_domain_value_check_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewAllowedValues.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int oldValueIndex = (int)enumerator.Current;
					Type selectedType = this.getSelectedType();
					string text = this.textBoxValue.Text;
					try
					{
						Value newValue;
						if (selectedType == typeof(int))
						{
							newValue = new Value(typeof(int), int.Parse(text));
						}
						else if (selectedType == typeof(string))
						{
							newValue = new Value(typeof(string), text);
						}
						else
						{
							if (selectedType != typeof(double))
							{
								throw new OperationCanceledException("Неизвестный тип домена");
							}
							newValue = new Value(typeof(double), double.Parse(text));
						}
						this.domainToEdit.replaceValue(oldValueIndex, newValue);
					}
					catch (FormatException var_4_F1)
					{
					}
					this.textBoxValue.Text = "";
					this.printAllowedValueList(this.domainToEdit, this.listViewAllowedValues);
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

		private void b_domain_value_delete_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewAllowedValues.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int valueIndex = (int)enumerator.Current;
					this.domainToEdit.removeValueAt(valueIndex);
					this.printAllowedValueList(this.domainToEdit, this.listViewAllowedValues);
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

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.CANCEL;
			base.Close();
		}

		private void listViewAllowedValues_DragDrop(object sender, DragEventArgs e)
		{
		}

		private void listViewAllowedValues_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewAllowedValues, e);
		}

		private void listViewAllowedValues_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
		}

		private void listViewAllowedValues_ItemDrag(object sender, ItemDragEventArgs e)
		{
		}

		private void listViewAllowedValues_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection selectedItems = this.listViewAllowedValues.SelectedItems;
			string text = "";
			IEnumerator enumerator = selectedItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					text += listViewItem.SubItems[1].Text;
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
			this.textBoxValue.Text = text;
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{
		}

		private void F_domain_add_Load(object sender, EventArgs e)
		{
		}

		private void textBoxValue_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode == (Keys)13)
			{
				this.b_domain_value_add_Click(this, new EventArgs());
			}
		}

		private void listViewAllowedValues_Click(object sender, EventArgs e)
		{
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
			this.label1 = new Label();
			this.textBoxName = new TextBox();
			this.comboBoxType = new ComboBox();
			this.groupBox1 = new GroupBox();
			this.label4 = new Label();
			this.textBoxValue = new TextBox();
			this.buttonRemoveValue = new Button();
			this.buttonReplaceValue = new Button();
			this.listViewAllowedValues = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.buttonAddValue = new Button();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(32, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Имя:";
			this.textBoxName.Location = new Point(16, 30);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(328, 20);
			this.textBoxName.TabIndex = 1;
			this.comboBoxType.Enabled = false;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.Items.AddRange(new object[]
			{
				"Целый",
				"Строковый",
				"Вещественный"
			});
			this.comboBoxType.Location = new Point(182, 342);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new Size(18, 21);
			this.comboBoxType.TabIndex = 3;
			this.comboBoxType.Visible = false;
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBoxValue);
			this.groupBox1.Controls.Add(this.buttonRemoveValue);
			this.groupBox1.Controls.Add(this.buttonReplaceValue);
			this.groupBox1.Controls.Add(this.listViewAllowedValues);
			this.groupBox1.Controls.Add(this.buttonAddValue);
			this.groupBox1.Location = new Point(16, 57);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(320, 266);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Список допустимых значений:";
			this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(14, 210);
			this.label4.Name = "label4";
			this.label4.Size = new Size(58, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Значение:";
			this.textBoxValue.Location = new Point(78, 207);
			this.textBoxValue.Name = "textBoxValue";
			this.textBoxValue.Size = new Size(233, 20);
			this.textBoxValue.TabIndex = 11;
			this.textBoxValue.KeyDown += new KeyEventHandler(this.textBoxValue_KeyDown);
			this.buttonRemoveValue.Location = new Point(216, 233);
			this.buttonRemoveValue.Name = "buttonRemoveValue";
			this.buttonRemoveValue.Size = new Size(95, 23);
			this.buttonRemoveValue.TabIndex = 2;
			this.buttonRemoveValue.Text = "Удалить";
			this.buttonRemoveValue.UseVisualStyleBackColor = true;
			this.buttonRemoveValue.Click += new EventHandler(this.b_domain_value_delete_Click);
			this.buttonReplaceValue.Location = new Point(116, 233);
			this.buttonReplaceValue.Name = "buttonReplaceValue";
			this.buttonReplaceValue.Size = new Size(95, 23);
			this.buttonReplaceValue.TabIndex = 1;
			this.buttonReplaceValue.Text = "Изменить";
			this.buttonReplaceValue.UseVisualStyleBackColor = true;
			this.buttonReplaceValue.Click += new EventHandler(this.b_domain_value_check_Click);
			this.listViewAllowedValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader2,
				this.columnHeader3
			});
			this.listViewAllowedValues.FullRowSelect = true;
			this.listViewAllowedValues.GridLines = true;
			this.listViewAllowedValues.Location = new Point(6, 19);
			this.listViewAllowedValues.MultiSelect = false;
			this.listViewAllowedValues.Name = "listViewAllowedValues";
			this.listViewAllowedValues.Size = new Size(305, 178);
			this.listViewAllowedValues.TabIndex = 9;
			this.listViewAllowedValues.UseCompatibleStateImageBehavior = false;
			this.listViewAllowedValues.View = 1;
			this.listViewAllowedValues.DrawItem += new DrawListViewItemEventHandler(this.listViewAllowedValues_DrawItem);
			this.listViewAllowedValues.ItemDrag += new ItemDragEventHandler(this.listViewAllowedValues_ItemDrag);
			this.listViewAllowedValues.SelectedIndexChanged += new EventHandler(this.listViewAllowedValues_SelectedIndexChanged);
			this.listViewAllowedValues.Click += new EventHandler(this.listViewAllowedValues_Click);
			this.listViewAllowedValues.DragDrop += new DragEventHandler(this.listViewAllowedValues_DragDrop);
			this.listViewAllowedValues.DragOver += new DragEventHandler(this.listViewAllowedValues_DragOver);
			this.columnHeader2.Text = "№";
			this.columnHeader3.Text = "Значение";
			this.columnHeader3.Width = 240;
			this.buttonAddValue.Location = new Point(17, 233);
			this.buttonAddValue.Name = "buttonAddValue";
			this.buttonAddValue.Size = new Size(95, 23);
			this.buttonAddValue.TabIndex = 0;
			this.buttonAddValue.Text = "Добавить";
			this.buttonAddValue.UseVisualStyleBackColor = true;
			this.buttonAddValue.Click += new EventHandler(this.b_domain_value_add_Click);
			this.buttonOK.Location = new Point(16, 342);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(150, 23);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "Выход с сохранением";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.button4_Click);
			this.buttonCancel.Location = new Point(182, 342);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(154, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Выход без сохранения";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.AutoSize = true;
			base.ClientSize = new Size(348, 376);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.comboBoxType);
			base.Controls.Add(this.textBoxName);
			base.Controls.Add(this.label1);
			base.Name = "F_domain_add";
			base.StartPosition = 1;
			this.Text = "F_domain_add";
			base.Load += new EventHandler(this.F_domain_add_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
