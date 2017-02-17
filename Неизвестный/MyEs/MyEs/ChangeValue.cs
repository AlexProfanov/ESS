using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyEs
{
	public class ChangeValue : Form
	{
		public int input;

		public int index;

		public int output;

		public int IdDomain;

		public KnowledgeBase KB;

		public bool Context;

		private IContainer components = null;

		public TextBox TxtValue;

		private Button BtnOk;

		private Button BtnCancel;

		private Button BtnOkContinue;

		public ChangeValue()
		{
			this.InitializeComponent();
			this.input = -1;
			this.Context = false;
		}

		private bool TestDomainValue()
		{
			bool result;
			if (this.TxtValue.Text.Trim() == "")
			{
				MessageBox.Show("Значение не может быть пустым");
				result = false;
			}
			else
			{
				int idDomainValueByName = this.KB.Domains.GetIdDomainValueByName(this.IdDomain, this.TxtValue.Text);
				if ((this.input == -1 && idDomainValueByName > -1) || (this.input > -1 && idDomainValueByName > -1 && idDomainValueByName != this.input))
				{
					MessageBox.Show("Подобное значение уже есть");
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			if (this.TestDomainValue())
			{
				if (this.input == -1)
				{
					int idDomainValue = this.KB.Domains.AddDomainValue(this.IdDomain, this.index);
					this.KB.Domains.ChangeDomainValue(this.IdDomain, idDomainValue, this.TxtValue.Text);
					this.output = idDomainValue;
				}
				else
				{
					this.KB.Domains.ChangeDomainValue(this.IdDomain, this.input, this.TxtValue.Text);
				}
				base.DialogResult = 1;
				base.Close();
			}
		}

		private void BtnOkContinue_Click(object sender, EventArgs e)
		{
			if (this.TestDomainValue())
			{
				int idDomainValue = this.KB.Domains.AddDomainValue(this.IdDomain, this.index);
				this.KB.Domains.ChangeDomainValue(this.IdDomain, idDomainValue, this.TxtValue.Text);
				this.output = idDomainValue;
				base.DialogResult = 4;
				base.Close();
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void ChangeValue_Load(object sender, EventArgs e)
		{
			if (this.input == -1)
			{
				if (!this.Context)
				{
					this.BtnOkContinue.Enabled = true;
					this.BtnOkContinue.Visible = true;
				}
				this.Text = "Adding new value";
			}
			else
			{
				this.Text = this.Text + " " + this.KB.Domains.GetDomainValueNameById(this.IdDomain, this.input);
				this.TxtValue.Text = this.KB.Domains.GetDomainValueNameById(this.IdDomain, this.input);
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
			this.TxtValue = new TextBox();
			this.BtnOk = new Button();
			this.BtnCancel = new Button();
			this.BtnOkContinue = new Button();
			base.SuspendLayout();
			this.TxtValue.Location = new Point(13, 13);
			this.TxtValue.Name = "TxtValue";
			this.TxtValue.Size = new Size(408, 20);
			this.TxtValue.TabIndex = 0;
			this.BtnOk.Location = new Point(192, 40);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new Size(139, 23);
			this.BtnOk.TabIndex = 2;
			this.BtnOk.Text = "Apply and exit";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new EventHandler(this.BtnOk_Click);
			this.BtnCancel.DialogResult = 2;
			this.BtnCancel.Location = new Point(346, 40);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new Size(75, 23);
			this.BtnCancel.TabIndex = 3;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new EventHandler(this.BtnCancel_Click);
			this.BtnOkContinue.Enabled = false;
			this.BtnOkContinue.Location = new Point(28, 40);
			this.BtnOkContinue.Name = "BtnOkContinue";
			this.BtnOkContinue.Size = new Size(152, 23);
			this.BtnOkContinue.TabIndex = 1;
			this.BtnOkContinue.Text = "Apply and continue";
			this.BtnOkContinue.UseVisualStyleBackColor = true;
			this.BtnOkContinue.Visible = false;
			this.BtnOkContinue.Click += new EventHandler(this.BtnOkContinue_Click);
			base.AcceptButton = this.BtnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.CancelButton = this.BtnCancel;
			base.ClientSize = new Size(433, 70);
			base.Controls.Add(this.BtnOkContinue);
			base.Controls.Add(this.BtnCancel);
			base.Controls.Add(this.BtnOk);
			base.Controls.Add(this.TxtValue);
			base.FormBorderStyle = 2;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChangeValue";
			base.StartPosition = 4;
			this.Text = "Value editing";
			base.Load += new EventHandler(this.ChangeValue_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
