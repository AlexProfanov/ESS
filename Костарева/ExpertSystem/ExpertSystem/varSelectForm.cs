using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExpertSystem
{
	public class varSelectForm : Form
	{
		private static string ores = "";

		private string res = "";

		private IContainer components = null;

		private ComboBox comboBox1;

		private Button button1;

		private Button button2;

		public static string exec(Expert cor)
		{
			varSelectForm varSelectForm = new varSelectForm();
			varSelectForm.comboBox1.Items.Clear();
			using (List<Expert.Variable>.Enumerator enumerator = cor.Variables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Expert.Variable current = enumerator.Current;
					if (current.vt != Expert.VarType.vtZapr)
					{
						varSelectForm.comboBox1.Items.Add(current.Name);
					}
				}
			}
			if (varSelectForm.ores != "")
			{
				varSelectForm.comboBox1.Text = varSelectForm.ores;
			}
			else if (varSelectForm.comboBox1.Items.Count > 0)
			{
				varSelectForm.comboBox1.Text = varSelectForm.comboBox1.Items[0].ToString();
			}
			varSelectForm.ShowDialog();
			if (varSelectForm.res != "")
			{
				varSelectForm.ores = varSelectForm.res;
			}
			return varSelectForm.res;
		}

		private varSelectForm()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.res = this.comboBox1.Text;
			base.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.res = "";
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
			this.comboBox1 = new ComboBox();
			this.button1 = new Button();
			this.button2 = new Button();
			base.SuspendLayout();
			this.comboBox1.DropDownStyle = (System.Windows.Forms.ComboBoxStyle)2;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new Point(3, 2);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new Size(278, 21);
			this.comboBox1.TabIndex = 0;
			this.button1.Location = new Point(12, 29);
			this.button1.Name = "button1";
			this.button1.Size = new Size(260, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Консультироваться";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.DialogResult = (System.Windows.Forms.DialogResult)2;
			this.button2.Location = new Point(12, 58);
			this.button2.Name = "button2";
			this.button2.Size = new Size(260, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Отмена";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			base.AcceptButton = this.button1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = (System.Windows.Forms.AutoScaleMode)1;
			base.CancelButton = this.button2;
			base.ClientSize = new Size(284, 93);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.comboBox1);
			base.FormBorderStyle = (System.Windows.Forms.FormBorderStyle)1;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "varSelectForm";
			base.StartPosition = (System.Windows.Forms.FormStartPosition)1;
			this.Text = "Выбор переменной";
			base.ResumeLayout(false);
		}
	}
}
