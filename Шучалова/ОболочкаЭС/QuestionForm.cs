using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ОболочкаЭС
{
    public partial class QuestionForm : Form
    {
        KnowledgeBase kbase;
        Variable var;
        bool okpressed = false;
        public QuestionForm(KnowledgeBase kbase, Variable var)
        {
            InitializeComponent();
            this.kbase = kbase;
            this.var = var;
            label1.Text = var.question;
            foreach (string d in var.domain.values)
            {
                cmbAns.Items.Add(d);
            }
            if (cmbAns.Items.Count > 0) 
                cmbAns.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VarsWithValue v = new VarsWithValue(var, cmbAns.SelectedItem.ToString());
            kbase.valuableVariable.Add(v);
            okpressed = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            okpressed = false;
            this.Close();
        }

        private void QuestionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!okpressed) this.DialogResult = DialogResult.Cancel; else this.DialogResult = DialogResult.OK;
        }

        private void QuestionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!okpressed) this.DialogResult = DialogResult.Cancel; else this.DialogResult = DialogResult.OK;
        }
    }
}
