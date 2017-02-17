using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyEs
{
	public class MyDGV : DataGridView
	{
		private Rectangle DragDropRectangle;

		public int DragDropSourceIndex;

		public int DragDropTargetIndex;

		public bool mode;

		private int DragDropCurrentIndex = -1;

		private int DragDropType;

		private DataGridViewColumn DragDropColumn;

		private object[] DragDropColumnCellValue;

		private IContainer components = null;

		public MyDGV()
		{
			base.SelectionMode = 1;
			base.AllowUserToOrderColumns = false;
			base.AllowUserToAddRows = false;
			base.AllowUserToDeleteRows = false;
			base.AllowUserToResizeRows = false;
			base.MultiSelect = false;
			this.AllowDrop = true;
			base.ReadOnly = true;
			this.mode = true;
			this.DragDropType = 1;
		}

		protected override void OnRowHeaderMouseClick(DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex > -1)
			{
				if (e.Button == 1048576)
				{
					if (base.SelectionMode != 3)
					{
						base.Rows[e.RowIndex].Selected = true;
						base.CurrentCell = base[0, e.RowIndex];
					}
				}
				else if (e.Button == 2097152)
				{
				}
			}
			base.OnRowHeaderMouseClick(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.AllowDrop)
			{
				if (base.HitTest(e.X, e.Y).ColumnIndex == -1 && base.HitTest(e.X, e.Y).RowIndex > -1)
				{
					if (base.Rows[base.HitTest(e.X, e.Y).RowIndex].Selected)
					{
						this.DragDropType = 1;
						Size dragSize = SystemInformation.DragSize;
						this.DragDropRectangle = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
						this.DragDropSourceIndex = base.HitTest(e.X, e.Y).RowIndex;
					}
					else
					{
						this.DragDropRectangle = Rectangle.Empty;
					}
				}
				else if (base.HitTest(e.X, e.Y).ColumnIndex <= -1 || base.HitTest(e.X, e.Y).RowIndex != -1)
				{
					this.DragDropRectangle = Rectangle.Empty;
				}
			}
			else
			{
				this.DragDropRectangle = Rectangle.Empty;
			}
			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.AllowDrop)
			{
				if ((e.Button & 1048576) == 1048576)
				{
					if (this.DragDropRectangle != Rectangle.Empty && !this.DragDropRectangle.Contains(e.X, e.Y))
					{
						if (this.DragDropType != 0)
						{
							if (this.DragDropType == 1)
							{
								DragDropEffects dragDropEffects = base.DoDragDrop(base.Rows[this.DragDropSourceIndex], 2);
							}
						}
					}
				}
			}
			base.OnMouseMove(e);
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			if (this.AllowDrop)
			{
				e.Effect = 2;
				if (this.DragDropType != 0)
				{
					if (this.DragDropType == 1)
					{
						DataGridView.HitTestInfo hitTestInfo = base.HitTest(base.PointToClient(new Point(e.X, e.Y)).X, base.PointToClient(new Point(e.X, e.Y)).Y);
						int rowIndex = hitTestInfo.RowIndex;
						if (this.DragDropCurrentIndex != rowIndex)
						{
							this.DragDropCurrentIndex = rowIndex;
							base.Invalidate();
						}
					}
				}
			}
			base.OnDragOver(e);
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if (this.AllowDrop)
			{
				if (drgevent.Effect == 2)
				{
					Point point = base.PointToClient(new Point(drgevent.X, drgevent.Y));
					if (this.DragDropType != 0)
					{
						if (this.DragDropType == 1)
						{
							this.DragDropTargetIndex = base.HitTest(point.X, point.Y).RowIndex;
							if (this.DragDropTargetIndex > -1 && this.DragDropCurrentIndex <= base.RowCount - 1)
							{
								this.DragDropCurrentIndex = -1;
								if (this.mode)
								{
									DataGridViewRow dataGridViewRow = drgevent.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
									if (base.Rows.Contains(dataGridViewRow))
									{
										base.Rows.RemoveAt(this.DragDropSourceIndex);
										base.Rows.Insert(this.DragDropTargetIndex, dataGridViewRow);
										base.Rows[this.DragDropTargetIndex].Selected = true;
									}
									else
									{
										this.DragDropSourceIndex = -1;
									}
								}
							}
						}
					}
				}
			}
			base.OnDragDrop(drgevent);
		}

		protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
		{
			if (this.DragDropCurrentIndex > -1)
			{
				if (this.DragDropType != 0)
				{
					if (this.DragDropType == 1)
					{
						if (e.RowIndex == this.DragDropCurrentIndex && this.DragDropCurrentIndex <= base.RowCount - 1)
						{
							Pen pen = new Pen(Color.Red, 1f);
							e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Top - 1, e.CellBounds.Right, e.CellBounds.Top - 1);
						}
					}
				}
			}
			base.OnCellPainting(e);
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
			base.SuspendLayout();
			base.Name = "UserControl1";
			base.Size = new Size(209, 219);
			base.ResumeLayout(false);
		}
	}
}
