using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Оболочка_Буряк
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
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.listBoxDialog);
			this.groupBox1.Location = new Point(12, 53);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(524, 215);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Диалог";
			this.listBoxDialog.FormattingEnabled = true;
			this.listBoxDialog.Location = new Point(4, 15);
			this.listBoxDialog.Name = "listBoxDialog";
			this.listBoxDialog.Size = new Size(514, 186);
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
			this.buttonAnswer.Location = new Point(378, 317);
			this.buttonAnswer.Name = "buttonAnswer";
			this.buttonAnswer.Size = new Size(158, 23);
			this.buttonAnswer.TabIndex = 6;
			this.buttonAnswer.Text = "Следующий вопрос";
			this.buttonAnswer.UseVisualStyleBackColor = true;
			this.buttonAnswer.add_Click(new EventHandler(this.buttonAnswer_Click));
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = SystemColors.Highlight;
			base.ClientSize = new Size(557, 352);
			base.Controls.Add(this.buttonAnswer);
			base.Controls.Add(this.comboBoxAnswer);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.labelGoal);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = 3;
			base.Name = "FormConsultation1";
			this.Text = "Консультация";
			base.add_Load(new EventHandler(this.FormConsultation1_Load));
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormConsultation1()
		{
			this.InitializeComponent();
		}

		private void FormConsultation1_Load(object sender, EventArgs e)
		{
		}

		private void buttonAnswer_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
