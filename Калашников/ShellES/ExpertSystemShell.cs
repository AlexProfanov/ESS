using ShellES.Components;
using ShellES.Entities;
using ShellES.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ShellES
{
	public class ExpertSystemShell
	{
		private ExplanationComponent KO;

		private LogicalOutputEngine MLV;

		private ReceiveKnowleageComponent KPZ;

		private ESDataBase DB;

		private WorkRamComponent RAM;

		private bool isConsulting;

		public MainForm ownerForm;

		private ConsultForm consForm;

		private AutoResetEvent eventSetVar;

		private AutoResetEvent eventInterrupt;

		private Thread threadConsult;

		public ExplanationComponent ComExplain
		{
			get
			{
				return this.KO;
			}
		}

		public LogicalOutputEngine ComMLV
		{
			get
			{
				return this.MLV;
			}
		}

		public ReceiveKnowleageComponent ComKPZ
		{
			get
			{
				return this.KPZ;
			}
		}

		public ESDataBase ESDB
		{
			get
			{
				return this.DB;
			}
		}

		public WorkRamComponent WorkRAM
		{
			get
			{
				return this.RAM;
			}
		}

		public List<ESDomains> Domains
		{
			get
			{
				return this.DB.Domains;
			}
		}

		public List<ESVars> Vars
		{
			get
			{
				return this.DB.Vars;
			}
		}

		public List<ESRules> Rules
		{
			get
			{
				return this.DB.Rules;
			}
		}

		public ShellES.Components.Settings Settings
		{
			get
			{
				return this.DB.Setting;
			}
		}

		public bool IsConsult
		{
			get
			{
				return this.isConsulting;
			}
			set
			{
				this.isConsulting = value;
			}
		}

		public bool IsChanged
		{
			get
			{
				return this.ESDB.Changed;
			}
			set
			{
				this.ESDB.Changed = value;
			}
		}

		public ExpertSystemShell(MainForm owner)
		{
			this.InitializeShell();
			this.DB = new ESDataBase(this);
			this.ownerForm = owner;
		}

		public ExpertSystemShell(MainForm owner, ESDataBase esDB)
		{
			this.InitializeShell();
			this.DB = esDB;
			this.DB.SetOwnerES(this);
			this.ownerForm = owner;
		}

		private void InitializeShell()
		{
			this.KO = new ExplanationComponent(this);
			this.MLV = new LogicalOutputEngine(this);
			this.KPZ = new ReceiveKnowleageComponent(this);
			this.RAM = new WorkRamComponent(this);
			this.consForm = this.KPZ.NewConsultForm();
			this.eventSetVar = new AutoResetEvent(false);
			this.eventInterrupt = new AutoResetEvent(false);
		}

		public DialogResult SaveDataBase(bool newFileNameRequired)
		{
			if (!newFileNameRequired && File.Exists(this.ESDB.BasePath))
			{
				this.IsChanged = !this.SaveESDBToFile(this.ESDB.BasePath);
			}
			else
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				if (this.ESDB.BasePath != "")
				{
					saveFileDialog.InitialDirectory = this.ESDB.BasePath;
				}
				else if (Directory.Exists(Application.StartupPath + "\\ESBases"))
				{
					saveFileDialog.InitialDirectory = Application.StartupPath + "\\ESBases";
				}
				else
				{
					saveFileDialog.InitialDirectory = Application.StartupPath;
				}
				saveFileDialog.Filter = ShellES.Properties.Settings.Default.FilePathFilter;
				if (saveFileDialog.ShowDialog(this.ownerForm) != DialogResult.OK)
				{
					return DialogResult.Cancel;
				}
				this.IsChanged = !this.SaveESDBToFile(saveFileDialog.FileName);
				this.ESDB.BasePath = saveFileDialog.FileName;
			}
			return DialogResult.Yes;
		}

		public DialogResult ExportKnowleages()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (this.ESDB.BasePath != "")
			{
				saveFileDialog.InitialDirectory = this.ESDB.BasePath;
			}
			else if (Directory.Exists(Application.StartupPath + "\\ESBases"))
			{
				saveFileDialog.InitialDirectory = Application.StartupPath + "\\ESBases";
			}
			else
			{
				saveFileDialog.InitialDirectory = Application.StartupPath;
			}
			saveFileDialog.Filter = ShellES.Properties.Settings.Default.ExportPathFilter;
			if (saveFileDialog.ShowDialog(this.ownerForm) == DialogResult.OK)
			{
				this.ExportKnowleagesToFile(saveFileDialog.FileName);
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}

		private void ExportKnowleagesToFile(string filePath)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Используемые домены:");
			foreach (ESDomains current in this.Domains)
			{
				string text = "";
				foreach (ESDomainValue current2 in current.Elements)
				{
					text += ((text != "") ? ", " : "");
					text += current2.Value;
				}
				stringBuilder.AppendFormat("{0}: [{1}]\n", current.Name, text).AppendLine();
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Используемые переменные:");
			foreach (ESVars current3 in this.Vars)
			{
				stringBuilder.AppendFormat("{0}: [{1}] - {2}", current3.Name, current3.Domain.Name, current3.Description).AppendLine();
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Продукционные правила:");
			foreach (ESRules current4 in this.Rules)
			{
				stringBuilder.AppendFormat("{0}: {1}    =>    {2}", current4.Name, current4.Presentation, current4.Reason).AppendLine();
			}
			File.WriteAllText(filePath, stringBuilder.ToString());
		}

		public DialogResult CheckForSave()
		{
			DialogResult dialogResult = DialogResult.No;
			if (this.IsChanged)
			{
				dialogResult = MessageBox.Show("Вы хотите сохранить изменения в базе знаний?", "Сохранение результатов", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (dialogResult == DialogResult.Yes)
				{
					dialogResult = this.SaveDataBase(false);
				}
			}
			return dialogResult;
		}

		public bool LoadESDBFromFile(string Path)
		{
			bool result;
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				using (FileStream fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
				{
					this.DB = (ESDataBase)binaryFormatter.Deserialize(fileStream);
				}
				this.DB.SetOwnerES(this);
				MessageBox.Show("База успешно загружена.", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				result = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при открытии базы знаний\nДополнительно: " + ex.Message, "Десериализация");
				result = false;
			}
			return result;
		}

		public bool SaveESDBToFile(string Path)
		{
			bool result;
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				using (FileStream fileStream = new FileStream(Path, FileMode.Create))
				{
					binaryFormatter.Serialize(fileStream, this.DB);
				}
				MessageBox.Show("База успешно сохранена.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				result = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при сохранении базы знаний\nДополнительно: " + ex.Message, "Сериализация");
				result = false;
			}
			return result;
		}

		public void StartConsult()
		{
			this.DB.ClearVariableValues();
			this.KO.ClearEvents();
			RichTextBox expr_21 = this.ownerForm.TxtBoxLog;
			expr_21.Text += ((this.ownerForm.TxtBoxLog.Lines.Length > 2) ? "\n\n" : "");
			RichTextBox expr_5D = this.ownerForm.TxtBoxLog;
			expr_5D.Text = expr_5D.Text + DateTime.Now.ToString() + " - старт консультации.";
			if (this.DB.ValidateESDB(this.ownerForm.TxtBoxLog, true) != DialogResult.OK)
			{
				this.ownerForm.TabCtrl.SelectTab(this.ownerForm.SettingPage);
				this.ownerForm.TxtBoxLog.Focus();
				this.StopConsult();
				return;
			}
			this.isConsulting = true;
			this.eventSetVar.Reset();
			this.eventInterrupt.Reset();
			this.threadConsult = new Thread(new ThreadStart(this.ConsultProcess));
			this.threadConsult.Name = "MLV Thread";
			this.threadConsult.Start();
			StaticHelper.AddSpecLine(this.ownerForm.TxtBoxLog, "#----- Запуск потока логического вывода -----#", 0);
		}

		private void ConsultProcess()
		{
			this.MLV.Consult();
			this.FinishCinsult(true, false);
		}

		public void InterruptConsult()
		{
			this.eventInterrupt.Set();
		}

		public void FinishCinsult(bool toShowResult, bool isInterrupted)
		{
			if (isInterrupted)
			{
				this.KO.ProcessEvent(eTypeEvent.END_CONSULTATION_BREAK, "");
				StaticHelper.AddSpecLine(this.ownerForm.TxtBoxLog, "Консультация прервана пользователем", 0);
				MessageBox.Show("Консультация прервана пользователем", "Отмена консультации");
			}
			else
			{
				StaticHelper.AddSpecLine(this.ownerForm.TxtBoxLog, "Вывод завершен! Значение(я) переменной " + this.Settings.Goal.Name + ": " + this.Settings.Goal.GetResults(), 0);
				if (toShowResult)
				{
					this.ShowResult(this.Settings.Goal);
				}
			}
			this.KO.ExpandConsultation(this.ownerForm.CollapsAll, this.ownerForm.ExpandLast);
			this.StopConsult();
		}

		public void StopConsult()
		{
			this.isConsulting = false;
			StaticHelper.SetTextDlg(this.ownerForm.btnStartConsult, "Начать консультацию");
			StaticHelper.SetTextDlg(this.ownerForm.menuStrip1, this.ownerForm.стартToolStripMenuItem, "Старт");
			try
			{
				if (this.threadConsult != null)
				{
					this.threadConsult.Abort();
				}
			}
			catch
			{
			}
		}

		public void PauseMLV()
		{
			int num = WaitHandle.WaitAny(new WaitHandle[]
			{
				this.eventSetVar,
				this.eventInterrupt
			}, -1);
			if (num == 1)
			{
				this.FinishCinsult(false, true);
			}
		}

		public void ResumeMLV()
		{
			this.eventSetVar.Set();
		}

		public void ShowResult(ESVars variable)
		{
			ResultForm resultForm = new ResultForm();
			if (variable.IsDefine(this.Settings.EDGE_UNKNOWN))
			{
				resultForm.DisplaySmth("Поздравляем! Цель установлена!", "Значение(я): " + variable.GetResults());
			}
			else
			{
				resultForm.DisplayNothing();
			}
			if (resultForm.ShowDialog() == DialogResult.OK)
			{
				this.ownerForm.Invoke(new dlgESSConsult(this.ownerForm.FocusOutputTree));
			}
		}
	}
}
