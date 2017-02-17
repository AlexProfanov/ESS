namespace ОболочкаЭС
{
    partial class fAddRule
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
            this.gbRule = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lbxThen = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbxIF = new System.Windows.Forms.ListBox();
            this.btnDeleteFact = new System.Windows.Forms.Button();
            this.btnChangeFact = new System.Windows.Forms.Button();
            this.btnAddFact = new System.Windows.Forms.Button();
            this.tbReasoning = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancelRule = new System.Windows.Forms.Button();
            this.btnSaveRule = new System.Windows.Forms.Button();
            this.tbRuleName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.gbRule.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRule
            // 
            this.gbRule.Controls.Add(this.groupBox6);
            this.gbRule.Controls.Add(this.groupBox5);
            this.gbRule.Controls.Add(this.tbReasoning);
            this.gbRule.Controls.Add(this.label6);
            this.gbRule.Controls.Add(this.btnCancelRule);
            this.gbRule.Controls.Add(this.btnSaveRule);
            this.gbRule.Controls.Add(this.tbRuleName);
            this.gbRule.Controls.Add(this.label8);
            this.gbRule.Location = new System.Drawing.Point(12, 12);
            this.gbRule.Name = "gbRule";
            this.gbRule.Size = new System.Drawing.Size(442, 382);
            this.gbRule.TabIndex = 12;
            this.gbRule.TabStop = false;
            this.gbRule.Text = "Параметры правила";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.button3);
            this.groupBox6.Controls.Add(this.lbxThen);
            this.groupBox6.Location = new System.Drawing.Point(4, 197);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(427, 96);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "THEN";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Удалить факт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(103, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Изменить факт";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(5, 67);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Добавить факт";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lbxThen
            // 
            this.lbxThen.FormattingEnabled = true;
            this.lbxThen.Location = new System.Drawing.Point(8, 19);
            this.lbxThen.Name = "lbxThen";
            this.lbxThen.Size = new System.Drawing.Size(413, 43);
            this.lbxThen.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lbxIF);
            this.groupBox5.Controls.Add(this.btnDeleteFact);
            this.groupBox5.Controls.Add(this.btnChangeFact);
            this.groupBox5.Controls.Add(this.btnAddFact);
            this.groupBox5.Location = new System.Drawing.Point(4, 42);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(427, 149);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "IF";
            // 
            // lbxIF
            // 
            this.lbxIF.FormattingEnabled = true;
            this.lbxIF.Location = new System.Drawing.Point(8, 19);
            this.lbxIF.Name = "lbxIF";
            this.lbxIF.Size = new System.Drawing.Size(413, 95);
            this.lbxIF.TabIndex = 17;
            // 
            // btnDeleteFact
            // 
            this.btnDeleteFact.Location = new System.Drawing.Point(204, 120);
            this.btnDeleteFact.Name = "btnDeleteFact";
            this.btnDeleteFact.Size = new System.Drawing.Size(97, 23);
            this.btnDeleteFact.TabIndex = 16;
            this.btnDeleteFact.Text = "Удалить факт";
            this.btnDeleteFact.UseVisualStyleBackColor = true;
            this.btnDeleteFact.Click += new System.EventHandler(this.btnDeleteFact_Click);
            // 
            // btnChangeFact
            // 
            this.btnChangeFact.Location = new System.Drawing.Point(103, 120);
            this.btnChangeFact.Name = "btnChangeFact";
            this.btnChangeFact.Size = new System.Drawing.Size(97, 23);
            this.btnChangeFact.TabIndex = 15;
            this.btnChangeFact.Text = "Изменить факт";
            this.btnChangeFact.UseVisualStyleBackColor = true;
            this.btnChangeFact.Click += new System.EventHandler(this.btnChangeFact_Click);
            // 
            // btnAddFact
            // 
            this.btnAddFact.Location = new System.Drawing.Point(5, 120);
            this.btnAddFact.Name = "btnAddFact";
            this.btnAddFact.Size = new System.Drawing.Size(94, 23);
            this.btnAddFact.TabIndex = 13;
            this.btnAddFact.Text = "Добавить факт";
            this.btnAddFact.UseVisualStyleBackColor = true;
            this.btnAddFact.Click += new System.EventHandler(this.btnAddFact_Click);
            // 
            // tbReasoning
            // 
            this.tbReasoning.Location = new System.Drawing.Point(85, 312);
            this.tbReasoning.Name = "tbReasoning";
            this.tbReasoning.Size = new System.Drawing.Size(346, 20);
            this.tbReasoning.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 315);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Объяснение";
            // 
            // btnCancelRule
            // 
            this.btnCancelRule.Location = new System.Drawing.Point(208, 353);
            this.btnCancelRule.Name = "btnCancelRule";
            this.btnCancelRule.Size = new System.Drawing.Size(125, 23);
            this.btnCancelRule.TabIndex = 6;
            this.btnCancelRule.Text = "Отменить";
            this.btnCancelRule.UseVisualStyleBackColor = true;
            this.btnCancelRule.Click += new System.EventHandler(this.btnCancelRule_Click);
            // 
            // btnSaveRule
            // 
            this.btnSaveRule.Location = new System.Drawing.Point(71, 353);
            this.btnSaveRule.Name = "btnSaveRule";
            this.btnSaveRule.Size = new System.Drawing.Size(131, 23);
            this.btnSaveRule.TabIndex = 5;
            this.btnSaveRule.Text = "Сохранить";
            this.btnSaveRule.UseVisualStyleBackColor = true;
            this.btnSaveRule.Click += new System.EventHandler(this.btnSaveRule_Click);
            // 
            // tbRuleName
            // 
            this.tbRuleName.Location = new System.Drawing.Point(85, 16);
            this.tbRuleName.Name = "tbRuleName";
            this.tbRuleName.Size = new System.Drawing.Size(204, 20);
            this.tbRuleName.TabIndex = 1;
            this.tbRuleName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRuleName_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Имя правила";
            // 
            // fAddRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 396);
            this.Controls.Add(this.gbRule);
            this.Name = "fAddRule";
            this.Text = "fAddRule";
            this.gbRule.ResumeLayout(false);
            this.gbRule.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox gbRule;
        public System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.ListBox lbxThen;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.ListBox lbxIF;
        public System.Windows.Forms.Button btnDeleteFact;
        public System.Windows.Forms.Button btnChangeFact;
        public System.Windows.Forms.Button btnAddFact;
        public System.Windows.Forms.TextBox tbReasoning;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button btnCancelRule;
        public System.Windows.Forms.Button btnSaveRule;
        public System.Windows.Forms.TextBox tbRuleName;
        public System.Windows.Forms.Label label8;
    }
}