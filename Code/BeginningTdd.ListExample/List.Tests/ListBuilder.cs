using List.Production;

namespace List.Tests
{
	public class ListBuilder<T>
	{
		private T[] _items;

		public ListBuilder<T> WithItems(params T[] items)
		{
			_items = items;
			return this;
		}

		public List<T> Build()
		{
			var list = new List<T>();
			foreach (var item in _items)
			{
				list.Add(item);
			}

			return list;
		}
	}
}
