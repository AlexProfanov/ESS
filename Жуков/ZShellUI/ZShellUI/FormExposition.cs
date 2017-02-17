using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ZShellUI
{
	public class FormExposition : Form
	{
		private IContainer components = null;

		private Panel panel1;

		private Button buttonOK;

		public FormExposition()
		{
			this.InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
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
			this.panel1 = new Panel();
			this.buttonOK = new Button();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.buttonOK);
			this.panel1.Dock = 2;
			this.panel1.Location = new Point(0, 386);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(464, 26);
			this.panel1.TabIndex = 0;
			this.buttonOK.Anchor = 1;
			this.buttonOK.Location = new Point(185, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(73, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			base.ClientSize = new Size(464, 412);
			base.Controls.Add(this.panel1);
			this.MinimumSize = new Size(480, 450);
			base.Name = "FormExposition";
			base.StartPosition = 4;
			this.Text = "Объяснение";
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
