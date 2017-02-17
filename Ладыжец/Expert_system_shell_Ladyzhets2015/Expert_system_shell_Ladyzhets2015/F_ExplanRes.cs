using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class F_ExplanRes : Form
	{
		private IContainer components = null;

		private Button buttonOK;

		private ListView listViewValues;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private Panel panel1;

		private Panel panel2;

		public F_ExplanRes()
		{
			this.InitializeComponent();
			this.var_values();
		}

		private void var_values()
		{
			using (IEnumerator<Variable> enumerator = Global.knowledgeBase.variableList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Variable current = enumerator.Current;
					if (current.Value != null)
					{
						ListView.ListViewItemCollection arg_59_0 = this.listViewValues.Items;
						ListViewItem listViewItem = new ListViewItem(current.ToString());
						listViewItem.SubItems.Add(current.Value.ToString());
						arg_59_0.Add(listViewItem);
					}
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void listViewValues_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void F_ExplanRes_Load(object sender, EventArgs e)
		{
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
			this.buttonOK = new Button();
			this.listViewValues = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.panel1 = new Panel();
			this.panel2 = new Panel();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.buttonOK.Location = new Point(300, 6);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(75, 21);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "Ок";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.button1_Click);
			this.listViewValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewValues.Dock = 4;
			this.listViewValues.FullRowSelect = true;
			this.listViewValues.GridLines = true;
			this.listViewValues.HeaderStyle = 1;
			this.listViewValues.Location = new Point(6, 0);
			this.listViewValues.MultiSelect = false;
			this.listViewValues.Name = "listViewValues";
			this.listViewValues.Size = new Size(290, 291);
			this.listViewValues.Sorting = 2;
			this.listViewValues.TabIndex = 1;
			this.listViewValues.UseCompatibleStateImageBehavior = false;
			this.listViewValues.View = 1;
			this.listViewValues.SelectedIndexChanged += new EventHandler(this.listViewValues_SelectedIndexChanged);
			this.columnHeader1.Text = "Переменная";
			this.columnHeader1.Width = 130;
			this.columnHeader2.Text = "Значение";
			this.columnHeader2.Width = 155;
			this.panel1.BackColor = SystemColors.ControlLightLight;
			this.panel1.Controls.Add(this.listViewValues);
			this.panel1.Dock = 4;
			this.panel1.Location = new Point(381, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(296, 291);
			this.panel1.TabIndex = 2;
			this.panel2.BackColor = SystemColors.ControlLightLight;
			this.panel2.Controls.Add(this.buttonOK);
			this.panel2.Dock = 2;
			this.panel2.Location = new Point(0, 261);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(381, 30);
			this.panel2.TabIndex = 3;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(677, 291);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Name = "F_ExplanRes";
			base.StartPosition = 1;
			this.Text = "Вывод";
			base.Load += new EventHandler(this.F_ExplanRes_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
