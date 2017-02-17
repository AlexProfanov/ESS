using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormAddEditResolution : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private IContainer components;

		private ComboBox comboBoxVariable;

		private Label label1;

		private Label label2;

		private Label label3;

		private ComboBox comboBoxValue;

		private Button buttonOK;

		private Button buttonCancel;

		private ToolTip toolTip;

		public FormAddEditResolution()
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
				MessageBox.Show("Добавление заключения невозможно: нет объявленых переменных", "Внимание", 0, 16);
				return;
			}
			this.Text = "Добавление заключения";
			DialogFuncs.showAllVariables(this.comboBoxVariable);
			this.comboBoxVariable.SelectedIndex = 0;
			DialogFuncs.showDomainForSelectedVariable(this.comboBoxValue, Global.knowledgeBase.getVariableAt(this.comboBoxVariable.SelectedIndex));
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxVariable);
				Value value = null;
				try
				{
					value = DialogFuncs.getValueForVariable(selectedVariable.VarType, this.comboBoxValue);
				}
				catch (FormatException)
				{
					MessageBox.Show("Добавление заключения невозможно. Значение переменной должно приводиться к типу " + selectedVariable.VarType.ToString(), "Ошибка", 0, 16);
					return;
				}
				Resolution resolution = new Resolution(selectedVariable, value);
				rule.addResolution(resolution);
			}
		}

		public void editResolution(Resolution resolution)
		{
			if (resolution == null)
			{
				throw new ArgumentNullException("resolution");
			}
			this.Text = "Редактирование заключения";
			DialogFuncs.showAllVariables(this.comboBoxVariable);
			DialogFuncs.setVariableToSelect(resolution.ResolutionVariable, this.comboBoxVariable);
			DialogFuncs.showDomainForSelectedVariable(this.comboBoxValue, Global.knowledgeBase.getVariableAt(this.comboBoxVariable.SelectedIndex));
			DialogFuncs.setValueToSelect(resolution.Value, this.comboBoxValue);
			base.ShowDialog();
			bool flag = DialogFuncs.needToSaveChanges(this.formClosingMethod);
			if (flag)
			{
				Variable selectedVariable = DialogFuncs.getSelectedVariable(this.comboBoxVariable);
				Value value = null;
				try
				{
					value = DialogFuncs.getValueForVariable(selectedVariable.VarType, this.comboBoxValue);
				}
				catch (FormatException)
				{
					MessageBox.Show("Добавление заключения невозможно. Значение переменной должно приводиться к типу " + selectedVariable.VarType.ToString(), "Ошибка", 0, 16);
					return;
				}
				resolution.setVarAndValue(selectedVariable, value);
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
			DialogFuncs.showDomainForSelectedVariable(this.comboBoxValue, Global.knowledgeBase.getVariableAt(this.comboBoxVariable.SelectedIndex));
		}

		private void FormAddEditResolution_KeyDown(object sender, KeyEventArgs e)
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
			this.comboBoxVariable = new ComboBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.comboBoxValue = new ComboBox();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.toolTip = new ToolTip(this.components);
			base.SuspendLayout();
			this.comboBoxVariable.DropDownHeight = 400;
			this.comboBoxVariable.DropDownStyle = 2;
			this.comboBoxVariable.FlatStyle = 0;
			this.comboBoxVariable.FormattingEnabled = true;
			this.comboBoxVariable.IntegralHeight = false;
			this.comboBoxVariable.Location = new Point(12, 27);
			this.comboBoxVariable.Name = "comboBoxVariable";
			this.comboBoxVariable.Size = new Size(200, 21);
			this.comboBoxVariable.TabIndex = 0;
			this.comboBoxVariable.SelectedIndexChanged += new EventHandler(this.comboBoxVariable_SelectedIndexChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(9, 11);
			this.label1.Name = "label1";
			this.label1.Size = new Size(71, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Переменная";
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Microsoft Sans Serif", 12f, 1, 3, 204);
			this.label2.Location = new Point(220, 25);
			this.label2.Name = "label2";
			this.label2.Size = new Size(24, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = ":=";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(249, 11);
			this.label3.Name = "label3";
			this.label3.Size = new Size(55, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Значение";
			this.comboBoxValue.DropDownHeight = 400;
			this.comboBoxValue.FlatStyle = 0;
			this.comboBoxValue.FormattingEnabled = true;
			this.comboBoxValue.IntegralHeight = false;
			this.comboBoxValue.Location = new Point(252, 27);
			this.comboBoxValue.Name = "comboBoxValue";
			this.comboBoxValue.Size = new Size(200, 21);
			this.comboBoxValue.TabIndex = 4;
			this.buttonOK.FlatStyle = 0;
			this.buttonOK.Location = new Point(12, 54);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(216, 23);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "OK";
			this.toolTip.SetToolTip(this.buttonOK, "Вызов через - Enter");
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.FlatStyle = 0;
			this.buttonCancel.Location = new Point(236, 54);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(216, 23);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "Отмена";
			this.toolTip.SetToolTip(this.buttonCancel, "Вызов через - Esc");
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(466, 82);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.comboBoxValue);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBoxVariable);
			base.FormBorderStyle = 3;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAddEditResolution";
			base.ShowInTaskbar = false;
			base.StartPosition = 4;
			base.KeyDown += new KeyEventHandler(this.FormAddEditResolution_KeyDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
