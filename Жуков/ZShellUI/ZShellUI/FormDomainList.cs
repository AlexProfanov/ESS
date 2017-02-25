using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZShellCore;

namespace ZShellUI
{
	public class FormDomainList : Form
	{
		private IContainer components = null;

		private Button buttonExit;

		private GroupBox groupBoxInfo;

		private Button buttonEditDomain;

		private Button buttonRemoveDomain;

		private Button buttonAddDomain;

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
				ListView.ListViewItemCollection arg_82_0 = this.listViewDomain.Items;
				ListViewItem listViewItem = new ListViewItem(num.ToString());
				listViewItem.SubItems.Add(enumeratorForEnumerations.Current.ToString());
				arg_82_0.Add(listViewItem);
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
					ListView.ListViewItemCollection arg_79_0 = this.listViewAllowedValues.Items;
					ListViewItem listViewItem = new ListViewItem(num.ToString());
					listViewItem.SubItems.Add(enumeratorForValues.Current.ToString());
					arg_79_0.Add(listViewItem);
					num++;
				}
				this.listViewAllowedValues.SelectedIndices.Add(0);
			}
		}

		private void buttonAddDomain_Click(object sender, EventArgs e)
		{
			int num = this.listViewDomain.Items.Count - 1;
			if (this.listViewDomain.SelectedIndices.Count > 0)
			{
				num = this.listViewDomain.SelectedIndices[0];
			}
			new FormAddEditDomain().addDomain(num);
			this.printDomainList();
			DialogFuncs.selectListViewItem(this.listViewDomain, num);
			this.listViewDomain.Focus();
		}

		private void buttonEditDomain_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					new FormAddEditDomain().editDomain(Global.knowledgeBase.getEnumerationAt(enumerationIndex));
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
			this.listViewDomain.Focus();
		}

		private void buttonRemoveDomain_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					if (MessageBox.Show("Вы действительно хотите удалить домен с именем " + Global.knowledgeBase.getEnumerationAt(enumerationIndex) + " ?.\nЭтот домен будет удален из всех переменных, которые на него ссылаются.\nКроме того, будут удалены все условия и заключения, которые используют его значения.", "Внимание", (System.Windows.Forms.MessageBoxButtons)4, (System.Windows.Forms.MessageBoxIcon)32) == (DialogResult)6)
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
			this.listViewDomain.Focus();
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
			if (MessageBox.Show("Вы действительно хотите переместить элемент ?", "Внимание", (System.Windows.Forms.MessageBoxButtons)4, (System.Windows.Forms.MessageBoxIcon)48) == (DialogResult)6)
			{
				if (e.Data.GetDataPresent(typeof(ListViewItem)))
				{
					ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
					Point point = this.listViewDomain.PointToClient(new Point(e.X, e.Y));
					ListViewItem itemAt = this.listViewDomain.GetItemAt(point.X, point.Y);
					if (itemAt != null)
					{
						if (e.Effect == (DragDropEffects)2)
						{
							Global.knowledgeBase.insertDomainInto(listViewItem.Index, itemAt.Index);
						}
						this.printDomainList();
					}
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
			if (MessageBox.Show("Вы действительно хотите переместить элемент ?", "Внимание", (System.Windows.Forms.MessageBoxButtons)4, (System.Windows.Forms.MessageBoxIcon)48) == (DialogResult)6)
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
							if (e.Effect == (DragDropEffects)2)
							{
								enumerationAt.insertValueInto(listViewItem.Index, itemAt.Index);
							}
							this.showDomainInfo(num);
						}
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
			if (keyCode != (Keys)13)
			{
				if (keyCode != (Keys)27)
				{
					if (keyCode == (Keys)46)
					{
						this.buttonRemoveDomain_Click(this, new EventArgs());
					}
				}
				else
				{
					this.buttonExit_Click(this, new EventArgs());
				}
			}
			else
			{
				this.buttonEditDomain_Click(this, new EventArgs());
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
			this.buttonEditDomain = new Button();
			this.buttonRemoveDomain = new Button();
			this.buttonAddDomain = new Button();
			this.listViewDomain = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.toolTip = new ToolTip(this.components);
			this.groupBoxInfo.SuspendLayout();
			base.SuspendLayout();
			this.buttonExit.Anchor = (System.Windows.Forms.AnchorStyles)10;
			this.buttonExit.Location = new Point(466, 407);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new Size(105, 23);
			this.buttonExit.TabIndex = 1;
			this.buttonExit.Text = "Закрыть";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new EventHandler(this.buttonExit_Click);
			this.groupBoxInfo.Anchor = (System.Windows.Forms.AnchorStyles)11;
			this.groupBoxInfo.Controls.Add(this.listViewAllowedValues);
			this.groupBoxInfo.Location = new Point(392, 0);
			this.groupBoxInfo.Name = "groupBoxInfo";
			this.groupBoxInfo.Size = new Size(259, 401);
			this.groupBoxInfo.TabIndex = 4;
			this.groupBoxInfo.TabStop = false;
			this.groupBoxInfo.Text = "Допустимые значения";
			this.listViewAllowedValues.AllowDrop = true;
			this.listViewAllowedValues.BackColor = SystemColors.Window;
			this.listViewAllowedValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeaderN,
				this.columnHeaderValue
			});
			this.listViewAllowedValues.Dock = (System.Windows.Forms.DockStyle)5;
			this.listViewAllowedValues.ForeColor = Color.DarkBlue;
			this.listViewAllowedValues.FullRowSelect = true;
			this.listViewAllowedValues.GridLines = true;
			this.listViewAllowedValues.HeaderStyle = (System.Windows.Forms.ColumnHeaderStyle)1;
			this.listViewAllowedValues.HideSelection = false;
			this.listViewAllowedValues.Location = new Point(3, 16);
			this.listViewAllowedValues.MultiSelect = false;
			this.listViewAllowedValues.Name = "listViewAllowedValues";
			this.listViewAllowedValues.Size = new Size(253, 382);
			this.listViewAllowedValues.TabIndex = 5;
			this.listViewAllowedValues.UseCompatibleStateImageBehavior = false;
			this.listViewAllowedValues.View = (System.Windows.Forms.View)1;
			this.listViewAllowedValues.ItemDrag += new ItemDragEventHandler(this.listViewAllowedValues_ItemDrag);
			this.listViewAllowedValues.DragDrop += new DragEventHandler(this.listViewAllowedValues_DragDrop);
			this.listViewAllowedValues.DragOver += new DragEventHandler(this.listViewAllowedValues_DragOver);
			this.columnHeaderN.Text = "№";
			this.columnHeaderN.Width = 40;
			this.columnHeaderValue.Text = "Значение";
			this.columnHeaderValue.Width = 200;
			this.buttonEditDomain.Anchor = (System.Windows.Forms.AnchorStyles)6;
			this.buttonEditDomain.Location = new Point(150, 407);
			this.buttonEditDomain.Name = "buttonEditDomain";
			this.buttonEditDomain.Size = new Size(105, 23);
			this.buttonEditDomain.TabIndex = 3;
			this.buttonEditDomain.Text = "Редактировать";
			this.buttonEditDomain.UseVisualStyleBackColor = true;
			this.buttonEditDomain.Click += new EventHandler(this.buttonEditDomain_Click);
			this.buttonRemoveDomain.Anchor = (System.Windows.Forms.AnchorStyles)6;
			this.buttonRemoveDomain.Location = new Point(261, 407);
			this.buttonRemoveDomain.Name = "buttonRemoveDomain";
			this.buttonRemoveDomain.Size = new Size(105, 23);
			this.buttonRemoveDomain.TabIndex = 4;
			this.buttonRemoveDomain.Text = "Удалить";
			this.buttonRemoveDomain.UseVisualStyleBackColor = true;
			this.buttonRemoveDomain.Click += new EventHandler(this.buttonRemoveDomain_Click);
			this.buttonAddDomain.Anchor = (System.Windows.Forms.AnchorStyles)6;
			this.buttonAddDomain.Location = new Point(39, 407);
			this.buttonAddDomain.Name = "buttonAddDomain";
			this.buttonAddDomain.Size = new Size(105, 23);
			this.buttonAddDomain.TabIndex = 2;
			this.buttonAddDomain.Text = "Добавить";
			this.buttonAddDomain.UseVisualStyleBackColor = true;
			this.buttonAddDomain.Click += new EventHandler(this.buttonAddDomain_Click);
			this.listViewDomain.AllowDrop = true;
			this.listViewDomain.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.listViewDomain.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewDomain.ForeColor = Color.DarkBlue;
			this.listViewDomain.FullRowSelect = true;
			this.listViewDomain.GridLines = true;
			this.listViewDomain.HeaderStyle = (System.Windows.Forms.ColumnHeaderStyle)1;
			this.listViewDomain.HideSelection = false;
			this.listViewDomain.Location = new Point(0, 0);
			this.listViewDomain.MultiSelect = false;
			this.listViewDomain.Name = "listViewDomain";
			this.listViewDomain.Size = new Size(389, 401);
			this.listViewDomain.TabIndex = 0;
			this.listViewDomain.UseCompatibleStateImageBehavior = false;
			this.listViewDomain.View = (System.Windows.Forms.View)1;
			this.listViewDomain.ItemDrag += new ItemDragEventHandler(this.listViewDomain_ItemDrag);
			this.listViewDomain.SelectedIndexChanged += new EventHandler(this.listViewDomain_SelectedIndexChanged);
			this.listViewDomain.DragDrop += new DragEventHandler(this.listViewDomain_DragDrop);
			this.listViewDomain.DragOver += new DragEventHandler(this.listViewDomain_DragOver);
			this.listViewDomain.DoubleClick += new EventHandler(this.listViewDomain_DoubleClick);
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 40;
			this.columnHeader2.Text = "Имя";
			this.columnHeader2.Width = 230;
			base.ClientSize = new Size(654, 442);
			base.Controls.Add(this.buttonExit);
			base.Controls.Add(this.groupBoxInfo);
			base.Controls.Add(this.buttonRemoveDomain);
			base.Controls.Add(this.buttonEditDomain);
			base.Controls.Add(this.listViewDomain);
			base.Controls.Add(this.buttonAddDomain);
			base.KeyPreview = true;
			this.MinimumSize = new Size(670, 480);
			base.Name = "FormDomainList";
			base.ShowInTaskbar = false;
			base.StartPosition = (System.Windows.Forms.FormStartPosition)4;
			this.Text = "Список доменов";
			base.KeyDown += new KeyEventHandler(this.FormDomainList_KeyDown);
			this.groupBoxInfo.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
