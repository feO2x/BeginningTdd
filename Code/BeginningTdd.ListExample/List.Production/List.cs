
namespace List.Production
{
	public class List<T>
	{
		private int _count;
		private T[] _internalArray;

		public List()
		{
			_internalArray = new T[8];
		}

		public int Count { get { return _count; } }

		public void Add(T item)
		{
			_internalArray[_count] = item;
			_count++;
		}

		public T this[int index]
		{
			get { return _internalArray[index];  }
		}
	}
}
