using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormSetGoal : Form
	{
		private IContainer components;

		private ComboBox comboBoxGoals;

		private Label label1;

		private Button buttonOK;

		private Button buttonCancel;

		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

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
			this.comboBoxGoals = new ComboBox();
			this.label1 = new Label();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			base.SuspendLayout();
			this.comboBoxGoals.DropDownHeight = 400;
			this.comboBoxGoals.DropDownStyle = 2;
			this.comboBoxGoals.FlatStyle = 0;
			this.comboBoxGoals.FormattingEnabled = true;
			this.comboBoxGoals.IntegralHeight = false;
			this.comboBoxGoals.Location = new Point(12, 25);
			this.comboBoxGoals.Name = "comboBoxGoals";
			this.comboBoxGoals.Size = new Size(200, 21);
			this.comboBoxGoals.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.FlatStyle = 0;
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(95, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Возможные цели";
			this.buttonOK.FlatStyle = 0;
			this.buttonOK.Location = new Point(12, 52);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(95, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.FlatStyle = 0;
			this.buttonCancel.Location = new Point(117, 52);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(95, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(225, 80);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBoxGoals);
			base.FormBorderStyle = 3;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormSetGoal";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			this.Text = "Задание цели консультации";
			base.KeyDown += new KeyEventHandler(this.FormSetGoal_KeyDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormSetGoal()
		{
			this.InitializeComponent();
		}

		public void setGoal()
		{
			if (!Global.knowledgeBase.hasAnyVariables())
			{
				MessageBox.Show("Задание цели: нет объявленых переменных", "Внимание", 0, 16);
				return;
			}
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
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				Global.knowledgeBase.Goal = DialogFuncs.getSelectedVariable(this.comboBoxGoals);
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
			if (keyCode == 13)
			{
				this.buttonOK_Click(this, new EventArgs());
				return;
			}
			if (keyCode != 27)
			{
				return;
			}
			this.buttonCancel_Click(this, new EventArgs());
		}
	}
}
