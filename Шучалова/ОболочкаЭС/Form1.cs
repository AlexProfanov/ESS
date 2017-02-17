using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ОболочкаЭС
{
    public partial class MainForm : Form
    {
        public string ESname = @"";
        string FileName = @"";
        string initialCat = Environment.CurrentDirectory;
        public KnowledgeBase kbase;
        bool ChangeMode = false;
        bool canSave;
        bool beingEdited;

        public MainForm()
        {
            InitializeComponent();
            tabControl1.Visible = false;
            openFileDialog1.InitialDirectory = initialCat;
            saveFileDialog1.InitialDirectory = openFileDialog1.InitialDirectory;
            openFileDialog1.DefaultExt = ".bin";
            //openFileDialog1.Filter = saveFileDialog1.Filter;
        }

        #region MENU FILE

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Создаём новую ЭС. Запрашиваем имя
            Name nameForm = new Name();
            if (nameForm.ShowDialog() == DialogResult.OK)
            {
                ESname = nameForm.textBox1.Text;
                tabControl1.Visible = true;
                kbase = new KnowledgeBase(ESname);
                lblStatus.Text = "Новая ЭС: " + ESname + "(сохранение не производилось)";
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Открыть готовую ЭС
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        Stream TestFileStream = File.OpenRead(openFileDialog1.FileName);
                        BinaryFormatter deserializer = new BinaryFormatter();
                        kbase = (KnowledgeBase)deserializer.Deserialize(TestFileStream);
                        ESname = kbase.Name;
                        TestFileStream.Close();
                        FileName = openFileDialog1.FileName;
                        //заполнить формы 
                        kbase.FillForms(this); 
                        tabControl1.Visible = true;
                        lblStatus.Text = "Открыта ЭС: " + ESname;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Сохранить
            if (FileName == "") сохранитьКакToolStripMenuItem_Click(sender, e);
            else
            {
                Stream fstream = File.Create(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fstream, kbase);
                fstream.Close();
                lblStatus.Text = "ЭС сохранена: " + FileName;
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Сохранить как
            SaveFileDialog fdialog = new SaveFileDialog();
            fdialog.AddExtension = true;
            fdialog.DefaultExt = ".bin";
            fdialog.InitialDirectory = Environment.CurrentDirectory;
            fdialog.FileName = ESname;
            DialogResult answer = fdialog.ShowDialog();
            if (answer == DialogResult.OK)
            {
                FileName = fdialog.FileName;
                if (!FileName.Contains(".bin")) FileName += ".bin";
                Stream fstream = File.Create(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fstream, kbase);
                fstream.Close();
                lblStatus.Text = "ЭС сохранена: " + FileName;
            }
        }

        #endregion

        #region DOMAINS
        private void btnAddDomain_Click(object sender, EventArgs e)
        {
            AllStructRename form = new AllStructRename(this);
            form.formtype = formType.addDomain;
            form.SetText(formType.addDomain);
            if (form.ShowDialog() == DialogResult.OK)
            {
                lblStatus.Text = "Добавлен новый домен";
            }  
        }
     
        private void btnChangeDomain_Click(object sender, EventArgs e)
        {
            if (lbxDomains.SelectedIndex >= 0)
            {
                AllStructRename form = new AllStructRename(this);
                form.old = tbDomainName.Text.Trim().ToUpper();
                form.formtype = formType.changeDomain;
                form.SetText(formType.changeDomain);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    lblStatus.Text = "Название домена изменено";
                }
            }
        }

        public void tbDomainName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void btnDeleteDomain_Click(object sender, EventArgs e)
        {
            if (kbase.vars.FindIndex(delegate(Variable v) { return v.domain.name == lbxDomains.SelectedItem.ToString(); }) == -1)
            {
                string name = lbxDomains.SelectedItem.ToString();
                int ind = kbase.domains.FindIndex(delegate(Domain d) { return d.name == name; });
                kbase.domains.RemoveAt(ind);
                lbxDomains.Items.Remove(name);
                cmbVarDomain.Items.Remove(name);
                if (lbxDomains.Items.Count > ind) lbxDomains.SelectedIndex = ind;
                else lbxDomains.SelectedIndex = lbxDomains.Items.Count - 1;
                lblStatus.Text = "Домен удалён";
            }
            else
            {
                MessageBox.Show("Невозможно удалить использующийся домен");
            }
        }

        private void lbxDomains_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbxValues.Items.Clear();
            if (lbxDomains.SelectedIndex >= 0)
            {
                foreach (string s in kbase.domains[kbase.domains.FindIndex(delegate(Domain d) { return d.name == lbxDomains.SelectedItem.ToString(); })].values)
                {
                    lbxValues.Items.Add(s);
                }
                tbDomainName.Text = lbxDomains.Items[lbxDomains.SelectedIndex].ToString();
                btnChangeDomain.Enabled = true;
                btnDeleteDomain.Enabled = true;
                btnAddValue.Enabled = true;
                btnChangeValue.Enabled = false;
                btnDeleteValue.Enabled = false;
            }
            else
            {
                btnChangeDomain.Enabled = false;
                btnDeleteDomain.Enabled = false;
                btnAddValue.Enabled = false;
            }
        }

        #endregion

        #region VALUES
        private void btnAddValue_Click(object sender, EventArgs e)
        {
            if (lbxDomains.SelectedIndex >= 0)
            {
                AllStructRename form = new AllStructRename(this);
                form.formtype = formType.addValue;
                form.SetText(formType.addValue);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    lblStatus.Text = "Добавлено новое значение";
                }
            }
        }

        private void btnChangeValue_Click(object sender, EventArgs e)
        {
            if (lbxValues.SelectedIndex >= 0)
            {
                AllStructRename form = new AllStructRename(this);
                form.old = lbxValues.SelectedItem.ToString().ToUpper();
                form.formtype = formType.changeValue;
                form.SetText(formType.changeValue);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    lblStatus.Text = "Значение изменено";
                }
            }
        }

        private void lbxValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxValues.SelectedIndex >= 0)
            {
                btnChangeValue.Enabled = true;
                btnDeleteValue.Enabled = true;
            }
            else
            {
                btnChangeValue.Enabled = false;
                btnDeleteValue.Enabled = false;
            }
        }

        private void btnDeleteValue_Click(object sender, EventArgs e)
        {
            if (kbase.vars.FindIndex(delegate(Variable v) { return v.domain.name == lbxDomains.SelectedItem.ToString(); }) == -1)
            {
                string name = lbxValues.SelectedItem.ToString();
                int indDomen = kbase.domains.FindIndex(delegate(Domain d) { return d.name == lbxDomains.SelectedItem.ToString(); });
                int indValue = kbase.domains[indDomen].values.FindIndex(delegate(string d) { return d == name; });
                kbase.domains[indDomen].values.RemoveAt(indValue);
                lbxValues.Items.Remove(name);
                if (lbxValues.Items.Count > indValue) lbxValues.SelectedIndex = indValue;
                else lbxValues.SelectedIndex = lbxValues.Items.Count - 1;
                lblStatus.Text = "Значение удалено";
            }
            else
            {
                MessageBox.Show("Невозможно удалить значение из используемого домена");
            }
        }

        #endregion

        #region VARIABLES

        private void btnAddVariable_Click(object sender, EventArgs e)
        {
            if (kbase.domains.Count > 0)
            {
                AllStructRename form = new AllStructRename(this);
                form.formtype = formType.addVariable;
                form.SetText(formType.addVariable);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    lblStatus.Text = "Добавлена новая переменная";
                }
            }
            else
            {
                MessageBox.Show("Необходимо добавить домены");
            }
        }

        private void btnDeleteVariable_Click(object sender, EventArgs e)
        {
            if (kbase.vars.FindIndex(delegate(Variable v) { return v.name == lbxVariables.SelectedItem.ToString(); }) == -1)
            {
                int ind = kbase.vars.FindIndex(delegate(Variable v) { return v.name == lbxVariables.SelectedItem.ToString(); });
                kbase.vars.RemoveAt(ind);
                int indlbx = lbxVariables.SelectedIndex;
                lbxVariables.Items.RemoveAt(lbxVariables.SelectedIndex);
                if (indlbx > lbxVariables.Items.Count - 1) indlbx = lbxVariables.Items.Count - 1;
                lbxVariables.SelectedIndex = indlbx;
                lblStatus.Text = "Переменная удалена";
            }
            else
            {
                MessageBox.Show("Невозможно удалить используемую переменную");
            }
        }

        private void lbxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxVariables.SelectedIndex >= 0)
            {
                Variable var = kbase.GetVarByName(lbxVariables.SelectedItem.ToString());
                switch (var.type)
                {
                    case varType.none:
                        break;
                    case varType.query:
                        rbQ.Checked = true;
                        gbQuest.Visible = true;
                        break;
                    case varType.deduction:
                        rbD.Checked = true;
                        gbQuest.Visible = false;
                        break;
                    case varType.queryDeduction:
                        rbQD.Checked = true;
                        gbQuest.Visible = true;
                        break;
                    default:
                        break;
                }
                lbxVarDomain.Items.Clear();
                foreach (string s in var.domain.values)
                {
                    lbxVarDomain.Items.Add(s);
                }
                tbVarName.Text = var.name;
                tbQuestion.Text = var.question;
                cmbVarDomain.SelectedItem = var.domain.name;
                btnChangeVariable.Enabled = true;
                btnDeleteVariable.Enabled = true;
                btnSaveVariable.Enabled = false;
                btnCancelVariable.Enabled = false;
            }
            else
            {
                btnChangeVariable.Enabled = false;
                btnDeleteVariable.Enabled = false;
                btnSaveVariable.Enabled = false;
                btnCancelVariable.Enabled = false;
            }
        }

        private void btnChangeVariable_Click(object sender, EventArgs e)
        {
            if (lbxVariables.SelectedIndex >= 0)
            {
                bool used = false;
                string rname = "";
                foreach (Rule r in kbase.rules)
                {
                    foreach (Fact f in r.sendFact)
                    {
                        if (f.word1 == lbxVariables.SelectedItem.ToString())
                        {
                            used = true;
                            if (rname == "") rname = r.name;
                        }
                    }
                    foreach (Fact f in r.concFact)
                    {
                        if (f.word1 == lbxVariables.SelectedItem.ToString())
                        {
                            used = true;
                            if (rname == "") rname = r.name;
                        }
                    }
                }
                DialogResult res = DialogResult.Yes;
                if (used)
                {
                    res = MessageBox.Show("Переменная " + lbxVariables.SelectedItem.ToString() + " используется в правиле " + rname + ". Продолжить?", "", MessageBoxButtons.YesNo);
                }
                if (res != DialogResult.No)
                {
                    AllStructRename form = new AllStructRename(this);
                    form.formtype = formType.changeVariable;
                    form.old = lbxVariables.SelectedItem.ToString().ToUpper();
                    form.SetText(formType.changeVariable);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        lblStatus.Text = "Переменная изменена";
                    }
                }
            }
        }
   
        //private void btnSaveVariable_Click(object sender, EventArgs e)
        //{
        //    if (lbxVariables.SelectedIndex >= 0)
        //    {
        //        int ind = kbase.vars.FindIndex(delegate(Variable v) { return v.name == lbxVariables.SelectedItem.ToString(); });
        //        Variable var = kbase.vars[ind];              
        //        var.domain = kbase.domains.Find(delegate(Domain d) { return d.name == cmbVarDomain.SelectedItem.ToString(); });
        //        var.domain.values.Clear();
        //        foreach (string s in lbxVarDomain.Items)
        //        {
        //            var.domain.values.Add(s);
        //        }
                
        //        if (rbD.Checked)
        //        {
        //            var.type = varType.deduction;
        //            var.question = "";
        //        }
        //        else if (rbQ.Checked)
        //        {
        //            var.type = varType.query;
        //            if (tbQuestion.Text == "")
        //                var.question = var.name + "?";
        //            else var.question = tbQuestion.Text;
        //        }
        //        else
        //        {
        //            var.type = varType.queryDeduction;
        //            if (tbQuestion.Text == "")
        //                var.question = var.name + "?";
        //            else var.question = tbQuestion.Text;
        //        }
        //        kbase.vars[ind] = var;
        //        btnSaveVariable.Enabled = false;
        //        btnCancelVariable.Enabled = false;
        //        lblStatus.Text = "Переменная изменена";
        //    }
        //}

        private void btnSaveVariable_Click(object sender, EventArgs e)
        {
            if (lbxVariables.SelectedIndex >= 0)
            {
                int ind = kbase.vars.FindIndex(delegate(Variable v) { return v.name == lbxVariables.SelectedItem.ToString(); });
                Variable var = kbase.vars[ind];

                bool used = false;
                if (var.domain.name != cmbVarDomain.SelectedItem.ToString())
                {
                    string rname = "";
                    foreach (Rule r in kbase.rules)
                    {
                        foreach (Fact f in r.sendFact)
                        {
                            if (f.word1 == lbxVariables.SelectedItem.ToString())
                            {
                                used = true;
                                if (rname == "") rname = r.name;
                            }
                        }
                        foreach (Fact f in r.concFact)
                        {
                            if (f.word1 == lbxVariables.SelectedItem.ToString())
                            {
                                used = true;
                                if (rname == "") rname = r.name;
                            }
                        }
                    }
                }
                if (used)
                {
                    MessageBox.Show("Переменная " + lbxVariables.SelectedItem.ToString() + " используется в правиле и не может изменять домен");
                }
                else
                {
                    var.domain = kbase.domains.Find(delegate(Domain d) { return d.name == cmbVarDomain.SelectedItem.ToString(); });
                    var.domain.values.Clear();
                    foreach (string s in lbxVarDomain.Items)
                    {
                        var.domain.values.Add(s);
                    }

                    if (rbD.Checked)
                    {
                        var.type = varType.deduction;
                        var.question = "";
                    }
                    else if (rbQ.Checked)
                    {
                        var.type = varType.query;
                        if (tbQuestion.Text == "")
                            var.question = var.name + "?";
                        else var.question = tbQuestion.Text;
                    }
                    else
                    {
                        var.type = varType.queryDeduction;
                        if (tbQuestion.Text == "")
                            var.question = var.name + "?";
                        else var.question = tbQuestion.Text;
                    }
                    kbase.vars[ind] = var;
                    btnSaveVariable.Enabled = false;
                    btnCancelVariable.Enabled = false;
                    lblStatus.Text = "Переменная изменена";
                }   
            }
        }   

        private void cmbVarDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!btnSaveVariable.Enabled)
            {
                btnSaveVariable.Enabled = true;
                btnCancelVariable.Enabled = true;
            }
            int varind = kbase.vars.FindIndex(delegate(Variable v) { return v.name == lbxVariables.SelectedItem.ToString(); });


            if (kbase.vars[varind].domain.name != cmbVarDomain.SelectedItem.ToString())
            {
                lbxVarDomain.Items.Clear();
                foreach (string s in kbase.domains[kbase.domains.FindIndex(delegate(Domain d) { return d.name == cmbVarDomain.SelectedItem.ToString(); })].values)
                {
                    lbxVarDomain.Items.Add(s);
                }
            }
        }  

        private void tbQuestion_TextChanged(object sender, EventArgs e)
        {
            btnSaveVariable.Enabled = true;
            btnCancelVariable.Enabled = true;
        }

        private void rbQ_CheckedChanged(object sender, EventArgs e)
        {
            btnSaveVariable.Enabled = true;
            btnCancelVariable.Enabled = true;
            if (rbQ.Checked == true)
                gbQuest.Visible = true;
        }

        private void rbD_CheckedChanged(object sender, EventArgs e)
        {
            btnSaveVariable.Enabled = true;
            btnCancelVariable.Enabled = true;
            if (rbQ.Checked == true)
                gbQuest.Visible = false;
        }

        private void rbQD_CheckedChanged(object sender, EventArgs e)
        {
            btnSaveVariable.Enabled = true;
            btnCancelVariable.Enabled = true;
            if (rbQD.Checked == true)
                gbQuest.Visible = true;
        }

        #endregion

        #region RULES
        
        private void btnAddRule_Click(object sender, EventArgs e)
        {
            int ind = lbxRules.SelectedIndex;
            //lbxRules.Items.Insert(ind + 1, "RN : IF THEN ");
            //Rule r = new Rule("RULENAME", "REASON", 1);
           fAddRule form = new fAddRule(this, ind + 1);
           form.ShowDialog();
            //lbxRules.SelectedIndex = ind + 1;
        }

        private void btnChangeRule_Click(object sender, EventArgs e)
        {
            int ind = lbxRules.SelectedIndex;
            string rname = lbxRules.Items[ind].ToString().Split(':')[0].Trim();
            Rule ChRule = kbase.rules.Find(delegate(Rule r) { return r.name == rname; });
            fAddRule form = new fAddRule(this, ChRule, ind);
            form.ShowDialog();
        }

        #endregion

        #region DRAGNDROP
        int startIndexd = -1;
        private void lbxValues_MouseUp(object sender, MouseEventArgs e)
        {
            startIndexd = -1;
        }

        private void lbxValues_MouseDown(object sender, MouseEventArgs e)
        {
            startIndexd = lbxValues.SelectedIndex;
        }

        private void lbxValues_MouseMove(object sender, MouseEventArgs e)
        {
            if (startIndexd != -1)
            {
                if (startIndexd != lbxValues.SelectedIndex)
                {
                    int endIndex = lbxValues.SelectedIndex;
                    if (startIndexd > endIndex)
                    {
                        lbxValues.Items.Insert(endIndex, lbxValues.Items[startIndexd]);
                        lbxValues.Items.RemoveAt(startIndexd + 1);
                        //теперь то же самое со списком правил
                        int domainindex = kbase.domains.FindIndex(delegate(Domain d) { return d.name == lbxDomains.SelectedItem.ToString(); });
                        kbase.domains[domainindex].values.Insert(endIndex, kbase.domains[domainindex].values[startIndexd]);
                        //kbase.rules.Insert(endIndex, kbase.rules[startIndex]);
                        kbase.domains[domainindex].values.RemoveAt(startIndexd + 1);
                        //kbase.rules.RemoveAt(startIndex + 1);
                    }
                    else
                    {
                        int domainindex = -1;
                        if (endIndex != lbxValues.Items.Count - 1)
                        {
                            lbxValues.Items.Insert(endIndex + 1, lbxValues.Items[startIndexd]);
                            domainindex = kbase.domains.FindIndex(delegate(Domain d) { return d.name == lbxDomains.SelectedItem.ToString(); });
                            kbase.domains[domainindex].values.Insert(endIndex + 1, kbase.domains[domainindex].values[startIndexd]);

                            // kbase.rules.Insert(endIndex + 1, kbase.rules[startIndexd]);
                        }
                        else
                        {
                            lbxValues.Items.Add(lbxValues.Items[startIndexd]);
                            domainindex = kbase.domains.FindIndex(delegate(Domain d) { return d.name == lbxDomains.SelectedItem.ToString(); });
                            kbase.domains[domainindex].values.Add(kbase.domains[domainindex].values[startIndexd]);
                            //kbase.rules.Add(kbase.rules[startIndex]);
                        }
                        lbxValues.Items.RemoveAt(startIndexd);
                        domainindex = kbase.domains.FindIndex(delegate(Domain d) { return d.name == lbxDomains.SelectedItem.ToString(); });
                        kbase.domains[domainindex].values.RemoveAt(startIndexd);
                        //kbase.rules.RemoveAt(startIndex);
                    }
                    startIndexd = endIndex;
                }
            }
        }
        #endregion

        private void btnDeleteRule_Click(object sender, EventArgs e)
        {
            int ind = lbxRules.SelectedIndex;
            if (lbxRules.SelectedIndex >= 0)
            {
                string rname = lbxRules.Items[ind].ToString().Split(':')[0].Trim();
                Rule ChRule = kbase.rules.Find(delegate(Rule r) { return r.name == rname; });
                lbxRules.Items.RemoveAt(ind);
                int indRule = kbase.rules.FindIndex(delegate(Rule r) { return r.name == rname; });
                kbase.rules.RemoveAt(indRule);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void начатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (kbase.rules.Count > 0)
            {
                ChooseGoal frm_vars = new ChooseGoal(this);
                frm_vars.ShowDialog();
            }
            else
            {
                MessageBox.Show("Список правил пуст");
            }
        }

        int startIndex = -1; 
        private void lbxRules_MouseDown(object sender, MouseEventArgs e)
        {
            startIndex = lbxRules.SelectedIndex;
        }

        private void lbxRules_MouseUp(object sender, MouseEventArgs e)
        {
            startIndex = -1;
        }

        private void lbxRules_MouseMove(object sender, MouseEventArgs e)
        {
            if (startIndex != -1)
            {
                if (startIndex != lbxRules.SelectedIndex)
                {
                    int endIndex = lbxRules.SelectedIndex;
                    if (startIndex > endIndex)
                    {
                        lbxRules.Items.Insert(endIndex, lbxRules.Items[startIndex]);
                        lbxRules.Items.RemoveAt(startIndex + 1);
                        //теперь то же самое со списком правил
                        kbase.rules.Insert(endIndex, kbase.rules[startIndex]);
                        kbase.rules.RemoveAt(startIndex + 1);
                    }
                    else
                    {
                        if (endIndex != lbxRules.Items.Count - 1)
                        {
                            lbxRules.Items.Insert(endIndex + 1, lbxRules.Items[startIndex]);
                            kbase.rules.Insert(endIndex + 1, kbase.rules[startIndex]);
                        }
                        else
                        {
                            lbxRules.Items.Add(lbxRules.Items[startIndex]);
                            kbase.rules.Add(kbase.rules[startIndex]);
                        }
                        lbxRules.Items.RemoveAt(startIndex);
                        kbase.rules.RemoveAt(startIndex);
                    }
                    startIndex = endIndex;
                }
            }
        }

        

        
    }
}
