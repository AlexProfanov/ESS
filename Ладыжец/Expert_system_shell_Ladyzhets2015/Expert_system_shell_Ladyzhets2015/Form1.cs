using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Expert_system_shell_Ladyzhets2015
{
	public class Form1 : Form
	{
		private ListViewItem lwi_r;

		private int glob_domainIndex = 0;

		private ListViewItem lwi_v;

		private IContainer components = null;

		private TabControl TabControl1;

		private TabPage tabVariables;

		private TabPage tabRules;

		private MenuStrip menuStrip1;

		private MenuStrip menuStrip2;

		private TabPage tabDomains;

		private ToolStripMenuItem консультацияToolStripMenuItem;

		private ToolStripMenuItem выборЦелиToolStripMenuItem;

		private ToolStripMenuItem начатьКонсультациюToolStripMenuItem;

		private ToolStripMenuItem объяснениеToolStripMenuItem;

		private ToolStripMenuItem показатьОбъяснениеToolStripMenuItem;

		private Label label1;

		private Label label2;

		private GroupBox groupBox1;

		private Button B_domain_delete;

		private Button B_domain_change;

		private Button B_domain_add;

		private GroupBox groupBox2;

		private Button B_variable_delete;

		private Button B_variable_change;

		private Button B_variable_add;

		private Label label3;

		private GroupBox groupBox3;

		private Label label7;

		private Label label6;

		private Label label5;

		private GroupBox groupBox4;

		private Button B_rule_delete;

		private Button B_rule_change;

		private Button B_rule_add;

		private GroupBox groupBox5;

		private Label label11;

		private Label label10;

		public ListView listViewDomain;

		private ListView listViewVar;

		private ListView listViewRules;

		private ListView listViewAllowedValues;

		private ListView listViewResolution;

		private ListView listViewCondition;

		private RichTextBox richTextBoxQuestion;

		private TextBox textBoxVarType;

		private TextBox textBoxDomain;

		private SaveFileDialog saveFileDialog;

		private OpenFileDialog openFileDialog;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		private ColumnHeader columnHeader10;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader12;

		private ColumnHeader columnHeader14;

		private ColumnHeader columnHeader13;

		private ToolStripMenuItem доменыToolStripMenuItem;

		private ToolStripMenuItem создатьToolStripMenuItem3;

		private ToolStripMenuItem изменитьToolStripMenuItem;

		private ToolStripMenuItem удалитьToolStripMenuItem;

		private ToolStripMenuItem переменныеToolStripMenuItem;

		private ToolStripMenuItem создатьToolStripMenuItem4;

		private ToolStripMenuItem изменитьToolStripMenuItem1;

		private ToolStripMenuItem удалитьToolStripMenuItem1;

		private ToolStripMenuItem правилаToolStripMenuItem;

		private ToolStripMenuItem создатьToolStripMenuItem5;

		private ToolStripMenuItem изменитьToolStripMenuItem2;

		private ToolStripMenuItem удалитьToolStripMenuItem5;

		private ToolStripMenuItem выходToolStripMenuItem1;

		private ToolStripMenuItem файлToolStripMenuItem;

		private ToolStripMenuItem создатьБЗToolStripMenuItem1;

		private ToolStripMenuItem открытьБЗToolStripMenuItem1;

		private ToolStripMenuItem сохранитьБЗToolStripMenuItem1;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem создатьБЗToolStripMenuItem;

		private ToolStripMenuItem открытьБЗToolStripMenuItem;

		private ToolStripMenuItem сохранитьБЗToolStripMenuItem;

		private ToolStripMenuItem доменыToolStripMenuItem1;

		private ToolStripMenuItem создатьToolStripMenuItem;

		private ToolStripMenuItem удалитьToolStripMenuItem2;

		private ToolStripMenuItem изменитьToolStripMenuItem4;

		private ToolStripMenuItem переменныеToolStripMenuItem1;

		private ToolStripMenuItem создатьToolStripMenuItem1;

		private ToolStripMenuItem удалитьToolStripMenuItem3;

		private ToolStripMenuItem изменитьToolStripMenuItem5;

		private ToolStripMenuItem правилаToolStripMenuItem1;

		private ToolStripMenuItem создатьToolStripMenuItem2;

		private ToolStripMenuItem изменитьToolStripMenuItem6;

		private ToolStripMenuItem удалитьToolStripMenuItem4;

		private ToolStripMenuItem выходToolStripMenuItem;

		private ToolStripMenuItem создатьБЗToolStripMenuItem2;

		private ToolStripMenuItem открытьБЗToolStripMenuItem2;

		private ToolStripMenuItem сохранитьБЗToolStripMenuItem2;

		private GroupBox groupBox6;

		private GroupBox groupBox7;

		private GroupBox groupBox8;

		public Form1()
		{
			this.InitializeComponent();
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
					int num = this.openFileDialog.FileName.LastIndexOf('\\');
					string text = this.openFileDialog.FileName.Substring(num + 1);
					this.Text = "Экспертная система " + text;
				}
				catch (ArgumentException ex)
				{
					int num2 = MessageBox.Show(ex.Message, "Ошибка", 0, 16);
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
					int num = this.saveFileDialog.FileName.LastIndexOf('\\');
					string text = this.saveFileDialog.FileName.Substring(num + 1);
					this.Text = "Экспертная система " + text;
				}
				catch (ArgumentException ex)
				{
					int num2 = MessageBox.Show(ex.Message, "Ошибка", 0, 16);
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
			new F_rule_add().createNewRule();
			this.printRuleList();
			if (this.listViewRules.Items.Count > 1)
			{
				Global.knowledgeBase.insertRuleInto(this.listViewRules.Items.Count - 1, this.lwi_r.Index + 1);
			}
			this.printRuleList();
			if (this.listViewRules.Items.Count > 1)
			{
				DialogFuncs.selectListViewItem(this.listViewRules, this.listViewRules.Items.Count - 1);
			}
			else
			{
				DialogFuncs.selectListViewItem(this.listViewRules, this.listViewRules.Items.Count);
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
					new F_rule_add().editRule(Global.knowledgeBase.getRuleAt(listViewItem.Index));
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
					if (MessageBox.Show("Вы действительно хотите удалить правило  ?", "Внимание", 4, 48) == 6)
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
			new F_SetGoal().setGoal();
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

		private void создатьБЗToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.createNewKnowledgeBase();
		}

		private void сохранитьБЗToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.saveKnowledgeBase();
		}

		private void открытьБЗToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.openKnowledgeBase();
			this.printDomainList();
			this.printVariableList();
			this.printRuleList();
		}

		private void выходToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
		{
		}

		private void изменитьToolStripMenuItem4_Click(object sender, EventArgs e)
		{
		}

		private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)
		{
		}

		private void удалитьToolStripMenuItem3_Click(object sender, EventArgs e)
		{
		}

		private void изменитьToolStripMenuItem5_Click(object sender, EventArgs e)
		{
		}

		private void создатьToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			this.addRule();
		}

		private void изменитьToolStripMenuItem6_Click(object sender, EventArgs e)
		{
			this.editRule();
		}

		private void удалитьToolStripMenuItem4_Click(object sender, EventArgs e)
		{
			this.removeRule();
		}

		private void B_domain_add_Click(object sender, EventArgs e)
		{
			new F_domain_add().addDomain();
			this.printDomainList();
			if (this.glob_domainIndex > 0)
			{
				Global.knowledgeBase.insertDomainInto(this.listViewDomain.Items.Count - 1, this.glob_domainIndex);
			}
			this.printDomainList();
			DialogFuncs.selectListViewItem(this.listViewDomain, this.listViewDomain.Items.Count - 1);
		}

		private void tabDomains_Click(object sender, EventArgs e)
		{
			this.printDomainList();
		}

		private void printDomainList()
		{
			int indexToSelect = 0;
			if (this.listViewDomain.SelectedIndices.Count > 0)
			{
				indexToSelect = this.listViewDomain.SelectedIndices[0];
			}
			this.listViewDomain.Items.Clear();
			IEnumerator<Enumeration> enumeratorForEnumerations = Global.knowledgeBase.getEnumeratorForEnumerations();
			int num = 1;
			while (enumeratorForEnumerations.MoveNext())
			{
				ListView.ListViewItemCollection arg_9E_0 = this.listViewDomain.Items;
				ListViewItem listViewItem = new ListViewItem(num.ToString());
				listViewItem.SubItems.Add(enumeratorForEnumerations.Current.ToString());
				listViewItem.SubItems.Add(DialogFuncs.printType(enumeratorForEnumerations.Current.DomainType));
				arg_9E_0.Add(listViewItem);
				num++;
			}
			DialogFuncs.selectListViewItem(this.listViewDomain, indexToSelect);
		}

		private void showDomainInfo(int domainIndex)
		{
			if (0 <= domainIndex && domainIndex < this.listViewDomain.Items.Count)
			{
				this.listViewAllowedValues.Items.Clear();
				IEnumerator<Value> enumeratorForValues = Global.knowledgeBase.getEnumerationAt(domainIndex).getEnumeratorForValues();
				int num = 1;
				while (enumeratorForValues.MoveNext())
				{
					ListView.ListViewItemCollection arg_78_0 = this.listViewAllowedValues.Items;
					ListViewItem listViewItem = new ListViewItem(num.ToString());
					listViewItem.SubItems.Add(enumeratorForValues.Current.ToString());
					arg_78_0.Add(listViewItem);
					num++;
				}
				this.listViewAllowedValues.SelectedIndices.Add(0);
			}
		}

		private void B_domain_change_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					new F_domain_add().editDomain(Global.knowledgeBase.getEnumerationAt(enumerationIndex));
					this.printDomainList();
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

		private void B_domain_delete_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					if (MessageBox.Show("Вы действительно хотите удалить домен с именем " + Global.knowledgeBase.getEnumerationAt(enumerationIndex), "Внимание", 4, 32) == 6)
					{
						Global.knowledgeBase.removeEnumerationAt(enumerationIndex);
						this.printDomainList();
					}
					DialogFuncs.selectListViewItem(this.listViewDomain, 0);
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

		private void listViewDomain_DoubleClick(object sender, EventArgs e)
		{
		}

		private void listViewDomain_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = this.listViewDomain.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewDomain.GetItemAt(point.X, point.Y);
				if (itemAt != null)
				{
					if (e.Effect == 2)
					{
						Global.knowledgeBase.insertDomainInto(listViewItem.Index, itemAt.Index);
					}
					else if (e.Effect == 1)
					{
						Global.knowledgeBase.switchEnumerations(listViewItem.Index, itemAt.Index);
					}
					this.printDomainList();
				}
			}
		}

		private void listViewDomain_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewDomain, e);
		}

		private void listViewDomain_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewDomain.AllowDrop = true;
			this.listViewAllowedValues.AllowDrop = false;
			DialogFuncs.doDragBeginning(this.listViewDomain);
		}

		private void listViewDomain_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int domainIndex = (int)enumerator.Current;
					this.showDomainInfo(domainIndex);
					this.glob_domainIndex = domainIndex;
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

		private void listView_domains_values_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = this.listViewAllowedValues.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewAllowedValues.GetItemAt(point.X, point.Y);
				if (this.listViewDomain.SelectedIndices.Count > 0)
				{
					int num = this.listViewDomain.SelectedIndices[0];
					Enumeration enumerationAt = Global.knowledgeBase.getEnumerationAt(num);
					if (itemAt != null)
					{
						if (e.Effect == 2)
						{
							enumerationAt.insertValueInto(listViewItem.Index, itemAt.Index);
						}
						else if (e.Effect == 1)
						{
							enumerationAt.switchValues(listViewItem.Index, itemAt.Index);
						}
						this.showDomainInfo(num);
					}
				}
			}
		}

		private void listView_domains_values_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewAllowedValues, e);
		}

		private void listView_domains_values_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewDomain.AllowDrop = false;
			this.listViewAllowedValues.AllowDrop = true;
			DialogFuncs.doDragBeginning(this.listViewAllowedValues);
		}

		private void TabControl1_Click(object sender, EventArgs e)
		{
		}

		private void B_rule_add_Click(object sender, EventArgs e)
		{
			this.addRule();
		}

		private void B_rule_change_Click(object sender, EventArgs e)
		{
			this.editRule();
		}

		private void B_rule_delete_Click(object sender, EventArgs e)
		{
			this.removeRule();
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

		private void listViewResolution_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewRules.AllowDrop = false;
			this.listViewCondition.AllowDrop = false;
			this.listViewResolution.AllowDrop = true;
			DialogFuncs.doDragBeginning(this.listViewResolution);
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

		private void listViewRules_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewRules, e);
		}

		private void listViewRules_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewRules.AllowDrop = true;
			this.listViewCondition.AllowDrop = false;
			this.listViewResolution.AllowDrop = false;
			DialogFuncs.doDragBeginning(this.listViewRules);
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

		private void listViewCondition_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewCondition, e);
		}

		private void listViewCondition_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this.listViewRules.AllowDrop = false;
			this.listViewCondition.AllowDrop = true;
			this.listViewResolution.AllowDrop = false;
			DialogFuncs.doDragBeginning(this.listViewCondition);
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
					this.lwi_r = this.listViewRules.SelectedItems[0];
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

		private void начатьКонсультациюToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.doConsultation();
		}

		private void выборЦелиToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.setGoalForConsultation();
		}

		private void показатьОбъяснениеToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.exposition.showExposition();
		}

		private void tabVariables_Click(object sender, EventArgs e)
		{
			this.printVariableList();
		}

		private void printVariableList()
		{
			int indexToSelect = 0;
			if (this.listViewVar.SelectedIndices.Count > 0)
			{
				indexToSelect = this.listViewVar.SelectedIndices[0];
			}
			this.listViewVar.Items.Clear();
			IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			int num = 1;
			while (enumeratorForVariables.MoveNext())
			{
				ListViewItem listViewItem = new ListViewItem(num.ToString());
				listViewItem.SubItems.Add(enumeratorForVariables.Current.Name);
				if (enumeratorForVariables.Current is DeducibleVariable)
				{
					listViewItem.SubItems.Add("Выводимая");
				}
				else if (enumeratorForVariables.Current is AskedVariable)
				{
					listViewItem.SubItems.Add("Запрашиваемая");
				}
				else if (enumeratorForVariables.Current is DeducibleAskedVariable)
				{
					listViewItem.SubItems.Add("Вывод.-запр.");
				}
				if (enumeratorForVariables.Current.Domain != null)
				{
					listViewItem.SubItems.Add(enumeratorForVariables.Current.Domain.ToString());
				}
				else
				{
					listViewItem.SubItems.Add("-");
				}
				listViewItem.SubItems.Add(DialogFuncs.printType(enumeratorForVariables.Current.VarType));
				this.listViewVar.Items.Add(listViewItem);
				num++;
			}
			DialogFuncs.selectListViewItem(this.listViewVar, indexToSelect);
		}

		private void showVarInfo(int varIndex)
		{
			if (0 <= varIndex && varIndex < this.listViewVar.Items.Count)
			{
				Variable variableAt = Global.knowledgeBase.getVariableAt(varIndex);
				if (variableAt.Domain != null)
				{
					this.textBoxDomain.Text = variableAt.Domain.Name;
				}
				else
				{
					this.textBoxDomain.Text = "Отсутствует";
				}
				this.richTextBoxQuestion.Text = "";
				if (variableAt is DeducibleVariable)
				{
					this.textBoxVarType.Text = "Выводимая";
					this.richTextBoxQuestion.Enabled = false;
				}
				else if (variableAt is DeducibleAskedVariable)
				{
					this.textBoxVarType.Text = "Выводимо-запрашиваемая";
					this.richTextBoxQuestion.Text = (variableAt as DeducibleAskedVariable).Question;
				}
				else if (variableAt is AskedVariable)
				{
					this.textBoxVarType.Text = "Запрашиваемая";
					this.richTextBoxQuestion.Text = (variableAt as AskedVariable).Question;
				}
			}
		}

		private void listViewVar_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				ListViewItem listViewItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				Point point = this.listViewVar.PointToClient(new Point(e.X, e.Y));
				ListViewItem itemAt = this.listViewVar.GetItemAt(point.X, point.Y);
				if (itemAt != null)
				{
					if (e.Effect == 2)
					{
						Global.knowledgeBase.insertVariableInto(listViewItem.Index, itemAt.Index);
					}
					else if (e.Effect == 1)
					{
						Global.knowledgeBase.switchVariables(listViewItem.Index, itemAt.Index);
					}
					this.printVariableList();
				}
			}
		}

		private void listViewVar_DragOver(object sender, DragEventArgs e)
		{
			DialogFuncs.doDragOver(this.listViewVar, e);
		}

		private void listViewVar_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DialogFuncs.doDragBeginning(this.listViewVar);
		}

		private void listViewVar_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewVar.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int varIndex = (int)enumerator.Current;
					this.showVarInfo(varIndex);
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

		private void B_variable_add_Click(object sender, EventArgs e)
		{
			try
			{
				new F_variable_add().addVariable();
				this.printVariableList();
				DialogFuncs.selectListViewItem(this.listViewVar, this.listViewVar.Items.Count - 1);
			}
			catch
			{
			}
		}

		private void B_variable_change_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewVar.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int variableIndex = (int)enumerator.Current;
					new F_variable_add().editVariable(Global.knowledgeBase.getVariableAt(variableIndex));
					this.printVariableList();
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

		private void B_variable_delete_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewVar.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int num = (int)enumerator.Current;
					if (MessageBox.Show("Вы действительно хотите удалить переменную с именем " + Global.knowledgeBase.getVariableAt(num).Name + " ? Удаление переменной приведет к удалению посылок и заключений, которые ее используют.", "Внимание", 4, 32) == 6)
					{
						Global.knowledgeBase.removeVariableAt(num);
						this.printVariableList();
						DialogFuncs.selectListViewItem(this.listViewVar, 0);
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

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void создатьБЗToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.createNewKnowledgeBase();
		}

		private void открытьБЗToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.openKnowledgeBase();
			this.printDomainList();
			this.printVariableList();
			this.printRuleList();
		}

		private void сохранитьБЗToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.saveKnowledgeBase();
		}

		private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					new F_domain_add().editDomain(Global.knowledgeBase.getEnumerationAt(enumerationIndex));
					this.printDomainList();
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

		private void создатьToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			new F_domain_add().addDomain();
			this.printDomainList();
			DialogFuncs.selectListViewItem(this.listViewDomain, this.listViewDomain.Items.Count - 1);
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					if (MessageBox.Show("Вы действительно хотите удалить домен с именем " + Global.knowledgeBase.getEnumerationAt(enumerationIndex), "Внимание", 4, 32) == 6)
					{
						Global.knowledgeBase.removeEnumerationAt(enumerationIndex);
						this.printDomainList();
					}
					DialogFuncs.selectListViewItem(this.listViewDomain, 0);
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

		private void создатьToolStripMenuItem4_Click(object sender, EventArgs e)
		{
			new F_variable_add().addVariable();
			this.printVariableList();
			DialogFuncs.selectListViewItem(this.listViewVar, this.listViewVar.Items.Count - 1);
		}

		private void изменитьToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					new F_domain_add().editDomain(Global.knowledgeBase.getEnumerationAt(enumerationIndex));
					this.printDomainList();
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

		private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listViewDomain.SelectedIndices.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					int enumerationIndex = (int)enumerator.Current;
					if (MessageBox.Show("Вы действительно хотите удалить домен с именем " + Global.knowledgeBase.getEnumerationAt(enumerationIndex), "Внимание", 4, 32) == 6)
					{
						Global.knowledgeBase.removeEnumerationAt(enumerationIndex);
						this.printDomainList();
					}
					DialogFuncs.selectListViewItem(this.listViewDomain, 0);
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

		private void создатьToolStripMenuItem5_Click(object sender, EventArgs e)
		{
			this.addRule();
		}

		private void изменитьToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			this.editRule();
		}

		private void удалитьToolStripMenuItem5_Click(object sender, EventArgs e)
		{
			this.removeRule();
		}

		private void доменыToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.TabControl1.SelectedIndex = 0;
		}

		private void переменныеToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.TabControl1.SelectedIndex = 1;
		}

		private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.TabControl1.SelectedIndex = 2;
		}

		private void доменыToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
		{
			this.TabControl1.SelectedIndex = 0;
		}

		private void доменыToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
		{
			this.TabControl1.SelectedIndex = 0;
		}

		private void переменныеToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
		{
			this.TabControl1.SelectedIndex = 1;
		}

		private void правилаToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
		{
			this.TabControl1.SelectedIndex = 2;
		}

		private void создатьБЗToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			this.createNewKnowledgeBase();
			this.listViewRules.Clear();
			this.listViewVar.Clear();
			this.listViewDomain.Clear();
			this.listViewAllowedValues.Clear();
			this.saveKnowledgeBase();
		}

		private void открытьБЗToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			this.openKnowledgeBase();
			this.printDomainList();
			this.printRuleList();
			this.printVariableList();
		}

		private void сохранитьБЗToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			this.saveKnowledgeBase();
		}

		private void richTextBoxQuestion_TextChanged(object sender, EventArgs e)
		{
		}

		private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вы действительно хотите выйти?", "Внимание", 4, 32) == 6)
			{
				base.Close();
			}
		}

		private void Form1_Load_1(object sender, EventArgs e)
		{
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Вы выходите. Желаете сохранить изменения?", "Внимание", 4, 32) == 6)
			{
				this.saveKnowledgeBase();
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
			this.TabControl1 = new TabControl();
			this.tabDomains = new TabPage();
			this.groupBox6 = new GroupBox();
			this.listViewAllowedValues = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.groupBox1 = new GroupBox();
			this.B_domain_delete = new Button();
			this.B_domain_change = new Button();
			this.B_domain_add = new Button();
			this.label2 = new Label();
			this.listViewDomain = new ListView();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.label1 = new Label();
			this.tabVariables = new TabPage();
			this.groupBox8 = new GroupBox();
			this.groupBox2 = new GroupBox();
			this.B_variable_delete = new Button();
			this.B_variable_change = new Button();
			this.B_variable_add = new Button();
			this.groupBox3 = new GroupBox();
			this.richTextBoxQuestion = new RichTextBox();
			this.textBoxVarType = new TextBox();
			this.textBoxDomain = new TextBox();
			this.label7 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.listViewVar = new ListView();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.label3 = new Label();
			this.tabRules = new TabPage();
			this.groupBox7 = new GroupBox();
			this.groupBox5 = new GroupBox();
			this.listViewResolution = new ListView();
			this.columnHeader14 = new ColumnHeader();
			this.listViewCondition = new ListView();
			this.columnHeader13 = new ColumnHeader();
			this.label11 = new Label();
			this.label10 = new Label();
			this.groupBox4 = new GroupBox();
			this.B_rule_delete = new Button();
			this.B_rule_change = new Button();
			this.B_rule_add = new Button();
			this.listViewRules = new ListView();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader12 = new ColumnHeader();
			this.menuStrip1 = new MenuStrip();
			this.menuStrip2 = new MenuStrip();
			this.файлToolStripMenuItem = new ToolStripMenuItem();
			this.создатьБЗToolStripMenuItem2 = new ToolStripMenuItem();
			this.открытьБЗToolStripMenuItem2 = new ToolStripMenuItem();
			this.сохранитьБЗToolStripMenuItem2 = new ToolStripMenuItem();
			this.доменыToolStripMenuItem = new ToolStripMenuItem();
			this.создатьToolStripMenuItem3 = new ToolStripMenuItem();
			this.изменитьToolStripMenuItem = new ToolStripMenuItem();
			this.удалитьToolStripMenuItem = new ToolStripMenuItem();
			this.переменныеToolStripMenuItem = new ToolStripMenuItem();
			this.создатьToolStripMenuItem4 = new ToolStripMenuItem();
			this.изменитьToolStripMenuItem1 = new ToolStripMenuItem();
			this.удалитьToolStripMenuItem1 = new ToolStripMenuItem();
			this.правилаToolStripMenuItem = new ToolStripMenuItem();
			this.создатьToolStripMenuItem5 = new ToolStripMenuItem();
			this.изменитьToolStripMenuItem2 = new ToolStripMenuItem();
			this.удалитьToolStripMenuItem5 = new ToolStripMenuItem();
			this.консультацияToolStripMenuItem = new ToolStripMenuItem();
			this.выборЦелиToolStripMenuItem = new ToolStripMenuItem();
			this.начатьКонсультациюToolStripMenuItem = new ToolStripMenuItem();
			this.объяснениеToolStripMenuItem = new ToolStripMenuItem();
			this.показатьОбъяснениеToolStripMenuItem = new ToolStripMenuItem();
			this.выходToolStripMenuItem1 = new ToolStripMenuItem();
			this.saveFileDialog = new SaveFileDialog();
			this.openFileDialog = new OpenFileDialog();
			this.создатьБЗToolStripMenuItem1 = new ToolStripMenuItem();
			this.открытьБЗToolStripMenuItem1 = new ToolStripMenuItem();
			this.сохранитьБЗToolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.создатьБЗToolStripMenuItem = new ToolStripMenuItem();
			this.открытьБЗToolStripMenuItem = new ToolStripMenuItem();
			this.сохранитьБЗToolStripMenuItem = new ToolStripMenuItem();
			this.доменыToolStripMenuItem1 = new ToolStripMenuItem();
			this.создатьToolStripMenuItem = new ToolStripMenuItem();
			this.удалитьToolStripMenuItem2 = new ToolStripMenuItem();
			this.изменитьToolStripMenuItem4 = new ToolStripMenuItem();
			this.переменныеToolStripMenuItem1 = new ToolStripMenuItem();
			this.создатьToolStripMenuItem1 = new ToolStripMenuItem();
			this.удалитьToolStripMenuItem3 = new ToolStripMenuItem();
			this.изменитьToolStripMenuItem5 = new ToolStripMenuItem();
			this.правилаToolStripMenuItem1 = new ToolStripMenuItem();
			this.создатьToolStripMenuItem2 = new ToolStripMenuItem();
			this.изменитьToolStripMenuItem6 = new ToolStripMenuItem();
			this.удалитьToolStripMenuItem4 = new ToolStripMenuItem();
			this.выходToolStripMenuItem = new ToolStripMenuItem();
			this.TabControl1.SuspendLayout();
			this.tabDomains.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabVariables.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabRules.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.menuStrip2.SuspendLayout();
			base.SuspendLayout();
			this.TabControl1.Anchor = 15;
			this.TabControl1.Controls.Add(this.tabDomains);
			this.TabControl1.Controls.Add(this.tabVariables);
			this.TabControl1.Controls.Add(this.tabRules);
			this.TabControl1.Location = new Point(12, 22);
			this.TabControl1.Name = "TabControl1";
			this.TabControl1.SelectedIndex = 0;
			this.TabControl1.Size = new Size(855, 403);
			this.TabControl1.TabIndex = 0;
			this.TabControl1.Click += new EventHandler(this.TabControl1_Click);
			this.tabDomains.Controls.Add(this.groupBox6);
			this.tabDomains.Controls.Add(this.listViewDomain);
			this.tabDomains.Controls.Add(this.label1);
			this.tabDomains.Location = new Point(4, 22);
			this.tabDomains.Name = "tabDomains";
			this.tabDomains.Size = new Size(847, 377);
			this.tabDomains.TabIndex = 2;
			this.tabDomains.Text = "Домены";
			this.tabDomains.UseVisualStyleBackColor = true;
			this.tabDomains.Click += new EventHandler(this.tabDomains_Click);
			this.groupBox6.Controls.Add(this.listViewAllowedValues);
			this.groupBox6.Controls.Add(this.groupBox1);
			this.groupBox6.Controls.Add(this.label2);
			this.groupBox6.Dock = 4;
			this.groupBox6.Location = new Point(553, 0);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new Size(294, 377);
			this.groupBox6.TabIndex = 7;
			this.groupBox6.TabStop = false;
			this.listViewAllowedValues.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewAllowedValues.FullRowSelect = true;
			this.listViewAllowedValues.GridLines = true;
			this.listViewAllowedValues.HeaderStyle = 1;
			this.listViewAllowedValues.Location = new Point(28, 195);
			this.listViewAllowedValues.MultiSelect = false;
			this.listViewAllowedValues.Name = "listViewAllowedValues";
			this.listViewAllowedValues.Size = new Size(260, 179);
			this.listViewAllowedValues.TabIndex = 6;
			this.listViewAllowedValues.UseCompatibleStateImageBehavior = false;
			this.listViewAllowedValues.View = 1;
			this.listViewAllowedValues.ItemDrag += new ItemDragEventHandler(this.listView_domains_values_ItemDrag);
			this.listViewAllowedValues.DragDrop += new DragEventHandler(this.listView_domains_values_DragDrop);
			this.listViewAllowedValues.DragOver += new DragEventHandler(this.listView_domains_values_DragOver);
			this.columnHeader1.Text = "№";
			this.columnHeader2.Text = "Значение";
			this.columnHeader2.Width = 195;
			this.groupBox1.Controls.Add(this.B_domain_delete);
			this.groupBox1.Controls.Add(this.B_domain_change);
			this.groupBox1.Controls.Add(this.B_domain_add);
			this.groupBox1.Location = new Point(25, 28);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(263, 115);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Действия";
			this.B_domain_delete.Location = new Point(15, 78);
			this.B_domain_delete.Name = "B_domain_delete";
			this.B_domain_delete.Size = new Size(180, 23);
			this.B_domain_delete.TabIndex = 2;
			this.B_domain_delete.Text = "Удалить";
			this.B_domain_delete.UseVisualStyleBackColor = true;
			this.B_domain_delete.Click += new EventHandler(this.B_domain_delete_Click);
			this.B_domain_change.Location = new Point(15, 48);
			this.B_domain_change.Name = "B_domain_change";
			this.B_domain_change.Size = new Size(180, 23);
			this.B_domain_change.TabIndex = 1;
			this.B_domain_change.Text = "Изменить";
			this.B_domain_change.UseVisualStyleBackColor = true;
			this.B_domain_change.Click += new EventHandler(this.B_domain_change_Click);
			this.B_domain_add.Location = new Point(15, 18);
			this.B_domain_add.Name = "B_domain_add";
			this.B_domain_add.Size = new Size(180, 23);
			this.B_domain_add.TabIndex = 0;
			this.B_domain_add.Text = "Добавить";
			this.B_domain_add.UseVisualStyleBackColor = true;
			this.B_domain_add.Click += new EventHandler(this.B_domain_add_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(25, 161);
			this.label2.Name = "label2";
			this.label2.Size = new Size(125, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Допустимые значения:";
			this.listViewDomain.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader3,
				this.columnHeader4,
				this.columnHeader5
			});
			this.listViewDomain.Dock = 5;
			this.listViewDomain.FullRowSelect = true;
			this.listViewDomain.GridLines = true;
			this.listViewDomain.HeaderStyle = 1;
			this.listViewDomain.Location = new Point(0, 0);
			this.listViewDomain.MultiSelect = false;
			this.listViewDomain.Name = "listViewDomain";
			this.listViewDomain.Size = new Size(847, 377);
			this.listViewDomain.TabIndex = 5;
			this.listViewDomain.UseCompatibleStateImageBehavior = false;
			this.listViewDomain.View = 1;
			this.listViewDomain.ItemDrag += new ItemDragEventHandler(this.listViewDomain_ItemDrag);
			this.listViewDomain.SelectedIndexChanged += new EventHandler(this.listViewDomain_SelectedIndexChanged);
			this.listViewDomain.DragDrop += new DragEventHandler(this.listViewDomain_DragDrop);
			this.listViewDomain.DragOver += new DragEventHandler(this.listViewDomain_DragOver);
			this.listViewDomain.DoubleClick += new EventHandler(this.listViewDomain_DoubleClick);
			this.columnHeader3.Text = "№";
			this.columnHeader4.Text = "Имя";
			this.columnHeader4.Width = 439;
			this.columnHeader5.Text = "Тип";
			this.columnHeader5.Width = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new Size(94, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Список доменов:";
			this.tabVariables.Controls.Add(this.groupBox8);
			this.tabVariables.Controls.Add(this.listViewVar);
			this.tabVariables.Controls.Add(this.label3);
			this.tabVariables.Location = new Point(4, 22);
			this.tabVariables.Name = "tabVariables";
			this.tabVariables.Padding = new Padding(3);
			this.tabVariables.Size = new Size(847, 377);
			this.tabVariables.TabIndex = 0;
			this.tabVariables.Text = "Переменные";
			this.tabVariables.UseVisualStyleBackColor = true;
			this.tabVariables.Click += new EventHandler(this.tabVariables_Click);
			this.groupBox8.Controls.Add(this.groupBox2);
			this.groupBox8.Controls.Add(this.groupBox3);
			this.groupBox8.Dock = 4;
			this.groupBox8.Location = new Point(524, 3);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new Size(320, 371);
			this.groupBox8.TabIndex = 9;
			this.groupBox8.TabStop = false;
			this.groupBox2.Controls.Add(this.B_variable_delete);
			this.groupBox2.Controls.Add(this.B_variable_change);
			this.groupBox2.Controls.Add(this.B_variable_add);
			this.groupBox2.Location = new Point(6, 16);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(210, 115);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Действия:";
			this.B_variable_delete.Location = new Point(7, 80);
			this.B_variable_delete.Name = "B_variable_delete";
			this.B_variable_delete.Size = new Size(180, 23);
			this.B_variable_delete.TabIndex = 2;
			this.B_variable_delete.Text = "Удалить";
			this.B_variable_delete.UseVisualStyleBackColor = true;
			this.B_variable_delete.Click += new EventHandler(this.B_variable_delete_Click);
			this.B_variable_change.Location = new Point(7, 50);
			this.B_variable_change.Name = "B_variable_change";
			this.B_variable_change.Size = new Size(180, 23);
			this.B_variable_change.TabIndex = 1;
			this.B_variable_change.Text = "Изменить";
			this.B_variable_change.UseVisualStyleBackColor = true;
			this.B_variable_change.Click += new EventHandler(this.B_variable_change_Click);
			this.B_variable_add.Location = new Point(7, 20);
			this.B_variable_add.Name = "B_variable_add";
			this.B_variable_add.Size = new Size(180, 23);
			this.B_variable_add.TabIndex = 0;
			this.B_variable_add.Text = "Добавить";
			this.B_variable_add.UseVisualStyleBackColor = true;
			this.B_variable_add.Click += new EventHandler(this.B_variable_add_Click);
			this.groupBox3.Controls.Add(this.richTextBoxQuestion);
			this.groupBox3.Controls.Add(this.textBoxVarType);
			this.groupBox3.Controls.Add(this.textBoxDomain);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Location = new Point(6, 133);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(308, 237);
			this.groupBox3.TabIndex = 7;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Переменная:";
			this.richTextBoxQuestion.Location = new Point(11, 131);
			this.richTextBoxQuestion.Name = "richTextBoxQuestion";
			this.richTextBoxQuestion.ReadOnly = true;
			this.richTextBoxQuestion.Size = new Size(280, 68);
			this.richTextBoxQuestion.TabIndex = 11;
			this.richTextBoxQuestion.Text = "";
			this.richTextBoxQuestion.TextChanged += new EventHandler(this.richTextBoxQuestion_TextChanged);
			this.textBoxVarType.Location = new Point(11, 92);
			this.textBoxVarType.Name = "textBoxVarType";
			this.textBoxVarType.ReadOnly = true;
			this.textBoxVarType.Size = new Size(280, 20);
			this.textBoxVarType.TabIndex = 10;
			this.textBoxDomain.Location = new Point(8, 53);
			this.textBoxDomain.Name = "textBoxDomain";
			this.textBoxDomain.ReadOnly = true;
			this.textBoxDomain.Size = new Size(283, 20);
			this.textBoxDomain.TabIndex = 9;
			this.label7.AutoSize = true;
			this.label7.Location = new Point(8, 115);
			this.label7.Name = "label7";
			this.label7.Size = new Size(47, 13);
			this.label7.TabIndex = 8;
			this.label7.Text = "Вопрос:";
			this.label6.AutoSize = true;
			this.label6.Location = new Point(6, 76);
			this.label6.Name = "label6";
			this.label6.Size = new Size(115, 13);
			this.label6.TabIndex = 7;
			this.label6.Text = "Способ означивания:";
			this.label5.AutoSize = true;
			this.label5.Location = new Point(8, 28);
			this.label5.Name = "label5";
			this.label5.Size = new Size(45, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "Домен:";
			this.listViewVar.AllowDrop = true;
			this.listViewVar.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader6,
				this.columnHeader7,
				this.columnHeader8,
				this.columnHeader9,
				this.columnHeader10
			});
			this.listViewVar.Dock = 5;
			this.listViewVar.FullRowSelect = true;
			this.listViewVar.GridLines = true;
			this.listViewVar.HeaderStyle = 1;
			this.listViewVar.Location = new Point(3, 3);
			this.listViewVar.MultiSelect = false;
			this.listViewVar.Name = "listViewVar";
			this.listViewVar.Size = new Size(841, 371);
			this.listViewVar.TabIndex = 8;
			this.listViewVar.UseCompatibleStateImageBehavior = false;
			this.listViewVar.View = 1;
			this.listViewVar.ItemDrag += new ItemDragEventHandler(this.listViewVar_ItemDrag);
			this.listViewVar.SelectedIndexChanged += new EventHandler(this.listViewVar_SelectedIndexChanged);
			this.listViewVar.DragDrop += new DragEventHandler(this.listViewVar_DragDrop);
			this.listViewVar.DragOver += new DragEventHandler(this.listViewVar_DragOver);
			this.columnHeader6.Text = "№";
			this.columnHeader7.Text = "Имя";
			this.columnHeader7.Width = 150;
			this.columnHeader8.Text = "Означивание";
			this.columnHeader8.Width = 140;
			this.columnHeader9.Text = "Домен";
			this.columnHeader9.Width = 150;
			this.columnHeader10.Text = "Тип";
			this.columnHeader10.Width = 0;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(6, 4);
			this.label3.Name = "label3";
			this.label3.Size = new Size(113, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Список переменных:";
			this.tabRules.Controls.Add(this.groupBox7);
			this.tabRules.Controls.Add(this.listViewRules);
			this.tabRules.Location = new Point(4, 22);
			this.tabRules.Name = "tabRules";
			this.tabRules.Padding = new Padding(3);
			this.tabRules.Size = new Size(847, 377);
			this.tabRules.TabIndex = 1;
			this.tabRules.Text = "Правила";
			this.tabRules.UseVisualStyleBackColor = true;
			this.groupBox7.Controls.Add(this.groupBox5);
			this.groupBox7.Controls.Add(this.groupBox4);
			this.groupBox7.Dock = 4;
			this.groupBox7.Location = new Point(511, 3);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new Size(333, 371);
			this.groupBox7.TabIndex = 4;
			this.groupBox7.TabStop = false;
			this.groupBox5.Controls.Add(this.listViewResolution);
			this.groupBox5.Controls.Add(this.listViewCondition);
			this.groupBox5.Controls.Add(this.label11);
			this.groupBox5.Controls.Add(this.label10);
			this.groupBox5.Location = new Point(8, 145);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new Size(325, 213);
			this.groupBox5.TabIndex = 5;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Текущее правило:";
			this.listViewResolution.Activation = 1;
			this.listViewResolution.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader14
			});
			this.listViewResolution.FullRowSelect = true;
			this.listViewResolution.GridLines = true;
			this.listViewResolution.HeaderStyle = 1;
			this.listViewResolution.Location = new Point(10, 121);
			this.listViewResolution.MultiSelect = false;
			this.listViewResolution.Name = "listViewResolution";
			this.listViewResolution.Size = new Size(309, 86);
			this.listViewResolution.TabIndex = 3;
			this.listViewResolution.UseCompatibleStateImageBehavior = false;
			this.listViewResolution.View = 1;
			this.listViewResolution.ItemDrag += new ItemDragEventHandler(this.listViewResolution_ItemDrag);
			this.listViewResolution.DragDrop += new DragEventHandler(this.listViewResolution_DragDrop);
			this.listViewResolution.DragOver += new DragEventHandler(this.listViewResolution_DragOver);
			this.columnHeader14.Text = "Заключение";
			this.columnHeader14.Width = 294;
			this.listViewCondition.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader13
			});
			this.listViewCondition.FullRowSelect = true;
			this.listViewCondition.GridLines = true;
			this.listViewCondition.HeaderStyle = 1;
			this.listViewCondition.Location = new Point(10, 37);
			this.listViewCondition.MultiSelect = false;
			this.listViewCondition.Name = "listViewCondition";
			this.listViewCondition.Size = new Size(309, 64);
			this.listViewCondition.TabIndex = 2;
			this.listViewCondition.UseCompatibleStateImageBehavior = false;
			this.listViewCondition.View = 1;
			this.listViewCondition.ItemDrag += new ItemDragEventHandler(this.listViewCondition_ItemDrag);
			this.listViewCondition.DragDrop += new DragEventHandler(this.listViewCondition_DragDrop);
			this.listViewCondition.DragOver += new DragEventHandler(this.listViewCondition_DragOver);
			this.columnHeader13.Text = "Условие";
			this.columnHeader13.Width = 303;
			this.label11.AutoSize = true;
			this.label11.Location = new Point(7, 104);
			this.label11.Name = "label11";
			this.label11.Size = new Size(72, 13);
			this.label11.TabIndex = 1;
			this.label11.Text = "Заключение:";
			this.label10.AutoSize = true;
			this.label10.Location = new Point(7, 20);
			this.label10.Name = "label10";
			this.label10.Size = new Size(54, 13);
			this.label10.TabIndex = 0;
			this.label10.Text = "Условие:";
			this.groupBox4.Controls.Add(this.B_rule_delete);
			this.groupBox4.Controls.Add(this.B_rule_change);
			this.groupBox4.Controls.Add(this.B_rule_add);
			this.groupBox4.Location = new Point(18, 4);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(211, 116);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Действия:";
			this.B_rule_delete.Location = new Point(7, 79);
			this.B_rule_delete.Name = "B_rule_delete";
			this.B_rule_delete.Size = new Size(180, 23);
			this.B_rule_delete.TabIndex = 2;
			this.B_rule_delete.Text = "Удалить";
			this.B_rule_delete.UseVisualStyleBackColor = true;
			this.B_rule_delete.Click += new EventHandler(this.B_rule_delete_Click);
			this.B_rule_change.Location = new Point(7, 50);
			this.B_rule_change.Name = "B_rule_change";
			this.B_rule_change.Size = new Size(180, 23);
			this.B_rule_change.TabIndex = 1;
			this.B_rule_change.Text = "Изменить";
			this.B_rule_change.UseVisualStyleBackColor = true;
			this.B_rule_change.Click += new EventHandler(this.B_rule_change_Click);
			this.B_rule_add.Location = new Point(7, 20);
			this.B_rule_add.Name = "B_rule_add";
			this.B_rule_add.Size = new Size(180, 23);
			this.B_rule_add.TabIndex = 0;
			this.B_rule_add.Text = "Добавить";
			this.B_rule_add.UseVisualStyleBackColor = true;
			this.B_rule_add.Click += new EventHandler(this.B_rule_add_Click);
			this.listViewRules.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader11,
				this.columnHeader12
			});
			this.listViewRules.Dock = 5;
			this.listViewRules.FullRowSelect = true;
			this.listViewRules.GridLines = true;
			this.listViewRules.HeaderStyle = 1;
			this.listViewRules.Location = new Point(3, 3);
			this.listViewRules.MultiSelect = false;
			this.listViewRules.Name = "listViewRules";
			this.listViewRules.Size = new Size(841, 371);
			this.listViewRules.TabIndex = 6;
			this.listViewRules.UseCompatibleStateImageBehavior = false;
			this.listViewRules.View = 1;
			this.listViewRules.ItemDrag += new ItemDragEventHandler(this.listViewRules_ItemDrag);
			this.listViewRules.SelectedIndexChanged += new EventHandler(this.listViewRules_SelectedIndexChanged);
			this.listViewRules.DragDrop += new DragEventHandler(this.listViewRules_DragDrop);
			this.listViewRules.DragOver += new DragEventHandler(this.listViewRules_DragOver);
			this.columnHeader11.Text = "Имя";
			this.columnHeader11.Width = 100;
			this.columnHeader12.Text = "Содержание";
			this.columnHeader12.Width = 500;
			this.menuStrip1.Location = new Point(0, 24);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(876, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.файлToolStripMenuItem,
				this.доменыToolStripMenuItem,
				this.переменныеToolStripMenuItem,
				this.правилаToolStripMenuItem,
				this.консультацияToolStripMenuItem,
				this.объяснениеToolStripMenuItem,
				this.выходToolStripMenuItem1
			});
			this.menuStrip2.Location = new Point(0, 0);
			this.menuStrip2.Name = "menuStrip2";
			this.menuStrip2.Size = new Size(876, 24);
			this.menuStrip2.TabIndex = 2;
			this.menuStrip2.Text = "menuStrip2";
			this.файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.создатьБЗToolStripMenuItem2,
				this.открытьБЗToolStripMenuItem2,
				this.сохранитьБЗToolStripMenuItem2
			});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new Size(48, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			this.создатьБЗToolStripMenuItem2.Name = "создатьБЗToolStripMenuItem2";
			this.создатьБЗToolStripMenuItem2.Size = new Size(149, 22);
			this.создатьБЗToolStripMenuItem2.Text = "Создать БЗ";
			this.создатьБЗToolStripMenuItem2.Click += new EventHandler(this.создатьБЗToolStripMenuItem2_Click);
			this.открытьБЗToolStripMenuItem2.Name = "открытьБЗToolStripMenuItem2";
			this.открытьБЗToolStripMenuItem2.Size = new Size(149, 22);
			this.открытьБЗToolStripMenuItem2.Text = "Открыть БЗ";
			this.открытьБЗToolStripMenuItem2.Click += new EventHandler(this.открытьБЗToolStripMenuItem2_Click);
			this.сохранитьБЗToolStripMenuItem2.Name = "сохранитьБЗToolStripMenuItem2";
			this.сохранитьБЗToolStripMenuItem2.Size = new Size(149, 22);
			this.сохранитьБЗToolStripMenuItem2.Text = "Сохранить БЗ";
			this.сохранитьБЗToolStripMenuItem2.Click += new EventHandler(this.сохранитьБЗToolStripMenuItem2_Click);
			this.доменыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.создатьToolStripMenuItem3,
				this.изменитьToolStripMenuItem,
				this.удалитьToolStripMenuItem
			});
			this.доменыToolStripMenuItem.Name = "доменыToolStripMenuItem";
			this.доменыToolStripMenuItem.Size = new Size(65, 20);
			this.доменыToolStripMenuItem.Text = "Домены";
			this.доменыToolStripMenuItem.CheckStateChanged += new EventHandler(this.доменыToolStripMenuItem_CheckStateChanged);
			this.доменыToolStripMenuItem.Click += new EventHandler(this.доменыToolStripMenuItem_Click);
			this.доменыToolStripMenuItem.MouseMove += new MouseEventHandler(this.доменыToolStripMenuItem_MouseMove);
			this.создатьToolStripMenuItem3.Name = "создатьToolStripMenuItem3";
			this.создатьToolStripMenuItem3.Size = new Size(128, 22);
			this.создатьToolStripMenuItem3.Text = "Создать";
			this.создатьToolStripMenuItem3.Click += new EventHandler(this.создатьToolStripMenuItem3_Click);
			this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
			this.изменитьToolStripMenuItem.Size = new Size(128, 22);
			this.изменитьToolStripMenuItem.Text = "Изменить";
			this.изменитьToolStripMenuItem.Click += new EventHandler(this.изменитьToolStripMenuItem_Click);
			this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			this.удалитьToolStripMenuItem.Size = new Size(128, 22);
			this.удалитьToolStripMenuItem.Text = "Удалить";
			this.удалитьToolStripMenuItem.Click += new EventHandler(this.удалитьToolStripMenuItem_Click);
			this.переменныеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.создатьToolStripMenuItem4,
				this.изменитьToolStripMenuItem1,
				this.удалитьToolStripMenuItem1
			});
			this.переменныеToolStripMenuItem.Name = "переменныеToolStripMenuItem";
			this.переменныеToolStripMenuItem.Size = new Size(91, 20);
			this.переменныеToolStripMenuItem.Text = "Переменные";
			this.переменныеToolStripMenuItem.Click += new EventHandler(this.переменныеToolStripMenuItem_Click);
			this.переменныеToolStripMenuItem.MouseMove += new MouseEventHandler(this.переменныеToolStripMenuItem_MouseMove);
			this.создатьToolStripMenuItem4.Name = "создатьToolStripMenuItem4";
			this.создатьToolStripMenuItem4.Size = new Size(128, 22);
			this.создатьToolStripMenuItem4.Text = "Создать";
			this.создатьToolStripMenuItem4.Click += new EventHandler(this.создатьToolStripMenuItem4_Click);
			this.изменитьToolStripMenuItem1.Name = "изменитьToolStripMenuItem1";
			this.изменитьToolStripMenuItem1.Size = new Size(128, 22);
			this.изменитьToolStripMenuItem1.Text = "Изменить";
			this.изменитьToolStripMenuItem1.Click += new EventHandler(this.изменитьToolStripMenuItem1_Click);
			this.удалитьToolStripMenuItem1.Name = "удалитьToolStripMenuItem1";
			this.удалитьToolStripMenuItem1.Size = new Size(128, 22);
			this.удалитьToolStripMenuItem1.Text = "Удалить";
			this.удалитьToolStripMenuItem1.Click += new EventHandler(this.удалитьToolStripMenuItem1_Click);
			this.правилаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.создатьToolStripMenuItem5,
				this.изменитьToolStripMenuItem2,
				this.удалитьToolStripMenuItem5
			});
			this.правилаToolStripMenuItem.Name = "правилаToolStripMenuItem";
			this.правилаToolStripMenuItem.Size = new Size(67, 20);
			this.правилаToolStripMenuItem.Text = "Правила";
			this.правилаToolStripMenuItem.Click += new EventHandler(this.правилаToolStripMenuItem_Click);
			this.правилаToolStripMenuItem.MouseMove += new MouseEventHandler(this.правилаToolStripMenuItem_MouseMove);
			this.создатьToolStripMenuItem5.Name = "создатьToolStripMenuItem5";
			this.создатьToolStripMenuItem5.Size = new Size(128, 22);
			this.создатьToolStripMenuItem5.Text = "Создать";
			this.создатьToolStripMenuItem5.Click += new EventHandler(this.создатьToolStripMenuItem5_Click);
			this.изменитьToolStripMenuItem2.Name = "изменитьToolStripMenuItem2";
			this.изменитьToolStripMenuItem2.Size = new Size(128, 22);
			this.изменитьToolStripMenuItem2.Text = "Изменить";
			this.изменитьToolStripMenuItem2.Click += new EventHandler(this.изменитьToolStripMenuItem2_Click);
			this.удалитьToolStripMenuItem5.Name = "удалитьToolStripMenuItem5";
			this.удалитьToolStripMenuItem5.Size = new Size(128, 22);
			this.удалитьToolStripMenuItem5.Text = "Удалить";
			this.удалитьToolStripMenuItem5.Click += new EventHandler(this.удалитьToolStripMenuItem5_Click);
			this.консультацияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.выборЦелиToolStripMenuItem,
				this.начатьКонсультациюToolStripMenuItem
			});
			this.консультацияToolStripMenuItem.Name = "консультацияToolStripMenuItem";
			this.консультацияToolStripMenuItem.Size = new Size(96, 20);
			this.консультацияToolStripMenuItem.Text = "Консультация";
			this.выборЦелиToolStripMenuItem.Name = "выборЦелиToolStripMenuItem";
			this.выборЦелиToolStripMenuItem.Size = new Size(196, 22);
			this.выборЦелиToolStripMenuItem.Text = "Выбор цели";
			this.выборЦелиToolStripMenuItem.Click += new EventHandler(this.выборЦелиToolStripMenuItem_Click);
			this.начатьКонсультациюToolStripMenuItem.Name = "начатьКонсультациюToolStripMenuItem";
			this.начатьКонсультациюToolStripMenuItem.Size = new Size(196, 22);
			this.начатьКонсультациюToolStripMenuItem.Text = "Начать консультацию";
			this.начатьКонсультациюToolStripMenuItem.Click += new EventHandler(this.начатьКонсультациюToolStripMenuItem_Click);
			this.объяснениеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.показатьОбъяснениеToolStripMenuItem
			});
			this.объяснениеToolStripMenuItem.Name = "объяснениеToolStripMenuItem";
			this.объяснениеToolStripMenuItem.Size = new Size(87, 20);
			this.объяснениеToolStripMenuItem.Text = "Объяснение";
			this.показатьОбъяснениеToolStripMenuItem.Name = "показатьОбъяснениеToolStripMenuItem";
			this.показатьОбъяснениеToolStripMenuItem.Size = new Size(193, 22);
			this.показатьОбъяснениеToolStripMenuItem.Text = "Показать объяснение";
			this.показатьОбъяснениеToolStripMenuItem.Click += new EventHandler(this.показатьОбъяснениеToolStripMenuItem_Click);
			this.выходToolStripMenuItem1.Name = "выходToolStripMenuItem1";
			this.выходToolStripMenuItem1.Size = new Size(53, 20);
			this.выходToolStripMenuItem1.Text = "Выход";
			this.выходToolStripMenuItem1.Click += new EventHandler(this.выходToolStripMenuItem1_Click);
			this.создатьБЗToolStripMenuItem1.Name = "создатьБЗToolStripMenuItem1";
			this.создатьБЗToolStripMenuItem1.Size = new Size(149, 22);
			this.создатьБЗToolStripMenuItem1.Text = "Создать БЗ";
			this.создатьБЗToolStripMenuItem1.Click += new EventHandler(this.создатьБЗToolStripMenuItem1_Click);
			this.открытьБЗToolStripMenuItem1.Name = "открытьБЗToolStripMenuItem1";
			this.открытьБЗToolStripMenuItem1.Size = new Size(149, 22);
			this.открытьБЗToolStripMenuItem1.Text = "Открыть БЗ";
			this.открытьБЗToolStripMenuItem1.Click += new EventHandler(this.открытьБЗToolStripMenuItem1_Click);
			this.сохранитьБЗToolStripMenuItem1.Name = "сохранитьБЗToolStripMenuItem1";
			this.сохранитьБЗToolStripMenuItem1.Size = new Size(149, 22);
			this.сохранитьБЗToolStripMenuItem1.Text = "Сохранить БЗ";
			this.сохранитьБЗToolStripMenuItem1.Click += new EventHandler(this.сохранитьБЗToolStripMenuItem1_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(152, 22);
			this.toolStripMenuItem1.Text = "Данные";
			this.создатьБЗToolStripMenuItem.Name = "создатьБЗToolStripMenuItem";
			this.создатьБЗToolStripMenuItem.Size = new Size(152, 22);
			this.создатьБЗToolStripMenuItem.Text = "Создать БЗ";
			this.создатьБЗToolStripMenuItem.Click += new EventHandler(this.создатьБЗToolStripMenuItem_Click);
			this.открытьБЗToolStripMenuItem.Name = "открытьБЗToolStripMenuItem";
			this.открытьБЗToolStripMenuItem.Size = new Size(152, 22);
			this.открытьБЗToolStripMenuItem.Text = "Открыть БЗ";
			this.открытьБЗToolStripMenuItem.Click += new EventHandler(this.открытьБЗToolStripMenuItem_Click);
			this.сохранитьБЗToolStripMenuItem.Name = "сохранитьБЗToolStripMenuItem";
			this.сохранитьБЗToolStripMenuItem.Size = new Size(152, 22);
			this.сохранитьБЗToolStripMenuItem.Text = "Сохранить БЗ";
			this.сохранитьБЗToolStripMenuItem.Click += new EventHandler(this.сохранитьБЗToolStripMenuItem_Click);
			this.доменыToolStripMenuItem1.Name = "доменыToolStripMenuItem1";
			this.доменыToolStripMenuItem1.Size = new Size(152, 22);
			this.доменыToolStripMenuItem1.Text = "Домены";
			this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
			this.создатьToolStripMenuItem.Size = new Size(152, 22);
			this.создатьToolStripMenuItem.Text = "Создать";
			this.создатьToolStripMenuItem.Click += new EventHandler(this.создатьToolStripMenuItem_Click);
			this.удалитьToolStripMenuItem2.Name = "удалитьToolStripMenuItem2";
			this.удалитьToolStripMenuItem2.Size = new Size(152, 22);
			this.удалитьToolStripMenuItem2.Text = "Удалить";
			this.удалитьToolStripMenuItem2.Click += new EventHandler(this.удалитьToolStripMenuItem2_Click);
			this.изменитьToolStripMenuItem4.Name = "изменитьToolStripMenuItem4";
			this.изменитьToolStripMenuItem4.Size = new Size(152, 22);
			this.изменитьToolStripMenuItem4.Text = "Изменить";
			this.изменитьToolStripMenuItem4.Click += new EventHandler(this.изменитьToolStripMenuItem4_Click);
			this.переменныеToolStripMenuItem1.Name = "переменныеToolStripMenuItem1";
			this.переменныеToolStripMenuItem1.Size = new Size(152, 22);
			this.переменныеToolStripMenuItem1.Text = "Переменные";
			this.создатьToolStripMenuItem1.Name = "создатьToolStripMenuItem1";
			this.создатьToolStripMenuItem1.Size = new Size(152, 22);
			this.создатьToolStripMenuItem1.Text = "Создать";
			this.создатьToolStripMenuItem1.Click += new EventHandler(this.создатьToolStripMenuItem1_Click);
			this.удалитьToolStripMenuItem3.Name = "удалитьToolStripMenuItem3";
			this.удалитьToolStripMenuItem3.Size = new Size(152, 22);
			this.удалитьToolStripMenuItem3.Text = "Удалить";
			this.удалитьToolStripMenuItem3.Click += new EventHandler(this.удалитьToolStripMenuItem3_Click);
			this.изменитьToolStripMenuItem5.Name = "изменитьToolStripMenuItem5";
			this.изменитьToolStripMenuItem5.Size = new Size(152, 22);
			this.изменитьToolStripMenuItem5.Text = "Изменить";
			this.изменитьToolStripMenuItem5.Click += new EventHandler(this.изменитьToolStripMenuItem5_Click);
			this.правилаToolStripMenuItem1.Name = "правилаToolStripMenuItem1";
			this.правилаToolStripMenuItem1.Size = new Size(152, 22);
			this.правилаToolStripMenuItem1.Text = "Правила";
			this.создатьToolStripMenuItem2.Name = "создатьToolStripMenuItem2";
			this.создатьToolStripMenuItem2.Size = new Size(152, 22);
			this.создатьToolStripMenuItem2.Text = "Создать";
			this.создатьToolStripMenuItem2.Click += new EventHandler(this.создатьToolStripMenuItem2_Click);
			this.изменитьToolStripMenuItem6.Name = "изменитьToolStripMenuItem6";
			this.изменитьToolStripMenuItem6.Size = new Size(152, 22);
			this.изменитьToolStripMenuItem6.Text = "Изменить";
			this.изменитьToolStripMenuItem6.Click += new EventHandler(this.изменитьToolStripMenuItem6_Click);
			this.удалитьToolStripMenuItem4.Name = "удалитьToolStripMenuItem4";
			this.удалитьToolStripMenuItem4.Size = new Size(152, 22);
			this.удалитьToolStripMenuItem4.Text = "Удалить";
			this.удалитьToolStripMenuItem4.Click += new EventHandler(this.удалитьToolStripMenuItem4_Click);
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.Size = new Size(152, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.Click += new EventHandler(this.выходToolStripMenuItem_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = 1;
			base.ClientSize = new Size(876, 444);
			base.Controls.Add(this.TabControl1);
			base.Controls.Add(this.menuStrip1);
			base.Controls.Add(this.menuStrip2);
			base.Name = "Form1";
			base.StartPosition = 1;
			this.Text = "ЭС";
			base.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
			base.Load += new EventHandler(this.Form1_Load_1);
			this.TabControl1.ResumeLayout(false);
			this.tabDomains.ResumeLayout(false);
			this.tabDomains.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.tabVariables.ResumeLayout(false);
			this.tabVariables.PerformLayout();
			this.groupBox8.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabRules.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.menuStrip2.ResumeLayout(false);
			this.menuStrip2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
