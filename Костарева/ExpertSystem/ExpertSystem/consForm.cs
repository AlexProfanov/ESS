using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExpertSystem
{
	public class consForm : Form
	{
		private IContainer components = null;

		private TabControl tabControl1;

		private TabPage tabPage2;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn VNme;

		private DataGridViewTextBoxColumn val;

		private TabPage tabPage1;

		private Panel panel1;

		private Panel panel2;

		public static void Execute(Expert c, TreeView tvFULL, TreeView tvACC, string varb, Dictionary<string, string> ds, mainForm mf = null)
		{
			consForm consForm = new consForm();
			tvFULL.Dock = (System.Windows.Forms.DockStyle)5;
			consForm.panel2.Controls.Add(tvFULL);
			consForm.panel1.Controls.Add(tvACC);
			tvACC.Dock = (System.Windows.Forms.DockStyle)5;
			using (List<Expert.Variable>.Enumerator enumerator = c.Variables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Expert.Variable current = enumerator.Current;
					consForm.dataGridView1.Rows.Add(new object[]
					{
						current.Name,
						ds.ContainsKey(current.Name) ? ds[current.Name] : "НЕ_ОПРЕДЕЛЕНО"
					});
				}
			}
			if (mf != null)
			{
			}
			consForm.Show();
		}

		private consForm()
		{
			this.InitializeComponent();
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
			this.tabControl1 = new TabControl();
			this.tabPage2 = new TabPage();
			this.panel1 = new Panel();
			this.dataGridView1 = new DataGridView();
			this.VNme = new DataGridViewTextBoxColumn();
			this.val = new DataGridViewTextBoxColumn();
			this.tabPage1 = new TabPage();
			this.panel2 = new Panel();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.tabPage1.SuspendLayout();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock = (System.Windows.Forms.DockStyle)5;
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(600, 491);
			this.tabControl1.TabIndex = 0;
			this.tabPage2.Controls.Add(this.panel1);
			this.tabPage2.Controls.Add(this.dataGridView1);
			this.tabPage2.Location = new Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3);
			this.tabPage2.Size = new Size(592, 465);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Результат";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.panel1.Dock = (System.Windows.Forms.DockStyle)1;
			this.panel1.Location = new Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(586, 192);
			this.panel1.TabIndex = 2;
			this.dataGridView1.Anchor = (System.Windows.Forms.AnchorStyles)15;
			this.dataGridView1.BackgroundColor = Color.White;
			this.dataGridView1.ColumnHeadersHeightSizeMode = (System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode)2;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.VNme,
				this.val
			});
			this.dataGridView1.Location = new Point(3, 198);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new Size(586, 271);
			this.dataGridView1.TabIndex = 1;
			this.VNme.HeaderText = "Имя переменной";
			this.VNme.Name = "VNme";
			this.VNme.ReadOnly = true;
			this.VNme.Width = 270;
			this.val.HeaderText = "Значение переменной";
			this.val.Name = "val";
			this.val.ReadOnly = true;
			this.val.Width = 270;
			this.tabPage1.Controls.Add(this.panel2);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(592, 465);
			this.tabPage1.TabIndex = 2;
			this.tabPage1.Text = "Debug";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.panel2.Dock = (System.Windows.Forms.DockStyle)5;
			this.panel2.Location = new Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(586, 459);
			this.panel2.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = (System.Windows.Forms.AutoScaleMode)1;
			base.ClientSize = new Size(600, 491);
			base.Controls.Add(this.tabControl1);
			base.Name = "consForm";
			base.StartPosition = (System.Windows.Forms.FormStartPosition)1;
			this.Text = "Результаты консультации";
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((ISupportInitialize)(this.dataGridView1)).EndInit();
			this.tabPage1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
