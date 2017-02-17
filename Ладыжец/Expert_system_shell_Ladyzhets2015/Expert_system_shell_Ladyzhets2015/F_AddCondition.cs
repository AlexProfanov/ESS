using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class F_AddCondition : Form
	{
		private IContainer components = null;

		private GroupBox groupBoxConditionType;

		private RadioButton radioButtonVarAndVar;

		private RadioButton radioButtonVarAndValue;

		private ComboBox comboBoxFirstArg;

		private ComboBox comboBoxSecArg;

		private Button buttonOK;

		private Button buttonCancel;

		private Label label1;

		private Label label2;

		private ComboBox comboBoxComparer;

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
			this.groupBoxConditionType = new GroupBox();
			this.radioButtonVarAndVar = new RadioButton();
			this.radioButtonVarAndValue = new RadioButton();
			this.comboBoxFirstArg = new ComboBox();
			this.comboBoxSecArg = new ComboBox();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.label1 = new Label();
			this.label2 = new Label();
			this.comboBoxComparer = new ComboBox();
			this.label3 = new Label();
			this.button1 = new Button();
			this.groupBoxConditionType.SuspendLayout();
			base.SuspendLayout();
			this.groupBoxConditionType.Controls.Add(this.radioButtonVarAndVar);
			this.groupBoxConditionType.Controls.Add(this.radioButtonVarAndValue);
			this.groupBoxConditionType.Location = new Point(12, 95);
			this.groupBoxConditionType.Name = "groupBoxConditionType";
			this.groupBoxConditionType.Size = new Size(11, 10);
			this.groupBoxConditionType.TabIndex = 0;
			this.groupBoxConditionType.TabStop = false;
			this.groupBoxConditionType.Text = "Тип условия:";
			this.groupBoxConditionType.Visible = false;
			this.radioButtonVarAndVar.AutoSize = true;
			this.radioButtonVarAndVar.Location = new Point(6, 33);
			this.radioButtonVarAndVar.Name = "radioButtonVarAndVar";
			this.radioButtonVarAndVar.Size = new Size(169, 17);
			this.radioButtonVarAndVar.TabIndex = 1;
			this.radioButtonVarAndVar.TabStop = true;
			this.radioButtonVarAndVar.Text = "Переменная == переменная";
			this.radioButtonVarAndVar.UseVisualStyleBackColor = true;
			this.radioButtonVarAndVar.Click += new EventHandler(this.radioButtonVarAndVar_Click);
			this.radioButtonVarAndValue.AutoSize = true;
			this.radioButtonVarAndValue.Checked = true;
			this.radioButtonVarAndValue.Location = new Point(6, 19);
			this.radioButtonVarAndValue.Name = "radioButtonVarAndValue";
			this.radioButtonVarAndValue.Size = new Size(157, 17);
			this.radioButtonVarAndValue.TabIndex = 0;
			this.radioButtonVarAndValue.TabStop = true;
			this.radioButtonVarAndValue.Text = "Переменная ==  значение";
			this.radioButtonVarAndValue.UseVisualStyleBackColor = true;
			this.radioButtonVarAndValue.Click += new EventHandler(this.radioButtonVarAndValue_Click);
			this.comboBoxFirstArg.DropDownStyle = 2;
			this.comboBoxFirstArg.FormattingEnabled = true;
			this.comboBoxFirstArg.Location = new Point(12, 34);
			this.comboBoxFirstArg.Name = "comboBoxFirstArg";
			this.comboBoxFirstArg.Size = new Size(200, 21);
			this.comboBoxFirstArg.TabIndex = 1;
			this.comboBoxFirstArg.SelectedIndexChanged += new EventHandler(this.comboBoxFirstArg_SelectedIndexChanged);
			this.comboBoxSecArg.DropDownStyle = 2;
			this.comboBoxSecArg.FormattingEnabled = true;
			this.comboBoxSecArg.Location = new Point(260, 34);
			this.comboBoxSecArg.Name = "comboBoxSecArg";
			this.comboBoxSecArg.Size = new Size(200, 21);
			this.comboBoxSecArg.TabIndex = 2;
			this.comboBoxSecArg.SelectedIndexChanged += new EventHandler(this.comboBoxSecArg_SelectedIndexChanged);
			this.buttonOK.Location = new Point(267, 108);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(96, 23);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "Ок";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(369, 108);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(96, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 14);
			this.label1.Name = "label1";
			this.label1.Size = new Size(74, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Переменная:";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(257, 14);
			this.label2.Name = "label2";
			this.label2.Size = new Size(58, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Значение:";
			this.comboBoxComparer.FormattingEnabled = true;
			this.comboBoxComparer.Items.AddRange(new object[]
			{
				"=="
			});
			this.comboBoxComparer.Location = new Point(218, 34);
			this.comboBoxComparer.Name = "comboBoxComparer";
			this.comboBoxComparer.Size = new Size(36, 21);
			this.comboBoxComparer.TabIndex = 3;
			this.comboBoxComparer.Visible = false;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(229, 37);
			this.label3.Name = "label3";
			this.label3.Size = new Size(13, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "=";
			this.button1.Location = new Point(12, 74);
			this.button1.Name = "button1";
			this.button1.Size = new Size(200, 23);
			this.button1.TabIndex = 9;
			this.button1.Text = "Новая переменная";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(483, 143);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.comboBoxComparer);
			base.Controls.Add(this.comboBoxSecArg);
			base.Controls.Add(this.comboBoxFirstArg);
			base.Controls.Add(this.groupBoxConditionType);
			base.Name = "F_AddCondition";
			base.StartPosition = 1;
			this.Text = "Условие";
			this.groupBoxConditionType.ResumeLayout(false);
			this.groupBoxConditionType.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public F_AddCondition()
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
				int num = MessageBox.Show("Прежде чем задать условие, необходимо объявить переменные", "Внимание", 0, 16);
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
						catch (FormatException var_6_EF)
						{
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
				int num2 = base.ShowDialog();
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
			DialogFuncs.showDomainForSelectedVariable(this.comboBoxSecArg, Global.knowledgeBase.getVariableAt(this.comboBoxFirstArg.SelectedIndex));
		}

		private void radioButtonVarAndVar_Click(object sender, EventArgs e)
		{
			DialogFuncs.showAllVariables(this.comboBoxSecArg, DialogFuncs.getSelectedVariable(this.comboBoxFirstArg));
		}

		private void comboBoxSecArg_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new F_variable_add().addVariable();
			DialogFuncs.showAllVariables(this.comboBoxFirstArg);
			this.comboBoxFirstArg.SelectedIndex = this.comboBoxFirstArg.Items.Count - 1;
			DialogFuncs.showDomainForSelectedVariable(this.comboBoxSecArg, DialogFuncs.getSelectedVariable(this.comboBoxFirstArg));
		}
	}
}
