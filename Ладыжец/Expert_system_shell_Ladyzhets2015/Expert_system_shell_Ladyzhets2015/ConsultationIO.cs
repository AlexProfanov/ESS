using System;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	[Serializable]
	public class ConsultationIO : IO
	{
		private F_consultation formConsultation = new F_consultation();

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
				throw new ArgumentNullException("question==null");
			}
			if (variable == null)
			{
				throw new ArgumentNullException("variable==null");
			}
			if (question == "")
			{
				this.formConsultation.listBoxDialog.Items.Add("?: " + variable.Name.ToString());
			}
			else
			{
				this.formConsultation.listBoxDialog.Items.Add("?: " + question);
			}
			this.formConsultation.listBoxDialog.SelectedIndex = this.formConsultation.listBoxDialog.Items.Count - 1;
			DialogFuncs.showDomainForSelectedVariable(this.formConsultation.comboBoxAnswer, variable);
			int num = this.formConsultation.ShowDialog();
			this.formConsultation.Hide();
			Value value;
			try
			{
				value = DialogFuncs.getValueForVariable(variable.VarType, this.formConsultation.comboBoxAnswer);
				this.formConsultation.listBoxDialog.Items.Add(">: " + value.ToString());
				this.formConsultation.listBoxDialog.Items.Add("");
			}
			catch (FormatException var_2_141)
			{
				int num2 = MessageBox.Show("Не удалось означить переменную ", "Ошибка", 0, 16);
				value = this.askQuestion(question, variable);
			}
			return new Value(value.type, value.Data);
		}
	}
}
