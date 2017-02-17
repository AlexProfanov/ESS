using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormMain : Form
	{
		private FormClosingMethod formClosingMethod = FormClosingMethod.EXIT;

		private string namefile = "";

		private IContainer components = null;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem файлToolStripMenuItem;

		private ToolStripMenuItem createToolStripMenuItem;

		private ToolStripMenuItem открытьToolStripMenuItem;

		private ToolStripMenuItem сохранитьКакToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem выходToolStripMenuItem;

		private ToolStripMenuItem знанияToolStripMenuItem;

		private ToolStripMenuItem доменыToolStripMenuItem;

		private ToolStripMenuItem переменныеToolStripMenuItem;

		private ToolStripMenuItem консультацияToolStripMenuItem;

		private ToolStripMenuItem задатьЦельToolStripMenuItem;

		private ToolStripMenuItem начатьToolStripMenuItem;

		private ToolStripMenuItem объяснениеToolStripMenuItem;

		private ToolStripMenuItem показатьToolStripMenuItem;

		private PictureBox pictureBox1;

		private ToolStripMenuItem правилаToolStripMenuItem;

		private OpenFileDialog openFileDialog;

		private SaveFileDialog saveFileDialog;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private Button button1;

		public FormMain()
		{
			this.InitializeComponent();
			this.createNewKnowledgeBase();
			this.toolStripStatusLabel1.Text = "";
			Image image = Image.FromFile("img.jpg");
			this.pictureBox1.Image = image;
		}

		private void createNewKnowledgeBase()
		{
			Global.knowledgeBase = new KnowledgeBase();
		}

		private void openKnowledgeBase()
		{
			if (this.openFileDialog.ShowDialog() == 1)
			{
				try
				{
					Global.knowledgeBase = KnowledgeBase.openKnowledgeBase(this.openFileDialog.FileName);
					this.namefile = this.openFileDialog.FileName.ToString();
					int num = this.namefile.LastIndexOf("\\");
					string text = this.namefile.Substring(num + 1, this.namefile.Length - num - 1);
					this.toolStripStatusLabel1.Text = "ЭС: " + text;
				}
				catch (ArgumentException ex)
				{
					int num2 = MessageBox.Show(ex.Message, "Ошибка", 0, 16);
				}
			}
		}

		private void saveKnowledgeBase()
		{
			if (this.namefile == "")
			{
				if (this.saveFileDialog.ShowDialog() == 1)
				{
					try
					{
						KnowledgeBase.saveKnowledgeBase(this.saveFileDialog.FileName, Global.knowledgeBase);
						this.namefile = this.saveFileDialog.FileName.ToString();
						int num = this.namefile.LastIndexOf("\\");
						string text = this.namefile.Substring(num + 1, this.namefile.Length - num - 1);
						this.toolStripStatusLabel1.Text = "ЭС: " + text;
					}
					catch (ArgumentException ex)
					{
						int num2 = MessageBox.Show(ex.Message, "Ошибка", 0, 16);
					}
				}
			}
			else
			{
				try
				{
					KnowledgeBase.saveKnowledgeBase(this.namefile, Global.knowledgeBase);
				}
				catch (ArgumentException ex)
				{
					int num2 = MessageBox.Show(ex.Message, "Ошибка", 0, 16);
				}
			}
		}

		private void setGoalForConsultation()
		{
			new FormSetGoal1().setGoal();
		}

		private void change_picture()
		{
			try
			{
				Image image = Image.FromFile(Global.knowledgeBase.Goal.Value.ToString() + ".jpg");
				this.pictureBox1.SizeMode = (image.Width > this.pictureBox1.Width || image.Height > this.pictureBox1.Height) ? 4 : 3;
				this.pictureBox1.Image = image;
			}
			catch
			{
			}
		}

		private void doConsultation()
		{
			if (Global.knowledgeBase.Goal == null)
			{
				this.setGoalForConsultation();
			}
			if (Global.knowledgeBase.Goal != null)
			{
				Global.io.startNewConsultation();
				Global.exposition.startRecording();
				try
				{
					if (Global.knowledgeBase.Goal.tryToGetValue())
					{
						int num = MessageBox.Show(Global.knowledgeBase.Goal.ToString() + " : " + Global.knowledgeBase.Goal.Value.ToString(), "Результат консультации", 0, 64);
						this.change_picture();
					}
					else
					{
						int num2 = MessageBox.Show("Цель консультации не смогла означиться", "Результат консультации", 0, 64);
					}
				}
				catch (InvalidOperationException ex)
				{
					int num3 = MessageBox.Show(ex.Message, "Ошибка выполнения", 0, 16);
				}
			}
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormMain1 formMain = new FormMain1();
			formMain.ShowDialog();
		}

		private void доменыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormDomain formDomain = new FormDomain();
			formDomain.ShowDialog();
		}

		private void переменныеToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int num = new FormVarList1().ShowDialog();
		}

		private void задатьЦельToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.setGoalForConsultation();
		}

		private void начатьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.doConsultation();
		}

		private void показатьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.exposition.showExposition();
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.createNewKnowledgeBase();
			this.saveKnowledgeBase();
		}

		private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.openKnowledgeBase();
		}

		private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.saveKnowledgeBase();
		}

		private void выходToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Сохранить ЭС?", "Внимание", 4, 32) == 6)
			{
				this.saveKnowledgeBase();
			}
			base.Close();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Сохранить ЭС?", "Внимание", 4, 32) == 6)
			{
				this.saveKnowledgeBase();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.doConsultation();
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
			this.createToolStripMenuItem = new ToolStripMenuItem();
			this.открытьToolStripMenuItem = new ToolStripMenuItem();
			this.сохранитьКакToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.выходToolStripMenuItem = new ToolStripMenuItem();
			this.знанияToolStripMenuItem = new ToolStripMenuItem();
			this.доменыToolStripMenuItem = new ToolStripMenuItem();
			this.переменныеToolStripMenuItem = new ToolStripMenuItem();
			this.правилаToolStripMenuItem = new ToolStripMenuItem();
			this.консультацияToolStripMenuItem = new ToolStripMenuItem();
			this.задатьЦельToolStripMenuItem = new ToolStripMenuItem();
			this.начатьToolStripMenuItem = new ToolStripMenuItem();
			this.объяснениеToolStripMenuItem = new ToolStripMenuItem();
			this.показатьToolStripMenuItem = new ToolStripMenuItem();
			this.pictureBox1 = new PictureBox();
			this.openFileDialog = new OpenFileDialog();
			this.saveFileDialog = new SaveFileDialog();
			this.statusStrip1 = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.button1 = new Button();
			this.menuStrip1.SuspendLayout();
			this.pictureBox1.BeginInit();
			this.statusStrip1.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.BackColor = Color.LavenderBlush;
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.файлToolStripMenuItem,
				this.знанияToolStripMenuItem,
				this.консультацияToolStripMenuItem,
				this.объяснениеToolStripMenuItem
			});
			this.menuStrip1.LayoutStyle = 3;
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(606, 23);
			this.menuStrip1.TabIndex = 10;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
			this.файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.createToolStripMenuItem,
				this.открытьToolStripMenuItem,
				this.сохранитьКакToolStripMenuItem,
				this.toolStripSeparator1,
				this.выходToolStripMenuItem
			});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new Size(48, 19);
			this.файлToolStripMenuItem.Text = "Файл";
			this.createToolStripMenuItem.Name = "createToolStripMenuItem";
			this.createToolStripMenuItem.Size = new Size(135, 22);
			this.createToolStripMenuItem.Text = "Создать";
			this.createToolStripMenuItem.Click += new EventHandler(this.createToolStripMenuItem_Click);
			this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
			this.открытьToolStripMenuItem.Size = new Size(135, 22);
			this.открытьToolStripMenuItem.Text = "Открыть";
			this.открытьToolStripMenuItem.Click += new EventHandler(this.открытьToolStripMenuItem_Click);
			this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
			this.сохранитьКакToolStripMenuItem.Size = new Size(135, 22);
			this.сохранитьКакToolStripMenuItem.Text = "Сохранить ";
			this.сохранитьКакToolStripMenuItem.Click += new EventHandler(this.сохранитьКакToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(132, 6);
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.Size = new Size(135, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.Click += new EventHandler(this.выходToolStripMenuItem_Click);
			this.знанияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.доменыToolStripMenuItem,
				this.переменныеToolStripMenuItem,
				this.правилаToolStripMenuItem
			});
			this.знанияToolStripMenuItem.Name = "знанияToolStripMenuItem";
			this.знанияToolStripMenuItem.Size = new Size(59, 19);
			this.знанияToolStripMenuItem.Text = "Знания";
			this.доменыToolStripMenuItem.Name = "доменыToolStripMenuItem";
			this.доменыToolStripMenuItem.Size = new Size(146, 22);
			this.доменыToolStripMenuItem.Text = "Домены";
			this.доменыToolStripMenuItem.Click += new EventHandler(this.доменыToolStripMenuItem_Click);
			this.переменныеToolStripMenuItem.Name = "переменныеToolStripMenuItem";
			this.переменныеToolStripMenuItem.Size = new Size(146, 22);
			this.переменныеToolStripMenuItem.Text = "Переменные";
			this.переменныеToolStripMenuItem.Click += new EventHandler(this.переменныеToolStripMenuItem_Click);
			this.правилаToolStripMenuItem.Name = "правилаToolStripMenuItem";
			this.правилаToolStripMenuItem.Size = new Size(146, 22);
			this.правилаToolStripMenuItem.Text = "Правила";
			this.правилаToolStripMenuItem.Click += new EventHandler(this.правилаToolStripMenuItem_Click);
			this.консультацияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.задатьЦельToolStripMenuItem,
				this.начатьToolStripMenuItem
			});
			this.консультацияToolStripMenuItem.Name = "консультацияToolStripMenuItem";
			this.консультацияToolStripMenuItem.Size = new Size(96, 19);
			this.консультацияToolStripMenuItem.Text = "Консультация";
			this.задатьЦельToolStripMenuItem.Name = "задатьЦельToolStripMenuItem";
			this.задатьЦельToolStripMenuItem.Size = new Size(139, 22);
			this.задатьЦельToolStripMenuItem.Text = "Задать цель";
			this.задатьЦельToolStripMenuItem.Click += new EventHandler(this.задатьЦельToolStripMenuItem_Click);
			this.начатьToolStripMenuItem.Name = "начатьToolStripMenuItem";
			this.начатьToolStripMenuItem.Size = new Size(139, 22);
			this.начатьToolStripMenuItem.Text = "Начать";
			this.начатьToolStripMenuItem.Click += new EventHandler(this.начатьToolStripMenuItem_Click);
			this.объяснениеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.показатьToolStripMenuItem
			});
			this.объяснениеToolStripMenuItem.Name = "объяснениеToolStripMenuItem";
			this.объяснениеToolStripMenuItem.Size = new Size(87, 19);
			this.объяснениеToolStripMenuItem.Text = "Объяснение";
			this.показатьToolStripMenuItem.Name = "показатьToolStripMenuItem";
			this.показатьToolStripMenuItem.Size = new Size(124, 22);
			this.показатьToolStripMenuItem.Text = "Показать";
			this.показатьToolStripMenuItem.Click += new EventHandler(this.показатьToolStripMenuItem_Click);
			this.pictureBox1.Location = new Point(-5, 25);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(616, 385);
			this.pictureBox1.TabIndex = 11;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
			this.openFileDialog.FileName = "openFileDialog1";
			this.statusStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripStatusLabel1
			});
			this.statusStrip1.Location = new Point(0, 381);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new Size(606, 22);
			this.statusStrip1.TabIndex = 12;
			this.statusStrip1.Text = "statusStrip1";
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new Size(118, 17);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			this.button1.Location = new Point(414, 50);
			this.button1.Name = "button1";
			this.button1.Size = new Size(149, 28);
			this.button1.TabIndex = 13;
			this.button1.Text = "Проконсультироваться";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(606, 403);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.menuStrip1);
			base.FormBorderStyle = 3;
			base.Name = "FormMain";
			base.StartPosition = 1;
			this.Text = "Продукционная оболочка Михайловой";
			base.FormClosing += new FormClosingEventHandler(this.FormMain_FormClosing);
			base.Load += new EventHandler(this.FormMain_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.pictureBox1.EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
