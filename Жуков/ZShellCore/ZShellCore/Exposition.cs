using System;

namespace ZShellCore
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
	}
}
