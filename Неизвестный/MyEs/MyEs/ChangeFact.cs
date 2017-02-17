using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyEs
{
	public class ChangeFact : Form
	{
		public int IdRule;

		public int index;

		public int mode;

		public KnowledgeBase KB;

		public string output;

		public MainForm MForm;

		private IContainer components = null;

		private Button BtnOk;

		private Button BtnOkContinue;

		private Button BtnCancel;

		private ComboBox CmbVar;

		private ComboBox CmbValue;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		public ChangeFact()
		{
			this.InitializeComponent();
		}

		private void ChangeFact_Load(object sender, EventArgs e)
		{
			List<string> varsNames = this.KB.Vars.GetVarsNames();
			using (List<string>.Enumerator enumerator = varsNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					this.CmbVar.Items.Add(current);
				}
			}
			if (this.mode < 2)
			{
				this.BtnOkContinue.Enabled = true;
				this.BtnOkContinue.Visible = true;
				if (this.mode == 0)
				{
					this.Text = "Adding new fact into the hypothesis";
				}
				else
				{
					this.Text = "Adding new fact into the conclusion";
				}
				if (this.CmbVar.Items.Count > 0)
				{
					this.CmbVar.Text = this.CmbVar.Items[0].ToString();
				}
			}
			else
			{
				Rule ruleById = this.KB.Rules.GetRuleById(this.IdRule);
				if (this.mode == 2)
				{
					Variable varById = this.KB.Vars.GetVarById(ruleById.IfVars[this.index].Variable);
					this.CmbVar.Text = varById.Name;
					this.CmbValue.Text = this.KB.Domains.GetDomainValueNameById(varById.domain, ruleById.IfVars[this.index].Value);
				}
				else
				{
					Variable varById = this.KB.Vars.GetVarById(ruleById.ThenVars[this.index].Variable);
					this.CmbVar.Text = varById.Name;
					this.CmbValue.Text = this.KB.Domains.GetDomainValueNameById(varById.domain, ruleById.ThenVars[this.index].Value);
				}
			}
		}

		private void CmbVar_TextChanged(object sender, EventArgs e)
		{
			int idVarByName = this.KB.Vars.GetIdVarByName(this.CmbVar.Text);
			if (idVarByName > -1)
			{
				List<string> list = Enumerable.ToList<string>(this.KB.Domains.GetDomainValues(this.KB.Vars.GetVarById(idVarByName).domain).Values);
				this.CmbValue.Items.Clear();
				using (List<string>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						this.CmbValue.Items.Add(current);
					}
				}
				if (!list.Contains(this.CmbValue.Text))
				{
					this.CmbValue.SelectedIndex = 0;
				}
			}
			else
			{
				this.CmbValue.Text = "";
				this.CmbValue.Items.Clear();
			}
		}

		private bool TestFact()
		{
			int idVarByName = this.KB.Vars.GetIdVarByName(this.CmbVar.Text);
			bool result;
			if (idVarByName == -1)
			{
				if (MessageBox.Show("Указанной переменной нет в базе. Вы хотите ее добавить?", "", 4) == 6)
				{
					VariablesForm variablesForm = new VariablesForm();
					DataGridView gridVars = ((MainForm)base.Owner.Owner).GridVars;
					int rowCount = gridVars.RowCount;
					variablesForm.index = rowCount;
					variablesForm.KB = this.KB;
					variablesForm.TxtName.Text = this.CmbVar.Text;
					variablesForm.Context = true;
					variablesForm.MForm = this.MForm;
					DialogResult dialogResult = variablesForm.ShowDialog(this);
					if (dialogResult != 2)
					{
						((MainForm)base.Owner.Owner).AddToTree(2, variablesForm.output, rowCount);
						gridVars.Rows.Insert(rowCount, 1);
						gridVars.Rows[rowCount].Tag = variablesForm.output;
						gridVars.CurrentCell = gridVars[0, rowCount];
						((MainForm)base.Owner.Owner).WriteVarToGrid(rowCount);
						this.CmbVar.Items.Add(variablesForm.TxtName.Text);
						this.CmbVar.Text = variablesForm.TxtName.Text;
						result = false;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			else if (this.KB.Domains.GetIdDomainValueByName(this.KB.Vars.GetVarById(idVarByName).domain, this.CmbValue.Text) == -1)
			{
				if (MessageBox.Show("Указанного значения нет в домене, используемом данной переменной. Вы хотите его добавить?", "", 4) == 6)
				{
					int domain = this.KB.Vars.GetVarById(idVarByName).domain;
					int idDomainValue = this.KB.Domains.AddDomainValue(domain, this.KB.Domains.GetDomainValues(domain).Count);
					this.KB.Domains.ChangeDomainValue(domain, idDomainValue, this.CmbValue.Text);
					int num = Enumerable.ToList<Domain>(this.KB.Domains.Domains.Values).IndexOf(this.KB.Domains.Domains[domain]);
					((MainForm)base.Owner.Owner).TreeCurrent.Nodes["root"].Nodes["domains"].Nodes.RemoveAt(num);
					((MainForm)base.Owner.Owner).AddToTree(1, domain, num);
					((MainForm)base.Owner.Owner).RefreshDomainRows();
					this.CmbValue.Items.Add(this.CmbValue.Text);
					result = false;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		private void BtnOkContinue_Click(object sender, EventArgs e)
		{
			if (this.TestFact())
			{
				if (this.mode == 0)
				{
					this.KB.Rules.AddIfFact(this.IdRule, this.index);
					this.KB.Rules.ChangeIfFact(this.IdRule, this.index, this.CmbVar.Text, this.CmbValue.Text);
				}
				else
				{
					this.KB.Rules.AddThenFact(this.IdRule, this.index);
					this.KB.Rules.ChangeThenFact(this.IdRule, this.index, this.CmbVar.Text, this.CmbValue.Text);
				}
				this.output = string.Concat(new string[]
				{
					"'",
					this.CmbVar.Text,
					"'='",
					this.CmbValue.Text,
					"'"
				});
				base.DialogResult = 4;
				base.Close();
			}
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			if (this.TestFact())
			{
				if (this.mode < 2)
				{
					if (this.mode == 0)
					{
						this.KB.Rules.AddIfFact(this.IdRule, this.index);
						this.KB.Rules.ChangeIfFact(this.IdRule, this.index, this.CmbVar.Text, this.CmbValue.Text);
					}
					else
					{
						this.KB.Rules.AddThenFact(this.IdRule, this.index);
						this.KB.Rules.ChangeThenFact(this.IdRule, this.index, this.CmbVar.Text, this.CmbValue.Text);
					}
				}
				else if (this.mode == 2)
				{
					this.KB.Rules.ChangeIfFact(this.IdRule, this.index, this.CmbVar.Text, this.CmbValue.Text);
				}
				else
				{
					this.KB.Rules.ChangeThenFact(this.IdRule, this.index, this.CmbVar.Text, this.CmbValue.Text);
				}
				this.output = string.Concat(new string[]
				{
					"'",
					this.CmbVar.Text,
					"'='",
					this.CmbValue.Text,
					"'"
				});
				base.DialogResult = 1;
				base.Close();
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.BtnOk = new Button();
			this.BtnOkContinue = new Button();
			this.BtnCancel = new Button();
			this.CmbVar = new ComboBox();
			this.CmbValue = new ComboBox();
			this.groupBox1 = new GroupBox();
			this.groupBox2 = new GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.BtnOk.Location = new Point(240, 91);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new Size(139, 23);
			this.BtnOk.TabIndex = 3;
			this.BtnOk.Text = "Apply and exit";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new EventHandler(this.BtnOk_Click);
			this.BtnOkContinue.Enabled = false;
			this.BtnOkContinue.Location = new Point(74, 91);
			this.BtnOkContinue.Name = "BtnOkContinue";
			this.BtnOkContinue.Size = new Size(152, 23);
			this.BtnOkContinue.TabIndex = 2;
			this.BtnOkContinue.Text = "Apply and continue";
			this.BtnOkContinue.UseVisualStyleBackColor = true;
			this.BtnOkContinue.Visible = false;
			this.BtnOkContinue.Click += new EventHandler(this.BtnOkContinue_Click);
			this.BtnCancel.DialogResult = 2;
			this.BtnCancel.Location = new Point(389, 91);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new Size(75, 23);
			this.BtnCancel.TabIndex = 4;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new EventHandler(this.BtnCancel_Click);
			this.CmbVar.FormattingEnabled = true;
			this.CmbVar.Location = new Point(16, 22);
			this.CmbVar.Name = "CmbVar";
			this.CmbVar.Size = new Size(182, 21);
			this.CmbVar.TabIndex = 0;
			this.CmbVar.TextChanged += new EventHandler(this.CmbVar_TextChanged);
			this.CmbValue.FormattingEnabled = true;
			this.CmbValue.Location = new Point(16, 22);
			this.CmbValue.Name = "CmbValue";
			this.CmbValue.Size = new Size(182, 21);
			this.CmbValue.TabIndex = 1;
			this.groupBox1.Controls.Add(this.CmbVar);
			this.groupBox1.Location = new Point(12, 11);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(214, 58);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Variable";
			this.groupBox2.Controls.Add(this.CmbValue);
			this.groupBox2.Location = new Point(241, 11);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(214, 58);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Variable's value";
			base.AcceptButton = this.BtnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.CancelButton = this.BtnCancel;
			base.ClientSize = new Size(471, 125);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.BtnOk);
			base.Controls.Add(this.BtnOkContinue);
			base.Controls.Add(this.BtnCancel);
			base.FormBorderStyle = 2;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChangeFact";
			base.StartPosition = 4;
			this.Text = "Fact editing";
			base.Load += new EventHandler(this.ChangeFact_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
