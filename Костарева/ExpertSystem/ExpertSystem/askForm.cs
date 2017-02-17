using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExpertSystem
{
	public class askForm : Form
	{
		private bool okcl = false;

		private string res = "";

		private IContainer components = null;

		private TextBox textBox1;

		private Panel panel1;

		private Button button1;

		public static string Execute(Expert.Variable v)
		{
			string name = v.Name;
			string question = v.Question;
			List<string> values = v.Dom.Values;
			askForm askForm = new askForm(name, question, values);
			askForm.ShowDialog();
			return askForm.okcl ? askForm.res : "";
		}

		private askForm(string varname, string quest, List<string> vals)
		{
			this.InitializeComponent();
			this.Text = varname;
			this.textBox1.ReadOnly = true;
			this.textBox1.Text = quest;
			bool @checked = true;
			int num = 0;
			this.res = vals[0];
			using (List<string>.Enumerator enumerator = vals.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					RadioButton radioButton = new RadioButton();
					radioButton.Width = 400;
					radioButton.Checked = @checked;
					@checked = false;
					radioButton.Text = current;
					radioButton.Name = "rb" + current;
					radioButton.Location = new Point(0, num);
					num += 25;
					radioButton.CheckedChanged += new EventHandler(this.rb_CheckedChanged);
					this.panel1.Controls.Add(radioButton);
				}
			}
		}

		private void rb_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton;
			if ((radioButton = (RadioButton)sender) != null)
			{
				this.res = radioButton.Text;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.okcl = true;
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
			this.textBox1 = new TextBox();
			this.panel1 = new Panel();
			this.button1 = new Button();
			base.SuspendLayout();
			this.textBox1.Location = new Point(3, 3);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(277, 20);
			this.textBox1.TabIndex = 0;
			this.panel1.Location = new Point(3, 29);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(277, 232);
			this.panel1.TabIndex = 1;
			this.button1.Location = new Point(3, 267);
			this.button1.Name = "button1";
			this.button1.Size = new Size(277, 27);
			this.button1.TabIndex = 2;
			this.button1.Text = "Принять!";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AcceptButton = this.button1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = (System.Windows.Forms.AutoScaleMode)1;
			base.ClientSize = new Size(284, 293);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.textBox1);
			base.FormBorderStyle = (System.Windows.Forms.FormBorderStyle)5;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "askForm";
			base.StartPosition = (System.Windows.Forms.FormStartPosition)1;
			this.Text = "askForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
