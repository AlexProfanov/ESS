using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Оболочка_Буряк
{
	public class Form1 : Form
	{
		private IContainer components = null;

		private ListView listViewRules;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private GroupBox groupBox2;

		private ListView listViewResolution;

		private ColumnHeader columnHeader2;

		private ListView listViewCondition;

		private ColumnHeader columnHeader1;

		private GroupBox groupBox3;

		private Button buttonRemoveRule;

		private Button buttonEditRule;

		private Button buttonAddRule;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem файлToolStripMenuItem;

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

		private OpenFileDialog openFileDialog;

		private SaveFileDialog saveFileDialog;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private ToolStripMenuItem создатьToolStripMenuItem;

		private Point poin;

		private ListViewItem itemAta = null;

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
			this.groupBox2 = new GroupBox();
			this.listViewResolution = new ListView();
			this.columnHeader2 = new ColumnHeader();
			this.listViewCondition = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.groupBox3 = new GroupBox();
			this.buttonRemoveRule = new Button();
			this.buttonEditRule = new Button();
			this.buttonAddRule = new Button();
			this.menuStrip1 = new MenuStrip();
			this.файлToolStripMenuItem = new ToolStripMenuItem();
			this.создатьToolStripMenuItem = new ToolStripMenuItem();
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
			this.openFileDialog = new OpenFileDialog();
			this.saveFileDialog = new SaveFileDialog();
			this.statusStrip1 = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
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
			this.listViewRules.Location = new Point(12, 26);
			this.listViewRules.MultiSelect = false;
			this.listViewRules.Name = "listViewRules";
			this.listViewRules.Size = new Size(647, 490);
			this.listViewRules.TabIndex = 13;
			this.listViewRules.UseCompatibleStateImageBehavior = false;
			this.listViewRules.View = 1;
			this.listViewRules.add_ItemDrag(new ItemDragEventHandler(this.listViewRules_ItemDrag));
			this.listViewRules.add_SelectedIndexChanged(new EventHandler(this.listViewRules_SelectedIndexChanged));
			this.listViewRules.add_DragDrop(new DragEventHandler(this.listViewRules_DragDrop));
			this.listViewRules.add_DragOver(new DragEventHandler(this.listViewRules_DragOver));
			this.listViewRules.add_DoubleClick(new EventHandler(this.listViewRules_DoubleClick));
			this.columnHeader3.Text = "Имя";
			this.columnHeader4.Text = "Содержание";
			this.columnHeader4.Width = 1000;
			this.groupBox2.Anchor = 11;
			this.groupBox2.Controls.Add(this.listViewResolution);
			this.groupBox2.Controls.Add(this.listViewCondition);
			this.groupBox2.Location = new Point(665, 146);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(210, 370);
			this.groupBox2.TabIndex = 15;
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
			this.listViewResolution.Location = new Point(5, 238);
			this.listViewResolution.MultiSelect = false;
			this.listViewResolution.Name = "listViewResolution";
			this.listViewResolution.Size = new Size(202, 126);
			this.listViewResolution.Sorting = 2;
			this.listViewResolution.TabIndex = 2;
			this.listViewResolution.UseCompatibleStateImageBehavior = false;
			this.listViewResolution.View = 1;
			this.listViewResolution.add_ItemDrag(new ItemDragEventHandler(this.listViewResolution_ItemDrag));
			this.listViewResolution.add_DragDrop(new DragEventHandler(this.listViewResolution_DragDrop));
			this.listViewResolution.add_DragOver(new DragEventHandler(this.listViewResolution_DragOver));
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
			this.listViewCondition.Size = new Size(202, 213);
			this.listViewCondition.Sorting = 2;
			this.listViewCondition.TabIndex = 0;
			this.listViewCondition.UseCompatibleStateImageBehavior = false;
			this.listViewCondition.View = 1;
			this.listViewCondition.add_ItemDrag(new ItemDragEventHandler(this.listViewCondition_ItemDrag));
			this.listViewCondition.add_DragDrop(new DragEventHandler(this.listViewCondition_DragDrop));
			this.listViewCondition.add_DragOver(new DragEventHandler(this.listViewCondition_DragOver));
			this.columnHeader1.Text = "Условие";
			this.columnHeader1.Width = 200;
			this.groupBox3.Anchor = 11;
			this.groupBox3.AutoSize = true;
			this.groupBox3.Controls.Add(this.buttonRemoveRule);
			this.groupBox3.Controls.Add(this.buttonEditRule);
			this.groupBox3.Controls.Add(this.buttonAddRule);
			this.groupBox3.Location = new Point(665, 26);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(213, 114);
			this.groupBox3.TabIndex = 14;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Действия";
			this.buttonRemoveRule.AutoSize = true;
			this.buttonRemoveRule.Location = new Point(3, 72);
			this.buttonRemoveRule.Name = "buttonRemoveRule";
			this.buttonRemoveRule.Size = new Size(204, 23);
			this.buttonRemoveRule.TabIndex = 2;
			this.buttonRemoveRule.Text = "Удалить";
			this.buttonRemoveRule.UseVisualStyleBackColor = true;
			this.buttonRemoveRule.add_Click(new EventHandler(this.buttonRemoveRule_Click));
			this.buttonEditRule.AutoSize = true;
			this.buttonEditRule.Location = new Point(3, 46);
			this.buttonEditRule.Name = "buttonEditRule";
			this.buttonEditRule.Size = new Size(204, 23);
			this.buttonEditRule.TabIndex = 1;
			this.buttonEditRule.Text = "Изменить";
			this.buttonEditRule.UseVisualStyleBackColor = true;
			this.buttonEditRule.add_Click(new EventHandler(this.buttonEditRule_Click));
			this.buttonAddRule.AutoSize = true;
			this.buttonAddRule.Location = new Point(3, 21);
			this.buttonAddRule.Name = "buttonAddRule";
			this.buttonAddRule.Size = new Size(204, 25);
			this.buttonAddRule.TabIndex = 0;
			this.buttonAddRule.Text = "Добавить";
			this.buttonAddRule.UseVisualStyleBackColor = true;
			this.buttonAddRule.add_Click(new EventHandler(this.buttonAddRule_Click));
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
			this.menuStrip1.Size = new Size(891, 23);
			this.menuStrip1.TabIndex = 17;
			this.menuStrip1.Text = "menuStrip1";
			this.файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.создатьToolStripMenuItem,
				this.открытьToolStripMenuItem,
				this.сохранитьКакToolStripMenuItem,
				this.toolStripSeparator1,
				this.выходToolStripMenuItem
			});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new Size(48, 19);
			this.файлToolStripMenuItem.Text = "Файл";
			this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
			this.создатьToolStripMenuItem.Size = new Size(153, 22);
			this.создатьToolStripMenuItem.Text = "Создать";
			this.создатьToolStripMenuItem.add_Click(new EventHandler(this.создатьToolStripMenuItem_Click));
			this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
			this.открытьToolStripMenuItem.Size = new Size(153, 22);
			this.открытьToolStripMenuItem.Text = "Открыть";
			this.открытьToolStripMenuItem.add_Click(new EventHandler(this.открытьToolStripMenuItem_Click));
			this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
			this.сохранитьКакToolStripMenuItem.Size = new Size(153, 22);
			this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
			this.сохранитьКакToolStripMenuItem.add_Click(new EventHandler(this.сохранитьКакToolStripMenuItem_Click));
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(150, 6);
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.Size = new Size(153, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.add_Click(new EventHandler(this.выходToolStripMenuItem_Click));
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
			this.доменыToolStripMenuItem.add_Click(new EventHandler(this.доменыToolStripMenuItem_Click));
			this.переменныеToolStripMenuItem.Name = "переменныеToolStripMenuItem";
			this.переменныеToolStripMenuItem.Size = new Size(146, 22);
			this.переменныеToolStripMenuItem.Text = "Переменные";
			this.переменныеToolStripMenuItem.add_Click(new EventHandler(this.переменныеToolStripMenuItem_Click));
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
			this.задатьЦельToolStripMenuItem.add_Click(new EventHandler(this.задатьЦельToolStripMenuItem_Click));
			this.начатьToolStripMenuItem.Name = "начатьToolStripMenuItem";
			this.начатьToolStripMenuItem.Size = new Size(139, 22);
			this.начатьToolStripMenuItem.Text = "Начать";
			this.начатьToolStripMenuItem.add_Click(new EventHandler(this.начатьToolStripMenuItem_Click));
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
			this.показатьToolStripMenuItem.add_Click(new EventHandler(this.показатьToolStripMenuItem_Click));
			this.openFileDialog.FileName = "openFileDialog1";
			this.statusStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripStatusLabel1
			});
			this.statusStrip1.Location = new Point(0, 519);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new Size(891, 22);
			this.statusStrip1.TabIndex = 18;
			this.statusStrip1.Text = "statusStrip1";
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new Size(0, 17);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			this.BackColor = SystemColors.Highlight;
			base.ClientSize = new Size(891, 541);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.menuStrip1);
			base.Controls.Add(this.listViewRules);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox3);
			base.Name = "Form1";
			this.Text = "Оболочка";
			base.add_FormClosing(new FormClosingEventHandler(this.Form1_FormClosing));
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public Form1()
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
					this.toolStripStatusLabel1.Text = "ЭС " + this.openFileDialog.FileName;
					this.printRuleList();
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
					this.toolStripStatusLabel1.Text = "ЭC " + this.saveFileDialog.FileName.ToString();
				}
				catch (ArgumentException ex)
				{
					int num = MessageBox.Show(ex.Message, "Ошибка", 0, 16);
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
			if (this.listViewRules.Items.Count > 1)
			{
				Global.knowledgeBase.insertRuleInto(this.listViewRules.Items.Count - 1, this.itemAta.Index + 1);
			}
			this.printRuleList();
			if (this.listViewRules.Items.Count > 1)
			{
				DialogFuncs.selectListViewItem(this.listViewRules, this.itemAta.Index + 1);
			}
			else
			{
				DialogFuncs.selectListViewItem(this.listViewRules, this.listViewRules.Items.Count - 1);
			}
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

		private void listViewRules_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewRules.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int ruleIndex = (int)enumerator.Current;
					this.showRuleInfo(ruleIndex);
					this.itemAta = this.listViewRules.SelectedItems[0];
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

		private void доменыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormDomain formDomain = new FormDomain();
			formDomain.ShowDialog();
		}

		private void переменныеToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int num = new FormVarList1().ShowDialog();
		}

		private void задатьЦельToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.setGoalForConsultation();
		}

		private void начатьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.doConsultation();
		}

		private void показатьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.exposition.showExposition();
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.createNewKnowledgeBase();
			this.saveKnowledgeBase();
		}

		private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.openKnowledgeBase();
		}

		private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.saveKnowledgeBase();
		}

		private void выходToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Хотите сохранить текущую базу перед закрытием?", "Внимание", 4, 32) == 6)
			{
				this.saveKnowledgeBase();
			}
			base.Close();
		}

		private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Хотите сохранить текущую базу перед закрытием?", "Внимание", 4, 32) == 6)
			{
				this.saveKnowledgeBase();
			}
			this.createNewKnowledgeBase();
			this.printRuleList();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Хотите сохранить текущую базу перед закрытием?", "Внимание", 4, 32) == 6)
			{
				this.saveKnowledgeBase();
			}
		}
	}
}
