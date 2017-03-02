using System;

namespace ShellES.Components
{
	[Serializable]
	public abstract class ComponentPrototype
	{
		[NonSerialized]
		protected ExpertSystemShell ESshell;

		public ComponentPrototype(ExpertSystemShell ess)
		{
			this.ESshell = ess;
		}
	}
}
