using System;
using System.Collections.Generic;

namespace MyEs
{
	[Serializable]
	public class Rule : cc
	{
		public string Name;

		public string Reason;

		public List<RulePair> IfVars;

		public List<RulePair> ThenVars;

		public Rule()
		{
			this.Name = "";
			this.IfVars = new List<RulePair>();
			this.ThenVars = new List<RulePair>();
		}

		public object Clone()
		{
			Rule rule = new Rule();
			rule.Name = this.Name;
			rule.Reason = this.Reason;
			using (List<RulePair>.Enumerator enumerator = this.IfVars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RulePair current = enumerator.Current;
					rule.IfVars.Add((RulePair)current.Clone());
				}
			}
			using (List<RulePair>.Enumerator enumerator = this.ThenVars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RulePair current = enumerator.Current;
					rule.ThenVars.Add((RulePair)current.Clone());
				}
			}
			return rule;
		}
	}
}
