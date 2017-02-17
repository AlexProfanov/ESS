using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormConsultation : Form
	{
		private IContainer components;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private Button buttonAnswer;

		private GroupBox groupBox3;

		internal Label labelGoal;

		internal ComboBox comboBoxAnswer;

		internal ListBox listBoxDialog;

		public FormConsultation()
		{
			this.InitializeComponent();
		}

		private void buttonAnswer_Click(object sender, EventArgs e)
		{
			base.Close();
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
			this.groupBox1 = new GroupBox();
			this.listBoxDialog = new ListBox();
			this.groupBox2 = new GroupBox();
			this.comboBoxAnswer = new ComboBox();
			this.buttonAnswer = new Button();
			this.groupBox3 = new GroupBox();
			this.labelGoal = new Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.listBoxDialog);
			this.groupBox1.Location = new Point(12, 59);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(526, 220);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Диалог";
			this.listBoxDialog.BorderStyle = 1;
			this.listBoxDialog.Dock = 5;
			this.listBoxDialog.Font = new Font("Microsoft Sans Serif", 9.75f, 1, 3, 204);
			this.listBoxDialog.ForeColor = Color.DarkBlue;
			this.listBoxDialog.FormattingEnabled = true;
			this.listBoxDialog.ItemHeight = 16;
			this.listBoxDialog.Location = new Point(3, 16);
			this.listBoxDialog.Name = "listBoxDialog";
			this.listBoxDialog.Size = new Size(520, 194);
			this.listBoxDialog.TabIndex = 0;
			this.groupBox2.Controls.Add(this.comboBoxAnswer);
			this.groupBox2.Controls.Add(this.buttonAnswer);
			this.groupBox2.Location = new Point(12, 285);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(526, 49);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Ваш ответ";
			this.comboBoxAnswer.FlatStyle = 0;
			this.comboBoxAnswer.FormattingEnabled = true;
			this.comboBoxAnswer.Location = new Point(6, 19);
			this.comboBoxAnswer.Name = "comboBoxAnswer";
			this.comboBoxAnswer.Size = new Size(352, 21);
			this.comboBoxAnswer.TabIndex = 3;
			this.buttonAnswer.FlatStyle = 0;
			this.buttonAnswer.Location = new Point(364, 19);
			this.buttonAnswer.Name = "buttonAnswer";
			this.buttonAnswer.Size = new Size(156, 23);
			this.buttonAnswer.TabIndex = 2;
			this.buttonAnswer.Text = "Ответить";
			this.buttonAnswer.UseVisualStyleBackColor = true;
			this.buttonAnswer.Click += new EventHandler(this.buttonAnswer_Click);
			this.groupBox3.Controls.Add(this.labelGoal);
			this.groupBox3.Location = new Point(12, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(526, 45);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Цель консультации";
			this.labelGoal.AutoSize = true;
			this.labelGoal.Location = new Point(11, 19);
			this.labelGoal.Name = "labelGoal";
			this.labelGoal.Size = new Size(106, 13);
			this.labelGoal.TabIndex = 0;
			this.labelGoal.Text = "Цель консультации";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(550, 340);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = 3;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormConsultation";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			this.Text = "Консультация";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
