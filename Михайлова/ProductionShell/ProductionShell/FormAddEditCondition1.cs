using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormAddEditCondition1 : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private IContainer components = null;

		private GroupBox groupBoxConditionType;

		private RadioButton radioButtonVarAndVar;

		private RadioButton radioButtonVarAndValue;

		private ComboBox comboBoxFirstArg;

		private ComboBox comboBoxSecArg;

		private Button buttonOK;

		private Button buttonCancel;

		private ComboBox comboBoxComparer;

		private Button button1;

		public FormAddEditCondition1()
		{
			this.InitializeComponent();
		}

		public void AddCondition(Rule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("item");
			}
			this.Text = "Добавление условия";
			this.groupBoxConditionType.Enabled = true;
			if (!Global.knowledgeBase.hasAnyVariables())
			{
				int num = MessageBox.Show("Добавление условия невозможно: нет объявленых переменных", "Внимание", 0, 16);
			}
			else
			{
				this.comboBoxComparer.SelectedIndex = 0;
				DialogFuncs.showAllVariables(this.comboBoxFirstArg);
				DialogFuncs.showDomainForSelectedVariable(this.comboBoxSecArg, DialogFuncs.getSelectedVariable(this.comboBoxFirstArg));
				int num2 = base.ShowDialog();
				if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
				{
					Condition condition;
					if (this.radioButtonVarAndValue.Checked)
					{
						Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
						ConditionComparer selectedComparer = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
						Value valueForVariable;
						try
						{
							valueForVariable = DialogFuncs.getValueForVariable(selectedVariable.VarType, this.comboBoxSecArg);
						}
						catch (FormatException var_6_F2)
						{
							int num3 = MessageBox.Show("Добавление условия невозможно. Значение переменной должно приводиться к типу " + selectedVariable.VarType.ToString(), "Ошибка", 0, 16);
							return;
						}
						try
						{
							condition = new VariableAndValueCondition(selectedVariable, selectedComparer, valueForVariable);
						}
						catch (ArgumentException ex)
						{
							int num3 = MessageBox.Show("Добавление условия невозможно.\n" + ex.Message, "Ошибка", 0, 16);
							return;
						}
					}
					else
					{
						if (!this.radioButtonVarAndVar.Checked)
						{
							throw new InvalidOperationException();
						}
						Variable selectedVariable2 = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
						ConditionComparer selectedComparer = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
						Variable selectedVariable3 = DialogFuncs.getSelectedVariable(this.comboBoxSecArg);
						try
						{
							condition = new VariableAndVariableCondition(selectedVariable2, selectedComparer, selectedVariable3);
						}
						catch (ArgumentException ex)
						{
							int num3 = MessageBox.Show("Добавление условия невозможно.\n" + ex.Message, "Ошибка", 0, 16);
							return;
						}
					}
					rule.addCondition(condition);
				}
			}
		}

		public void editCondition(Condition condition)
		{
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			this.Text = "Редактирование условия";
			DialogFuncs.showAllVariables(this.comboBoxFirstArg);
			DialogFuncs.setComparerToSelect(condition.Comparer, this.comboBoxComparer);
			this.groupBoxConditionType.Enabled = false;
			if (condition is VariableAndValueCondition)
			{
				this.radioButtonVarAndValue.Checked = true;
				VariableAndValueCondition variableAndValueCondition = condition as VariableAndValueCondition;
				DialogFuncs.setVariableToSelect(variableAndValueCondition.ConditionVariable, this.comboBoxFirstArg);
				DialogFuncs.showDomainForSelectedVariable(this.comboBoxSecArg, DialogFuncs.getSelectedVariable(this.comboBoxFirstArg));
				DialogFuncs.setValueToSelect(variableAndValueCondition.Value, this.comboBoxSecArg);
				int num = base.ShowDialog();
				if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
				{
					Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
					Value valueForVariable;
					try
					{
						valueForVariable = DialogFuncs.getValueForVariable(selectedVariable.VarType, this.comboBoxSecArg);
					}
					catch (FormatException var_4_F4)
					{
						int num2 = MessageBox.Show("Добавление условия невозможно. Значение переменной должно приводиться к типу " + selectedVariable.VarType.ToString(), "Ошибка", 0, 16);
						return;
					}
					variableAndValueCondition.setVarAndValue(selectedVariable, valueForVariable);
					variableAndValueCondition.Comparer = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
				}
			}
			else
			{
				if (!(condition is VariableAndVariableCondition))
				{
					throw new InvalidOperationException();
				}
				this.radioButtonVarAndVar.Checked = true;
				VariableAndVariableCondition variableAndVariableCondition = condition as VariableAndVariableCondition;
				DialogFuncs.setVariableToSelect(variableAndVariableCondition.VariableFirst, this.comboBoxFirstArg);
				DialogFuncs.showAllVariables(this.comboBoxSecArg, variableAndVariableCondition.VariableFirst);
				DialogFuncs.setVariableToSelect(variableAndVariableCondition.VariableSecond, this.comboBoxSecArg);
				int num3 = base.ShowDialog();
				if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
				{
					Variable selectedVariable2 = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
					Variable selectedVariable3 = DialogFuncs.getSelectedVariable(this.comboBoxSecArg);
					variableAndVariableCondition.setVariables(selectedVariable2, selectedVariable3);
					variableAndVariableCondition.Comparer = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
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

		private void radioButtonVarAndValue_Click(object sender, EventArgs e)
		{
			DialogFuncs.showDomainForSelectedVariable(this.comboBoxSecArg, Global.knowledgeBase.getVariableAt(this.comboBoxFirstArg.SelectedIndex));
		}

		private void comboBoxFirstArg_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.radioButtonVarAndValue.Checked)
			{
				DialogFuncs.showDomainForSelectedVariable(this.comboBoxSecArg, Global.knowledgeBase.getVariableAt(this.comboBoxFirstArg.SelectedIndex));
			}
			else if (this.radioButtonVarAndVar.Checked)
			{
				DialogFuncs.showAllVariables(this.comboBoxSecArg, DialogFuncs.getSelectedVariable(this.comboBoxFirstArg));
			}
		}

		private void radioButtonVarAndVar_Click(object sender, EventArgs e)
		{
			DialogFuncs.showAllVariables(this.comboBoxSecArg, DialogFuncs.getSelectedVariable(this.comboBoxFirstArg));
		}

		private void FormAddEditCondition_KeyDown(object sender, KeyEventArgs e)
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

		private void ButtonOK_Click(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new FormAddEditVar1().addVariable();
			DialogFuncs.showAllVariables(this.comboBoxFirstArg);
			this.comboBoxFirstArg.SelectedIndex = this.comboBoxFirstArg.Items.Count - 1;
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
			this.groupBoxConditionType = new GroupBox();
			this.radioButtonVarAndVar = new RadioButton();
			this.radioButtonVarAndValue = new RadioButton();
			this.comboBoxFirstArg = new ComboBox();
			this.comboBoxSecArg = new ComboBox();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.comboBoxComparer = new ComboBox();
			this.button1 = new Button();
			this.groupBoxConditionType.SuspendLayout();
			base.SuspendLayout();
			this.groupBoxConditionType.Controls.Add(this.button1);
			this.groupBoxConditionType.Controls.Add(this.radioButtonVarAndVar);
			this.groupBoxConditionType.Controls.Add(this.radioButtonVarAndValue);
			this.groupBoxConditionType.Location = new Point(9, 11);
			this.groupBoxConditionType.Name = "groupBoxConditionType";
			this.groupBoxConditionType.Size = new Size(400, 73);
			this.groupBoxConditionType.TabIndex = 0;
			this.groupBoxConditionType.TabStop = false;
			this.groupBoxConditionType.Text = "Тип условия";
			this.radioButtonVarAndVar.AutoSize = true;
			this.radioButtonVarAndVar.Location = new Point(16, 44);
			this.radioButtonVarAndVar.Name = "radioButtonVarAndVar";
			this.radioButtonVarAndVar.Size = new Size(163, 17);
			this.radioButtonVarAndVar.TabIndex = 1;
			this.radioButtonVarAndVar.TabStop = true;
			this.radioButtonVarAndVar.Text = "Переменная и переменная";
			this.radioButtonVarAndVar.UseVisualStyleBackColor = true;
			this.radioButtonVarAndVar.Visible = false;
			this.radioButtonVarAndVar.Click += new EventHandler(this.radioButtonVarAndVar_Click);
			this.radioButtonVarAndValue.AutoSize = true;
			this.radioButtonVarAndValue.Location = new Point(16, 21);
			this.radioButtonVarAndValue.Name = "radioButtonVarAndValue";
			this.radioButtonVarAndValue.Size = new Size(148, 17);
			this.radioButtonVarAndValue.TabIndex = 0;
			this.radioButtonVarAndValue.TabStop = true;
			this.radioButtonVarAndValue.Text = "Переменная и значение";
			this.radioButtonVarAndValue.UseVisualStyleBackColor = true;
			this.radioButtonVarAndValue.Click += new EventHandler(this.radioButtonVarAndValue_Click);
			this.comboBoxFirstArg.DropDownStyle = 2;
			this.comboBoxFirstArg.FlatStyle = 1;
			this.comboBoxFirstArg.FormattingEnabled = true;
			this.comboBoxFirstArg.Location = new Point(13, 100);
			this.comboBoxFirstArg.Name = "comboBoxFirstArg";
			this.comboBoxFirstArg.Size = new Size(160, 21);
			this.comboBoxFirstArg.TabIndex = 1;
			this.comboBoxFirstArg.SelectedIndexChanged += new EventHandler(this.comboBoxFirstArg_SelectedIndexChanged);
			this.comboBoxSecArg.DropDownStyle = 2;
			this.comboBoxSecArg.FlatStyle = 1;
			this.comboBoxSecArg.FormattingEnabled = true;
			this.comboBoxSecArg.Location = new Point(250, 100);
			this.comboBoxSecArg.Name = "comboBoxSecArg";
			this.comboBoxSecArg.Size = new Size(159, 21);
			this.comboBoxSecArg.TabIndex = 3;
			this.buttonOK.Location = new Point(151, 131);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(126, 23);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(283, 131);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(126, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			this.comboBoxComparer.Enabled = false;
			this.comboBoxComparer.FlatStyle = 1;
			this.comboBoxComparer.FormattingEnabled = true;
			this.comboBoxComparer.Items.AddRange(new object[]
			{
				"=="
			});
			this.comboBoxComparer.Location = new Point(179, 100);
			this.comboBoxComparer.Name = "comboBoxComparer";
			this.comboBoxComparer.Size = new Size(65, 21);
			this.comboBoxComparer.TabIndex = 7;
			this.button1.Location = new Point(241, 21);
			this.button1.Name = "button1";
			this.button1.Size = new Size(153, 23);
			this.button1.TabIndex = 8;
			this.button1.Text = "Добавить переменную";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = Color.LavenderBlush;
			base.ClientSize = new Size(436, 166);
			base.Controls.Add(this.comboBoxComparer);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.comboBoxSecArg);
			base.Controls.Add(this.comboBoxFirstArg);
			base.Controls.Add(this.groupBoxConditionType);
			base.FormBorderStyle = 3;
			base.Name = "FormAddEditCondition1";
			base.StartPosition = 1;
			this.Text = "Добавление условия";
			this.groupBoxConditionType.ResumeLayout(false);
			this.groupBoxConditionType.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
