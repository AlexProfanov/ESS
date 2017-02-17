using System;

namespace Expert_system_shell_Ladyzhets2015
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
					throw new ArgumentNullException("value==null");
				}
				this.question = value;
			}
		}

		public AskedVariable(string name, Type type, string question) : base(name, type)
		{
			if (question == null)
			{
				throw new ArgumentNullException("question==null");
			}
			this.question = question;
		}

		public override bool tryToGetValue()
		{
			Global.exposition.printText("Цель = " + base.Name);
			if (!this.valueWasSet)
			{
				base.Value = Global.io.askQuestion(this.question, this);
				Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (было запрошено)");
			}
			else
			{
				Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (было выведено)");
			}
			return true;
		}
	}
}
