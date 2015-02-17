using List.Production;
using Xunit;
using TestData = System.Collections.Generic.IEnumerable<object[]>;

namespace List.Tests
{
    public class ListTests
    {
		[Theory]
		[MemberData("CountTestDataForAdd")]
        public void CountReflectsTheNumberOfItemsThatWereAddedToTheList(int[] itemsToAdd)
		{
			var testTarget = new List<int>();

			foreach (var item in itemsToAdd)
			{
				testTarget.Add(item);
			}

			Assert.Equal(itemsToAdd.Length, testTarget.Count);
		}

		public static readonly TestData CountTestDataForAdd = new[]
														      {
														        	new object[] { new int[] { 1, 2, 3, 4 } },
														      		new object[] { new int[] { 33, 22, 11 } },
														      		new object[] { new int[] { 76, 103, 105, 87, 66, 53, 89 } }
														      };
    }
}
