using System;

namespace MyEs
{
	internal class AddedInfomation
	{
		public int id;

		public bool dirty;

		public AddedInfomation(int _id, bool _dirty)
		{
			this.id = _id;
			this.dirty = _dirty;
		}
	}
}
