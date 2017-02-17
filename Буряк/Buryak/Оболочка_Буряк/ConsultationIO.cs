using System;
using System.Windows.Forms;

namespace Оболочка_Буряк
{
	public class ConsultationIO : IO
	{
		private FormConsultation1 formConsultation = new FormConsultation1();

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
			this.formConsultation.listBoxDialog.Items.Add("?: " + question);
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
			catch (FormatException var_2_100)
			{
				int num2 = MessageBox.Show("Означивание переменной невозможно. Значение переменной должно приводиться к типу " + variable.VarType.ToString(), "Ошибка", 0, 16);
				value = this.askQuestion(question, variable);
			}
			return new Value(value.type, value.Data);
		}
	}
}
