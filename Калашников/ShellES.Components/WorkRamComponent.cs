using ShellES.Entities;
using System;
using System.Collections.Generic;

namespace ShellES.Components
{
	public class WorkRamComponent : ComponentPrototype
	{
		public List<ESFact> RejectedFacts;

		public List<ESRules> RejectedRules;

		public List<ESVars> StackOfSolvingVariables;

		public WorkRamComponent(ExpertSystemShell ess) : base(ess)
		{
			this.RejectedFacts = new List<ESFact>();
			this.RejectedRules = new List<ESRules>();
			this.StackOfSolvingVariables = new List<ESVars>();
		}

		public void ClearRam()
		{
			this.RejectedFacts.Clear();
			this.RejectedRules.Clear();
			this.StackOfSolvingVariables.Clear();
		}

		public bool isDeadFact(ESFact fact)
		{
			if (fact.Variable == null || fact.Value == null)
			{
				return false;
			}
			for (int i = 0; i < this.RejectedFacts.Count; i++)
			{
				if (fact == this.RejectedFacts[i])
				{
					return true;
				}
				if (this.RejectedFacts[i].Variable == fact.Variable && this.RejectedFacts[i].Value == fact.Value)
				{
					return true;
				}
			}
			return false;
		}

		public bool isDeadRule(ESRules rule)
		{
			return this.RejectedRules.Contains(rule);
		}

		public bool isAlreadySolving(ESVars var)
		{
			return this.StackOfSolvingVariables.Contains(var);
		}
	}
}
