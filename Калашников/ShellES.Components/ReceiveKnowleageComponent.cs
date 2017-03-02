using ShellES.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShellES.Components
{
	public class ReceiveKnowleageComponent : ComponentPrototype
	{
		private ConsultForm consultForm;

		private Control ctrlQuestion;

		private List<RadioButton> ctrlRadio;

		private ComboBox cmbVariants;

		private bool UseCombo;

		private ESVars curVar;

		public bool answerWasGet;

		public ReceiveKnowleageComponent(ExpertSystemShell ess) : base(ess)
		{
			this.ctrlQuestion = null;
			this.cmbVariants = null;
			this.ctrlRadio = new List<RadioButton>();
		}

		public void BindingControls(Control Question, ComboBox Variants, params RadioButton[] Radio)
		{
			this.ctrlQuestion = Question;
			this.cmbVariants = Variants;
			this.ctrlRadio = new List<RadioButton>();
			for (int i = 0; i < Radio.Length; i++)
			{
				this.ctrlRadio.Add(Radio[i]);
			}
		}

		public ConsultForm NewConsultForm()
		{
			if (this.consultForm == null)
			{
				this.consultForm = new ConsultForm(this, this.ESshell);
			}
			return this.consultForm;
		}

		private void HideVariants()
		{
			for (int i = 0; i < this.ctrlRadio.Count; i++)
			{
				this.ctrlRadio[i].Visible = false;
			}
			if (this.cmbVariants != null)
			{
				this.cmbVariants.Visible = false;
			}
		}

		public void SetNextQuestion(ESVars v)
		{
			if (this.consultForm.InvokeRequired)
			{
				this.consultForm.Invoke(new dlgESSConsult(this.HideVariants));
			}
			else
			{
				this.HideVariants();
			}
			int count = v.Domain.Elements.Count;
			this.UseCombo = (count > this.ctrlRadio.Count);
			this.curVar = v;
			if (this.UseCombo)
			{
				this.cmbVariants.DataSource = v.Domain.Elements;
				this.cmbVariants.DisplayMember = "Value";
				this.cmbVariants.ValueMember = "Self";
				if (this.cmbVariants.Items.Count > 0)
				{
					this.cmbVariants.SelectedIndex = 0;
				}
				this.cmbVariants.Show();
			}
			else
			{
				for (int i = 0; i < v.Domain.Elements.Count; i++)
				{
					this.ctrlRadio[i].Text = v.Domain.Elements[i].Value;
					this.ctrlRadio[i].Tag = v.Domain.Elements[i];
					this.ctrlRadio[i].Visible = true;
					this.ctrlRadio[i].Checked = false;
				}
				if (v.Domain.Elements.Count > 0)
				{
					this.ctrlRadio[0].Checked = true;
				}
			}
			this.consultForm.LblQuest.Text = v.Question;
			this.answerWasGet = false;
			StaticHelper.ShowForm(this.consultForm, this.consultForm.userFormLocation);
		}

		public void ReceiveAnswer()
		{
			string text = "";
			if (this.UseCombo)
			{
				if (this.cmbVariants.SelectedIndex >= 0)
				{
					text = this.cmbVariants.Text;
				}
			}
			else
			{
				for (int i = 0; i < this.ctrlRadio.Count; i++)
				{
					if (this.ctrlRadio[i].Checked && this.ctrlRadio[i].Visible)
					{
						text = this.ctrlRadio[i].Text;
						break;
					}
				}
			}
			if (text == "")
			{
				MessageBox.Show("Пожалуйста, выберите значение переменной " + this.curVar.Name, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (this.UseCombo)
				{
					this.cmbVariants.Focus();
				}
				return;
			}
			this.answerWasGet = true;
			this.consultForm.userFormLocation = this.consultForm.Location;
			StaticHelper.HideForm(this.consultForm);
			this.curVar.AddValue(text, 100);
			this.ESshell.ResumeMLV();
		}

		public void InterruptConsult()
		{
			this.ESshell.InterruptConsult();
		}
	}
}
