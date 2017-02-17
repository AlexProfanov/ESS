using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyEs
{
	public class SelectGoal : Form
	{
		public static List<string> Varlist;

		private IContainer components = null;

		private Button button1;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn Column1;

		public SelectGoal()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MainForm.GoalVar = this.dataGridView1.CurrentCell.Value.ToString();
			base.DialogResult = 1;
			base.Close();
		}

		private void SelectGoal_Load(object sender, EventArgs e)
		{
			this.dataGridView1.Rows.Add(SelectGoal.Varlist.Count);
			for (int i = 0; i < SelectGoal.Varlist.Count; i++)
			{
				this.dataGridView1.Rows[i].Cells[0].Value = SelectGoal.Varlist[i];
				if (MainForm.GoalVar != null && this.dataGridView1[0, i].Value.ToString() == MainForm.GoalVar)
				{
					this.dataGridView1.CurrentCell = this.dataGridView1[0, i];
				}
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
			this.button1 = new Button();
			this.dataGridView1 = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.dataGridView1.BeginInit();
			base.SuspendLayout();
			this.button1.Location = new Point(67, 241);
			this.button1.Name = "button1";
			this.button1.Size = new Size(107, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = 2;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Column1
			});
			this.dataGridView1.Location = new Point(16, 16);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new Size(210, 214);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.TabStop = false;
			this.Column1.AutoSizeMode = 16;
			this.Column1.HeaderText = "Variables";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.SortMode = 0;
			base.AcceptButton = this.button1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(240, 275);
			base.Controls.Add(this.dataGridView1);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = 2;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SelectGoal";
			base.StartPosition = 4;
			this.Text = "Select goal variable";
			base.Load += new EventHandler(this.SelectGoal_Load);
			this.dataGridView1.EndInit();
			base.ResumeLayout(false);
		}
	}
}
