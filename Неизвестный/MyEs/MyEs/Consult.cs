using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyEs
{
	public class Consult : Form
	{
		public bool success;

		public int IdAnswer;

		private List<RadioButton> rb = new List<RadioButton>();

		private IContainer components = null;

		private Button BtnNext;

		private Panel RadioPanel;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;

		private Button BtnExit;

		private Label label1;

		public Consult()
		{
			this.InitializeComponent();
		}

		public void SetQuestion(string quest, Dictionary<int, string> variants)
		{
			this.label1.Text = quest;
			int num = 10;
			if (this.label1.Height > 18)
			{
				this.RadioPanel.Top = this.label1.Bottom + 20;
				base.Height = this.RadioPanel.Bottom + 90;
			}
			using (Dictionary<int, string>.Enumerator enumerator = variants.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, string> current = enumerator.Current;
					this.AddRadio(num, current.Value, current.Key);
					num += 25;
				}
			}
			if (num > this.RadioPanel.Height)
			{
				this.RadioPanel.Height = num + 20;
				base.Height = this.RadioPanel.Bottom + 90;
			}
			this.rb[0].Checked = true;
		}

		private void AddRadio(int location, string text, int id)
		{
			RadioButton radioButton = new RadioButton();
			this.rb.Add(radioButton);
			radioButton.AutoSize = true;
			radioButton.Location = new Point(10, location);
			radioButton.Name = "rad" + text;
			radioButton.Size = new Size(85, 17);
			radioButton.TabIndex = 0;
			radioButton.TabStop = true;
			radioButton.Text = text;
			radioButton.Tag = id;
			radioButton.CheckedChanged += new EventHandler(this.rad_CheckedChanged);
			radioButton.UseVisualStyleBackColor = true;
			this.RadioPanel.Controls.Add(radioButton);
			radioButton.Checked = true;
		}

		private void rad_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				this.IdAnswer = Convert.ToInt32(((RadioButton)sender).Tag);
			}
		}

		private void BtnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void Consult_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Вы уверены, что хотите выйти?", "", 4, 0, 256) == 6)
			{
				this.success = false;
			}
			else
			{
				e.Cancel = true;
			}
		}

		private void BtnNext_Click(object sender, EventArgs e)
		{
			this.success = true;
			base.remove_FormClosing(new FormClosingEventHandler(this.Consult_FormClosing));
			base.Close();
		}

		private void Consult_Load(object sender, EventArgs e)
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
			this.BtnNext = new Button();
			this.BtnExit = new Button();
			this.RadioPanel = new Panel();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn17 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn18 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn19 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn20 = new DataGridViewTextBoxColumn();
			this.label1 = new Label();
			base.SuspendLayout();
			this.BtnNext.Anchor = 2;
			this.BtnNext.Location = new Point(143, 176);
			this.BtnNext.Name = "BtnNext";
			this.BtnNext.Size = new Size(96, 32);
			this.BtnNext.TabIndex = 1;
			this.BtnNext.Text = "Далее";
			this.BtnNext.UseVisualStyleBackColor = true;
			this.BtnNext.Click += new EventHandler(this.BtnNext_Click);
			this.BtnExit.Anchor = 2;
			this.BtnExit.DialogResult = 2;
			this.BtnExit.Location = new Point(275, 176);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new Size(88, 32);
			this.BtnExit.TabIndex = 2;
			this.BtnExit.Text = "Выйти";
			this.BtnExit.UseVisualStyleBackColor = true;
			this.BtnExit.Click += new EventHandler(this.BtnExit_Click);
			this.RadioPanel.BorderStyle = 1;
			this.RadioPanel.Location = new Point(28, 47);
			this.RadioPanel.Name = "RadioPanel";
			this.RadioPanel.Size = new Size(463, 117);
			this.RadioPanel.TabIndex = 0;
			this.dataGridViewTextBoxColumn2.HeaderText = "column 1";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.SortMode = 2;
			this.dataGridViewTextBoxColumn3.HeaderText = "column 2";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.SortMode = 2;
			this.dataGridViewTextBoxColumn4.HeaderText = "column 3";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.SortMode = 2;
			this.dataGridViewTextBoxColumn5.HeaderText = "column 4";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.SortMode = 2;
			this.dataGridViewTextBoxColumn6.HeaderText = "column 5";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.SortMode = 2;
			this.dataGridViewTextBoxColumn7.HeaderText = "column 6";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.SortMode = 2;
			this.dataGridViewTextBoxColumn8.HeaderText = "column 7";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.SortMode = 2;
			this.dataGridViewTextBoxColumn9.HeaderText = "column 8";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.SortMode = 2;
			this.dataGridViewTextBoxColumn10.HeaderText = "column 9";
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.SortMode = 2;
			this.dataGridViewTextBoxColumn11.HeaderText = "column 10";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.SortMode = 2;
			this.dataGridViewTextBoxColumn12.HeaderText = "column 11";
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			this.dataGridViewTextBoxColumn12.SortMode = 2;
			this.dataGridViewTextBoxColumn13.HeaderText = "column 12";
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			this.dataGridViewTextBoxColumn13.SortMode = 2;
			this.dataGridViewTextBoxColumn14.HeaderText = "column 13";
			this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			this.dataGridViewTextBoxColumn14.SortMode = 2;
			this.dataGridViewTextBoxColumn15.HeaderText = "column 14";
			this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
			this.dataGridViewTextBoxColumn15.SortMode = 2;
			this.dataGridViewTextBoxColumn16.HeaderText = "column 15";
			this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
			this.dataGridViewTextBoxColumn16.SortMode = 2;
			this.dataGridViewTextBoxColumn17.HeaderText = "column 16";
			this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
			this.dataGridViewTextBoxColumn17.SortMode = 2;
			this.dataGridViewTextBoxColumn18.HeaderText = "column 17";
			this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
			this.dataGridViewTextBoxColumn18.SortMode = 2;
			this.dataGridViewTextBoxColumn19.HeaderText = "column 18";
			this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
			this.dataGridViewTextBoxColumn19.SortMode = 2;
			this.dataGridViewTextBoxColumn20.HeaderText = "column 19";
			this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
			this.dataGridViewTextBoxColumn20.SortMode = 2;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Times New Roman", 11.25f, 3, 3, 204);
			this.label1.Location = new Point(25, 9);
			this.label1.MaximumSize = new Size(463, 0);
			this.label1.MinimumSize = new Size(463, 18);
			this.label1.Name = "label1";
			this.label1.Size = new Size(463, 18);
			this.label1.TabIndex = 6;
			this.label1.Text = "label1";
			base.AcceptButton = this.BtnNext;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.CancelButton = this.BtnExit;
			base.ClientSize = new Size(513, 216);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.RadioPanel);
			base.Controls.Add(this.BtnExit);
			base.Controls.Add(this.BtnNext);
			base.FormBorderStyle = 2;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Consult";
			base.StartPosition = 4;
			this.Text = "Консультация";
			base.Load += new EventHandler(this.Consult_Load);
			base.FormClosing += new FormClosingEventHandler(this.Consult_FormClosing);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
