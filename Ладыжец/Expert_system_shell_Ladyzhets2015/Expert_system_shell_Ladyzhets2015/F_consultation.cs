using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class F_consultation : Form
	{
		private IContainer components = null;

		private Label label1;

		public TextBox labelGoal;

		public ListBox listBoxDialog;

		private Label label2;

		public ComboBox comboBoxAnswer;

		public Button buttonAnswer;

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
			this.label1 = new Label();
			this.labelGoal = new TextBox();
			this.listBoxDialog = new ListBox();
			this.label2 = new Label();
			this.comboBoxAnswer = new ComboBox();
			this.buttonAnswer = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(109, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Цель консультации:";
			this.labelGoal.Location = new Point(128, 12);
			this.labelGoal.Name = "labelGoal";
			this.labelGoal.Size = new Size(342, 20);
			this.labelGoal.TabIndex = 1;
			this.listBoxDialog.FormattingEnabled = true;
			this.listBoxDialog.Location = new Point(16, 39);
			this.listBoxDialog.Name = "listBoxDialog";
			this.listBoxDialog.Size = new Size(454, 186);
			this.listBoxDialog.TabIndex = 2;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(16, 251);
			this.label2.Name = "label2";
			this.label2.Size = new Size(40, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Ответ:";
			this.comboBoxAnswer.DropDownStyle = 2;
			this.comboBoxAnswer.FormattingEnabled = true;
			this.comboBoxAnswer.Location = new Point(87, 248);
			this.comboBoxAnswer.Name = "comboBoxAnswer";
			this.comboBoxAnswer.Size = new Size(383, 21);
			this.comboBoxAnswer.TabIndex = 4;
			this.buttonAnswer.Location = new Point(294, 275);
			this.buttonAnswer.Name = "buttonAnswer";
			this.buttonAnswer.Size = new Size(176, 23);
			this.buttonAnswer.TabIndex = 5;
			this.buttonAnswer.Text = "Вперед   -->";
			this.buttonAnswer.UseVisualStyleBackColor = true;
			this.buttonAnswer.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(482, 306);
			base.Controls.Add(this.listBoxDialog);
			base.Controls.Add(this.buttonAnswer);
			base.Controls.Add(this.comboBoxAnswer);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.labelGoal);
			base.Controls.Add(this.label1);
			base.Name = "F_consultation";
			base.StartPosition = 1;
			this.Text = "Консультация";
			base.Load += new EventHandler(this.F_consultation_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public F_consultation()
		{
			this.InitializeComponent();
		}

		private void FormConsultation1_Load(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void F_consultation_Load(object sender, EventArgs e)
		{
		}
	}
}
