using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormDomainList : Form
	{
		private IContainer components;

		private Button buttonExit;

		private GroupBox groupBoxInfo;

		private GroupBox groupBox1;

		private Button buttonEditDomain;

		private Button buttonRemoveDomain;

		private Button buttonAddDomain;

		private Panel panel1;

		private ListView listViewDomain;

		private ListView listViewAllowedValues;

		private ColumnHeader columnHeaderN;

		private ColumnHeader columnHeaderValue;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ToolTip toolTip;

		public FormDomainList()
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
				ListViewItem listViewItem = new ListViewItem(num.ToString());
				listViewItem.SubItems.Add(enumeratorForEnumerations.Current.ToString());
				listViewItem.SubItems.Add(DialogFuncs.printType(enumeratorForEnumerations.Current.DomainType));
				this.listViewDomain.Items.Add(listViewItem);
				num++;
			}
			DialogFuncs.selectListViewItem(this.listViewDomain, indexToSelect);
		}

		private void showDomainInfo(int domainIndex)
		{
			if (0 <= domainIndex && domainIndex < this.listViewDomain.Items.Count)
			{
				this.listViewAllowedValues.Items.Clear();
				Enumeration enumerationAt = Global.knowledgeBase.getEnumerationAt(domainIndex);
				IEnumerator<Value> enumeratorForValues = enumerationAt.getEnumeratorForValues();
				int num = 1;
				while (enumeratorForValues.MoveNext())
				{
					ListViewItem listViewItem = new ListViewItem(num.ToString());
					listViewItem.SubItems.Add(enumeratorForValues.Current.ToString());
					this.listViewAllowedValues.Items.Add(listViewItem);
					num++;
				}
				this.listViewAllowedValues.SelectedIndices.Add(0);
			}
		}

		private void buttonAddDomain_Click(object sender, EventArgs e)
		{
			FormAddEditDomain formAddEditDomain = new FormAddEditDomain();
			formAddEditDomain.addDomain();
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
					FormAddEditDomain formAddEditDomain = new FormAddEditDomain();
					formAddEditDomain.editDomain(Global.knowledgeBase.getEnumerationAt(enumerationIndex));
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
				Point point = new Point(e.X, e.Y);
				Point point2 = this.listViewDomain.PointToClient(point);
				ListViewItem itemAt = this.listViewDomain.GetItemAt(point2.X, point2.Y);
				if (itemAt == null)
				{
					return;
				}
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
				Point point = new Point(e.X, e.Y);
				Point point2 = this.listViewAllowedValues.PointToClient(point);
				ListViewItem itemAt = this.listViewAllowedValues.GetItemAt(point2.X, point2.Y);
				if (this.listViewDomain.SelectedIndices.Count > 0)
				{
					int num = this.listViewDomain.SelectedIndices[0];
					Enumeration enumerationAt = Global.knowledgeBase.getEnumerationAt(num);
					if (itemAt == null)
					{
						return;
					}
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

		private void listViewDomain_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditDomain_Click(this, new EventArgs());
		}

		private void FormDomainList_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode <= 27)
			{
				if (keyCode == 13)
				{
					this.buttonEditDomain_Click(this, new EventArgs());
					return;
				}
				if (keyCode != 27)
				{
					return;
				}
				this.buttonExit_Click(this, new EventArgs());
				return;
			}
			else
			{
				if (keyCode == 32)
				{
					this.buttonAddDomain_Click(this, new EventArgs());
					return;
				}
				if (keyCode != 46)
				{
					return;
				}
				this.buttonRemoveDomain_Click(this, new EventArgs());
				return;
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
			this.buttonExit = new Button();
			this.groupBoxInfo = new GroupBox();
			this.listViewAllowedValues = new ListView();
			this.columnHeaderN = new ColumnHeader();
			this.columnHeaderValue = new ColumnHeader();
			this.groupBox1 = new GroupBox();
			this.buttonEditDomain = new Button();
			this.buttonRemoveDomain = new Button();
			this.buttonAddDomain = new Button();
			this.panel1 = new Panel();
			this.listViewDomain = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.toolTip = new ToolTip(this.components);
			this.groupBoxInfo.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.buttonExit.Anchor = 2;
			this.buttonExit.FlatStyle = 0;
			this.buttonExit.Location = new Point(10, 438);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new Size(250, 23);
			this.buttonExit.TabIndex = 1;
			this.buttonExit.Text = "Закрыть";
			this.toolTip.SetToolTip(this.buttonExit, "Вызов через - Esc");
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new EventHandler(this.buttonExit_Click);
			this.groupBoxInfo.Anchor = 3;
			this.groupBoxInfo.Controls.Add(this.listViewAllowedValues);
			this.groupBoxInfo.Location = new Point(10, 121);
			this.groupBoxInfo.Name = "groupBoxInfo";
			this.groupBoxInfo.Size = new Size(250, 311);
			this.groupBoxInfo.TabIndex = 4;
			this.groupBoxInfo.TabStop = false;
			this.groupBoxInfo.Text = "Допустимые значения";
			this.listViewAllowedValues.AllowDrop = true;
			this.listViewAllowedValues.BackColor = SystemColors.Window;
			this.listViewAllowedValues.BorderStyle = 1;
			this.listViewAllowedValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeaderN,
				this.columnHeaderValue
			});
			this.listViewAllowedValues.Dock = 5;
			this.listViewAllowedValues.ForeColor = Color.DarkBlue;
			this.listViewAllowedValues.FullRowSelect = true;
			this.listViewAllowedValues.GridLines = true;
			this.listViewAllowedValues.HeaderStyle = 1;
			this.listViewAllowedValues.HideSelection = false;
			this.listViewAllowedValues.Location = new Point(3, 16);
			this.listViewAllowedValues.MultiSelect = false;
			this.listViewAllowedValues.Name = "listViewAllowedValues";
			this.listViewAllowedValues.Size = new Size(244, 292);
			this.listViewAllowedValues.TabIndex = 5;
			this.toolTip.SetToolTip(this.listViewAllowedValues, "Drag&Drop - вставка значения на новое место\r\nDrag&Drop + Shift - перестановка значений местами");
			this.listViewAllowedValues.UseCompatibleStateImageBehavior = false;
			this.listViewAllowedValues.View = 1;
			this.listViewAllowedValues.DragDrop += new DragEventHandler(this.listViewAllowedValues_DragDrop);
			this.listViewAllowedValues.DragOver += new DragEventHandler(this.listViewAllowedValues_DragOver);
			this.listViewAllowedValues.ItemDrag += new ItemDragEventHandler(this.listViewAllowedValues_ItemDrag);
			this.columnHeaderN.Text = "№";
			this.columnHeaderN.Width = 40;
			this.columnHeaderValue.Text = "Значение";
			this.columnHeaderValue.Width = 200;
			this.groupBox1.Controls.Add(this.buttonEditDomain);
			this.groupBox1.Controls.Add(this.buttonRemoveDomain);
			this.groupBox1.Controls.Add(this.buttonAddDomain);
			this.groupBox1.Location = new Point(10, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(250, 112);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Действия";
			this.buttonEditDomain.FlatStyle = 0;
			this.buttonEditDomain.Location = new Point(6, 50);
			this.buttonEditDomain.Name = "buttonEditDomain";
			this.buttonEditDomain.Size = new Size(238, 23);
			this.buttonEditDomain.TabIndex = 3;
			this.buttonEditDomain.Text = "Редактировать";
			this.toolTip.SetToolTip(this.buttonEditDomain, "Вызов через -  Enter/Double Click");
			this.buttonEditDomain.UseVisualStyleBackColor = true;
			this.buttonEditDomain.Click += new EventHandler(this.buttonEditDomain_Click);
			this.buttonRemoveDomain.FlatStyle = 0;
			this.buttonRemoveDomain.Location = new Point(6, 79);
			this.buttonRemoveDomain.Name = "buttonRemoveDomain";
			this.buttonRemoveDomain.Size = new Size(238, 23);
			this.buttonRemoveDomain.TabIndex = 4;
			this.buttonRemoveDomain.Text = "Удалить";
			this.toolTip.SetToolTip(this.buttonRemoveDomain, "Вызов через - Del");
			this.buttonRemoveDomain.UseVisualStyleBackColor = true;
			this.buttonRemoveDomain.Click += new EventHandler(this.buttonRemoveDomain_Click);
			this.buttonAddDomain.FlatStyle = 0;
			this.buttonAddDomain.Location = new Point(6, 21);
			this.buttonAddDomain.Name = "buttonAddDomain";
			this.buttonAddDomain.Size = new Size(238, 23);
			this.buttonAddDomain.TabIndex = 2;
			this.buttonAddDomain.Text = "Добавить";
			this.toolTip.SetToolTip(this.buttonAddDomain, "Вызов через - Space");
			this.buttonAddDomain.UseVisualStyleBackColor = true;
			this.buttonAddDomain.Click += new EventHandler(this.buttonAddDomain_Click);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.groupBoxInfo);
			this.panel1.Controls.Add(this.buttonExit);
			this.panel1.Dock = 4;
			this.panel1.Location = new Point(402, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(270, 473);
			this.panel1.TabIndex = 0;
			this.listViewDomain.AllowDrop = true;
			this.listViewDomain.BorderStyle = 1;
			this.listViewDomain.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3
			});
			this.listViewDomain.Dock = 5;
			this.listViewDomain.ForeColor = Color.DarkBlue;
			this.listViewDomain.FullRowSelect = true;
			this.listViewDomain.GridLines = true;
			this.listViewDomain.HeaderStyle = 1;
			this.listViewDomain.HideSelection = false;
			this.listViewDomain.Location = new Point(0, 0);
			this.listViewDomain.MultiSelect = false;
			this.listViewDomain.Name = "listViewDomain";
			this.listViewDomain.Size = new Size(402, 473);
			this.listViewDomain.TabIndex = 0;
			this.toolTip.SetToolTip(this.listViewDomain, "Drag&Drop - вставка домена на новое место\r\nDrag&Drop + Shift - перестановка доменов местами");
			this.listViewDomain.UseCompatibleStateImageBehavior = false;
			this.listViewDomain.View = 1;
			this.listViewDomain.DragDrop += new DragEventHandler(this.listViewDomain_DragDrop);
			this.listViewDomain.DoubleClick += new EventHandler(this.listViewDomain_DoubleClick);
			this.listViewDomain.DragOver += new DragEventHandler(this.listViewDomain_DragOver);
			this.listViewDomain.SelectedIndexChanged += new EventHandler(this.listViewDomain_SelectedIndexChanged);
			this.listViewDomain.ItemDrag += new ItemDragEventHandler(this.listViewDomain_ItemDrag);
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 40;
			this.columnHeader2.Text = "Имя";
			this.columnHeader2.Width = 230;
			this.columnHeader3.Text = "Тип";
			this.columnHeader3.Width = 100;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(672, 473);
			base.Controls.Add(this.listViewDomain);
			base.Controls.Add(this.panel1);
			base.KeyPreview = true;
			this.MinimumSize = new Size(350, 250);
			base.Name = "FormDomainList";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			this.Text = "Список доменов";
			base.KeyDown += new KeyEventHandler(this.FormDomainList_KeyDown);
			this.groupBoxInfo.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
