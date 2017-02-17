using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExpertSystem
{
	public class domainForm : Form
	{
		private static domainForm currentF = null;

		private bool isDirty = false;

		private Rectangle dragBoxFromMouseDown;

		private int rowIndexFromMouseDown;

		private int rowIndexOfItemUnderMouseToDrop;

		private int lastposDoms = -1;

		private bool wc = false;

		private string lstnme = "";

		private IContainer components = null;

		private SplitContainer splitContainer1;

		private DataGridView dgvDomains;

		private SplitContainer splitContainer2;

		private Button bAdd;

		private Button bDel;

		private TextBox tbName;

		private DataGridView dgvVals;

		private Label label1;

		private DataGridViewTextBoxColumn cValue;

		private Panel panel1;

		private Button button2;

		private Button button1;

		private DataGridViewTextBoxColumn idDom;

		private Label lbStatus;

		private Button deleteValue;

		private Button EditValue;

		private TextBox domValue;

		private Button addValue;

		public static domainForm get(bool getnew = false)
		{
			domainForm result;
			if (domainForm.currentF != null)
			{
				if (!getnew)
				{
					domainForm.currentF.Select();
					result = domainForm.currentF;
					return result;
				}
				domainForm.currentF.Close();
			}
			domainForm.currentF = new domainForm();
			result = domainForm.currentF;
			return result;
		}

		public static void SelectDomain(Expert.Domain d, string nm = "")
		{
			domainForm domainForm = domainForm.get(false);
			domainForm.Show();
			domainForm.Select();
			if (d == null)
			{
				d = Expert.Cor.DomainByName(nm);
				if (d != null)
				{
					IEnumerator enumerator = ((IEnumerable)(domainForm.dgvDomains.Rows)).GetEnumerator();
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
					IEnumerator enumerator = ((IEnumerable)(domainForm.dgvDomains.Rows)).GetEnumerator();
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
					domainForm.tbName.Text = nm;
				}
			}
			else
			{
				domainForm.initDomain(d);
			}
		}

		private domainForm()
		{
			this.InitializeComponent();
			Expert.ExpertEvents.ExpertChangedEvent += new Expert.ExpertEvents.vv(this.upgradeDgvDom);
			base.FormClosed += new FormClosedEventHandler(this.domainForm_FormClosed);
			this.dgvVals.MouseMove += new MouseEventHandler(this.dgvVals_MouseMove);
			this.dgvVals.MouseDown += new MouseEventHandler(this.dgvVals_MouseDown);
			this.dgvVals.DragOver += new DragEventHandler(this.dgvVals_DragOver);
			this.dgvVals.DragDrop += new DragEventHandler(this.dgvVals_DragDrop);
		}

		private void domainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Expert.ExpertEvents.ExpertChangedEvent -= new Expert.ExpertEvents.vv(this.upgradeDgvDom);
			domainForm.currentF = null;
		}

		private void bClear_Click(object sender, EventArgs e)
		{
			this.tbName.Text = "";
			this.dgvVals.Rows.Clear();
			this.isDirty = false;
		}

		private void dgvVals_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & (MouseButtons)1048576) == (MouseButtons)1048576)
			{
				if (this.dragBoxFromMouseDown != Rectangle.Empty && !this.dragBoxFromMouseDown.Contains(e.X, e.Y))
				{
					DragDropEffects dragDropEffects = this.dgvVals.DoDragDrop(this.dgvVals.Rows[this.rowIndexFromMouseDown], (System.Windows.Forms.DragDropEffects)2);
				}
			}
		}

		private void dgvVals_MouseDown(object sender, MouseEventArgs e)
		{
			this.rowIndexFromMouseDown = this.dgvVals.HitTest(e.X, e.Y).RowIndex;
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

		private void dgvVals_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = (System.Windows.Forms.DragDropEffects)2;
		}

		private void dgvVals_DragDrop(object sender, DragEventArgs e)
		{
			Point point = this.dgvVals.PointToClient(new Point(e.X, e.Y));
			this.rowIndexOfItemUnderMouseToDrop = this.dgvVals.HitTest(point.X, point.Y).RowIndex;
			if (e.Effect == (DragDropEffects)2)
			{
				DataGridViewRow dataGridViewRow = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
				this.dgvVals.Rows.RemoveAt(this.rowIndexFromMouseDown);
				this.dgvVals.Rows.Insert(this.rowIndexOfItemUnderMouseToDrop, dataGridViewRow);
			}
		}

		private void dgvVals_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
		}

		private void tbName_TextChanged(object sender, EventArgs e)
		{
		}

		private void bAdd_Click(object sender, EventArgs e)
		{
			Expert cor = Expert.Cor;
			List<string> vals = new List<string>();
			Expert.Domain domain = cor.AddNewDomain(cor.GetNextDomName(), vals, this.lastposDoms);
			if (domain != null)
			{
				this.isDirty = false;
				this.bClear_Click(null, null);
				this.tbName.Text = domain.Name;
				this.tbName.Select();
				this.tbName.SelectionStart = 0;
				this.tbName.SelectionLength = this.tbName.Text.Length;
				this.lbStatus.Text = domain.Name + " создан";
				this.wc = true;
				this.lstnme = domain.Name;
			}
			else
			{
				this.lbStatus.Text = "Невозможно создать доман!";
			}
		}

		private void upgradeDgvDom()
		{
			int num = this.lastposDoms;
			bool flag = this.isDirty;
			this.isDirty = true;
			this.dgvDomains.Rows.Clear();
			int num2 = -1;
			using (List<Expert.Domain>.Enumerator enumerator = Expert.Cor.Domains.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Expert.Domain current = enumerator.Current;
					this.dgvDomains.Rows.Add();
					num2++;
					this.dgvDomains[0, num2].Value = current.Name;
				}
			}
			this.lastposDoms = num;
			if (this.lastposDoms != -1)
			{
				IEnumerator enumerator2 = ((IEnumerable)(this.dgvDomains.Rows)).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator2.Current;
						try
						{
							dataGridViewRow.Selected = dataGridViewRow.Cells[0].RowIndex == this.lastposDoms;
						}
						catch (Exception)
						{
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator2 as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			try
			{
				this.dgvDomains.Rows[this.lastposDoms].Selected = false;
				this.dgvDomains.Rows[this.lastposDoms].Selected = true;
			}
			catch (Exception)
			{
			}
			this.isDirty = flag;
		}

		private void domainForm_Load(object sender, EventArgs e)
		{
			this.upgradeDgvDom();
		}

		private void dgvDomains_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				this.lastposDoms = this.dgvDomains.SelectedRows[0].Cells[0].RowIndex;
				if (!this.isDirty)
				{
					this.wc = false;
					this.initDomain((this.dgvDomains.SelectedRows[0].Cells[0].Value == null) ? null : Expert.Cor.DomainByName(this.dgvDomains.SelectedRows[0].Cells[0].Value.ToString()));
				}
			}
			catch (Exception)
			{
			}
		}

		private void initDomain(int pos)
		{
			try
			{
				object value = this.dgvDomains.SelectedRows[0].Cells[0].Value;
				this.initDomain((value == null) ? null : Expert.Cor.DomainByName(value.ToString()));
			}
			catch (Exception ex)
			{
				this.lbStatus.Text = ex.Message;
			}
		}

		private void initDomain(Expert.Domain d)
		{
			if (d == null)
			{
				this.dgvVals.Rows.Clear();
				this.tbName.Text = "";
				this.isDirty = false;
				this.lbStatus.Text = "Домен выгружен!";
			}
			else
			{
				this.dgvVals.Rows.Clear();
				this.tbName.Text = d.Name;
				using (List<string>.Enumerator enumerator = d.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						this.dgvVals.Rows.Add(new object[]
						{
							current
						});
					}
				}
				IEnumerator enumerator2 = ((IEnumerable)(this.dgvDomains.Rows)).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator2.Current;
						try
						{
							dataGridViewRow.Selected = dataGridViewRow.Cells[0].Value.ToString() == d.Name;
						}
						catch (Exception)
						{
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator2 as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this.lbStatus.Text = "Домен " + d.Name + " загружен!";
				this.isDirty = false;
			}
		}

		private void bDel_Click(object sender, EventArgs e)
		{
			bool flag = false;
			IEnumerator enumerator = this.dgvDomains.SelectedRows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
					if (dataGridViewRow.Cells[0].Value != null)
					{
						Expert.Cor.DeleteDomain(dataGridViewRow.Cells[0].Value.ToString(), false);
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
				this.isDirty = false;
				Expert.ExpertEvents.ExpertChanged();
				this.tbName.Text = "";
				this.dgvVals.Rows.Clear();
				this.initDomain(this.lastposDoms);
				this.lbStatus.Text = "Выделенный домен удален!";
				this.isDirty = false;
			}
			else
			{
				this.lbStatus.Text = "Удаление не было проведенно!";
			}
		}

		private void dgvDomains_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			this.dgvDomains_SelectionChanged(null, null);
		}

		private void bEdit_Click(object sender, EventArgs e)
		{
			try
			{
				this.wc = false;
				if (this.dgvDomains.SelectedRows[0].Cells[0].Value == null)
				{
					List<string> list = new List<string>();
					IEnumerator enumerator = ((IEnumerable)(this.dgvVals.Rows)).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
							if (dataGridViewRow.Cells[0].Value != null)
							{
								list.Add(dataGridViewRow.Cells[0].Value.ToString());
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
					Expert.Domain domain = Expert.Cor.AddNewDomain(this.tbName.Text.Trim().ToUpper(), list, this.lastposDoms);
					this.lbStatus.Text = (domain == null) ? "Домен не был добавлен!" : ("Домен " + this.tbName.Text + " добавлен!");
					if (domain != null)
					{
						this.dgvDomains.Rows[this.lastposDoms].Selected = false;
						this.lastposDoms++;
						this.dgvDomains.Rows[this.lastposDoms].Selected = true;
					}
				}
				else
				{
					string name = this.dgvDomains.SelectedRows[0].Cells[0].Value.ToString();
					Expert.Domain domain2 = Expert.Cor.DomainByName(name);
					if (this.tbName.Text != domain2.Name && Expert.Cor.DomainByName(this.tbName.Text) != null)
					{
						MessageBox.Show("Такой домен уже существует!");
					}
					else
					{
						domain2.Name = this.tbName.Text.Trim().ToUpper();
						List<string> list2 = new List<string>();
						IEnumerator enumerator = ((IEnumerable)(this.dgvVals.Rows)).GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
								if (dataGridViewRow.Cells[0].Value != null)
								{
									list2.Add(dataGridViewRow.Cells[0].Value.ToString());
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
						this.isDirty = false;
						domain2.assignNewVals(list2);
						this.lbStatus.Text = "Домен " + domain2.Name + " обновлен!";
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.wc)
				{
					this.wc = false;
					Expert.Cor.DeleteDomain(this.dgvDomains.SelectedRows[0].Cells[0].Value.ToString(), true);
					this.initDomain(this.lastposDoms);
				}
				else
				{
					this.initDomain(Expert.Cor.DomainByName(this.dgvDomains.SelectedRows[0].Cells[0].Value.ToString()));
				}
			}
			catch (Exception)
			{
				this.initDomain(null);
			}
		}

		private void addValue_Click(object sender, EventArgs e)
		{
			if (this.domValue.Text == "")
			{
				this.lbStatus.Text = "Не введено значение";
			}
			else
			{
				bool flag = false;
				int count = this.dgvVals.Rows.Count;
				for (int i = 0; i < count - 1; i++)
				{
					if (this.domValue.Text == this.dgvVals.Rows[i].Cells[0].Value.ToString())
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.lbStatus.Text = "Такое значение уже есть в текущем домене!";
					this.domValue.Text = "";
				}
				else
				{
					this.dgvVals.Rows.Add(new object[]
					{
						this.domValue.Text
					});
					this.domValue.Text = "";
					this.lbStatus.Text = "Значение " + this.domValue.Text + " успешно добавлено";
				}
			}
		}

		private void EditValue_Click(object sender, EventArgs e)
		{
			this.dgvVals.SelectedRows[0].Cells[0].Value = this.domValue.Text;
			this.dgvVals.Text = "";
		}

		private void dgvVals_SelectionChanged(object sender, EventArgs e)
		{
			if (this.dgvVals.SelectedRows.Count != 0)
			{
				this.domValue.Text = this.dgvVals.SelectedRows[0].Cells[0].Value.ToString();
			}
		}

		private void deleteValue_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedCellCollection selectedCells = this.dgvVals.SelectedCells;
			IEnumerator enumerator = selectedCells.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)enumerator.Current;
					this.dgvVals.Rows.RemoveAt(dataGridViewCell.RowIndex);
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
			this.lbStatus.Text = "Значения удалены";
			this.domValue.Text = "";
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
			this.dgvDomains = new DataGridView();
			this.idDom = new DataGridViewTextBoxColumn();
			this.bDel = new Button();
			this.bAdd = new Button();
			this.panel1 = new Panel();
			this.button2 = new Button();
			this.button1 = new Button();
			this.dgvVals = new DataGridView();
			this.cValue = new DataGridViewTextBoxColumn();
			this.tbName = new TextBox();
			this.label1 = new Label();
			this.lbStatus = new Label();
			this.addValue = new Button();
			this.domValue = new TextBox();
			this.EditValue = new Button();
			this.deleteValue = new Button();
			this.splitContainer1.BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((ISupportInitialize)(this.dgvDomains)).BeginInit();
			this.panel1.SuspendLayout();
			((ISupportInitialize)(this.dgvVals)).BeginInit();
			base.SuspendLayout();
			this.splitContainer1.Anchor = 0;
			this.splitContainer1.Location = new Point(2, -5);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel1MinSize = 100;
			this.splitContainer1.Panel2.Controls.Add(this.deleteValue);
			this.splitContainer1.Panel2.Controls.Add(this.EditValue);
			this.splitContainer1.Panel2.Controls.Add(this.domValue);
			this.splitContainer1.Panel2.Controls.Add(this.addValue);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Panel2.Controls.Add(this.dgvVals);
			this.splitContainer1.Panel2.Controls.Add(this.tbName);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Size = new Size(466, 426);
			this.splitContainer1.SplitterDistance = 227;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer2.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.splitContainer2.FixedPanel = (System.Windows.Forms.FixedPanel)2;
			this.splitContainer2.Location = new Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = 0;
			this.splitContainer2.Panel1.Controls.Add(this.dgvDomains);
			this.splitContainer2.Panel1MinSize = 100;
			this.splitContainer2.Panel2.Controls.Add(this.bDel);
			this.splitContainer2.Panel2.Controls.Add(this.bAdd);
			this.splitContainer2.Panel2MinSize = 70;
			this.splitContainer2.Size = new Size(237, 426);
			this.splitContainer2.SplitterDistance = 352;
			this.splitContainer2.TabIndex = 0;
			this.dgvDomains.BackgroundColor = Color.White;
			this.dgvDomains.ColumnHeadersHeightSizeMode = (System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode)2;
			this.dgvDomains.Columns.AddRange(new DataGridViewColumn[]
			{
				this.idDom
			});
			this.dgvDomains.GridColor = SystemColors.ActiveCaptionText;
			this.dgvDomains.Location = new Point(3, 0);
			this.dgvDomains.Name = "dgvDomains";
			this.dgvDomains.ReadOnly = true;
			this.dgvDomains.SelectionMode = (System.Windows.Forms.DataGridViewSelectionMode)1;
			this.dgvDomains.Size = new Size(221, 352);
			this.dgvDomains.TabIndex = 0;
			this.dgvDomains.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvDomains_CellDoubleClick);
			this.dgvDomains.SelectionChanged += new EventHandler(this.dgvDomains_SelectionChanged);
			this.idDom.HeaderText = "Домены";
			this.idDom.Name = "idDom";
			this.idDom.ReadOnly = true;
			this.idDom.SortMode = 0;
			this.idDom.Width = 150;
			this.bDel.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.bDel.Location = new Point(0, 41);
			this.bDel.Name = "bDel";
			this.bDel.Size = new Size(225, 25);
			this.bDel.TabIndex = 2;
			this.bDel.Text = "Удалить домен";
			this.bDel.UseVisualStyleBackColor = true;
			this.bDel.Click += new EventHandler(this.bDel_Click);
			this.bAdd.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.bAdd.Location = new Point(0, 10);
			this.bAdd.Name = "bAdd";
			this.bAdd.Size = new Size(225, 25);
			this.bAdd.TabIndex = 0;
			this.bAdd.Text = "Добавить домен";
			this.bAdd.UseVisualStyleBackColor = true;
			this.bAdd.Click += new EventHandler(this.bAdd_Click);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = (System.Windows.Forms.DockStyle)2;
			this.panel1.Location = new Point(0, 358);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(235, 68);
			this.panel1.TabIndex = 3;
			this.button2.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.button2.Location = new Point(3, 40);
			this.button2.Name = "button2";
			this.button2.Size = new Size(219, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Отменить";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.button1.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.button1.Location = new Point(6, 9);
			this.button1.Name = "button1";
			this.button1.Size = new Size(216, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Применить к домену";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.bEdit_Click);
			this.dgvVals.AllowDrop = true;
			this.dgvVals.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.dgvVals.BackgroundColor = Color.White;
			this.dgvVals.ColumnHeadersHeightSizeMode = (System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode)2;
			this.dgvVals.Columns.AddRange(new DataGridViewColumn[]
			{
				this.cValue
			});
			this.dgvVals.Location = new Point(3, 55);
			this.dgvVals.Name = "dgvVals";
			this.dgvVals.ReadOnly = true;
			this.dgvVals.SelectionMode = (System.Windows.Forms.DataGridViewSelectionMode)1;
			this.dgvVals.Size = new Size(219, 184);
			this.dgvVals.TabIndex = 2;
			this.dgvVals.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgvVals_RowsAdded);
			this.dgvVals.SelectionChanged += new EventHandler(this.dgvVals_SelectionChanged);
			this.cValue.HeaderText = "Значения";
			this.cValue.Name = "cValue";
			this.cValue.ReadOnly = true;
			this.cValue.Width = 150;
			this.tbName.Anchor = (System.Windows.Forms.AnchorStyles)13;
			this.tbName.Location = new Point(3, 25);
			this.tbName.Name = "tbName";
			this.tbName.Size = new Size(219, 20);
			this.tbName.TabIndex = 0;
			this.tbName.TextChanged += new EventHandler(this.tbName_TextChanged);
			this.label1.Anchor = (System.Windows.Forms.AnchorStyles)1;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(92, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Имя";
			this.lbStatus.Anchor = (System.Windows.Forms.AnchorStyles)6;
			this.lbStatus.AutoSize = true;
			this.lbStatus.Location = new Point(0, 420);
			this.lbStatus.Name = "lbStatus";
			this.lbStatus.Size = new Size(0, 13);
			this.lbStatus.TabIndex = 1;
			this.addValue.Location = new Point(3, 271);
			this.addValue.Name = "addValue";
			this.addValue.Size = new Size(219, 23);
			this.addValue.TabIndex = 4;
			this.addValue.Text = "Добавить значение";
			this.addValue.UseVisualStyleBackColor = true;
			this.addValue.Click += new EventHandler(this.addValue_Click);
			this.domValue.Location = new Point(3, 245);
			this.domValue.Name = "domValue";
			this.domValue.Size = new Size(219, 20);
			this.domValue.TabIndex = 5;
			this.EditValue.Location = new Point(3, 300);
			this.EditValue.Name = "EditValue";
			this.EditValue.Size = new Size(219, 23);
			this.EditValue.TabIndex = 6;
			this.EditValue.Text = "Изменить значение";
			this.EditValue.UseVisualStyleBackColor = true;
			this.EditValue.Click += new EventHandler(this.EditValue_Click);
			this.deleteValue.Location = new Point(3, 329);
			this.deleteValue.Name = "deleteValue";
			this.deleteValue.Size = new Size(219, 23);
			this.deleteValue.TabIndex = 7;
			this.deleteValue.Text = "Удалить значение";
			this.deleteValue.UseVisualStyleBackColor = true;
			this.deleteValue.Click += new EventHandler(this.deleteValue_Click);
			base.AutoScaleMode = 0;
			base.ClientSize = new Size(471, 434);
			base.Controls.Add(this.lbStatus);
			base.Controls.Add(this.splitContainer1);
			base.Name = "domainForm";
			base.StartPosition = (System.Windows.Forms.FormStartPosition)1;
			this.Text = "Домены";
			base.Load += new EventHandler(this.domainForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.EndInit();
			this.splitContainer2.ResumeLayout(false);
			((ISupportInitialize)(this.dgvDomains)).EndInit();
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)(this.dgvVals)).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
