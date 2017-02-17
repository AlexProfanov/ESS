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
    public partial class AddFact : Form
    {
        public MainForm form;
        public fAddRule ruleForm;
        public Fact oldFact;
        public Fact newFact;
        public formFactType fft;

        public AddFact(MainForm frm, fAddRule far, string word1, string word2)
        {
            InitializeComponent();
            form = frm;
            ruleForm = far;
            setInitials();
            varName.Text = word1;
            valName.Text = word2;
        }

        public AddFact(MainForm frm, fAddRule far)
        {
            InitializeComponent();
            form = frm;
            ruleForm = far;
            setInitials();
        }

        public void setInitials()
        {
            foreach (Variable v in form.kbase.vars)
            {
                varName.Items.Add(v.name);
            }
            varName.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (fft)
            {
                case formFactType.addIF:
                    ruleForm.lbxIF.Items.Add(varName.SelectedItem.ToString() + " = " + valName.SelectedItem.ToString());
                    break;
                case formFactType.changeIF:
                    int ind = ruleForm.lbxIF.SelectedIndex;
                    ruleForm.lbxIF.Items[ind] = varName.SelectedItem.ToString() + " = " + valName.SelectedItem.ToString();
                    break;
                case formFactType.addThen:
                    ruleForm.lbxThen.Items.Add(varName.SelectedItem.ToString() + " = " + valName.SelectedItem.ToString());
                    ruleForm.lbxThen.SelectedIndex = ruleForm.lbxThen.Items.Count - 1;
                    break;
                case formFactType.changeThen:
                    int ind1 = ruleForm.lbxThen.SelectedIndex;
                    ruleForm.lbxThen.Items[ind1] = varName.SelectedItem.ToString() + " = " + valName.SelectedItem.ToString();
                    break;
                default:
                    break;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        //addvariable
        private void button16_Click(object sender, EventArgs e)
        {
            if (form.kbase.domains.Count > 0)
            {
                AllStructRename form1 = new AllStructRename(form);
                form1.formtype = formType.addVariable;
                form1.SetText(formType.addVariable);
                form1.ShowDialog();
                varName.Items.Clear();
                foreach (Variable v in form.kbase.vars)
                {
                    varName.Items.Add(v.name);
                }
                varName.SelectedIndex = varName.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("Необходимо добавить домены");
            }
            
        }

        //addValue
        private void button17_Click(object sender, EventArgs e)
        {
            AllStructRename frm = new AllStructRename(form);
            frm.formtype = formType.addValue;
            frm.SetText(formType.addValue);
            frm.ShowDialog();
            valName.Items.Clear();
            int ind = form.kbase.vars.FindIndex(delegate(Variable v) { return v.name == varName.SelectedItem.ToString(); });
            foreach (string s in form.kbase.vars[ind].domain.values)
            {
                valName.Items.Add(s);
            }
            valName.SelectedIndex = valName.Items.Count - 1;

        }

        private void varName_SelectedIndexChanged(object sender, EventArgs e)
        {
            valName.Items.Clear();
            int ind = form.kbase.vars.FindIndex(delegate(Variable v) { return v.name == varName.SelectedItem.ToString(); });
            foreach (string s in form.kbase.vars[ind].domain.values)
            {
                valName.Items.Add(s);
            }
            if (valName.Items.Count > 0) valName.SelectedIndex = 0;

        }
    }
}
