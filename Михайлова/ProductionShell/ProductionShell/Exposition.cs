using System;

namespace ProductionShell
{
	public abstract class Exposition
	{
		public virtual void startRecording()
		{
		}

		public virtual void goToLowLevel()
		{
		}

		public virtual void goToHighLevel()
		{
		}

		public virtual void printText(string text)
		{
		}

		public virtual void deleteNode(string text)
		{
		}
	}
}
