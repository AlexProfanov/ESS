using System;

namespace ZShellCore
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
			bool result;
			if (this.isValueSet)
			{
				result = true;
			}
			else
			{
				if (Global.knowledgeBase == null)
				{
					throw new InvalidOperationException();
				}
				Global.exposition.printText("Цель = " + base.Name);
				if (!Global.knowledgeBase.tryToGetValue(this))
				{
					base.Value = Global.io.askQuestion(this.question, this);
					Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (запросилось)");
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
