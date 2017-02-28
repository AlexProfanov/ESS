using System;
using System.Windows.Forms;
using ZShellCore;

namespace ZShellUI
{
	public class ConsultationIO : IO
	{
		private FormConsultation formConsultation = new FormConsultation();

		public void startNewConsultation()
		{
			this.formConsultation.listBoxDialog.Items.Clear();
			this.formConsultation.labelGoal.Text = Global.knowledgeBase.Goal.ToString();
			Global.knowledgeBase.setAllVariablesToBeUnknown();
		}

		public override Value askQuestion(string question, Variable variable)
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (question == "")
			{
				question = variable.Name;
			}
			this.formConsultation.listBoxDialog.Items.Add("?: " + question);
			this.formConsultation.listBoxDialog.SelectedIndex = this.formConsultation.listBoxDialog.Items.Count - 1;
			DialogFuncs.showDomainForSelectedVariable(this.formConsultation.comboBoxAnswer, variable);
			this.formConsultation.ShowDialog();
			this.formConsultation.Hide();
			Value value = null;
			try
			{
				value = DialogFuncs.getValueForVariable(variable.Type, this.formConsultation.comboBoxAnswer);
				this.formConsultation.listBoxDialog.Items.Add(">: " + value.ToString());
				this.formConsultation.listBoxDialog.Items.Add("");
			}
			catch (FormatException)
			{
				MessageBox.Show("Означивание переменной невозможно. Значение переменной должно приводиться к типу " + variable.Type.ToString(), "Ошибка", 0, (System.Windows.Forms.MessageBoxIcon)16);
				value = this.askQuestion(question, variable);
			}
			return value;
		}
	}
}
