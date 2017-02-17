using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Оболочка_Буряк
{
	public class FormDomain : Form
	{
		private IContainer components = null;

		private GroupBox groupBox1;

		private Button buttonRemoveDomain;

		private Button buttonEditDomain;

		private Button buttonAddDomain;

		private GroupBox groupBox2;

		private Button buttonExit;

		private ListView listViewDomain;

		private ListView listViewAllowedValues;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

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
			this.groupBox1 = new GroupBox();
			this.buttonRemoveDomain = new Button();
			this.buttonEditDomain = new Button();
			this.buttonAddDomain = new Button();
			this.groupBox2 = new GroupBox();
			this.listViewAllowedValues = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.buttonExit = new Button();
			this.listViewDomain = new ListView();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Anchor = 11;
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.buttonRemoveDomain);
			this.groupBox1.Controls.Add(this.buttonEditDomain);
			this.groupBox1.Controls.Add(this.buttonAddDomain);
			this.groupBox1.Location = new Point(361, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(213, 121);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Действия";
			this.buttonRemoveDomain.AutoSize = true;
			this.buttonRemoveDomain.Location = new Point(3, 79);
			this.buttonRemoveDomain.Name = "buttonRemoveDomain";
			this.buttonRemoveDomain.Size = new Size(204, 23);
			this.buttonRemoveDomain.TabIndex = 2;
			this.buttonRemoveDomain.Text = "Удалить";
			this.buttonRemoveDomain.UseVisualStyleBackColor = true;
			this.buttonRemoveDomain.add_Click(new EventHandler(this.buttonRemoveDomain_Click));
			this.buttonEditDomain.AutoSize = true;
			this.buttonEditDomain.Location = new Point(3, 50);
			this.buttonEditDomain.Name = "buttonEditDomain";
			this.buttonEditDomain.Size = new Size(204, 23);
			this.buttonEditDomain.TabIndex = 1;
			this.buttonEditDomain.Text = "Изменить";
			this.buttonEditDomain.UseVisualStyleBackColor = true;
			this.buttonEditDomain.add_Click(new EventHandler(this.buttonEditDomain_Click));
			this.buttonAddDomain.AutoSize = true;
			this.buttonAddDomain.Location = new Point(3, 21);
			this.buttonAddDomain.Name = "buttonAddDomain";
			this.buttonAddDomain.Size = new Size(204, 23);
			this.buttonAddDomain.TabIndex = 0;
			this.buttonAddDomain.Text = "Добавить";
			this.buttonAddDomain.UseVisualStyleBackColor = true;
			this.buttonAddDomain.add_Click(new EventHandler(this.buttonAddDomain_Click));
			this.groupBox2.Anchor = 11;
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.listViewAllowedValues);
			this.groupBox2.Location = new Point(364, 128);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(210, 200);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Допустимые значения";
			this.listViewAllowedValues.Anchor = 7;
			this.listViewAllowedValues.BorderStyle = 1;
			this.listViewAllowedValues.CausesValidation = false;
			this.listViewAllowedValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewAllowedValues.FullRowSelect = true;
			this.listViewAllowedValues.GridLines = true;
			this.listViewAllowedValues.HeaderStyle = 1;
			this.listViewAllowedValues.Location = new Point(7, 20);
			this.listViewAllowedValues.MultiSelect = false;
			this.listViewAllowedValues.Name = "listViewAllowedValues";
			this.listViewAllowedValues.Size = new Size(197, 178);
			this.listViewAllowedValues.TabIndex = 0;
			this.listViewAllowedValues.UseCompatibleStateImageBehavior = false;
			this.listViewAllowedValues.View = 1;
			this.listViewAllowedValues.add_ItemDrag(new ItemDragEventHandler(this.listViewAllowedValues_ItemDrag));
			this.listViewAllowedValues.add_DragDrop(new DragEventHandler(this.listViewAllowedValues_DragDrop));
			this.listViewAllowedValues.add_DragOver(new DragEventHandler(this.listViewAllowedValues_DragOver));
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 40;
			this.columnHeader2.Text = "Значение";
			this.columnHeader2.Width = 154;
			this.buttonExit.Anchor = 10;
			this.buttonExit.AutoSize = true;
			this.buttonExit.Location = new Point(364, 332);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new Size(204, 23);
			this.buttonExit.TabIndex = 3;
			this.buttonExit.Text = "Закрыть";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.add_Click(new EventHandler(this.buttonExit_Click));
			this.listViewDomain.AllowDrop = true;
			this.listViewDomain.Anchor = 15;
			this.listViewDomain.BorderStyle = 1;
			this.listViewDomain.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader3,
				this.columnHeader4,
				this.columnHeader5
			});
			this.listViewDomain.FullRowSelect = true;
			this.listViewDomain.GridLines = true;
			this.listViewDomain.HeaderStyle = 1;
			this.listViewDomain.ImeMode = 0;
			this.listViewDomain.Location = new Point(3, 2);
			this.listViewDomain.MultiSelect = false;
			this.listViewDomain.Name = "listViewDomain";
			this.listViewDomain.Size = new Size(352, 353);
			this.listViewDomain.TabIndex = 4;
			this.listViewDomain.UseCompatibleStateImageBehavior = false;
			this.listViewDomain.View = 1;
			this.listViewDomain.add_ItemDrag(new ItemDragEventHandler(this.listViewDomain_ItemDrag));
			this.listViewDomain.add_SelectedIndexChanged(new EventHandler(this.listViewDomain_SelectedIndexChanged));
			this.listViewDomain.add_DragDrop(new DragEventHandler(this.listViewDomain_DragDrop));
			this.listViewDomain.add_DragOver(new DragEventHandler(this.listViewDomain_DragOver));
			this.listViewDomain.add_DoubleClick(new EventHandler(this.listViewDomain_DoubleClick));
			this.listViewDomain.add_KeyDown(new KeyEventHandler(this.FormDomainList_KeyDown));
			this.columnHeader3.Text = "№";
			this.columnHeader4.Text = "Имя";
			this.columnHeader4.Width = 290;
			this.columnHeader5.Text = "Тип";
			this.columnHeader5.Width = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = SystemColors.Highlight;
			base.ClientSize = new Size(580, 358);
			base.Controls.Add(this.listViewDomain);
			base.Controls.Add(this.buttonExit);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Name = "FormDomain";
			this.Text = "Список доменов";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormDomain()
		{
			this.InitializeComponent();
			this.printDomainList();
		}

		private void printDomainList()
		{
			int indexToSelect = 0;
			if (this.listViewDomain.SelectedIndices.Count > 0)
			{
				indexToSelect = this.listViewDomain.SelectedIndices[0];
			}
			this.listViewDomain.Items.Clear();
			IEnumerator<Enumeration> enumeratorForEnumerations = Global.knowledgeBase.getEnumeratorForEnumerations();
			int num = 1;
			while (enumeratorForEnumerations.MoveNext())
			{
				ListView.ListViewItemCollection arg_9E_0 = this.listViewDomain.Items;
				ListViewItem listViewItem = new ListViewItem(num.ToString());
				listViewItem.SubItems.Add(enumeratorForEnumerations.Current.ToString());
				listViewItem.SubItems.Add(DialogFuncs.printType(enumeratorForEnumerations.Current.DomainType));
				arg_9E_0.Add(listViewItem);
				num++;
			}
			DialogFuncs.selectListViewItem(this.listViewDomain, indexToSelect);
		}

		private void showDomainInfo(int domainIndex)
		{
			if (0 <= domainIndex && domainIndex < this.listViewDomain.Items.Count)
			{
				this.listViewAllowedValues.Items.Clear();
				IEnumerator<Value> enumeratorForValues = Global.knowledgeBase.getEnumerationAt(domainIndex).getEnumeratorForValues();
				int num = 1;
				while (enumeratorForValues.MoveNext())
				{
					ListView.ListViewItemCollection arg_78_0 = this.listViewAllowedValues.Items;
					ListViewItem listViewItem = new ListViewItem(num.ToString());
					listViewItem.SubItems.Add(enumeratorForValues.Current.ToString());
					arg_78_0.Add(listViewItem);
					num++;
				}
				this.listViewAllowedValues.SelectedIndices.Add(0);
			}
		}

		private void buttonAddDomain_Click(object sender, EventArgs e)
		{
			new FormAddEditDomain1().addDomain();
			this.printDomainList();
			DialogFuncs.selectListViewItem(this.listViewDomain, this.listViewDomain.Items.Count - 1);
		}

		private void buttonEditDomain_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					new FormAddEditDomain1().editDomain(Global.knowledgeBase.getEnumerationAt(enumerationIndex));
					this.printDomainList();
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

		private void buttonRemoveDomain_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					if (MessageBox.Show("Вы действительно хотите удалить домен с именем " + Global.knowledgeBase.getEnumerationAt(enumerationIndex) + " ?.\nЭтот домен будет удален из всех переменных, которые на него ссылаются.\nКроме того, будут удалены все условия и заключения, которые используют его значения.", "Внимание", 4, 32) == 6)
					{
						Global.knowledgeBase.removeEnumerationAt(enumerationIndex);
						this.printDomainList();
					}
					DialogFuncs.selectListViewItem(this.listViewDomain, 0);
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

		private void buttonExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void listViewDomain_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int domainIndex = (int)enumerator.Current;
					this.showDomainInfo(domainIndex);
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

		private void listViewDomain_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewDomain.AllowDrop = true;
			this.listViewAllowedValues.AllowDrop = false;
			DialogFuncs.doDragBeginning(this.listViewDomain);
		}

		private void listViewDomain_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewDomain, e);
		}

		private void listViewDomain_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = this.listViewDomain.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewDomain.GetItemAt(point.X, point.Y);
				if (itemAt != null)
				{
					if (e.Effect == 2)
					{
						Global.knowledgeBase.insertDomainInto(listViewItem.Index, itemAt.Index);
					}
					else if (e.Effect == 1)
					{
						Global.knowledgeBase.switchEnumerations(listViewItem.Index, itemAt.Index);
					}
					this.printDomainList();
				}
			}
		}

		private void listViewAllowedValues_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewDomain.AllowDrop = false;
			this.listViewAllowedValues.AllowDrop = true;
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
				if (this.listViewDomain.SelectedIndices.Count > 0)
				{
					int num = this.listViewDomain.SelectedIndices[0];
					Enumeration enumerationAt = Global.knowledgeBase.getEnumerationAt(num);
					if (itemAt != null)
					{
						if (e.Effect == 2)
						{
							enumerationAt.insertValueInto(listViewItem.Index, itemAt.Index);
						}
						else if (e.Effect == 1)
						{
							enumerationAt.switchValues(listViewItem.Index, itemAt.Index);
						}
						this.showDomainInfo(num);
					}
				}
			}
		}

		private void listViewDomain_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditDomain_Click(this, new EventArgs());
		}

		private void FormDomainList_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode <= 27)
			{
				if (keyCode != 13)
				{
					if (keyCode == 27)
					{
						this.buttonExit_Click(this, new EventArgs());
					}
				}
				else
				{
					this.buttonEditDomain_Click(this, new EventArgs());
				}
			}
			else if (keyCode != 32)
			{
				if (keyCode == 46)
				{
					this.buttonRemoveDomain_Click(this, new EventArgs());
				}
			}
			else
			{
				this.buttonAddDomain_Click(this, new EventArgs());
			}
		}
	}
}
