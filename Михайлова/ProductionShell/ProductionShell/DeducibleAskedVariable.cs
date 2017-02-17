using System;

namespace ProductionShell
{
	[Serializable]
	public class DeducibleAskedVariable : Variable
	{
		private string question = "";

		public string Question
		{
			get
			{
				return this.question;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.question = value;
			}
		}

		public DeducibleAskedVariable(string name, Type type, string question) : base(name, type)
		{
			if (question == null)
			{
				throw new ArgumentNullException("question");
			}
			this.question = question;
		}

		public override bool tryToGetValue()
		{
			Global.exposition.printText("Цель = " + base.Name);
			bool result;
			if (this.valueWasSet)
			{
				Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (вывелось раньше)");
				result = true;
			}
			else
			{
				if (Global.knowledgeBase == null)
				{
					throw new InvalidOperationException();
				}
				if (!Global.knowledgeBase.tryToGetValue(this))
				{
					base.Value = Global.io.askQuestion(this.question, this);
					Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (спросилось)");
				}
				else
				{
					Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (вывелось)");
				}
				result = true;
			}
			return result;
		}
	}
}
