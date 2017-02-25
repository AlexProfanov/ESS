using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZShellCore;

namespace ZShellUI
{
	public class FormAddEditDomain : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private Enumeration domainToEdit;

		private IContainer components = null;

		private Label label1;

		private TextBox textBoxName;

		private GroupBox groupBox1;

		private Button buttonOK;

		private Button buttonRemoveValue;

		private Button buttonAddValue;

		private Button buttonCancel;

		private ListView listViewAllowedValues;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ToolTip toolTip;

		private GroupBox groupBox2;

		private TextBox textBoxValue;

		public FormAddEditDomain()
		{
			this.InitializeComponent();
			this.buttonOK.DialogResult = (System.Windows.Forms.DialogResult)1;
			this.textBoxValue.Focus();
		}

		public void addDomain(int indexToInsert = -1)
		{
			this.domainToEdit = new Enumeration(Global.knowledgeBase.UniqueDomainName, this.getSelectedType());
			this.addDomainWithSavingContext(indexToInsert);
			this.textBoxValue.Focus();
		}

		private void addDomainWithSavingContext(int indexToInsert = -1)
		{
			this.Text = "Добавление домена";
			this.showDomainToEdit(this.domainToEdit);
			base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				this.domainToEdit.Name = this.textBoxName.Text;
				this.domainToEdit.DomainType = this.getSelectedType();
				if (this.domainToEdit.getAllowedValuesCount() == 0)
				{
					MessageBox.Show("Этот домен не имеет допустимых значений", "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
				}
				else
				{
					try
					{
						Global.knowledgeBase.addEnumeration(this.domainToEdit);
						if (indexToInsert != -1)
						{
							Global.knowledgeBase.insertDomainInto(Global.knowledgeBase.getEnumerationCount() - 1, indexToInsert);
						}
					}
					catch (ArgumentException ex)
					{
						MessageBox.Show("Добавление домена невозможно:\n" + ex.Message, "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
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
			base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				this.domainToEdit.Name = this.textBoxName.Text;
				if (domain.Name != this.domainToEdit.Name && Global.knowledgeBase.containsEnumerationName(this.domainToEdit.Name))
				{
					MessageBox.Show("Изменение домена невозможно:\nБаза знаний уже содержит домен с таким именем", "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
				}
				else if (this.domainToEdit.getAllowedValuesCount() == 0)
				{
					MessageBox.Show("Этот домен не имеет допустимых значений", "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
				}
				else if (!domain.hasTheSameValues(this.domainToEdit) && MessageBox.Show("Множество допустимых значений домена изменилось. Если условие/заключение правила использовало удаленное значение домена, то условие/заключение будет удалено из правила. Хотите ли вы создать новый домен и при этом избежать возможных изменений?", "Внимание", (System.Windows.Forms.MessageBoxButtons)4, (System.Windows.Forms.MessageBoxIcon)32) == (DialogResult)6)
				{
					this.domainToEdit.Name = Global.knowledgeBase.UniqueDomainName;
					this.addDomainWithSavingContext(-1);
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
			this.printAllowedValueList(domain, this.listViewAllowedValues);
			this.textBoxValue.Focus();
		}

		private Type getSelectedType()
		{
			return typeof(string);
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
				MessageBox.Show("Добавление невозможно. Значение указано в неверном формате", "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
			}
			catch (ArgumentException ex)
			{
				MessageBox.Show("Добавление невозможно.\n" + ex.Message, "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
			}
			this.textBoxValue.Text = "";
			this.textBoxValue.Focus();
		}

		private void buttonReplaceValue_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewAllowedValues.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int valueIndex = (int)enumerator.Current;
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
						this.domainToEdit.replaceValueAt(valueIndex, newValue);
					}
					catch (FormatException)
					{
						MessageBox.Show("Добавление невозможно. Значение указано в неверном формате", "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
					}
					this.textBoxValue.Text = "";
					this.printAllowedValueList(this.domainToEdit, this.listViewAllowedValues);
					this.textBoxValue.Focus();
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
			this.textBoxValue.Focus();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.OK;
			base.Close();
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.domainToEdit != null && !(this.domainToEdit.DomainType == this.getSelectedType()))
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
			if (MessageBox.Show("Вы действительно хотите переместить элемент ?", "Внимание", (System.Windows.Forms.MessageBoxButtons)4, (System.Windows.Forms.MessageBoxIcon)48) == (DialogResult)6)
			{
				if (e.Data.GetDataPresent(typeof(ListViewItem)))
				{
					ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
					Point point = this.listViewAllowedValues.PointToClient(new Point(e.X, e.Y));
					ListViewItem itemAt = this.listViewAllowedValues.GetItemAt(point.X, point.Y);
					if (itemAt != null)
					{
						if (e.Effect == (DragDropEffects)2)
						{
							this.domainToEdit.insertValueInto(listViewItem.Index, itemAt.Index);
						}
						this.printAllowedValueList(this.domainToEdit, this.listViewAllowedValues);
					}
				}
			}
		}

		private void FormAddEditDomain_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != (Keys)13)
			{
				if (keyCode == (Keys)27)
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
			this.groupBox1 = new GroupBox();
			this.listViewAllowedValues = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.groupBox2 = new GroupBox();
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
			this.textBoxName.Location = new Point(15, 29);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new Size(334, 20);
			this.textBoxName.TabIndex = 2;
			this.groupBox1.Controls.Add(this.listViewAllowedValues);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Location = new Point(15, 56);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(334, 278);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Список допустимых значений";
			this.listViewAllowedValues.AllowDrop = true;
			this.listViewAllowedValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewAllowedValues.ForeColor = Color.DarkBlue;
			this.listViewAllowedValues.FullRowSelect = true;
			this.listViewAllowedValues.GridLines = true;
			this.listViewAllowedValues.HeaderStyle = (System.Windows.Forms.ColumnHeaderStyle)1;
			this.listViewAllowedValues.HideSelection = false;
			this.listViewAllowedValues.Location = new Point(6, 19);
			this.listViewAllowedValues.MultiSelect = false;
			this.listViewAllowedValues.Name = "listViewAllowedValues";
			this.listViewAllowedValues.Size = new Size(316, 163);
			this.listViewAllowedValues.TabIndex = 1;
			this.listViewAllowedValues.UseCompatibleStateImageBehavior = false;
			this.listViewAllowedValues.View = (System.Windows.Forms.View)1;
			this.listViewAllowedValues.ItemDrag += new ItemDragEventHandler(this.listViewAllowedValues_ItemDrag);
			this.listViewAllowedValues.Click += new EventHandler(this.listViewAllowedValues_Click);
			this.listViewAllowedValues.DragDrop += new DragEventHandler(this.listViewAllowedValues_DragDrop);
			this.listViewAllowedValues.DragOver += new DragEventHandler(this.listViewAllowedValues_DragOver);
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 40;
			this.columnHeader2.Text = "Значение";
			this.columnHeader2.Width = 200;
			this.groupBox2.Controls.Add(this.textBoxValue);
			this.groupBox2.Controls.Add(this.buttonRemoveValue);
			this.groupBox2.Controls.Add(this.buttonAddValue);
			this.groupBox2.Location = new Point(6, 188);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(322, 79);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Допустимое значение";
			this.textBoxValue.Location = new Point(6, 19);
			this.textBoxValue.Name = "textBoxValue";
			this.textBoxValue.Size = new Size(310, 20);
			this.textBoxValue.TabIndex = 1;
			this.buttonRemoveValue.Location = new Point(154, 45);
			this.buttonRemoveValue.Name = "buttonRemoveValue";
			this.buttonRemoveValue.Size = new Size(91, 23);
			this.buttonRemoveValue.TabIndex = 6;
			this.buttonRemoveValue.Text = "Удалить";
			this.buttonRemoveValue.UseVisualStyleBackColor = true;
			this.buttonRemoveValue.Click += new EventHandler(this.buttonRemoveValue_Click);
			this.buttonAddValue.Location = new Point(57, 45);
			this.buttonAddValue.Name = "buttonAddValue";
			this.buttonAddValue.Size = new Size(91, 23);
			this.buttonAddValue.TabIndex = 4;
			this.buttonAddValue.Text = "Добавить";
			this.buttonAddValue.UseVisualStyleBackColor = true;
			this.buttonAddValue.Click += new EventHandler(this.buttonAddValue_Click);
			this.buttonOK.Location = new Point(15, 340);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(91, 23);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(258, 340);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(91, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.ClientSize = new Size(364, 372);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.textBoxName);
			base.Controls.Add(this.label1);
			base.KeyPreview = true;
			base.MaximizeBox = false;
			this.MaximumSize = new Size(380, 410);
			base.MinimizeBox = false;
			this.MinimumSize = new Size(380, 410);
			base.Name = "FormAddEditDomain";
			base.ShowInTaskbar = false;
			base.StartPosition = (System.Windows.Forms.FormStartPosition)4;
			base.KeyDown += new KeyEventHandler(this.FormAddEditDomain_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
