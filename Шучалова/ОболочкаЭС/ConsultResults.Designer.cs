namespace ОболочкаЭС
{
    partial class ConsultResults
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tview_steps = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbx_workMemory = new System.Windows.Forms.ListBox();
            this.lbl_goal = new System.Windows.Forms.Label();
            this.lbl_explain = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(899, 267);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tview_steps);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(891, 241);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Сработавшие правила";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tview_steps
            // 
            this.tview_steps.Location = new System.Drawing.Point(3, 3);
            this.tview_steps.Name = "tview_steps";
            this.tview_steps.Size = new System.Drawing.Size(884, 232);
            this.tview_steps.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbx_workMemory);
            this.groupBox1.Location = new System.Drawing.Point(5, 326);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(895, 135);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Рабочая память";
            // 
            // lbx_workMemory
            // 
            this.lbx_workMemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbx_workMemory.FormattingEnabled = true;
            this.lbx_workMemory.Location = new System.Drawing.Point(3, 16);
            this.lbx_workMemory.Name = "lbx_workMemory";
            this.lbx_workMemory.Size = new System.Drawing.Size(889, 116);
            this.lbx_workMemory.TabIndex = 0;
            // 
            // lbl_goal
            // 
            this.lbl_goal.AutoSize = true;
            this.lbl_goal.Location = new System.Drawing.Point(13, 275);
            this.lbl_goal.Name = "lbl_goal";
            this.lbl_goal.Size = new System.Drawing.Size(35, 13);
            this.lbl_goal.TabIndex = 2;
            this.lbl_goal.Text = "label1";
            // 
            // lbl_explain
            // 
            this.lbl_explain.AutoSize = true;
            this.lbl_explain.Location = new System.Drawing.Point(13, 299);
            this.lbl_explain.Name = "lbl_explain";
            this.lbl_explain.Size = new System.Drawing.Size(35, 13);
            this.lbl_explain.TabIndex = 3;
            this.lbl_explain.Text = "label2";
            // 
            // ConsultResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 464);
            this.Controls.Add(this.lbl_explain);
            this.Controls.Add(this.lbl_goal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "ConsultResults";
            this.Text = "Результаты консультации";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ListBox lbx_workMemory;
        public System.Windows.Forms.TreeView tview_steps;
        private System.Windows.Forms.Label lbl_goal;
        private System.Windows.Forms.Label lbl_explain;
    }
}