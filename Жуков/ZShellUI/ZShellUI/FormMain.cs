using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZShellCore;

namespace ZShellUI
{
	public class FormMain : Form
	{
		private IContainer components = null;

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

		private ToolStripMenuItem variablesToolStripMenuItem;

		private ToolStripMenuItem domainsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem doConsultationToolStripMenuItem;

		private ListView listViewRules;

		private ColumnHeader columnHeaderName;

		private ColumnHeader columnHeaderRule;

		private ToolStripMenuItem showExpositionToolStripMenuItem;

		private Button buttonRemoveRule;

		private Button buttonEditRule;

		private Button buttonAddRule;

		private ToolTip toolTip;

		private Button buttonClose;

		private GroupBox groupBox2;

		private SplitContainer splitContainer1;

		private ListView listViewCondition;

		private ColumnHeader columnHeader1;

		private ListView listViewResolution;

		private ColumnHeader columnHeader2;

		public FormMain()
		{
			this.InitializeComponent();
		}

		private void setControlsVisible(bool flag)
		{
			this.listViewRules.Visible = flag;
			this.groupBox2.Visible = flag;
			this.buttonAddRule.Visible = flag;
			this.buttonEditRule.Visible = flag;
			this.buttonRemoveRule.Visible = flag;
			this.buttonClose.Visible = flag;
			this.domainToolStripMenuItem.Visible = flag;
			this.consultationToolStripMenuItem.Visible = flag;
			this.expositionToolStripMenuItem.Visible = flag;
		}

		private void createNewKnowledgeBase()
		{
			Global.knowledgeBase = new KnowledgeBase();
			Global.io = new ConsultationIO();
			Global.exposition = new TreeViewExposition();
			this.Text = "ZShellCore - Безымянная база знаний";
			this.listViewRules.Clear();
			this.listViewCondition.Clear();
			this.listViewResolution.Clear();
			this.setControlsVisible(true);
		}

