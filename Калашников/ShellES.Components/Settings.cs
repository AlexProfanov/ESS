using ShellES.Entities;
using System;

namespace ShellES.Components
{
	[Serializable]
	public class Settings : ComponentPrototype
	{
		public enum eSTRATEGY_SELECT
		{
			FIFO,
			LIFO,
			PRIORITY,
			CF,
			COST,
			UNKNOWN,
			RANDOM
		}

		public enum eSTRATEGY_CFJO
		{
			MIN,
			PROIZVED,
			MIDDLE
		}

		public enum eSTRATEGY_CFCO
		{
			MAX,
			BAYES,
			MIDDLE
		}

		public enum eSTRATEGY_MLV_DIRECTION
		{
			STRAIGHT,
			BACKWARD
		}

		public const int DEFAULT_CF = 100;

		public const int DEFAULT_ENTER_CF = 100;

		public const int DEFAULT_EDGE_CF = 20;

		public const int MAX_CF = 100;

		public const int DEFAULT_RULE_PRIORITY = 0;

		private const Settings.eSTRATEGY_SELECT DEFAULT_STRATEGY_SELECTION_RULE = Settings.eSTRATEGY_SELECT.FIFO;

		private const Settings.eSTRATEGY_MLV_DIRECTION DEFAULT_STRATEGY_MLV_DIRECTION = Settings.eSTRATEGY_MLV_DIRECTION.BACKWARD;

		private const Settings.eSTRATEGY_CFCO DEFAULT_STRATEGY_CFCO_FACT_IN_PREM = Settings.eSTRATEGY_CFCO.BAYES;

		private const Settings.eSTRATEGY_CFJO DEFAULT_STRATEGY_CFJO_ALL_PREM = Settings.eSTRATEGY_CFJO.MIN;

		private const Settings.eSTRATEGY_CFCO DEFAULT_STRATEGY_CFCO_ALL_CONCL = Settings.eSTRATEGY_CFCO.BAYES;

		private const Settings.eSTRATEGY_CFJO DEFAULT_STRATEGY_CFJO_FACT_IN_CONCL = Settings.eSTRATEGY_CFJO.PROIZVED;

		private const Settings.eSTRATEGY_CFCO DEFAULT_STRATEGY_CFVA_JOIN_VARIABLES = Settings.eSTRATEGY_CFCO.BAYES;

		public static string[] Str_SELECT = new string[]
		{
			"FIFO",
			"LIFO",
			"Priority",
			"CF",
			"Cost",
			"Unknown",
			"Random"
		};

		public static string[] Str_CFJO = new string[]
		{
			"Минимум",
			"Произведение вероятностей",
			"Среднее"
		};

		public static string[] Str_CFСO = new string[]
		{
			"Максимум",
			"Байесовская",
			"Среднее"
		};

		public static string[] Str_MLV = new string[]
		{
			"Прямой",
			"Обратный"
		};

		private Settings.eSTRATEGY_SELECT sel_STRGY;

		private Settings.eSTRATEGY_MLV_DIRECTION mlv_Direction;

		private Settings.eSTRATEGY_CFCO cfFactPrem;

		private Settings.eSTRATEGY_CFJO cfPrem;

		private Settings.eSTRATEGY_CFCO cfConcl;

		private Settings.eSTRATEGY_CFJO cfFactConcl;

		private Settings.eSTRATEGY_CFCO cfVA;

		private int edge_unknown;

		private ESVars goal;

		public int EDGE_UNKNOWN
		{
			get
			{
				return this.edge_unknown;
			}
			set
			{
				this.edge_unknown = value;
			}
		}

		public ESVars Goal
		{
			get
			{
				return this.goal;
			}
			set
			{
				this.goal = value;
			}
		}

		public Settings.eSTRATEGY_SELECT SelStrategy
		{
			get
			{
				return this.sel_STRGY;
			}
			set
			{
				this.sel_STRGY = value;
			}
		}

		public Settings.eSTRATEGY_MLV_DIRECTION MLV_Direction
		{
			get
			{
				return this.mlv_Direction;
			}
			set
			{
				this.mlv_Direction = value;
			}
		}

		public Settings.eSTRATEGY_CFJO CFPrem
		{
			get
			{
				return this.cfPrem;
			}
			set
			{
				this.cfPrem = value;
			}
		}

		public Settings.eSTRATEGY_CFCO CFFact
		{
			get
			{
				return this.cfFactPrem;
			}
			set
			{
				this.cfFactPrem = value;
			}
		}

		public Settings.eSTRATEGY_CFCO CFConcl
		{
			get
			{
				return this.cfConcl;
			}
			set
			{
				this.cfConcl = value;
			}
		}

		public Settings.eSTRATEGY_CFJO CFFactConcl
		{
			get
			{
				return this.cfFactConcl;
			}
			set
			{
				this.cfFactConcl = value;
			}
		}

		public Settings.eSTRATEGY_CFCO CFVA
		{
			get
			{
				return this.cfVA;
			}
			set
			{
				this.cfVA = value;
			}
		}

		public Settings(ExpertSystemShell ess) : base(ess)
		{
			this.ResetDefaults();
		}

		public void ResetDefaults()
		{
			this.sel_STRGY = Settings.eSTRATEGY_SELECT.FIFO;
			this.mlv_Direction = Settings.eSTRATEGY_MLV_DIRECTION.BACKWARD;
			this.cfFactPrem = Settings.eSTRATEGY_CFCO.BAYES;
			this.cfPrem = Settings.eSTRATEGY_CFJO.MIN;
			this.cfConcl = Settings.eSTRATEGY_CFCO.BAYES;
			this.cfFactConcl = Settings.eSTRATEGY_CFJO.PROIZVED;
			this.cfVA = Settings.eSTRATEGY_CFCO.BAYES;
			this.edge_unknown = 20;
			this.goal = null;
		}

		public static int CalculateStrategyCFJO(Settings.eSTRATEGY_CFJO strategy, int cf1, int cf2)
		{
			switch (strategy)
			{
			case Settings.eSTRATEGY_CFJO.MIN:
				return Math.Min(cf1, cf2);
			case Settings.eSTRATEGY_CFJO.PROIZVED:
				return cf1 * cf2 / 100;
			case Settings.eSTRATEGY_CFJO.MIDDLE:
				return (Math.Min(cf1, cf2) + cf1 * cf2 / 100) / 2;
			default:
				return Math.Min(cf1, cf2);
			}
		}

		public static int CalculateStrategyCFCO(Settings.eSTRATEGY_CFCO strategy, int cf1, int cf2)
		{
			switch (strategy)
			{
			case Settings.eSTRATEGY_CFCO.MAX:
				return Math.Max(cf1, cf2);
			case Settings.eSTRATEGY_CFCO.BAYES:
				return cf1 + cf2 - cf1 * cf2 / 100;
			case Settings.eSTRATEGY_CFCO.MIDDLE:
				return Math.Max(cf1, cf2) + cf1 + cf2 - cf1 * cf2 / 100;
			default:
				return cf1 + cf2 - cf1 * cf2 / 100;
			}
		}

		public static int NormalizeCF(int cf)
		{
			cf = ((cf < 0) ? 0 : cf);
			cf = ((cf > 100) ? 100 : cf);
			return cf;
		}

		public static Enum SetEnumElement(Type enType, string val)
		{
			return (Enum)Enum.Parse(enType, val, true);
		}
	}
}
