using ShellES.Components;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShellES
{
	public class ConsultForm : Form
	{
		private IContainer components;

		private GroupBox groupBox10;

		private ComboBox cmbAnswer;

		private RadioButton RadBtn4;

		private RadioButton RadBtn3;

		private RadioButton RadBtn2;

		private RadioButton RadBtn1;

		private Button btnNext;

		private GroupBox groupBox9;

		private Button btnInterrupt;

		public GroupBox groupBox8;

		internal Label LblQuest;

		private ExpertSystemShell ess;

		private ReceiveKnowleageComponent rkc;

		public Point userFormLocation;

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
			this.groupBox8 = new GroupBox();
			this.btnInterrupt = new Button();
			this.groupBox10 = new GroupBox();
			this.cmbAnswer = new ComboBox();
			this.RadBtn4 = new RadioButton();
			this.RadBtn3 = new RadioButton();
			this.RadBtn2 = new RadioButton();
			this.RadBtn1 = new RadioButton();
			this.btnNext = new Button();
			this.groupBox9 = new GroupBox();
			this.LblQuest = new Label();
			this.groupBox8.SuspendLayout();
			this.groupBox10.SuspendLayout();
			this.groupBox9.SuspendLayout();
			base.SuspendLayout();
			this.groupBox8.Controls.Add(this.btnInterrupt);
			this.groupBox8.Controls.Add(this.groupBox10);
			this.groupBox8.Controls.Add(this.btnNext);
			this.groupBox8.Controls.Add(this.groupBox9);
			this.groupBox8.Dock = DockStyle.Fill;
			this.groupBox8.Location = new Point(0, 0);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new Size(593, 241);
			this.groupBox8.TabIndex = 3;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Консультация";
			this.btnInterrupt.DialogResult = DialogResult.Cancel;
			this.btnInterrupt.Location = new Point(6, 186);
			this.btnInterrupt.Name = "btnInterrupt";
			this.btnInterrupt.Size = new Size(163, 34);
			this.btnInterrupt.TabIndex = 10;
			this.btnInterrupt.Text = "Прервать консультацию";
			this.btnInterrupt.UseVisualStyleBackColor = true;
			this.btnInterrupt.Click += new EventHandler(this.btnInterrupt_Click);
			this.groupBox10.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.groupBox10.Controls.Add(this.cmbAnswer);
			this.groupBox10.Controls.Add(this.RadBtn4);
			this.groupBox10.Controls.Add(this.RadBtn3);
			this.groupBox10.Controls.Add(this.RadBtn2);
			this.groupBox10.Controls.Add(this.RadBtn1);
			this.groupBox10.Location = new Point(9, 72);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new Size(578, 96);
			this.groupBox10.TabIndex = 7;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "Выберите ответ:";
			this.cmbAnswer.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbAnswer.FormattingEnabled = true;
			this.cmbAnswer.Location = new Point(205, 43);
			this.cmbAnswer.Name = "cmbAnswer";
			this.cmbAnswer.Size = new Size(184, 21);
			this.cmbAnswer.TabIndex = 10;
			this.cmbAnswer.Visible = false;
			this.RadBtn4.Location = new Point(265, 56);
			this.RadBtn4.Name = "RadBtn4";
			this.RadBtn4.Size = new Size(229, 19);
			this.RadBtn4.TabIndex = 8;
			this.RadBtn4.TabStop = true;
			this.RadBtn4.UseVisualStyleBackColor = true;
			this.RadBtn4.Visible = false;
			this.RadBtn3.Location = new Point(265, 31);
			this.RadBtn3.Name = "RadBtn3";
			this.RadBtn3.Size = new Size(229, 19);
			this.RadBtn3.TabIndex = 7;
			this.RadBtn3.TabStop = true;
			this.RadBtn3.UseVisualStyleBackColor = true;
			this.RadBtn3.Visible = false;
			this.RadBtn2.Location = new Point(25, 56);
			this.RadBtn2.Name = "RadBtn2";
			this.RadBtn2.Size = new Size(229, 19);
			this.RadBtn2.TabIndex = 6;
			this.RadBtn2.TabStop = true;
			this.RadBtn2.UseVisualStyleBackColor = true;
			this.RadBtn2.Visible = false;
			this.RadBtn1.Location = new Point(25, 31);
			this.RadBtn1.Name = "RadBtn1";
			this.RadBtn1.Size = new Size(229, 19);
			this.RadBtn1.TabIndex = 5;
			this.RadBtn1.TabStop = true;
			this.RadBtn1.UseVisualStyleBackColor = true;
			this.RadBtn1.Visible = false;
			this.btnNext.Location = new Point(481, 186);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new Size(100, 34);
			this.btnNext.TabIndex = 9;
			this.btnNext.Text = "Далее";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new EventHandler(this.btnNext_Click);
			this.groupBox9.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.groupBox9.Controls.Add(this.LblQuest);
			this.groupBox9.Location = new Point(9, 19);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new Size(578, 53);
			this.groupBox9.TabIndex = 6;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Вопрос:";
			this.LblQuest.Dock = DockStyle.Fill;
			this.LblQuest.Location = new Point(3, 16);
			this.LblQuest.Margin = new Padding(0);
			this.LblQuest.Name = "LblQuest";
			this.LblQuest.Padding = new Padding(2, 0, 2, 2);
			this.LblQuest.Size = new Size(572, 34);
			this.LblQuest.TabIndex = 1;
			this.LblQuest.Text = "Вопрос:";
			this.LblQuest.TextAlign = ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.btnInterrupt;
			base.ClientSize = new Size(593, 241);
			base.Controls.Add(this.groupBox8);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Name = "ConsultForm";
			this.Text = "Консультация:";
			base.Load += new EventHandler(this.ConsultForm_Load);
			base.FormClosing += new FormClosingEventHandler(this.ConsultForm_FormClosing);
			this.groupBox8.ResumeLayout(false);
			this.groupBox10.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public ConsultForm(ReceiveKnowleageComponent RKC, ExpertSystemShell ESS)
		{
			this.InitializeComponent();
			this.userFormLocation = new Point((Screen.PrimaryScreen.Bounds.Width - base.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - base.Height) / 2);
			this.rkc = RKC;
			this.rkc.BindingControls(this.LblQuest, this.cmbAnswer, new RadioButton[]
			{
				this.RadBtn1,
				this.RadBtn2,
				this.RadBtn3,
				this.RadBtn4
			});
			this.ess = ESS;
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			this.rkc.ReceiveAnswer();
		}

		private void btnInterrupt_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void ConsultForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.userFormLocation = base.Location;
			if (!this.rkc.answerWasGet)
			{
				if (MessageBox.Show("Вы действительно хотите прервать консультацию ?", "Отмена консультации", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					this.rkc.InterruptConsult();
					return;
				}
				e.Cancel = true;
			}
		}

		private void ConsultForm_Load(object sender, EventArgs e)
		{
			base.Location = this.userFormLocation;
		}
	}
}
