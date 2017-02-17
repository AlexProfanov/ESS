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
    public partial class ChooseGoal : Form
    {
        MainForm frm;
        public ChooseGoal(MainForm frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void OKbtn_Click(object sender, EventArgs e)
        {
            Variable var = frm.kbase.vars.Find(delegate(Variable v) { return v.name == cmbVar.SelectedItem.ToString(); });
            ConsultResults formRez = new ConsultResults();
            if (frm.kbase.Consult(var, formRez) == -1)
            {
                formRez.tview_steps.Nodes.Add("Консультация была прервана");
            }
            this.Close();

            formRez.FillForms(frm.kbase, var);

            formRez.ShowDialog();
        }

        private void ChooseGoal_Load(object sender, EventArgs e)
        {
            foreach (Variable v in frm.kbase.vars)
            {
                if (v.type == varType.deduction || v.type == varType.queryDeduction)
                {
                    cmbVar.Items.Add(v.name);
                }
            }
            if (frm.kbase.vars.Count > 0)
            {
                cmbVar.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Список выводимых переменных пуст");
                this.Close();
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
