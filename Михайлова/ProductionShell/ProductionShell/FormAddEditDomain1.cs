using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormAddEditDomain1 : Form
	{
		private IContainer components = null;

		private Button buttonRemoveValue;

		private Button buttonReplaceValue;

		private Button buttonAddValue;

		private GroupBox groupBox2;

		private ListView listViewAllowedValues;

		private TextBox textBoxName;

		private Label label1;

		private Label label2;

		private ComboBox comboBoxType;

		private ColumnHeader columnHeader2;

		private GroupBox groupBox1;

		private ColumnHeader columnHeader3;

		private TextBox textBoxValue;

		private Button buttonOK;

		private Button buttonCancel;

		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private Enumeration domainToEdit;

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
			this.buttonRemoveValue = new Button();
			this.buttonReplaceValue = new Button();
			this.buttonAddValue = new Button();
			this.groupBox2 = new GroupBox();
			this.buttonCancel = new Button();
			this.groupBox1 = new GroupBox();
			this.textBoxValue = new TextBox();
			this.buttonOK = new Button();
			this.listViewAllowedValues = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.textBoxName = new TextBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.comboBoxType = new ComboBox();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.buttonRemoveValue.Anchor = 14;
			this.buttonRemoveValue.AutoSize = true;
			this.buttonRemoveValue.Location = new Point(6, 90);
			this.buttonRemoveValue.Name = "buttonRemoveValue";
			this.buttonRemoveValue.Size = new Size(239, 23);
			this.buttonRemoveValue.TabIndex = 13;
			this.buttonRemoveValue.Text = "Удалить";
			this.buttonRemoveValue.UseVisualStyleBackColor = true;
			this.buttonRemoveValue.Click += new EventHandler(this.buttonRemoveValue_Click);
			this.buttonReplaceValue.Anchor = 14;
			this.buttonReplaceValue.AutoSize = true;
			this.buttonReplaceValue.Location = new Point(6, 67);
			this.buttonReplaceValue.Name = "buttonReplaceValue";
			this.buttonReplaceValue.Size = new Size(239, 23);
			this.buttonReplaceValue.TabIndex = 12;
			this.buttonReplaceValue.Text = "Изменить";
			this.buttonReplaceValue.UseVisualStyleBackColor = true;
			this.buttonReplaceValue.Click += new EventHandler(this.buttonReplaceValue_Click);
			this.buttonAddValue.Anchor = 14;
			this.buttonAddValue.AutoSize = true;
			this.buttonAddValue.Location = new Point(6, 44);
			this.buttonAddValue.Name = "buttonAddValue";
			this.buttonAddValue.Size = new Size(239, 23);
			this.buttonAddValue.TabIndex = 11;
			this.buttonAddValue.Text = "Добавить";
			this.buttonAddValue.UseVisualStyleBackColor = true;
			this.buttonAddValue.Click += new EventHandler(this.buttonAddValue_Click);
			this.groupBox2.Anchor = 15;
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.buttonCancel);
			this.groupBox2.Controls.Add(this.groupBox1);
			this.groupBox2.Controls.Add(this.buttonOK);
			this.groupBox2.Controls.Add(this.listViewAllowedValues);
			this.groupBox2.Location = new Point(13, 92);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(265, 323);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Список допустимых зачений";
			this.buttonCancel.Location = new Point(184, 281);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(75, 23);
			this.buttonCancel.TabIndex = 17;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			this.groupBox1.Controls.Add(this.textBoxValue);
			this.groupBox1.Controls.Add(this.buttonAddValue);
			this.groupBox1.Controls.Add(this.buttonRemoveValue);
			this.groupBox1.Controls.Add(this.buttonReplaceValue);
			this.groupBox1.Location = new Point(7, 153);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(251, 123);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Допустимое значение";
			this.textBoxValue.Anchor = 12;
			this.textBoxValue.Location = new Point(6, 20);
			this.textBoxValue.Name = "textBoxValue";
			this.textBoxValue.Size = new Size(239, 20);
			this.textBoxValue.TabIndex = 10;
			this.buttonOK.Location = new Point(106, 281);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(75, 23);
			this.buttonOK.TabIndex = 16;
			this.buttonOK.Text = "ОК";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.listViewAllowedValues.Anchor = 15;
			this.listViewAllowedValues.BorderStyle = 1;
			this.listViewAllowedValues.CausesValidation = false;
			this.listViewAllowedValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader2,
				this.columnHeader3
			});
			this.listViewAllowedValues.FullRowSelect = true;
			this.listViewAllowedValues.GridLines = true;
			this.listViewAllowedValues.HeaderStyle = 1;
			this.listViewAllowedValues.Location = new Point(7, 20);
			this.listViewAllowedValues.MultiSelect = false;
			this.listViewAllowedValues.Name = "listViewAllowedValues";
			this.listViewAllowedValues.Size = new Size(251, 117);
			this.listViewAllowedValues.TabIndex = 0;
			this.listViewAllowedValues.UseCompatibleStateImageBehavior = false;
			this.listViewAllowedValues.View = 1;
			this.listViewAllowedValues.ItemDrag += new ItemDragEventHandler(this.listViewAllowedValues_ItemDrag);
			this.listViewAllowedValues.Click += new EventHandler(this.listViewAllowedValues_Click);
			this.listViewAllowedValues.DragDrop += new DragEventHandler(this.listViewAllowedValues_DragDrop);
			this.listViewAllowedValues.DragOver += new DragEventHandler(this.listViewAllowedValues_DragOver);
			this.columnHeader2.Text = "№";
			this.columnHeader2.Width = 40;
			this.columnHeader3.Text = "Значение";
			this.columnHeader3.Width = 210;
			this.textBoxName.Anchor = 12;
			this.textBoxName.Location = new Point(20, 26);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(251, 20);
			this.textBoxName.TabIndex = 9;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(10, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(70, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Имя домена";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(10, 52);
			this.label2.Name = "label2";
			this.label2.Size = new Size(26, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Тип";
			this.comboBoxType.Enabled = false;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.Items.AddRange(new object[]
			{
				"Строковый"
			});
			this.comboBoxType.Location = new Point(20, 68);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new Size(251, 21);
			this.comboBoxType.TabIndex = 15;
			this.comboBoxType.SelectedIndexChanged += new EventHandler(this.comboBoxType_SelectedIndexChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = Color.LavenderBlush;
			base.ClientSize = new Size(292, 402);
			base.Controls.Add(this.comboBoxType);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.textBoxName);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = 3;
			base.Name = "FormAddEditDomain1";
			base.StartPosition = 1;
			this.Text = "Домен";
			base.Load += new EventHandler(this.FormAddEditDomain1_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormAddEditDomain1()
		{
			this.InitializeComponent();
		}

		public void addDomain()
		{
			this.comboBoxType.SelectedIndex = 0;
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
					int num2 = MessageBox.Show("Этот домен не имеет допустимых значений", "Ошибка", 0, 16);
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
					int num2 = MessageBox.Show("Изменение домена невозможно:\nБаза знаний уже содержит домен с таким именем", "Ошибка", 0, 16);
				}
				else if (this.domainToEdit.getAllowedValueCount() == 0)
				{
					int num3 = MessageBox.Show("Этот домен не имеет допустимых значений", "Ошибка", 0, 16);
				}
				else if (!domain.hasTheSameValues(this.domainToEdit) && MessageBox.Show("Множество допустимых значений домена изменилось. Если условие/заключение правила использовало удаленное значение домена, то условие/заключение будет удалено из правила. Хотите ли вы создать новый домен и при этом избежать возможных изменений?", "Внимание", 4, 32) == 6)
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

		private void showDomainToEdit(Enumeration domain)
		{
			this.textBoxName.Text = domain.Name;
			this.setSelectedType(domain);
			this.printAllowedValueList(domain, this.listViewAllowedValues);
		}

		private void setSelectedType(Enumeration domain)
		{
			if (domain.DomainType == typeof(string))
			{
				this.comboBoxType.SelectedIndex = 0;
			}
			else if (domain.DomainType == typeof(int))
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
				typeFromHandle = typeof(string);
				break;
			case 1:
				typeFromHandle = typeof(int);
				break;
			case 2:
				typeFromHandle = typeof(double);
				break;
			default:
				throw new OperationCanceledException("Неизвестный тип домена");
			}
			return typeFromHandle;
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

		private void buttonAddValue_Click(object sender, EventArgs e)
		{
			if (this.textBoxValue.Text != "")
			{
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
						if (selectedType != typeof(double))
						{
							throw new OperationCanceledException("Неизвестный тип домена");
						}
						value = new Value(typeof(double), double.Parse(text));
					}
					this.domainToEdit.addValue(value);
					this.printAllowedValueList(this.domainToEdit, this.listViewAllowedValues);
				}
				catch (FormatException var_3_F3)
				{
					int num = MessageBox.Show("Добавление невозможно. Значение указано в неверном формате", "Ошибка", 0, 16);
				}
				catch (ArgumentException ex)
				{
					int num = MessageBox.Show("Добавление невозможно.\n" + ex.Message, "Ошибка", 0, 16);
				}
				this.textBoxValue.Text = "";
			}
			this.textBoxValue.Focus();
		}

		private void buttonReplaceValue_Click(object sender, EventArgs e)
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
					catch (FormatException var_4_E5)
					{
						int num = MessageBox.Show("Добавление невозможно. Значение указано в неверном формате", "Ошибка", 0, 16);
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
			this.textBoxValue.Focus();
		}

		private void buttonRemoveValue_Click(object sender, EventArgs e)
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

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.OK;
			base.Close();
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.domainToEdit != null && this.domainToEdit.DomainType != this.getSelectedType())
			{
				this.domainToEdit.DomainType = this.getSelectedType();
				this.domainToEdit.removeAllValues();
				this.printAllowedValueList(this.domainToEdit, this.listViewAllowedValues);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.CANCEL;
			base.Close();
		}

		private void listViewAllowedValues_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DialogFuncs.doDragBeginning(this.listViewAllowedValues);
		}

		private void listViewAllowedValues_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewAllowedValues, e);
		}

		private void listViewAllowedValues_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = this.listViewAllowedValues.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewAllowedValues.GetItemAt(point.X, point.Y);
				if (itemAt != null)
				{
					if (e.Effect == 2)
					{
						this.domainToEdit.insertValueInto(listViewItem.Index, itemAt.Index);
					}
					else if (e.Effect == 1)
					{
						this.domainToEdit.switchValues(listViewItem.Index, itemAt.Index);
					}
					this.printAllowedValueList(this.domainToEdit, this.listViewAllowedValues);
				}
			}
		}

		private void FormAddEditDomain_KeyDown(object sender, KeyEventArgs e)
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

		private void listViewAllowedValues_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewAllowedValues.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int num = (int)enumerator.Current;
					this.textBoxValue.Text = this.listViewAllowedValues.Items[num].SubItems[1].Text;
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

		private void FormAddEditDomain1_Load(object sender, EventArgs e)
		{
		}
	}
}
