using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	public class FormMain1 : Form
	{
		private IContainer components = null;

		private ListView listViewRules;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private GroupBox groupBox1;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem файлToolStripMenuItem;

		private ToolStripMenuItem createToolStripMenuItem;

		private ToolStripMenuItem открытьToolStripMenuItem;

		private ToolStripMenuItem сохранитьКакToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem выходToolStripMenuItem;

		private ToolStripMenuItem знанияToolStripMenuItem;

		private ToolStripMenuItem доменыToolStripMenuItem;

		private ToolStripMenuItem переменныеToolStripMenuItem;

		private ToolStripMenuItem консультацияToolStripMenuItem;

		private ToolStripMenuItem задатьЦельToolStripMenuItem;

		private ToolStripMenuItem начатьToolStripMenuItem;

		private ToolStripMenuItem объяснениеToolStripMenuItem;

		private ToolStripMenuItem показатьToolStripMenuItem;

		private GroupBox groupBox3;

		private Button buttonRemoveRule;

		private Button buttonEditRule;

		private Button buttonAddRule;

		private Button buttonClose;

		private GroupBox groupBox2;

		private ColumnHeader columnHeader5;

		private ListView listViewCondition;

		private ColumnHeader columnHeader1;

		private OpenFileDialog openFileDialog;

		private SaveFileDialog saveFileDialog;

		private ListView listViewResolution;

		private ColumnHeader columnHeader2;

		public FormMain1()
		{
			this.InitializeComponent();
			this.printRuleList();
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
					int num = MessageBox.Show(ex.Message, "Ошибка", 0, 16);
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
					int num = MessageBox.Show(ex.Message, "Ошибка", 0, 16);
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
				ListView.ListViewItemCollection arg_81_0 = this.listViewRules.Items;
				ListViewItem listViewItem = new ListViewItem(enumeratorForRules.Current.Name);
				listViewItem.SubItems.Add(enumeratorForRules.Current.ToString());
				arg_81_0.Add(listViewItem);
			}
			DialogFuncs.selectListViewItem(this.listViewRules, indexToSelect);
			this.listViewRules.Select();
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
			new FormAddEditRule().createNewRule();
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
					new FormAddEditRule().editRule(Global.knowledgeBase.getRuleAt(listViewItem.Index));
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
			new FormSetGoal1().setGoal();
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
						int num = MessageBox.Show(Global.knowledgeBase.Goal.ToString() + " = " + Global.knowledgeBase.Goal.Value.ToString(), "Результат консультации", 0, 64);
					}
					else
					{
						int num2 = MessageBox.Show("Цель консультации не смогда означиться", "Результат консультации", 0, 64);
					}
				}
				catch (InvalidOperationException ex)
				{
					int num3 = MessageBox.Show(ex.Message, "Ошибка выполнения", 0, 16);
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
			int index = this.listViewRules.FocusedItem.Index;
			this.addRule();
			DialogFuncs.selectListViewItem(this.listViewRules, this.listViewRules.Items.Count - 1);
			Global.knowledgeBase.insertRuleInto(this.listViewRules.Items.Count - 1, index + 1);
			this.printRuleList();
			DialogFuncs.selectListViewItem(this.listViewRules, index + 1);
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
			FormDomain formDomain = new FormDomain();
			formDomain.ShowDialog();
			this.printRuleList();
		}

		private void variablesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int num = new FormVarList1().ShowDialog();
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
				Point point = this.listViewRules.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewRules.GetItemAt(point.X, point.Y);
				if (itemAt != null)
				{
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
				if (keyCode != 13)
				{
					if (keyCode == 27)
					{
						this.buttonClose_Click(this, new EventArgs());
					}
				}
				else
				{
					this.buttonEditRule_Click(this, new EventArgs());
				}
			}
			else if (keyCode != 32)
			{
				if (keyCode == 46)
				{
					this.buttonRemoveRule_Click(this, new EventArgs());
				}
			}
			else
			{
				this.buttonAddRule_Click(this, new EventArgs());
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
					else if (e.Effect == 1)
					{
						ruleAt.switchResolutions(listViewItem.Index, itemAt.Index);
					}
					this.printRuleList();
					this.showRuleInfo(ruleIndex);
				}
			}
		}

		private void FormMain1_Load(object sender, EventArgs e)
		{
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
			this.listViewRules = new ListView();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.groupBox1 = new GroupBox();
			this.menuStrip1 = new MenuStrip();
			this.файлToolStripMenuItem = new ToolStripMenuItem();
			this.createToolStripMenuItem = new ToolStripMenuItem();
			this.открытьToolStripMenuItem = new ToolStripMenuItem();
			this.сохранитьКакToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.выходToolStripMenuItem = new ToolStripMenuItem();
			this.знанияToolStripMenuItem = new ToolStripMenuItem();
			this.доменыToolStripMenuItem = new ToolStripMenuItem();
			this.переменныеToolStripMenuItem = new ToolStripMenuItem();
			this.консультацияToolStripMenuItem = new ToolStripMenuItem();
			this.задатьЦельToolStripMenuItem = new ToolStripMenuItem();
			this.начатьToolStripMenuItem = new ToolStripMenuItem();
			this.объяснениеToolStripMenuItem = new ToolStripMenuItem();
			this.показатьToolStripMenuItem = new ToolStripMenuItem();
			this.groupBox3 = new GroupBox();
			this.buttonRemoveRule = new Button();
			this.buttonEditRule = new Button();
			this.buttonAddRule = new Button();
			this.buttonClose = new Button();
			this.groupBox2 = new GroupBox();
			this.listViewResolution = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.listViewCondition = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.openFileDialog = new OpenFileDialog();
			this.saveFileDialog = new SaveFileDialog();
			this.menuStrip1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.listViewRules.Anchor = 15;
			this.listViewRules.BorderStyle = 1;
			this.listViewRules.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader3,
				this.columnHeader4
			});
			this.listViewRules.FullRowSelect = true;
			this.listViewRules.GridLines = true;
			this.listViewRules.HeaderStyle = 1;
			this.listViewRules.ImeMode = 0;
			this.listViewRules.Location = new Point(6, 6);
			this.listViewRules.MultiSelect = false;
			this.listViewRules.Name = "listViewRules";
			this.listViewRules.Size = new Size(998, 415);
			this.listViewRules.TabIndex = 8;
			this.listViewRules.UseCompatibleStateImageBehavior = false;
			this.listViewRules.View = 1;
			this.listViewRules.ItemDrag += new ItemDragEventHandler(this.listViewRules_ItemDrag);
			this.listViewRules.SelectedIndexChanged += new EventHandler(this.listViewRules_SelectedIndexChanged);
			this.listViewRules.DragDrop += new DragEventHandler(this.listViewRules_DragDrop);
			this.listViewRules.DragOver += new DragEventHandler(this.listViewRules_DragOver);
			this.listViewRules.DoubleClick += new EventHandler(this.listViewRules_DoubleClick);
			this.columnHeader3.Text = "Имя";
			this.columnHeader4.Text = "Содержание";
			this.columnHeader4.Width = 1500;
			this.groupBox1.Anchor = 10;
			this.groupBox1.AutoSize = true;
			this.groupBox1.Location = new Point(1010, -6669);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(213, 135);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Действия";
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.файлToolStripMenuItem,
				this.знанияToolStripMenuItem,
				this.консультацияToolStripMenuItem,
				this.объяснениеToolStripMenuItem
			});
			this.menuStrip1.LayoutStyle = 3;
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(832, 23);
			this.menuStrip1.TabIndex = 9;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			this.файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.createToolStripMenuItem,
				this.открытьToolStripMenuItem,
				this.сохранитьКакToolStripMenuItem,
				this.toolStripSeparator1,
				this.выходToolStripMenuItem
			});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new Size(48, 19);
			this.файлToolStripMenuItem.Text = "Файл";
			this.createToolStripMenuItem.Name = "createToolStripMenuItem";
			this.createToolStripMenuItem.Size = new Size(153, 22);
			this.createToolStripMenuItem.Text = "Создать";
			this.createToolStripMenuItem.Click += new EventHandler(this.createToolStripMenuItem_Click);
			this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
			this.открытьToolStripMenuItem.Size = new Size(153, 22);
			this.открытьToolStripMenuItem.Text = "Открыть";
			this.открытьToolStripMenuItem.Click += new EventHandler(this.openToolStripMenuItem_Click);
			this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
			this.сохранитьКакToolStripMenuItem.Size = new Size(153, 22);
			this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
			this.сохранитьКакToolStripMenuItem.Click += new EventHandler(this.saveToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(150, 6);
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.Size = new Size(153, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
			this.знанияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.доменыToolStripMenuItem,
				this.переменныеToolStripMenuItem
			});
			this.знанияToolStripMenuItem.Name = "знанияToolStripMenuItem";
			this.знанияToolStripMenuItem.Size = new Size(59, 19);
			this.знанияToolStripMenuItem.Text = "Знания";
			this.доменыToolStripMenuItem.Name = "доменыToolStripMenuItem";
			this.доменыToolStripMenuItem.Size = new Size(146, 22);
			this.доменыToolStripMenuItem.Text = "Домены";
			this.доменыToolStripMenuItem.Click += new EventHandler(this.domainsToolStripMenuItem_Click);
			this.переменныеToolStripMenuItem.Name = "переменныеToolStripMenuItem";
			this.переменныеToolStripMenuItem.Size = new Size(146, 22);
			this.переменныеToolStripMenuItem.Text = "Переменные";
			this.переменныеToolStripMenuItem.Click += new EventHandler(this.variablesToolStripMenuItem_Click);
			this.консультацияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.задатьЦельToolStripMenuItem,
				this.начатьToolStripMenuItem
			});
			this.консультацияToolStripMenuItem.Name = "консультацияToolStripMenuItem";
			this.консультацияToolStripMenuItem.Size = new Size(96, 19);
			this.консультацияToolStripMenuItem.Text = "Консультация";
			this.задатьЦельToolStripMenuItem.Name = "задатьЦельToolStripMenuItem";
			this.задатьЦельToolStripMenuItem.Size = new Size(139, 22);
			this.задатьЦельToolStripMenuItem.Text = "Задать цель";
			this.задатьЦельToolStripMenuItem.Click += new EventHandler(this.setGoalToolStripMenuItem_Click);
			this.начатьToolStripMenuItem.Name = "начатьToolStripMenuItem";
			this.начатьToolStripMenuItem.Size = new Size(139, 22);
			this.начатьToolStripMenuItem.Text = "Начать";
			this.начатьToolStripMenuItem.Click += new EventHandler(this.doConsultationToolStripMenuItem_Click);
			this.объяснениеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.показатьToolStripMenuItem
			});
			this.объяснениеToolStripMenuItem.Name = "объяснениеToolStripMenuItem";
			this.объяснениеToolStripMenuItem.Size = new Size(87, 19);
			this.объяснениеToolStripMenuItem.Text = "Объяснение";
			this.показатьToolStripMenuItem.Name = "показатьToolStripMenuItem";
			this.показатьToolStripMenuItem.Size = new Size(124, 22);
			this.показатьToolStripMenuItem.Text = "Показать";
			this.показатьToolStripMenuItem.Click += new EventHandler(this.showExpositionToolStripMenuItem_Click);
			this.groupBox3.Anchor = 11;
			this.groupBox3.AutoSize = true;
			this.groupBox3.Controls.Add(this.buttonRemoveRule);
			this.groupBox3.Controls.Add(this.buttonEditRule);
			this.groupBox3.Controls.Add(this.buttonAddRule);
			this.groupBox3.Location = new Point(1009, 5);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(213, 95);
			this.groupBox3.TabIndex = 10;
			this.groupBox3.TabStop = false;
			this.buttonRemoveRule.AutoSize = true;
			this.buttonRemoveRule.Location = new Point(3, 53);
			this.buttonRemoveRule.Name = "buttonRemoveRule";
			this.buttonRemoveRule.Size = new Size(204, 23);
			this.buttonRemoveRule.TabIndex = 2;
			this.buttonRemoveRule.Text = "Удалить";
			this.buttonRemoveRule.UseVisualStyleBackColor = true;
			this.buttonRemoveRule.Click += new EventHandler(this.buttonRemoveRule_Click);
			this.buttonEditRule.AutoSize = true;
			this.buttonEditRule.Location = new Point(3, 31);
			this.buttonEditRule.Name = "buttonEditRule";
			this.buttonEditRule.Size = new Size(204, 23);
			this.buttonEditRule.TabIndex = 1;
			this.buttonEditRule.Text = "Изменить";
			this.buttonEditRule.UseVisualStyleBackColor = true;
			this.buttonEditRule.Click += new EventHandler(this.buttonEditRule_Click);
			this.buttonAddRule.AutoSize = true;
			this.buttonAddRule.Location = new Point(3, 10);
			this.buttonAddRule.Name = "buttonAddRule";
			this.buttonAddRule.Size = new Size(204, 23);
			this.buttonAddRule.TabIndex = 0;
			this.buttonAddRule.Text = "Добавить";
			this.buttonAddRule.UseVisualStyleBackColor = true;
			this.buttonAddRule.Click += new EventHandler(this.buttonAddRule_Click);
			this.buttonClose.Anchor = 10;
			this.buttonClose.AutoSize = true;
			this.buttonClose.Location = new Point(1013, 398);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new Size(204, 23);
			this.buttonClose.TabIndex = 12;
			this.buttonClose.Text = "Закрыть";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new EventHandler(this.buttonClose_Click);
			this.groupBox2.Anchor = 11;
			this.groupBox2.Controls.Add(this.listViewResolution);
			this.groupBox2.Controls.Add(this.listViewCondition);
			this.groupBox2.Location = new Point(1008, 107);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(210, 285);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Текущее правило";
			this.listViewResolution.Anchor = 2;
			this.listViewResolution.BorderStyle = 1;
			this.listViewResolution.CausesValidation = false;
			this.listViewResolution.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader2
			});
			this.listViewResolution.FullRowSelect = true;
			this.listViewResolution.GridLines = true;
			this.listViewResolution.HeaderStyle = 1;
			this.listViewResolution.Location = new Point(5, 187);
			this.listViewResolution.MultiSelect = false;
			this.listViewResolution.Name = "listViewResolution";
			this.listViewResolution.Size = new Size(202, 92);
			this.listViewResolution.TabIndex = 2;
			this.listViewResolution.UseCompatibleStateImageBehavior = false;
			this.listViewResolution.View = 1;
			this.listViewResolution.ItemDrag += new ItemDragEventHandler(this.listViewResolution_ItemDrag);
			this.listViewResolution.DragDrop += new DragEventHandler(this.listViewResolution_DragDrop);
			this.listViewResolution.DragOver += new DragEventHandler(this.listViewResolution_DragOver);
			this.columnHeader2.Text = "Заключение";
			this.columnHeader2.Width = 200;
			this.listViewCondition.Anchor = 11;
			this.listViewCondition.BorderStyle = 1;
			this.listViewCondition.CausesValidation = false;
			this.listViewCondition.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listViewCondition.FullRowSelect = true;
			this.listViewCondition.GridLines = true;
			this.listViewCondition.HeaderStyle = 1;
			this.listViewCondition.Location = new Point(7, 19);
			this.listViewCondition.MultiSelect = false;
			this.listViewCondition.Name = "listViewCondition";
			this.listViewCondition.Size = new Size(202, 162);
			this.listViewCondition.TabIndex = 0;
			this.listViewCondition.UseCompatibleStateImageBehavior = false;
			this.listViewCondition.View = 1;
			this.listViewCondition.ItemDrag += new ItemDragEventHandler(this.listViewCondition_ItemDrag);
			this.listViewCondition.DragDrop += new DragEventHandler(this.listViewCondition_DragDrop);
			this.listViewCondition.DragOver += new DragEventHandler(this.listViewCondition_DragOver);
			this.columnHeader1.Text = "Условие";
			this.columnHeader1.Width = 200;
			this.columnHeader5.Text = "Заключение";
			this.columnHeader5.Width = 200;
			this.openFileDialog.FileName = "openFileDialog1";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = Color.LavenderBlush;
			base.ClientSize = new Size(1228, 425);
			base.Controls.Add(this.buttonClose);
			base.Controls.Add(this.listViewRules);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.menuStrip1);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox3);
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "FormMain1";
			base.StartPosition = 1;
			this.Text = "Продукционная оболочка Михайловой";
			base.Load += new EventHandler(this.FormMain1_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
