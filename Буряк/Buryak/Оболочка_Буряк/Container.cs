using System;
using System.Collections.Generic;

namespace Оболочка_Буряк
{
	[Serializable]
	public class Container<ItemClass> where ItemClass : NamedItem
	{
		private List<ItemClass> itemList = new List<ItemClass>();

		public ItemClass this[int itemIndex]
		{
			get
			{
				if (itemIndex < 0 || itemIndex > this.itemList.Count)
				{
					throw new ArgumentOutOfRangeException("itemIndex");
				}
				return this.itemList[itemIndex];
			}
		}

		public ItemClass this[string itemName]
		{
			get
			{
				if (itemName == null)
				{
					throw new ArgumentNullException("itemName");
				}
				using (List<ItemClass>.Enumerator enumerator = this.itemList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ItemClass current = enumerator.Current;
						if (current.Name == itemName)
						{
							return current;
						}
					}
				}
				throw new ArgumentException("Элемент с именем " + itemName + " ранее объявлен не был/");
			}
		}

		public ItemClass this[ItemClass item]
		{
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.itemList.Contains(item))
				{
					throw new ArgumentException("Элемента " + item.ToString() + " в котейнере нет");
				}
				this.itemList[this.itemList.IndexOf(item)] = value;
			}
		}

		public int Count
		{
			get
			{
				return this.itemList.Count;
			}
		}

		public void Add(ItemClass item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.Contains(item.Name))
			{
				throw new ArgumentException("Элемент с именем " + item.Name + " уже зарегестрирован");
			}
			this.itemList.Add(item);
		}

		public bool Contains(string itemName)
		{
			if (this.itemList == null)
			{
				throw new ArgumentNullException("itemName");
			}
			bool result;
			using (List<ItemClass>.Enumerator enumerator = this.itemList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ItemClass current = enumerator.Current;
					if (current.Name == itemName)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public bool Contains(ItemClass item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.itemList.Contains(item);
		}

		public IEnumerator<ItemClass> GetEnumerator()
		{
			return this.itemList.GetEnumerator();
		}

		public int IndexOf(ItemClass item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (!this.itemList.Contains(item))
			{
				throw new ArgumentException("В контейнере нет элемента " + item.ToString());
			}
			return this.itemList.IndexOf(item);
		}

		public void Insert(int oldItemIndex, int newItemIndex)
		{
			if (oldItemIndex < 0 || oldItemIndex > this.itemList.Count)
			{
				throw new ArgumentOutOfRangeException("oldItemIndex");
			}
			if (newItemIndex < 0 || newItemIndex > this.itemList.Count)
			{
				throw new ArgumentOutOfRangeException("newItemIndex");
			}
			ItemClass itemClass = this.itemList[oldItemIndex];
			this.itemList.Remove(itemClass);
			this.itemList.Insert(newItemIndex, itemClass);
		}

		public void Switch(int firstItemIndex, int secondItemIndex)
		{
			if (firstItemIndex < 0 || firstItemIndex > this.itemList.Count)
			{
				throw new ArgumentOutOfRangeException("firstItemIndex");
			}
			if (secondItemIndex < 0 || secondItemIndex > this.itemList.Count)
			{
				throw new ArgumentOutOfRangeException("secondItemIndex");
			}
			ItemClass itemClass = this.itemList[firstItemIndex];
			this.itemList[firstItemIndex] = this.itemList[secondItemIndex];
			this.itemList[secondItemIndex] = itemClass;
		}

		public void RemoveAt(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex > this.itemList.Count)
			{
				throw new ArgumentOutOfRangeException("itemIndex");
			}
			this.itemList.RemoveAt(itemIndex);
		}
	}
}
