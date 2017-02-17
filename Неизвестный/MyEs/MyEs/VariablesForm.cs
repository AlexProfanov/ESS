using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyEs
{
	public class VariablesForm : Form
	{
		public int input;

		public int output;

		public int index;

		public KnowledgeBase KB;

		private int IdVar;

		public bool Context;

		public MainForm MForm;

		private IContainer components = null;

		private Label label1;

		public TextBox TxtName;

		private Label label2;

		private Label label3;

		private Label label4;

		private TextBox TxtQuestion;

		private ComboBox CmbType;

		private ComboBox CmbDomain;

		private Button BtnCancel;

		private Button BtnOkContinue;

		private Button BtnOk;

		private Label label5;

		private TextBox TxtReason;

		public VariablesForm()
		{
			this.InitializeComponent();
			this.input = -1;
		}

		private void VariablesForm_Load(object sender, EventArgs e)
		{
			List<string> domainsNames = this.KB.Domains.GetDomainsNames();
			using (List<string>.Enumerator enumerator = domainsNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					this.CmbDomain.Items.Add(current);
				}
			}
			if (this.input == -1)
			{
				if (!this.Context)
				{
					this.BtnOkContinue.Enabled = true;
					this.BtnOkContinue.Visible = true;
				}
				this.Text = "Creating new variable";
				this.CmbType.SelectedIndex = 2;
				if (this.CmbDomain.Items.Count > 0)
				{
					this.CmbDomain.SelectedIndex = 0;
				}
				this.IdVar = this.KB.Vars.AddVar(this.index);
			}
			else
			{
				this.IdVar = this.KB.Vars.CloneVar(this.input);
				Variable varById = this.KB.Vars.GetVarById(this.IdVar);
				this.TxtName.Text = varById.Name;
				this.Text = this.Text + " " + varById.Name;
				this.CmbType.Text = varById.type;
				this.CmbDomain.Text = this.KB.Domains.GetNameById(varById.domain);
				this.TxtQuestion.Text = varById.Question;
				this.TxtReason.Text = varById.Reason;
			}
		}

		private void VariablesForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == 2)
			{
				this.KB.Vars.RemoveVar(this.IdVar);
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private bool TestVar()
		{
			bool result;
			if (this.TxtName.Text.Trim() == "")
			{
				MessageBox.Show("У переменной должно быть имя");
				this.TxtName.Select();
				result = false;
			}
			else
			{
				int idVarByName = this.KB.Vars.GetIdVarByName(this.TxtName.Text);
				if (this.input == -1 && idVarByName > -1)
				{
					MessageBox.Show("Переменная с таким именем уже есть");
					this.TxtName.Select();
					result = false;
				}
				else if (this.input > -1 && idVarByName > -1 && idVarByName != this.IdVar && idVarByName != this.input)
				{
					MessageBox.Show("Переменная с таким именем уже есть");
					this.TxtName.Select();
					result = false;
				}
				else if (this.input > -1 && this.KB.Domains.GetIdByName(this.CmbDomain.Text) != this.KB.Vars.GetVarById(this.IdVar).domain)
				{
					MessageBox.Show("Нельзя менять домен у переменной, используемой в правилах");
					this.CmbDomain.Select();
					result = false;
				}
				else if (this.KB.Domains.GetIdByName(this.CmbDomain.Text) == -1)
				{
					if (MessageBox.Show("Указанного домена нет в базе. Вы хотите его добавить?", "", 4) == 6)
					{
						DomainsForm domainsForm = new DomainsForm();
						DataGridView gridDomains = this.MForm.GridDomains;
						int rowCount = gridDomains.RowCount;
						domainsForm.index = rowCount;
						domainsForm.KB = this.KB;
						domainsForm.TxtName.Text = this.CmbDomain.Text;
						domainsForm.Context = true;
						DialogResult dialogResult = domainsForm.ShowDialog(this);
						if (dialogResult != 2)
						{
							this.MForm.AddToTree(1, domainsForm.output, rowCount);
							gridDomains.Rows.Insert(rowCount, 1);
							gridDomains.Rows[rowCount].Tag = domainsForm.output;
							gridDomains.CurrentCell = gridDomains[0, rowCount];
							this.MForm.WriteDomainToGrid(rowCount);
							this.CmbDomain.Items.Add(domainsForm.TxtName.Text);
							this.CmbDomain.Text = domainsForm.TxtName.Text;
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
				else
				{
					result = true;
				}
			}
			return result;
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			if (this.TestVar())
			{
				if (this.CmbType.SelectedIndex == 0)
				{
					this.TxtQuestion.Text = "";
				}
				if (this.input == -1)
				{
					this.KB.Vars.ChangeVar(this.IdVar, this.TxtName.Text, this.CmbType.Text, this.CmbDomain.Text, this.TxtQuestion.Text, this.TxtReason.Text);
					this.output = this.IdVar;
				}
				else
				{
					this.KB.Vars.RemoveVar(this.input);
					this.KB.Vars.ChangeVar(this.IdVar, this.TxtName.Text, this.CmbType.Text, this.CmbDomain.Text, this.TxtQuestion.Text, this.TxtReason.Text);
					this.KB.Vars.PasteVar(this.index, this.input, (Variable)this.KB.Vars.Vars[this.IdVar].Clone());
					this.KB.Vars.RemoveVar(this.IdVar);
				}
				base.DialogResult = 1;
				base.Close();
			}
		}

		private void BtnOkContinue_Click(object sender, EventArgs e)
		{
			if (this.TestVar())
			{
				if (this.CmbType.SelectedIndex == 0)
				{
					this.TxtQuestion.Text = "";
				}
				this.KB.Vars.ChangeVar(this.IdVar, this.TxtName.Text, this.CmbType.Text, this.CmbDomain.Text, this.TxtQuestion.Text, this.TxtReason.Text);
				this.output = this.IdVar;
				base.DialogResult = 4;
				base.Close();
			}
		}

		private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.CmbType.SelectedIndex == 0)
			{
				this.TxtQuestion.Enabled = false;
			}
			else
			{
				this.TxtQuestion.Enabled = true;
			}
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
			this.label1 = new Label();
			this.TxtName = new TextBox();
			this.label2 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.TxtQuestion = new TextBox();
			this.CmbType = new ComboBox();
			this.CmbDomain = new ComboBox();
			this.BtnCancel = new Button();
			this.BtnOkContinue = new Button();
			this.BtnOk = new Button();
			this.label5 = new Label();
			this.TxtReason = new TextBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new Size(84, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Variable's name:";
			this.TxtName.Location = new Point(112, 10);
			this.TxtName.Name = "TxtName";
			this.TxtName.Size = new Size(319, 20);
			this.TxtName.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 54);
			this.label2.Name = "label2";
			this.label2.Size = new Size(34, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Type:";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(12, 95);
			this.label3.Name = "label3";
			this.label3.Size = new Size(46, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Domain:";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(12, 132);
			this.label4.Name = "label4";
			this.label4.Size = new Size(52, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Question:";
			this.TxtQuestion.Location = new Point(15, 160);
			this.TxtQuestion.Multiline = true;
			this.TxtQuestion.Name = "TxtQuestion";
			this.TxtQuestion.Size = new Size(416, 86);
			this.TxtQuestion.TabIndex = 4;
			this.CmbType.DropDownStyle = 2;
			this.CmbType.FormattingEnabled = true;
			this.CmbType.Items.AddRange(new object[]
			{
				"Inferable",
				"Infer-askable",
				"Askable"
			});
			this.CmbType.Location = new Point(112, 49);
			this.CmbType.Name = "CmbType";
			this.CmbType.Size = new Size(319, 21);
			this.CmbType.TabIndex = 2;
			this.CmbType.SelectedIndexChanged += new EventHandler(this.CmbType_SelectedIndexChanged);
			this.CmbDomain.FormattingEnabled = true;
			this.CmbDomain.Location = new Point(112, 90);
			this.CmbDomain.Name = "CmbDomain";
			this.CmbDomain.Size = new Size(319, 21);
			this.CmbDomain.TabIndex = 3;
			this.BtnCancel.DialogResult = 2;
			this.BtnCancel.Location = new Point(356, 314);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new Size(75, 23);
			this.BtnCancel.TabIndex = 8;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new EventHandler(this.BtnCancel_Click);
			this.BtnOkContinue.Enabled = false;
			this.BtnOkContinue.Location = new Point(41, 314);
			this.BtnOkContinue.Name = "BtnOkContinue";
			this.BtnOkContinue.Size = new Size(152, 23);
			this.BtnOkContinue.TabIndex = 6;
			this.BtnOkContinue.Text = "Apply and continue";
			this.BtnOkContinue.UseVisualStyleBackColor = true;
			this.BtnOkContinue.Visible = false;
			this.BtnOkContinue.Click += new EventHandler(this.BtnOkContinue_Click);
			this.BtnOk.Location = new Point(202, 314);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new Size(139, 23);
			this.BtnOk.TabIndex = 7;
			this.BtnOk.Text = "Apply and exit";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new EventHandler(this.BtnOk_Click);
			this.label5.AutoSize = true;
			this.label5.Location = new Point(12, 261);
			this.label5.Name = "label5";
			this.label5.Size = new Size(61, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Reasoning:";
			this.TxtReason.Location = new Point(15, 286);
			this.TxtReason.Name = "TxtReason";
			this.TxtReason.Size = new Size(416, 20);
			this.TxtReason.TabIndex = 5;
			base.AcceptButton = this.BtnOk;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.CancelButton = this.BtnCancel;
			base.ClientSize = new Size(443, 345);
			base.Controls.Add(this.TxtReason);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.BtnOkContinue);
			base.Controls.Add(this.BtnOk);
			base.Controls.Add(this.BtnCancel);
			base.Controls.Add(this.CmbDomain);
			base.Controls.Add(this.CmbType);
			base.Controls.Add(this.TxtQuestion);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.TxtName);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = 2;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "VariablesForm";
			base.StartPosition = 4;
			this.Text = "Variable editing";
			base.FormClosing += new FormClosingEventHandler(this.VariablesForm_FormClosing);
			base.Load += new EventHandler(this.VariablesForm_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
