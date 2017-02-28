using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZShellCore;

namespace ZShellUI
{
	public class FormAddEditCondition : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private IContainer components = null;

		private ComboBox comboBoxFirstArg;

		private ComboBox comboBoxComparer;

		private ComboBox comboBoxSecArg;

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
			if (!Global.knowledgeBase.hasAnyVariables())
			{
				MessageBox.Show("Добавление условия невозможно: нет объявленых переменных", "Внимание", 0, (System.Windows.Forms.MessageBoxIcon)16);
			}
			else
			{
				this.comboBoxComparer.SelectedIndex = 0;
				DialogFuncs.showAllVariables(this.comboBoxFirstArg);
				DialogFuncs.showDomainForSelectedVariable(this.comboBoxSecArg, DialogFuncs.getSelectedVariable(this.comboBoxFirstArg));
				base.ShowDialog();
				if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
				{
					Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
					ConditionComparer selectedComparer = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
					Value valueForVariable;
					try
					{
						valueForVariable = DialogFuncs.getValueForVariable(selectedVariable.Type, this.comboBoxSecArg);
					}
					catch (FormatException)
					{
						MessageBox.Show("Добавление условия невозможно. Значение переменной должно приводиться к типу " + selectedVariable.Type.ToString(), "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
						return;
					}
					Condition condition;
					try
					{
						condition = new VariableAndValueCondition(selectedVariable, selectedComparer, valueForVariable);
					}
					catch (ArgumentException ex)
					{
						MessageBox.Show("Добавление условия невозможно.\n" + ex.Message, "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
						return;
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
			if (condition is VariableAndValueCondition)
			{
				VariableAndValueCondition variableAndValueCondition = condition as VariableAndValueCondition;
				DialogFuncs.setVariableToSelect(variableAndValueCondition.ConditionVariable, this.comboBoxFirstArg);
				DialogFuncs.showDomainForSelectedVariable(this.comboBoxSecArg, DialogFuncs.getSelectedVariable(this.comboBoxFirstArg));
				DialogFuncs.setValueToSelect(variableAndValueCondition.Value, this.comboBoxSecArg);
				base.ShowDialog();
				if (DialogFuncs.needToSaveChanges(this.formClosingMethod))
				{
					Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxFirstArg);
					Value valueForVariable;
					try
					{
						valueForVariable = DialogFuncs.getValueForVariable(selectedVariable.Type, this.comboBoxSecArg);
					}
					catch (FormatException)
					{
						MessageBox.Show("Добавление условия невозможно. Значение переменной должно приводиться к типу " + selectedVariable.Type.ToString(), "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
						return;
					}
					variableAndValueCondition.setVariableAndValue(selectedVariable, valueForVariable);
					variableAndValueCondition.Comparer = DialogFuncs.getSelectedComparer(this.comboBoxComparer);
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

		private void FormAddEditCondition_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != (Keys)13)
			{
				if (keyCode == (Keys)27)
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
			this.components = new Container();
			this.comboBoxFirstArg = new ComboBox();
			this.comboBoxComparer = new ComboBox();
			this.comboBoxSecArg = new ComboBox();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.toolTip = new ToolTip(this.components);
			base.SuspendLayout();
			this.comboBoxFirstArg.DropDownHeight = 400;
			this.comboBoxFirstArg.DropDownStyle = (System.Windows.Forms.ComboBoxStyle)2;
			this.comboBoxFirstArg.FormattingEnabled = true;
			this.comboBoxFirstArg.IntegralHeight = false;
			this.comboBoxFirstArg.Location = new Point(10, 12);
			this.comboBoxFirstArg.Name = "comboBoxFirstArg";
			this.comboBoxFirstArg.Size = new Size(200, 21);
			this.comboBoxFirstArg.TabIndex = 0;
			this.comboBoxFirstArg.SelectedIndexChanged += new EventHandler(this.comboBoxFirstArg_SelectedIndexChanged);
			this.comboBoxComparer.DropDownHeight = 400;
			this.comboBoxComparer.DropDownStyle = (System.Windows.Forms.ComboBoxStyle)2;
			this.comboBoxComparer.FormattingEnabled = true;
			this.comboBoxComparer.IntegralHeight = false;
			this.comboBoxComparer.Items.AddRange(new object[]
			{
				"=="
			});
			this.comboBoxComparer.Location = new Point(216, 12);
			this.comboBoxComparer.Name = "comboBoxComparer";
			this.comboBoxComparer.Size = new Size(80, 21);
			this.comboBoxComparer.TabIndex = 1;
			this.comboBoxSecArg.DropDownHeight = 400;
			this.comboBoxSecArg.FormattingEnabled = true;
			this.comboBoxSecArg.IntegralHeight = false;
			this.comboBoxSecArg.Location = new Point(302, 12);
			this.comboBoxSecArg.Name = "comboBoxSecArg";
			this.comboBoxSecArg.Size = new Size(200, 21);
			this.comboBoxSecArg.TabIndex = 2;
			this.buttonOK.Location = new Point(319, 39);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(89, 23);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new Point(414, 39);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(89, 23);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.ClientSize = new Size(516, 72);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.comboBoxSecArg);
			base.Controls.Add(this.comboBoxComparer);
			base.Controls.Add(this.comboBoxFirstArg);
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAddEditCondition";
			base.ShowInTaskbar = false;
			base.StartPosition = (System.Windows.Forms.FormStartPosition)4;
			base.KeyDown += new KeyEventHandler(this.FormAddEditCondition_KeyDown);
			base.ResumeLayout(false);
		}
	}
}
