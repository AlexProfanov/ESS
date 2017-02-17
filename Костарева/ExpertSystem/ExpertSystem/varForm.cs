using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExpertSystem
{
	public class varForm : Form
	{
		private static varForm currentF = null;

		private bool isDirty = false;

		private int lastpos = -1;

		private bool wc = false;

		private string lstnme = "";

		private IContainer components = null;

		private SplitContainer splitContainer1;

		private DataGridView dgvVars;

		private SplitContainer splitContainer2;

		private Button bDel;

		private Button bAdd;

		private SplitContainer splitContainer3;

		private Panel panel1;

		private TextBox tbQuest;

		private RadioButton rbVZ;

		private RadioButton rbVivod;

		private RadioButton rbZap;

		private ComboBox cbDoms;

		private Label label2;

		private Label label1;

		private TextBox tbName;

		private Button bCancel;

		private Button bApply;

		private DataGridViewTextBoxColumn cName;

		private Label lbStatus;

		private DataGridView dataVal;

		private DataGridViewTextBoxColumn Val;

		public static varForm get(bool getnew = false)
		{
			varForm result;
			if (varForm.currentF != null)
			{
				if (!getnew)
				{
					varForm.currentF.Select();
					result = varForm.currentF;
					return result;
				}
				varForm.currentF.Close();
			}
			varForm.currentF = new varForm();
			result = varForm.currentF;
			return result;
		}

		public static void SelectVar(Expert.Variable v, string nm = "")
		{
			varForm varForm = varForm.get(false);
			varForm.Show();
			varForm.Select();
			if (v == null)
			{
				v = Expert.Cor.varByName(nm);
				if (v != null)
				{
					IEnumerator enumerator = ((IEnumerable)(varForm.dgvVars.Rows)).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
							if (dataGridViewRow.Cells[0].Value == null)
							{
								dataGridViewRow.Selected = false;
							}
							else
							{
								dataGridViewRow.Selected = dataGridViewRow.Cells[0].Value.ToString().ToUpper().Trim() == nm.Trim().ToUpper();
							}
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
				}
				else
				{
					IEnumerator enumerator = ((IEnumerable)(varForm.dgvVars.Rows)).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
							if (dataGridViewRow.Cells[0].Value == null)
							{
								dataGridViewRow.Selected = true;
							}
							else
							{
								dataGridViewRow.Selected = false;
							}
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
					varForm.tbName.Text = nm;
				}
			}
			else
			{
				varForm.initVar(v);
			}
		}

		private varForm()
		{
			this.InitializeComponent();
			base.FormClosed += new FormClosedEventHandler(this.varForm_FormClosed);
			this.rbVivod.CheckedChanged += new EventHandler(this.rb_CheckedChanged);
			this.rbVZ.CheckedChanged += new EventHandler(this.rb_CheckedChanged);
			this.rbZap.CheckedChanged += new EventHandler(this.rb_CheckedChanged);
		}

		private void varForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			varForm.currentF = null;
			Expert.ExpertEvents.ExpertChangedEvent -= new Expert.ExpertEvents.vv(this.onExpertChanged_Vars);
		}

		private void varForm_Load(object sender, EventArgs e)
		{
			Expert.ExpertEvents.ExpertChangedEvent += new Expert.ExpertEvents.vv(this.onExpertChanged_Vars);
			this.onExpertChanged_Vars();
		}

		private void onExpertChanged_Vars()
		{
			int num = this.lastpos;
			this.cbDoms.Items.Clear();
			bool flag = this.isDirty;
			using (List<Expert.Domain>.Enumerator enumerator = Expert.Cor.Domains.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Expert.Domain current = enumerator.Current;
					this.cbDoms.Items.Add(current.Name);
				}
			}
			this.dgvVars.Rows.Clear();
			using (List<Expert.Variable>.Enumerator enumerator2 = Expert.Cor.Variables.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Expert.Variable current2 = enumerator2.Current;
					this.dgvVars.Rows.Add(new object[]
					{
						current2.Name
					});
				}
			}
			this.lastpos = num;
			if (this.lastpos != -1)
			{
				IEnumerator enumerator3 = ((IEnumerable)(this.dgvVars.Rows)).GetEnumerator();
				try
				{
					while (enumerator3.MoveNext())
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator3.Current;
						try
						{
							dataGridViewRow.Selected = dataGridViewRow.Cells[0].RowIndex == this.lastpos;
						}
						catch (Exception)
						{
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator3 as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			this.isDirty = flag;
		}

		private void rb_CheckedChanged(object sender, EventArgs e)
		{
			this.tbQuest.Visible = !this.rbVivod.Checked;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			domainForm.SelectDomain(null, this.cbDoms.Text);
		}

		private string getVT(Expert.VarType vt)
		{
			string result;
			switch (vt)
			{
				case Expert.VarType.vtVivod:
					result = "Выводимая";
					break;
				case Expert.VarType.vtZapr:
					result = "Запрашиваемая";
					break;
				case Expert.VarType.vtVivZapr:
					result = "Выводимо-запрашиваемая";
					break;
				default:
					result = "UNKNOWN";
					break;
			}
			return result;
		}

		private Expert.VarType getCurVt()
		{
			Expert.VarType result;
			if (this.rbVivod.Checked)
			{
				result = Expert.VarType.vtVivod;
			}
			else if (this.rbVZ.Checked)
			{
				result = Expert.VarType.vtVivZapr;
			}
			else
			{
				result = Expert.VarType.vtZapr;
			}
			return result;
		}

		private void selectRbByType(Expert.VarType vt)
		{
			this.rbVivod.Checked = false;
			this.rbZap.Checked = false;
			this.rbVZ.Checked = false;
			switch (vt)
			{
				case Expert.VarType.vtVivod:
					this.rbVivod.Checked = true;
					break;
				case Expert.VarType.vtZapr:
					this.rbZap.Checked = true;
					break;
				case Expert.VarType.vtVivZapr:
					this.rbVZ.Checked = true;
					break;
			}
		}

		private void bAdd_Click(object sender, EventArgs e)
		{
			Expert cor = Expert.Cor;
			if (cor.Domains.Count == 0)
			{
				MessageBox.Show("Прежде чем работать с переменными, необходимо наличие хотя бы одного домена!");
			}
			else
			{
				Expert.Domain domain = cor.Domains[0];
				Expert.Variable variable = cor.addVar(cor.GetNextVarName(), Expert.VarType.vtZapr, domain, "", this.lastpos);
				if (variable != null)
				{
					this.isDirty = false;
					this.bClear_Click(null, null);
					this.tbName.Text = variable.Name;
					this.tbQuest.Text = "";
					this.cbDoms.Text = domain.Name;
					this.tbName.Select();
					this.tbName.SelectionStart = 0;
					this.tbName.SelectionLength = this.tbName.Text.Length;
					this.lbStatus.Text = variable.Name + " создана";
					this.wc = true;
					this.lstnme = variable.Name;
				}
				else
				{
					this.lbStatus.Text = "Невозможно добавить переменную";
				}
			}
		}

		private void bDel_Click(object sender, EventArgs e)
		{
			bool flag = false;
			IEnumerator enumerator = this.dgvVars.SelectedRows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					if (dataGridViewRow.Cells[0].Value != null)
					{
						Expert.Cor.deleteVar(dataGridViewRow.Cells[0].Value.ToString());
						flag = true;
					}
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
			if (flag)
			{
				Expert.ExpertEvents.ExpertChanged();
				this.isDirty = false;
			}
		}

		private void bClear_Click(object sender, EventArgs e)
		{
			if (!this.isDirty || MessageBox.Show("Вы не применили изменения, продолжить?", "", (System.Windows.Forms.MessageBoxButtons)4) == (DialogResult)6)
			{
				this.tbName.Text = "";
				this.cbDoms.Text = "";
				this.rbZap.Checked = true;
				this.isDirty = false;
			}
		}

		private void initVar(int pos)
		{
			try
			{
				object value = this.dgvVars.SelectedRows[0].Cells[0].Value;
				this.initVar((value == null) ? null : Expert.Cor.varByName(value.ToString()));
			}
			catch (Exception ex)
			{
				this.lbStatus.Text = ex.Message;
			}
		}

		private void initVar(Expert.Variable var)
		{
			if (var == null)
			{
				this.tbName.Text = "";
				this.cbDoms.Text = "";
				this.rbZap.Checked = true;
				this.tbQuest.Text = "";
				this.lbStatus.Text = "Переменная выгружена!";
			}
			else
			{
				this.selectRbByType(var.vt);
				if (var.vt != Expert.VarType.vtVivod)
				{
					this.tbQuest.Text = var.Question;
				}
				this.tbName.Text = var.Name;
				this.cbDoms.Text = var.Dom.Name;
				IEnumerator enumerator = ((IEnumerable)(this.dgvVars.Rows)).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
						try
						{
							dataGridViewRow.Selected = dataGridViewRow.Cells[0].Value != null && dataGridViewRow.Cells[0].Value.ToString() == var.Name;
						}
						catch (Exception)
						{
						}
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
				this.lbStatus.Text = "Переменная " + var.Name + " загружена!";
				this.isDirty = false;
			}
		}

		private void dgvVars_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				this.lastpos = this.dgvVars.SelectedRows[0].Cells[0].RowIndex;
				this.initVar((this.dgvVars.SelectedRows[0].Cells[0].Value == null) ? null : Expert.Cor.varByName(this.dgvVars.SelectedRows[0].Cells[0].Value.ToString()));
			}
			catch (Exception)
			{
			}
		}

		private void dgvVars_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				this.lastpos = this.dgvVars.SelectedRows[0].Cells[0].RowIndex;
				if (!this.isDirty)
				{
					this.wc = false;
					this.initVar((this.dgvVars.SelectedRows[0].Cells[0].Value == null) ? null : Expert.Cor.varByName(this.dgvVars.SelectedRows[0].Cells[0].Value.ToString()));
				}
			}
			catch (Exception)
			{
			}
		}

		private void tbName_TextChanged(object sender, EventArgs e)
		{
		}

		private void tbQuest_TextChanged(object sender, EventArgs e)
		{
		}

		private void cbDoms_TextChanged(object sender, EventArgs e)
		{
		}

		private void bEdit_Click(object sender, EventArgs e)
		{
			try
			{
				this.wc = false;
				if (this.dgvVars.SelectedRows[0].Cells[0].Value == null)
				{
					Expert.Domain domain = Expert.Cor.DomainByName(this.cbDoms.Text);
					if (domain == null)
					{
						MessageBox.Show("Такого домена не существует, добавление не произведено!");
					}
					else
					{
						Expert.Variable variable = Expert.Cor.addVar(this.tbName.Text.Trim().ToUpper(), this.getCurVt(), domain, (!this.rbVivod.Checked) ? this.tbQuest.Text : "", this.lastpos);
						this.lbStatus.Text = (variable == null) ? "Переменная не была добавлена!" : ("Переменная " + this.tbName.Text + " добавленa!");
						if (variable != null)
						{
							this.dgvVars.Rows[this.lastpos].Selected = false;
							this.lastpos++;
							this.dgvVars.Rows[this.lastpos].Selected = true;
						}
					}
				}
				else
				{
					string name = this.dgvVars.SelectedRows[0].Cells[0].Value.ToString();
					Expert.Variable variable2 = Expert.Cor.varByName(name);
					if (variable2 != null)
					{
						if (this.tbName.Text != variable2.Name && Expert.Cor.varByName(this.tbName.Text) != null)
						{
							MessageBox.Show("Такая переменная уже существует!");
						}
						else
						{
							variable2.ChangeThis(this.tbName.Text, (!this.rbVivod.Checked) ? this.tbQuest.Text : "", this.cbDoms.Text, this.getCurVt());
							this.isDirty = false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void bCancel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.wc)
				{
					this.wc = false;
					Expert.Cor.deleteVar(this.dgvVars.SelectedRows[0].Cells[0].Value.ToString());
					Expert.ExpertEvents.ExpertChanged();
					this.initVar(this.lastpos);
				}
				else
				{
					this.initVar(Expert.Cor.varByName(this.dgvVars.SelectedRows[0].Cells[0].Value.ToString()));
				}
			}
			catch (Exception)
			{
				this.initVar(null);
			}
		}

		private void cbDoms_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.dataVal.Rows.Clear();
			Expert.Domain domain = Expert.Cor.DomainByName(this.cbDoms.SelectedItem.ToString());
			for (int i = 0; i < domain.Values.Count; i++)
			{
				this.dataVal.Rows.Add(new object[]
				{
					domain.Values[i]
				});
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
			this.splitContainer1 = new SplitContainer();
			this.splitContainer2 = new SplitContainer();
			this.dgvVars = new DataGridView();
			this.cName = new DataGridViewTextBoxColumn();
			this.bDel = new Button();
			this.bAdd = new Button();
			this.splitContainer3 = new SplitContainer();
			this.dataVal = new DataGridView();
			this.Val = new DataGridViewTextBoxColumn();
			this.panel1 = new Panel();
			this.tbQuest = new TextBox();
			this.rbVZ = new RadioButton();
			this.rbVivod = new RadioButton();
			this.rbZap = new RadioButton();
			this.cbDoms = new ComboBox();
			this.label2 = new Label();
			this.label1 = new Label();
			this.tbName = new TextBox();
			this.bCancel = new Button();
			this.bApply = new Button();
			this.lbStatus = new Label();
			this.splitContainer1.BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((ISupportInitialize)(this.dgvVars)).BeginInit();
			this.splitContainer3.BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			((ISupportInitialize)(this.dataVal)).BeginInit();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.splitContainer1.Location = new Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel1MinSize = 100;
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer1.Panel2MinSize = 100;
			this.splitContainer1.Size = new Size(475, 389);
			this.splitContainer1.SplitterDistance = 253;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer2.Dock = (System.Windows.Forms.DockStyle)5;
			this.splitContainer2.FixedPanel = (System.Windows.Forms.FixedPanel)2;
			this.splitContainer2.Location = new Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = 0;
			this.splitContainer2.Panel1.Controls.Add(this.dgvVars);
			this.splitContainer2.Panel1MinSize = 200;
			this.splitContainer2.Panel2.Controls.Add(this.bDel);
			this.splitContainer2.Panel2.Controls.Add(this.bAdd);
			this.splitContainer2.Panel2MinSize = 65;
			this.splitContainer2.Size = new Size(253, 389);
			this.splitContainer2.SplitterDistance = 324;
			this.splitContainer2.TabIndex = 1;
			this.dgvVars.BackgroundColor = Color.White;
			this.dgvVars.ColumnHeadersHeightSizeMode = (System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode)2;
			this.dgvVars.Columns.AddRange(new DataGridViewColumn[]
			{
				this.cName
			});
			this.dgvVars.Dock = (System.Windows.Forms.DockStyle)5;
			this.dgvVars.Location = new Point(0, 0);
			this.dgvVars.Name = "dgvVars";
			this.dgvVars.ReadOnly = true;
			this.dgvVars.SelectionMode = (System.Windows.Forms.DataGridViewSelectionMode)1;
			this.dgvVars.Size = new Size(253, 324);
			this.dgvVars.TabIndex = 0;
			this.dgvVars.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvVars_CellDoubleClick);
			this.dgvVars.SelectionChanged += new EventHandler(this.dgvVars_SelectionChanged);
			this.cName.HeaderText = "Имя";
			this.cName.Name = "cName";
			this.cName.ReadOnly = true;
			this.cName.Width = 200;
			this.bDel.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.bDel.Location = new Point(3, 34);
			this.bDel.Name = "bDel";
			this.bDel.Size = new Size(247, 25);
			this.bDel.TabIndex = 2;
			this.bDel.Text = "Удалить";
			this.bDel.UseVisualStyleBackColor = true;
			this.bDel.Click += new EventHandler(this.bDel_Click);
			this.bAdd.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.bAdd.Location = new Point(3, 3);
			this.bAdd.Name = "bAdd";
			this.bAdd.Size = new Size(247, 25);
			this.bAdd.TabIndex = 0;
			this.bAdd.Text = "Добавить";
			this.bAdd.UseVisualStyleBackColor = true;
			this.bAdd.Click += new EventHandler(this.bAdd_Click);
			this.splitContainer3.Dock = (System.Windows.Forms.DockStyle)5;
			this.splitContainer3.FixedPanel = (System.Windows.Forms.FixedPanel)2;
			this.splitContainer3.Location = new Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = 0;
			this.splitContainer3.Panel1.Controls.Add(this.dataVal);
			this.splitContainer3.Panel1.Controls.Add(this.panel1);
			this.splitContainer3.Panel1.Controls.Add(this.cbDoms);
			this.splitContainer3.Panel1.Controls.Add(this.label2);
			this.splitContainer3.Panel1.Controls.Add(this.label1);
			this.splitContainer3.Panel1.Controls.Add(this.tbName);
			this.splitContainer3.Panel2.Controls.Add(this.bCancel);
			this.splitContainer3.Panel2.Controls.Add(this.bApply);
			this.splitContainer3.Panel2MinSize = 65;
			this.splitContainer3.Size = new Size(218, 389);
			this.splitContainer3.SplitterDistance = 324;
			this.splitContainer3.TabIndex = 7;
			this.dataVal.BackgroundColor = Color.White;
			this.dataVal.ColumnHeadersHeightSizeMode = (System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode)2;
			this.dataVal.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Val
			});
			this.dataVal.Location = new Point(17, 85);
			this.dataVal.Name = "dataVal";
			this.dataVal.ReadOnly = true;
			this.dataVal.SelectionMode = (System.Windows.Forms.DataGridViewSelectionMode)1;
			this.dataVal.Size = new Size(181, 132);
			this.dataVal.TabIndex = 13;
			this.Val.HeaderText = "Значение";
			this.Val.Name = "Val";
			this.Val.ReadOnly = true;
			this.Val.Width = 135;
			this.panel1.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.panel1.Controls.Add(this.tbQuest);
			this.panel1.Controls.Add(this.rbVZ);
			this.panel1.Controls.Add(this.rbVivod);
			this.panel1.Controls.Add(this.rbZap);
			this.panel1.Location = new Point(12, 223);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(186, 98);
			this.panel1.TabIndex = 12;
			this.tbQuest.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.tbQuest.Location = new Point(3, 72);
			this.tbQuest.Name = "tbQuest";
			this.tbQuest.Size = new Size(180, 20);
			this.tbQuest.TabIndex = 3;
			this.rbVZ.AutoSize = true;
			this.rbVZ.Location = new Point(3, 49);
			this.rbVZ.Name = "rbVZ";
			this.rbVZ.Size = new Size(169, 17);
			this.rbVZ.TabIndex = 2;
			this.rbVZ.Text = "Выводимо - запрашиваемая";
			this.rbVZ.UseVisualStyleBackColor = true;
			this.rbVivod.AutoSize = true;
			this.rbVivod.Location = new Point(3, 26);
			this.rbVivod.Name = "rbVivod";
			this.rbVivod.Size = new Size(84, 17);
			this.rbVivod.TabIndex = 1;
			this.rbVivod.Text = "Выводимая";
			this.rbVivod.UseVisualStyleBackColor = true;
			this.rbZap.AutoSize = true;
			this.rbZap.Checked = true;
			this.rbZap.Location = new Point(3, 3);
			this.rbZap.Name = "rbZap";
			this.rbZap.Size = new Size(108, 17);
			this.rbZap.TabIndex = 0;
			this.rbZap.TabStop = true;
			this.rbZap.Text = "Запрашиваемая";
			this.rbZap.UseVisualStyleBackColor = true;
			this.cbDoms.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.cbDoms.FormattingEnabled = true;
			this.cbDoms.Location = new Point(17, 58);
			this.cbDoms.Name = "cbDoms";
			this.cbDoms.Size = new Size(181, 21);
			this.cbDoms.TabIndex = 10;
			this.cbDoms.SelectedIndexChanged += new EventHandler(this.cbDoms_SelectedIndexChanged);
			this.label2.Anchor = (System.Windows.Forms.AnchorStyles)1;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(85, 42);
			this.label2.Name = "label2";
			this.label2.Size = new Size(42, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Домен";
			this.label1.Anchor = (System.Windows.Forms.AnchorStyles)1;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(91, 3);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Имя";
			this.tbName.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.tbName.Location = new Point(17, 19);
			this.tbName.Name = "tbName";
			this.tbName.Size = new Size(181, 20);
			this.tbName.TabIndex = 7;
			this.bCancel.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.bCancel.Location = new Point(3, 35);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new Size(212, 23);
			this.bCancel.TabIndex = 3;
			this.bCancel.Text = "Отменить";
			this.bCancel.UseVisualStyleBackColor = true;
			this.bCancel.Click += new EventHandler(this.bCancel_Click);
			this.bApply.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.bApply.Location = new Point(3, 3);
			this.bApply.Name = "bApply";
			this.bApply.Size = new Size(212, 23);
			this.bApply.TabIndex = 2;
			this.bApply.Text = "Применить";
			this.bApply.UseVisualStyleBackColor = true;
			this.bApply.Click += new EventHandler(this.bEdit_Click);
			this.lbStatus.Anchor = (System.Windows.Forms.AnchorStyles)6;
			this.lbStatus.AutoSize = true;
			this.lbStatus.Location = new Point(0, 392);
			this.lbStatus.Name = "lbStatus";
			this.lbStatus.Size = new Size(18, 13);
			this.lbStatus.TabIndex = 2;
			this.lbStatus.Text = "txt";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = (System.Windows.Forms.AutoScaleMode)1;
			base.ClientSize = new Size(475, 407);
			base.Controls.Add(this.lbStatus);
			base.Controls.Add(this.splitContainer1);
			base.Name = "varForm";
			base.StartPosition = (System.Windows.Forms.FormStartPosition)1;
			this.Text = "Переменные";
			base.Load += new EventHandler(this.varForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.EndInit();
			this.splitContainer2.ResumeLayout(false);
			((ISupportInitialize)(this.dgvVars)).EndInit();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.EndInit();
			this.splitContainer3.ResumeLayout(false);
			((ISupportInitialize)(this.dataVal)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
