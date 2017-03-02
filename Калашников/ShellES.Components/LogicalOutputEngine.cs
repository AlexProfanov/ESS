using ShellES.Entities;
using System;
using System.Collections.Generic;

namespace ShellES.Components
{
	public class LogicalOutputEngine : ComponentPrototype
	{
		public LogicalOutputEngine(ExpertSystemShell ess) : base(ess)
		{
		}

		public void Consult()
		{
			this.ESshell.WorkRAM.ClearRam();
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.START_CONSULTATION, DateTime.Now.ToLocalTime().ToString());
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GET_SETTINGS, "");
			switch (this.ESshell.Settings.MLV_Direction)
			{
			case Settings.eSTRATEGY_MLV_DIRECTION.STRAIGHT:
				if (this.SolveGoalStraight(this.ESshell.Settings.Goal))
				{
					this.ESshell.ComExplain.ProcessEvent(eTypeEvent.END_CONSULTATION_GOOD, "");
					return;
				}
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.END_CONSULTATION_BAD, "");
				return;
			case Settings.eSTRATEGY_MLV_DIRECTION.BACKWARD:
				if (this.SolveGoalBackWard(this.ESshell.Settings.Goal))
				{
					this.ESshell.ComExplain.ProcessEvent(eTypeEvent.END_CONSULTATION_GOOD, "");
					return;
				}
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.END_CONSULTATION_BAD, "");
				return;
			default:
				return;
			}
		}

		private bool SolveGoalBackWard(ESVars goal)
		{
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.SET_GOAL, goal, "");
			if (goal.IsDefine(this.ESshell.Settings.EDGE_UNKNOWN))
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GOAL_HAVE_VALUE, goal, "");
				return true;
			}
			if (goal.VarType == ESVars.VAR_TYPE.Запрашиваемая)
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GOAL_ASK, goal, "");
				this.AskForVariable(goal);
				return this.CheckForDefine(goal, this.ESshell.Settings.EDGE_UNKNOWN);
			}
			if (this.ESshell.WorkRAM.isAlreadySolving(goal))
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.ERROR_GOAL_ALREADY_PROVING, goal, "");
				return false;
			}
			this.ESshell.WorkRAM.StackOfSolvingVariables.Add(goal);
			List<ESRules> conflictRuleSetBackward = this.GetConflictRuleSetBackward(goal, true);
			bool flag = false;
			while (conflictRuleSetBackward.Count > 0 && !flag)
			{
				ESRules eSRules = this.ChooseRuleFromSet(conflictRuleSetBackward, this.ESshell.Settings.SelStrategy);
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.CHOOSE_RULE, eSRules, "");
				flag = this.TryToProveRule(eSRules, true);
				if (!flag)
				{
					conflictRuleSetBackward.Remove(eSRules);
				}
			}
			if (conflictRuleSetBackward.Count == 0)
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.HAVE_NO_ANY_RULE, goal, "");
			}
			this.ESshell.WorkRAM.StackOfSolvingVariables.Remove(goal);
			if (goal.VarType == ESVars.VAR_TYPE.Выводимо_Запрашиваемая && (!flag || (flag && !goal.IsDefine(this.ESshell.Settings.EDGE_UNKNOWN))))
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GOAL_ASK_AFTER_TRYING_PROVE, goal, "");
				this.AskForVariable(goal);
			}
			return this.CheckForDefine(goal, this.ESshell.Settings.EDGE_UNKNOWN);
		}

		private bool SolveGoalStraight(ESVars goal)
		{
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.SET_GOAL, goal, "");
			if (goal.IsDefine(this.ESshell.Settings.EDGE_UNKNOWN))
			{
				return true;
			}
			if (goal.VarType == ESVars.VAR_TYPE.Запрашиваемая)
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GOAL_ASK, goal, "");
				this.AskForVariable(goal);
				return this.CheckForDefine(goal, this.ESshell.Settings.EDGE_UNKNOWN);
			}
			bool flag = true;
			List<ESRules> conflictRuleSetStraight = this.GetConflictRuleSetStraight(goal, flag);
			bool flag2 = false;
			while (conflictRuleSetStraight.Count > 0 && !flag2)
			{
				ESRules eSRules = this.ChooseRuleFromSet(conflictRuleSetStraight, this.ESshell.Settings.SelStrategy);
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.CHOOSE_RULE, eSRules, "");
				if (this.AskEveryThingForRule(eSRules, flag))
				{
					this.RuleFired(eSRules);
					flag2 = goal.IsDefine(this.ESshell.Settings.EDGE_UNKNOWN);
					if (flag2)
					{
						this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GOAL_MEET, goal, "");
					}
					else
					{
						this.ESshell.WorkRAM.RejectedRules.Add(eSRules);
						conflictRuleSetStraight = this.GetConflictRuleSetStraight(goal, flag);
					}
				}
				else
				{
					this.ESshell.ComExplain.ProcessEvent(eTypeEvent.CANNOT_PROVE_RULE, eSRules, "");
					conflictRuleSetStraight = this.GetConflictRuleSetStraight(goal, flag);
				}
			}
			if (conflictRuleSetStraight.Count == 0)
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.HAVE_NO_ANY_RULE, goal, "");
			}
			if (goal.VarType == ESVars.VAR_TYPE.Выводимо_Запрашиваемая && !flag2)
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GOAL_ASK_AFTER_TRYING_PROVE, goal, "");
				this.AskForVariable(goal);
			}
			return this.CheckForDefine(goal, this.ESshell.Settings.EDGE_UNKNOWN);
		}

		private ESRules ChooseRuleFromSet(List<ESRules> ruleSet, Settings.eSTRATEGY_SELECT Strategy)
		{
			switch (Strategy)
			{
			case Settings.eSTRATEGY_SELECT.FIFO:
				return ruleSet[0];
			case Settings.eSTRATEGY_SELECT.LIFO:
				return ruleSet[ruleSet.Count - 1];
			case Settings.eSTRATEGY_SELECT.PRIORITY:
			{
				int i = 1;
				int index = 0;
				while (i < ruleSet.Count)
				{
					if (ruleSet[i].Priority > ruleSet[index].Priority)
					{
						index = i;
					}
					i++;
				}
				return ruleSet[index];
			}
			case Settings.eSTRATEGY_SELECT.CF:
			{
				int i = 1;
				int index = 0;
				while (i < ruleSet.Count)
				{
					if (ruleSet[i].RuleCF > ruleSet[index].RuleCF)
					{
						index = i;
					}
					i++;
				}
				return ruleSet[index];
			}
			case Settings.eSTRATEGY_SELECT.COST:
			{
				int i = 1;
				int index = 0;
				while (i < ruleSet.Count)
				{
					if (ruleSet[i].Premises.Count < ruleSet[index].Premises.Count)
					{
						index = i;
					}
					i++;
				}
				return ruleSet[index];
			}
			case Settings.eSTRATEGY_SELECT.UNKNOWN:
			{
				int num = ruleSet[0].HowMuchPremsIsUnknown(this.ESshell.Settings.EDGE_UNKNOWN);
				int i = 1;
				int index = 0;
				while (i < ruleSet.Count)
				{
					int num2 = ruleSet[i].HowMuchPremsIsUnknown(this.ESshell.Settings.EDGE_UNKNOWN);
					if (num2 < num)
					{
						num = num2;
						index = i;
					}
					i++;
				}
				return ruleSet[index];
			}
			case Settings.eSTRATEGY_SELECT.RANDOM:
			{
				Random random = new Random(Environment.TickCount);
				return ruleSet[random.Next(ruleSet.Count - 1)];
			}
			default:
				return ruleSet[0];
			}
		}

		private bool CheckForDefine(ESVars goal, int EDGE)
		{
			if (goal.IsDefine(EDGE))
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.SOLVED_GOAL, goal, "");
				return true;
			}
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.CANNOT_SOLVE_GOAL, goal, "");
			return false;
		}

		private List<ESRules> GetConflictRuleSetStraight(ESVars goal, bool useAsk_Prove)
		{
			List<ESRules> list = new List<ESRules>();
			foreach (ESRules current in this.ESshell.Rules)
			{
				if (!this.ESshell.WorkRAM.isDeadRule(current) && current.isReadyToFire(this.ESshell.Settings.EDGE_UNKNOWN, useAsk_Prove))
				{
					list.Add(current);
				}
			}
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GET_RULES_SET2, goal, "[" + StaticHelper.GetСклонение("правил", list.Count) + "]");
			return list;
		}

		private List<ESRules> GetConflictRuleSetBackward(ESVars goal, bool WatchDead)
		{
			List<ESRules> list = new List<ESRules>();
			foreach (ESRules current in this.ESshell.Rules)
			{
				if (current.HaveFactInCons(goal))
				{
					if (WatchDead)
					{
						if (!this.ESshell.WorkRAM.isDeadRule(current))
						{
							list.Add(current);
						}
					}
					else
					{
						list.Add(current);
					}
				}
			}
			string text = WatchDead ? ", с исключением" : ", без исключения";
			text += " несработавших правил";
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GET_RULES_SET, goal, "[" + StaticHelper.GetСклонение("правил", list.Count) + "]" + text);
			return list;
		}

		private bool TryToProveRule(ESRules rule, bool WatchDead)
		{
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.TRY_PROVE_RULE, rule, "");
			if (WatchDead && this.ESshell.WorkRAM.isDeadRule(rule))
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.DEAD_RULE, rule, "");
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.CANNOT_PROVE_RULE, rule, "");
				return false;
			}
			int i = 0;
			while (i < rule.Premises.Count)
			{
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.TRY_PROVE_PREM, rule.Premises[i], (i + 1).ToString());
				if (this.ESshell.WorkRAM.isDeadFact(rule.Premises[i]))
				{
					this.ESshell.ComExplain.ProcessEvent(eTypeEvent.PREM_DEAD, rule.Premises[i], "");
				}
				else
				{
					if (this.SolveGoalBackWard(rule.Premises[i].Variable))
					{
						if (rule.Premises[i].Variable.GetValueCF(rule.Premises[i].Value.Value) > this.ESshell.Settings.EDGE_UNKNOWN)
						{
							this.ESshell.ComExplain.ProcessEvent(eTypeEvent.PROVED_PREM, rule.Premises[i], "");
							i++;
							continue;
						}
						this.ESshell.ComExplain.ProcessEvent(eTypeEvent.CANNOT_PROVE_PREM_BECAUSE_OF_CF, rule.Premises[i], "");
					}
					else
					{
						this.ESshell.ComExplain.ProcessEvent(eTypeEvent.CANNOT_PROVE_PREM_BECAUSE_OF_VAR, rule.Premises[i], "");
					}
					this.ESshell.WorkRAM.RejectedFacts.Add(rule.Premises[i]);
				}
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.CANNOT_PROVE_RULE, rule, "");
				this.ESshell.WorkRAM.RejectedRules.Add(rule);
				return false;
			}
			this.RuleFired(rule);
			return true;
		}

		private void RuleFired(ESRules rule)
		{
			int conclRuleCf = this.CalculateAllConclCF(rule);
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.PROVED_RULE, rule, conclRuleCf.ToString());
			foreach (ESFact current in rule.Consequences)
			{
				int newCF = this.CalculateConclFactCF(current, conclRuleCf);
				int cF = this.CalculateJOINVariable(current.Value.Value, newCF, current.Variable);
				current.Variable.AddValue(current.Value.Value, cF);
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.SOLVED_CONCLUSION_FACT, current, "CF = " + newCF.ToString() + ", после - CF = " + cF.ToString());
				this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GOAL_HAS_VALUE, current.Variable, "");
			}
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.PROVED_RULE_THATS_ALL, rule, "");
		}

		private void AskForVariable(ESVars variable)
		{
			this.ESshell.ComKPZ.SetNextQuestion(variable);
			this.ESshell.PauseMLV();
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.GOAL_HAS_VALUE, variable, "");
		}

		private bool AskEveryThingForRule(ESRules rule, bool checkAskProveVar)
		{
			this.ESshell.ComExplain.ProcessEvent(eTypeEvent.TRY_PROVE_RULE, rule, "");
			foreach (ESFact current in rule.Premises)
			{
				if (!current.Variable.IsDefine(this.ESshell.Settings.EDGE_UNKNOWN) && (current.Variable.VarType == ESVars.VAR_TYPE.Запрашиваемая || (current.Variable.VarType == ESVars.VAR_TYPE.Выводимо_Запрашиваемая && checkAskProveVar)))
				{
					this.ESshell.ComExplain.ProcessEvent(eTypeEvent.PREM_ASK, current, "");
					this.AskForVariable(current.Variable);
					this.ESshell.ComExplain.ProcessEvent(eTypeEvent.PREM_ASK_RESULT, current, "");
					if (current.Variable.GetValueCF(current.Value.Value) <= this.ESshell.Settings.EDGE_UNKNOWN)
					{
						this.ESshell.ComExplain.ProcessEvent(eTypeEvent.CANNOT_PROVE_PREM_BECAUSE_OF_CF, current, "do not get up level");
						return false;
					}
					this.ESshell.ComExplain.ProcessEvent(eTypeEvent.PROVED_PREM, current, "do not get up level");
				}
			}
			return true;
		}

		private int CalculatePremFactCF(ESFact fact)
		{
			int factCF = fact.FactCF;
			int cf = fact.Variable.Value[fact.Value.Value];
			int cf2 = Settings.CalculateStrategyCFCO(this.ESshell.Settings.CFFact, factCF, cf);
			return Settings.NormalizeCF(cf2);
		}

		private int CalculateAllPremCF(ESRules rule)
		{
			int num = this.CalculatePremFactCF(rule.Premises[0]);
			for (int i = 1; i < rule.Premises.Count; i++)
			{
				int cf = this.CalculatePremFactCF(rule.Premises[i]);
				num = Settings.CalculateStrategyCFJO(this.ESshell.Settings.CFPrem, num, cf);
			}
			return Settings.NormalizeCF(num);
		}

		private int CalculateAllConclCF(ESRules rule)
		{
			int cf = this.CalculateAllPremCF(rule);
			int cf2 = Settings.CalculateStrategyCFCO(this.ESshell.Settings.CFConcl, cf, rule.RuleCF);
			return Settings.NormalizeCF(cf2);
		}

		private int CalculateConclFactCF(ESFact fact, int conclRuleCf)
		{
			int cf = Settings.CalculateStrategyCFJO(this.ESshell.Settings.CFFactConcl, fact.FactCF, conclRuleCf);
			return Settings.NormalizeCF(cf);
		}

		private int CalculateJOINVariable(string strValue, int newCF, ESVars var)
		{
			newCF = Settings.NormalizeCF(newCF);
			if (var.ExistValue(strValue))
			{
				newCF = Settings.CalculateStrategyCFCO(this.ESshell.Settings.CFVA, newCF, var.GetValueCF(strValue));
			}
			return Settings.NormalizeCF(newCF);
		}
	}
}
