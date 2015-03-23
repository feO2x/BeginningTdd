using List.Production;
using Xunit;
using TestData = System.Collections.Generic.IEnumerable<object[]>;

namespace List.Tests
{
	public class CompareWithHashCodeAndEqualsTests
	{
		[Theory]
		[MemberData("NotNullTestData")]
		public void GetHashCodeIsCalledOnAllParametersWhenNoneOfThemIsNull(EqualityMock first, 
															   		       EqualityMock second)
		{
			first.CompareWithHashCodeAndEquals(second);

			Assert.Equal(1, first.NumberOfGetHashCodeCalls);
			Assert.Equal(1, second.NumberOfGetHashCodeCalls);
		}

		public static readonly TestData NotNullTestData = new[]
			{
				new object[] { new EqualityMock(true, 1), new EqualityMock(true, 1) },
				new object[] { new EqualityMock(false, 1), new EqualityMock(false, 5) }
			};

		[Theory]
		[MemberData("CompareTestData")]
        public void ValidBooleanIsReturnedAccordingToParameters(EqualityMock first,
															    EqualityMock second,
																bool expected)
		{
			var actual = first.CompareWithHashCodeAndEquals(second);

			Assert.Equal(expected, actual);
		}

		public static readonly TestData CompareTestData = new[]
			{
				new object[] { new EqualityMock(true, 1), new EqualityMock(true, 1), true },
				new object[] { new EqualityMock(false, 1), new EqualityMock(false, 2), false },
				new object[] { new EqualityMock(false, 1), null, false },
				new object[] { null, new EqualityMock(false, 1), false },
				new object[] { null, null, true }
			};
    }
}
