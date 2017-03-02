using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShellES.VisualComponents
{
	public class DataGridViewRowsReorderBehavior
	{
		private int rowIndex = -1;

		private BindingSource _bind;

		private DataGridView _grid;

		private Color FillSpecialRowColor = Color.Plum;

		private MainForm ownerForm;

		private dlgKeyUp EventHandlerKeyAdd;

		private dlgKeyUp EventHandlerkeyDel;

		public DataGridViewRowsReorderBehavior(DataGridView grid, BindingSource bind, MainForm mainForm, dlgKeyUp kAdd, dlgKeyUp kDelete)
		{
			grid.AllowDrop = true;
			grid.AllowUserToResizeRows = false;
			grid.MouseMove += new MouseEventHandler(this.Grid_MouseMove);
			grid.MouseDown += new MouseEventHandler(this.Grid_MouseDown);
			grid.DragOver += new DragEventHandler(this.Grid_DragOver);
			grid.DragDrop += new DragEventHandler(this.Grid_DragDrop);
			grid.DragLeave += new EventHandler(this.Grid_DragLeave);
			grid.KeyUp += new KeyEventHandler(this.grid_KeyUp);
			grid.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.grid_CellBeginEdit);
			grid.CellEndEdit += new DataGridViewCellEventHandler(this.grid_CellEndEdit);
			this._grid = grid;
			this._bind = bind;
			this.ownerForm = mainForm;
			this.EventHandlerKeyAdd = kAdd;
			this.EventHandlerkeyDel = kDelete;
		}

		private void grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			this.ownerForm.SetStatusBar("Для завершения редактирования нажмите клавишу <Enter>...", true, Color.Brown);
		}

		private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			this.ownerForm.SetStatusBar("", true, Color.Black);
		}

		private void Grid_DragLeave(object sender, EventArgs e)
		{
			if (this.rowIndex != -1)
			{
				this.SetRowSpecialStyle(-1, this.FillSpecialRowColor);
			}
		}

		private void grid_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Insert)
			{
				this.EventHandlerKeyAdd(sender, e);
			}
			if (e.KeyCode == Keys.Delete)
			{
				this.EventHandlerkeyDel(sender, e);
			}
		}

		public void CheckAvailableNavigation(int index, bool CloseAll)
		{
			if (!CloseAll)
			{
				this.ownerForm.btnMoveFirst.Enabled = (index > 0);
				this.ownerForm.btnMoveLast.Enabled = (index < this._bind.Count - 1);
				this.ownerForm.btnMoveNext.Enabled = this.ownerForm.btnMoveLast.Enabled;
				this.ownerForm.btnMovePrev.Enabled = this.ownerForm.btnMoveFirst.Enabled;
				return;
			}
			this.ownerForm.btnMoveFirst.Enabled = false;
			this.ownerForm.btnMoveLast.Enabled = false;
			this.ownerForm.btnMoveNext.Enabled = false;
			this.ownerForm.btnMovePrev.Enabled = false;
		}

		public bool MoveCurrentObjectInBinding(BindingSource bind, int newPos)
		{
			if (newPos < 0 || newPos == bind.Position || newPos >= bind.Count)
			{
				return false;
			}
			object current = bind.Current;
			int arg_25_0 = bind.Position;
			bind.RemoveCurrent();
			bind.Insert(newPos, current);
			bind.Position = newPos;
			this.CheckAvailableNavigation(newPos, false);
			return true;
		}

		private void Grid_DragDrop(object sender, DragEventArgs e)
		{
			if (this.rowIndex != -1)
			{
				Point localPoint = this.GetLocalPoint(e.X, e.Y);
				DataGridView.HitTestInfo hitTestInfo = this._grid.HitTest(localPoint.X, localPoint.Y);
				int num = (hitTestInfo.RowIndex < this._bind.Count) ? hitTestInfo.RowIndex : (this._bind.Count - 1);
				if (num >= 0 && num != this._bind.Position && e.Effect == DragDropEffects.Move)
				{
					this.MoveCurrentObjectInBinding(this._bind, num);
				}
				this.SetRowSpecialStyle(-1, this.FillSpecialRowColor);
			}
		}

		private void Grid_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetData(typeof(DataGridView)) != this._grid)
			{
				return;
			}
			e.Effect = DragDropEffects.Move;
			Point localPoint = this.GetLocalPoint(e.X, e.Y);
			DataGridView.HitTestInfo hitTestInfo = this._grid.HitTest(localPoint.X, localPoint.Y);
			if (hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader)
			{
				this._grid.FirstDisplayedScrollingRowIndex--;
			}
			int num = hitTestInfo.RowIndex;
			if (num == this._grid.FirstDisplayedScrollingRowIndex + this._grid.DisplayedRowCount(false) - 1)
			{
				this._grid.FirstDisplayedScrollingRowIndex++;
			}
			this.SetRowSpecialStyle(num, this.FillSpecialRowColor);
		}

		private void Grid_MouseDown(object sender, MouseEventArgs e)
		{
			this.rowIndex = -1;
			DataGridView.HitTestInfo hitTestInfo = this._grid.HitTest(e.X, e.Y);
			if (e.Button == MouseButtons.Left && hitTestInfo.ColumnIndex == -1 && hitTestInfo.RowIndex >= 0 && hitTestInfo.RowIndex < this._bind.Count)
			{
				this.rowIndex = hitTestInfo.RowIndex;
			}
		}

		private void Grid_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.rowIndex != -1 && (e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				Rectangle rowDisplayRectangle = this._grid.GetRowDisplayRectangle(this.rowIndex, true);
				if (rowDisplayRectangle != Rectangle.Empty && !rowDisplayRectangle.Contains(e.X, e.Y))
				{
					this._grid.DoDragDrop(this._grid, DragDropEffects.Move);
				}
			}
		}

		private Point GetLocalPoint(int X, int Y)
		{
			return this._grid.PointToClient(new Point(X, Y));
		}

		private void SetRowSpecialStyle(int rowIndex, Color Col)
		{
			for (int i = 0; i < this._grid.Rows.Count; i++)
			{
				this._grid.Rows[i].DefaultCellStyle.BackColor = ((i == rowIndex) ? Col : this._grid.DefaultCellStyle.BackColor);
			}
		}
	}
}
