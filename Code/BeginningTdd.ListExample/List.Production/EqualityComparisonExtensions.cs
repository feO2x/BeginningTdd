namespace List.Production
{
	public static class EqualityComparisonExtensions
	{
		public static bool CompareWithHashCodeAndEquals<T>(this T source, T other)
		{
			if (source == null)
			{
				if (other == null)
					return true;

				return other.Equals(source);
			}
			if (other == null)
				return source.Equals(other);

			return source.GetHashCode() == other.GetHashCode() &&
				   source.Equals(other);
		}
	}
}
