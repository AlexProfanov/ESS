using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class F_AddResolution1 : Form
	{
		private IContainer components = null;

		private Label label1;

		private Label label2;

		private ComboBox comboBoxVariable;

		private ComboBox comboBoxValue;

		private Label label3;

		private Button buttonOK;

		private Button buttonCancel;

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
			this.label1 = new Label();
			this.label2 = new Label();
			this.comboBoxVariable = new ComboBox();
			this.comboBoxValue = new ComboBox();
			this.label3 = new Label();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.button1 = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(9, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(74, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Переменная:";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(250, 13);
			this.label2.Name = "label2";
			this.label2.Size = new Size(58, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Значение:";
			this.comboBoxVariable.DropDownStyle = 2;
			this.comboBoxVariable.FormattingEnabled = true;
			this.comboBoxVariable.Location = new Point(12, 39);
			this.comboBoxVariable.Name = "comboBoxVariable";
			this.comboBoxVariable.Size = new Size(195, 21);
			this.comboBoxVariable.TabIndex = 2;
			this.comboBoxVariable.SelectedIndexChanged += new EventHandler(this.comboBoxVariable_SelectedIndexChanged);
			this.comboBoxValue.DropDownStyle = 2;
			this.comboBoxValue.FormattingEnabled = true;
			this.comboBoxValue.Location = new Point(253, 39);
			this.comboBoxValue.Name = "comboBoxValue";
			this.comboBoxValue.Size = new Size(192, 21);
			this.comboBoxValue.TabIndex = 3;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(223, 42);
			this.label3.Name = "label3";
			this.label3.Size = new Size(13, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "=";
			this.buttonOK.Location = new Point(259, 114);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(94, 23);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "Ок";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(359, 114);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(94, 23);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			this.button1.Location = new Point(12, 82);
			this.button1.Name = "button1";
			this.button1.Size = new Size(195, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "Новая переменная";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(472, 152);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.comboBoxValue);
			base.Controls.Add(this.comboBoxVariable);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Name = "F_AddResolution1";
			base.StartPosition = 1;
			this.Text = "Заключение";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public F_AddResolution1()
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

		private void button1_Click(object sender, EventArgs e)
		{
			new F_variable_add().addVariable();
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
