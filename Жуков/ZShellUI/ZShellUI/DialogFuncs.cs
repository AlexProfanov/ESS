using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZShellCore;

namespace ZShellUI
{
	public static class DialogFuncs
	{
		public static void doDragBeginning(ListView listView)
		{
			if (listView.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = listView.SelectedItems[0];
				listView.DoDragDrop(listViewItem, (System.Windows.Forms.DragDropEffects)2);
			}
		}

		public static void doDragOver(ListView listView, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				e.Effect = 0;
			}
			else
			{
				listView.SelectedItems.Clear();
				e.Effect = (System.Windows.Forms.DragDropEffects)2;
				Point point = new Point(e.X, e.Y);
				Point point2 = listView.PointToClient(point);
				ListViewItem itemAt = listView.GetItemAt(point2.X, point2.Y);
				if (itemAt != null)
				{
					listView.Items[itemAt.Index].Selected = true;
				}
			}
		}

		public static void selectListViewItem(ListView listView, int indexToSelect)
		{
			if (0 <= indexToSelect && indexToSelect < listView.Items.Count)
			{
				listView.SelectedIndices.Clear();
				listView.Items[indexToSelect].Selected = true;
				listView.Items[indexToSelect].EnsureVisible();
			}
		}

		public static bool needToSaveChanges(FormClosingMethod formClosingMethod)
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
			}
			return result;
		}

		public static void showAllVariables(ComboBox comboBox)
		{
			comboBox.DropDownStyle = (System.Windows.Forms.ComboBoxStyle)2;
			comboBox.Items.Clear();
			IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			while (enumeratorForVariables.MoveNext())
			{
				comboBox.Items.Add(enumeratorForVariables.Current.ToString());
			}
			comboBox.SelectedIndex = 0;
		}

		public static void showDeducableVariables(ComboBox comboBox)
		{
			comboBox.DropDownStyle = (System.Windows.Forms.ComboBoxStyle)2;
			comboBox.Items.Clear();
			IEnumerator<Variable> enumeratorForVariables = Global.knowledgeBase.getEnumeratorForVariables();
			while (enumeratorForVariables.MoveNext())
			{
				if (enumeratorForVariables.Current is DeducibleVariable || enumeratorForVariables.Current is DeducibleAskedVariable)
				{
					comboBox.Items.Add(enumeratorForVariables.Current.ToString());
				}
			}
			comboBox.SelectedIndex = 0;
		}

		public static void showAllVariables(ComboBox comboBox, Variable variable)
		{
			comboBox.DropDownStyle = (System.Windows.Forms.ComboBoxStyle)2;
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
					if (enumeratorForVariables.Current.Type == variable.Type)
					{
						comboBox.Items.Add(enumeratorForVariables.Current.ToString());
					}
				}
			}
			comboBox.SelectedIndex = 0;
		}

		public static void showDomainForSelectedVariable(ComboBox comboBox, Variable variable)
		{
			comboBox.Items.Clear();
			if (variable.Domain != null)
			{
				comboBox.DropDownStyle = (System.Windows.Forms.ComboBoxStyle)2;
				IEnumerator<Value> enumeratorForValues = variable.Domain.getEnumeratorForValues();
				while (enumeratorForValues.MoveNext())
				{
					comboBox.Items.Add(enumeratorForValues.Current.ToString());
				}
				if (comboBox.Items.Count != 0)
				{
					comboBox.SelectedIndex = 0;
				}
			}
			else
			{
				comboBox.DropDownStyle = (System.Windows.Forms.ComboBoxStyle)1;
				comboBox.Text = "";
			}
		}

		public static void setVariableToSelect(Variable variable, ComboBox comboBox)
		{
			for (int i = 0; i < comboBox.Items.Count; i++)
			{
				if (comboBox.Items[i].ToString() == variable.ToString())
				{
					comboBox.SelectedIndex = i;
					break;
				}
			}
		}

		public static Variable getSelectedVariable(ComboBox comboBox)
		{
			if (comboBox.SelectedIndex == -1)
			{
				throw new InvalidOperationException();
			}
			return Global.knowledgeBase.getVariable(comboBox.Text);
		}

		public static void setValueToSelect(Value value, ComboBox comboBox)
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

		public static Value getValueForVariable(Type variableType, ComboBox comboBox)
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
				if (!(variableType == typeof(double)))
				{
					throw new InvalidOperationException();
				}
				result = new Value(typeof(double), double.Parse(text));
			}
			return result;
		}

		public static ConditionComparer getSelectedComparer(ComboBox comboBox)
		{
			switch (comboBox.SelectedIndex)
			{
				case -1:
					throw new InvalidOperationException();
				case 0:
					return new ConditionEqualComparer();
				default:
					throw new InvalidOperationException();
			}
		}

		public static void setComparerToSelect(ConditionComparer comparer, ComboBox comboBox)
		{
			if (comparer is ConditionEqualComparer)
			{
				comboBox.SelectedIndex = 0;
				return;
			}
			throw new InvalidOperationException();
		}
	}
}
