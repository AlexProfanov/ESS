using ShellES.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ShellES
{
	public static class StaticHelper
	{
		private delegate void dlgRichBoxSelectAndSetColor(RichTextBox TxtBox, string str, Color LineColor, bool useColor);

		private delegate void dlgSettingTextMenu(ToolStripItem ctrl, string text);

		public const string PATH_TO_DATABASES = "\\ESBases";

		private static void InvokeAddSpecLineWithColor(RichTextBox TxtBox, string str, Color LineColor, bool useColor)
		{
			if (useColor)
			{
				TxtBox.SelectionColor = LineColor;
			}
			TxtBox.AppendText(str);
			if (useColor)
			{
				TxtBox.SelectionColor = Color.Black;
			}
		}

		private static string GetStringWithIndent(string str, int indent)
		{
			return "\n" + new string(' ', indent) + str;
		}

		public static void AddSpecLine(RichTextBox TxtBox, string S, int indent)
		{
			string stringWithIndent = StaticHelper.GetStringWithIndent(S, indent);
			if (TxtBox.InvokeRequired)
			{
				TxtBox.Invoke(new StaticHelper.dlgRichBoxSelectAndSetColor(StaticHelper.InvokeAddSpecLineWithColor), new object[]
				{
					TxtBox,
					stringWithIndent,
					Color.Black,
					false
				});
				return;
			}
			StaticHelper.InvokeAddSpecLineWithColor(TxtBox, stringWithIndent, Color.Black, false);
		}

		public static void AddSpecLine(RichTextBox TxtBox, string S, int indent, eExplainType exlp)
		{
			string stringWithIndent = StaticHelper.GetStringWithIndent(S, indent);
			Color color = Color.Blue;
			switch (exlp)
			{
			case eExplainType.Warning:
				color = Color.YellowGreen;
				break;
			case eExplainType.Error:
				color = Color.Red;
				break;
			}
			if (TxtBox.InvokeRequired)
			{
				TxtBox.Invoke(new StaticHelper.dlgRichBoxSelectAndSetColor(StaticHelper.InvokeAddSpecLineWithColor), new object[]
				{
					TxtBox,
					stringWithIndent,
					color,
					true
				});
				return;
			}
			StaticHelper.InvokeAddSpecLineWithColor(TxtBox, stringWithIndent, color, true);
		}

		public static void SlightVisibility(Form frm, double fromOpacity, double toOpacity)
		{
			double num = 0.1;
			int millisecondsTimeout = 20;
			frm.Opacity = fromOpacity;
			if (toOpacity < fromOpacity)
			{
				num *= -1.0;
			}
			int num2 = 0;
			while ((double)num2 < (toOpacity - fromOpacity) / num)
			{
				frm.Opacity += num;
				frm.Refresh();
				Thread.Sleep(millisecondsTimeout);
				num2++;
			}
			frm.Opacity = toOpacity;
			frm.Refresh();
		}

		public static void ShowForm(Form frm, Point location)
		{
			if (!frm.Visible)
			{
				frm.Opacity = 1.0;
				frm.ShowDialog();
			}
		}

		public static void HideForm(Form frm)
		{
			frm.Hide();
		}

		private static void SetText(Control ctrl, string text)
		{
			ctrl.Text = text;
		}

		private static void SetText(ToolStripItem ctrl, string text)
		{
			ctrl.Text = text;
		}

		public static void SetTextDlg(Control ctrl, string text)
		{
			if (ctrl.InvokeRequired)
			{
				ctrl.Invoke(new dlgSettingText(StaticHelper.SetText), new object[]
				{
					ctrl,
					text
				});
				return;
			}
			StaticHelper.SetText(ctrl, text);
		}

		public static void SetTextDlg(ToolStrip TS, ToolStripItem ctrl, string text)
		{
			if (TS.InvokeRequired)
			{
				TS.BeginInvoke(new StaticHelper.dlgSettingTextMenu(StaticHelper.SetText), new object[]
				{
					ctrl,
					text
				});
				return;
			}
			StaticHelper.SetText(ctrl, text);
		}

		public static void SetAllTrimToGrid(DataGridView dgv)
		{
			dgv.CellEndEdit += new DataGridViewCellEventHandler(StaticHelper.Grid_CellEndEdit);
		}

		private static void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = sender as DataGridView;
			if (dataGridView == null)
			{
				return;
			}
			DataGridViewCell dataGridViewCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
			if (dataGridViewCell.ValueType == typeof(string))
			{
				dataGridViewCell.Value = StaticHelper.AllTrim(dataGridViewCell.Value);
			}
		}

		private static string AllTrim(object str)
		{
			return Convert.ToString(str).Trim();
		}

		public static string GetСклонение(string str, int number)
		{
			int num = number % 10;
			string text = str + ((num == 1) ? "о" : "");
			text += ((num >= 2 && num <= 4) ? "а" : "");
			return number.ToString() + " " + text;
		}

		public static bool IsSingleOccuranceInList(List<ESFact> Arr, ESDomainValue O)
		{
			if (O == null || Arr == null)
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < Arr.Count; i++)
			{
				if (Arr[i].Value == O)
				{
					if (flag)
					{
						return false;
					}
					flag = true;
				}
			}
			return true;
		}

		public static KeyValuePair<string, int> GetMaxInStringDictionary(SortedDictionary<string, int> SD)
		{
			KeyValuePair<string, int> result = new KeyValuePair<string, int>("", 0);
			foreach (KeyValuePair<string, int> current in SD)
			{
				if (result.Key != "")
				{
					if (current.Value > result.Value)
					{
						result = current;
					}
				}
				else
				{
					result = current;
				}
			}
			return result;
		}

		public static void RunMethodAsDelegate(Control ctrl, Delegate dlg, params object[] args)
		{
			if (ctrl == null)
			{
				return;
			}
			try
			{
				if (ctrl.InvokeRequired)
				{
					ctrl.Invoke(dlg, args);
				}
				else
				{
					dlg.DynamicInvoke(args);
				}
			}
			catch
			{
				MessageBox.Show("Ошибка в Invoke-методе компонента " + ctrl.Name, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}
