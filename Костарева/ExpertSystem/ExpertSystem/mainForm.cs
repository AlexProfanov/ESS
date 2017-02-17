using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExpertSystem
{
	public class mainForm : Form
	{
		public static mainForm mf;

		private string usingPath = "";

		private IContainer components = null;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem доменыToolStripMenuItem;

		private ToolStripMenuItem переменныеToolStripMenuItem;

		private ToolStripMenuItem правилаToolStripMenuItem;

		private ToolStripMenuItem консультацияToolStripMenuItem;

		private ToolStripMenuItem файлToolStripMenuItem;

		private ToolStripMenuItem открытьToolStripMenuItem;

		private ToolStripMenuItem сохранитьToolStripMenuItem;

		private ToolStripMenuItem сохранитьКакToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem выходToolStripMenuItem;

		private SaveFileDialog saveFileDialog1;

		private OpenFileDialog openFileDialog1;

		private ToolStripMenuItem создатьToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		public mainForm()
		{
			this.InitializeComponent();
			this.openFileDialog1.Filter = this.saveFileDialog1.Filter;
			IEnumerator enumerator = base.Controls.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Control control = (Control)enumerator.Current;
					MdiClient mdiClient = control as MdiClient;
					if (mdiClient != null)
					{
						mdiClient.BackColor = Color.White;
						break;
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
			mainForm.mf = this;
		}

		private void доменыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			domainForm domainForm = domainForm.get(false);
			domainForm.Show();
		}

		private void переменныеToolStripMenuItem_Click(object sender, EventArgs e)
		{
			varForm varForm = varForm.get(false);
			varForm.Show();
		}

		private void выходToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.usingPath == "")
			{
				this.сохранитьКакToolStripMenuItem_Click(sender, e);
			}
			else
			{
				Expert.Cor.save(this.usingPath, true);
				this.Text = "ОболочкаЭС " + this.usingPath;
				this.toolStripStatusLabel1.Text = "Экспертная система сохранена";
			}
		}

		private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.saveFileDialog1.ShowDialog() == (DialogResult)1)
			{
				this.usingPath = this.saveFileDialog1.FileName;
				Expert.Cor.save(this.usingPath, false);
				this.Text = "ОболочкаЭС " + this.usingPath;
				this.toolStripStatusLabel1.Text = "Экспертная система по адресу сохранена";
			}
		}

		private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.openFileDialog1.ShowDialog() == (DialogResult)1)
			{
				this.usingPath = this.openFileDialog1.FileName;
				Expert.Cor.open(this.usingPath);
				this.Text = "ОболочкаЭС " + this.usingPath;
				this.toolStripStatusLabel1.Text = "Экспертная система открыта";
			}
		}

		private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Expert.Cor.IsDirty)
			{
				if (MessageBox.Show("Сохранить изменения?", "", (System.Windows.Forms.MessageBoxButtons)4) == (DialogResult)6)
				{
					this.сохранитьToolStripMenuItem_Click(sender, e);
				}
			}
			Expert.CreateNewExpert();
			this.сохранитьToolStripMenuItem_Click(null, null);
			this.toolStripStatusLabel1.Text = "Экспертная система создана";
		}

		private void mainForm_Load(object sender, EventArgs e)
		{
			domainForm domainForm = domainForm.get(false);
			varForm varForm = varForm.get(false);
			rulsetForm rulsetForm = rulsetForm.get(false);
		}

		private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
		{
			rulsetForm rulsetForm = rulsetForm.get(false);
			rulsetForm.Show();
		}

		private void консультацияToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Consultation.Execute(Expert.Cor, this);
		}

		private void файлToolStripMenuItem_Click(object sender, EventArgs e)
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
			this.menuStrip1 = new MenuStrip();
			this.файлToolStripMenuItem = new ToolStripMenuItem();
			this.создатьToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.открытьToolStripMenuItem = new ToolStripMenuItem();
			this.сохранитьToolStripMenuItem = new ToolStripMenuItem();
			this.сохранитьКакToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.выходToolStripMenuItem = new ToolStripMenuItem();
			this.доменыToolStripMenuItem = new ToolStripMenuItem();
			this.переменныеToolStripMenuItem = new ToolStripMenuItem();
			this.правилаToolStripMenuItem = new ToolStripMenuItem();
			this.консультацияToolStripMenuItem = new ToolStripMenuItem();
			this.saveFileDialog1 = new SaveFileDialog();
			this.openFileDialog1 = new OpenFileDialog();
			this.statusStrip1 = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.файлToolStripMenuItem,
				this.доменыToolStripMenuItem,
				this.переменныеToolStripMenuItem,
				this.правилаToolStripMenuItem,
				this.консультацияToolStripMenuItem
			});
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(410, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.создатьToolStripMenuItem,
				this.toolStripSeparator2,
				this.открытьToolStripMenuItem,
				this.сохранитьToolStripMenuItem,
				this.сохранитьКакToolStripMenuItem,
				this.toolStripSeparator1,
				this.выходToolStripMenuItem
			});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new Size(48, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			this.файлToolStripMenuItem.Click += new EventHandler(this.файлToolStripMenuItem_Click);
			this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
			this.создатьToolStripMenuItem.Size = new Size(153, 22);
			this.создатьToolStripMenuItem.Text = "Создать";
			this.создатьToolStripMenuItem.Click += new EventHandler(this.создатьToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(150, 6);
			this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
			this.открытьToolStripMenuItem.Size = new Size(153, 22);
			this.открытьToolStripMenuItem.Text = "Открыть";
			this.открытьToolStripMenuItem.Click += new EventHandler(this.открытьToolStripMenuItem_Click);
			this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
			this.сохранитьToolStripMenuItem.Size = new Size(153, 22);
			this.сохранитьToolStripMenuItem.Text = "Сохранить";
			this.сохранитьToolStripMenuItem.Click += new EventHandler(this.сохранитьToolStripMenuItem_Click);
			this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
			this.сохранитьКакToolStripMenuItem.Size = new Size(153, 22);
			this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
			this.сохранитьКакToolStripMenuItem.Click += new EventHandler(this.сохранитьКакToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(150, 6);
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.Size = new Size(153, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.Click += new EventHandler(this.выходToolStripMenuItem_Click);
			this.доменыToolStripMenuItem.Name = "доменыToolStripMenuItem";
			this.доменыToolStripMenuItem.Size = new Size(65, 20);
			this.доменыToolStripMenuItem.Text = "Домены";
			this.доменыToolStripMenuItem.Click += new EventHandler(this.доменыToolStripMenuItem_Click);
			this.переменныеToolStripMenuItem.Name = "переменныеToolStripMenuItem";
			this.переменныеToolStripMenuItem.Size = new Size(91, 20);
			this.переменныеToolStripMenuItem.Text = "Переменные";
			this.переменныеToolStripMenuItem.Click += new EventHandler(this.переменныеToolStripMenuItem_Click);
			this.правилаToolStripMenuItem.Name = "правилаToolStripMenuItem";
			this.правилаToolStripMenuItem.Size = new Size(67, 20);
			this.правилаToolStripMenuItem.Text = "Правила";
			this.правилаToolStripMenuItem.Click += new EventHandler(this.правилаToolStripMenuItem_Click);
			this.консультацияToolStripMenuItem.Name = "консультацияToolStripMenuItem";
			this.консультацияToolStripMenuItem.Size = new Size(96, 20);
			this.консультацияToolStripMenuItem.Text = "Консультация";
			this.консультацияToolStripMenuItem.Click += new EventHandler(this.консультацияToolStripMenuItem_Click);
			this.saveFileDialog1.Filter = "Файл экспертной системы |*.exs";
			this.statusStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripStatusLabel1
			});
			this.statusStrip1.Location = new Point(0, 245);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new Size(410, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			this.toolStripStatusLabel1.BackColor = Color.White;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new Size(0, 17);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = (System.Windows.Forms.AutoScaleMode)1;
			this.BackColor = SystemColors.MenuHighlight;
			base.ClientSize = new Size(410, 267);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.menuStrip1);
			this.ForeColor = SystemColors.ControlText;
			base.IsMdiContainer = true;
			base.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new Size(418, 306);
			base.Name = "mainForm";
			base.StartPosition = (System.Windows.Forms.FormStartPosition)1;
			this.Text = "ОболочкаЭС";
			base.Load += new EventHandler(this.mainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
