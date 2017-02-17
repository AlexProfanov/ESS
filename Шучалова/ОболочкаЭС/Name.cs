using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ОболочкаЭС
{
    public partial class Name : Form
    {
        public string curFile;
        public Name()
        {
            InitializeComponent();
            curFile = "ЭкспертнаяСистема";
            int i = 1;
            while (File.Exists(curFile + i.ToString())) i++;
            curFile += i.ToString();
            textBox1.Text = curFile;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                MessageBox.Show("Файл с таким названием уже существует");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Name_Load(object sender, EventArgs e)
        {

        }

    }
}
