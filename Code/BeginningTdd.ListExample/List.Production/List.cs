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
			_internalArray = new T[4];
		}

		public List(int initialCapacity)
		{
			if (initialCapacity < 2)
				throw new ArgumentException("initialCapacity cannot be less than two", "initialCapacity");

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
			for (int i = 0; i < _count; i++)
			{
				if (item.CompareWithHashCodeAndEquals(_internalArray[i]))
                    return i;
			}
			return -1;
		}

		public void Insert(int index, T item)
		{
			if (index > _count)
				throw new ArgumentOutOfRangeException("index", string.Format("The specified index with value {0} must not be larger than Count ({1})", index, _count));
			if (index < 0)
				throw new ArgumentOutOfRangeException("index", string.Format("index with value {0} must not be less than zero", index));

			if (_count == Capacity)
				EnlargeArray();

			for (int i = _count - 1; i >= index; i--)
			{
				_internalArray[i + 1] = _internalArray[i];
			}
			_internalArray[index] = item;
			_count++;
		}

		public void RemoveAt(int index)
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException("index", string.Format("The index with value {0} must not be less than zero", index));
			if (index >= _count)
				throw new ArgumentOutOfRangeException("index", string.Format("You did not specify a valid index. Your value: {0}, Count of Collection: {1}", index, _count));

			_internalArray[index] = default(T);

			for (int i = index; i < _count - 1; i++)
			{
				_internalArray[i] = _internalArray[i + 1];
				if (i == _count - 2)
					_internalArray[i + 1] = default(T);
			}
			_count--;
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
			return IndexOf(item) != -1;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException("array");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex must not be less than zero, but you specified " + arrayIndex);

			var numberOfPositionsInArray = array.Length - arrayIndex;
			if (numberOfPositionsInArray < _count)
				throw new ArgumentException("The target array is too small because it can only hold " + numberOfPositionsInArray + " items, but " + _count + " would be required.",
											"array");

			for (int i = 0; i < _count; i++)
			{
				array[arrayIndex + i] = _internalArray[i];
			}
		}

		public bool Remove(T item)
		{
			var indexOfItemBeingRemoved = IndexOf(item);
			if (indexOfItemBeingRemoved == -1)
				return false;

			RemoveAt(indexOfItemBeingRemoved);
			return true;
		}

		public T this[int index]
		{
			get { return _internalArray[index];  }
			set
			{
				if (index > _count)
					throw new IndexOutOfRangeException(string.Format("index with value {0} must not be larger than Count ({1}).", index, _count));

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
