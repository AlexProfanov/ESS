using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExpertSystem
{
	public class rulsetForm : Form
	{
		public class MyRulePanel : Panel
		{
			private const int panhg = 35;

			private ComboBox cbVar;

			private ComboBox cbDom;

			private Button bAddV;

			private Button bAddD;

			private Button bDel;

			private rulsetForm parent;

			public ComboBox CbVar
			{
				get
				{
					return this.cbVar;
				}
				set
				{
					this.cbVar = value;
				}
			}

			public MyRulePanel(rulsetForm rf, Expert.Variable v = null, int posAtDom = 0)
			{
				this.parent = rf;
				this.AutoScroll = true;
				int num = 5;
				this.cbVar = new ComboBox();
				using (List<Expert.Variable>.Enumerator enumerator = Expert.Cor.Variables.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Expert.Variable current = enumerator.Current;
						this.cbVar.Items.Add(current.Name);
					}
				}
				this.cbVar.Location = new Point(num, 0);
				int num2 = 130;
				num += num2;
				this.cbVar.Width = num2;
				this.cbVar.TextChanged += new EventHandler(this.cbVar_TextChanged);
				this.bAddV = new Button();
				this.bAddV.Location = new Point(num, 0);
				num2 = 15;
				num += num2;
				this.bAddV.Width = num2;
				this.bAddV.Text = "+";
				this.cbDom = new ComboBox();
				this.cbDom.Location = new Point(num, 0);
				num2 = 130;
				num += num2;
				this.cbDom.Width = num2;
				this.cbVar.Text = (this.cbVar.Items.Count > 0) ? this.cbVar.Items[0].ToString() : "";
				this.bAddD = new Button();
				this.bAddD.Location = new Point(num, 0);
				num2 = 15;
				num += num2;
				this.bAddD.Width = num2;
				this.bAddD.Text = "+";
				this.bDel = new Button();
				this.bDel.Location = new Point(num, 0);
				num2 = 65;
				num += num2;
				this.bDel.Width = num2;
				this.bDel.Text = "Удалить";
				base.Controls.Add(this.cbVar);
				base.Controls.Add(this.bAddV);
				base.Controls.Add(this.cbDom);
				base.Controls.Add(this.bAddD);
				base.Controls.Add(this.bDel);
				base.Height = 35;
				base.Width = num + 5;
				this.Anchor = (System.Windows.Forms.AnchorStyles)1;
				this.BackColor = Color.White;
				Expert.ExpertEvents.ExpertChangedEvent += new Expert.ExpertEvents.vv(this.onChng);
				if (v != null)
				{
					this.cbVar.Text = v.Name;
					this.cbDom.Text = v.Dom.Values[posAtDom];
				}
				this.bDel.Click += new EventHandler(this.bDel_Click);
				this.bAddD.Visible = false;
				this.bAddV.Visible = false;
			}

			private void bAddD_Click(object sender, EventArgs e)
			{
				rulsetForm.selDom(this.cbVar, this.cbDom);
			}

			private void bAddV_Click(object sender, EventArgs e)
			{
				rulsetForm.selVar(this.cbVar);
			}

			private void cbVar_TextChanged(object sender, EventArgs e)
			{
				rulsetForm.upgradeCombobox(this.cbVar, this.cbDom);
			}

			private void bDel_Click(object sender, EventArgs e)
			{
				this.parent.deleteFact(this);
			}

			protected override void Dispose(bool disposing)
			{
				Expert.ExpertEvents.ExpertChangedEvent -= new Expert.ExpertEvents.vv(this.onChng);
				base.Dispose(disposing);
			}

			public void onChng()
			{
				this.cbVar.Items.Clear();
				using (List<Expert.Variable>.Enumerator enumerator = Expert.Cor.Variables.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Expert.Variable current = enumerator.Current;
						this.cbVar.Items.Add(current.Name);
					}
				}
			}

			public Expert.Fact ToFact()
			{
				Expert.Fact result;
				try
				{
					Expert.Variable variable = Expert.Cor.varByName(this.cbVar.Text);
					if (variable == null)
					{
						this.BackColor = Color.Red;
						result = null;
					}
					else
					{
						Expert.Domain dom = variable.Dom;
						int num = dom.Values.IndexOf(this.cbDom.Text);
						if (num == -1)
						{
							this.BackColor = Color.Red;
							result = null;
						}
						else
						{
							Expert.Fact fact = new Expert.Fact();
							fact.Var = variable;
							fact.PosAtDom = num;
							this.BackColor = Color.White;
							result = fact;
						}
					}
				}
				catch (Exception)
				{
					this.BackColor = Color.Red;
					result = null;
				}
				return result;
			}
		}

		public const int panhg = 40;

		private static rulsetForm currentF = null;

		private Dictionary<int, Expert.Fact> curFacts = new Dictionary<int, Expert.Fact>();

		public List<rulsetForm.MyRulePanel> pannels = new List<rulsetForm.MyRulePanel>();

		private Rectangle dragBoxFromMouseDown;

		private int rowIndexFromMouseDown;

		private int rowIndexOfItemUnderMouseToDrop;

		private int lastsel = 0;

		private IContainer components = null;

		private SplitContainer splitContainer1;

		private SplitContainer splitContainer3;

		private Label label1;

		private Panel pFacts;

		private TextBox tbName;

		private Button bAddf;

		private Button bCancel;

		private Button bApply;

		private Label label2;

		private TextBox tbReason;

		private Label label5;

		private Label label4;

		private ComboBox cbTDom;

		private Label label3;

		private ComboBox cbTVar;

		private DataGridView dgvRulSet;

		private DataGridViewTextBoxColumn id;

		private DataGridViewTextBoxColumn rule;

		private DataGridViewTextBoxColumn thn;

		private DataGridViewTextBoxColumn Reason;

		private Button bDel;

		private Button bAdd;

		public static rulsetForm get(bool getnew = false)
		{
			rulsetForm result;
			if (rulsetForm.currentF != null)
			{
				if (!getnew)
				{
					rulsetForm.currentF.Select();
					result = rulsetForm.currentF;
					return result;
				}
				rulsetForm.currentF.Close();
			}
			rulsetForm.currentF = new rulsetForm();
			result = rulsetForm.currentF;
			return result;
		}

		private rulsetForm()
		{
			this.InitializeComponent();
		}

		private void rulsetForm_Load(object sender, EventArgs e)
		{
			Expert.ExpertEvents.ExpertChangedEvent += new Expert.ExpertEvents.vv(this.onExpertChangedRuleSet);
			this.onExpertChangedRuleSet();
			IEnumerator enumerator = this.dgvRulSet.Columns.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)enumerator.Current;
					dataGridViewColumn.SortMode = 0;
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			this.dgvRulSet.MouseMove += new MouseEventHandler(this.dgvRulSet_MouseMove);
			this.dgvRulSet.MouseDown += new MouseEventHandler(this.dgvRulSet_MouseDown);
			this.dgvRulSet.DragOver += new DragEventHandler(this.dgvRulSet_DragOver);
			this.dgvRulSet.DragDrop += new DragEventHandler(this.dgvRulSet_DragDrop);
		}

		private void dgvRulSet_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & (MouseButtons)1048576) == (MouseButtons)1048576)
			{
				if (this.dragBoxFromMouseDown != Rectangle.Empty && !this.dragBoxFromMouseDown.Contains(e.X, e.Y))
				{
					DragDropEffects dragDropEffects = this.dgvRulSet.DoDragDrop(this.dgvRulSet.Rows[this.rowIndexFromMouseDown], (System.Windows.Forms.DragDropEffects)2);
				}
			}
		}

		private void dgvRulSet_MouseDown(object sender, MouseEventArgs e)
		{
			this.rowIndexFromMouseDown = this.dgvRulSet.HitTest(e.X, e.Y).RowIndex;
			if (this.rowIndexFromMouseDown != -1)
			{
				Size dragSize = SystemInformation.DragSize;
				this.dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
			}
			else
			{
				this.dragBoxFromMouseDown = Rectangle.Empty;
			}
		}

		private void dgvRulSet_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = (System.Windows.Forms.DragDropEffects)2;
		}

		private void dgvRulSet_DragDrop(object sender, DragEventArgs e)
		{
			Point point = this.dgvRulSet.PointToClient(new Point(e.X, e.Y));
			this.rowIndexOfItemUnderMouseToDrop = this.dgvRulSet.HitTest(point.X, point.Y).RowIndex;
			if (e.Effect == (System.Windows.Forms.DragDropEffects)2)
			{
				DataGridViewRow dataGridViewRow = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
				Expert.Cor.setNewDomainPos(this.rowIndexFromMouseDown, this.rowIndexOfItemUnderMouseToDrop);
			}
		}

		private void rulsetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			rulsetForm.currentF = null;
			Expert.ExpertEvents.ExpertChangedEvent -= new Expert.ExpertEvents.vv(this.onExpertChangedRuleSet);
		}

		private void onExpertChangedRuleSet()
		{
			this.cbTVar.Items.Clear();
			using (List<Expert.Variable>.Enumerator enumerator = Expert.Cor.Variables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Expert.Variable current = enumerator.Current;
					if (current.vt != Expert.VarType.vtZapr)
					{
						this.cbTVar.Items.Add(current.Name);
					}
				}
			}
			this.cbTVar_TextChanged(null, null);
			this.RebuildDgvRules();
		}

		private void bAddf_Click(object sender, EventArgs e)
		{
			rulsetForm.MyRulePanel myRulePanel = new rulsetForm.MyRulePanel(this, null, 0);
			myRulePanel.Location = new Point((this.pFacts.Width - myRulePanel.Width) / 2, 40 * this.pannels.Count + 25);
			this.pannels.Add(myRulePanel);
			this.pFacts.Controls.Add(myRulePanel);
		}

		private void rebuildLocationsOfPannel()
		{
			int num = 0;
			using (List<rulsetForm.MyRulePanel>.Enumerator enumerator = this.pannels.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					rulsetForm.MyRulePanel current = enumerator.Current;
					current.Location = new Point((this.pFacts.Width - current.Width) / 2, 40 * num + 25);
					num++;
				}
			}
		}

		private void deleteFact(rulsetForm.MyRulePanel rp)
		{
			if (rp != null)
			{
				int num = this.pannels.IndexOf(rp);
				if (num != -1)
				{
					rp.Dispose();
					this.pannels.RemoveAt(num);
					this.rebuildLocationsOfPannel();
				}
			}
		}

		private static void upgradeCombobox(ComboBox cbVar, ComboBox cbDom)
		{
			Expert.Variable variable = Expert.Cor.varByName(cbVar.Text);
			if (variable != null)
			{
				cbDom.Items.Clear();
				using (List<string>.Enumerator enumerator = variable.Dom.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						cbDom.Items.Add(current);
					}
				}
			}
		}

		private static void selVar(ComboBox cbVar)
		{
			varForm.SelectVar(null, cbVar.Text);
		}

		private static void selDom(ComboBox cbVar, ComboBox cbDom)
		{
			domainForm.SelectDomain(null, (Expert.Cor.varByName(cbVar.Text) == null) ? cbDom.Text : Expert.Cor.varByName(cbVar.Text).Dom.Name);
		}

		private void RebuildDgvRules()
		{
			int num = this.lastsel;
			this.dgvRulSet.Rows.Clear();
			using (Dictionary<int, Expert.Rule>.Enumerator enumerator = Expert.Cor.Rules.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Expert.Rule> current = enumerator.Current;
					this.dgvRulSet.Rows.Add(new object[]
					{
						current.Value.Name,
						current.Value.ToString(),
						(current.Value.RuleResult != null) ? current.Value.RuleResult.ToString() : "",
						current.Value.Reason
					});
					if (current.Key != num)
					{
						this.dgvRulSet.Rows[current.Key].Selected = false;
					}
				}
			}
			try
			{
				this.dgvRulSet.Rows[num].Selected = true;
			}
			catch (Exception)
			{
			}
			this.lastsel = num;
		}

		private int curRulePos()
		{
			int result;
			try
			{
				int rowIndex = this.dgvRulSet.SelectedRows[0].Cells[0].RowIndex;
				result = rowIndex;
			}
			catch (Exception)
			{
				result = 0;
			}
			return result;
		}

		private void clearPannels()
		{
			using (List<rulsetForm.MyRulePanel>.Enumerator enumerator = this.pannels.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					rulsetForm.MyRulePanel current = enumerator.Current;
					current.Dispose();
				}
			}
			this.pannels.Clear();
		}

		private void initRule(int pos)
		{
			Expert.Rule r = Expert.Cor.LoadRule(pos);
			this.initRule(r);
		}

		private void initRule(Expert.Rule r)
		{
			this.clearPannels();
			if (r == null)
			{
				this.cbTVar.Text = "";
				this.cbTDom.Text = "";
				this.tbName.Text = "";
				this.tbReason.Text = "";
			}
			else
			{
				using (Dictionary<int, Expert.Fact>.Enumerator enumerator = r.Facts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Expert.Fact> current = enumerator.Current;
						rulsetForm.MyRulePanel myRulePanel = new rulsetForm.MyRulePanel(this, current.Value.Var, current.Value.PosAtDom);
						this.pannels.Add(myRulePanel);
						this.pFacts.Controls.Add(myRulePanel);
					}
				}
				this.rebuildLocationsOfPannel();
				this.cbTVar.BackColor = Color.White;
				this.cbTDom.BackColor = Color.White;
				if (r.RuleResult != null)
				{
					this.cbTVar.Text = r.RuleResult.Var.Name;
					this.cbTDom.Text = r.RuleResult.Dom.Values[r.RuleResult.PosAtDom];
				}
				else
				{
					this.cbTVar.Text = "";
					this.cbTDom.Text = "";
				}
				this.tbName.Text = r.Name;
				this.tbReason.Text = r.Reason;
			}
		}

		private void dgvRulSet_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				this.lastsel = this.dgvRulSet.SelectedRows[0].Cells[0].RowIndex;
				this.initRule(this.lastsel);
			}
			catch (Exception)
			{
			}
		}

		private void cbTVar_TextChanged(object sender, EventArgs e)
		{
			rulsetForm.upgradeCombobox(this.cbTVar, this.cbTDom);
		}

		private void bEditVar_Click(object sender, EventArgs e)
		{
			rulsetForm.selVar(this.cbTVar);
		}

		private void bEditDomain_Click(object sender, EventArgs e)
		{
			rulsetForm.selDom(this.cbTVar, this.cbTDom);
		}

		private void bApply_Click(object sender, EventArgs e)
		{
			Expert.Rule rule = new Expert.Rule();
			rule.Name = this.tbName.Text;
			bool flag = true;
			using (List<rulsetForm.MyRulePanel>.Enumerator enumerator = this.pannels.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					rulsetForm.MyRulePanel current = enumerator.Current;
					Expert.Fact fact = current.ToFact();
					if (fact == null)
					{
						flag = false;
					}
					else
					{
						rule.addFact(fact, -1);
					}
				}
			}
			Expert.Fact fact2 = new Expert.Fact();
			Expert.Variable variable = Expert.Cor.varByName(this.cbTVar.Text);
			if (variable == null)
			{
				this.cbTVar.BackColor = Color.Red;
			}
			else
			{
				this.cbTVar.BackColor = Color.White;
				int num = variable.Dom.Values.IndexOf(this.cbTDom.Text);
				if (num == -1)
				{
					this.cbTDom.BackColor = Color.Red;
				}
				else
				{
					fact2.Var = variable;
					fact2.PosAtDom = num;
					rule.RuleResult = fact2;
					if (this.tbReason.Text != "")
					{
						rule.Reason = this.tbReason.Text;
					}
					if (flag)
					{
						Expert.Cor.AcceptRule(rule, this.curRulePos());
					}
				}
			}
		}

		private void bAdd_Click(object sender, EventArgs e)
		{
			this.clearPannels();
			if (this.lastsel == -1)
			{
				this.lastsel = 0;
			}
			Expert.Rule rule = new Expert.Rule();
			rule.Name = Expert.Cor.GetNextRuleName();
			Expert.Cor.AddRule(rule, this.lastsel);
			this.tbName.Select();
			this.tbName.Text = rule.Name;
			this.tbName.SelectionStart = 0;
			this.tbName.SelectionLength = rule.Name.Length;
		}

		private void bDel_Click(object sender, EventArgs e)
		{
			this.lastsel = this.curRulePos();
			if (this.lastsel > 0)
			{
				this.lastsel--;
			}
			Expert.Cor.removeRule(this.curRulePos());
		}

		private void bCancel_Click(object sender, EventArgs e)
		{
			this.initRule(this.lastsel);
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
			this.splitContainer1 = new SplitContainer();
			this.dgvRulSet = new DataGridView();
			this.id = new DataGridViewTextBoxColumn();
			this.rule = new DataGridViewTextBoxColumn();
			this.thn = new DataGridViewTextBoxColumn();
			this.Reason = new DataGridViewTextBoxColumn();
			this.bDel = new Button();
			this.bAdd = new Button();
			this.bCancel = new Button();
			this.bApply = new Button();
			this.splitContainer3 = new SplitContainer();
			this.label2 = new Label();
			this.label1 = new Label();
			this.pFacts = new Panel();
			this.bAddf = new Button();
			this.tbName = new TextBox();
			this.tbReason = new TextBox();
			this.label5 = new Label();
			this.label4 = new Label();
			this.cbTDom = new ComboBox();
			this.label3 = new Label();
			this.cbTVar = new ComboBox();
			this.splitContainer1.BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((ISupportInitialize)(this.dgvRulSet)).BeginInit();
			this.splitContainer3.BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.pFacts.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.Dock = (System.Windows.Forms.DockStyle)5;
			this.splitContainer1.FixedPanel = (System.Windows.Forms.FixedPanel)2;
			this.splitContainer1.Location = new Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.dgvRulSet);
			this.splitContainer1.Panel1.Controls.Add(this.bDel);
			this.splitContainer1.Panel1.Controls.Add(this.bAdd);
			this.splitContainer1.Panel2.Controls.Add(this.bCancel);
			this.splitContainer1.Panel2.Controls.Add(this.bApply);
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer1.Panel2MinSize = 355;
			this.splitContainer1.Size = new Size(898, 545);
			this.splitContainer1.SplitterDistance = 496;
			this.splitContainer1.TabIndex = 2;
			this.dgvRulSet.AllowDrop = true;
			this.dgvRulSet.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.dgvRulSet.BackgroundColor = Color.White;
			this.dgvRulSet.ColumnHeadersHeightSizeMode = (System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode)2;
			this.dgvRulSet.Columns.AddRange(new DataGridViewColumn[]
			{
				this.id,
				this.rule,
				this.thn,
				this.Reason
			});
			this.dgvRulSet.Location = new Point(0, 0);
			this.dgvRulSet.MultiSelect = false;
			this.dgvRulSet.Name = "dgvRulSet";
			this.dgvRulSet.ReadOnly = true;
			this.dgvRulSet.SelectionMode = (System.Windows.Forms.DataGridViewSelectionMode)1;
			this.dgvRulSet.Size = new Size(493, 467);
			this.dgvRulSet.TabIndex = 7;
			this.dgvRulSet.SelectionChanged += new EventHandler(this.dgvRulSet_SelectionChanged);
			this.id.HeaderText = "Имя";
			this.id.Name = "id";
			this.id.ReadOnly = true;
			this.id.Width = 50;
			this.rule.HeaderText = "IF";
			this.rule.Name = "rule";
			this.rule.ReadOnly = true;
			this.rule.Width = 200;
			this.thn.HeaderText = "THEN";
			this.thn.Name = "thn";
			this.thn.ReadOnly = true;
			this.Reason.HeaderText = "Объяснение";
			this.Reason.Name = "Reason";
			this.Reason.ReadOnly = true;
			this.bDel.Anchor = (System.Windows.Forms.AnchorStyles)14;
			this.bDel.Location = new Point(3, 505);
			this.bDel.Name = "bDel";
			this.bDel.Size = new Size(490, 25);
			this.bDel.TabIndex = 6;
			this.bDel.Text = "Удалить";
			this.bDel.UseVisualStyleBackColor = true;
			this.bDel.Click += new EventHandler(this.bDel_Click);
			this.bAdd.Anchor = (System.Windows.Forms.AnchorStyles)14;
			this.bAdd.Location = new Point(3, 474);
			this.bAdd.Name = "bAdd";
			this.bAdd.Size = new Size(490, 25);
			this.bAdd.TabIndex = 5;
			this.bAdd.Text = "Добавить";
			this.bAdd.UseVisualStyleBackColor = true;
			this.bAdd.Click += new EventHandler(this.bAdd_Click);
			this.bCancel.Anchor = (System.Windows.Forms.AnchorStyles)14;
			this.bCancel.Location = new Point(2, 505);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new Size(395, 23);
			this.bCancel.TabIndex = 3;
			this.bCancel.Text = "Отменить";
			this.bCancel.UseVisualStyleBackColor = true;
			this.bCancel.Click += new EventHandler(this.bCancel_Click);
			this.bApply.Anchor = (System.Windows.Forms.AnchorStyles)14;
			this.bApply.Location = new Point(2, 473);
			this.bApply.Name = "bApply";
			this.bApply.Size = new Size(395, 23);
			this.bApply.TabIndex = 2;
			this.bApply.Text = "Применить";
			this.bApply.UseVisualStyleBackColor = true;
			this.bApply.Click += new EventHandler(this.bApply_Click);
			this.splitContainer3.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.splitContainer3.Location = new Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = 0;
			this.splitContainer3.Panel1.Controls.Add(this.label2);
			this.splitContainer3.Panel1.Controls.Add(this.label1);
			this.splitContainer3.Panel1.Controls.Add(this.pFacts);
			this.splitContainer3.Panel1.Controls.Add(this.tbName);
			this.splitContainer3.Panel1MinSize = 250;
			this.splitContainer3.Panel2.Controls.Add(this.tbReason);
			this.splitContainer3.Panel2.Controls.Add(this.label5);
			this.splitContainer3.Panel2.Controls.Add(this.label4);
			this.splitContainer3.Panel2.Controls.Add(this.cbTDom);
			this.splitContainer3.Panel2.Controls.Add(this.label3);
			this.splitContainer3.Panel2.Controls.Add(this.cbTVar);
			this.splitContainer3.Panel2MinSize = 110;
			this.splitContainer3.Size = new Size(398, 467);
			this.splitContainer3.SplitterDistance = 358;
			this.splitContainer3.TabIndex = 0;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(2, 29);
			this.label2.Name = "label2";
			this.label2.Size = new Size(16, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "IF";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new Size(74, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Имя правила";
			this.pFacts.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.pFacts.AutoScroll = true;
			this.pFacts.BackColor = Color.White;
			this.pFacts.Controls.Add(this.bAddf);
			this.pFacts.Location = new Point(0, 45);
			this.pFacts.Name = "pFacts";
			this.pFacts.Size = new Size(398, 313);
			this.pFacts.TabIndex = 1;
			this.bAddf.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.bAddf.Location = new Point(22, 0);
			this.bAddf.Name = "bAddf";
			this.bAddf.Size = new Size(349, 23);
			this.bAddf.TabIndex = 0;
			this.bAddf.Text = "Добавить факт";
			this.bAddf.UseVisualStyleBackColor = true;
			this.bAddf.Click += new EventHandler(this.bAddf_Click);
			this.tbName.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.tbName.Location = new Point(76, 3);
			this.tbName.Name = "tbName";
			this.tbName.Size = new Size(310, 20);
			this.tbName.TabIndex = 0;
			this.tbReason.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.tbReason.Location = new Point(6, 70);
			this.tbReason.Name = "tbReason";
			this.tbReason.Size = new Size(386, 20);
			this.tbReason.TabIndex = 10;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(3, 54);
			this.label5.Name = "label5";
			this.label5.Size = new Size(70, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Объяснение";
			this.label4.Anchor = (System.Windows.Forms.AnchorStyles)1;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(188, 22);
			this.label4.Name = "label4";
			this.label4.Size = new Size(13, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "=";
			this.cbTDom.Anchor = (System.Windows.Forms.AnchorStyles)1;
			this.cbTDom.FormattingEnabled = true;
			this.cbTDom.Location = new Point(202, 19);
			this.cbTDom.Name = "cbTDom";
			this.cbTDom.Size = new Size(157, 21);
			this.cbTDom.TabIndex = 5;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(2, 3);
			this.label3.Name = "label3";
			this.label3.Size = new Size(37, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "THEN";
			this.cbTVar.Anchor = (System.Windows.Forms.AnchorStyles)1;
			this.cbTVar.FormattingEnabled = true;
			this.cbTVar.Location = new Point(13, 19);
			this.cbTVar.Name = "cbTVar";
			this.cbTVar.Size = new Size(169, 21);
			this.cbTVar.TabIndex = 0;
			this.cbTVar.TextChanged += new EventHandler(this.cbTVar_TextChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = (System.Windows.Forms.AutoScaleMode)1;
			base.ClientSize = new Size(898, 545);
			base.Controls.Add(this.splitContainer1);
			base.Name = "rulsetForm";
			base.StartPosition = (System.Windows.Forms.FormStartPosition)1;
			this.Text = "Правила";
			base.FormClosed += new FormClosedEventHandler(this.rulsetForm_FormClosed);
			base.Load += new EventHandler(this.rulsetForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.EndInit();
			this.splitContainer1.ResumeLayout(false);
			((ISupportInitialize)(this.dgvRulSet)).EndInit();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.Panel2.PerformLayout();
			this.splitContainer3.EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.pFacts.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
