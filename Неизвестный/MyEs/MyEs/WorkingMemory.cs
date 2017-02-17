using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEs
{
	public static class WorkingMemory
	{
		public class pair
		{
			public int var;

			public int val;

			public pair(int _var, int _val)
			{
				this.var = _var;
				this.val = _val;
			}
		}

		public static Dictionary<int, int> KnownVars = new Dictionary<int, int>();

		public static List<int> AllRules = new List<int>();

		public static Stack<int> StackRules = new Stack<int>();

		public static List<int> Chain = new List<int>();

		public static List<int> InAccessedRules = new List<int>();

		public static bool success;

		public static List<List<WorkingMemory.pair>> ForAll = new List<List<WorkingMemory.pair>>();

		public static bool finish;

		private static VariablesList Vars;

		private static RulesList Rules;

		private static DomainList Domains;

		private static int Goal;

		private static int id;

		private static bool OrderRules;

		public static void InitMemory(DomainList dom, VariablesList vars, RulesList ruls, int GoalVar, bool order)
		{
			WorkingMemory.Domains = dom;
			WorkingMemory.Vars = vars;
			WorkingMemory.Rules = ruls;
			WorkingMemory.Goal = GoalVar;
			WorkingMemory.KnownVars.Clear();
			WorkingMemory.InAccessedRules.Clear();
			WorkingMemory.StackRules.Clear();
			WorkingMemory.Chain.Clear();
			WorkingMemory.AllRules.Clear();
			WorkingMemory.ForAll.Clear();
			WorkingMemory.id = -1;
			WorkingMemory.finish = false;
			WorkingMemory.success = true;
			WorkingMemory.OrderRules = order;
		}

		public static void StartFinding()
		{
			WorkingMemory.Finding(WorkingMemory.Goal, 0);
		}

		private static void Finding(int GoalVar, int RulIndex)
		{
			if (WorkingMemory.success)
			{
				Variable varById = WorkingMemory.Vars.GetVarById(GoalVar);
				if (varById.type != "Запрашиваемая")
				{
					List<int> list = new List<int>();
					list = WorkingMemory.Rules.GetRulesForVar(GoalVar, RulIndex);
					if (list.Count == 0)
					{
						if (varById.type != "Выводимая")
						{
							WorkingMemory.AskQuestion(GoalVar);
						}
						else if (GoalVar == WorkingMemory.Goal)
						{
						}
					}
					else
					{
						int num = 0;
						while (!WorkingMemory.KnownVars.ContainsKey(GoalVar) && num < list.Count)
						{
							if (!WorkingMemory.StackRules.Contains(Enumerable.ToList<int>(list)[num]))
							{
								WorkingMemory.StackRules.Push(Enumerable.ToList<int>(list)[num]);
								WorkingMemory.AllRules.Add(Enumerable.ToList<int>(list)[num]);
								int num2 = WorkingMemory.AllRules.Count - 1;
								WorkingMemory.ForAll.Add(new List<WorkingMemory.pair>());
								WorkingMemory.id++;
								List<int> varsInRule = WorkingMemory.Rules.GetVarsInRule(Enumerable.ToList<int>(list)[num]);
								int num3 = 0;
								for (int i = 0; i < varsInRule.Count; i++)
								{
									if (WorkingMemory.KnownVars.ContainsKey(varsInRule[i]))
									{
										if (WorkingMemory.KnownVars[varsInRule[i]] != WorkingMemory.Rules.GetRuleById(Enumerable.ToList<int>(list)[num]).IfVars[i].Value)
										{
											break;
										}
										num3++;
									}
									else
									{
										if (WorkingMemory.OrderRules)
										{
											WorkingMemory.Finding(varsInRule[i], WorkingMemory.Rules.GetRuleIndex(Enumerable.ToList<int>(list)[num]));
										}
										else
										{
											WorkingMemory.Finding(varsInRule[i], 0);
										}
										if (!WorkingMemory.success)
										{
											return;
										}
										if (!WorkingMemory.KnownVars.ContainsKey(varsInRule[i]))
										{
											break;
										}
										if (WorkingMemory.KnownVars[varsInRule[i]] != WorkingMemory.Rules.GetRuleById(Enumerable.ToList<int>(list)[num]).IfVars[i].Value)
										{
											break;
										}
										num3++;
									}
								}
								List<RulePair> ifVars = WorkingMemory.Rules.GetRuleById(Enumerable.ToList<int>(list)[num]).IfVars;
								for (int i = 0; i < ifVars.Count; i++)
								{
									if (WorkingMemory.KnownVars.ContainsKey(ifVars[i].Variable))
									{
										WorkingMemory.ForAll[num2].Add(new WorkingMemory.pair(ifVars[i].Variable, WorkingMemory.KnownVars[ifVars[i].Variable]));
									}
									else
									{
										WorkingMemory.ForAll[num2].Add(new WorkingMemory.pair(ifVars[i].Variable, -1));
									}
								}
								if (num3 == varsInRule.Count)
								{
									List<RulePair> thenVars = WorkingMemory.Rules.GetRuleById(Enumerable.ToList<int>(list)[num]).ThenVars;
									for (int i = 0; i < thenVars.Count; i++)
									{
										if (WorkingMemory.KnownVars.ContainsKey(thenVars[i].Variable))
										{
											WorkingMemory.KnownVars.Remove(thenVars[i].Variable);
										}
										WorkingMemory.KnownVars.Add(thenVars[i].Variable, thenVars[i].Value);
									}
									WorkingMemory.Chain.Add(WorkingMemory.StackRules.Pop());
								}
								else
								{
									WorkingMemory.StackRules.Pop();
									num++;
								}
								WorkingMemory.id--;
							}
							else
							{
								num++;
							}
						}
						if (!WorkingMemory.KnownVars.ContainsKey(GoalVar) && varById.type == "Выводимо-запрашиваемая")
						{
							WorkingMemory.AskQuestion(GoalVar);
						}
						if (GoalVar == WorkingMemory.Goal)
						{
							if (WorkingMemory.KnownVars.ContainsKey(WorkingMemory.Goal))
							{
								WorkingMemory.success = true;
							}
						}
					}
				}
				else
				{
					WorkingMemory.AskQuestion(GoalVar);
				}
			}
		}

		private static void AskQuestion(int GoalVar)
		{
			Variable varById = WorkingMemory.Vars.GetVarById(GoalVar);
			Consult consult = new Consult();
			consult.SetQuestion(varById.Question, WorkingMemory.Domains.GetDomainValues(varById.domain));
			consult.ShowDialog();
			if (consult.success)
			{
				WorkingMemory.KnownVars.Add(GoalVar, consult.IdAnswer);
			}
			else
			{
				WorkingMemory.finish = true;
				WorkingMemory.success = false;
			}
		}
	}
}
