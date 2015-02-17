
namespace List.Production
{
	public class List<T>
	{
		private int _count;

		public int Count { get { return _count; } }

		public void Add(int v)
		{
			_count++;
		}
	}
}
