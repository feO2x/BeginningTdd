namespace List.Tests
{
	public class EqualityMock
	{
		private readonly bool _isEqualToOther;
		private readonly int _hashCode;

		public int NumberOfEqualsCalls { get; private set; }
		public int NumberOfGetHashCodeCalls { get; private set; }

		public EqualityMock(bool isEqualToOther, int hashCode)
		{
			_isEqualToOther = isEqualToOther;
			_hashCode = hashCode;
		}

		public override bool Equals(object obj)
		{
			NumberOfEqualsCalls++;
			return _isEqualToOther;
		}

		public override int GetHashCode()
		{
			NumberOfGetHashCodeCalls++;
			return _hashCode;
		}
	}
}
