﻿using List.Production;
using System;
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

		[Theory]
		[MemberData("IndexTestDataForAdd")]
		public void ItemsThatAreAddedMustBeRetrievableViaIndex<T>(T[] itemsToBeAdded,
			                                                      int index,
																  T expected)
		{
			var testTarget = new List<T>();

			foreach (var item in itemsToBeAdded)
			{
				testTarget.Add(item);
			}

			Assert.Equal(expected, testTarget[index]);
		}

		public static readonly TestData IndexTestDataForAdd = new[]
			{
				new object[] { new int[] {1, 2 ,3 }, 1, 2 },
				new object[] { new string[] {"Hello", "World" }, 0, "Hello" }
			};

		[Fact]
		public void InternalArrayMustBeResizedAutomaticallyWhenExceedingItsCapacity()
		{
			var testTarget = new List<int>(4);
			Assert.Equal(4, testTarget.Capacity);

			var random = new Random();
			for (int i = 0; i < 5; i++)
			{
				testTarget.Add(random.Next());
			}

			Assert.True(testTarget.Capacity > 4);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(0)]
		[InlineData(-42)]
		public void ConstructorMustThrowExceptionWhenInitialCapacityIsLessThanTwo(int initialCapacity)
		{
			Assert.Throws<ArgumentException>(() => new List<int>(initialCapacity));
		}
    }
}
