using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProductionShell
{
	internal static class DialogFuncs
	{
		internal static void doDragBeginning(ListView listView)
		{
			if (listView.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = listView.SelectedItems[0];
				listView.DoDragDrop(listViewItem, 3);
			}
		}

		internal static void doDragOver(ListView listView, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				e.Effect = 0;
				return;
			}
			listView.SelectedItems.Clear();
			if ((e.KeyState & 4) == 4 && (e.AllowedEffect & 1) == 1)
			{
				e.Effect = 1;
			}
			else
			{
				e.Effect = 2;
			}
			Point point = new Point(e.X, e.Y);
			Point point2 = listView.PointToClient(point);
			ListViewItem itemAt = listView.GetItemAt(point2.X, point2.Y);
			if (itemAt != null)
			{
				listView.Items[itemAt.Index].Selected = true;
			}
		}

		internal static void selectListViewItem(ListView listView, int indexToSelect)
		{
			if (0 <= indexToSelect && indexToSelect < listView.Items.Count)
			{
				listView.SelectedIndices.Clear();
				listView.Items[indexToSelect].Selected = true;
				listView.Items[indexToSelect].EnsureVisible();
			}
		}

		internal static string printType(Type type)
		{
			string result = type.ToString();
			if (type == typeof(int))
			{
				result = "Целый";
			}
			else if (type == typeof(string))
			{
				result = "Строковый";
			}
			else if (type == typeof(double))
			{
				result = "Вещественный";
			}
			else if (type == typeof(bool))
			{
				result = "Логический";
			}
			return result;
		}

		internal static bool needToSaveChanges(FormClosingMethod formClosingMethod)
		{
			bool result = false;
			switch (formClosingMethod)
			{
				case FormClosingMethod.OK:
					result = true;
					break;
				case FormClosingMethod.CANCEL:
					result = false;
					break;
				case FormClosingMethod.EXIT:
					if (MessageBox.Show("Нужно ли сохранять результаты?", "Внимание", 4, 32) == 6)
					{
						result = true;
					}
					break;
			}
			return result;
		}

		internal static void showAllVariables(ComboBox comboBox)
		{
			comboBox.DropDownStyle = 2;
			comboBox.Items.Clear();
			IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			while (enumeratorForVariables.MoveNext())
			{
				comboBox.Items.Add(enumeratorForVariables.Current.ToString());
			}
			comboBox.SelectedIndex = 0;
		}

		internal static void showAllVariables(ComboBox comboBox, Variable variable)
		{
			comboBox.DropDownStyle = 2;
			comboBox.Items.Clear();
			IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			if (variable.Domain != null)
			{
				while (enumeratorForVariables.MoveNext())
				{
					if (enumeratorForVariables.Current.Domain == variable.Domain)
					{
						comboBox.Items.Add(enumeratorForVariables.Current.ToString());
					}
				}
			}
			else
			{
				while (enumeratorForVariables.MoveNext())
				{
					if (enumeratorForVariables.Current.VarType == variable.VarType)
					{
						comboBox.Items.Add(enumeratorForVariables.Current.ToString());
					}
				}
			}
			comboBox.SelectedIndex = 0;
		}

		internal static void showDomainForSelectedVariable(ComboBox comboBox, Variable variable)
		{
			comboBox.Items.Clear();
			if (variable.Domain != null)
			{
				comboBox.DropDownStyle = 2;
				IEnumerator<Value> enumeratorForValues = variable.Domain.getEnumeratorForValues();
				while (enumeratorForValues.MoveNext())
				{
					comboBox.Items.Add(enumeratorForValues.Current.ToString());
				}
				if (comboBox.Items.Count != 0)
				{
					comboBox.SelectedIndex = 0;
					return;
				}
			}
			else
			{
				comboBox.DropDownStyle = 1;
				comboBox.Text = "";
			}
		}

		internal static void setVariableToSelect(Variable variable, ComboBox comboBox)
		{
			for (int i = 0; i < comboBox.Items.Count; i++)
			{
				if (comboBox.Items[i].ToString() == variable.ToString())
				{
					comboBox.SelectedIndex = i;
					return;
				}
			}
		}

		internal static Variable getSelectedVariable(ComboBox comboBox)
		{
			if (comboBox.SelectedIndex == -1)
			{
				throw new InvalidOperationException();
			}
			return Global.knowledgeBase.getVariable(comboBox.Text);
		}

		internal static void setValueToSelect(Value value, ComboBox comboBox)
		{
			for (int i = 0; i < comboBox.Items.Count; i++)
			{
				if (comboBox.Items[i].ToString() == value.ToString())
				{
					comboBox.SelectedIndex = i;
					return;
				}
			}
			comboBox.Text = value.ToString();
		}

		internal static Value getValueForVariable(Type variableType, ComboBox comboBox)
		{
			string text = comboBox.Text;
			Value result;
			if (variableType == typeof(int))
			{
				result = new Value(typeof(int), int.Parse(text));
			}
			else if (variableType == typeof(bool))
			{
				result = new Value(typeof(bool), bool.Parse(text));
			}
			else if (variableType == typeof(string))
			{
				result = new Value(typeof(string), text);
			}
			else
			{
				if (variableType != typeof(double))
				{
					throw new InvalidOperationException();
				}
				result = new Value(typeof(double), double.Parse(text));
			}
			return result;
		}

		internal static ConditionComparer getSelectedComparer(ComboBox comboBox)
		{
			int selectedIndex = comboBox.SelectedIndex;
			if (selectedIndex == -1)
			{
				throw new InvalidOperationException();
			}
			ConditionComparer result;
			switch (selectedIndex)
			{
				case 0:
					result = new ConditionEqualComparer();
					break;
				case 1:
					result = new ConditionNotEqualComparer();
					break;
				case 2:
					result = new ConditionAGreaterBComparer();
					break;
				case 3:
					result = new ConditionALaterBComparer();
					break;
				default:
					throw new InvalidOperationException();
			}
			return result;
		}

		internal static void setComparerToSelect(ConditionComparer comparer, ComboBox comboBox)
		{
			if (comparer is ConditionEqualComparer)
			{
				comboBox.SelectedIndex = 0;
				return;
			}
			if (comparer is ConditionNotEqualComparer)
			{
				comboBox.SelectedIndex = 1;
				return;
			}
			if (comparer is ConditionAGreaterBComparer)
			{
				comboBox.SelectedIndex = 2;
				return;
			}
			if (comparer is ConditionALaterBComparer)
			{
				comboBox.SelectedIndex = 3;
				return;
			}
			throw new InvalidOperationException();
		}
	}
}
