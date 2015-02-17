using System;
using System.Collections;
using System.Collections.Generic;

namespace List.Production
{
	public class List<T> : IList<T>
	{
		private int _count;
		private T[] _internalArray;

		public List()
		{
			_internalArray = new T[8];
		}

		public List(int initialCapacity)
		{
			if (initialCapacity < 2)
				throw new ArgumentException("initialCapacity cannot be less than two", nameof(initialCapacity));

			_internalArray = new T[initialCapacity];
		}

		public int Count { get { return _count; } }

		public void Add(T item)
		{
			if (_count == Capacity)
				EnlargeArray();

			_internalArray[_count] = item;
			_count++;
		}

		private void EnlargeArray()
		{
			var newArray = new T[2 * Capacity];
			for (int i = 0; i < Capacity; i++)
			{
				newArray[i] = _internalArray[i];
			}
			_internalArray = newArray;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new ArrayEnumerator(_internalArray, _count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			for (int i = 0; i < _count; i++)
			{
				_internalArray[i] = default(T);
			}
			_count = 0;
		}

		public bool Contains(T item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public T this[int index]
		{
			get { return _internalArray[index];  }
			set
			{
				if (index > _count)
					throw new IndexOutOfRangeException(string.Format("index with value {0} must not be larger than Count ({0}).", index, _count));

				if (index == _count)
				{
					Add(value);
					return;
				}

				_internalArray[index] = value;
			}
		}

		public int Capacity
		{
			get { return _internalArray.Length; }
		}

		public bool IsReadOnly
		{
			get { return false; }
			
		}

		private class ArrayEnumerator : IEnumerator<T>
		{
			private int _count;
			private T[] _array;
			private T _currentItem;
			private int _currentIndex = -1;

			public ArrayEnumerator(T[] array, int count)
			{
				_array = array;
				_count = count;
			}

			public T Current
			{
				get
				{
					return _currentItem;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return Current;
				}
			}

			public void Dispose()
			{
				
			}

			public bool MoveNext()
			{
				if (_currentIndex + 1 == _count)
					return false;

				_currentIndex++;
				_currentItem = _array[_currentIndex];
				return true;
			}

			public void Reset()
			{
				_currentIndex = -1;
				_currentItem = default(T);
			}
		}
	}
}
