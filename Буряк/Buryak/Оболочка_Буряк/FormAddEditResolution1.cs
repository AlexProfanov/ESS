using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Оболочка_Буряк
{
	public class FormAddEditResolution1 : Form
	{
		private IContainer components = null;

		private Button buttonCancel;

		private Button buttonOK;

		private ComboBox comboBoxValue;

		private ComboBox comboBoxVariable;

		private Label label1;

		private Label label2;

		private Label label3;

		private Button button1;

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
			this.buttonCancel = new Button();
			this.buttonOK = new Button();
			this.comboBoxValue = new ComboBox();
			this.comboBoxVariable = new ComboBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.button1 = new Button();
			base.SuspendLayout();
			this.buttonCancel.Location = new Point(213, 100);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(153, 23);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.add_Click(new EventHandler(this.buttonCancel_Click));
			this.buttonOK.Location = new Point(12, 100);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(160, 23);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.add_Click(new EventHandler(this.buttonOK_Click));
			this.comboBoxValue.DropDownStyle = 2;
			this.comboBoxValue.FlatStyle = 1;
			this.comboBoxValue.FormattingEnabled = true;
			this.comboBoxValue.Location = new Point(213, 44);
			this.comboBoxValue.Name = "comboBoxValue";
			this.comboBoxValue.Size = new Size(153, 21);
			this.comboBoxValue.TabIndex = 9;
			this.comboBoxVariable.DropDownStyle = 2;
			this.comboBoxVariable.FlatStyle = 1;
			this.comboBoxVariable.FormattingEnabled = true;
			this.comboBoxVariable.Location = new Point(12, 44);
			this.comboBoxVariable.Name = "comboBoxVariable";
			this.comboBoxVariable.Size = new Size(160, 21);
			this.comboBoxVariable.TabIndex = 7;
			this.comboBoxVariable.add_SelectedIndexChanged(new EventHandler(this.comboBoxVariable_SelectedIndexChanged));
			this.label1.AutoSize = true;
			this.label1.Location = new Point(50, 18);
			this.label1.Name = "label1";
			this.label1.Size = new Size(71, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Переменная";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(248, 18);
			this.label2.Name = "label2";
			this.label2.Size = new Size(55, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Значение";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(178, 47);
			this.label3.Name = "label3";
			this.label3.Size = new Size(13, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "=";
			this.button1.Location = new Point(12, 71);
			this.button1.Name = "button1";
			this.button1.Size = new Size(132, 23);
			this.button1.TabIndex = 15;
			this.button1.Text = "Добавить переменную";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.add_Click(new EventHandler(this.button1_Click));
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = SystemColors.Highlight;
			base.ClientSize = new Size(392, 128);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.comboBoxValue);
			base.Controls.Add(this.comboBoxVariable);
			base.FormBorderStyle = 3;
			base.Name = "FormAddEditResolution1";
			this.Text = "Добавление заключение";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormAddEditResolution1()
		{
			this.InitializeComponent();
		}

		public void addResolution(Rule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("item");
			}
			if (!Global.knowledgeBase.hasAnyVariables())
			{
				int num = MessageBox.Show("Добавление заключения невозможно: нет объявленых переменных", "Внимание", 0, 16);
			}
			else
			{
				this.Text = "Добавление заключения";
				this.comboBoxVariable.Items.Clear();
				IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
				while (enumeratorForVariables.MoveNext())
				{
					if (enumeratorForVariables.Current is DeducibleVariable || enumeratorForVariables.Current is DeducibleAskedVariable)
					{
						this.comboBoxVariable.Items.Add(enumeratorForVariables.Current.ToString());
					}
				}
				this.comboBoxVariable.SelectedIndex = 0;
				enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
				while (enumeratorForVariables.MoveNext())
				{
					if (enumeratorForVariables.Current.Name == this.comboBoxVariable.Text)
					{
						DialogFuncs.showDomainForSelectedVariable(this.comboBoxValue, enumeratorForVariables.Current);
						break;
					}
				}
				int num2 = base.ShowDialog();
				if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
				{
					Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxVariable);
					Value valueForVariable;
					try
					{
						valueForVariable = DialogFuncs.getValueForVariable(selectedVariable.VarType, this.comboBoxValue);
					}
					catch (FormatException var_5_15D)
					{
						int num3 = MessageBox.Show("Добавление заключения невозможно. Значение переменной должно приводиться к типу " + selectedVariable.VarType.ToString(), "Ошибка", 0, 16);
						return;
					}
					Resolution resolution = new Resolution(selectedVariable, valueForVariable);
					rule.addResolution(resolution);
				}
			}
		}

		public void editResolution(Resolution resolution)
		{
			if (resolution == null)
			{
				throw new ArgumentNullException("resolution");
			}
			this.Text = "Редактирование заключения";
			this.comboBoxVariable.Items.Clear();
			IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			while (enumeratorForVariables.MoveNext())
			{
				if (enumeratorForVariables.Current is DeducibleVariable || enumeratorForVariables.Current is DeducibleAskedVariable)
				{
					this.comboBoxVariable.Items.Add(enumeratorForVariables.Current.ToString());
				}
			}
			DialogFuncs.setVariableToSelect(resolution.ResolutionVariable, this.comboBoxVariable);
			enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			while (enumeratorForVariables.MoveNext())
			{
				if (enumeratorForVariables.Current.Name == this.comboBoxVariable.Text)
				{
					DialogFuncs.showDomainForSelectedVariable(this.comboBoxValue, enumeratorForVariables.Current);
					break;
				}
			}
			DialogFuncs.setValueToSelect(resolution.Value, this.comboBoxValue);
			int num = base.ShowDialog();
			if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
			{
				Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxVariable);
				Value valueForVariable;
				try
				{
					valueForVariable = DialogFuncs.getValueForVariable(selectedVariable.VarType, this.comboBoxValue);
				}
				catch (FormatException var_4_148)
				{
					int num2 = MessageBox.Show("Добавление заключения невозможно. Значение переменной должно приводиться к типу " + selectedVariable.VarType.ToString(), "Ошибка", 0, 16);
					return;
				}
				resolution.setVarAndValue(selectedVariable, valueForVariable);
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

		private void comboBoxVariable_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			while (enumeratorForVariables.MoveNext())
			{
				if (enumeratorForVariables.Current.Name == this.comboBoxVariable.Text)
				{
					DialogFuncs.showDomainForSelectedVariable(this.comboBoxValue, enumeratorForVariables.Current);
					break;
				}
			}
		}

		private void FormAddEditResolution_KeyDown(object sender, KeyEventArgs e)
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

		private void button1_Click(object sender, EventArgs e)
		{
			new FormAddEditVar1().addVariable();
			this.comboBoxVariable.Items.Clear();
			IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			while (enumeratorForVariables.MoveNext())
			{
				if (enumeratorForVariables.Current is DeducibleVariable || enumeratorForVariables.Current is DeducibleAskedVariable)
				{
					this.comboBoxVariable.Items.Add(enumeratorForVariables.Current.ToString());
				}
			}
			this.comboBoxVariable.SelectedIndex = this.comboBoxVariable.Items.Count - 1;
		}
	}
}
