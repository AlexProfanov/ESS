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
    public partial class AllStructRename : Form
    {
        MainForm form;
        public formType formtype;
        public string old;
        bool NoMore;

        public AllStructRename(MainForm frm)
        {
            InitializeComponent();
            form = frm;
            textBox1.Select();
            NoMore = false;
        }



        public void SetText(formType formtype)
        {
            switch (formtype)
            {
                case formType.addDomain: this.Text = "Создать домен";
                    label1.Text = "Введите имя домена";
                    textBox1.Text = "";
                    btnOK.Text = "Добавить";
                    btnMoreok.Text = "Добавить следующий";
                    btnCancel.Text = "Отмена";
                    break;
                case formType.changeDomain: this.Text = "Изменить домен";
                    label1.Text = "Введите новое имя домена";
                    textBox1.Text = old;
                    btnOK.Text = "Добавить";
                    btnMoreok.Visible = false;
                    btnCancel.Text = "Отмена";
                    break;
                case formType.addVariable: this.Text = "Создать переменную";
                    label1.Text = "Введите имя переменной";
                    textBox1.Text = "";
                    btnOK.Text = "Добавить";
                    btnMoreok.Text = "Добавить следующую";
                    btnCancel.Text = "Отмена";
                    break;
                case formType.changeVariable: this.Text = "Изменить переменную";
                    label1.Text = "Введите новое имя переменной";
                    textBox1.Text = old;
                    btnOK.Text = "Добавить";
                    btnMoreok.Visible = false;
                    btnCancel.Text = "Отмена";
                    break;
                case formType.addValue: this.Text = "Создать значение";
                    label1.Text = "Введите значение";
                    textBox1.Text = "";
                    btnOK.Text = "Добавить";
                    btnMoreok.Text = "Добавить следующее";
                    btnCancel.Text = "Отмена";
                    break;
                case formType.changeValue: this.Text = "Изменить значение";
                    label1.Text = "Введите новое значение";
                    textBox1.Text = old;
                    btnOK.Text = "Добавить";
                    btnMoreok.Visible = false;
                    btnCancel.Text = "Отмена";
                    break;
            }
        }

        bool canClose = true;

        private void btnOK_Click(object sender, EventArgs e)
        {
            canClose = true;
            NoMore = true;
            btnMoreok_Click(sender, e);
            if (canClose) this.Close();
        }

        private void btnMoreok_Click(object sender, EventArgs e)
        {
            switch (formtype)
            {
                #region добавление домена
                case formType.addDomain :
                    if (!form.lbxDomains.Items.Contains(textBox1.Text.Trim().ToUpper()))
                    {
                        form.kbase.addDomain(textBox1.Text.Trim().ToUpper());
                        form.lbxDomains.Items.Add(textBox1.Text.Trim().ToUpper());
                        form.cmbVarDomain.Items.Add(textBox1.Text.Trim().ToUpper());
                        this.textBox1.Text = "";
                        textBox1.Select();
                        if (NoMore)
                            this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Имя домена уже существует");
                        textBox1.Select();
                        canClose = false;
                    }
                    break;
                #endregion
                #region переименование домена
                case formType.changeDomain :
                    if (!form.lbxDomains.Items.Contains(textBox1.Text.Trim().ToUpper()))
                    {
                        int ind = form.kbase.domains.FindIndex(delegate(Domain d) { return d.name == old; });
                        Domain oldDomain = form.kbase.domains[ind];
                        string oldname = oldDomain.name;
                        oldDomain.name = textBox1.Text.Trim().ToUpper();
                        form.kbase.domains[ind] = oldDomain;
                        ind = form.lbxDomains.Items.IndexOf(old);
                        form.lbxDomains.Items[ind] = textBox1.Text.Trim().ToUpper();
                        int indVar;
                        int indCmbbox = form.cmbVarDomain.Items.IndexOf(oldname);
                        form.cmbVarDomain.Items[indCmbbox] = textBox1.Text.Trim().ToUpper();
                        while ((indVar = form.kbase.vars.FindIndex(delegate(Variable v) { return (v.domain.name.ToUpper() == oldname.ToUpper()); })) != -1)
                        {
                            Variable var1 = form.kbase.vars[indVar];
                            var1.domain = oldDomain;
                            form.kbase.vars[indVar] = var1;
                            form.lbxDomains.SelectedIndex = ind;
                        }
                        if (NoMore) 
                            this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Имя домена уже существует");
                        textBox1.Select();
                        canClose = false;
                    }
                    break;
                #endregion
                #region добавление значения
                case formType.addValue :
                    if (!form.lbxValues.Items.Contains(textBox1.Text.Trim().ToUpper()))
                    {
                        int ind = form.kbase.domains.FindIndex(delegate(Domain d) { return d.name == form.lbxDomains.SelectedItem.ToString(); });
                        form.kbase.domains[ind].addValue(textBox1.Text.Trim().ToUpper());
                        form.lbxValues.Items.Add(textBox1.Text.Trim().ToUpper());
                        textBox1.Text = "";
                        textBox1.Select();
                        if (NoMore) this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Значение уже существует");
                        textBox1.Select();
                        canClose = false;
                    }
                    break;
                #endregion
                #region изменение значения
                case formType.changeValue:
                    if (!form.lbxValues.Items.Contains(textBox1.Text.Trim().ToUpper()))
                    {
                        int indDomain = form.kbase.domains.FindIndex(delegate(Domain d) { return d.name == form.lbxDomains.SelectedItem.ToString().ToUpper(); });
                        int indValue = form.kbase.domains[indDomain].values.FindIndex(delegate(string d) { return d == old; });
                        form.kbase.domains[indDomain].values[indValue] = textBox1.Text.Trim().ToUpper();
                        indValue = form.lbxValues.Items.IndexOf(old.ToUpper());
                        form.lbxValues.Items[indValue] = textBox1.Text.Trim().ToUpper();
                        form.lbxValues.SelectedIndex = indValue;

                        for (int irule = 0; irule < form.kbase.rules.Count; irule++)
                        {
                            for (int ifact = 0; ifact < form.kbase.rules[irule].sendFact.Count; ifact++)
                            {
                                if (form.kbase.rules[irule].sendFact[ifact].word2.ToUpper() == old.ToUpper())
                                {
                                    Fact f = form.kbase.rules[irule].sendFact[ifact];
                                    f.word2 = textBox1.Text.Trim().ToUpper();
                                    form.kbase.rules[irule].sendFact[ifact] = f;
                                }
                            }

                            for (int ifact = 0; ifact < form.kbase.rules[irule].concFact.Count; ifact++)
                            {
                                if (form.kbase.rules[irule].concFact[ifact].word2.ToUpper() == old.ToUpper())
                                {
                                    Fact f = form.kbase.rules[irule].concFact[ifact];
                                    f.word2 = textBox1.Text.Trim().ToUpper();
                                    form.kbase.rules[irule].concFact[ifact] = f;
                                }
                            }
                        }

                        form.lbxRules.Items.Clear();
                        form.kbase.FillRules(form);
                        textBox1.Text = "";
                        textBox1.Select();
                        if (NoMore) this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Значение уже существует");
                        textBox1.Select();
                        canClose = false;
                    }
                    break;
                #endregion
                #region добавление переменной
                case formType.addVariable:
                    if (!form.lbxVariables.Items.Contains(textBox1.Text.Trim().ToUpper()))
                    {
                        form.kbase.addVar(textBox1.Text.Trim().ToUpper());
                        Variable v = form.kbase.GetVarByName(textBox1.Text);
                        v.domain = form.kbase.domains[0];
                        v.question = v.name.ToUpper() + "?";
                        v.type = varType.query;

                        int ind = form.kbase.GetVarIDByName(textBox1.Text.Trim().ToUpper());
                        form.kbase.vars[ind] = v;
                        form.lbxVariables.Items.Add(textBox1.Text.Trim().ToUpper());
                        form.lbxVariables.SelectedIndex = form.lbxVariables.Items.Count - 1;

                        this.textBox1.Text = "";
                        textBox1.Select();
                        if (NoMore) this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Такое имя переменной уже существует");
                        textBox1.Select();
                        canClose = false;
                    }
                    break;
                #endregion
                #region изменение переменной
                case formType.changeVariable:
                    if (!form.lbxVariables.Items.Contains(textBox1.Text.Trim().ToUpper()))
                    {
                        int indVar = form.kbase.vars.FindIndex(delegate(Variable v) { return v.name == form.lbxVariables.SelectedItem.ToString(); });
                        Variable var = form.kbase.vars[indVar];
                        string oldnamevar = var.name;
                        var.name = textBox1.Text.Trim().ToUpper();
                        form.kbase.vars[indVar] = var;
                        int lbxIndVar = form.lbxVariables.Items.IndexOf(oldnamevar);
                        form.lbxVariables.Items[lbxIndVar] = textBox1.Text.Trim().ToUpper();
                        form.lbxVariables.SelectedIndex = lbxIndVar;
                        form.tbVarName.Text = textBox1.Text.Trim().ToUpper();
                        if (var.question == oldnamevar + "?")
                        {
                            var.question = textBox1.Text.Trim().ToUpper() + "?";
                            form.tbQuestion.Text = var.question;
                        }
                        for (int irule = 0; irule < form.kbase.rules.Count; irule++)
                        {
                            for (int ifact = 0; ifact < form.kbase.rules[irule].sendFact.Count; ifact++)
                            {
                                if (form.kbase.rules[irule].sendFact[ifact].word1.ToUpper() == old.ToUpper())
                                {
                                    Fact f = form.kbase.rules[irule].sendFact[ifact];
                                    f.word1 = textBox1.Text.Trim().ToUpper();
                                    form.kbase.rules[irule].sendFact[ifact] = f;
                                }
                            }

                            for (int ifact = 0; ifact < form.kbase.rules[irule].concFact.Count; ifact++)
                            {
                                if (form.kbase.rules[irule].concFact[ifact].word1.ToUpper() == old.ToUpper())
                                {
                                    Fact f = form.kbase.rules[irule].concFact[ifact];
                                    f.word1 = textBox1.Text.Trim().ToUpper();
                                    form.kbase.rules[irule].concFact[ifact] = f;
                                }
                            }
                        }

                        form.lbxRules.Items.Clear();
                        form.kbase.FillRules(form);

                        this.textBox1.Text = "";
                        textBox1.Select();
                        if (NoMore) this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Такое имя переменной уже существует");
                        textBox1.Select();
                        canClose = false;
                    }
                    break;
                #endregion
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                btnOK.Enabled = false;
                btnMoreok.Enabled = false;
            }
            else
            {
                btnOK.Enabled = true;
                btnMoreok.Enabled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
