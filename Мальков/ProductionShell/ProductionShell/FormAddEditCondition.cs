using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormAddEditCondition : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private IContainer components;

		private ComboBox comboBoxFirstArg;

		private ComboBox comboBoxComparer;

		private ComboBox comboBoxSecArg;

		private GroupBox groupBoxConditionType;

		private RadioButton radioButtonVarAndVar;

		private RadioButton radioButtonVarAndValue;

		private Button buttonOK;

		private Button buttonCancel;

		private ToolTip toolTip;

		public FormAddEditCondition()
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
				MessageBox.Show("Добавление условия невозможно: нет объявленых переменных", "Внимание", 0, 16);
				return;
			}
			this.comboBoxComparer.SelectedIndex = 0;
			DialogFuncs.showAllVariables(this.comboBoxFirstArg);
			DialogFuncs.showDomainForSelectedVariable(this.comboBoxSecArg, DialogFuncs.getSelectedVariable(this.comboBoxFirstArg));
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				Condition condition = null;
				if (this.radioButtonVarAndValue.Checked)
				{
					Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
					ConditionComparer selectedComparer = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
					Value value = null;
					try
					{
						value = DialogFuncs.getValueForVariable(selectedVariable.VarType, this.comboBoxSecArg);
					}
					catch (FormatException)
					{
						MessageBox.Show("Добавление условия невозможно. Значение переменной должно приводиться к типу " + selectedVariable.VarType.ToString(), "Ошибка", 0, 16);
						return;
					}
					try
					{
						condition = new VariableAndValueCondition(selectedVariable, selectedComparer, value);
						goto IL_193;
					}
					catch (ArgumentException ex)
					{
						MessageBox.Show("Добавление условия невозможно.\n" + ex.Message, "Ошибка", 0, 16);
						return;
					}
				}
				if (this.radioButtonVarAndVar.Checked)
				{
					Variable selectedVariable2 = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
					ConditionComparer selectedComparer2 = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
					Variable selectedVariable3 = DialogFuncs.getSelectedVariable(this.comboBoxSecArg);
					try
					{
						condition = new VariableAndVariableCondition(selectedVariable2, selectedComparer2, selectedVariable3);
						goto IL_193;
					}
					catch (ArgumentException ex2)
					{
						MessageBox.Show("Добавление условия невозможно.\n" + ex2.Message, "Ошибка", 0, 16);
						return;
					}
				}
				throw new InvalidOperationException();
				IL_193:
				rule.addCondition(condition);
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
				base.ShowDialog();
				bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
				if (flag)
				{
					Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
					Value value = null;
					try
					{
						value = DialogFuncs.getValueForVariable(selectedVariable.VarType, this.comboBoxSecArg);
					}
					catch (FormatException)
					{
						MessageBox.Show("Добавление условия невозможно. Значение переменной должно приводиться к типу " + selectedVariable.VarType.ToString(), "Ошибка", 0, 16);
						return;
					}
					variableAndValueCondition.setVarAndValue(selectedVariable, value);
					variableAndValueCondition.Comparer = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
					return;
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
				base.ShowDialog();
				bool flag2 = DialogFuncs.needToSaveChanges(this.formClosingMethod);
				if (flag2)
				{
					Variable selectedVariable2 = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
					Variable selectedVariable3 = DialogFuncs.getSelectedVariable(this.comboBoxSecArg);
					variableAndVariableCondition.setVariables(selectedVariable2, selectedVariable3);
					variableAndVariableCondition.Comparer = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
					return;
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
				return;
			}
			if (this.radioButtonVarAndVar.Checked)
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
			this.components = new Container();
			this.comboBoxFirstArg = new ComboBox();
			this.comboBoxComparer = new ComboBox();
			this.comboBoxSecArg = new ComboBox();
			this.groupBoxConditionType = new GroupBox();
			this.radioButtonVarAndVar = new RadioButton();
			this.radioButtonVarAndValue = new RadioButton();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.toolTip = new ToolTip(this.components);
			this.groupBoxConditionType.SuspendLayout();
			base.SuspendLayout();
			this.comboBoxFirstArg.DropDownHeight = 400;
			this.comboBoxFirstArg.DropDownStyle = 2;
			this.comboBoxFirstArg.FlatStyle = 0;
			this.comboBoxFirstArg.FormattingEnabled = true;
			this.comboBoxFirstArg.IntegralHeight = false;
			this.comboBoxFirstArg.Location = new Point(11, 92);
			this.comboBoxFirstArg.Name = "comboBoxFirstArg";
			this.comboBoxFirstArg.Size = new Size(200, 21);
			this.comboBoxFirstArg.TabIndex = 0;
			this.comboBoxFirstArg.SelectedIndexChanged += new EventHandler(this.comboBoxFirstArg_SelectedIndexChanged);
			this.comboBoxComparer.DropDownHeight = 400;
			this.comboBoxComparer.DropDownStyle = 2;
			this.comboBoxComparer.FlatStyle = 0;
			this.comboBoxComparer.FormattingEnabled = true;
			this.comboBoxComparer.IntegralHeight = false;
			this.comboBoxComparer.Items.AddRange(new object[]
			{
				"==",
				"!=",
				">",
				"<"
			});
			this.comboBoxComparer.Location = new Point(217, 92);
			this.comboBoxComparer.Name = "comboBoxComparer";
			this.comboBoxComparer.Size = new Size(80, 21);
			this.comboBoxComparer.TabIndex = 1;
			this.comboBoxSecArg.DropDownHeight = 400;
			this.comboBoxSecArg.FlatStyle = 0;
			this.comboBoxSecArg.FormattingEnabled = true;
			this.comboBoxSecArg.IntegralHeight = false;
			this.comboBoxSecArg.Location = new Point(303, 92);
			this.comboBoxSecArg.Name = "comboBoxSecArg";
			this.comboBoxSecArg.Size = new Size(200, 21);
			this.comboBoxSecArg.TabIndex = 2;
			this.groupBoxConditionType.Controls.Add(this.radioButtonVarAndVar);
			this.groupBoxConditionType.Controls.Add(this.radioButtonVarAndValue);
			this.groupBoxConditionType.FlatStyle = 0;
			this.groupBoxConditionType.Location = new Point(12, 12);
			this.groupBoxConditionType.Name = "groupBoxConditionType";
			this.groupBoxConditionType.Size = new Size(491, 74);
			this.groupBoxConditionType.TabIndex = 3;
			this.groupBoxConditionType.TabStop = false;
			this.groupBoxConditionType.Text = "Тип условия";
			this.radioButtonVarAndVar.AutoSize = true;
			this.radioButtonVarAndVar.FlatStyle = 0;
			this.radioButtonVarAndVar.Location = new Point(7, 43);
			this.radioButtonVarAndVar.Name = "radioButtonVarAndVar";
			this.radioButtonVarAndVar.Size = new Size(162, 17);
			this.radioButtonVarAndVar.TabIndex = 1;
			this.radioButtonVarAndVar.Text = "Переменная и переменная";
			this.radioButtonVarAndVar.UseVisualStyleBackColor = true;
			this.radioButtonVarAndVar.Click += new EventHandler(this.radioButtonVarAndVar_Click);
			this.radioButtonVarAndValue.AutoSize = true;
			this.radioButtonVarAndValue.Checked = true;
			this.radioButtonVarAndValue.FlatStyle = 0;
			this.radioButtonVarAndValue.Location = new Point(7, 20);
			this.radioButtonVarAndValue.Name = "radioButtonVarAndValue";
			this.radioButtonVarAndValue.Size = new Size(147, 17);
			this.radioButtonVarAndValue.TabIndex = 0;
			this.radioButtonVarAndValue.TabStop = true;
			this.radioButtonVarAndValue.Text = "Переменная и значение";
			this.radioButtonVarAndValue.UseVisualStyleBackColor = true;
			this.radioButtonVarAndValue.Click += new EventHandler(this.radioButtonVarAndValue_Click);
			this.buttonOK.FlatStyle = 0;
			this.buttonOK.Location = new Point(11, 119);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(245, 23);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "OK";
			this.toolTip.SetToolTip(this.buttonOK, "Вызов через - Enter");
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.FlatStyle = 0;
			this.buttonCancel.Location = new Point(258, 119);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(245, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Отмена";
			this.toolTip.SetToolTip(this.buttonCancel, "Вызов через - Escape");
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(516, 151);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBoxConditionType);
			base.Controls.Add(this.comboBoxSecArg);
			base.Controls.Add(this.comboBoxComparer);
			base.Controls.Add(this.comboBoxFirstArg);
			base.FormBorderStyle = 3;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAddEditCondition";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			base.KeyDown += new KeyEventHandler(this.FormAddEditCondition_KeyDown);
			this.groupBoxConditionType.ResumeLayout(false);
			this.groupBoxConditionType.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
