using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZShellCore;

namespace ZShellUI
{
	public class FormSetGoal : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private IContainer components = null;

		private ComboBox comboBoxGoals;

		private Label label1;

		private Button buttonOK;

		private Button buttonCancel;

		public FormSetGoal()
		{
			this.InitializeComponent();
		}

		public void setGoal()
		{
			if (!Global.knowledgeBase.hasAnyVariables())
			{
				MessageBox.Show("Задание цели: нет объявленых переменных", "Внимание", 0, 16);
			}
			else
			{
				DialogFuncs.showDeducableVariables(this.comboBoxGoals);
				if (Global.knowledgeBase.Goal != null)
				{
					DialogFuncs.setVariableToSelect(Global.knowledgeBase.Goal, this.comboBoxGoals);
				}
				base.ShowDialog();
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
			this.comboBoxGoals.FormattingEnabled = true;
			this.comboBoxGoals.IntegralHeight = false;
			this.comboBoxGoals.Location = new Point(12, 25);
			this.comboBoxGoals.Name = "comboBoxGoals";
			this.comboBoxGoals.Size = new Size(200, 21);
			this.comboBoxGoals.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(95, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Возможные цели";
			this.buttonOK.Location = new Point(12, 52);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(95, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(117, 52);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(95, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.ClientSize = new Size(224, 82);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBoxGoals);
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new Size(240, 120);
			base.Name = "FormSetGoal";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			this.Text = "Задание цели консультации";
			base.KeyDown += new KeyEventHandler(this.FormSetGoal_KeyDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
