using System;

namespace Оболочка_Буряк
{
	[Serializable]
	public class AskedVariable : Variable
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

		public AskedVariable(string name, Type type, string question) : base(name, type)
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
			if (!this.valueWasSet)
			{
				base.Value = Global.io.askQuestion(this.question, this);
				Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (спросилось)");
			}
			else
			{
				Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (вывелось раньше)");
			}
			return true;
		}
	}
}
