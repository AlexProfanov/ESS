using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ShellES
{
	public class ResultForm : Form
	{
		private IContainer components;

		private Button btnShowRes;

		private Panel panel1;

		private ImageList imgListGood;

		private ImageList imgListBad;

		private PictureBox pict;

		private RichTextBox RtxtResult;

		public ResultForm()
		{
			this.InitializeComponent();
			base.DialogResult = DialogResult.Cancel;
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void btnShowRes_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		public void DisplayNothing()
		{
			Random random = new Random(Environment.TickCount);
			if (this.imgListBad.Images.Count > 0)
			{
				this.pict.Image = this.imgListBad.Images[random.Next(this.imgListBad.Images.Count)];
			}
			else
			{
				this.pict.Image = null;
			}
			this.RtxtResult.Text = "К сожалению, ничего не определено!\nОбратитесь к другой ЭС.";
		}

		public void DisplaySmth(string HeadTxt, string varValue)
		{
			Random random = new Random(Environment.TickCount);
			if (this.imgListGood.Images.Count > 0)
			{
				this.pict.Image = this.imgListGood.Images[random.Next(this.imgListGood.Images.Count)];
			}
			else
			{
				this.pict.Image = null;
			}
			this.RtxtResult.Clear();
			RichTextBox expr_6E = this.RtxtResult;
			expr_6E.Text += HeadTxt;
			RichTextBox expr_85 = this.RtxtResult;
			expr_85.Text = expr_85.Text + "\n\n" + varValue;
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ResultForm));
			this.btnShowRes = new Button();
			this.panel1 = new Panel();
			this.RtxtResult = new RichTextBox();
			this.imgListGood = new ImageList(this.components);
			this.imgListBad = new ImageList(this.components);
			this.pict = new PictureBox();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.pict).BeginInit();
			base.SuspendLayout();
			this.btnShowRes.Location = new Point(108, 106);
			this.btnShowRes.Name = "btnShowRes";
			this.btnShowRes.Size = new Size(172, 54);
			this.btnShowRes.TabIndex = 2;
			this.btnShowRes.Text = "Посмотреть результат";
			this.btnShowRes.UseVisualStyleBackColor = true;
			this.btnShowRes.Click += new EventHandler(this.btnShowRes_Click);
			this.panel1.BorderStyle = BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.RtxtResult);
			this.panel1.Controls.Add(this.pict);
			this.panel1.Location = new Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(368, 82);
			this.panel1.TabIndex = 4;
			this.RtxtResult.Location = new Point(83, 8);
			this.RtxtResult.Name = "RtxtResult";
			this.RtxtResult.ReadOnly = true;
			this.RtxtResult.Size = new Size(274, 64);
			this.RtxtResult.TabIndex = 7;
			this.RtxtResult.Text = "";
			this.imgListGood.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imgListGood.ImageStream");
			this.imgListGood.TransparentColor = Color.Transparent;
			this.imgListGood.Images.SetKeyName(0, "byebye.ico");
			this.imgListGood.Images.SetKeyName(1, "Daisy.ico");
			this.imgListGood.Images.SetKeyName(2, "feel_good.ico");
			this.imgListGood.Images.SetKeyName(3, "haha.ico");
			this.imgListGood.Images.SetKeyName(4, "look_down.ico");
			this.imgListGood.Images.SetKeyName(5, "smile.ico");
			this.imgListGood.Images.SetKeyName(6, "Sun.ico");
			this.imgListGood.Images.SetKeyName(7, "beauty.ico");
			this.imgListGood.Images.SetKeyName(8, "big_smile.ico");
			this.imgListGood.Images.SetKeyName(9, "boss.ico");
			this.imgListBad.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imgListBad.ImageStream");
			this.imgListBad.TransparentColor = Color.Transparent;
			this.imgListBad.Images.SetKeyName(0, "shocked.ico");
			this.imgListBad.Images.SetKeyName(1, "after_boom.ico");
			this.imgListBad.Images.SetKeyName(2, "ah.ico");
			this.imgListBad.Images.SetKeyName(3, "amazing.ico");
			this.imgListBad.Images.SetKeyName(4, "anger.ico");
			this.imgListBad.Images.SetKeyName(5, "beat_shot.ico");
			this.imgListBad.Images.SetKeyName(6, "choler.ico");
			this.imgListBad.Images.SetKeyName(7, "cold.ico");
			this.imgListBad.Images.SetKeyName(8, "electric_shock.ico");
			this.imgListBad.Images.SetKeyName(9, "hell_boy.ico");
			this.imgListBad.Images.SetKeyName(10, "horror.ico");
			this.imgListBad.Images.SetKeyName(11, "ops.ico");
			this.pict.Location = new Point(10, 8);
			this.pict.Name = "pict";
			this.pict.Size = new Size(64, 64);
			this.pict.TabIndex = 6;
			this.pict.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(392, 170);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.btnShowRes);
			base.Name = "ResultForm";
			this.Text = "Итоги:";
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.pict).EndInit();
			base.ResumeLayout(false);
		}
	}
}
