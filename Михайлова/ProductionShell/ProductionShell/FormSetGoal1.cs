using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormSetGoal1 : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private IContainer components = null;

		private Label label1;

		private ComboBox comboBoxGoals;

		private Button buttonOK;

		private Button buttonCancel;

		public FormSetGoal1()
		{
			this.InitializeComponent();
		}

		public void setGoal()
		{
			if (!Global.knowledgeBase.hasAnyVariables())
			{
				int num = MessageBox.Show("Задание цели: нет объявленых переменных", "Внимание", 0, 16);
			}
			else
			{
				this.comboBoxGoals.DropDownStyle = 2;
				this.comboBoxGoals.Items.Clear();
				IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
				while (enumeratorForVariables.MoveNext())
				{
					if (enumeratorForVariables.Current is DeducibleVariable || enumeratorForVariables.Current is DeducibleAskedVariable)
					{
						this.comboBoxGoals.Items.Add(enumeratorForVariables.Current.ToString());
					}
				}
				this.comboBoxGoals.SelectedIndex = 0;
				if (Global.knowledgeBase.Goal != null)
				{
					DialogFuncs.setVariableToSelect(Global.knowledgeBase.Goal, this.comboBoxGoals);
				}
				int num2 = base.ShowDialog();
				if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
				{
					Global.knowledgeBase.Goal = DialogFuncs.getSelectedVariable(this.comboBoxGoals);
				}
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.OK;
			base.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.formClosingMethod = FormClosingMethod.CANCEL;
			base.Close();
		}

		private void FormSetGoal_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != 13)
			{
				if (keyCode == 27)
				{
					this.buttonCancel_Click(this, new EventArgs());
				}
			}
			else
			{
				this.buttonOK_Click(this, new EventArgs());
			}
		}

		private void Form1_Load(object sender, EventArgs e)
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
			this.label1 = new Label();
			this.comboBoxGoals = new ComboBox();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 26);
			this.label1.Name = "label1";
			this.label1.Size = new Size(33, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Цель";
			this.comboBoxGoals.DropDownStyle = 2;
			this.comboBoxGoals.FlatStyle = 0;
			this.comboBoxGoals.FormattingEnabled = true;
			this.comboBoxGoals.Location = new Point(68, 23);
			this.comboBoxGoals.Name = "comboBoxGoals";
			this.comboBoxGoals.Size = new Size(241, 21);
			this.comboBoxGoals.TabIndex = 1;
			this.buttonOK.Location = new Point(88, 54);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(83, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "ОК";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(190, 54);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(86, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = Color.LavenderBlush;
			base.ClientSize = new Size(321, 89);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.comboBoxGoals);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = 3;
			base.Name = "FormSetGoal1";
			base.StartPosition = 1;
			this.Text = "Цель консультации";
			base.Load += new EventHandler(this.Form1_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
