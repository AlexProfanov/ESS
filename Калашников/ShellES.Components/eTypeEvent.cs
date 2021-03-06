using System;

namespace ShellES.Components
{
	public enum eTypeEvent
	{
		START_CONSULTATION,
		GET_SETTINGS,
		END_CONSULTATION_BREAK,
		END_CONSULTATION_GOOD,
		END_CONSULTATION_BAD,
		SET_GOAL,
		GOAL_HAVE_VALUE,
		GOAL_ASK,
		GOAL_MEET,
		ERROR_GOAL_ALREADY_PROVING,
		GET_RULES_SET,
		GET_RULES_SET2,
		HAVE_NO_ANY_RULE,
		SOLVED_GOAL,
		GOAL_HAS_VALUE,
		GOAL_ASK_AFTER_TRYING_PROVE,
		CANNOT_SOLVE_GOAL,
		CHOOSE_RULE,
		TRY_PROVE_RULE,
		CANNOT_PROVE_RULE,
		DEAD_RULE,
		PROVED_RULE,
		PROVED_RULE_THATS_ALL,
		TRY_PROVE_PREM,
		PROVED_PREM,
		PREM_DEAD,
		CANNOT_PROVE_PREM_BECAUSE_OF_VAR,
		CANNOT_PROVE_PREM_BECAUSE_OF_CF,
		PREM_ASK,
		PREM_ASK_RESULT,
		SOLVED_CONCLUSION_FACT
	}
}
