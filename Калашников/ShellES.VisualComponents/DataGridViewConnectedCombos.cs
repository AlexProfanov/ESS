using ShellES.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShellES.VisualComponents
{
	public class DataGridViewConnectedCombos
	{
		private BindingSource _slaveBind;

		private BindingSource _primaryBind;

		private DataGridView _grid;

		private MainForm ownerForm;

		private ESDomainValue tmpDomainValue;

		private ESDomains tmpDomain;

		public DataGridViewConnectedCombos(DataGridView grid, BindingSource primaryBind, BindingSource slaveBind, MainForm mainForm)
		{
			grid.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.grid_CellBeginEdit);
			grid.CellEndEdit += new DataGridViewCellEventHandler(this.grid_CellEndEdit);
			this._grid = grid;
			this._primaryBind = primaryBind;
			this._slaveBind = slaveBind;
			this.ownerForm = mainForm;
			this.tmpDomainValue = null;
			this._slaveBind.Add(new ESDomainValue(""));
			this._slaveBind.RemoveAt(0);
		}

		private List<ESDomainValue> GetDomainValues(ESFact f)
		{
			if (f == null)
			{
				return null;
			}
			return f.GetDomainValuesFromFact();
		}

		private void grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				DataGridViewComboBoxCell dataGridViewComboBoxCell = this._grid.CurrentCell as DataGridViewComboBoxCell;
				ESVars eSVars = dataGridViewComboBoxCell.Value as ESVars;
				if (eSVars == null)
				{
					this.tmpDomain = null;
				}
				else
				{
					this.tmpDomain = eSVars.Domain;
				}
			}
			if (e.ColumnIndex == 1)
			{
				DataGridViewComboBoxCell dataGridViewComboBoxCell2 = this._grid.CurrentCell as DataGridViewComboBoxCell;
				this.tmpDomainValue = (dataGridViewComboBoxCell2.Value as ESDomainValue);
				dataGridViewComboBoxCell2.Value = null;
				List<ESDomainValue> domainValues = this.GetDomainValues(this._primaryBind.Current as ESFact);
				dataGridViewComboBoxCell2.DataSource = domainValues;
				if (domainValues != null)
				{
					dataGridViewComboBoxCell2.ValueMember = "Self";
					dataGridViewComboBoxCell2.DisplayMember = "Value";
					return;
				}
				dataGridViewComboBoxCell2.ValueMember = "";
				dataGridViewComboBoxCell2.DisplayMember = "";
			}
		}

		private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				DataGridViewComboBoxCell dataGridViewComboBoxCell = this._grid.CurrentCell as DataGridViewComboBoxCell;
				ESVars eSVars = dataGridViewComboBoxCell.Value as ESVars;
				ESDomains eSDomains;
				if (eSVars == null)
				{
					eSDomains = null;
				}
				else
				{
					eSDomains = eSVars.Domain;
				}
				if (eSDomains == this.tmpDomain)
				{
					return;
				}
				DataGridViewComboBoxCell dataGridViewComboBoxCell2 = this._grid.Rows[e.RowIndex].Cells[1] as DataGridViewComboBoxCell;
				ESDomainValue eSDomainValue = dataGridViewComboBoxCell2.Value as ESDomainValue;
				dataGridViewComboBoxCell2.Value = null;
				if (StaticHelper.IsSingleOccuranceInList(this.ownerForm.GetCurrentList(this._grid), eSDomainValue))
				{
					this._slaveBind.Remove(eSDomainValue);
				}
			}
			if (e.ColumnIndex == 1)
			{
				DataGridViewComboBoxCell dataGridViewComboBoxCell3 = this._grid.CurrentCell as DataGridViewComboBoxCell;
				if (dataGridViewComboBoxCell3.Value == null)
				{
					List<ESDomainValue> domainValues = this.GetDomainValues(this._primaryBind.Current as ESFact);
					if (domainValues != null && domainValues.Contains(this.tmpDomainValue))
					{
						dataGridViewComboBoxCell3.Value = this.tmpDomainValue;
					}
				}
				else if (dataGridViewComboBoxCell3.Value != this.tmpDomainValue)
				{
					if (StaticHelper.IsSingleOccuranceInList(this.ownerForm.GetCurrentList(this._grid), this.tmpDomainValue))
					{
						this._slaveBind.Remove(this.tmpDomainValue);
					}
					if (dataGridViewComboBoxCell3.Value != null)
					{
						this._slaveBind.Add(dataGridViewComboBoxCell3.Value);
					}
				}
				dataGridViewComboBoxCell3.DataSource = this._slaveBind;
			}
		}
	}
}