		private void openKnowledgeBase()
		{
			if (this.openFileDialog.ShowDialog() == 1)
			{
				try
				{
					Global.knowledgeBase = KnowledgeBase.openKnowledgeBase(this.openFileDialog.FileName);
					Global.io = new ConsultationIO();
					Global.exposition = new TreeViewExposition();
					this.Text = "ZShellCore - " + this.openFileDialog.FileName;
					this.setControlsVisible(true);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", 0, 16);
					Global.knowledgeBase = null;
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
					this.Text = "ZShellCore - " + this.saveFileDialog.FileName;
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", 0, 16);
					Global.knowledgeBase = null;
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
				ListView.ListViewItemCollection arg_83_0 = this.listViewRules.Items;
				ListViewItem listViewItem = new ListViewItem(enumeratorForRules.Current.Name);
				listViewItem.SubItems.Add(enumeratorForRules.Current.ToString());
				arg_83_0.Add(listViewItem);
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
			int num = this.listViewRules.Items.Count - 1;
			if (this.listViewRules.SelectedIndices.Count > 0)
			{
				num = this.listViewRules.SelectedIndices[0];
			}
			new FormRule().createNewRule(num);
			this.printRuleList();
			DialogFuncs.selectListViewItem(this.listViewRules, num);
		}

		private void editRule()
		{
			IEnumerator enumerator = this.listViewRules.SelectedItems.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					new FormRule().editRule(Global.knowledgeBase.getRuleAt(listViewItem.Index));
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
					if (MessageBox.Show("Вы действительно хотите удалить правило " + Global.knowledgeBase.getRuleAt(listViewItem.Index).Name + " ?", "Внимание", 4, 48) == 6)
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
			new FormSetGoal().setGoal();
		}

		private void doConsultation()
		{
			if (Global.knowledgeBase.Goal == null)
			{
				this.setGoalForConsultation();
			}
			if (Global.knowledgeBase.Goal != null)
			{
				(Global.io as ConsultationIO).startNewConsultation();
				Global.exposition.startRecording();
				try
				{
					if (Global.knowledgeBase.Goal.tryToGetValue())
					{
						MessageBox.Show(Global.knowledgeBase.Goal.ToString() + " = " + Global.knowledgeBase.Goal.Value.ToString(), "Результат консультации", 0, 64);
					}
					else
					{
						MessageBox.Show("Цель консультации не смогла означиться", "Результат консультации", 0, 64);
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
			if (Global.knowledgeBase != null)
			{
				if (MessageBox.Show("Вы завершаете работу с текущей базой знаний. Хотите ее сохранить ?", "Внимание", 4, 32) == 6)
				{
					this.saveToolStripMenuItem_Click(sender, e);
				}
			}
			this.createNewKnowledgeBase();
			this.listViewRules.Focus();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.knowledgeBase != null)
			{
				if (MessageBox.Show("Вы завершаете работу с текущей базой знаний. Хотите ее сохранить ?", "Внимание", 4, 32) == 6)
				{
					this.saveToolStripMenuItem_Click(sender, e);
				}
			}
			this.openKnowledgeBase();
			if (Global.knowledgeBase != null)
			{
				this.printRuleList();
			}
			this.listViewRules.Focus();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.saveKnowledgeBase();
			this.listViewRules.Focus();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void buttonAddRule_Click(object sender, EventArgs e)
		{
			this.addRule();
			this.listViewRules.Focus();
		}

		private void buttonEditRule_Click(object sender, EventArgs e)
		{
			this.editRule();
			this.listViewRules.Focus();
		}

		private void buttonRemoveRule_Click(object sender, EventArgs e)
		{
			this.removeRule();
			this.listViewRules.Focus();
		}

		private void domainsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new FormDomainList().ShowDialog();
			this.printRuleList();
			this.listViewRules.Focus();
		}

		private void variablesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new FormVariableList().ShowDialog();
			this.printRuleList();
			this.listViewRules.Focus();
		}

		private void doConsultationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.doConsultation();
			this.listViewRules.Focus();
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
			if (MessageBox.Show("Вы действительно хотите переместить элемент ?", "Внимание", 4, 48) == 6)
			{
				if (e.Data.GetDataPresent(typeof(ListViewItem)))
				{
					ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
					Point point = this.listViewRules.PointToClient(new Point(e.X, e.Y));
					ListViewItem itemAt = this.listViewRules.GetItemAt(point.X, point.Y);
					if (itemAt != null)
					{
						if (e.Effect == 2)
						{
							Global.knowledgeBase.insertRuleInto(listViewItem.Index, itemAt.Index);
						}
						this.printRuleList();
					}
				}
			}
		}

		private void showExpositionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(Global.exposition as TreeViewExposition).showExposition();
			this.listViewRules.Focus();
		}

		private void FormMain_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != 13)
			{
				if (keyCode != 27)
				{
					if (keyCode == 46)
					{
						this.buttonRemoveRule_Click(this, new EventArgs());
					}
				}
				else
				{
					this.buttonClose_Click(this, new EventArgs());
				}
			}
			else
			{
				this.buttonEditRule_Click(this, new EventArgs());
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
			if (MessageBox.Show("Вы действительно хотите переместить элемент ?", "Внимание", 4, 48) == 6)
			{
				if (e.Data.GetDataPresent(typeof(ListViewItem)))
				{
					ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
					Point point = this.listViewCondition.PointToClient(new Point(e.X, e.Y));
					ListViewItem itemAt = this.listViewCondition.GetItemAt(point.X, point.Y);
					if (itemAt != null && this.listViewRules.SelectedIndices.Count != 0)
					{
						int ruleIndex = this.listViewRules.SelectedIndices[0];
						Rule ruleAt = Global.knowledgeBase.getRuleAt(ruleIndex);
						if (e.Effect == 2)
						{
							ruleAt.insertConditionInto(listViewItem.Index, itemAt.Index);
						}
						this.printRuleList();
						this.showRuleInfo(ruleIndex);
					}
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
			if (MessageBox.Show("Вы действительно хотите переместить элемент ?", "Внимание", 4, 48) == 6)
			{
				if (e.Data.GetDataPresent(typeof(ListViewItem)))
				{
					ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
					Point point = this.listViewResolution.PointToClient(new Point(e.X, e.Y));
					ListViewItem itemAt = this.listViewResolution.GetItemAt(point.X, point.Y);
					if (itemAt != null && this.listViewRules.SelectedIndices.Count != 0)
					{
						int ruleIndex = this.listViewRules.SelectedIndices[0];
						Rule ruleAt = Global.knowledgeBase.getRuleAt(ruleIndex);
						if (e.Effect == 2)
						{
							ruleAt.insertResolutionInto(listViewItem.Index, itemAt.Index);
						}
						this.printRuleList();
						this.showRuleInfo(ruleIndex);
					}
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
			this.doConsultationToolStripMenuItem = new ToolStripMenuItem();
			this.expositionToolStripMenuItem = new ToolStripMenuItem();
			this.showExpositionToolStripMenuItem = new ToolStripMenuItem();
			this.saveFileDialog = new SaveFileDialog();
			this.openFileDialog = new OpenFileDialog();
			this.buttonRemoveRule = new Button();
			this.buttonEditRule = new Button();
			this.buttonAddRule = new Button();
			this.listViewRules = new ListView();
			this.columnHeaderName = new ColumnHeader();
			this.columnHeaderRule = new ColumnHeader();
			this.buttonClose = new Button();
			this.groupBox2 = new GroupBox();
			this.splitContainer1 = new SplitContainer();
			this.listViewCondition = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.listViewResolution = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.menuStrip.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.splitContainer1.BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
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
			this.menuStrip.Size = new Size(734, 24);
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
			this.fileToolStripMenuItem.Size = new Size(48, 20);
			this.fileToolStripMenuItem.Text = "&Файл";
			this.createToolStripMenuItem.Name = "createToolStripMenuItem";
			this.createToolStripMenuItem.Size = new Size(162, 22);
			this.createToolStripMenuItem.Text = "&Новый";
			this.createToolStripMenuItem.Click += new EventHandler(this.createToolStripMenuItem_Click);
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new Size(162, 22);
			this.openToolStripMenuItem.Text = "&Открыть...";
			this.openToolStripMenuItem.Click += new EventHandler(this.openToolStripMenuItem_Click);
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new Size(162, 22);
			this.saveToolStripMenuItem.Text = "&Сохранить как...";
			this.saveToolStripMenuItem.Click += new EventHandler(this.saveToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(159, 6);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new Size(162, 22);
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
			this.domainToolStripMenuItem.Visible = false;
			this.domainsToolStripMenuItem.Name = "domainsToolStripMenuItem";
			this.domainsToolStripMenuItem.Size = new Size(146, 22);
			this.domainsToolStripMenuItem.Text = "&Домены";
			this.domainsToolStripMenuItem.Click += new EventHandler(this.domainsToolStripMenuItem_Click);
			this.variablesToolStripMenuItem.Name = "variablesToolStripMenuItem";
			this.variablesToolStripMenuItem.Size = new Size(146, 22);
			this.variablesToolStripMenuItem.Text = "&Переменные";
			this.variablesToolStripMenuItem.Click += new EventHandler(this.variablesToolStripMenuItem_Click);
			this.consultationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.doConsultationToolStripMenuItem
			});
			this.consultationToolStripMenuItem.Name = "consultationToolStripMenuItem";
			this.consultationToolStripMenuItem.Size = new Size(96, 20);
			this.consultationToolStripMenuItem.Text = "&Консультация";
			this.consultationToolStripMenuItem.Visible = false;
			this.doConsultationToolStripMenuItem.Name = "doConsultationToolStripMenuItem";
			this.doConsultationToolStripMenuItem.Size = new Size(152, 22);
			this.doConsultationToolStripMenuItem.Text = "&Начать";
			this.doConsultationToolStripMenuItem.Click += new EventHandler(this.doConsultationToolStripMenuItem_Click);
			this.expositionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.showExpositionToolStripMenuItem
			});
			this.expositionToolStripMenuItem.Name = "expositionToolStripMenuItem";
			this.expositionToolStripMenuItem.Size = new Size(87, 20);
			this.expositionToolStripMenuItem.Text = "&Объяснение";
			this.expositionToolStripMenuItem.Visible = false;
			this.showExpositionToolStripMenuItem.Name = "showExpositionToolStripMenuItem";
			this.showExpositionToolStripMenuItem.Size = new Size(124, 22);
			this.showExpositionToolStripMenuItem.Text = "&Показать";
			this.showExpositionToolStripMenuItem.Click += new EventHandler(this.showExpositionToolStripMenuItem_Click);
			this.saveFileDialog.DefaultExt = "bin";
			this.saveFileDialog.Filter = "Binary files|*.bin";
			this.openFileDialog.DefaultExt = "bin";
			this.openFileDialog.Filter = "Binary files|*.bin";
			this.buttonRemoveRule.Anchor = 6;
			this.buttonRemoveRule.Location = new Point(256, 530);
			this.buttonRemoveRule.Name = "buttonRemoveRule";
			this.buttonRemoveRule.Size = new Size(110, 23);
			this.buttonRemoveRule.TabIndex = 5;
			this.buttonRemoveRule.Text = "Удалить";
			this.buttonRemoveRule.UseVisualStyleBackColor = true;
			this.buttonRemoveRule.Visible = false;
			this.buttonRemoveRule.Click += new EventHandler(this.buttonRemoveRule_Click);
			this.buttonEditRule.Anchor = 6;
			this.buttonEditRule.Location = new Point(140, 530);
			this.buttonEditRule.Name = "buttonEditRule";
			this.buttonEditRule.Size = new Size(110, 23);
			this.buttonEditRule.TabIndex = 4;
			this.buttonEditRule.Text = "Редактировать";
			this.buttonEditRule.UseVisualStyleBackColor = true;
			this.buttonEditRule.Visible = false;
			this.buttonEditRule.Click += new EventHandler(this.buttonEditRule_Click);
			this.buttonAddRule.Anchor = 6;
			this.buttonAddRule.Location = new Point(24, 530);
			this.buttonAddRule.Name = "buttonAddRule";
			this.buttonAddRule.Size = new Size(110, 23);
			this.buttonAddRule.TabIndex = 3;
			this.buttonAddRule.Text = "Добавить";
			this.buttonAddRule.UseVisualStyleBackColor = true;
			this.buttonAddRule.Visible = false;
			this.buttonAddRule.Click += new EventHandler(this.buttonAddRule_Click);
			this.listViewRules.AllowDrop = true;
			this.listViewRules.Anchor = 15;
			this.listViewRules.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeaderName,
				this.columnHeaderRule
			});
			this.listViewRules.ForeColor = Color.DarkBlue;
			this.listViewRules.FullRowSelect = true;
			this.listViewRules.GridLines = true;
			this.listViewRules.HeaderStyle = 1;
			this.listViewRules.HideSelection = false;
			this.listViewRules.Location = new Point(0, 27);
			this.listViewRules.MultiSelect = false;
			this.listViewRules.Name = "listViewRules";
			this.listViewRules.Size = new Size(418, 494);
			this.listViewRules.TabIndex = 2;
			this.listViewRules.UseCompatibleStateImageBehavior = false;
			this.listViewRules.View = 1;
			this.listViewRules.Visible = false;
			this.listViewRules.ItemDrag += new ItemDragEventHandler(this.listViewRules_ItemDrag);
			this.listViewRules.SelectedIndexChanged += new EventHandler(this.listViewRules_SelectedIndexChanged);
			this.listViewRules.DragDrop += new DragEventHandler(this.listViewRules_DragDrop);
			this.listViewRules.DragOver += new DragEventHandler(this.listViewRules_DragOver);
			this.listViewRules.DoubleClick += new EventHandler(this.listViewRules_DoubleClick);
			this.columnHeaderName.Text = "Имя";
			this.columnHeaderName.Width = 80;
			this.columnHeaderRule.Text = "Содержание";
			this.columnHeaderRule.Width = 800;
			this.buttonClose.Anchor = 10;
			this.buttonClose.Location = new Point(519, 530);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new Size(110, 23);
			this.buttonClose.TabIndex = 6;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Visible = false;
			this.buttonClose.Click += new EventHandler(this.buttonClose_Click);
			this.groupBox2.Anchor = 11;
			this.groupBox2.Controls.Add(this.splitContainer1);
			this.groupBox2.Location = new Point(424, 27);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(310, 497);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Текущее правило";
			this.groupBox2.Visible = false;
			this.splitContainer1.Dock = 5;
			this.splitContainer1.Location = new Point(3, 16);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = 0;
			this.splitContainer1.Panel1.Controls.Add(this.listViewCondition);
			this.splitContainer1.Panel2.Controls.Add(this.listViewResolution);
			this.splitContainer1.Size = new Size(304, 478);
			this.splitContainer1.SplitterDistance = 217;
			this.splitContainer1.TabIndex = 0;
			this.listViewCondition.AllowDrop = true;
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
			this.listViewCondition.Size = new Size(304, 217);
			this.listViewCondition.TabIndex = 1;
			this.listViewCondition.UseCompatibleStateImageBehavior = false;
			this.listViewCondition.View = 1;
			this.listViewCondition.ItemDrag += new ItemDragEventHandler(this.listViewCondition_ItemDrag);
			this.listViewCondition.DragDrop += new DragEventHandler(this.listViewCondition_DragDrop);
			this.listViewCondition.DragOver += new DragEventHandler(this.listViewCondition_DragOver);
			this.columnHeader1.Text = "Условие";
			this.columnHeader1.Width = 217;
			this.listViewResolution.AllowDrop = true;
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
			this.listViewResolution.Size = new Size(304, 257);
			this.listViewResolution.TabIndex = 1;
			this.listViewResolution.UseCompatibleStateImageBehavior = false;
			this.listViewResolution.View = 1;
			this.listViewResolution.ItemDrag += new ItemDragEventHandler(this.listViewResolution_ItemDrag);
			this.listViewResolution.DragDrop += new DragEventHandler(this.listViewResolution_DragDrop);
			this.listViewResolution.DragOver += new DragEventHandler(this.listViewResolution_DragOver);
			this.columnHeader2.Text = "Заключение";
			this.columnHeader2.Width = 217;
			base.ClientSize = new Size(734, 562);
			base.Controls.Add(this.buttonClose);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.buttonRemoveRule);
			base.Controls.Add(this.listViewRules);
			base.Controls.Add(this.buttonEditRule);
			base.Controls.Add(this.buttonAddRule);
			base.Controls.Add(this.menuStrip);
			base.KeyPreview = true;
			base.MainMenuStrip = this.menuStrip;
			this.MinimumSize = new Size(750, 600);
			base.Name = "FormMain";
			base.StartPosition = 1;
			this.Text = "ZShell";
			base.KeyDown += new KeyEventHandler(this.FormMain_KeyDown);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.EndInit();
			this.splitContainer1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
