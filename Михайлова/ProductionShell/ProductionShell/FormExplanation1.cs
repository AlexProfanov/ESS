using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormExplanation1 : Form
	{
		private IContainer components = null;

		private Button buttonOK;

		private Panel panel2;

		private Panel panel1;

		public ListView listViewValues;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		public Panel panel3;

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
			this.panel2 = new Panel();
			this.panel1 = new Panel();
			this.listViewValues = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.panel3 = new Panel();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.buttonOK.Anchor = 9;
			this.buttonOK.Location = new Point(454, 6);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(113, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "ОК";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.panel2.Controls.Add(this.buttonOK);
			this.panel2.Dock = 2;
			this.panel2.Location = new Point(0, 248);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(947, 32);
			this.panel2.TabIndex = 2;
			this.panel1.BackColor = SystemColors.ControlLight;
			this.panel1.Controls.Add(this.listViewValues);
			this.panel1.Dock = 4;
			this.panel1.Location = new Point(685, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(262, 248);
			this.panel1.TabIndex = 3;
			this.listViewValues.BorderStyle = 1;
			this.listViewValues.CausesValidation = false;
			this.listViewValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewValues.Dock = 4;
			this.listViewValues.FullRowSelect = true;
			this.listViewValues.GridLines = true;
			this.listViewValues.HeaderStyle = 1;
			this.listViewValues.Location = new Point(2, 0);
			this.listViewValues.MultiSelect = false;
			this.listViewValues.Name = "listViewValues";
			this.listViewValues.Size = new Size(260, 248);
			this.listViewValues.Sorting = 2;
			this.listViewValues.TabIndex = 1;
			this.listViewValues.UseCompatibleStateImageBehavior = false;
			this.listViewValues.View = 1;
			this.listViewValues.SelectedIndexChanged += new EventHandler(this.listViewValues_SelectedIndexChanged);
			this.columnHeader1.Text = "Переменная";
			this.columnHeader1.Width = 100;
			this.columnHeader2.Text = "Значение";
			this.columnHeader2.Width = 154;
			this.panel3.Anchor = 15;
			this.panel3.Location = new Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(681, 248);
			this.panel3.TabIndex = 4;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new Size(947, 280);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.panel2);
			base.FormBorderStyle = 3;
			base.Name = "FormExplanation1";
			base.StartPosition = 1;
			this.Text = "Объяснение";
			base.WindowState = 2;
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public FormExplanation1()
		{
			this.InitializeComponent();
			this.var_values();
		}

		private void var_values()
		{
			this.listViewValues.Items.Clear();
			using (IEnumerator<Variable> enumerator = Global.knowledgeBase.variableList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Variable current = enumerator.Current;
					if (current.Value != null)
					{
						ListView.ListViewItemCollection arg_6A_0 = this.listViewValues.Items;
						ListViewItem listViewItem = new ListViewItem(current.ToString());
						listViewItem.SubItems.Add(current.Value.ToString());
						arg_6A_0.Add(listViewItem);
					}
				}
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void listViewValues_SelectedIndexChanged(object sender, EventArgs e)
		{
		}
	}
}
