namespace ОболочкаЭС
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.консультацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.начатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbxDomains = new System.Windows.Forms.ListBox();
            this.gbDomain = new System.Windows.Forms.GroupBox();
            this.lbxValues = new System.Windows.Forms.ListBox();
            this.btnDeleteValue = new System.Windows.Forms.Button();
            this.btnChangeDomain = new System.Windows.Forms.Button();
            this.btnChangeValue = new System.Windows.Forms.Button();
            this.btnAddValue = new System.Windows.Forms.Button();
            this.tbDomainName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteDomain = new System.Windows.Forms.Button();
            this.btnAddDomain = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbxVariables = new System.Windows.Forms.ListBox();
            this.btnDeleteVariable = new System.Windows.Forms.Button();
            this.btnAddVariable = new System.Windows.Forms.Button();
            this.gbVar = new System.Windows.Forms.GroupBox();
            this.btnChangeVariable = new System.Windows.Forms.Button();
            this.gbQuest = new System.Windows.Forms.GroupBox();
            this.tbQuestion = new System.Windows.Forms.TextBox();
            this.rbQD = new System.Windows.Forms.RadioButton();
            this.rbD = new System.Windows.Forms.RadioButton();
            this.rbQ = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbVarDomain = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancelVariable = new System.Windows.Forms.Button();
            this.btnSaveVariable = new System.Windows.Forms.Button();
            this.tbVarName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lbxRules = new System.Windows.Forms.ListBox();
            this.btnDeleteRule = new System.Windows.Forms.Button();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lbxVarDomain = new System.Windows.Forms.ListBox();
            this.btnChangeRule = new System.Windows.Forms.Button();
            this.menuStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbDomain.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbVar.SuspendLayout();
            this.gbQuest.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.консультацияToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(742, 24);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.новыйToolStripMenuItem.Text = "Новый";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.новыйToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.открытьToolStripMenuItem.Text = "Открыть...";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // консультацияToolStripMenuItem
            // 
            this.консультацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.начатьToolStripMenuItem});
            this.консультацияToolStripMenuItem.Name = "консультацияToolStripMenuItem";
            this.консультацияToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.консультацияToolStripMenuItem.Text = "Консультация";
            // 
            // начатьToolStripMenuItem
            // 
            this.начатьToolStripMenuItem.Name = "начатьToolStripMenuItem";
            this.начатьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.начатьToolStripMenuItem.Text = "Начать";
            this.начатьToolStripMenuItem.Click += new System.EventHandler(this.начатьToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(741, 390);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbxDomains);
            this.tabPage1.Controls.Add(this.gbDomain);
            this.tabPage1.Controls.Add(this.btnDeleteDomain);
            this.tabPage1.Controls.Add(this.btnAddDomain);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(733, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Домены";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbxDomains
            // 
            this.lbxDomains.FormattingEnabled = true;
            this.lbxDomains.Location = new System.Drawing.Point(8, 6);
            this.lbxDomains.Name = "lbxDomains";
            this.lbxDomains.Size = new System.Drawing.Size(296, 316);
            this.lbxDomains.TabIndex = 17;
            this.lbxDomains.SelectedIndexChanged += new System.EventHandler(this.lbxDomains_SelectedIndexChanged);
            // 
            // gbDomain
            // 
            this.gbDomain.Controls.Add(this.lbxValues);
            this.gbDomain.Controls.Add(this.btnDeleteValue);
            this.gbDomain.Controls.Add(this.btnChangeDomain);
            this.gbDomain.Controls.Add(this.btnChangeValue);
            this.gbDomain.Controls.Add(this.btnAddValue);
            this.gbDomain.Controls.Add(this.tbDomainName);
            this.gbDomain.Controls.Add(this.label1);
            this.gbDomain.Location = new System.Drawing.Point(310, 6);
            this.gbDomain.Name = "gbDomain";
            this.gbDomain.Size = new System.Drawing.Size(416, 351);
            this.gbDomain.TabIndex = 7;
            this.gbDomain.TabStop = false;
            this.gbDomain.Text = "Параметры домена";
            // 
            // lbxValues
            // 
            this.lbxValues.FormattingEnabled = true;
            this.lbxValues.Location = new System.Drawing.Point(17, 52);
            this.lbxValues.Name = "lbxValues";
            this.lbxValues.Size = new System.Drawing.Size(313, 186);
            this.lbxValues.TabIndex = 17;
            this.lbxValues.SelectedIndexChanged += new System.EventHandler(this.lbxValues_SelectedIndexChanged);
            this.lbxValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbxValues_MouseDown);
            this.lbxValues.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbxValues_MouseMove);
            this.lbxValues.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbxValues_MouseUp);
            // 
            // btnDeleteValue
            // 
            this.btnDeleteValue.Location = new System.Drawing.Point(101, 309);
            this.btnDeleteValue.Name = "btnDeleteValue";
            this.btnDeleteValue.Size = new System.Drawing.Size(147, 23);
            this.btnDeleteValue.TabIndex = 9;
            this.btnDeleteValue.Text = "Удалить значение";
            this.btnDeleteValue.UseVisualStyleBackColor = true;
            this.btnDeleteValue.Click += new System.EventHandler(this.btnDeleteValue_Click);
            // 
            // btnChangeDomain
            // 
            this.btnChangeDomain.Location = new System.Drawing.Point(310, 16);
            this.btnChangeDomain.Name = "btnChangeDomain";
            this.btnChangeDomain.Size = new System.Drawing.Size(100, 23);
            this.btnChangeDomain.TabIndex = 16;
            this.btnChangeDomain.Text = "Переименовать";
            this.btnChangeDomain.UseVisualStyleBackColor = true;
            this.btnChangeDomain.Click += new System.EventHandler(this.btnChangeDomain_Click);
            // 
            // btnChangeValue
            // 
            this.btnChangeValue.Location = new System.Drawing.Point(101, 280);
            this.btnChangeValue.Name = "btnChangeValue";
            this.btnChangeValue.Size = new System.Drawing.Size(147, 23);
            this.btnChangeValue.TabIndex = 8;
            this.btnChangeValue.Text = "Редактировать значение";
            this.btnChangeValue.UseVisualStyleBackColor = true;
            this.btnChangeValue.Click += new System.EventHandler(this.btnChangeValue_Click);
            // 
            // btnAddValue
            // 
            this.btnAddValue.Location = new System.Drawing.Point(101, 251);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new System.Drawing.Size(147, 23);
            this.btnAddValue.TabIndex = 7;
            this.btnAddValue.Text = "Добавить значение";
            this.btnAddValue.UseVisualStyleBackColor = true;
            this.btnAddValue.Click += new System.EventHandler(this.btnAddValue_Click);
            // 
            // tbDomainName
            // 
            this.tbDomainName.Location = new System.Drawing.Point(101, 19);
            this.tbDomainName.Name = "tbDomainName";
            this.tbDomainName.ReadOnly = true;
            this.tbDomainName.Size = new System.Drawing.Size(193, 20);
            this.tbDomainName.TabIndex = 1;
            this.tbDomainName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDomainName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя домена";
            // 
            // btnDeleteDomain
            // 
            this.btnDeleteDomain.Location = new System.Drawing.Point(184, 334);
            this.btnDeleteDomain.Name = "btnDeleteDomain";
            this.btnDeleteDomain.Size = new System.Drawing.Size(85, 23);
            this.btnDeleteDomain.TabIndex = 6;
            this.btnDeleteDomain.Text = "Удалить";
            this.btnDeleteDomain.UseVisualStyleBackColor = true;
            this.btnDeleteDomain.Click += new System.EventHandler(this.btnDeleteDomain_Click);
            // 
            // btnAddDomain
            // 
            this.btnAddDomain.Location = new System.Drawing.Point(58, 334);
            this.btnAddDomain.Name = "btnAddDomain";
            this.btnAddDomain.Size = new System.Drawing.Size(87, 23);
            this.btnAddDomain.TabIndex = 1;
            this.btnAddDomain.Text = "Добавить";
            this.btnAddDomain.UseVisualStyleBackColor = true;
            this.btnAddDomain.Click += new System.EventHandler(this.btnAddDomain_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbxVariables);
            this.tabPage2.Controls.Add(this.btnDeleteVariable);
            this.tabPage2.Controls.Add(this.btnAddVariable);
            this.tabPage2.Controls.Add(this.gbVar);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(733, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Переменные";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbxVariables
            // 
            this.lbxVariables.FormattingEnabled = true;
            this.lbxVariables.Location = new System.Drawing.Point(13, 9);
            this.lbxVariables.Name = "lbxVariables";
            this.lbxVariables.Size = new System.Drawing.Size(312, 316);
            this.lbxVariables.TabIndex = 10;
            this.lbxVariables.SelectedIndexChanged += new System.EventHandler(this.lbxVariables_SelectedIndexChanged);
            // 
            // btnDeleteVariable
            // 
            this.btnDeleteVariable.Location = new System.Drawing.Point(184, 335);
            this.btnDeleteVariable.Name = "btnDeleteVariable";
            this.btnDeleteVariable.Size = new System.Drawing.Size(114, 23);
            this.btnDeleteVariable.TabIndex = 9;
            this.btnDeleteVariable.Text = "Удалить";
            this.btnDeleteVariable.UseVisualStyleBackColor = true;
            this.btnDeleteVariable.Click += new System.EventHandler(this.btnDeleteVariable_Click);
            // 
            // btnAddVariable
            // 
            this.btnAddVariable.Location = new System.Drawing.Point(27, 335);
            this.btnAddVariable.Name = "btnAddVariable";
            this.btnAddVariable.Size = new System.Drawing.Size(126, 23);
            this.btnAddVariable.TabIndex = 8;
            this.btnAddVariable.Text = "Добавить";
            this.btnAddVariable.UseVisualStyleBackColor = true;
            this.btnAddVariable.Click += new System.EventHandler(this.btnAddVariable_Click);
            // 
            // gbVar
            // 
            this.gbVar.Controls.Add(this.lbxVarDomain);
            this.gbVar.Controls.Add(this.btnChangeVariable);
            this.gbVar.Controls.Add(this.gbQuest);
            this.gbVar.Controls.Add(this.rbQD);
            this.gbVar.Controls.Add(this.rbD);
            this.gbVar.Controls.Add(this.rbQ);
            this.gbVar.Controls.Add(this.label5);
            this.gbVar.Controls.Add(this.cmbVarDomain);
            this.gbVar.Controls.Add(this.label4);
            this.gbVar.Controls.Add(this.btnCancelVariable);
            this.gbVar.Controls.Add(this.btnSaveVariable);
            this.gbVar.Controls.Add(this.tbVarName);
            this.gbVar.Controls.Add(this.label3);
            this.gbVar.Location = new System.Drawing.Point(341, 6);
            this.gbVar.Name = "gbVar";
            this.gbVar.Size = new System.Drawing.Size(376, 351);
            this.gbVar.TabIndex = 7;
            this.gbVar.TabStop = false;
            this.gbVar.Text = "Параметры переменной";
            // 
            // btnChangeVariable
            // 
            this.btnChangeVariable.Location = new System.Drawing.Point(237, 24);
            this.btnChangeVariable.Name = "btnChangeVariable";
            this.btnChangeVariable.Size = new System.Drawing.Size(132, 23);
            this.btnChangeVariable.TabIndex = 15;
            this.btnChangeVariable.Text = "Переименовать";
            this.btnChangeVariable.UseVisualStyleBackColor = true;
            this.btnChangeVariable.Click += new System.EventHandler(this.btnChangeVariable_Click);
            // 
            // gbQuest
            // 
            this.gbQuest.Controls.Add(this.tbQuestion);
            this.gbQuest.Location = new System.Drawing.Point(20, 240);
            this.gbQuest.Name = "gbQuest";
            this.gbQuest.Size = new System.Drawing.Size(349, 76);
            this.gbQuest.TabIndex = 13;
            this.gbQuest.TabStop = false;
            this.gbQuest.Text = "Вопрос";
            this.toolTip1.SetToolTip(this.gbQuest, "Вопрос, который будет задан пользователю при запросе переменной");
            // 
            // tbQuestion
            // 
            this.tbQuestion.Location = new System.Drawing.Point(6, 19);
            this.tbQuestion.Multiline = true;
            this.tbQuestion.Name = "tbQuestion";
            this.tbQuestion.Size = new System.Drawing.Size(337, 51);
            this.tbQuestion.TabIndex = 0;
            this.tbQuestion.TextChanged += new System.EventHandler(this.tbQuestion_TextChanged);
            // 
            // rbQD
            // 
            this.rbQD.AutoSize = true;
            this.rbQD.Location = new System.Drawing.Point(122, 217);
            this.rbQD.Name = "rbQD";
            this.rbQD.Size = new System.Drawing.Size(163, 17);
            this.rbQD.TabIndex = 12;
            this.rbQD.TabStop = true;
            this.rbQD.Tag = "1";
            this.rbQD.Text = "Выводимо-запрашиваемая";
            this.rbQD.UseVisualStyleBackColor = true;
            this.rbQD.CheckedChanged += new System.EventHandler(this.rbQD_CheckedChanged);
            // 
            // rbD
            // 
            this.rbD.AutoSize = true;
            this.rbD.Location = new System.Drawing.Point(122, 194);
            this.rbD.Name = "rbD";
            this.rbD.Size = new System.Drawing.Size(84, 17);
            this.rbD.TabIndex = 11;
            this.rbD.TabStop = true;
            this.rbD.Tag = "1";
            this.rbD.Text = "Выводимая";
            this.rbD.UseVisualStyleBackColor = true;
            this.rbD.CheckedChanged += new System.EventHandler(this.rbD_CheckedChanged);
            // 
            // rbQ
            // 
            this.rbQ.AutoSize = true;
            this.rbQ.Location = new System.Drawing.Point(122, 171);
            this.rbQ.Name = "rbQ";
            this.rbQ.Size = new System.Drawing.Size(108, 17);
            this.rbQ.TabIndex = 10;
            this.rbQ.TabStop = true;
            this.rbQ.Tag = "1";
            this.rbQ.Text = "Запрашиваемая";
            this.rbQ.UseVisualStyleBackColor = true;
            this.rbQ.CheckedChanged += new System.EventHandler(this.rbQ_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Тип";
            // 
            // cmbVarDomain
            // 
            this.cmbVarDomain.FormattingEnabled = true;
            this.cmbVarDomain.Location = new System.Drawing.Point(106, 53);
            this.cmbVarDomain.Name = "cmbVarDomain";
            this.cmbVarDomain.Size = new System.Drawing.Size(197, 21);
            this.cmbVarDomain.TabIndex = 8;
            this.cmbVarDomain.SelectedIndexChanged += new System.EventHandler(this.cmbVarDomain_SelectedIndexChanged);
            this.cmbVarDomain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDomainName_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Домен";
            // 
            // btnCancelVariable
            // 
            this.btnCancelVariable.Location = new System.Drawing.Point(187, 322);
            this.btnCancelVariable.Name = "btnCancelVariable";
            this.btnCancelVariable.Size = new System.Drawing.Size(132, 23);
            this.btnCancelVariable.TabIndex = 6;
            this.btnCancelVariable.Text = "Отменить изменения";
            this.btnCancelVariable.UseVisualStyleBackColor = true;
            this.btnCancelVariable.Click += new System.EventHandler(this.lbxVariables_SelectedIndexChanged);
            // 
            // btnSaveVariable
            // 
            this.btnSaveVariable.Location = new System.Drawing.Point(35, 322);
            this.btnSaveVariable.Name = "btnSaveVariable";
            this.btnSaveVariable.Size = new System.Drawing.Size(132, 23);
            this.btnSaveVariable.TabIndex = 5;
            this.btnSaveVariable.Text = "Сохранить изменения";
            this.btnSaveVariable.UseVisualStyleBackColor = true;
            this.btnSaveVariable.Click += new System.EventHandler(this.btnSaveVariable_Click);
            // 
            // tbVarName
            // 
            this.tbVarName.Location = new System.Drawing.Point(106, 27);
            this.tbVarName.Name = "tbVarName";
            this.tbVarName.Size = new System.Drawing.Size(125, 20);
            this.tbVarName.TabIndex = 1;
            this.tbVarName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDomainName_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Имя переменной";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnChangeRule);
            this.tabPage3.Controls.Add(this.lbxRules);
            this.tabPage3.Controls.Add(this.btnDeleteRule);
            this.tabPage3.Controls.Add(this.btnAddRule);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(733, 364);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Правила";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lbxRules
            // 
            this.lbxRules.FormattingEnabled = true;
            this.lbxRules.Location = new System.Drawing.Point(8, 6);
            this.lbxRules.Name = "lbxRules";
            this.lbxRules.Size = new System.Drawing.Size(718, 329);
            this.lbxRules.TabIndex = 14;
            this.lbxRules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbxRules_MouseDown);
            this.lbxRules.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbxRules_MouseMove);
            this.lbxRules.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbxRules_MouseUp);
            // 
            // btnDeleteRule
            // 
            this.btnDeleteRule.Location = new System.Drawing.Point(431, 338);
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.Size = new System.Drawing.Size(132, 23);
            this.btnDeleteRule.TabIndex = 13;
            this.btnDeleteRule.Text = "Удалить";
            this.btnDeleteRule.UseVisualStyleBackColor = true;
            this.btnDeleteRule.Click += new System.EventHandler(this.btnDeleteRule_Click);
            // 
            // btnAddRule
            // 
            this.btnAddRule.Location = new System.Drawing.Point(162, 338);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(125, 23);
            this.btnAddRule.TabIndex = 12;
            this.btnAddRule.Text = "Добавить";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(9, 421);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(404, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Добро пожаловать! Чтобы начать работу, откройте или создайте новый файл";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "\"Бинарный файл | *.bin\"";
            // 
            // lbxVarDomain
            // 
            this.lbxVarDomain.FormattingEnabled = true;
            this.lbxVarDomain.Location = new System.Drawing.Point(9, 80);
            this.lbxVarDomain.Name = "lbxVarDomain";
            this.lbxVarDomain.Size = new System.Drawing.Size(360, 82);
            this.lbxVarDomain.TabIndex = 16;
            // 
            // btnChangeRule
            // 
            this.btnChangeRule.Location = new System.Drawing.Point(293, 338);
            this.btnChangeRule.Name = "btnChangeRule";
            this.btnChangeRule.Size = new System.Drawing.Size(132, 23);
            this.btnChangeRule.TabIndex = 15;
            this.btnChangeRule.Text = "Редактировать";
            this.btnChangeRule.UseVisualStyleBackColor = true;
            this.btnChangeRule.Click += new System.EventHandler(this.btnChangeRule_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 438);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip2);
            this.Name = "MainForm";
            this.Text = "Экспертная система";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gbDomain.ResumeLayout(false);
            this.gbDomain.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.gbVar.ResumeLayout(false);
            this.gbVar.PerformLayout();
            this.gbQuest.ResumeLayout(false);
            this.gbQuest.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem консультацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem начатьToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public System.Windows.Forms.Button btnAddDomain;
        public System.Windows.Forms.Button btnDeleteDomain;
        public System.Windows.Forms.GroupBox gbDomain;
        public System.Windows.Forms.Button btnDeleteValue;
        public System.Windows.Forms.Button btnChangeValue;
        public System.Windows.Forms.Button btnAddValue;
        public System.Windows.Forms.Button btnChangeDomain;
        public System.Windows.Forms.Button btnDeleteVariable;
        public System.Windows.Forms.Button btnAddVariable;
        public System.Windows.Forms.GroupBox gbVar;
        public System.Windows.Forms.TextBox tbVarName;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmbVarDomain;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.GroupBox gbQuest;
        public System.Windows.Forms.TextBox tbQuestion;
        public System.Windows.Forms.RadioButton rbQD;
        public System.Windows.Forms.RadioButton rbD;
        public System.Windows.Forms.RadioButton rbQ;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button btnChangeVariable;
        public System.Windows.Forms.Button btnDeleteRule;
        public System.Windows.Forms.Button btnAddRule;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TextBox tbDomainName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnCancelVariable;
        public System.Windows.Forms.Button btnSaveVariable;
        public System.Windows.Forms.ListBox lbxDomains;
        public System.Windows.Forms.ListBox lbxValues;
        public System.Windows.Forms.ListBox lbxVariables;
        public System.Windows.Forms.ListBox lbxRules;
        private System.Windows.Forms.ListBox lbxVarDomain;
        public System.Windows.Forms.Button btnChangeRule;

    }
}

