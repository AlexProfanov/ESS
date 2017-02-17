using System;

namespace ProductionShell
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
				Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (вывелось раньше)");
				result = true;
			}
			else
			{
				if (this.tryingToGetValue)
				{
					throw new InvalidOperationException("Построенная система правил допускает рекурсию (на переменной - " + this.ToString() + ")");
				}
				this.tryingToGetValue = true;
				if (Global.knowledgeBase == null)
				{
					throw new InvalidOperationException();
				}
				bool flag = Global.knowledgeBase.tryToGetValue(this);
				this.tryingToGetValue = false;
				if (flag)
				{
					Global.exposition.printText(base.Name + " = " + base.Value.ToString() + " (вывелось)");
				}
				else
				{
					Global.exposition.printText(base.Name + " - не вывелось ");
				}
				result = flag;
			}
			return result;
		}
	}
}
