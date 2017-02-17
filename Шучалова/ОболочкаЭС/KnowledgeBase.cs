using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ОболочкаЭС
{
    [Serializable()]
    public class KnowledgeBase : System.ComponentModel.INotifyPropertyChanged
    {
        [field: NonSerialized()]
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public string Name = "";
        public List<Domain> domains;
        public List<Variable> vars;
        public List<Rule> rules;
        int idRule, idVar, idFact;

        public KnowledgeBase(string name)
        {
            this.Name = name;

            rules = new List<Rule>();
            domains = new List<Domain>();
            vars = new List<Variable>();

            idFact = 0;
            idRule = 0;
            idVar = 0;
        }

        #region METHODS

        #region FILL DATA

        public void FillForms(MainForm form)
        {
            form.lbxDomains.Items.Clear();
            form.lbxValues.Items.Clear();
            form.lbxVariables.Items.Clear();
            //form.lbxIF.Items.Clear();
            //form.lbxThen.Items.Clear();
            form.lbxRules.Items.Clear();
            form.cmbVarDomain.Items.Clear();

            form.tbDomainName.Text = "";
            form.tbQuestion.Text = "";
            //form.tbReasoning.Text = "";
            //form.tbRuleName.Text = "";
            form.tbVarName.Text = "";

            form.gbQuest.Visible = false;

            foreach (Domain d in domains)
            {
                form.lbxDomains.Items.Add(d.name);
                form.cmbVarDomain.Items.Add(d.name);
            }

            foreach (Variable v in vars)
            {
                form.lbxVariables.Items.Add(v.name);
            }
            FillRules(form);

            if (form.lbxVariables.SelectedIndex > 0) form.lbxVariables.SelectedIndex = 0;
            if (form.lbxRules.Items.Count > 0) form.lbxRules.SelectedIndex = 0;
            form.lbxDomains.SelectedIndex = 0;
            //form.btnSaveRule.Enabled = false;
        }

        public void FillRules(MainForm form)
        {
            foreach (Rule r in rules)
            {
                string rulestring = "";
                rulestring += r.name + " : IF ";
                foreach (Fact f in r.sendFact)
                {
                    if (r.sendFact.IndexOf(f) > 0) rulestring += " AND ";
                    rulestring += "('" + f.word1 + "' = '" + f.word2 + "')";
                }
                rulestring += " THEN ";
                foreach (Fact f in r.concFact)
                {
                    if (r.concFact.IndexOf(f) > 0) rulestring += " AND ";
                    rulestring += " '" + f.word1 + "' = '" + f.word2 + "'";
                }
                form.lbxRules.Items.Add(rulestring);
            }
        }

        #endregion

        #region DOMAINS

        public void addDomain(string name)
        {
            domains.Add(new Domain(name.ToUpper()));
        }

        public void addDomenValues(string name, string v)
        {
            domains[domains.FindIndex(delegate(Domain d) { return d.name == name.ToUpper(); })].addValue(v.ToUpper());
        }
        public void addDomenValues(string name, List<string> ds)
        {
            Domain dnew = new Domain(name.ToUpper());
            dnew.values = ds;
            domains[domains.FindIndex(delegate(Domain d) { return d.name == name.ToUpper(); })] = dnew;
        }

        #endregion

        #region VARIABLES

        public int addVar(string name)
        {
            vars.Add(new Variable(name, new Domain("temp"), "", varType.none, idVar, 0));
            return (idVar++);
        }

        public Variable GetVarByName(string name)
        {
            return vars.Find(delegate(Variable v)
            {
                return v.name == name;
            }
            );
        }

        public int GetVarIDByName(string name)
        {
            return vars.FindIndex(delegate(Variable v)
            {
                return v.name == name;
            }
            );
        }

        #endregion



        #endregion

        #region MLV

        public List<VarsWithValue> valuableVariable;
        public List<Rule> workedRules;

        public int Consult(Variable var, ConsultResults cRez)
        {
            valuableVariable = new List<VarsWithValue>();
            workedRules = new List<Rule>();
            cRez.tview_steps.Nodes.Add("Начинаем консультацию");
            if (ConcGoal(var, cRez.tview_steps.Nodes[0]) == "Прервано") return -1;
            else return 0;
        }

        public string ConcGoal(Variable var, TreeNode tn)
        {
            int indexRules = 0;
            string goal = "";
            while (goal == "" && indexRules < rules.Count)
            {
                bool concHasVar = false, good = true;
                foreach (Fact f in rules[indexRules].concFact)
                {
                    if (f.word1 == var.name && !workedRules.Contains(rules[indexRules]))
                        concHasVar = true;
                }
                if (concHasVar && !workedRules.Contains(rules[indexRules]))
                {
                    tn.Nodes.Add(GetStringForRule(rules[indexRules]));
                    foreach (Fact f in rules[indexRules].sendFact)
                    {
                        if (good)
                        {
                            TreeNode tn1 = tn.Nodes[tn.Nodes.Count - 1];
                            tn1.Nodes.Add("Выводим " + f.word1);
                            if (hasVarValue(f.word1))
                            {
                                TreeNode tn2 = tn1.Nodes[tn1.Nodes.Count - 1];
                                VarsWithValue vWv = valuableVariable.Find(delegate(VarsWithValue v) { return v.var.name == f.word1; });
                                if (vWv.value != f.word2) good = false;
                                tn2.Nodes.Add("Уже означено " + vWv.var.name + " = " + vWv.value);
                            }
                            else
                            {
                                Variable sendVar = vars.Find(delegate(Variable v) { return v.name == f.word1; });
                                if (sendVar.type == varType.query)
                                {
                                    TreeNode tn2 = tn1.Nodes[tn1.Nodes.Count - 1];
                                    int count = valuableVariable.Count;
                                    if (AskVar(sendVar) == -1)
                                    {
                                        goal = "Прервано";
                                        good = false;
                                    }
                                    else
                                    {
                                        VarsWithValue vWv = valuableVariable.Find(delegate(VarsWithValue v) { return v.var.name == f.word1; });
                                        if (vWv.value != f.word2) good = false;
                                        tn2.Nodes.Add("Пользователь ввел " + vWv.var.name + " = " + vWv.value);
                                    }
                                }
                                else if (sendVar.type == varType.queryDeduction)
                                {
                                    Variable varDed = vars.Find(delegate(Variable v2) { return v2.name == f.word1; });
                                    TreeNode tn2 = tn1.Nodes[tn1.Nodes.Count - 1];
                                    if (ConcGoal(varDed, tn2) == "")
                                    {
                                        AskVar(sendVar);
                                        tn2 = tn1.Nodes[tn1.Nodes.Count - 1];
                                        VarsWithValue vWv = valuableVariable.Find(delegate(VarsWithValue v) { return v.var.name == f.word1; });
                                        if (vWv.value != f.word2) good = false;
                                        tn2.Nodes.Add("Пользователь ввел " + vWv.var.name + " = " + vWv.value);
                                    }
                                    else
                                    {
                                        int indvar = valuableVariable.FindIndex(delegate(VarsWithValue v) { return v.var.name == varDed.name; });
                                        if (valuableVariable[indvar].value != f.word2)
                                        {
                                            good = false;
                                        }
                                    }
                                }
                                else
                                {
                                    Variable varDed = vars.Find(delegate(Variable v2) { return v2.name == f.word1; });
                                    TreeNode tn2 = tn1.Nodes[tn1.Nodes.Count - 1];
                                    if (ConcGoal(varDed, tn2) == "") good = false;
                                    else
                                    {
                                        int indvar = valuableVariable.FindIndex(delegate(VarsWithValue v) { return v.var.name == varDed.name; });
                                        if (valuableVariable[indvar].value != f.word2)
                                        {
                                            good = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (good)
                    {
                        TreeNode tn1 = tn.Nodes[tn.Nodes.Count - 1];
                        tn1.Nodes.Add("Правило сработало");
                        foreach (Fact f in rules[indexRules].concFact)
                        {
                            Variable varConc = vars.Find(delegate(Variable v2) { return v2.name == f.word1; });
                            VarsWithValue v = new VarsWithValue(varConc, f.word2);
                            if (valuableVariable.FindIndex(delegate(VarsWithValue v3) { return v3.var.name == f.word1; }) == -1)
                            {
                                valuableVariable.Add(v);
                                tn.Nodes.Add("Означили : " + v.var.name + " = " + v.value);
                            }
                        }
                        tn1.Nodes.Add(rules[indexRules].reason);
                        VarsWithValue vWv = valuableVariable.Find(delegate(VarsWithValue v) { return v.var.name == var.name; });
                        goal = vWv.value;
                        workedRules.Add(rules[indexRules]);
                    }
                    else
                    {
                        TreeNode tn1 = tn.Nodes[tn.Nodes.Count - 1];
                        tn1.Nodes.Add("Правило не сработало");
                    }
                }
                indexRules++;
            }
            return goal;
        }

        public int AskVar(Variable var)
        {
            QuestionForm frm = new QuestionForm(this, var);
            if (frm.ShowDialog() == DialogResult.Cancel) return -1; else return 0;
        }

        public string GetStringForRule(Rule r)
        {
            string rulestring = "";
            rulestring += r.name + " : IF ";
            foreach (Fact f in r.sendFact)
            {
                if (r.sendFact.IndexOf(f) > 0) rulestring += " AND ";
                rulestring += "('" + f.word1 + "' = '" + f.word2 + "')";
            }
            rulestring += " THEN ";
            foreach (Fact f in r.concFact)
            {
                if (r.concFact.IndexOf(f) > 0) rulestring += " AND ";
                rulestring += " '" + f.word1 + "' = '" + f.word2 + "'";
            }
            return rulestring;
        }

        public bool hasVarValue(string var)
        {
            int ind = valuableVariable.FindIndex(delegate(VarsWithValue v) { return v.var.name == var; });
            if (ind >= 0) return true;
            else return false;
        }

        #endregion
    }

    #region STRUCTURES
    public enum formFactType
    {
        addIF, changeIF, addThen, changeThen
    }
    public enum formType
    {
        addDomain, changeDomain, addValue, changeValue, addVariable, changeVariable
    }
    public enum varType
    {
        none, query, deduction, queryDeduction 
    }
     [Serializable()]
    public struct Variable
    {
        public string name, question;
        public Domain domain;
        public int id, answerCount;
        public varType type;
        public Variable(string name, Domain domain, string question, varType type, int id, int answerCount)
        {
            this.name = name;
            this.domain = domain;
            this.question = question;
            this.type = type;
            this.id = id;
            this.answerCount = answerCount;
        }
    }

     [Serializable()]
     public struct Fact
     {
         //храним только название переменной и значение
         public string word1, word2;
         public Fact(string word1, string word2)
         {
             this.word1 = word1;
             this.word2 = word2;
         }
     }

     [Serializable]
     public struct VarsWithValue
     {
         public Variable var;
         public string value;
         public VarsWithValue(Variable var, string value)
         {
             this.var = var;
             this.value = value;
         }
     }

     [Serializable()]
     public struct Domain
     {
         public string name;
         public List<string> values;
         public Domain(string name)
         {
             this.name = name;
             values = new List<string>();
         }
         public void addValue(string v)
         {
             values.Add(v);
         }
     }

     [Serializable()]
     public struct Rule
     {
         public string name, reason;
         int id;
         public List<Fact> sendFact, concFact;
         public Rule(string name, string reason, int id)
         {
             this.name = name;
             this.sendFact = new List<Fact>();
             this.concFact = new List<Fact>();
             this.reason = reason;
             this.id = id;
         }
         public void addFactInRule(Fact f1)
         {
             sendFact.Add(f1);
         }
         public void addConcInRule(Fact f1)
         {
             concFact.Add(f1);
         }
     }
    #endregion

     
}
