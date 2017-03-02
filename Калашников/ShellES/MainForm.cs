using ShellES.Components;
using ShellES.Entities;
using ShellES.Properties;
using ShellES.VisualComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace ShellES
{
	public class MainForm : Form
	{
		private ExpertSystemShell ES;

		private bool _userMode;

		private DataGridViewRowsReorderBehavior RulesStyler;

		private IContainer components;

		private StatusStrip StatusBar;

		private BindingSource bindDomainName;

		private BindingSource bindDomainValue;

		private ImageList SysImgList;

		private BindingSource bindVars;

		private ImageList OrdImgList;

		private BindingSource bindRules;

		private BindingSource bindPrems;

		private BindingSource bindCons;

		private SplitContainer splitContainer2;

		private TabPage VarPage;

		private GroupBox groupBox2;

		private DataGridView grdVars;

		private DataGridViewTextBoxColumn colVarName;

		private DataGridViewComboBoxColumn colVarDomain;

		private DataGridViewComboBoxColumn colVarType;

		private DataGridViewTextBoxColumn colVarQuestion;

		private DataGridViewTextBoxColumn colVarDescription;

		private Button btnRemVar;

		private Button btnAddVar;

		private GroupBox groupBox1;

		private Button btnRemDomainValue;

		private Button btnAddDomainValue;

		private Button btnRemDomain;

		private Button btnAddDomain;

		private DataGridView grdDomainValues;

		private DataGridViewTextBoxColumn colDomainValues;

		private DataGridView grdDomainNames;

		private DataGridViewTextBoxColumn colDomainName;

		private TabPage RulePage;

		private SplitContainer splitContainer1;

		private GroupBox groupBox3;

		private Panel panel1;

		private Button btnRemRule;

		private Button btnAddRule;

		private DataGridView grdRules;

		private GroupBox groupBox5;

		private Button btnRemCons;

		private Button btnAddCons;

		private DataGridView grdCons;

		private DataGridViewComboBoxColumn colFactConsVar;

		private DataGridViewComboBoxColumn colFactConsValue;

		private DataGridViewTextBoxColumn colFactConsCF;

		private GroupBox groupBox4;

		private Button btnRemPrem;

		private Button btnAddPrem;

		private DataGridView grdPrems;

		private GroupBox groupBox7;

		private Label label13;

		private Label label12;

		private Label label11;

		private Label label10;

		private Label label9;

		private ComboBox cmbCFFactPrem;

		private ComboBox cmbCFVA;

		private ComboBox cmbCFCons;

		private ComboBox cmbCFFactCons;

		private ComboBox cmbCFPrem;

		private Label label8;

		private Label label7;

		private Label label6;

		private Label label5;

		private Label label4;

		private GroupBox groupBox6;

		private NumericUpDown nUpDown;

		private ComboBox cmbMLVDirectional;

		private ComboBox cmbRuleChoose;

		private Label label3;

		private Label label2;

		private Label label1;

		private ImageList TreeImgList;

		private Button btnRefreshTreeView;

		private BindingSource bindPremValues;

		private BindingSource bindConsValues;

		private DataGridViewComboBoxColumn colFactVar;

		private DataGridViewComboBoxColumn colFactValue;

		private DataGridViewTextBoxColumn colFactCF;

		private Button btnSaveLog;

		private Button btnClear;

		internal RichTextBox TxtBoxLog;

		internal Button btnStartConsult;

		internal TreeView treeElements;

		private Button btnRestoreDefaultSettings;

		private ToolStripMenuItem восстановитьУмолчанияToolStripMenuItem;

		private TabPage OutputTab;

		private SplitContainer splitContainer3;

		private GroupBox groupBox9;

		private RichTextBox LogExplain;

		internal Button btnMoveLast;

		internal Button btnMoveNext;

		internal Button btnMovePrev;

		internal Button btnMoveFirst;

		private GroupBox groupBox8;

		private ToolStripMenuItem базаЗнанийToolStripMenuItem1;

		private ToolStripMenuItem создатьToolStripMenuItem1;

		private ToolStripMenuItem открытьToolStripMenuItem1;

		private ToolStripSeparator toolStripMenuItem8;

		private ToolStripMenuItem закрытьToolStripMenuItem1;

		private ToolStripSeparator toolStripMenuItem9;

		private ToolStripMenuItem сохранитьToolStripMenuItem1;

		private ToolStripMenuItem сохранитьКакToolStripMenuItem1;

		private ToolStripSeparator toolStripMenuItem10;

		private ToolStripMenuItem выходToolStripMenuItem1;

		private ToolStripMenuItem консультацияToolStripMenuItem1;

		private ToolStripSeparator toolStripMenuItem11;

		private ToolStripMenuItem валидацияМоделиToolStripMenuItem;

		private ToolStripMenuItem настройкиToolStripMenuItem1;

		private ToolStripMenuItem каскадноеУдалениеToolStripMenuItem1;

		private ToolStripSeparator toolStripMenuItem7;

		private ToolStripMenuItem установкиПоУмолчаниюToolStripMenuItem;

		private ToolStripMenuItem справкаToolStripMenuItem;

		private ToolStripMenuItem оПрограммеToolStripMenuItem1;

		internal MenuStrip menuStrip1;

		internal ToolStripMenuItem стартToolStripMenuItem;

		private Button btnMore;

		private CheckedListBox AdditionalDomains;

		private ToolTip toolTips;

		private TextBox txtExplainRule;

		private Label label15;

		private DataGridViewTextBoxColumn colRuleName;

		private DataGridViewTextBoxColumn colRuleExplain;

		private DataGridViewTextBoxColumn colRuleCF;

		private DataGridViewTextBoxColumn colRulePriority;

		private HelpProvider HelpProvide;

		private Label label16;

		private ComboBox cmbGoal;

		private Label label14;

		private ToolStripStatusLabel LblStatusLeft;

		private ToolStripStatusLabel LblStatusRight;

		private TabControl tabTreeOut;

		private TabPage tabPageFull;

		private TreeView treeExplain;

		private TabPage tabPageRules;

		private TabPage tabPageVars;

		private ToolStripMenuItem компонентаОбъясненияToolStripMenuItem;

		private ToolStripMenuItem автоколлапсДеревьевВыводаToolStripMenuItem;

		private ToolStripMenuItem полностьюРаскрыватьПоследнююКонсультациюToolStripMenuItem;

		private TreeView treeOutVars;

		private TreeView treeOutRules;

		internal TabPage SettingPage;

		internal TabControl TabCtrl;

		private ContextMenuStrip contextDelete;

		private ToolStripMenuItem удалитьВершинуToolStripMenuItem;

		private ToolStripSeparator toolStripMenuItem1;

		private ToolStripMenuItem очиститьToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem экспортБЗToolStripMenuItem;

		private bool UseCascade
		{
			get
			{
				return this.каскадноеУдалениеToolStripMenuItem1.Checked;
			}
		}

		public bool CollapsAll
		{
			get
			{
				return this.автоколлапсДеревьевВыводаToolStripMenuItem.Checked;
			}
		}

		public bool ExpandLast
		{
			get
			{
				return this.полностьюРаскрыватьПоследнююКонсультациюToolStripMenuItem.Checked;
			}
		}

		public MainForm()
		{
			this.InitializeComponent();
			this.ES = new ExpertSystemShell(this);
			this.ES.ComExplain.BindingControls(this.treeExplain, this.treeOutVars, this.treeOutRules, this.LogExplain);
			this.RulesStyler = new DataGridViewRowsReorderBehavior(this.grdRules, this.bindRules, this, new dlgKeyUp(this.btnAddRule_Click), new dlgKeyUp(this.btnRemRule_Click));
			new DataGridViewRowsReorderBehavior(this.grdVars, this.bindVars, this, new dlgKeyUp(this.btnAddVar_Click), new dlgKeyUp(this.btnRemVar_Click));
			new DataGridViewRowsReorderBehavior(this.grdDomainNames, this.bindDomainName, this, new dlgKeyUp(this.btnAddDomain_Click), new dlgKeyUp(this.btnRemDomain_Click));
			new DataGridViewRowsReorderBehavior(this.grdDomainValues, this.bindDomainValue, this, new dlgKeyUp(this.btnAddDomainValue_Click), new dlgKeyUp(this.btnRemDomainValue_Click));
			new DataGridViewRowsReorderBehavior(this.grdPrems, this.bindPrems, this, new dlgKeyUp(this.btnAddPrem_Click), new dlgKeyUp(this.btnRemPrem_Click));
			new DataGridViewRowsReorderBehavior(this.grdCons, this.bindCons, this, new dlgKeyUp(this.btnAddCons_Click), new dlgKeyUp(this.btnRemPrem_Click));
			new DataGridViewConnectedCombos(this.grdPrems, this.bindPrems, this.bindPremValues, this);
			new DataGridViewConnectedCombos(this.grdCons, this.bindCons, this.bindConsValues, this);
			StaticHelper.SetAllTrimToGrid(this.grdDomainNames);
			StaticHelper.SetAllTrimToGrid(this.grdDomainValues);
			StaticHelper.SetAllTrimToGrid(this.grdVars);
			StaticHelper.SetAllTrimToGrid(this.grdRules);
			if (File.Exists("DefaultDomains.xml"))
			{
				XmlReader xmlReader = XmlReader.Create("DefaultDomains.xml");
				ESDomains eSDomains = null;
				while (xmlReader.Read())
				{
					XmlNodeType nodeType = xmlReader.NodeType;
					string name;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if (xmlReader.Name == "domain" && eSDomains != null)
							{
								this.AdditionalDomains.Items.Add(eSDomains);
							}
						}
					}
					else if ((name = xmlReader.Name) != null)
					{
						if (!(name == "domain"))
						{
							if (name == "value")
							{
								if (eSDomains != null)
								{
									eSDomains.AddValue(xmlReader.ReadElementString());
								}
							}
						}
						else
						{
							eSDomains = new ESDomains(xmlReader.GetAttribute("name"));
						}
					}
				}
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.ShowOrHideES(false);
			this.RefreshEveryThing(true);
			this.SettingGridsWithBindings();
			this._userMode = true;
			this.RefreshAddDeleteBtns();
			this.ES.IsChanged = false;
		}

		private void RefreshEveryThing(bool FirstTime)
		{
			this._userMode = false;
			this.ES.ESDB.ConnectBindings(this.bindDomainName, this.bindDomainValue, this.bindVars, this.bindRules, this.bindPrems, this.bindCons, this.bindPremValues, this.bindConsValues);
			this.ES.ESDB.SetMainBindings();
			this.RefreshBindings();
			this.SettingSettings(!FirstTime);
			this.SetUpFormSettings(this.ES);
			this.SettingTree(this.ES);
			this._userMode = true;
		}

		public void SetStatusBar(string Msg, bool RightToLeft, Color c)
		{
			if (RightToLeft)
			{
				this.LblStatusRight.ForeColor = c;
				this.LblStatusRight.Text = Msg;
				return;
			}
			this.LblStatusLeft.ForeColor = c;
			this.LblStatusLeft.Text = Msg;
		}

		public void FocusOutputTree()
		{
			this.TabCtrl.SelectedTab = this.OutputTab;
			this.treeExplain.Focus();
		}

		private void splitContainer2_DoubleClick(object sender, EventArgs e)
		{
			this.splitContainer2.SplitterDistance = 750;
		}

		private void txtExplainRule_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.txtExplainRule_Validated(sender, e);
			}
		}

		private void txtExplainRule_Validated(object sender, EventArgs e)
		{
			ESRules eSRules = this.bindRules.Current as ESRules;
			if (eSRules != null)
			{
				eSRules.Reason = this.txtExplainRule.Text.Trim();
				this.txtExplainRule.SelectAll();
			}
		}

		private void SettingGridsWithBindings()
		{
			this.SettingDomainGrids();
			this.SettingVarGrid();
			this.SettingRuleGrids();
		}

		private void SettingDomainGrids()
		{
			this.grdDomainNames.AutoGenerateColumns = false;
			this.grdDomainValues.AutoGenerateColumns = false;
			this.grdDomainNames.DataSource = this.bindDomainName;
			this.grdDomainValues.DataSource = this.bindDomainValue;
			this.grdDomainNames.Columns[0].DataPropertyName = "Name";
			this.grdDomainValues.Columns[0].DataPropertyName = "Value";
		}

		private void SettingVarGrid()
		{
			this.grdVars.AutoGenerateColumns = false;
			this.grdVars.DataSource = this.bindVars;
			this.grdVars.Columns["colVarName"].DataPropertyName = "Name";
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn = this.grdVars.Columns["colVarDomain"] as DataGridViewComboBoxColumn;
			dataGridViewComboBoxColumn.DataSource = this.bindDomainName;
			dataGridViewComboBoxColumn.DataPropertyName = "Domain";
			dataGridViewComboBoxColumn.ValueMember = "Self";
			dataGridViewComboBoxColumn.DisplayMember = "Name";
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn2 = this.grdVars.Columns["colVarType"] as DataGridViewComboBoxColumn;
			dataGridViewComboBoxColumn2.DataSource = Enum.GetValues(typeof(ESVars.VAR_TYPE));
			dataGridViewComboBoxColumn2.DataPropertyName = "VarType";
			this.grdVars.Columns["colVarQuestion"].DataPropertyName = "Question";
			this.grdVars.Columns["colVarDescription"].DataPropertyName = "Description";
		}

		private void SettingRuleGrids()
		{
			this.grdRules.AutoGenerateColumns = false;
			this.grdRules.DataSource = this.bindRules;
			this.grdRules.Columns["colRuleName"].DataPropertyName = "Name";
			this.grdRules.Columns["colRuleExplain"].DataPropertyName = "Presentation";
			this.grdRules.Columns["colRuleCF"].DataPropertyName = "RuleCF";
			this.grdRules.Columns["colRulePriority"].DataPropertyName = "Priority";
			this.grdPrems.AutoGenerateColumns = false;
			this.grdPrems.DataSource = this.bindPrems;
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn = this.grdPrems.Columns["colFactVar"] as DataGridViewComboBoxColumn;
			dataGridViewComboBoxColumn.DataSource = this.bindVars;
			dataGridViewComboBoxColumn.DataPropertyName = "Variable";
			dataGridViewComboBoxColumn.ValueMember = "Self";
			dataGridViewComboBoxColumn.DisplayMember = "Name";
			dataGridViewComboBoxColumn = (this.grdPrems.Columns["colFactValue"] as DataGridViewComboBoxColumn);
			dataGridViewComboBoxColumn.DataSource = this.bindPremValues;
			dataGridViewComboBoxColumn.DisplayMember = "Value";
			dataGridViewComboBoxColumn.DataPropertyName = "Value";
			dataGridViewComboBoxColumn.ValueMember = "Self";
			this.grdPrems.Columns["colFactCF"].DataPropertyName = "FactCF";
			this.grdCons.AutoGenerateColumns = false;
			this.grdCons.DataSource = this.bindCons;
			dataGridViewComboBoxColumn = (this.grdCons.Columns["colFactConsVar"] as DataGridViewComboBoxColumn);
			dataGridViewComboBoxColumn.DataSource = this.bindVars;
			dataGridViewComboBoxColumn.DataPropertyName = "Variable";
			dataGridViewComboBoxColumn.ValueMember = "Self";
			dataGridViewComboBoxColumn.DisplayMember = "Name";
			dataGridViewComboBoxColumn = (this.grdCons.Columns["colFactConsValue"] as DataGridViewComboBoxColumn);
			dataGridViewComboBoxColumn.DataSource = this.bindConsValues;
			dataGridViewComboBoxColumn.DisplayMember = "Value";
			dataGridViewComboBoxColumn.DataPropertyName = "Value";
			dataGridViewComboBoxColumn.ValueMember = "Self";
			this.grdCons.Columns["colFactConsCF"].DataPropertyName = "FactCF";
		}

		private void SettingSettings(bool goalOnly)
		{
			if (!goalOnly)
			{
				this.cmbRuleChoose.Items.AddRange(ShellES.Components.Settings.Str_SELECT);
				this.cmbMLVDirectional.Items.AddRange(ShellES.Components.Settings.Str_MLV);
				this.cmbCFFactPrem.Items.AddRange(ShellES.Components.Settings.Str_CFСO);
				this.cmbCFPrem.Items.AddRange(ShellES.Components.Settings.Str_CFJO);
				this.cmbCFFactCons.Items.AddRange(ShellES.Components.Settings.Str_CFJO);
				this.cmbCFCons.Items.AddRange(ShellES.Components.Settings.Str_CFСO);
				this.cmbCFVA.Items.AddRange(ShellES.Components.Settings.Str_CFСO);
			}
			this.cmbGoal.DataSource = this.bindVars;
			this.cmbGoal.DisplayMember = "Name";
			this.cmbGoal.ValueMember = "Self";
		}

		private void SettingImageIndexes(int ImgIndex, params TreeNode[] nodes)
		{
			for (int i = 0; i < nodes.Length; i++)
			{
				TreeNode treeNode = nodes[i];
				treeNode.ImageIndex = ImgIndex;
				treeNode.SelectedImageIndex = ImgIndex;
			}
		}

		private void SettingSubImageIndexes(int ImgIndex, params TreeNode[] nodes)
		{
			for (int i = 0; i < nodes.Length; i++)
			{
				TreeNode treeNode = nodes[i];
				foreach (TreeNode treeNode2 in treeNode.Nodes)
				{
					treeNode2.ImageIndex = ImgIndex;
					treeNode2.SelectedImageIndex = ImgIndex;
				}
			}
		}

		private void btnRefreshTreeView_Click(object sender, EventArgs e)
		{
			this.SettingTree(this.ES);
		}

		private void SettingTree(ExpertSystemShell ESS)
		{
			this.treeElements.BeginUpdate();
			this.treeElements.Nodes.Clear();
			TreeNode treeNode = this.treeElements.Nodes.Add("Экспертная система");
			this.SettingImageIndexes(1, new TreeNode[]
			{
				treeNode
			});
			TreeNode treeNode2 = treeNode.Nodes.Add("Домены (" + ESS.Domains.Count.ToString() + ")");
			TreeNode treeNode3 = treeNode.Nodes.Add("Переменные (" + ESS.Vars.Count.ToString() + ")");
			TreeNode treeNode4 = treeNode.Nodes.Add("Правила (" + ESS.Rules.Count.ToString() + ")");
			TreeNode treeNode5 = treeNode.Nodes.Add("Настройки");
			this.SettingSubImageIndexes(2, new TreeNode[]
			{
				treeNode
			});
			TreeNode treeNode6;
			foreach (ESDomains current in ESS.Domains)
			{
				treeNode6 = treeNode2.Nodes.Add(current.Name + " (" + current.Elements.Count.ToString() + ")");
				foreach (ESDomainValue current2 in current.Elements)
				{
					treeNode6.Nodes.Add(current2.Value);
				}
				this.SettingSubImageIndexes(4, new TreeNode[]
				{
					treeNode6
				});
			}
			foreach (ESVars current3 in ESS.Vars)
			{
				treeNode6 = treeNode3.Nodes.Add(current3.Name);
				treeNode6.Nodes.Add("Тип: " + current3.VarType.ToString());
				if (current3.Domain != null)
				{
					treeNode6.Nodes.Add("Домен: " + current3.Domain.Name);
				}
				else
				{
					treeNode6.Nodes.Add("Домен: нет домена!");
				}
				this.SettingSubImageIndexes(3, new TreeNode[]
				{
					treeNode6
				});
				TreeNode treeNode7 = treeNode6.Nodes.Add("Значения (" + current3.Value.Count.ToString() + ")");
				foreach (KeyValuePair<string, int> current4 in current3.Value)
				{
					treeNode7.Nodes.Add(current4.Key + " [" + current4.Value.ToString() + "]");
				}
				this.SettingImageIndexes(2, new TreeNode[]
				{
					treeNode7
				});
				this.SettingSubImageIndexes(4, new TreeNode[]
				{
					treeNode7
				});
			}
			foreach (ESRules current5 in ESS.Rules)
			{
				treeNode6 = treeNode4.Nodes.Add(current5.Name);
				treeNode6.Nodes.Add("?: " + current5.Reason);
				treeNode6.Nodes.Add("CF: " + current5.RuleCF.ToString());
				this.SettingSubImageIndexes(3, new TreeNode[]
				{
					treeNode6
				});
				TreeNode treeNode7 = treeNode6.Nodes.Add("Посылки (" + current5.Premises.Count.ToString() + ")");
				foreach (ESFact current6 in current5.Premises)
				{
					treeNode7.Nodes.Add(current6.GetPresentation(true));
				}
				this.SettingImageIndexes(2, new TreeNode[]
				{
					treeNode7
				});
				this.SettingSubImageIndexes(4, new TreeNode[]
				{
					treeNode7
				});
				treeNode7 = treeNode6.Nodes.Add("Заключения (" + current5.Consequences.Count.ToString() + ")");
				foreach (ESFact current7 in current5.Consequences)
				{
					treeNode7.Nodes.Add(current7.GetPresentation(true));
				}
				this.SettingImageIndexes(2, new TreeNode[]
				{
					treeNode7
				});
				this.SettingSubImageIndexes(4, new TreeNode[]
				{
					treeNode7
				});
			}
			this.SettingImageIndexes(5, new TreeNode[]
			{
				treeNode5
			});
			if (ESS.Settings.Goal != null)
			{
				treeNode5.Nodes.Add("Цель: <" + ESS.Settings.Goal.Name + ">");
			}
			else
			{
				treeNode5.Nodes.Add("Цель: <не определена>");
			}
			treeNode5.Nodes.Add("Направл. МЛВ: " + ESS.Settings.MLV_Direction.ToString());
			treeNode5.Nodes.Add("Выборка правила: " + ESS.Settings.SelStrategy.ToString());
			treeNode5.Nodes.Add("CF.Unknkown = " + ESS.Settings.EDGE_UNKNOWN.ToString());
			this.SettingSubImageIndexes(3, new TreeNode[]
			{
				treeNode5
			});
			treeNode6 = treeNode5.Nodes.Add("Стратегии пересчета");
			treeNode6.Nodes.Add("CF ф. в посылке: " + ESS.Settings.CFFact.ToString());
			treeNode6.Nodes.Add("CF посылки: " + ESS.Settings.CFPrem.ToString());
			treeNode6.Nodes.Add("CF заключения: " + ESS.Settings.CFConcl.ToString());
			treeNode6.Nodes.Add("CF ф. в закл.: " + ESS.Settings.CFFactConcl.ToString());
			treeNode6.Nodes.Add("CFVA: " + ESS.Settings.CFVA.ToString());
			this.SettingSubImageIndexes(3, new TreeNode[]
			{
				treeNode6
			});
			this.SettingImageIndexes(2, new TreeNode[]
			{
				treeNode6
			});
			treeNode.Expand();
			this.treeElements.EndUpdate();
		}

		private void SetUpFormSettings(ExpertSystemShell ESS)
		{
			if (ESS.Settings.Goal != null)
			{
				this.cmbGoal.SelectedIndex = this.cmbGoal.Items.IndexOf(ESS.Settings.Goal);
			}
			this.cmbMLVDirectional.SelectedIndex = (int)ESS.Settings.MLV_Direction;
			this.cmbRuleChoose.SelectedIndex = (int)ESS.Settings.SelStrategy;
			this.nUpDown.Minimum = 0m;
			this.nUpDown.Maximum = 100m;
			this.nUpDown.Value = ESS.Settings.EDGE_UNKNOWN;
			this.cmbCFFactPrem.SelectedIndex = (int)ESS.Settings.CFFact;
			this.cmbCFPrem.SelectedIndex = (int)ESS.Settings.CFPrem;
			this.cmbCFCons.SelectedIndex = (int)ESS.Settings.CFConcl;
			this.cmbCFFactCons.SelectedIndex = (int)ESS.Settings.CFFactConcl;
			this.cmbCFVA.SelectedIndex = (int)ESS.Settings.CFVA;
		}

		private void SaveToES(ExpertSystemShell ESS)
		{
			ESS.Settings.MLV_Direction = (ShellES.Components.Settings.eSTRATEGY_MLV_DIRECTION)ShellES.Components.Settings.SetEnumElement(typeof(ShellES.Components.Settings.eSTRATEGY_MLV_DIRECTION), this.cmbMLVDirectional.SelectedIndex.ToString());
			ESS.Settings.CFFact = (ShellES.Components.Settings.eSTRATEGY_CFCO)ShellES.Components.Settings.SetEnumElement(typeof(ShellES.Components.Settings.eSTRATEGY_CFCO), this.cmbCFFactPrem.SelectedIndex.ToString());
			ESS.Settings.CFPrem = (ShellES.Components.Settings.eSTRATEGY_CFJO)ShellES.Components.Settings.SetEnumElement(typeof(ShellES.Components.Settings.eSTRATEGY_CFJO), this.cmbCFPrem.SelectedIndex.ToString());
			ESS.Settings.CFFactConcl = (ShellES.Components.Settings.eSTRATEGY_CFJO)ShellES.Components.Settings.SetEnumElement(typeof(ShellES.Components.Settings.eSTRATEGY_CFJO), this.cmbCFFactCons.SelectedIndex.ToString());
			ESS.Settings.CFConcl = (ShellES.Components.Settings.eSTRATEGY_CFCO)ShellES.Components.Settings.SetEnumElement(typeof(ShellES.Components.Settings.eSTRATEGY_CFCO), this.cmbCFCons.SelectedIndex.ToString());
			ESS.Settings.CFVA = (ShellES.Components.Settings.eSTRATEGY_CFCO)ShellES.Components.Settings.SetEnumElement(typeof(ShellES.Components.Settings.eSTRATEGY_CFCO), this.cmbCFVA.SelectedIndex.ToString());
			ESS.Settings.EDGE_UNKNOWN = (int)this.nUpDown.Value;
			ESS.Settings.Goal = (this.cmbGoal.SelectedItem as ESVars);
		}

		private void bindPremConsValues_CurrentChanged(object sender, EventArgs e)
		{
			this.bindRules.ResetBindings(false);
		}

		private void bindDomainName_PositionChanged(object sender, EventArgs e)
		{
			if (this.ES != null)
				this.ES.ESDB.BindingsRefresh_DomainValue();
			this.RefreshAddDeleteBtns();
		}

		private void bindVars_PositionChanged(object sender, EventArgs e)
		{
			if (this.ES != null)
				this.ES.ESDB.BindingsRefresh_DomainName();
		}

		private void bindRules_PositionChanged(object sender, EventArgs e)
		{
			if (this.ES != null)
				this.ES.ESDB.BindingRefresh_RulePremCons();
			if (this.RulesStyler != null)
				this.RulesStyler.CheckAvailableNavigation(this.bindRules.Position, false);
			this.RefreshAddDeleteBtns();

			if (this.bindRules != null && this.bindRules.Current is ESRules)
			{
				this.txtExplainRule.Enabled = true;
				this.txtExplainRule.Text = (this.bindRules.Current as ESRules).Reason;
				return;
			}
			if (this.txtExplainRule != null)
			{
				this.txtExplainRule.Enabled = false;
				this.txtExplainRule.Text = "";
			}
		}

		private void Binding_ItemChanged(object sender, EventArgs e)
		{
			if (this._userMode && this.ES != null)
			{
				this.ES.ESDB.Changed = true;
			}
		}

		private void btnMoveFirst_Click(object sender, EventArgs e)
		{
			this.RulesStyler.MoveCurrentObjectInBinding(this.bindRules, 0);
		}

		private void btnMovePrev_Click(object sender, EventArgs e)
		{
			this.RulesStyler.MoveCurrentObjectInBinding(this.bindRules, this.bindRules.Position - 1);
		}

		private void btnMoveNext_Click(object sender, EventArgs e)
		{
			this.RulesStyler.MoveCurrentObjectInBinding(this.bindRules, this.bindRules.Position + 1);
		}

		private void btnMoveLast_Click(object sender, EventArgs e)
		{
			this.RulesStyler.MoveCurrentObjectInBinding(this.bindRules, this.bindRules.Count - 1);
		}

		private void RefreshBindings()
		{
			this.bindDomainName.ResetBindings(false);
			this.bindDomainValue.ResetBindings(false);
			this.bindVars.ResetBindings(false);
			this.bindRules.ResetBindings(false);
			this.bindPrems.ResetBindings(false);
			this.bindCons.ResetBindings(false);
		}

		private void RefreshAddDeleteBtns()
		{
			this.btnRemDomain.Enabled = (this.bindDomainName.Count > 0);
			this.btnAddDomainValue.Enabled = (this.bindDomainName.Count > 0);
			this.btnRemDomainValue.Enabled = (this.bindDomainValue.Count > 0);
			this.btnRemVar.Enabled = (this.bindVars.Count > 0);
			this.btnRemRule.Enabled = (this.bindRules.Count > 0);
			this.btnAddPrem.Enabled = this.btnRemRule.Enabled;
			this.btnRemPrem.Enabled = (this.bindPrems.Count > 0);
			this.btnAddCons.Enabled = this.btnRemRule.Enabled;
			this.btnRemCons.Enabled = (this.bindCons.Count > 0);
		}

		private void btnAddDomain_Click(object sender, EventArgs e)
		{
			this.ES.ESDB.AddDomain("");
			this.bindDomainName.ResetBindings(false);
			this.bindDomainName.Position = this.bindDomainName.Count - 1;
			this.grdDomainNames.BeginEdit(true);
			this.RefreshAddDeleteBtns();
		}

		private void btnRemDomain_Click(object sender, EventArgs e)
		{
			if (this.bindDomainName.Current == null)
			{
				return;
			}
			this.ES.ESDB.DeleteDomain(this.bindDomainName.Current as ESDomains, this.UseCascade, true);
			this.RefreshBindings();
			this.ES.ESDB.BindingsRefresh_DomainValue();
			this.RefreshAddDeleteBtns();
		}

		private void btnAddDomainValue_Click(object sender, EventArgs e)
		{
			if (this.bindDomainName.Current == null)
			{
				return;
			}
			this.ES.ESDB.AddDomainValue(this.bindDomainName.Current as ESDomains, "");
			this.bindDomainValue.ResetBindings(false);
			this.bindDomainValue.Position = this.bindDomainValue.Count - 1;
			this.grdDomainValues.BeginEdit(true);
			this.RefreshAddDeleteBtns();
		}

		private void btnRemDomainValue_Click(object sender, EventArgs e)
		{
			if (this.bindDomainName.Current == null || this.bindDomainValue.Current == null)
			{
				return;
			}
			this.ES.ESDB.DeleteDomainValue(this.bindDomainName.Current as ESDomains, this.bindDomainValue.Current as ESDomainValue, this.UseCascade, true);
			this.RefreshBindings();
			this.RefreshAddDeleteBtns();
		}

		private void btnAddVar_Click(object sender, EventArgs e)
		{
			this.ES.ESDB.AddVar();
			this.bindVars.ResetBindings(false);
			this.bindVars.Position = this.bindVars.Count - 1;
			this.grdVars.BeginEdit(true);
			this.RefreshAddDeleteBtns();
		}

		private void btnRemVar_Click(object sender, EventArgs e)
		{
			if (this.bindVars.Current == null)
			{
				return;
			}
			this.ES.ESDB.DeleteVar(this.bindVars.Current as ESVars, this.UseCascade, true);
			this.RefreshBindings();
			this.ES.ESDB.BindingsRefresh_DomainName();
			this.RefreshAddDeleteBtns();
		}

		private void btnAddRule_Click(object sender, EventArgs e)
		{
			this.ES.ESDB.AddRule("R" + (this.bindRules.Count + 1).ToString());
			this.bindRules.ResetBindings(false);
			this.bindRules.Position = this.bindRules.Count - 1;
			this.grdRules.BeginEdit(true);
			this.RefreshAddDeleteBtns();
		}

		private void btnRemRule_Click(object sender, EventArgs e)
		{
			if (this.bindRules.Current == null)
			{
				return;
			}
			this.ES.ESDB.DeleteRule(this.bindRules.Current as ESRules, true);
			this.bindRules.ResetBindings(false);
			this.ES.ESDB.BindingRefresh_RulePremCons();
			this.RefreshAddDeleteBtns();
		}

		private void btnAddPrem_Click(object sender, EventArgs e)
		{
			if (this.bindRules.Current == null)
			{
				return;
			}
			this.ES.ESDB.AddFactInPrem(this.bindRules.Current as ESRules);
			this.bindPrems.ResetBindings(false);
			this.bindPrems.Position = this.bindPrems.Count - 1;
			this.grdPrems.BeginEdit(true);
			this.RefreshAddDeleteBtns();
		}

		private void btnRemPrem_Click(object sender, EventArgs e)
		{
			if (this.bindRules.Current == null || this.bindPrems.Current == null)
			{
				return;
			}
			this.ES.ESDB.DeleteFactInPrem(this.bindRules.Current as ESRules, (ESFact)this.bindPrems.Current, true);
			this.bindPrems.ResetBindings(false);
			this.RefreshAddDeleteBtns();
		}

		private void btnAddCons_Click(object sender, EventArgs e)
		{
			if (this.bindRules.Current == null)
			{
				return;
			}
			this.ES.ESDB.AddFactInCons(this.bindRules.Current as ESRules);
			this.bindCons.ResetBindings(false);
			this.bindCons.Position = this.bindCons.Count - 1;
			this.grdCons.BeginEdit(true);
			this.RefreshAddDeleteBtns();
		}

		private void btnRemCons_Click(object sender, EventArgs e)
		{
			if (this.bindRules.Current == null || this.bindCons.Current == null)
			{
				return;
			}
			this.ES.ESDB.DeleteFactInCons(this.bindRules.Current as ESRules, (ESFact)this.bindCons.Current, true);
			this.bindCons.ResetBindings(false);
			this.RefreshAddDeleteBtns();
		}

		private void grdDomainNames_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (!this._userMode)
			{
				return;
			}
			if (this.grdDomainNames.Rows[e.RowIndex].IsNewRow)
			{
				return;
			}
			if (e.FormattedValue.Equals(string.Empty) || !this.ES.ESDB.IsUniqueDomainName(e.FormattedValue as string, this.bindDomainName.Current as ESDomains))
			{
				MessageBox.Show("Название домена должно быть не пустым и уникальным!", "Проверка ввода");
				e.Cancel = true;
			}
		}

		private void grdDomainValues_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (!this._userMode)
			{
				return;
			}
			if (this.grdDomainValues.Rows[e.RowIndex].IsNewRow)
			{
				return;
			}
			if (e.FormattedValue.Equals(string.Empty) || !this.ES.ESDB.IsUniqueDomainValue(this.bindDomainName.Current as ESDomains, e.FormattedValue as string, this.bindDomainValue.Position))
			{
				MessageBox.Show("Название домена должно быть не пустым и уникальным!", "Проверка ввода");
				e.Cancel = true;
			}
		}

		private void grdVars_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (!this._userMode)
			{
				return;
			}
			if (this.grdVars.Rows[e.RowIndex].IsNewRow)
			{
				return;
			}
			switch (e.ColumnIndex)
			{
				case 0:
					if (e.FormattedValue.Equals(string.Empty) || !this.ES.ESDB.IsUniqueVarName(e.FormattedValue as string, this.bindVars.Current as ESVars))
					{
						MessageBox.Show("Название домена должно быть не пустым и уникальным!", "Проверка ввода");
						e.Cancel = true;
						return;
					}
					break;
				case 1:
					{
						ESDomains domainByName = this.ES.ESDB.GetDomainByName(e.FormattedValue);
						if (domainByName == this.grdVars.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
						{
							return;
						}
						int num = this.ES.ESDB.CheckWhileChangingDomain(this.bindVars.Current as ESVars, domainByName);
						if (num > 0)
						{
							MessageBox.Show("В посылках " + num.ToString() + " правил значения текущей переменной были сброшены.\nПожалуйста, проверьте эти правила!", "Изменение домена используемой переменной", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							this.bindPrems.ResetBindings(false);
							this.bindCons.ResetBindings(false);
						}
						break;
					}
				default:
					return;
			}
		}

		private void grdRules_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (!this._userMode)
			{
				return;
			}
			if (this.grdRules.Rows[e.RowIndex].IsNewRow)
			{
				return;
			}
			int num = 0;
			switch (e.ColumnIndex)
			{
				case 0:
					if (e.FormattedValue.Equals(string.Empty) || !this.ES.ESDB.IsUniqueRuleName(e.FormattedValue as string, this.bindRules.Current as ESRules))
					{
						MessageBox.Show("Название правила должно быть не пустым и уникальным!", "Проверка ввода");
						e.Cancel = true;
						return;
					}
					break;
				case 1:
					break;
				case 2:
					if (!int.TryParse(e.FormattedValue as string, out num) || num > 100 || num < 0)
					{
						MessageBox.Show("Коэффициент доверия должен быть целым числом в границах [0; " + 100.ToString() + "]", "Проверка ввода");
						e.Cancel = true;
						return;
					}
					break;
				case 3:
					if (!int.TryParse(e.FormattedValue as string, out num) || num < 0)
					{
						MessageBox.Show("Приоритет должен быть неотрицательным числом", "Проверка ввода");
						e.Cancel = true;
					}
					break;
				default:
					return;
			}
		}

		private void grdPrems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (!this._userMode)
			{
				return;
			}
			if (this.grdPrems.Rows[e.RowIndex].IsNewRow)
			{
				return;
			}
			switch (e.ColumnIndex)
			{
				case 0:
					if (e.FormattedValue.Equals(null))
					{
						MessageBox.Show("Выберите переменную!", "Проверка ввода");
						e.Cancel = true;
						return;
					}
					return;
				case 1:
					try
					{
						if (e.FormattedValue == null && (this.bindPrems.Current as ESFact).Variable.Domain.Elements.Count > 0)
						{
							MessageBox.Show("Выберите значение домена!", "Проверка ввода");
							e.Cancel = true;
						}
						return;
					}
					catch
					{
						return;
					}
					break;
				case 2:
					break;
				default:
					return;
			}
			int num = 0;
			if (!int.TryParse(e.FormattedValue as string, out num) || num > 100 || num < 0)
			{
				MessageBox.Show("Коэффициент доверия должен быть целым числом в границах [0; " + 100.ToString() + "]", "Проверка ввода");
				e.Cancel = true;
			}
		}

		private void grdCons_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (!this._userMode)
			{
				return;
			}
			if (this.grdCons.Rows[e.RowIndex].IsNewRow)
			{
				return;
			}
			switch (e.ColumnIndex)
			{
				case 0:
					if (e.FormattedValue.Equals(null))
					{
						MessageBox.Show("Выберите переменную!", "Проверка ввода");
						e.Cancel = true;
						return;
					}
					return;
				case 1:
					try
					{
						if (e.FormattedValue == null && (this.bindCons.Current as ESFact).Variable.Domain.Elements.Count > 0)
						{
							MessageBox.Show("Выберите значение домена!", "Проверка ввода");
							e.Cancel = true;
						}
						return;
					}
					catch
					{
						return;
					}
					break;
				case 2:
					break;
				default:
					return;
			}
			int num = 0;
			if (!int.TryParse(e.FormattedValue as string, out num) || num > 100 || num < 0)
			{
				MessageBox.Show("Коэффициент доверия должен быть целым числом в границах [0; " + 100.ToString() + "]", "Проверка ввода");
				e.Cancel = true;
			}
		}

		public List<ESFact> GetCurrentList(DataGridView dgv)
		{
			if (dgv == this.grdPrems)
			{
				if (this.bindRules.Current is ESRules)
				{
					return (this.bindRules.Current as ESRules).Premises;
				}
				return null;
			}
			else
			{
				if (dgv != this.grdCons)
				{
					return null;
				}
				if (this.bindRules.Current is ESRules)
				{
					return (this.bindRules.Current as ESRules).Consequences;
				}
				return null;
			}
		}

		private void btnStartConsult_Click(object sender, EventArgs e)
		{
			if (!this.ES.IsConsult)
			{
				this.SaveToES(this.ES);
				this.btnStartConsult.Text = "Остановить консультацию";
				this.стартToolStripMenuItem.Text = "Остановить";
				this.ES.StartConsult();
				return;
			}
			if (MessageBox.Show("Вы действительно хотите прервать консультацию ?", "Отмена консультации", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				this.ES.InterruptConsult();
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			if (this.TxtBoxLog.Lines.Length > 0 && MessageBox.Show("Вы действительно хотите очистить Log?", "Очистка Log'а", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				this.TxtBoxLog.Clear();
			}
		}

		private void btnSaveLog_Click(object sender, EventArgs e)
		{
			if (this.TxtBoxLog.Text.Length == 0)
			{
				return;
			}
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.DefaultExt = "log";
			try
			{
				saveFileDialog.InitialDirectory = this.ES.ESDB.BasePath;
				saveFileDialog.Filter = ShellES.Properties.Settings.Default.FilePathFilter;
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					this.TxtBoxLog.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
				}
			}
			catch
			{
				MessageBox.Show("Ошибка при записи log-файла", "Сохранение");
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.ES.IsConsult)
			{
				if (MessageBox.Show("Консультация в самом разгаре! Вы хотите её остановить?", "Закрытие оболочки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes)
				{
					e.Cancel = true;
					return;
				}
				this.ES.StopConsult();
			}
			if (this.ES.IsChanged)
			{
				e.Cancel = (this.ES.CheckForSave() == DialogResult.Cancel);
				return;
			}
			e.Cancel = (MessageBox.Show("Вы действительно хотите завершить работу с приложением?", "Закрытие оболочки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes);
		}

		private void btnRestoreDefaultSettings_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вы хотите воостановить установки по умолчанию?", "Подтверждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				this.ES.ESDB.ResetDefaults();
				this.SetUpFormSettings(this.ES);
				this.SettingTree(this.ES);
				MessageBox.Show("Установки восстановлены");
			}
		}

		private void ShowOrHideES(bool toHide)
		{
			this.splitContainer2.Visible = !toHide;
			this.закрытьToolStripMenuItem1.Enabled = !toHide;
			this.сохранитьToolStripMenuItem1.Enabled = !toHide;
			this.сохранитьКакToolStripMenuItem1.Enabled = !toHide;
			this.стартToolStripMenuItem.Enabled = !toHide;
			this.валидацияМоделиToolStripMenuItem.Enabled = !toHide;
			this.каскадноеУдалениеToolStripMenuItem1.Enabled = !toHide;
			this.установкиПоУмолчаниюToolStripMenuItem.Enabled = !toHide;
		}

		private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveToES(this.ES);
			if (this.ES.CheckForSave() == DialogResult.Cancel)
			{
				return;
			}
			this.ES.ESDB.ClearAll(true, true);
			this.ShowOrHideES(false);
			this.RefreshEveryThing(false);
		}

		private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveToES(this.ES);
			if (this.ES.CheckForSave() == DialogResult.Cancel)
			{
				return;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (this.ES.ESDB.BasePath != "")
			{
				openFileDialog.InitialDirectory = this.ES.ESDB.BasePath;
			}
			else if (Directory.Exists(Application.StartupPath + "\\ESBases"))
			{
				openFileDialog.InitialDirectory = Application.StartupPath + "\\ESBases";
			}
			else
			{
				openFileDialog.InitialDirectory = Application.StartupPath;
			}
			openFileDialog.Filter = ShellES.Properties.Settings.Default.FilePathFilter;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (this.ES.LoadESDBFromFile(openFileDialog.FileName))
				{
					this.ES.ESDB.BasePath = openFileDialog.FileName;
					this.ShowOrHideES(false);
					this.ES.IsChanged = false;
				}
				this.RefreshEveryThing(false);
			}
		}

		private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.ES.IsChanged)
			{
				this.SaveToES(this.ES);
				this.ES.SaveDataBase(false);
			}
		}

		private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveToES(this.ES);
			this.ES.SaveDataBase(true);
		}

		private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.SaveToES(this.ES);
			if (this.ES.CheckForSave() == DialogResult.Cancel)
			{
				return;
			}
			this.ES.ESDB.ClearAll(true, false);
			this.ShowOrHideES(true);
			this.RefreshEveryThing(false);
		}

		private void выходToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void начатьКонсультациюToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.btnStartConsult_Click(sender, e);
		}

		private void проверитьНаОшибкиToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ES.ESDB.ValidateESDB(this.TxtBoxLog, false);
		}

		private void восстановитьУмолчанияToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.btnRestoreDefaultSettings_Click(sender, e);
		}

		private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			string text = "Программа ShellES, ver. " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\n";
			text += "Организация: МОВС, ПГУ\n";
			text += "Автор: Калашников Евгений]\n";
			text += "[keatrance@gmail.com]";
			MessageBox.Show(text, "О программе ShellES", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue == CheckState.Checked)
			{
				ESDomains eSDomains = this.AdditionalDomains.Items[e.Index] as ESDomains;
				ESDomains item = new ESDomains(eSDomains.Name, eSDomains.StringElements().ToArray());
				if (!this.ES.ESDB.Domains.Contains(eSDomains))
				{
					this.ES.ESDB.Domains.Add(item);
					this.bindDomainName.ResetBindings(false);
					this.bindDomainName.Position = this.bindDomainName.Count - 1;
					this.grdDomainNames.BeginEdit(true);
					this.RefreshAddDeleteBtns();
					e.NewValue = CheckState.Unchecked;
				}
			}
			this.AdditionalDomains.Hide();
		}

		private void btnMore_Click(object sender, EventArgs e)
		{
			this.AdditionalDomains.Visible = !this.AdditionalDomains.Visible;
		}

		private void grdVars_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (this._userMode && e.ColumnIndex == 2)
			{
				ESVars eSVars = this.bindVars.Current as ESVars;
				if (eSVars == null)
				{
					return;
				}
				if (eSVars.VarType != ESVars.VAR_TYPE.Выводимая && eSVars.Question.Trim() == "")
				{
					eSVars.Question = eSVars.GenerateQuestion();
					this.bindVars.ResetCurrentItem();
				}
				if (eSVars.VarType == ESVars.VAR_TYPE.Выводимая && eSVars.Question.Trim() != "")
				{
					eSVars.Question = "";
					this.bindVars.ResetCurrentItem();
				}
			}
		}

		private void удалитьВершинуToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeView treeView = null;
			switch (this.tabTreeOut.SelectedIndex)
			{
				case 0:
					treeView = this.treeExplain;
					break;
				case 1:
					treeView = this.treeOutRules;
					break;
				case 2:
					treeView = this.treeOutVars;
					break;
			}
			if (treeView != null && treeView.SelectedNode != null)
			{
				treeView.SelectedNode.Remove();
			}
		}

		private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeView treeView = null;
			switch (this.tabTreeOut.SelectedIndex)
			{
				case 0:
					treeView = this.treeExplain;
					break;
				case 1:
					treeView = this.treeOutRules;
					break;
				case 2:
					treeView = this.treeOutVars;
					break;
			}
			if (treeView != null)
			{
				treeView.Nodes.Clear();
			}
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		private void экспортБЗToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.ES.ExportKnowleages() != DialogResult.Cancel)
			{
				MessageBox.Show("Экспорт успешно завершен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.StatusBar = new StatusStrip();
			this.LblStatusLeft = new ToolStripStatusLabel();
			this.LblStatusRight = new ToolStripStatusLabel();
			this.SysImgList = new ImageList(this.components);
			this.OrdImgList = new ImageList(this.components);
			this.восстановитьУмолчанияToolStripMenuItem = new ToolStripMenuItem();
			this.splitContainer2 = new SplitContainer();
			this.TabCtrl = new TabControl();
			this.VarPage = new TabPage();
			this.groupBox2 = new GroupBox();
			this.grdVars = new DataGridView();
			this.colVarName = new DataGridViewTextBoxColumn();
			this.colVarDomain = new DataGridViewComboBoxColumn();
			this.colVarType = new DataGridViewComboBoxColumn();
			this.colVarQuestion = new DataGridViewTextBoxColumn();
			this.colVarDescription = new DataGridViewTextBoxColumn();
			this.btnRemVar = new Button();
			this.btnAddVar = new Button();
			this.groupBox1 = new GroupBox();
			this.btnMore = new Button();
			this.AdditionalDomains = new CheckedListBox();
			this.btnRemDomainValue = new Button();
			this.btnAddDomainValue = new Button();
			this.btnRemDomain = new Button();
			this.btnAddDomain = new Button();
			this.grdDomainValues = new DataGridView();
			this.colDomainValues = new DataGridViewTextBoxColumn();
			this.bindDomainValue = new BindingSource(this.components);
			this.grdDomainNames = new DataGridView();
			this.colDomainName = new DataGridViewTextBoxColumn();
			this.bindDomainName = new BindingSource(this.components);
			this.RulePage = new TabPage();
			this.splitContainer1 = new SplitContainer();
			this.groupBox3 = new GroupBox();
			this.txtExplainRule = new TextBox();
			this.label15 = new Label();
			this.panel1 = new Panel();
			this.btnMoveLast = new Button();
			this.btnMoveNext = new Button();
			this.btnMovePrev = new Button();
			this.btnMoveFirst = new Button();
			this.btnRemRule = new Button();
			this.btnAddRule = new Button();
			this.grdRules = new DataGridView();
			this.colRuleName = new DataGridViewTextBoxColumn();
			this.colRuleExplain = new DataGridViewTextBoxColumn();
			this.colRuleCF = new DataGridViewTextBoxColumn();
			this.colRulePriority = new DataGridViewTextBoxColumn();
			this.groupBox5 = new GroupBox();
			this.btnRemCons = new Button();
			this.btnAddCons = new Button();
			this.grdCons = new DataGridView();
			this.colFactConsVar = new DataGridViewComboBoxColumn();
			this.colFactConsValue = new DataGridViewComboBoxColumn();
			this.colFactConsCF = new DataGridViewTextBoxColumn();
			this.groupBox4 = new GroupBox();
			this.btnRemPrem = new Button();
			this.btnAddPrem = new Button();
			this.grdPrems = new DataGridView();
			this.colFactVar = new DataGridViewComboBoxColumn();
			this.colFactValue = new DataGridViewComboBoxColumn();
			this.colFactCF = new DataGridViewTextBoxColumn();
			this.SettingPage = new TabPage();
			this.label16 = new Label();
			this.cmbGoal = new ComboBox();
			this.label14 = new Label();
			this.btnSaveLog = new Button();
			this.btnClear = new Button();
			this.TxtBoxLog = new RichTextBox();
			this.groupBox7 = new GroupBox();
			this.label13 = new Label();
			this.label12 = new Label();
			this.label11 = new Label();
			this.label10 = new Label();
			this.label9 = new Label();
			this.cmbCFFactPrem = new ComboBox();
			this.cmbCFVA = new ComboBox();
			this.cmbCFCons = new ComboBox();
			this.cmbCFFactCons = new ComboBox();
			this.cmbCFPrem = new ComboBox();
			this.label8 = new Label();
			this.label7 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.label4 = new Label();
			this.groupBox6 = new GroupBox();
			this.btnRestoreDefaultSettings = new Button();
			this.TreeImgList = new ImageList(this.components);
			this.nUpDown = new NumericUpDown();
			this.cmbMLVDirectional = new ComboBox();
			this.cmbRuleChoose = new ComboBox();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.btnStartConsult = new Button();
			this.OutputTab = new TabPage();
			this.splitContainer3 = new SplitContainer();
			this.groupBox8 = new GroupBox();
			this.tabTreeOut = new TabControl();
			this.tabPageFull = new TabPage();
			this.treeExplain = new TreeView();
			this.contextDelete = new ContextMenuStrip(this.components);
			this.удалитьВершинуToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.очиститьToolStripMenuItem = new ToolStripMenuItem();
			this.tabPageRules = new TabPage();
			this.treeOutRules = new TreeView();
			this.tabPageVars = new TabPage();
			this.treeOutVars = new TreeView();
			this.groupBox9 = new GroupBox();
			this.LogExplain = new RichTextBox();
			this.btnRefreshTreeView = new Button();
			this.treeElements = new TreeView();
			this.базаЗнанийToolStripMenuItem1 = new ToolStripMenuItem();
			this.создатьToolStripMenuItem1 = new ToolStripMenuItem();
			this.открытьToolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripMenuItem8 = new ToolStripSeparator();
			this.закрытьToolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripMenuItem9 = new ToolStripSeparator();
			this.сохранитьToolStripMenuItem1 = new ToolStripMenuItem();
			this.сохранитьКакToolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripMenuItem10 = new ToolStripSeparator();
			this.выходToolStripMenuItem1 = new ToolStripMenuItem();
			this.консультацияToolStripMenuItem1 = new ToolStripMenuItem();
			this.стартToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem11 = new ToolStripSeparator();
			this.валидацияМоделиToolStripMenuItem = new ToolStripMenuItem();
			this.настройкиToolStripMenuItem1 = new ToolStripMenuItem();
			this.каскадноеУдалениеToolStripMenuItem1 = new ToolStripMenuItem();
			this.компонентаОбъясненияToolStripMenuItem = new ToolStripMenuItem();
			this.автоколлапсДеревьевВыводаToolStripMenuItem = new ToolStripMenuItem();
			this.полностьюРаскрыватьПоследнююКонсультациюToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem7 = new ToolStripSeparator();
			this.установкиПоУмолчаниюToolStripMenuItem = new ToolStripMenuItem();
			this.справкаToolStripMenuItem = new ToolStripMenuItem();
			this.оПрограммеToolStripMenuItem1 = new ToolStripMenuItem();
			this.menuStrip1 = new MenuStrip();
			this.toolTips = new ToolTip(this.components);
			this.HelpProvide = new HelpProvider();
			this.bindVars = new BindingSource(this.components);
			this.bindRules = new BindingSource(this.components);
			this.bindPrems = new BindingSource(this.components);
			this.bindCons = new BindingSource(this.components);
			this.bindPremValues = new BindingSource(this.components);
			this.bindConsValues = new BindingSource(this.components);
			this.экспортБЗToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.StatusBar.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.TabCtrl.SuspendLayout();
			this.VarPage.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.grdVars).BeginInit();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.grdDomainValues).BeginInit();
			((ISupportInitialize)this.bindDomainValue).BeginInit();
			((ISupportInitialize)this.grdDomainNames).BeginInit();
			((ISupportInitialize)this.bindDomainName).BeginInit();
			this.RulePage.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.grdRules).BeginInit();
			this.groupBox5.SuspendLayout();
			((ISupportInitialize)this.grdCons).BeginInit();
			this.groupBox4.SuspendLayout();
			((ISupportInitialize)this.grdPrems).BeginInit();
			this.SettingPage.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupBox6.SuspendLayout();
			((ISupportInitialize)this.nUpDown).BeginInit();
			this.OutputTab.SuspendLayout();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.tabTreeOut.SuspendLayout();
			this.tabPageFull.SuspendLayout();
			this.contextDelete.SuspendLayout();
			this.tabPageRules.SuspendLayout();
			this.tabPageVars.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((ISupportInitialize)this.bindVars).BeginInit();
			((ISupportInitialize)this.bindRules).BeginInit();
			((ISupportInitialize)this.bindPrems).BeginInit();
			((ISupportInitialize)this.bindCons).BeginInit();
			((ISupportInitialize)this.bindPremValues).BeginInit();
			((ISupportInitialize)this.bindConsValues).BeginInit();
			base.SuspendLayout();
			this.StatusBar.Items.AddRange(new ToolStripItem[]
			{
				this.LblStatusLeft,
				this.LblStatusRight
			});
			this.StatusBar.Location = new Point(0, 639);
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Size = new Size(992, 24);
			this.StatusBar.TabIndex = 2;
			this.LblStatusLeft.Margin = new Padding(3, 3, 0, 2);
			this.LblStatusLeft.Name = "LblStatusLeft";
			this.LblStatusLeft.Size = new Size(485, 19);
			this.LblStatusLeft.Spring = true;
			this.LblStatusLeft.Text = "Добро пожаловать!";
			this.LblStatusLeft.TextAlign = ContentAlignment.MiddleLeft;
			this.LblStatusRight.AutoSize = false;
			this.LblStatusRight.Margin = new Padding(0, 3, 3, 2);
			this.LblStatusRight.Name = "LblStatusRight";
			this.LblStatusRight.RightToLeft = RightToLeft.Yes;
			this.LblStatusRight.Size = new Size(485, 19);
			this.LblStatusRight.Spring = true;
			this.LblStatusRight.TextAlign = ContentAlignment.MiddleLeft;
			this.SysImgList.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("SysImgList.ImageStream");
			this.SysImgList.TransparentColor = Color.Fuchsia;
			this.SysImgList.Images.SetKeyName(0, "add.bmp");
			this.SysImgList.Images.SetKeyName(1, "delete.bmp");
			this.SysImgList.Images.SetKeyName(2, "refresh.png");
			this.SysImgList.Images.SetKeyName(3, "save.bmp");
			this.SysImgList.Images.SetKeyName(4, "cross24.bmp");
			this.SysImgList.Images.SetKeyName(5, "Network.ico");
			this.SysImgList.Images.SetKeyName(6, "Refresh.ico");
			this.SysImgList.Images.SetKeyName(7, "Bittorrent Plus.ico");
			this.OrdImgList.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("OrdImgList.ImageStream");
			this.OrdImgList.TransparentColor = Color.Fuchsia;
			this.OrdImgList.Images.SetKeyName(0, "last24_h.BMP");
			this.OrdImgList.Images.SetKeyName(1, "next24_h.BMP");
			this.OrdImgList.Images.SetKeyName(2, "next24_h2.BMP");
			this.OrdImgList.Images.SetKeyName(3, "first24_h.bmp");
			this.восстановитьУмолчанияToolStripMenuItem.Name = "восстановитьУмолчанияToolStripMenuItem";
			this.восстановитьУмолчанияToolStripMenuItem.Size = new Size(32, 19);
			this.splitContainer2.Dock = DockStyle.Fill;
			this.splitContainer2.Location = new Point(0, 27);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Panel1.Controls.Add(this.TabCtrl);
			this.splitContainer2.Panel2.Controls.Add(this.btnRefreshTreeView);
			this.splitContainer2.Panel2.Controls.Add(this.treeElements);
			this.splitContainer2.Size = new Size(992, 612);
			this.splitContainer2.SplitterDistance = 750;
			this.splitContainer2.TabIndex = 5;
			this.splitContainer2.DoubleClick += new EventHandler(this.splitContainer2_DoubleClick);
			this.TabCtrl.Controls.Add(this.VarPage);
			this.TabCtrl.Controls.Add(this.RulePage);
			this.TabCtrl.Controls.Add(this.SettingPage);
			this.TabCtrl.Controls.Add(this.OutputTab);
			this.TabCtrl.Dock = DockStyle.Fill;
			this.TabCtrl.Location = new Point(0, 0);
			this.TabCtrl.Name = "TabCtrl";
			this.TabCtrl.SelectedIndex = 0;
			this.TabCtrl.Size = new Size(750, 612);
			this.TabCtrl.TabIndex = 4;
			this.VarPage.Controls.Add(this.groupBox2);
			this.VarPage.Controls.Add(this.groupBox1);
			this.VarPage.Location = new Point(4, 22);
			this.VarPage.Name = "VarPage";
			this.VarPage.Padding = new Padding(3);
			this.VarPage.Size = new Size(742, 586);
			this.VarPage.TabIndex = 0;
			this.VarPage.Text = "1. Домены и переменные";
			this.VarPage.UseVisualStyleBackColor = true;
			this.groupBox2.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.groupBox2.Controls.Add(this.grdVars);
			this.groupBox2.Controls.Add(this.btnRemVar);
			this.groupBox2.Controls.Add(this.btnAddVar);
			this.groupBox2.Location = new Point(8, 289);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(726, 291);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Переменные";
			this.grdVars.AllowUserToAddRows = false;
			this.grdVars.AllowUserToDeleteRows = false;
			this.grdVars.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.grdVars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.grdVars.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdVars.Columns.AddRange(new DataGridViewColumn[]
			{
				this.colVarName,
				this.colVarDomain,
				this.colVarType,
				this.colVarQuestion,
				this.colVarDescription
			});
			this.grdVars.Location = new Point(15, 28);
			this.grdVars.Name = "grdVars";
			this.grdVars.Size = new Size(699, 217);
			this.grdVars.TabIndex = 9;
			this.grdVars.CellEndEdit += new DataGridViewCellEventHandler(this.grdVars_CellEndEdit);
			this.grdVars.CellValidating += new DataGridViewCellValidatingEventHandler(this.grdVars_CellValidating);
			this.colVarName.HeaderText = "Имя переменной";
			this.colVarName.Name = "colVarName";
			this.colVarDomain.HeaderText = "Домен";
			this.colVarDomain.Name = "colVarDomain";
			this.colVarDomain.Resizable = DataGridViewTriState.True;
			this.colVarDomain.SortMode = DataGridViewColumnSortMode.Automatic;
			this.colVarType.HeaderText = "Тип";
			this.colVarType.Name = "colVarType";
			this.colVarQuestion.HeaderText = "Вопрос";
			this.colVarQuestion.Name = "colVarQuestion";
			this.colVarDescription.HeaderText = "Описание";
			this.colVarDescription.Name = "colVarDescription";
			this.btnRemVar.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnRemVar.ImageIndex = 1;
			this.btnRemVar.ImageList = this.SysImgList;
			this.btnRemVar.Location = new Point(614, 251);
			this.btnRemVar.Name = "btnRemVar";
			this.btnRemVar.Size = new Size(100, 34);
			this.btnRemVar.TabIndex = 8;
			this.btnRemVar.Text = "Удалить";
			this.btnRemVar.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnRemVar.UseVisualStyleBackColor = true;
			this.btnRemVar.Click += new EventHandler(this.btnRemVar_Click);
			this.btnAddVar.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnAddVar.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnAddVar.ImageIndex = 0;
			this.btnAddVar.ImageList = this.SysImgList;
			this.btnAddVar.Location = new Point(508, 251);
			this.btnAddVar.Name = "btnAddVar";
			this.btnAddVar.Size = new Size(100, 34);
			this.btnAddVar.TabIndex = 7;
			this.btnAddVar.Text = "Добавить";
			this.btnAddVar.TextAlign = ContentAlignment.MiddleRight;
			this.btnAddVar.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnAddVar.UseVisualStyleBackColor = true;
			this.btnAddVar.Click += new EventHandler(this.btnAddVar_Click);
			this.groupBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.groupBox1.Controls.Add(this.btnMore);
			this.groupBox1.Controls.Add(this.AdditionalDomains);
			this.groupBox1.Controls.Add(this.btnRemDomainValue);
			this.groupBox1.Controls.Add(this.btnAddDomainValue);
			this.groupBox1.Controls.Add(this.btnRemDomain);
			this.groupBox1.Controls.Add(this.btnAddDomain);
			this.groupBox1.Controls.Add(this.grdDomainValues);
			this.groupBox1.Controls.Add(this.grdDomainNames);
			this.groupBox1.Location = new Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(726, 275);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Домены допустимых значений";
			this.btnMore.FlatAppearance.BorderColor = Color.White;
			this.btnMore.FlatAppearance.MouseOverBackColor = Color.White;
			this.btnMore.ImageIndex = 7;
			this.btnMore.ImageList = this.SysImgList;
			this.btnMore.Location = new Point(15, 234);
			this.btnMore.Name = "btnMore";
			this.btnMore.Size = new Size(32, 32);
			this.btnMore.TabIndex = 10;
			this.toolTips.SetToolTip(this.btnMore, "Встроенные домены:\r\nнажмите для вставки \r\nпредустановленного домена");
			this.btnMore.UseVisualStyleBackColor = true;
			this.btnMore.Click += new EventHandler(this.btnMore_Click);
			this.AdditionalDomains.CheckOnClick = true;
			this.AdditionalDomains.FormattingEnabled = true;
			this.AdditionalDomains.Location = new Point(33, 138);
			this.AdditionalDomains.Name = "AdditionalDomains";
			this.AdditionalDomains.Size = new Size(144, 109);
			this.AdditionalDomains.TabIndex = 9;
			this.AdditionalDomains.Visible = false;
			this.AdditionalDomains.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
			this.btnRemDomainValue.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnRemDomainValue.ImageIndex = 1;
			this.btnRemDomainValue.ImageList = this.SysImgList;
			this.btnRemDomainValue.Location = new Point(614, 235);
			this.btnRemDomainValue.Name = "btnRemDomainValue";
			this.btnRemDomainValue.Size = new Size(100, 34);
			this.btnRemDomainValue.TabIndex = 8;
			this.btnRemDomainValue.Text = "Удалить";
			this.btnRemDomainValue.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnRemDomainValue.UseVisualStyleBackColor = true;
			this.btnRemDomainValue.Click += new EventHandler(this.btnRemDomainValue_Click);
			this.btnAddDomainValue.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnAddDomainValue.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnAddDomainValue.ImageIndex = 0;
			this.btnAddDomainValue.ImageList = this.SysImgList;
			this.btnAddDomainValue.Location = new Point(508, 235);
			this.btnAddDomainValue.Name = "btnAddDomainValue";
			this.btnAddDomainValue.Size = new Size(100, 34);
			this.btnAddDomainValue.TabIndex = 7;
			this.btnAddDomainValue.Text = "Добавить";
			this.btnAddDomainValue.TextAlign = ContentAlignment.MiddleRight;
			this.btnAddDomainValue.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnAddDomainValue.UseVisualStyleBackColor = true;
			this.btnAddDomainValue.Click += new EventHandler(this.btnAddDomainValue_Click);
			this.btnRemDomain.ImageIndex = 1;
			this.btnRemDomain.ImageList = this.SysImgList;
			this.btnRemDomain.Location = new Point(240, 235);
			this.btnRemDomain.Name = "btnRemDomain";
			this.btnRemDomain.Size = new Size(100, 34);
			this.btnRemDomain.TabIndex = 6;
			this.btnRemDomain.Text = "Удалить";
			this.btnRemDomain.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnRemDomain.UseVisualStyleBackColor = true;
			this.btnRemDomain.Click += new EventHandler(this.btnRemDomain_Click);
			this.btnAddDomain.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnAddDomain.ImageIndex = 0;
			this.btnAddDomain.ImageList = this.SysImgList;
			this.btnAddDomain.Location = new Point(134, 235);
			this.btnAddDomain.Name = "btnAddDomain";
			this.btnAddDomain.Size = new Size(100, 34);
			this.btnAddDomain.TabIndex = 5;
			this.btnAddDomain.Text = "Добавить";
			this.btnAddDomain.TextAlign = ContentAlignment.MiddleRight;
			this.btnAddDomain.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnAddDomain.UseVisualStyleBackColor = true;
			this.btnAddDomain.Click += new EventHandler(this.btnAddDomain_Click);
			this.grdDomainValues.AllowUserToAddRows = false;
			this.grdDomainValues.AllowUserToDeleteRows = false;
			this.grdDomainValues.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.grdDomainValues.AutoGenerateColumns = false;
			this.grdDomainValues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.grdDomainValues.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdDomainValues.Columns.AddRange(new DataGridViewColumn[]
			{
				this.colDomainValues
			});
			this.grdDomainValues.DataSource = this.bindDomainValue;
			this.grdDomainValues.Location = new Point(389, 28);
			this.grdDomainValues.Name = "grdDomainValues";
			this.grdDomainValues.Size = new Size(325, 201);
			this.grdDomainValues.TabIndex = 4;
			this.grdDomainValues.CellValidating += new DataGridViewCellValidatingEventHandler(this.grdDomainValues_CellValidating);
			this.colDomainValues.HeaderText = "Значения доменов";
			this.colDomainValues.Name = "colDomainValues";
			this.bindDomainValue.CurrentItemChanged += new EventHandler(this.Binding_ItemChanged);
			this.grdDomainNames.AllowUserToAddRows = false;
			this.grdDomainNames.AllowUserToDeleteRows = false;
			this.grdDomainNames.AutoGenerateColumns = false;
			this.grdDomainNames.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.grdDomainNames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdDomainNames.Columns.AddRange(new DataGridViewColumn[]
			{
				this.colDomainName
			});
			this.grdDomainNames.DataSource = this.bindDomainName;
			this.grdDomainNames.Location = new Point(15, 28);
			this.grdDomainNames.Name = "grdDomainNames";
			this.grdDomainNames.Size = new Size(325, 201);
			this.grdDomainNames.TabIndex = 2;
			this.grdDomainNames.CellValidating += new DataGridViewCellValidatingEventHandler(this.grdDomainNames_CellValidating);
			this.colDomainName.HeaderText = "Название домена";
			this.colDomainName.MaxInputLength = 100;
			this.colDomainName.Name = "colDomainName";
			this.bindDomainName.CurrentItemChanged += new EventHandler(this.Binding_ItemChanged);
			this.bindDomainName.PositionChanged += new EventHandler(this.bindDomainName_PositionChanged);
			this.RulePage.Controls.Add(this.splitContainer1);
			this.RulePage.Location = new Point(4, 22);
			this.RulePage.Name = "RulePage";
			this.RulePage.Padding = new Padding(3);
			this.RulePage.Size = new Size(742, 586);
			this.RulePage.TabIndex = 1;
			this.RulePage.Text = "2. Правила";
			this.RulePage.UseVisualStyleBackColor = true;
			this.splitContainer1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.splitContainer1.Location = new Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = Orientation.Horizontal;
			this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
			this.splitContainer1.Panel1.RightToLeft = RightToLeft.No;
			this.splitContainer1.Panel2.Controls.Add(this.groupBox5);
			this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
			this.splitContainer1.Panel2.RightToLeft = RightToLeft.No;
			this.splitContainer1.Size = new Size(736, 580);
			this.splitContainer1.SplitterDistance = 356;
			this.splitContainer1.TabIndex = 0;
			this.groupBox3.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.groupBox3.Controls.Add(this.txtExplainRule);
			this.groupBox3.Controls.Add(this.label15);
			this.groupBox3.Controls.Add(this.panel1);
			this.groupBox3.Controls.Add(this.btnRemRule);
			this.groupBox3.Controls.Add(this.btnAddRule);
			this.groupBox3.Controls.Add(this.grdRules);
			this.groupBox3.Location = new Point(5, 1);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(724, 352);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Набор правил";
			this.txtExplainRule.AllowDrop = true;
			this.txtExplainRule.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.txtExplainRule.AutoCompleteCustomSource.AddRange(new string[]
			{
				"Правило для ...",
				"Это правило ...",
				"Обозначение ..."
			});
			this.txtExplainRule.AutoCompleteMode = AutoCompleteMode.Append;
			this.txtExplainRule.Location = new Point(15, 326);
			this.txtExplainRule.Name = "txtExplainRule";
			this.txtExplainRule.Size = new Size(445, 20);
			this.txtExplainRule.TabIndex = 10;
			this.txtExplainRule.KeyUp += new KeyEventHandler(this.txtExplainRule_KeyUp);
			this.txtExplainRule.Validated += new EventHandler(this.txtExplainRule_Validated);
			this.label15.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.label15.AutoSize = true;
			this.label15.Location = new Point(18, 308);
			this.label15.Margin = new Padding(0, 0, 3, 0);
			this.label15.Name = "label15";
			this.label15.Size = new Size(187, 15);
			this.label15.TabIndex = 11;
			this.label15.Text = "Объяснение правила (Reason):";
			this.panel1.Anchor = AnchorStyles.Right;
			this.panel1.Controls.Add(this.btnMoveLast);
			this.panel1.Controls.Add(this.btnMoveNext);
			this.panel1.Controls.Add(this.btnMovePrev);
			this.panel1.Controls.Add(this.btnMoveFirst);
			this.panel1.Location = new Point(678, 99);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(38, 153);
			this.panel1.TabIndex = 9;
			this.btnMoveLast.ImageIndex = 3;
			this.btnMoveLast.ImageList = this.OrdImgList;
			this.btnMoveLast.Location = new Point(3, 117);
			this.btnMoveLast.Name = "btnMoveLast";
			this.btnMoveLast.Size = new Size(32, 32);
			this.btnMoveLast.TabIndex = 3;
			this.toolTips.SetToolTip(this.btnMoveLast, "Опустить правило\r\nна последнюю позицию");
			this.btnMoveLast.UseVisualStyleBackColor = true;
			this.btnMoveLast.Click += new EventHandler(this.btnMoveLast_Click);
			this.btnMoveNext.ImageIndex = 2;
			this.btnMoveNext.ImageList = this.OrdImgList;
			this.btnMoveNext.Location = new Point(3, 79);
			this.btnMoveNext.Name = "btnMoveNext";
			this.btnMoveNext.Size = new Size(32, 32);
			this.btnMoveNext.TabIndex = 2;
			this.toolTips.SetToolTip(this.btnMoveNext, "Опустить правило\r\nна 1 позицию вниз");
			this.btnMoveNext.UseVisualStyleBackColor = true;
			this.btnMoveNext.Click += new EventHandler(this.btnMoveNext_Click);
			this.btnMovePrev.ImageIndex = 1;
			this.btnMovePrev.ImageList = this.OrdImgList;
			this.btnMovePrev.Location = new Point(3, 41);
			this.btnMovePrev.Name = "btnMovePrev";
			this.btnMovePrev.Size = new Size(32, 32);
			this.btnMovePrev.TabIndex = 1;
			this.toolTips.SetToolTip(this.btnMovePrev, "Поднять правило\r\nна 1 позицию вверх");
			this.btnMovePrev.UseVisualStyleBackColor = true;
			this.btnMovePrev.Click += new EventHandler(this.btnMovePrev_Click);
			this.btnMoveFirst.ImageIndex = 0;
			this.btnMoveFirst.ImageList = this.OrdImgList;
			this.btnMoveFirst.Location = new Point(3, 3);
			this.btnMoveFirst.Name = "btnMoveFirst";
			this.btnMoveFirst.Size = new Size(32, 32);
			this.btnMoveFirst.TabIndex = 0;
			this.toolTips.SetToolTip(this.btnMoveFirst, "Поднять правило\r\nна первую позицию");
			this.btnMoveFirst.UseVisualStyleBackColor = true;
			this.btnMoveFirst.Click += new EventHandler(this.btnMoveFirst_Click);
			this.btnRemRule.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnRemRule.ImageIndex = 1;
			this.btnRemRule.ImageList = this.SysImgList;
			this.btnRemRule.Location = new Point(572, 312);
			this.btnRemRule.Name = "btnRemRule";
			this.btnRemRule.Size = new Size(100, 34);
			this.btnRemRule.TabIndex = 8;
			this.btnRemRule.Text = "Удалить";
			this.btnRemRule.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnRemRule.UseVisualStyleBackColor = true;
			this.btnRemRule.Click += new EventHandler(this.btnRemRule_Click);
			this.btnAddRule.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnAddRule.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnAddRule.ImageIndex = 0;
			this.btnAddRule.ImageList = this.SysImgList;
			this.btnAddRule.Location = new Point(466, 312);
			this.btnAddRule.Name = "btnAddRule";
			this.btnAddRule.Size = new Size(100, 34);
			this.btnAddRule.TabIndex = 7;
			this.btnAddRule.Text = "Добавить";
			this.btnAddRule.TextAlign = ContentAlignment.MiddleRight;
			this.btnAddRule.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnAddRule.UseVisualStyleBackColor = true;
			this.btnAddRule.Click += new EventHandler(this.btnAddRule_Click);
			this.grdRules.AllowDrop = true;
			this.grdRules.AllowUserToAddRows = false;
			this.grdRules.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.grdRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new Font("Microsoft Sans Serif", 7.471698f, FontStyle.Regular, GraphicsUnit.Point, 204);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.grdRules.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.grdRules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdRules.Columns.AddRange(new DataGridViewColumn[]
			{
				this.colRuleName,
				this.colRuleExplain,
				this.colRuleCF,
				this.colRulePriority
			});
			this.grdRules.Location = new Point(15, 28);
			this.grdRules.MultiSelect = false;
			this.grdRules.Name = "grdRules";
			this.grdRules.Size = new Size(657, 278);
			this.grdRules.TabIndex = 0;
			this.grdRules.CellValidating += new DataGridViewCellValidatingEventHandler(this.grdRules_CellValidating);
			this.colRuleName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			this.colRuleName.DefaultCellStyle = dataGridViewCellStyle2;
			this.colRuleName.FillWeight = 2.719239f;
			this.colRuleName.HeaderText = "Название";
			this.colRuleName.MinimumWidth = 10;
			this.colRuleName.Name = "colRuleName";
			this.colRuleExplain.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.colRuleExplain.FillWeight = 14.54793f;
			this.colRuleExplain.HeaderText = "Объясняющая часть правила";
			this.colRuleExplain.Name = "colRuleExplain";
			this.colRuleExplain.ReadOnly = true;
			this.colRuleCF.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.colRuleCF.DefaultCellStyle = dataGridViewCellStyle3;
			this.colRuleCF.FillWeight = 50f;
			this.colRuleCF.HeaderText = "CF";
			this.colRuleCF.MinimumWidth = 10;
			this.colRuleCF.Name = "colRuleCF";
			this.colRuleCF.Width = 35;
			this.colRulePriority.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.colRulePriority.DefaultCellStyle = dataGridViewCellStyle4;
			this.colRulePriority.FillWeight = 50f;
			this.colRulePriority.HeaderText = "PR";
			this.colRulePriority.Name = "colRulePriority";
			this.colRulePriority.ToolTipText = "Приоритет правила";
			this.colRulePriority.Width = 35;
			this.groupBox5.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.groupBox5.Controls.Add(this.btnRemCons);
			this.groupBox5.Controls.Add(this.btnAddCons);
			this.groupBox5.Controls.Add(this.grdCons);
			this.groupBox5.Location = new Point(369, 2);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new Size(360, 215);
			this.groupBox5.TabIndex = 1;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Заключения";
			this.btnRemCons.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.btnRemCons.ImageIndex = 1;
			this.btnRemCons.ImageList = this.SysImgList;
			this.btnRemCons.Location = new Point(246, 175);
			this.btnRemCons.Name = "btnRemCons";
			this.btnRemCons.Size = new Size(100, 34);
			this.btnRemCons.TabIndex = 12;
			this.btnRemCons.Text = "Удалить";
			this.btnRemCons.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnRemCons.UseVisualStyleBackColor = true;
			this.btnRemCons.Click += new EventHandler(this.btnRemCons_Click);
			this.btnAddCons.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.btnAddCons.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnAddCons.ImageIndex = 0;
			this.btnAddCons.ImageList = this.SysImgList;
			this.btnAddCons.Location = new Point(140, 175);
			this.btnAddCons.Name = "btnAddCons";
			this.btnAddCons.Size = new Size(100, 34);
			this.btnAddCons.TabIndex = 11;
			this.btnAddCons.Text = "Добавить";
			this.btnAddCons.TextAlign = ContentAlignment.MiddleRight;
			this.btnAddCons.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnAddCons.UseVisualStyleBackColor = true;
			this.btnAddCons.Click += new EventHandler(this.btnAddCons_Click);
			this.grdCons.AllowUserToAddRows = false;
			this.grdCons.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.grdCons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.grdCons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdCons.Columns.AddRange(new DataGridViewColumn[]
			{
				this.colFactConsVar,
				this.colFactConsValue,
				this.colFactConsCF
			});
			this.grdCons.EditMode = DataGridViewEditMode.EditOnEnter;
			this.grdCons.Location = new Point(15, 27);
			this.grdCons.Name = "grdCons";
			this.grdCons.Size = new Size(331, 142);
			this.grdCons.TabIndex = 2;
			this.grdCons.CellValidating += new DataGridViewCellValidatingEventHandler(this.grdCons_CellValidating);
			this.colFactConsVar.HeaderText = "Переменная";
			this.colFactConsVar.Name = "colFactConsVar";
			this.colFactConsValue.HeaderText = "Значение";
			this.colFactConsValue.Name = "colFactConsValue";
			this.colFactConsCF.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colFactConsCF.HeaderText = "CF";
			this.colFactConsCF.Name = "colFactConsCF";
			this.colFactConsCF.Resizable = DataGridViewTriState.True;
			this.colFactConsCF.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.colFactConsCF.Width = 28;
			this.groupBox4.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.groupBox4.Controls.Add(this.btnRemPrem);
			this.groupBox4.Controls.Add(this.btnAddPrem);
			this.groupBox4.Controls.Add(this.grdPrems);
			this.groupBox4.Location = new Point(5, 2);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(360, 215);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Посылки";
			this.btnRemPrem.ImageIndex = 1;
			this.btnRemPrem.ImageList = this.SysImgList;
			this.btnRemPrem.Location = new Point(245, 175);
			this.btnRemPrem.Name = "btnRemPrem";
			this.btnRemPrem.Size = new Size(100, 34);
			this.btnRemPrem.TabIndex = 10;
			this.btnRemPrem.Text = "Удалить";
			this.btnRemPrem.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnRemPrem.UseVisualStyleBackColor = true;
			this.btnRemPrem.Click += new EventHandler(this.btnRemPrem_Click);
			this.btnAddPrem.ImageAlign = ContentAlignment.MiddleLeft;
			this.btnAddPrem.ImageIndex = 0;
			this.btnAddPrem.ImageList = this.SysImgList;
			this.btnAddPrem.Location = new Point(139, 175);
			this.btnAddPrem.Name = "btnAddPrem";
			this.btnAddPrem.Size = new Size(100, 34);
			this.btnAddPrem.TabIndex = 9;
			this.btnAddPrem.Text = "Добавить";
			this.btnAddPrem.TextAlign = ContentAlignment.MiddleRight;
			this.btnAddPrem.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.btnAddPrem.UseVisualStyleBackColor = true;
			this.btnAddPrem.Click += new EventHandler(this.btnAddPrem_Click);
			this.grdPrems.AllowUserToAddRows = false;
			this.grdPrems.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.grdPrems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.grdPrems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdPrems.Columns.AddRange(new DataGridViewColumn[]
			{
				this.colFactVar,
				this.colFactValue,
				this.colFactCF
			});
			this.grdPrems.EditMode = DataGridViewEditMode.EditOnEnter;
			this.grdPrems.Location = new Point(15, 27);
			this.grdPrems.Name = "grdPrems";
			this.grdPrems.Size = new Size(330, 142);
			this.grdPrems.TabIndex = 0;
			this.grdPrems.CellValidating += new DataGridViewCellValidatingEventHandler(this.grdPrems_CellValidating);
			this.colFactVar.HeaderText = "Переменная";
			this.colFactVar.Name = "colFactVar";
			this.colFactValue.HeaderText = "Значение";
			this.colFactValue.Name = "colFactValue";
			this.colFactCF.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colFactCF.HeaderText = "CF";
			this.colFactCF.Name = "colFactCF";
			this.colFactCF.Resizable = DataGridViewTriState.True;
			this.colFactCF.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.colFactCF.Width = 28;
			this.SettingPage.Controls.Add(this.label16);
			this.SettingPage.Controls.Add(this.cmbGoal);
			this.SettingPage.Controls.Add(this.label14);
			this.SettingPage.Controls.Add(this.btnSaveLog);
			this.SettingPage.Controls.Add(this.btnClear);
			this.SettingPage.Controls.Add(this.TxtBoxLog);
			this.SettingPage.Controls.Add(this.groupBox7);
			this.SettingPage.Controls.Add(this.groupBox6);
			this.SettingPage.Controls.Add(this.btnStartConsult);
			this.SettingPage.Location = new Point(4, 22);
			this.SettingPage.Name = "SettingPage";
			this.SettingPage.Size = new Size(742, 586);
			this.SettingPage.TabIndex = 2;
			this.SettingPage.Text = "3. Консультация, установки";
			this.SettingPage.UseVisualStyleBackColor = true;
			this.label16.AutoSize = true;
			this.label16.Location = new Point(8, 275);
			this.label16.Name = "label16";
			this.label16.Size = new Size(31, 15);
			this.label16.TabIndex = 16;
			this.label16.Text = "Log:";
			this.cmbGoal.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbGoal.FormattingEnabled = true;
			this.cmbGoal.Location = new Point(365, 194);
			this.cmbGoal.Name = "cmbGoal";
			this.cmbGoal.Size = new Size(153, 21);
			this.cmbGoal.TabIndex = 15;
			this.label14.AutoSize = true;
			this.label14.Location = new Point(236, 197);
			this.label14.Name = "label14";
			this.label14.Size = new Size(123, 15);
			this.label14.TabIndex = 14;
			this.label14.Text = "Цель консультации:";
			this.toolTips.SetToolTip(this.label14, "Определяет переменную, \r\nвыводимую во время консультации");
			this.btnSaveLog.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnSaveLog.ImageIndex = 3;
			this.btnSaveLog.ImageList = this.SysImgList;
			this.btnSaveLog.Location = new Point(662, 258);
			this.btnSaveLog.Name = "btnSaveLog";
			this.btnSaveLog.Size = new Size(32, 32);
			this.btnSaveLog.TabIndex = 11;
			this.toolTips.SetToolTip(this.btnSaveLog, "Сохранение Log'a");
			this.btnSaveLog.UseVisualStyleBackColor = true;
			this.btnSaveLog.Click += new EventHandler(this.btnSaveLog_Click);
			this.btnClear.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnClear.ImageIndex = 4;
			this.btnClear.ImageList = this.SysImgList;
			this.btnClear.Location = new Point(700, 258);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new Size(32, 32);
			this.btnClear.TabIndex = 10;
			this.toolTips.SetToolTip(this.btnClear, "Очистить Log");
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new EventHandler(this.btnClear_Click);
			this.TxtBoxLog.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.TxtBoxLog.AutoWordSelection = true;
			this.TxtBoxLog.BackColor = Color.LightCyan;
			this.TxtBoxLog.Font = new Font("Verdana", 10.18868f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.TxtBoxLog.Location = new Point(9, 296);
			this.TxtBoxLog.Name = "TxtBoxLog";
			this.TxtBoxLog.ReadOnly = true;
			this.TxtBoxLog.Size = new Size(723, 282);
			this.TxtBoxLog.TabIndex = 9;
			this.TxtBoxLog.Text = "";
			this.groupBox7.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.groupBox7.Controls.Add(this.label13);
			this.groupBox7.Controls.Add(this.label12);
			this.groupBox7.Controls.Add(this.label11);
			this.groupBox7.Controls.Add(this.label10);
			this.groupBox7.Controls.Add(this.label9);
			this.groupBox7.Controls.Add(this.cmbCFFactPrem);
			this.groupBox7.Controls.Add(this.cmbCFVA);
			this.groupBox7.Controls.Add(this.cmbCFCons);
			this.groupBox7.Controls.Add(this.cmbCFFactCons);
			this.groupBox7.Controls.Add(this.cmbCFPrem);
			this.groupBox7.Controls.Add(this.label8);
			this.groupBox7.Controls.Add(this.label7);
			this.groupBox7.Controls.Add(this.label6);
			this.groupBox7.Controls.Add(this.label5);
			this.groupBox7.Controls.Add(this.label4);
			this.groupBox7.Location = new Point(384, 21);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new Size(338, 160);
			this.groupBox7.TabIndex = 1;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Стратегии пересчета cf";
			this.label13.AutoSize = true;
			this.label13.Location = new Point(285, 79);
			this.label13.Name = "label13";
			this.label13.Size = new Size(47, 15);
			this.label13.TabIndex = 25;
			this.label13.Text = "(CFCO)";
			this.label12.AutoSize = true;
			this.label12.Location = new Point(285, 105);
			this.label12.Name = "label12";
			this.label12.Size = new Size(45, 15);
			this.label12.TabIndex = 24;
			this.label12.Text = "(CFJO)";
			this.label11.AutoSize = true;
			this.label11.Location = new Point(285, 52);
			this.label11.Name = "label11";
			this.label11.Size = new Size(45, 15);
			this.label11.TabIndex = 23;
			this.label11.Text = "(CFJO)";
			this.label10.AutoSize = true;
			this.label10.Location = new Point(285, 25);
			this.label10.Name = "label10";
			this.label10.Size = new Size(47, 15);
			this.label10.TabIndex = 22;
			this.label10.Text = "(CFCO)";
			this.label9.AutoSize = true;
			this.label9.Location = new Point(285, 133);
			this.label9.Name = "label9";
			this.label9.Size = new Size(44, 15);
			this.label9.TabIndex = 21;
			this.label9.Text = "(CFVA)";
			this.cmbCFFactPrem.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbCFFactPrem.FormattingEnabled = true;
			this.cmbCFFactPrem.Location = new Point(171, 22);
			this.cmbCFFactPrem.Name = "cmbCFFactPrem";
			this.cmbCFFactPrem.Size = new Size(108, 21);
			this.cmbCFFactPrem.TabIndex = 20;
			this.cmbCFFactPrem.SelectionChangeCommitted += new EventHandler(this.Binding_ItemChanged);
			this.cmbCFVA.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbCFVA.FormattingEnabled = true;
			this.cmbCFVA.Location = new Point(171, 130);
			this.cmbCFVA.Name = "cmbCFVA";
			this.cmbCFVA.Size = new Size(108, 21);
			this.cmbCFVA.TabIndex = 19;
			this.cmbCFVA.SelectionChangeCommitted += new EventHandler(this.Binding_ItemChanged);
			this.cmbCFCons.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbCFCons.FormattingEnabled = true;
			this.cmbCFCons.Location = new Point(171, 76);
			this.cmbCFCons.Name = "cmbCFCons";
			this.cmbCFCons.Size = new Size(108, 21);
			this.cmbCFCons.TabIndex = 18;
			this.cmbCFCons.SelectionChangeCommitted += new EventHandler(this.Binding_ItemChanged);
			this.cmbCFFactCons.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbCFFactCons.FormattingEnabled = true;
			this.cmbCFFactCons.Location = new Point(171, 102);
			this.cmbCFFactCons.Name = "cmbCFFactCons";
			this.cmbCFFactCons.Size = new Size(108, 21);
			this.cmbCFFactCons.TabIndex = 17;
			this.cmbCFFactCons.SelectionChangeCommitted += new EventHandler(this.Binding_ItemChanged);
			this.cmbCFPrem.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbCFPrem.FormattingEnabled = true;
			this.cmbCFPrem.Location = new Point(171, 49);
			this.cmbCFPrem.Name = "cmbCFPrem";
			this.cmbCFPrem.Size = new Size(108, 21);
			this.cmbCFPrem.TabIndex = 16;
			this.cmbCFPrem.SelectionChangeCommitted += new EventHandler(this.Binding_ItemChanged);
			this.label8.AutoSize = true;
			this.label8.Location = new Point(6, 79);
			this.label8.Name = "label8";
			this.label8.Size = new Size(97, 15);
			this.label8.TabIndex = 12;
			this.label8.Text = "CF заключения:";
			this.toolTips.SetToolTip(this.label8, "Стратегия вычисления CF всего заключения:\r\n(объединение CF всей посылки и CF правила)\r\n");
			this.label7.AutoSize = true;
			this.label7.Location = new Point(6, 105);
			this.label7.Name = "label7";
			this.label7.Size = new Size(148, 15);
			this.label7.TabIndex = 11;
			this.label7.Text = "CF факта в заключении:";
			this.toolTips.SetToolTip(this.label7, "Стратегия вычисления CF факта в заключении:\r\n(объединение CF заключения и CF факта в заключении)\r\n");
			this.label6.AutoSize = true;
			this.label6.Location = new Point(6, 133);
			this.label6.Name = "label6";
			this.label6.Size = new Size(153, 15);
			this.label6.TabIndex = 10;
			this.label6.Text = "CF объединения фактов:";
			this.toolTips.SetToolTip(this.label6, "Стратегия объединения CF фактов:\r\n(с одинаковыми значениями 1-й переменной)\r\n");
			this.label5.AutoSize = true;
			this.label5.Location = new Point(6, 25);
			this.label5.Name = "label5";
			this.label5.Size = new Size(128, 15);
			this.label5.TabIndex = 9;
			this.label5.Text = "CF факта в посылке:";
			this.toolTips.SetToolTip(this.label5, "Стратегия вычисления CF факта в посылке:\r\n(объединение CF посылки и значения переменной)");
			this.label4.AutoSize = true;
			this.label4.Location = new Point(6, 52);
			this.label4.Name = "label4";
			this.label4.Size = new Size(77, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "CF посылки:";
			this.toolTips.SetToolTip(this.label4, "Стратегия вычисления CF всей посылки:\r\n(объединение CF всех фактов в посылке)\r\n");
			this.groupBox6.Controls.Add(this.btnRestoreDefaultSettings);
			this.groupBox6.Controls.Add(this.nUpDown);
			this.groupBox6.Controls.Add(this.cmbMLVDirectional);
			this.groupBox6.Controls.Add(this.cmbRuleChoose);
			this.groupBox6.Controls.Add(this.label3);
			this.groupBox6.Controls.Add(this.label2);
			this.groupBox6.Controls.Add(this.label1);
			this.groupBox6.Location = new Point(31, 21);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new Size(307, 133);
			this.groupBox6.TabIndex = 0;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Основные установки";
			this.btnRestoreDefaultSettings.ImageKey = "Favourites.ico";
			this.btnRestoreDefaultSettings.ImageList = this.TreeImgList;
			this.btnRestoreDefaultSettings.Location = new Point(6, 98);
			this.btnRestoreDefaultSettings.Name = "btnRestoreDefaultSettings";
			this.btnRestoreDefaultSettings.Size = new Size(289, 26);
			this.btnRestoreDefaultSettings.TabIndex = 14;
			this.btnRestoreDefaultSettings.Text = "Восстановить установки";
			this.btnRestoreDefaultSettings.TextAlign = ContentAlignment.MiddleRight;
			this.btnRestoreDefaultSettings.TextImageRelation = TextImageRelation.TextBeforeImage;
			this.toolTips.SetToolTip(this.btnRestoreDefaultSettings, "Восстановить \"заводские\" настройки ЭС\r\n(основные установки и стратегии пересчета CF)\r\n");
			this.btnRestoreDefaultSettings.UseVisualStyleBackColor = true;
			this.btnRestoreDefaultSettings.Click += new EventHandler(this.btnRestoreDefaultSettings_Click);
			this.TreeImgList.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("TreeImgList.ImageStream");
			this.TreeImgList.TransparentColor = Color.Fuchsia;
			this.TreeImgList.Images.SetKeyName(0, "Favourites.ico");
			this.TreeImgList.Images.SetKeyName(1, "Home.ico");
			this.TreeImgList.Images.SetKeyName(2, "Closed Folder yellow.ico");
			this.TreeImgList.Images.SetKeyName(3, "Notes.ico");
			this.TreeImgList.Images.SetKeyName(4, "Document.ico");
			this.TreeImgList.Images.SetKeyName(5, "Advanced Options.ico");
			this.TreeImgList.Images.SetKeyName(6, "Open Folder yellow.ico");
			this.TreeImgList.Images.SetKeyName(7, "Folder Yellow.ico");
			this.TreeImgList.Images.SetKeyName(8, "Folder Green.ico");
			this.TreeImgList.Images.SetKeyName(9, "Folder Blue.ico");
			this.TreeImgList.Images.SetKeyName(10, "Control Panel 1.ico");
			this.TreeImgList.Images.SetKeyName(11, "Contacts 2.ico");
			this.TreeImgList.Images.SetKeyName(12, "Home 1.ico");
			this.TreeImgList.Images.SetKeyName(13, "Control Panel.ico");
			this.TreeImgList.Images.SetKeyName(14, "Help.ico");
			this.TreeImgList.Images.SetKeyName(15, "Work.ico");
			this.nUpDown.Location = new Point(186, 72);
			this.nUpDown.Name = "nUpDown";
			this.nUpDown.Size = new Size(40, 20);
			this.nUpDown.TabIndex = 11;
			this.nUpDown.ValueChanged += new EventHandler(this.Binding_ItemChanged);
			this.cmbMLVDirectional.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbMLVDirectional.FormattingEnabled = true;
			this.cmbMLVDirectional.Location = new Point(186, 45);
			this.cmbMLVDirectional.Name = "cmbMLVDirectional";
			this.cmbMLVDirectional.Size = new Size(109, 21);
			this.cmbMLVDirectional.TabIndex = 10;
			this.cmbMLVDirectional.SelectionChangeCommitted += new EventHandler(this.Binding_ItemChanged);
			this.cmbRuleChoose.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbRuleChoose.FormattingEnabled = true;
			this.cmbRuleChoose.Location = new Point(186, 18);
			this.cmbRuleChoose.Name = "cmbRuleChoose";
			this.cmbRuleChoose.Size = new Size(109, 21);
			this.cmbRuleChoose.TabIndex = 9;
			this.cmbRuleChoose.SelectionChangeCommitted += new EventHandler(this.Binding_ItemChanged);
			this.label3.AutoSize = true;
			this.label3.Location = new Point(3, 48);
			this.label3.Name = "label3";
			this.label3.Size = new Size(136, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "Направление вывода:";
			this.toolTips.SetToolTip(this.label3, "Прямой / обратный вывод\r\nмеханизма логического вывода");
			this.label2.AutoSize = true;
			this.label2.Location = new Point(3, 21);
			this.label2.Name = "label2";
			this.label2.Size = new Size(177, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Стратегия выборки правила:";
			this.toolTips.SetToolTip(this.label2, "Стратегия выборки правила\r\nиз конфликтного набора");
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 74);
			this.label1.Name = "label1";
			this.label1.Size = new Size(115, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Порог  UNKNOWN:";
			this.toolTips.SetToolTip(this.label1, "Пороговое значение, определяющее\r\nминимальный уровень доверия к факту");
			this.btnStartConsult.Anchor = AnchorStyles.Top;
			this.btnStartConsult.Font = new Font("Verdana", 8.830189f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.btnStartConsult.Image = Resources.Consult;
			this.btnStartConsult.Location = new Point(258, 226);
			this.btnStartConsult.Name = "btnStartConsult";
			this.btnStartConsult.Size = new Size(229, 54);
			this.btnStartConsult.TabIndex = 3;
			this.btnStartConsult.Text = "Начать консультацию";
			this.btnStartConsult.TextAlign = ContentAlignment.MiddleRight;
			this.btnStartConsult.TextImageRelation = TextImageRelation.TextBeforeImage;
			this.btnStartConsult.UseVisualStyleBackColor = true;
			this.btnStartConsult.Click += new EventHandler(this.btnStartConsult_Click);
			this.OutputTab.BackColor = Color.Transparent;
			this.OutputTab.Controls.Add(this.splitContainer3);
			this.OutputTab.Location = new Point(4, 22);
			this.OutputTab.Name = "OutputTab";
			this.OutputTab.Padding = new Padding(3);
			this.OutputTab.Size = new Size(742, 586);
			this.OutputTab.TabIndex = 3;
			this.OutputTab.Text = "4. Дерево вывода";
			this.OutputTab.UseVisualStyleBackColor = true;
			this.splitContainer3.Dock = DockStyle.Fill;
			this.splitContainer3.Location = new Point(3, 3);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = Orientation.Horizontal;
			this.splitContainer3.Panel1.Controls.Add(this.groupBox8);
			this.splitContainer3.Panel1.RightToLeft = RightToLeft.No;
			this.splitContainer3.Panel2.Controls.Add(this.groupBox9);
			this.splitContainer3.Panel2.RightToLeft = RightToLeft.No;
			this.splitContainer3.Size = new Size(736, 580);
			this.splitContainer3.SplitterDistance = 400;
			this.splitContainer3.TabIndex = 2;
			this.groupBox8.Controls.Add(this.tabTreeOut);
			this.groupBox8.Dock = DockStyle.Fill;
			this.groupBox8.Location = new Point(0, 0);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new Size(736, 400);
			this.groupBox8.TabIndex = 0;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Дерево вывода";
			this.tabTreeOut.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.tabTreeOut.Controls.Add(this.tabPageFull);
			this.tabTreeOut.Controls.Add(this.tabPageRules);
			this.tabTreeOut.Controls.Add(this.tabPageVars);
			this.tabTreeOut.Location = new Point(3, 16);
			this.tabTreeOut.Multiline = true;
			this.tabTreeOut.Name = "tabTreeOut";
			this.tabTreeOut.SelectedIndex = 0;
			this.tabTreeOut.Size = new Size(730, 381);
			this.tabTreeOut.TabIndex = 0;
			this.tabPageFull.BackColor = Color.Transparent;
			this.tabPageFull.Controls.Add(this.treeExplain);
			this.tabPageFull.Location = new Point(4, 22);
			this.tabPageFull.Name = "tabPageFull";
			this.tabPageFull.Padding = new Padding(3);
			this.tabPageFull.Size = new Size(722, 355);
			this.tabPageFull.TabIndex = 0;
			this.tabPageFull.Text = "Полный вывод";
			this.tabPageFull.UseVisualStyleBackColor = true;
			this.treeExplain.BackColor = Color.WhiteSmoke;
			this.treeExplain.ContextMenuStrip = this.contextDelete;
			this.treeExplain.Dock = DockStyle.Fill;
			this.treeExplain.Location = new Point(3, 3);
			this.treeExplain.Name = "treeExplain";
			this.treeExplain.Size = new Size(716, 349);
			this.treeExplain.TabIndex = 1;
			this.contextDelete.Items.AddRange(new ToolStripItem[]
			{
				this.удалитьВершинуToolStripMenuItem,
				this.toolStripMenuItem1,
				this.очиститьToolStripMenuItem
			});
			this.contextDelete.Name = "contextDelete";
			this.contextDelete.Size = new Size(190, 58);
			this.удалитьВершинуToolStripMenuItem.Image = Resources._131;
			this.удалитьВершинуToolStripMenuItem.Name = "удалитьВершинуToolStripMenuItem";
			this.удалитьВершинуToolStripMenuItem.Size = new Size(189, 24);
			this.удалитьВершинуToolStripMenuItem.Text = "&Удалить вершину";
			this.удалитьВершинуToolStripMenuItem.Click += new EventHandler(this.удалитьВершинуToolStripMenuItem_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(186, 6);
			this.очиститьToolStripMenuItem.Image = Resources._20480;
			this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
			this.очиститьToolStripMenuItem.Size = new Size(189, 24);
			this.очиститьToolStripMenuItem.Text = "Очистить";
			this.очиститьToolStripMenuItem.Click += new EventHandler(this.очиститьToolStripMenuItem_Click);
			this.tabPageRules.Controls.Add(this.treeOutRules);
			this.tabPageRules.Location = new Point(4, 22);
			this.tabPageRules.Name = "tabPageRules";
			this.tabPageRules.Padding = new Padding(3);
			this.tabPageRules.Size = new Size(722, 355);
			this.tabPageRules.TabIndex = 1;
			this.tabPageRules.Text = "Сработавшие правила";
			this.tabPageRules.UseVisualStyleBackColor = true;
			this.treeOutRules.ContextMenuStrip = this.contextDelete;
			this.treeOutRules.Dock = DockStyle.Fill;
			this.treeOutRules.Location = new Point(3, 3);
			this.treeOutRules.Name = "treeOutRules";
			this.treeOutRules.Size = new Size(716, 349);
			this.treeOutRules.TabIndex = 1;
			this.tabPageVars.Controls.Add(this.treeOutVars);
			this.tabPageVars.Location = new Point(4, 22);
			this.tabPageVars.Name = "tabPageVars";
			this.tabPageVars.Size = new Size(722, 355);
			this.tabPageVars.TabIndex = 2;
			this.tabPageVars.Text = "Означенные переменные";
			this.tabPageVars.UseVisualStyleBackColor = true;
			this.treeOutVars.ContextMenuStrip = this.contextDelete;
			this.treeOutVars.Dock = DockStyle.Fill;
			this.treeOutVars.Location = new Point(0, 0);
			this.treeOutVars.Name = "treeOutVars";
			this.treeOutVars.Size = new Size(722, 355);
			this.treeOutVars.TabIndex = 3;
			this.groupBox9.Controls.Add(this.LogExplain);
			this.groupBox9.Dock = DockStyle.Fill;
			this.groupBox9.Location = new Point(0, 0);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new Size(736, 176);
			this.groupBox9.TabIndex = 0;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Log";
			this.LogExplain.BackColor = Color.WhiteSmoke;
			this.LogExplain.BorderStyle = BorderStyle.None;
			this.LogExplain.Dock = DockStyle.Fill;
			this.LogExplain.Location = new Point(3, 16);
			this.LogExplain.Name = "LogExplain";
			this.LogExplain.Size = new Size(730, 157);
			this.LogExplain.TabIndex = 0;
			this.LogExplain.Text = "";
			this.btnRefreshTreeView.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.btnRefreshTreeView.ImageIndex = 6;
			this.btnRefreshTreeView.ImageList = this.SysImgList;
			this.btnRefreshTreeView.Location = new Point(206, 1);
			this.btnRefreshTreeView.Name = "btnRefreshTreeView";
			this.btnRefreshTreeView.Size = new Size(32, 32);
			this.btnRefreshTreeView.TabIndex = 1;
			this.toolTips.SetToolTip(this.btnRefreshTreeView, "Обновить дерево объектов ЭС");
			this.btnRefreshTreeView.UseVisualStyleBackColor = true;
			this.btnRefreshTreeView.Click += new EventHandler(this.btnRefreshTreeView_Click);
			this.treeElements.BackColor = Color.LightCyan;
			this.treeElements.Dock = DockStyle.Fill;
			this.treeElements.ImageIndex = 0;
			this.treeElements.ImageList = this.TreeImgList;
			this.treeElements.Location = new Point(0, 0);
			this.treeElements.Name = "treeElements";
			this.treeElements.SelectedImageIndex = 0;
			this.treeElements.Size = new Size(238, 612);
			this.treeElements.TabIndex = 0;
			this.базаЗнанийToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.создатьToolStripMenuItem1,
				this.открытьToolStripMenuItem1,
				this.toolStripMenuItem8,
				this.закрытьToolStripMenuItem1,
				this.toolStripSeparator1,
				this.экспортБЗToolStripMenuItem,
				this.toolStripMenuItem9,
				this.сохранитьToolStripMenuItem1,
				this.сохранитьКакToolStripMenuItem1,
				this.toolStripMenuItem10,
				this.выходToolStripMenuItem1
			});
			this.базаЗнанийToolStripMenuItem1.Name = "базаЗнанийToolStripMenuItem1";
			this.базаЗнанийToolStripMenuItem1.Size = new Size(98, 23);
			this.базаЗнанийToolStripMenuItem1.Text = "&База знаний";
			this.создатьToolStripMenuItem1.Image = Resources.Document_Blank;
			this.создатьToolStripMenuItem1.Name = "создатьToolStripMenuItem1";
			this.создатьToolStripMenuItem1.ShortcutKeys = (Keys)131150;
			this.создатьToolStripMenuItem1.Size = new Size(254, 24);
			this.создатьToolStripMenuItem1.Text = "&Создать";
			this.создатьToolStripMenuItem1.Click += new EventHandler(this.создатьToolStripMenuItem_Click);
			this.открытьToolStripMenuItem1.Image = Resources.Folder_Yellow_Downloads;
			this.открытьToolStripMenuItem1.Name = "открытьToolStripMenuItem1";
			this.открытьToolStripMenuItem1.ShortcutKeys = (Keys)131151;
			this.открытьToolStripMenuItem1.Size = new Size(254, 24);
			this.открытьToolStripMenuItem1.Text = "&Открыть...";
			this.открытьToolStripMenuItem1.Click += new EventHandler(this.открытьToolStripMenuItem_Click);
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new Size(251, 6);
			this.закрытьToolStripMenuItem1.Name = "закрытьToolStripMenuItem1";
			this.закрытьToolStripMenuItem1.ShortcutKeys = (Keys)131187;
			this.закрытьToolStripMenuItem1.Size = new Size(254, 24);
			this.закрытьToolStripMenuItem1.Text = "&Закрыть";
			this.закрытьToolStripMenuItem1.Click += new EventHandler(this.закрытьToolStripMenuItem_Click);
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new Size(251, 6);
			this.сохранитьToolStripMenuItem1.Image = Resources.Save;
			this.сохранитьToolStripMenuItem1.Name = "сохранитьToolStripMenuItem1";
			this.сохранитьToolStripMenuItem1.ShortcutKeys = (Keys)131155;
			this.сохранитьToolStripMenuItem1.Size = new Size(254, 24);
			this.сохранитьToolStripMenuItem1.Text = "&Сохранить";
			this.сохранитьToolStripMenuItem1.Click += new EventHandler(this.сохранитьToolStripMenuItem_Click);
			this.сохранитьКакToolStripMenuItem1.Image = Resources.FSaveAll;
			this.сохранитьКакToolStripMenuItem1.ImageTransparentColor = Color.Red;
			this.сохранитьКакToolStripMenuItem1.Name = "сохранитьКакToolStripMenuItem1";
			this.сохранитьКакToolStripMenuItem1.ShortcutKeys = (Keys)393299;
			this.сохранитьКакToolStripMenuItem1.Size = new Size(254, 24);
			this.сохранитьКакToolStripMenuItem1.Text = "&Сохранить как...";
			this.сохранитьКакToolStripMenuItem1.Click += new EventHandler(this.сохранитьКакToolStripMenuItem_Click);
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			this.toolStripMenuItem10.Size = new Size(251, 6);
			this.выходToolStripMenuItem1.Image = Resources.Back_Previous;
			this.выходToolStripMenuItem1.Name = "выходToolStripMenuItem1";
			this.выходToolStripMenuItem1.ShortcutKeys = (Keys)262259;
			this.выходToolStripMenuItem1.Size = new Size(254, 24);
			this.выходToolStripMenuItem1.Text = "&Выход";
			this.выходToolStripMenuItem1.Click += new EventHandler(this.выходToolStripMenuItem_Click);
			this.консультацияToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.стартToolStripMenuItem,
				this.toolStripMenuItem11,
				this.валидацияМоделиToolStripMenuItem
			});
			this.консультацияToolStripMenuItem1.Name = "консультацияToolStripMenuItem1";
			this.консультацияToolStripMenuItem1.Size = new Size(108, 23);
			this.консультацияToolStripMenuItem1.Text = "&Консультация";
			this.стартToolStripMenuItem.Image = Resources._3;
			this.стартToolStripMenuItem.Name = "стартToolStripMenuItem";
			this.стартToolStripMenuItem.ShortcutKeys = Keys.F5;
			this.стартToolStripMenuItem.Size = new Size(244, 24);
			this.стартToolStripMenuItem.Text = "&Начать консультацию";
			this.стартToolStripMenuItem.Click += new EventHandler(this.начатьКонсультациюToolStripMenuItem_Click);
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			this.toolStripMenuItem11.Size = new Size(241, 6);
			this.валидацияМоделиToolStripMenuItem.Image = Resources.Spell;
			this.валидацияМоделиToolStripMenuItem.Name = "валидацияМоделиToolStripMenuItem";
			this.валидацияМоделиToolStripMenuItem.ShortcutKeys = Keys.F9;
			this.валидацияМоделиToolStripMenuItem.Size = new Size(244, 24);
			this.валидацияМоделиToolStripMenuItem.Text = "&Проверить на ошибки";
			this.валидацияМоделиToolStripMenuItem.Click += new EventHandler(this.проверитьНаОшибкиToolStripMenuItem_Click);
			this.настройкиToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.каскадноеУдалениеToolStripMenuItem1,
				this.компонентаОбъясненияToolStripMenuItem,
				this.toolStripMenuItem7,
				this.установкиПоУмолчаниюToolStripMenuItem
			});
			this.настройкиToolStripMenuItem1.Name = "настройкиToolStripMenuItem1";
			this.настройкиToolStripMenuItem1.Size = new Size(89, 23);
			this.настройкиToolStripMenuItem1.Text = "Настройки";
			this.каскадноеУдалениеToolStripMenuItem1.Checked = true;
			this.каскадноеУдалениеToolStripMenuItem1.CheckOnClick = true;
			this.каскадноеУдалениеToolStripMenuItem1.CheckState = CheckState.Checked;
			this.каскадноеУдалениеToolStripMenuItem1.Name = "каскадноеУдалениеToolStripMenuItem1";
			this.каскадноеУдалениеToolStripMenuItem1.Size = new Size(286, 24);
			this.каскадноеУдалениеToolStripMenuItem1.Text = "&Каскадное удаление";
			this.компонентаОбъясненияToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.автоколлапсДеревьевВыводаToolStripMenuItem,
				this.полностьюРаскрыватьПоследнююКонсультациюToolStripMenuItem
			});
			this.компонентаОбъясненияToolStripMenuItem.Name = "компонентаОбъясненияToolStripMenuItem";
			this.компонентаОбъясненияToolStripMenuItem.Size = new Size(286, 24);
			this.компонентаОбъясненияToolStripMenuItem.Text = "Компонента &Объяснения";
			this.автоколлапсДеревьевВыводаToolStripMenuItem.Checked = true;
			this.автоколлапсДеревьевВыводаToolStripMenuItem.CheckOnClick = true;
			this.автоколлапсДеревьевВыводаToolStripMenuItem.CheckState = CheckState.Checked;
			this.автоколлапсДеревьевВыводаToolStripMenuItem.Name = "автоколлапсДеревьевВыводаToolStripMenuItem";
			this.автоколлапсДеревьевВыводаToolStripMenuItem.Size = new Size(398, 24);
			this.автоколлапсДеревьевВыводаToolStripMenuItem.Text = "&Автоколлапс деревьев вывода";
			this.полностьюРаскрыватьПоследнююКонсультациюToolStripMenuItem.CheckOnClick = true;
			this.полностьюРаскрыватьПоследнююКонсультациюToolStripMenuItem.Name = "полностьюРаскрыватьПоследнююКонсультациюToolStripMenuItem";
			this.полностьюРаскрыватьПоследнююКонсультациюToolStripMenuItem.Size = new Size(398, 24);
			this.полностьюРаскрыватьПоследнююКонсультациюToolStripMenuItem.Text = "&Полностью раскрывать последнюю консультацию";
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new Size(283, 6);
			this.установкиПоУмолчаниюToolStripMenuItem.Image = Resources.Favourites;
			this.установкиПоУмолчаниюToolStripMenuItem.Name = "установкиПоУмолчаниюToolStripMenuItem";
			this.установкиПоУмолчаниюToolStripMenuItem.ShortcutKeys = (Keys)131154;
			this.установкиПоУмолчаниюToolStripMenuItem.Size = new Size(286, 24);
			this.установкиПоУмолчаниюToolStripMenuItem.Text = "&Восстановить умолчания";
			this.установкиПоУмолчаниюToolStripMenuItem.Click += new EventHandler(this.восстановитьУмолчанияToolStripMenuItem_Click);
			this.справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.оПрограммеToolStripMenuItem1
			});
			this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
			this.справкаToolStripMenuItem.Size = new Size(74, 23);
			this.справкаToolStripMenuItem.Text = "&Справка";
			this.оПрограммеToolStripMenuItem1.Image = Resources.HAbout;
			this.оПрограммеToolStripMenuItem1.Name = "оПрограммеToolStripMenuItem1";
			this.оПрограммеToolStripMenuItem1.Size = new Size(164, 24);
			this.оПрограммеToolStripMenuItem1.Text = "&О программе";
			this.оПрограммеToolStripMenuItem1.Click += new EventHandler(this.оПрограммеToolStripMenuItem1_Click);
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.базаЗнанийToolStripMenuItem1,
				this.консультацияToolStripMenuItem1,
				this.настройкиToolStripMenuItem1,
				this.справкаToolStripMenuItem
			});
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(992, 27);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
			this.toolTips.AutomaticDelay = 100;
			this.toolTips.AutoPopDelay = 2000;
			this.toolTips.InitialDelay = 100;
			this.toolTips.ReshowDelay = 20;
			this.toolTips.ToolTipIcon = ToolTipIcon.Info;
			this.toolTips.ToolTipTitle = "<подсказка>";
			this.bindVars.CurrentItemChanged += new EventHandler(this.Binding_ItemChanged);
			this.bindVars.PositionChanged += new EventHandler(this.bindVars_PositionChanged);
			this.bindRules.CurrentItemChanged += new EventHandler(this.Binding_ItemChanged);
			this.bindRules.PositionChanged += new EventHandler(this.bindRules_PositionChanged);
			this.bindPrems.CurrentItemChanged += new EventHandler(this.Binding_ItemChanged);
			this.bindCons.CurrentItemChanged += new EventHandler(this.Binding_ItemChanged);
			this.bindPremValues.CurrentChanged += new EventHandler(this.bindPremConsValues_CurrentChanged);
			this.bindPremValues.CurrentItemChanged += new EventHandler(this.Binding_ItemChanged);
			this.bindConsValues.CurrentChanged += new EventHandler(this.bindPremConsValues_CurrentChanged);
			this.bindConsValues.CurrentItemChanged += new EventHandler(this.Binding_ItemChanged);
			this.экспортБЗToolStripMenuItem.Name = "экспортБЗToolStripMenuItem";
			this.экспортБЗToolStripMenuItem.Size = new Size(254, 24);
			this.экспортБЗToolStripMenuItem.Text = "Экспорт БЗ...";
			this.экспортБЗToolStripMenuItem.Click += new EventHandler(this.экспортБЗToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(251, 6);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(992, 663);
			base.Controls.Add(this.splitContainer2);
			base.Controls.Add(this.StatusBar);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "MainForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Оболочка ЭС";
			base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
			base.Load += new EventHandler(this.MainForm_Load);
			this.StatusBar.ResumeLayout(false);
			this.StatusBar.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.TabCtrl.ResumeLayout(false);
			this.VarPage.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.grdVars).EndInit();
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.grdDomainValues).EndInit();
			((ISupportInitialize)this.bindDomainValue).EndInit();
			((ISupportInitialize)this.grdDomainNames).EndInit();
			((ISupportInitialize)this.bindDomainName).EndInit();
			this.RulePage.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.grdRules).EndInit();
			this.groupBox5.ResumeLayout(false);
			((ISupportInitialize)this.grdCons).EndInit();
			this.groupBox4.ResumeLayout(false);
			((ISupportInitialize)this.grdPrems).EndInit();
			this.SettingPage.ResumeLayout(false);
			this.SettingPage.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			((ISupportInitialize)this.nUpDown).EndInit();
			this.OutputTab.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.tabTreeOut.ResumeLayout(false);
			this.tabPageFull.ResumeLayout(false);
			this.contextDelete.ResumeLayout(false);
			this.tabPageRules.ResumeLayout(false);
			this.tabPageVars.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((ISupportInitialize)this.bindVars).EndInit();
			((ISupportInitialize)this.bindRules).EndInit();
			((ISupportInitialize)this.bindPrems).EndInit();
			((ISupportInitialize)this.bindCons).EndInit();
			((ISupportInitialize)this.bindPremValues).EndInit();
			((ISupportInitialize)this.bindConsValues).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
