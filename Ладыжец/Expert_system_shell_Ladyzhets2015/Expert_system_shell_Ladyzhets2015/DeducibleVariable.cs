using System;

namespace Expert_system_shell_Ladyzhets2015
{
	[Serializable]
	public class DeducibleVariable : Variable
	{
		[NonSerialized]
		private bool tryingToGetValue;

		public DeducibleVariable(string name, Type type) : base(name, type)
		{
		}

		public override void prepareForNewConsultation()
		{
			this.tryingToGetValue = false;
			base.prepareForNewConsultation();
		}

		public override bool tryToGetValue()
		{
			Global.exposition.printText("Цель = " + base.Name);
			bool result;
			if (this.valueWasSet)
			{
				Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (было выведено ранее)");
				result = true;
			}
			else
			{
				this.tryingToGetValue = true;
				if (Global.knowledgeBase == null)
				{
					throw new InvalidOperationException();
				}
				bool flag = Global.knowledgeBase.tryToGetValue(this);
				this.tryingToGetValue = false;
				if (flag)
				{
					Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (было выведено)");
				}
				else
				{
					Global.exposition.printText(base.Name + " - не было выведено ");
				}
				result = flag;
			}
			return result;
		}
	}
}
