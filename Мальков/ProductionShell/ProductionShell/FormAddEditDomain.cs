using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormAddEditDomain : Form
	{
		private IContainer components;

		private Label label1;

		private TextBox textBoxName;

		private ComboBox comboBoxType;

		private Label label2;

		private GroupBox groupBox1;

		private Button buttonOK;

		private GroupBox groupBox2;

		private TextBox textBoxValue;

		private Button buttonRemoveValue;

		private Button buttonAddValue;

		private Button buttonCancel;

		private ListView listViewAllowedValues;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private Button buttonReplaceValue;

		private ToolTip toolTip;

		private Enumeration domainToEdit;

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
			this.components = new Container();
			this.label1 = new Label();
			this.textBoxName = new TextBox();
			this.comboBoxType = new ComboBox();
			this.label2 = new Label();
			this.groupBox1 = new GroupBox();
			this.listViewAllowedValues = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.groupBox2 = new GroupBox();
			this.buttonReplaceValue = new Button();
			this.textBoxValue = new TextBox();
			this.buttonRemoveValue = new Button();
			this.buttonAddValue = new Button();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.toolTip = new ToolTip(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Имя";
			this.textBoxName.BorderStyle = 1;
			this.textBoxName.Location = new Point(15, 29);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(276, 20);
			this.textBoxName.TabIndex = 2;
			this.toolTip.SetToolTip(this.textBoxName, "Имя домена");
			this.comboBoxType.BackColor = SystemColors.Window;
			this.comboBoxType.DropDownHeight = 400;
			this.comboBoxType.DropDownStyle = 2;
			this.comboBoxType.FlatStyle = 0;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.IntegralHeight = false;
			this.comboBoxType.Items.AddRange(new object[]
			{
				"Целый",
				"Строковый",
				"Вещественный"
			});
			this.comboBoxType.Location = new Point(14, 76);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new Size(279, 21);
			this.comboBoxType.TabIndex = 3;
			this.toolTip.SetToolTip(this.comboBoxType, "Тип домена");
			this.comboBoxType.SelectedIndexChanged += new EventHandler(this.comboBoxType_SelectedIndexChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 56);
			this.label2.Name = "label2";
			this.label2.Size = new Size(26, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Тип";
			this.groupBox1.Controls.Add(this.listViewAllowedValues);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Location = new Point(14, 104);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(279, 298);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Список допустимых значений";
			this.listViewAllowedValues.AllowDrop = true;
			this.listViewAllowedValues.BorderStyle = 1;
			this.listViewAllowedValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewAllowedValues.ForeColor = Color.DarkBlue;
			this.listViewAllowedValues.FullRowSelect = true;
			this.listViewAllowedValues.GridLines = true;
			this.listViewAllowedValues.HeaderStyle = 1;
			this.listViewAllowedValues.HideSelection = false;
			this.listViewAllowedValues.Location = new Point(6, 19);
			this.listViewAllowedValues.MultiSelect = false;
			this.listViewAllowedValues.Name = "listViewAllowedValues";
			this.listViewAllowedValues.Size = new Size(261, 134);
			this.listViewAllowedValues.TabIndex = 1;
			this.toolTip.SetToolTip(this.listViewAllowedValues, "Drag&Drop - вставка значения на новое место\r\nDrag&Drop + Shift - перестановка значений местами");
			this.listViewAllowedValues.UseCompatibleStateImageBehavior = false;
			this.listViewAllowedValues.View = 1;
			this.listViewAllowedValues.DragDrop += new DragEventHandler(this.listViewAllowedValues_DragDrop);
			this.listViewAllowedValues.DragOver += new DragEventHandler(this.listViewAllowedValues_DragOver);
			this.listViewAllowedValues.ItemDrag += new ItemDragEventHandler(this.listViewAllowedValues_ItemDrag);
			this.listViewAllowedValues.Click += new EventHandler(this.listViewAllowedValues_Click);
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 40;
			this.columnHeader2.Text = "Значение";
			this.columnHeader2.Width = 200;
			this.groupBox2.Controls.Add(this.buttonReplaceValue);
			this.groupBox2.Controls.Add(this.textBoxValue);
			this.groupBox2.Controls.Add(this.buttonRemoveValue);
			this.groupBox2.Controls.Add(this.buttonAddValue);
			this.groupBox2.Location = new Point(6, 159);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(267, 133);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Допустимое значение";
			this.buttonReplaceValue.FlatStyle = 0;
			this.buttonReplaceValue.Location = new Point(6, 75);
			this.buttonReplaceValue.Name = "buttonReplaceValue";
			this.buttonReplaceValue.Size = new Size(255, 23);
			this.buttonReplaceValue.TabIndex = 5;
			this.buttonReplaceValue.Text = "Заменить";
			this.buttonReplaceValue.UseVisualStyleBackColor = true;
			this.buttonReplaceValue.Click += new EventHandler(this.buttonReplaceValue_Click);
			this.textBoxValue.BorderStyle = 1;
			this.textBoxValue.Location = new Point(6, 19);
			this.textBoxValue.Name = "textBoxValue";
			this.textBoxValue.Size = new Size(255, 20);
			this.textBoxValue.TabIndex = 1;
			this.buttonRemoveValue.FlatStyle = 0;
			this.buttonRemoveValue.Location = new Point(6, 104);
			this.buttonRemoveValue.Name = "buttonRemoveValue";
			this.buttonRemoveValue.Size = new Size(255, 23);
			this.buttonRemoveValue.TabIndex = 6;
			this.buttonRemoveValue.Text = "Удалить";
			this.buttonRemoveValue.UseVisualStyleBackColor = true;
			this.buttonRemoveValue.Click += new EventHandler(this.buttonRemoveValue_Click);
			this.buttonAddValue.FlatStyle = 0;
			this.buttonAddValue.Location = new Point(6, 45);
			this.buttonAddValue.Name = "buttonAddValue";
			this.buttonAddValue.Size = new Size(255, 23);
			this.buttonAddValue.TabIndex = 4;
			this.buttonAddValue.Text = "Добавить";
			this.buttonAddValue.UseVisualStyleBackColor = true;
			this.buttonAddValue.Click += new EventHandler(this.buttonAddValue_Click);
			this.buttonOK.DialogResult = 1;
			this.buttonOK.FlatStyle = 0;
			this.buttonOK.Location = new Point(14, 408);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(139, 23);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "OK";
			this.toolTip.SetToolTip(this.buttonOK, "Вызов через - Enter");
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.FlatStyle = 0;
			this.buttonCancel.Location = new Point(159, 408);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(131, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Отмена";
			this.toolTip.SetToolTip(this.buttonCancel, "Вызов через - Esc");
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(302, 443);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.comboBoxType);
			base.Controls.Add(this.textBoxName);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = 3;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAddEditDomain";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			base.KeyDown += new KeyEventHandler(this.FormAddEditDomain_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormAddEditDomain()
		{
			this.InitializeComponent();
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
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				this.domainToEdit.Name = this.textBoxName.Text;
				this.domainToEdit.DomainType = this.getSelectedType();
				if (this.domainToEdit.getAllowedValueCount() == 0)
				{
					MessageBox.Show("Этот домен не имеет допустимых значений", "Ошибка", 0, 16);
					return;
				}
				try
				{
					Global.knowledgeBase.addEnumeration(this.domainToEdit);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show("Добавление домена невозможно:\n" + ex.Message, "Ошибка", 0, 16);
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
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				this.domainToEdit.Name = this.textBoxName.Text;
				if (domain.Name != this.domainToEdit.Name && Global.knowledgeBase.containsEnumerationName(this.domainToEdit.Name))
				{
					MessageBox.Show("Изменение домена невозможно:\nБаза знаний уже содержит домен с таким именем", "Ошибка", 0, 16);
					return;
				}
				if (this.domainToEdit.getAllowedValueCount() == 0)
				{
					MessageBox.Show("Этот домен не имеет допустимых значений", "Ошибка", 0, 16);
					return;
				}
				if (!domain.hasTheSameValues(this.domainToEdit) && MessageBox.Show("Множество допустимых значений домена изменилось. Если условие/заключение правила использовало удаленное значение домена, то условие/заключение будет удалено из правила. Хотите ли вы создать новый домен и при этом избежать возможных изменений?", "Внимание", 4, 32) == 6)
				{
					this.domainToEdit.Name = Global.knowledgeBase.UniqueDomainName;
					this.addDomainWithSavingContext();
					return;
				}
				domain.Name = this.domainToEdit.Name;
				domain.setEqualAllowedValuesWith(this.domainToEdit);
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
			if (domain.DomainType == typeof(int))
			{
				this.comboBoxType.SelectedIndex = 0;
				return;
			}
			if (domain.DomainType == typeof(string))
			{
				this.comboBoxType.SelectedIndex = 1;
				return;
			}
			if (domain.DomainType == typeof(double))
			{
				this.comboBoxType.SelectedIndex = 2;
				return;
			}
			throw new OperationCanceledException("Неизвестный тип домена");
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

		private void printAllowedValueList(Enumeration domain, ListView listView)
		{
			if (domain == null)
			{
				return;
			}
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
				ListViewItem listViewItem = new ListViewItem(num.ToString());
				listViewItem.SubItems.Add(enumeratorForValues.Current.ToString());
				listView.Items.Add(listViewItem);
				num++;
			}
			DialogFuncs.selectListViewItem(listView, indexToSelect);
		}

		private void buttonAddValue_Click(object sender, EventArgs e)
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
			catch (FormatException)
			{
				MessageBox.Show("Добавление невозможно. Значение указано в неверном формате", "Ошибка", 0, 16);
			}
			catch (ArgumentException ex)
			{
				MessageBox.Show("Добавление невозможно.\n" + ex.Message, "Ошибка", 0, 16);
			}
			this.textBoxValue.Text = "";
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
					catch (FormatException)
					{
						MessageBox.Show("Добавление невозможно. Значение указано в неверном формате", "Ошибка", 0, 16);
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
			if (this.domainToEdit == null)
			{
				return;
			}
			if (this.domainToEdit.DomainType != this.getSelectedType())
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
				Point point = new Point(e.X, e.Y);
				Point point2 = this.listViewAllowedValues.PointToClient(point);
				ListViewItem itemAt = this.listViewAllowedValues.GetItemAt(point2.X, point2.Y);
				if (itemAt == null)
				{
					return;
				}
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

		private void FormAddEditDomain_KeyDown(object sender, KeyEventArgs e)
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
	}
}
