using System;

namespace ProductionShell
{
	[Serializable]
	public class AskedVariable : Variable
	{
		private string question = "";

		private bool flag = false;

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
				if (base.Value.Data.ToString() == "error")
				{
					this.flag = true;
				}
			}
			else
			{
				Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (вывелось раньше)");
			}
			return !this.flag;
		}
	}
}
