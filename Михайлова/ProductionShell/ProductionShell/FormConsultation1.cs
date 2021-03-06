using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormConsultation1 : Form
	{
		private IContainer components = null;

		private GroupBox groupBox1;

		public Label label1;

		public Label labelGoal;

		private Label label3;

		public ComboBox comboBoxAnswer;

		public Button buttonAnswer;

		public ListBox listBoxDialog;

		private Button button1;

		public bool flag_exit;

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
			this.label1 = new Label();
			this.labelGoal = new Label();
			this.label3 = new Label();
			this.comboBoxAnswer = new ComboBox();
			this.buttonAnswer = new Button();
			this.button1 = new Button();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.listBoxDialog);
			this.groupBox1.Location = new Point(12, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(524, 228);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Диалог";
			this.listBoxDialog.FormattingEnabled = true;
			this.listBoxDialog.Location = new Point(4, 15);
			this.listBoxDialog.Name = "listBoxDialog";
			this.listBoxDialog.Size = new Size(514, 199);
			this.listBoxDialog.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(19, 24);
			this.label1.Name = "label1";
			this.label1.Size = new Size(109, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Цель консультации:";
			this.labelGoal.AutoSize = true;
			this.labelGoal.Location = new Point(154, 24);
			this.labelGoal.Name = "labelGoal";
			this.labelGoal.Size = new Size(35, 13);
			this.labelGoal.TabIndex = 3;
			this.labelGoal.Text = "label2";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(13, 282);
			this.label3.Name = "label3";
			this.label3.Size = new Size(59, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Ваш ответ";
			this.comboBoxAnswer.DropDownStyle = 2;
			this.comboBoxAnswer.FlatStyle = 0;
			this.comboBoxAnswer.FormattingEnabled = true;
			this.comboBoxAnswer.Location = new Point(94, 282);
			this.comboBoxAnswer.Name = "comboBoxAnswer";
			this.comboBoxAnswer.Size = new Size(343, 21);
			this.comboBoxAnswer.TabIndex = 5;
			this.buttonAnswer.Location = new Point(199, 317);
			this.buttonAnswer.Name = "buttonAnswer";
			this.buttonAnswer.Size = new Size(158, 23);
			this.buttonAnswer.TabIndex = 6;
			this.buttonAnswer.Text = "Следующий вопрос";
			this.buttonAnswer.UseVisualStyleBackColor = true;
			this.buttonAnswer.Click += new EventHandler(this.buttonAnswer_Click);
			this.button1.Location = new Point(376, 317);
			this.button1.Name = "button1";
			this.button1.Size = new Size(160, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "Закончить консультацию";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = Color.LavenderBlush;
			base.ClientSize = new Size(557, 352);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.buttonAnswer);
			base.Controls.Add(this.comboBoxAnswer);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.labelGoal);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = 1;
			base.Name = "FormConsultation1";
			base.StartPosition = 1;
			this.Text = "Консультация";
			base.FormClosing += new FormClosingEventHandler(this.FormConsultation1_FormClosing);
			base.Load += new EventHandler(this.FormConsultation1_Load);
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormConsultation1()
		{
			this.InitializeComponent();
			this.flag_exit = false;
		}

		private void FormConsultation1_Load(object sender, EventArgs e)
		{
		}

		private void buttonAnswer_Click(object sender, EventArgs e)
		{
			this.flag_exit = false;
			base.Close();
		}

		private void FormConsultation1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == 3)
			{
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.flag_exit = true;
			base.Close();
		}
	}
}
