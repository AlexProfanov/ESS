using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class F_SetGoal : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private IContainer components = null;

		private Label label1;

		private ComboBox comboBoxGoals;

		private Button buttonOK;

		private Button buttonCancel;

		public F_SetGoal()
		{
			this.InitializeComponent();
		}

		public void setGoal()
		{
			if (!Global.knowledgeBase.hasAnyVariables())
			{
				int num = MessageBox.Show("Невозможно задать цель! Обратите внимание на переменные", "Внимание", 0, 16);
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
			this.label1.Location = new Point(3, 14);
			this.label1.Name = "label1";
			this.label1.Size = new Size(109, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Цель консультации:";
			this.comboBoxGoals.DropDownStyle = 2;
			this.comboBoxGoals.FormattingEnabled = true;
			this.comboBoxGoals.Location = new Point(6, 38);
			this.comboBoxGoals.Name = "comboBoxGoals";
			this.comboBoxGoals.Size = new Size(279, 21);
			this.comboBoxGoals.TabIndex = 1;
			this.buttonOK.Location = new Point(129, 65);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "Ок";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(210, 65);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(297, 100);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.comboBoxGoals);
			base.Controls.Add(this.label1);
			base.Name = "F_SetGoal";
			base.StartPosition = 1;
			this.Text = "Цель";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
