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
    public partial class fAddRule : Form
    {
        public MainForm form;
        public Rule rule;
        int index;
        bool AddingNew;

        public fAddRule(MainForm frm, int ind)
        {
            InitializeComponent();
            form = frm;
            index = ind;
            AddingNew = true;
            setInitials();
        }
        public fAddRule(MainForm frm, Rule r, int ind)
        {
            InitializeComponent();
            form = frm;
            index = ind;
            AddingNew = false;
            rule = r;
            setInitials();
        }
        void setInitials()
        {
            if (AddingNew)
            {
                tbRuleName.Text = "RULENAME";
                tbReasoning.Text = "REASON";
                lbxIF.Items.Clear();
                lbxThen.Items.Clear(); 
            }
            else
            {
                tbRuleName.Text = rule.name;
                tbReasoning.Text = rule.reason;
                lbxIF.Items.Clear();
                lbxThen.Items.Clear(); 
                foreach (Fact f in rule.sendFact)
                {
                    lbxIF.Items.Add(f.word1 + " = " + f.word2);
                }
                foreach (Fact f in rule.concFact)
                {
                    lbxThen.Items.Add(f.word1 + " = " + f.word2);
                }
            }
        }

        public void FillRule(int index)
        {
            int indRule = form.kbase.rules.FindIndex(delegate(Rule r) { return r.name == rule.name; });
            string rulestring = "";
            string name = tbRuleName.Text;

            rulestring += name + " : IF ";
            foreach (string f in lbxIF.Items)
            {
                if (lbxIF.Items.IndexOf(f) > 0) rulestring += " AND ";
                rulestring += "('" + f + "')";
            }
            rulestring += " THEN ";
            foreach (string f in lbxThen.Items)
            {
                if (lbxThen.Items.IndexOf(f) > 0) rulestring += " AND ";
                rulestring += " " + f + "";
            }
            form.lbxRules.Items[index] = rulestring;

            //fill kbase

            if (indRule == -1)
                indRule++;
            Rule ChRule = new Rule("", "", indRule);
            ChRule.name = name;
            ChRule.reason = tbReasoning.Text;
            ChRule.sendFact.Clear();
            foreach (string s in lbxIF.Items)
            {
                Fact f = new Fact(s.Split('=')[0].Trim(), s.Split('=')[1].Trim());
                ChRule.sendFact.Add(f);
            }
            ChRule.concFact.Clear();
            foreach (string s in lbxThen.Items)
            {
                Fact f = new Fact(s.Split('=')[0].Trim(), s.Split('=')[1].Trim());
                ChRule.concFact.Add(f);
            }
            form.kbase.rules[indRule] = ChRule;

        }

        private void tbRuleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        bool ruleNameExists(string name)
        {
            bool b = false;
            foreach (Rule r in form.kbase.rules)
            {
                if (r.name == name)
                {
                    b = true;
                    break;
                }
            }
            return b;
        }

        private void btnSaveRule_Click(object sender, EventArgs e)
        {
            if (AddingNew == true && ruleNameExists(tbRuleName.Text))
            {
                MessageBox.Show("Правило с таким названием уже существует");
            }
            else
            {
                if (lbxIF.Items.Count > 0 && lbxThen.Items.Count > 0 && tbRuleName.Text != "")
                {
                    if (AddingNew)
                    {
                        rule = new Rule(tbRuleName.Text, tbReasoning.Text, index);
                        form.lbxRules.Items.Insert(index, "RULENAME : IF THEN ");
                        form.kbase.rules.Insert(index, new Rule(tbRuleName.Text, tbReasoning.Text, index));
                        FillRule(index);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        FillRule(index);
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("Не заполнены необходимые поля");
                }
            }
        }

        private void btnCancelRule_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAddFact_Click(object sender, EventArgs e)
        {
            AddFact af = new AddFact(form, this);
            af.fft = formFactType.addIF;
            af.ShowDialog();
        }

        private void btnChangeFact_Click(object sender, EventArgs e)
        {
            if (lbxIF.SelectedIndex >= 0)
            {
                string s = lbxIF.SelectedItem.ToString();
                AddFact af = new AddFact(form, this, s.Split('=')[0].Trim(), s.Split('=')[1].Trim());
                af.fft = formFactType.changeIF;
                af.ShowDialog();
            }
        }

        private void btnDeleteFact_Click(object sender, EventArgs e)
        {
            if (lbxIF.SelectedIndex >= 0)
            {
                int ind = lbxIF.SelectedIndex;
                lbxIF.Items.RemoveAt(ind);
                if (lbxIF.Items.Count != ind) 
                    lbxIF.SelectedIndex = ind;
                else if (ind > 0) 
                    lbxIF.SelectedIndex = ind - 1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddFact af = new AddFact(form, this);
            af.fft = formFactType.addThen;
            af.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lbxThen.SelectedIndex >= 0)
            {
                string s = lbxThen.SelectedItem.ToString();
                AddFact af = new AddFact(form, this, s.Split('=')[0].Trim(), s.Split('=')[1].Trim());
                af.fft = formFactType.changeThen;
                af.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbxThen.SelectedIndex >= 0)
            {
                int ind = lbxThen.SelectedIndex;
                lbxThen.Items.RemoveAt(ind);
                if (lbxThen.Items.Count != ind)
                    lbxThen.SelectedIndex = ind;
                else if (ind > 0)
                    lbxThen.SelectedIndex = ind - 1;
            }
        }


    }
}
