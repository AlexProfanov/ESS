using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyEs
{
	public class DomainsForm : Form
	{
		public int input;

		public int output;

		public int index;

		public KnowledgeBase KB;

		private int IdDomain;

		public bool Context;

		private IContainer components = null;

		private Label LbName;

		public TextBox TxtName;

		private Button BtnAdd;

		private Button BtnChange;

		private Button BtnRemove;

		private MyDGV GridDomain;

		private Button BtnOk;

		private Button BtnCancel;

		private Button BtnOkContinue;

		private DataGridViewTextBoxColumn Column1;

		public DomainsForm()
		{
			this.InitializeComponent();
			this.input = -1;
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private bool TestDomain()
		{
			bool result;
			if (this.TxtName.Text.Trim() == "")
			{
				MessageBox.Show("У домена должно быть имя");
				this.TxtName.Select();
				result = false;
			}
			else
			{
				int idByName = this.KB.Domains.GetIdByName(this.TxtName.Text);
				if (this.input == -1 && idByName > -1)
				{
					MessageBox.Show("Домен с таким именем уже есть");
					this.TxtName.Select();
					result = false;
				}
				else if (this.input > -1 && idByName > -1 && idByName != this.IdDomain && idByName != this.input)
				{
					MessageBox.Show("Домен с таким именем уже есть");
					this.TxtName.Select();
					result = false;
				}
				else if (this.GridDomain.RowCount == 0)
				{
					MessageBox.Show("У домена должны быть значения");
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		private void BtnOkContinue_Click(object sender, EventArgs e)
		{
			if (this.TestDomain())
			{
				this.KB.Domains.ChangeDomainName(this.IdDomain, this.TxtName.Text);
				this.output = this.IdDomain;
				base.DialogResult = 4;
				base.Close();
			}
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			if (this.TestDomain())
			{
				if (this.input == -1)
				{
					this.KB.Domains.ChangeDomainName(this.IdDomain, this.TxtName.Text);
					this.output = this.IdDomain;
				}
				else
				{
					this.KB.Domains.RemoveDomain(this.input);
					this.KB.Domains.ChangeDomainName(this.IdDomain, this.TxtName.Text);
					this.KB.Domains.PasteDomain(this.index, this.input, (Domain)this.KB.Domains.Domains[this.IdDomain].Clone());
					this.KB.Domains.RemoveDomain(this.IdDomain);
				}
				base.DialogResult = 1;
				base.Close();
			}
		}

		private void DomainsForm_Load(object sender, EventArgs e)
		{
			if (this.input == -1)
			{
				if (!this.Context)
				{
					this.BtnOkContinue.Enabled = true;
					this.BtnOkContinue.Visible = true;
				}
				this.Text = "Creating new domain";
				this.IdDomain = this.KB.Domains.AddDomain(this.index);
			}
			else
			{
				this.IdDomain = this.KB.Domains.CloneDomain(this.input);
				Domain domainById = this.KB.Domains.GetDomainById(this.IdDomain);
				this.TxtName.Text = domainById.Name;
				this.Text = this.Text + " " + domainById.Name;
				using (List<KeyValuePair<int, string>>.Enumerator enumerator = Enumerable.ToList<KeyValuePair<int, string>>(domainById.DomValues).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, string> current = enumerator.Current;
						int num = this.GridDomain.Rows.Add();
						this.GridDomain.Rows[num].Cells[0].Value = current.Value;
						this.GridDomain.Rows[num].Tag = current.Key;
					}
				}
				this.BtnChange.Enabled = true;
				this.BtnRemove.Enabled = true;
			}
		}

		private void GridDomain_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			this.BtnChange.Enabled = true;
			this.BtnRemove.Enabled = true;
		}

		private void GridDomain_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (this.GridDomain.RowCount == 0)
			{
				this.BtnChange.Enabled = false;
				this.BtnRemove.Enabled = false;
			}
		}

		private void BtnAdd_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.GridDomain.CurrentRow != null)
			{
				num = this.GridDomain.CurrentRow.Index + 1;
			}
			DialogResult dialogResult;
			do
			{
				ChangeValue changeValue = new ChangeValue();
				changeValue.IdDomain = this.IdDomain;
				changeValue.KB = this.KB;
				changeValue.index = num;
				dialogResult = changeValue.ShowDialog(this);
				if (dialogResult != 2)
				{
					this.GridDomain.Rows.Insert(num, 1);
					this.GridDomain[0, num].Value = this.KB.Domains.GetDomainValueNameById(this.IdDomain, changeValue.output);
					this.GridDomain.Rows[num].Tag = changeValue.output;
					this.GridDomain.CurrentCell = this.GridDomain[0, num];
				}
				num++;
			}
			while (dialogResult == 4);
		}

		private void BtnChange_Click(object sender, EventArgs e)
		{
			ChangeValue changeValue = new ChangeValue();
			changeValue.IdDomain = this.IdDomain;
			changeValue.KB = this.KB;
			changeValue.index = this.index;
			changeValue.input = Convert.ToInt32(this.GridDomain.CurrentRow.Tag);
			changeValue.ShowDialog(this);
			if (changeValue.DialogResult == 1)
			{
				this.GridDomain.CurrentRow.Cells[0].Value = this.KB.Domains.GetDomainValueNameById(this.IdDomain, changeValue.input);
			}
		}

		private void BtnRemove_Click(object sender, EventArgs e)
		{
			int idDomainValue = Convert.ToInt32(this.GridDomain.CurrentRow.Tag);
			bool flag;
			if (this.input > -1)
			{
				flag = this.KB.Rules.TestForValue(this.input, idDomainValue);
			}
			else
			{
				flag = this.KB.Rules.TestForValue(this.IdDomain, idDomainValue);
			}
			if (flag)
			{
				this.KB.Domains.RemoveDomainValue(this.IdDomain, idDomainValue);
				this.GridDomain.Rows.Remove(this.GridDomain.CurrentRow);
			}
			else
			{
				MessageBox.Show("Данное значение используется в правилах");
			}
		}

		private void DomainsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == 2)
			{
				this.KB.Domains.RemoveDomain(this.IdDomain);
			}
		}

		private void GridDomain_DragDrop(object sender, DragEventArgs e)
		{
			int dragDropSourceIndex = this.GridDomain.DragDropSourceIndex;
			int dragDropTargetIndex = this.GridDomain.DragDropTargetIndex;
			if (dragDropTargetIndex > -1)
			{
				this.KB.Domains.ReplaceDomainValues(this.IdDomain, dragDropSourceIndex, dragDropTargetIndex);
				this.GridDomain.CurrentCell = this.GridDomain.Rows[dragDropTargetIndex].Cells[0];
			}
		}

		private void GridDomain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (this.GridDomain.CurrentRow != null)
			{
				this.BtnChange_Click(sender, new EventArgs());
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
			this.LbName = new Label();
			this.TxtName = new TextBox();
			this.BtnAdd = new Button();
			this.BtnChange = new Button();
			this.BtnRemove = new Button();
			this.BtnOk = new Button();
			this.BtnCancel = new Button();
			this.BtnOkContinue = new Button();
			this.GridDomain = new MyDGV();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.GridDomain.BeginInit();
			base.SuspendLayout();
			this.LbName.AutoSize = true;
			this.LbName.Location = new Point(19, 19);
			this.LbName.Name = "LbName";
			this.LbName.Size = new Size(82, 13);
			this.LbName.TabIndex = 0;
			this.LbName.Text = "Domain's name:";
			this.TxtName.Location = new Point(116, 14);
			this.TxtName.Name = "TxtName";
			this.TxtName.Size = new Size(312, 20);
			this.TxtName.TabIndex = 1;
			this.BtnAdd.Location = new Point(22, 74);
			this.BtnAdd.Name = "BtnAdd";
			this.BtnAdd.Size = new Size(75, 37);
			this.BtnAdd.TabIndex = 3;
			this.BtnAdd.Text = "Add value";
			this.BtnAdd.UseVisualStyleBackColor = true;
			this.BtnAdd.Click += new EventHandler(this.BtnAdd_Click);
			this.BtnChange.Enabled = false;
			this.BtnChange.Location = new Point(22, 129);
			this.BtnChange.Name = "BtnChange";
			this.BtnChange.Size = new Size(75, 37);
			this.BtnChange.TabIndex = 4;
			this.BtnChange.Text = "Change value";
			this.BtnChange.UseVisualStyleBackColor = true;
			this.BtnChange.Click += new EventHandler(this.BtnChange_Click);
			this.BtnRemove.Enabled = false;
			this.BtnRemove.Location = new Point(22, 183);
			this.BtnRemove.Name = "BtnRemove";
			this.BtnRemove.Size = new Size(75, 37);
			this.BtnRemove.TabIndex = 5;
			this.BtnRemove.Text = "Remove value";
			this.BtnRemove.UseVisualStyleBackColor = true;
			this.BtnRemove.Click += new EventHandler(this.BtnRemove_Click);
			this.BtnOk.Location = new Point(203, 250);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new Size(139, 23);
			this.BtnOk.TabIndex = 7;
			this.BtnOk.Text = "Apply and exit";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new EventHandler(this.BtnOk_Click);
			this.BtnCancel.DialogResult = 2;
			this.BtnCancel.Location = new Point(353, 250);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new Size(75, 23);
			this.BtnCancel.TabIndex = 8;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new EventHandler(this.BtnCancel_Click);
			this.BtnOkContinue.Enabled = false;
			this.BtnOkContinue.Location = new Point(42, 250);
			this.BtnOkContinue.Name = "BtnOkContinue";
			this.BtnOkContinue.Size = new Size(152, 23);
			this.BtnOkContinue.TabIndex = 6;
			this.BtnOkContinue.Text = "Apply and continue";
			this.BtnOkContinue.UseVisualStyleBackColor = true;
			this.BtnOkContinue.Visible = false;
			this.BtnOkContinue.Click += new EventHandler(this.BtnOkContinue_Click);
			this.GridDomain.AllowDrop = true;
			this.GridDomain.AllowUserToAddRows = false;
			this.GridDomain.AllowUserToDeleteRows = false;
			this.GridDomain.AllowUserToResizeRows = false;
			this.GridDomain.ColumnHeadersHeightSizeMode = 2;
			this.GridDomain.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column1
			});
			this.GridDomain.EditMode = 0;
			this.GridDomain.Location = new Point(116, 52);
			this.GridDomain.MultiSelect = false;
			this.GridDomain.Name = "GridDomain";
			this.GridDomain.ReadOnly = true;
			this.GridDomain.RowHeadersWidthSizeMode = 1;
			this.GridDomain.SelectionMode = 1;
			this.GridDomain.Size = new Size(312, 183);
			this.GridDomain.TabIndex = 2;
			this.GridDomain.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.GridDomain_CellMouseDoubleClick);
			this.GridDomain.RowsAdded += new DataGridViewRowsAddedEventHandler(this.GridDomain_RowsAdded);
			this.GridDomain.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.GridDomain_RowsRemoved);
			this.GridDomain.DragDrop += new DragEventHandler(this.GridDomain_DragDrop);
			this.Column1.AutoSizeMode = 16;
			this.Column1.HeaderText = "Domain's values";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.SortMode = 0;
			base.AcceptButton = this.BtnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.CancelButton = this.BtnCancel;
			base.ClientSize = new Size(445, 284);
			base.Controls.Add(this.BtnOkContinue);
			base.Controls.Add(this.BtnCancel);
			base.Controls.Add(this.BtnOk);
			base.Controls.Add(this.GridDomain);
			base.Controls.Add(this.BtnRemove);
			base.Controls.Add(this.BtnChange);
			base.Controls.Add(this.BtnAdd);
			base.Controls.Add(this.TxtName);
			base.Controls.Add(this.LbName);
			base.FormBorderStyle = 2;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DomainsForm";
			base.StartPosition = 4;
			this.Text = "Domain editing";
			base.FormClosing += new FormClosingEventHandler(this.DomainsForm_FormClosing);
			base.Load += new EventHandler(this.DomainsForm_Load);
			this.GridDomain.EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
