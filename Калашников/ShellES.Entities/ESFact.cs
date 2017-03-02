using System;
using System.Collections.Generic;

namespace ShellES.Entities
{
	[Serializable]
	public class ESFact
	{
		private ESVars variable;

		private ESDomainValue value;

		private int factCF;

		public ESVars Variable
		{
			get
			{
				return this.variable;
			}
			set
			{
				this.variable = value;
			}
		}

		public ESDomainValue Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		public int FactCF
		{
			get
			{
				return this.factCF;
			}
			set
			{
				this.factCF = value;
			}
		}

		public ESFact()
		{
			this.variable = null;
			this.value = null;
			this.factCF = 100;
		}

		public ESFact(ESVars v, ESDomainValue dv)
		{
			this.variable = v;
			this.value = dv;
			this.factCF = 100;
		}

		public ESFact(ESVars v, ESDomainValue dv, int cf)
		{
			this.variable = v;
			this.value = dv;
			this.factCF = cf;
		}

		public string GetPresentation(bool isExtended)
		{
			string text = string.Format("{0} = {1}", (this.variable != null) ? this.variable.Name : "<нет переменной>", (this.value != null) ? this.value.Value : "<нет значения>");
			if (!isExtended)
			{
				return text;
			}
			return text + " [" + this.factCF.ToString() + "]";
		}

		public List<ESDomainValue> GetDomainValuesFromFact()
		{
			if (this.variable == null)
			{
				return null;
			}
			if (this.variable.Domain == null)
			{
				return null;
			}
			return this.variable.Domain.Elements;
		}
	}
}
