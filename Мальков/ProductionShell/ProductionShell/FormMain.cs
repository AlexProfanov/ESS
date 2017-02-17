using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormMain : Form
	{
		private IContainer components;

		private MenuStrip menuStrip;

		private ToolStripMenuItem fileToolStripMenuItem;

		private ToolStripMenuItem domainToolStripMenuItem;

		private ToolStripMenuItem consultationToolStripMenuItem;

		private ToolStripMenuItem expositionToolStripMenuItem;

		private ToolStripMenuItem createToolStripMenuItem;

		private SaveFileDialog saveFileDialog;

		private OpenFileDialog openFileDialog;

		private ToolStripMenuItem openToolStripMenuItem;

		private ToolStripMenuItem saveToolStripMenuItem;

		private ToolStripMenuItem exitToolStripMenuItem;

		private Panel panel1;

		private ToolStripMenuItem variablesToolStripMenuItem;

		private ToolStripMenuItem domainsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem setGoalToolStripMenuItem;

		private ToolStripMenuItem doConsultationToolStripMenuItem;

		private ListView listViewRules;

		private ColumnHeader columnHeaderName;

		private ColumnHeader columnHeaderRule;

		private ToolStripMenuItem showExpositionToolStripMenuItem;

		private GroupBox groupBox1;

		private Button buttonRemoveRule;

		private Button buttonEditRule;

		private Button buttonAddRule;

		private ToolTip toolTip;

		private Button buttonClose;

		private GroupBox groupBox2;

		private SplitContainer splitContainer1;

		private ListView listViewCondition;

		private ListView listViewResolution;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		public FormMain()
		{
			this.InitializeComponent();
			this.createNewKnowledgeBase();
		}

		private void createNewKnowledgeBase()
		{
			Global.knowledgeBase = new KnowledgeBase();
		}

		private void openKnowledgeBase()
		{
			if (this.openFileDialog.ShowDialog() == 1)
			{
				try
				{
					Global.knowledgeBase = KnowledgeBase.openKnowledgeBase(this.openFileDialog.FileName);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", 0, 16);
				}
			}
		}

		private void saveKnowledgeBase()
		{
			if (this.saveFileDialog.ShowDialog() == 1)
			{
				try
				{
					KnowledgeBase.saveKnowledgeBase(this.saveFileDialog.FileName, Global.knowledgeBase);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", 0, 16);
				}
			}
		}

		private void printRuleList()
		{
			int indexToSelect = 0;
			if (this.listViewRules.SelectedIndices.Count > 0)
			{
				indexToSelect = this.listViewRules.SelectedIndices[0];
			}
			this.listViewRules.Items.Clear();
			IEnumerator<Rule> enumeratorForRules = Global.knowledgeBase.getEnumeratorForRules();
			while (enumeratorForRules.MoveNext())
			{
				ListViewItem listViewItem = new ListViewItem(enumeratorForRules.Current.Name);
				listViewItem.SubItems.Add(enumeratorForRules.Current.ToString());
				this.listViewRules.Items.Add(listViewItem);
			}
			DialogFuncs.selectListViewItem(this.listViewRules, indexToSelect);
		}

		private void showRuleInfo(int ruleIndex)
		{
			if (0 <= ruleIndex && ruleIndex < this.listViewRules.Items.Count)
			{
				Rule ruleAt = Global.knowledgeBase.getRuleAt(this.listViewRules.SelectedIndices[0]);
				this.listViewCondition.Items.Clear();
				IEnumerator<Condition> enumeratorForConditionList = ruleAt.getEnumeratorForConditionList();
				while (enumeratorForConditionList.MoveNext())
				{
					this.listViewCondition.Items.Add(enumeratorForConditionList.Current.ToString());
				}
				this.listViewResolution.Items.Clear();
				IEnumerator<Resolution> enumeratorForResolutionList = ruleAt.getEnumeratorForResolutionList();
				while (enumeratorForResolutionList.MoveNext())
				{
					this.listViewResolution.Items.Add(enumeratorForResolutionList.Current.ToString());
				}
			}
		}

		private void addRule()
		{
			RuleForm ruleForm = new RuleForm();
			ruleForm.createNewRule();
			this.printRuleList();
			DialogFuncs.selectListViewItem(this.listViewRules, this.listViewRules.Items.Count - 1);
		}

		private void editRule()
		{
			IEnumerator enumerator = this.listViewRules.SelectedItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					RuleForm ruleForm = new RuleForm();
					ruleForm.editRule(Global.knowledgeBase.getRuleAt(listViewItem.Index));
					this.printRuleList();
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		private void removeRule()
		{
			IEnumerator enumerator = this.listViewRules.SelectedItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					Rule ruleAt = Global.knowledgeBase.getRuleAt(listViewItem.Index);
					if (MessageBox.Show("Вы действительно хотите удалить правило " + ruleAt.Name + " ?", "Внимание", 4, 48) == 6)
					{
						Global.knowledgeBase.removeRuleAt(listViewItem.Index);
						this.printRuleList();
						DialogFuncs.selectListViewItem(this.listViewRules, 0);
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		private void setGoalForConsultation()
		{
			FormSetGoal formSetGoal = new FormSetGoal();
			formSetGoal.setGoal();
		}

		private void doConsultation()
		{
			if (Global.knowledgeBase.Goal == null)
			{
				this.setGoalForConsultation();
			}
			if (Global.knowledgeBase.Goal != null)
			{
				Global.io.startNewConsultation();
				Global.exposition.startRecording();
				try
				{
					if (Global.knowledgeBase.Goal.tryToGetValue())
					{
						MessageBox.Show(Global.knowledgeBase.Goal.ToString() + " = " + Global.knowledgeBase.Goal.Value.ToString(), "Результат консультации", 0, 64);
					}
					else
					{
						MessageBox.Show("Цель консультации не смогда означиться", "Результат консультации", 0, 64);
					}
				}
				catch (InvalidOperationException ex)
				{
					MessageBox.Show(ex.Message, "Ошибка выполнения", 0, 16);
				}
			}
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.createNewKnowledgeBase();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.openKnowledgeBase();
			this.printRuleList();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.saveKnowledgeBase();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void buttonAddRule_Click(object sender, EventArgs e)
		{
			this.addRule();
		}

		private void buttonEditRule_Click(object sender, EventArgs e)
		{
			this.editRule();
		}

		private void buttonRemoveRule_Click(object sender, EventArgs e)
		{
			this.removeRule();
		}

		private void domainsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormDomainList formDomainList = new FormDomainList();
			formDomainList.ShowDialog();
			this.printRuleList();
		}

		private void variablesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormVarList formVarList = new FormVarList();
			formVarList.ShowDialog();
			this.printRuleList();
		}

		private void setGoalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.setGoalForConsultation();
		}

		private void doConsultationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.doConsultation();
		}

		private void listViewRules_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewRules.AllowDrop = true;
			this.listViewCondition.AllowDrop = false;
			this.listViewResolution.AllowDrop = false;
			DialogFuncs.doDragBeginning(this.listViewRules);
		}

		private void listViewRules_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewRules, e);
		}

		private void listViewRules_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = new Point(e.X, e.Y);
				Point point2 = this.listViewRules.PointToClient(point);
				ListViewItem itemAt = this.listViewRules.GetItemAt(point2.X, point2.Y);
				if (itemAt == null)
				{
					return;
				}
				if (e.Effect == 2)
				{
					Global.knowledgeBase.insertRuleInto(listViewItem.Index, itemAt.Index);
				}
				else if (e.Effect == 1)
				{
					Global.knowledgeBase.switchRules(listViewItem.Index, itemAt.Index);
				}
				this.printRuleList();
			}
		}

		private void showExpositionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.exposition.showExposition();
		}

		private void FormMain_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode <= 27)
			{
				if (keyCode == 13)
				{
					this.buttonEditRule_Click(this, new EventArgs());
					return;
				}
				if (keyCode != 27)
				{
					return;
				}
				this.buttonClose_Click(this, new EventArgs());
				return;
			}
			else
			{
				if (keyCode == 32)
				{
					this.buttonAddRule_Click(this, new EventArgs());
					return;
				}
				if (keyCode != 46)
				{
					return;
				}
				this.buttonRemoveRule_Click(this, new EventArgs());
				return;
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void listViewRules_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewRules.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int ruleIndex = (int)enumerator.Current;
					this.showRuleInfo(ruleIndex);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		private void listViewRules_DoubleClick(object sender, EventArgs e)
		{
			this.buttonEditRule_Click(this, new EventArgs());
		}

		private void listViewCondition_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewRules.AllowDrop = false;
			this.listViewCondition.AllowDrop = true;
			this.listViewResolution.AllowDrop = false;
			DialogFuncs.doDragBeginning(this.listViewCondition);
		}

		private void listViewCondition_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewCondition, e);
		}

		private void listViewCondition_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = new Point(e.X, e.Y);
				Point point2 = this.listViewCondition.PointToClient(point);
				ListViewItem itemAt = this.listViewCondition.GetItemAt(point2.X, point2.Y);
				if (itemAt == null)
				{
					return;
				}
				if (this.listViewRules.SelectedIndices.Count != 0)
				{
					int ruleIndex = this.listViewRules.SelectedIndices[0];
					Rule ruleAt = Global.knowledgeBase.getRuleAt(ruleIndex);
					if (e.Effect == 2)
					{
						ruleAt.insertConditionInto(listViewItem.Index, itemAt.Index);
					}
					else if (e.Effect == 1)
					{
						ruleAt.switchConditions(listViewItem.Index, itemAt.Index);
					}
					this.printRuleList();
					this.showRuleInfo(ruleIndex);
				}
			}
		}

		private void listViewResolution_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewRules.AllowDrop = false;
			this.listViewCondition.AllowDrop = false;
			this.listViewResolution.AllowDrop = true;
			DialogFuncs.doDragBeginning(this.listViewResolution);
		}

		private void listViewResolution_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewResolution, e);
		}

		private void listViewResolution_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = new Point(e.X, e.Y);
				Point point2 = this.listViewResolution.PointToClient(point);
				ListViewItem itemAt = this.listViewResolution.GetItemAt(point2.X, point2.Y);
				if (itemAt == null)
				{
					return;
				}
				if (this.listViewRules.SelectedIndices.Count != 0)
				{
					int ruleIndex = this.listViewRules.SelectedIndices[0];
					Rule ruleAt = Global.knowledgeBase.getRuleAt(ruleIndex);
					if (e.Effect == 2)
					{
						ruleAt.insertResolutionInto(listViewItem.Index, itemAt.Index);
					}
					else if (e.Effect == 1)
					{
						ruleAt.switchResolutions(listViewItem.Index, itemAt.Index);
					}
					this.printRuleList();
					this.showRuleInfo(ruleIndex);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			this.menuStrip = new MenuStrip();
			this.fileToolStripMenuItem = new ToolStripMenuItem();
			this.createToolStripMenuItem = new ToolStripMenuItem();
			this.openToolStripMenuItem = new ToolStripMenuItem();
			this.saveToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.exitToolStripMenuItem = new ToolStripMenuItem();
			this.domainToolStripMenuItem = new ToolStripMenuItem();
			this.domainsToolStripMenuItem = new ToolStripMenuItem();
			this.variablesToolStripMenuItem = new ToolStripMenuItem();
			this.consultationToolStripMenuItem = new ToolStripMenuItem();
			this.setGoalToolStripMenuItem = new ToolStripMenuItem();
			this.doConsultationToolStripMenuItem = new ToolStripMenuItem();
			this.expositionToolStripMenuItem = new ToolStripMenuItem();
			this.showExpositionToolStripMenuItem = new ToolStripMenuItem();
			this.saveFileDialog = new SaveFileDialog();
			this.openFileDialog = new OpenFileDialog();
			this.panel1 = new Panel();
			this.groupBox2 = new GroupBox();
			this.splitContainer1 = new SplitContainer();
			this.listViewCondition = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.listViewResolution = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.buttonClose = new Button();
			this.groupBox1 = new GroupBox();
			this.buttonRemoveRule = new Button();
			this.buttonEditRule = new Button();
			this.buttonAddRule = new Button();
			this.listViewRules = new ListView();
			this.columnHeaderName = new ColumnHeader();
			this.columnHeaderRule = new ColumnHeader();
			this.toolTip = new ToolTip(this.components);
			this.menuStrip.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip.Items.AddRange(new ToolStripItem[]
			{
				this.fileToolStripMenuItem,
				this.domainToolStripMenuItem,
				this.consultationToolStripMenuItem,
				this.expositionToolStripMenuItem
			});
			this.menuStrip.Location = new Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new Size(865, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.createToolStripMenuItem,
				this.openToolStripMenuItem,
				this.saveToolStripMenuItem,
				this.toolStripSeparator1,
				this.exitToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new Size(49, 20);
			this.fileToolStripMenuItem.Text = "&Файл";
			this.createToolStripMenuItem.Name = "createToolStripMenuItem";
			this.createToolStripMenuItem.ShortcutKeys = 131150;
			this.createToolStripMenuItem.Size = new Size(219, 22);
			this.createToolStripMenuItem.Text = "&Новый";
			this.createToolStripMenuItem.Click += new EventHandler(this.createToolStripMenuItem_Click);
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = 131151;
			this.openToolStripMenuItem.Size = new Size(219, 22);
			this.openToolStripMenuItem.Text = "&Открыть...";
			this.openToolStripMenuItem.Click += new EventHandler(this.openToolStripMenuItem_Click);
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = 131155;
			this.saveToolStripMenuItem.Size = new Size(219, 22);
			this.saveToolStripMenuItem.Text = "&Сохранить как...";
			this.saveToolStripMenuItem.Click += new EventHandler(this.saveToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(216, 6);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new Size(219, 22);
			this.exitToolStripMenuItem.Text = "&Выход";
			this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
			this.domainToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.domainsToolStripMenuItem,
				this.variablesToolStripMenuItem
			});
			this.domainToolStripMenuItem.Name = "domainToolStripMenuItem";
			this.domainToolStripMenuItem.Size = new Size(59, 20);
			this.domainToolStripMenuItem.Text = "&Знания";
			this.domainsToolStripMenuItem.Name = "domainsToolStripMenuItem";
			this.domainsToolStripMenuItem.ShortcutKeys = 131140;
			this.domainsToolStripMenuItem.Size = new Size(200, 22);
			this.domainsToolStripMenuItem.Text = "&Домены";
			this.domainsToolStripMenuItem.Click += new EventHandler(this.domainsToolStripMenuItem_Click);
			this.variablesToolStripMenuItem.Name = "variablesToolStripMenuItem";
			this.variablesToolStripMenuItem.ShortcutKeys = 131158;
			this.variablesToolStripMenuItem.Size = new Size(200, 22);
			this.variablesToolStripMenuItem.Text = "&Переменные";
			this.variablesToolStripMenuItem.Click += new EventHandler(this.variablesToolStripMenuItem_Click);
			this.consultationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.setGoalToolStripMenuItem,
				this.doConsultationToolStripMenuItem
			});
			this.consultationToolStripMenuItem.Name = "consultationToolStripMenuItem";
			this.consultationToolStripMenuItem.Size = new Size(98, 20);
			this.consultationToolStripMenuItem.Text = "&Консультация";
			this.setGoalToolStripMenuItem.Name = "setGoalToolStripMenuItem";
			this.setGoalToolStripMenuItem.ShortcutKeys = 131139;
			this.setGoalToolStripMenuItem.Size = new Size(194, 22);
			this.setGoalToolStripMenuItem.Text = "&Задать цель";
			this.setGoalToolStripMenuItem.Click += new EventHandler(this.setGoalToolStripMenuItem_Click);
			this.doConsultationToolStripMenuItem.Name = "doConsultationToolStripMenuItem";
			this.doConsultationToolStripMenuItem.ShortcutKeys = 131138;
			this.doConsultationToolStripMenuItem.Size = new Size(194, 22);
			this.doConsultationToolStripMenuItem.Text = "&Начать";
			this.doConsultationToolStripMenuItem.Click += new EventHandler(this.doConsultationToolStripMenuItem_Click);
			this.expositionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.showExpositionToolStripMenuItem
			});
			this.expositionToolStripMenuItem.Name = "expositionToolStripMenuItem";
			this.expositionToolStripMenuItem.Size = new Size(90, 20);
			this.expositionToolStripMenuItem.Text = "&Объяснение";
			this.showExpositionToolStripMenuItem.Name = "showExpositionToolStripMenuItem";
			this.showExpositionToolStripMenuItem.ShortcutKeys = 131141;
			this.showExpositionToolStripMenuItem.Size = new Size(176, 22);
			this.showExpositionToolStripMenuItem.Text = "&Показать";
			this.showExpositionToolStripMenuItem.Click += new EventHandler(this.showExpositionToolStripMenuItem_Click);
			this.saveFileDialog.DefaultExt = "bin";
			this.saveFileDialog.Filter = "Binary files|*.bin";
			this.openFileDialog.DefaultExt = "bin";
			this.openFileDialog.Filter = "Binary files|*.bin";
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = 4;
			this.panel1.Location = new Point(595, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(270, 490);
			this.panel1.TabIndex = 1;
			this.groupBox2.Anchor = 3;
			this.groupBox2.Controls.Add(this.splitContainer1);
			this.groupBox2.Location = new Point(11, 125);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(250, 328);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Текущее правило";
			this.splitContainer1.Dock = 5;
			this.splitContainer1.Location = new Point(3, 16);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = 0;
			this.splitContainer1.Panel1.Controls.Add(this.listViewCondition);
			this.splitContainer1.Panel2.Controls.Add(this.listViewResolution);
			this.splitContainer1.Size = new Size(244, 309);
			this.splitContainer1.SplitterDistance = 141;
			this.splitContainer1.TabIndex = 0;
			this.listViewCondition.AllowDrop = true;
			this.listViewCondition.BorderStyle = 1;
			this.listViewCondition.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listViewCondition.Dock = 5;
			this.listViewCondition.ForeColor = Color.DarkBlue;
			this.listViewCondition.FullRowSelect = true;
			this.listViewCondition.GridLines = true;
			this.listViewCondition.HeaderStyle = 1;
			this.listViewCondition.HideSelection = false;
			this.listViewCondition.Location = new Point(0, 0);
			this.listViewCondition.MultiSelect = false;
			this.listViewCondition.Name = "listViewCondition";
			this.listViewCondition.Size = new Size(244, 141);
			this.listViewCondition.TabIndex = 1;
			this.toolTip.SetToolTip(this.listViewCondition, "Drag&Drop - вставка условия на новое место\r\nDrag&Drop + Shift - перестановка условий местами");
			this.listViewCondition.UseCompatibleStateImageBehavior = false;
			this.listViewCondition.View = 1;
			this.listViewCondition.DragDrop += new DragEventHandler(this.listViewCondition_DragDrop);
			this.listViewCondition.DragOver += new DragEventHandler(this.listViewCondition_DragOver);
			this.listViewCondition.ItemDrag += new ItemDragEventHandler(this.listViewCondition_ItemDrag);
			this.columnHeader1.Text = "Условие";
			this.columnHeader1.Width = 217;
			this.listViewResolution.AllowDrop = true;
			this.listViewResolution.BorderStyle = 1;
			this.listViewResolution.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader2
			});
			this.listViewResolution.Dock = 5;
			this.listViewResolution.ForeColor = Color.DarkBlue;
			this.listViewResolution.FullRowSelect = true;
			this.listViewResolution.GridLines = true;
			this.listViewResolution.HeaderStyle = 1;
			this.listViewResolution.HideSelection = false;
			this.listViewResolution.Location = new Point(0, 0);
			this.listViewResolution.MultiSelect = false;
			this.listViewResolution.Name = "listViewResolution";
			this.listViewResolution.Size = new Size(244, 164);
			this.listViewResolution.TabIndex = 1;
			this.toolTip.SetToolTip(this.listViewResolution, "Drag&Drop - вставка заключения на новое место\r\nDrag&Drop + Shift - перестановка заключений местами");
			this.listViewResolution.UseCompatibleStateImageBehavior = false;
			this.listViewResolution.View = 1;
			this.listViewResolution.DragDrop += new DragEventHandler(this.listViewResolution_DragDrop);
			this.listViewResolution.DragOver += new DragEventHandler(this.listViewResolution_DragOver);
			this.listViewResolution.ItemDrag += new ItemDragEventHandler(this.listViewResolution_ItemDrag);
			this.columnHeader2.Text = "Заключение";
			this.columnHeader2.Width = 217;
			this.buttonClose.Anchor = 2;
			this.buttonClose.FlatStyle = 0;
			this.buttonClose.Location = new Point(11, 459);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new Size(250, 23);
			this.buttonClose.TabIndex = 6;
			this.buttonClose.Text = "Закрыть";
			this.toolTip.SetToolTip(this.buttonClose, "Вызов через - Esc");
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new EventHandler(this.buttonClose_Click);
			this.groupBox1.Controls.Add(this.buttonRemoveRule);
			this.groupBox1.Controls.Add(this.buttonEditRule);
			this.groupBox1.Controls.Add(this.buttonAddRule);
			this.groupBox1.Location = new Point(11, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(250, 113);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Действия";
			this.buttonRemoveRule.FlatStyle = 0;
			this.buttonRemoveRule.Location = new Point(6, 81);
			this.buttonRemoveRule.Name = "buttonRemoveRule";
			this.buttonRemoveRule.Size = new Size(238, 23);
			this.buttonRemoveRule.TabIndex = 5;
			this.buttonRemoveRule.Text = "Удалить";
			this.toolTip.SetToolTip(this.buttonRemoveRule, "Вызов через - Del");
			this.buttonRemoveRule.UseVisualStyleBackColor = true;
			this.buttonRemoveRule.Click += new EventHandler(this.buttonRemoveRule_Click);
			this.buttonEditRule.FlatStyle = 0;
			this.buttonEditRule.Location = new Point(6, 52);
			this.buttonEditRule.Name = "buttonEditRule";
			this.buttonEditRule.Size = new Size(238, 23);
			this.buttonEditRule.TabIndex = 4;
			this.buttonEditRule.Text = "Редактировать";
			this.toolTip.SetToolTip(this.buttonEditRule, "Вызов через - Enter");
			this.buttonEditRule.UseVisualStyleBackColor = true;
			this.buttonEditRule.Click += new EventHandler(this.buttonEditRule_Click);
			this.buttonAddRule.FlatStyle = 0;
			this.buttonAddRule.Location = new Point(6, 23);
			this.buttonAddRule.Name = "buttonAddRule";
			this.buttonAddRule.Size = new Size(238, 23);
			this.buttonAddRule.TabIndex = 3;
			this.buttonAddRule.Text = "Добавить";
			this.toolTip.SetToolTip(this.buttonAddRule, "Вызов через - Space");
			this.buttonAddRule.UseVisualStyleBackColor = true;
			this.buttonAddRule.Click += new EventHandler(this.buttonAddRule_Click);
			this.listViewRules.AllowDrop = true;
			this.listViewRules.BorderStyle = 1;
			this.listViewRules.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeaderName,
				this.columnHeaderRule
			});
			this.listViewRules.Dock = 5;
			this.listViewRules.ForeColor = Color.DarkBlue;
			this.listViewRules.FullRowSelect = true;
			this.listViewRules.GridLines = true;
			this.listViewRules.HeaderStyle = 1;
			this.listViewRules.HideSelection = false;
			this.listViewRules.Location = new Point(0, 24);
			this.listViewRules.MultiSelect = false;
			this.listViewRules.Name = "listViewRules";
			this.listViewRules.Size = new Size(595, 490);
			this.listViewRules.TabIndex = 2;
			this.toolTip.SetToolTip(this.listViewRules, "Drag&Drop - вставка правила на новое место\r\nDrag&Drop + Shift - перестановка правил местами");
			this.listViewRules.UseCompatibleStateImageBehavior = false;
			this.listViewRules.View = 1;
			this.listViewRules.DragDrop += new DragEventHandler(this.listViewRules_DragDrop);
			this.listViewRules.DoubleClick += new EventHandler(this.listViewRules_DoubleClick);
			this.listViewRules.DragOver += new DragEventHandler(this.listViewRules_DragOver);
			this.listViewRules.SelectedIndexChanged += new EventHandler(this.listViewRules_SelectedIndexChanged);
			this.listViewRules.ItemDrag += new ItemDragEventHandler(this.listViewRules_ItemDrag);
			this.columnHeaderName.Text = "Имя";
			this.columnHeaderName.Width = 80;
			this.columnHeaderRule.Text = "Содержание";
			this.columnHeaderRule.Width = 800;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(865, 514);
			base.Controls.Add(this.listViewRules);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.menuStrip);
			base.KeyPreview = true;
			base.MainMenuStrip = this.menuStrip;
			this.MinimumSize = new Size(400, 350);
			base.Name = "FormMain";
			base.StartPosition = 1;
			this.Text = "Продукционная оболочка";
			base.KeyDown += new KeyEventHandler(this.FormMain_KeyDown);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
