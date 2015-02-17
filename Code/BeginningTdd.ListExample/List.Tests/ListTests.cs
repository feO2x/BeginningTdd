using List.Production;
using System;
using System.Collections;
using System.Linq;
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
			var testTarget = new ListBuilder<int>().WithItems(itemsToAdd)
												   .Build();

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
			var testTarget = new ListBuilder<T>().WithItems(itemsToBeAdded)
												 .Build();

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

		[Theory]
		[MemberData("ForeachTestData")]
        public void TestTargetMustBeIterableWithForeachLoop<T>(T[] items)
		{
			var testTarget = new ListBuilder<T>().WithItems(items)
											     .Build();

			var index = 0;
			foreach (var item in testTarget)
			{
				Assert.Equal(items[index], item);
				index++;
			}
		}

		[Theory]
		[MemberData("ForeachTestData")]
		public void TestTargetMustBeIterableWithIEnumerableInterface<T>(T[] items)
		{
			var testTarget = new ListBuilder<T>().WithItems(items)
												 .Build();

			var castedTestTarget = (IEnumerable) testTarget;

			var index = 0;
			foreach (T item in castedTestTarget)
			{
				Assert.Equal(items[index], item);
				index++;
			}
		}

		public static readonly TestData ForeachTestData = new[]
			{
				new object[] { new string[] { "Hello", "World", "Foo" } },
				new object[] { new object[] { new object(), new object() } }
			};

		[Theory]
		[InlineData(1)]
		[InlineData("Hello")]
		[InlineData(false)]
        public void TestTargetImplementsListOfT<T>(T item)
		{
			Assert.IsAssignableFrom<System.Collections.Generic.IList<T>>(new List<T>());
		}

		[Fact]
		public void IsReadOnlyMustReturnFalse()
		{
			Assert.False(new List<int>().IsReadOnly);
		}

		[Theory]
		[MemberData("IndexSetTestData")]
		public void ItemCanBeExchangedViaTheIndexOperator<T>(T[] items, int index, T newItem)
		{
			var testTarget = new List<T>();
			foreach (var item in items)
			{
				testTarget.Add(item);
			}

			testTarget[index] = newItem;

			Assert.Equal(newItem, testTarget[index]);
		}

		public static readonly TestData IndexSetTestData = new[]
			{
				new object[] { new string[] { "Hello", "There" }, 1, "World" },
				new object[] { new object[] { 1, 2, 3, 4 }, 3, 87 }
			};

		[Theory]
		[MemberData("IndexSetAtEndTestData")]
		public void ItemIsAddedToTheEndOfTheCollectionWhenIndexIsEqualToCount<T>(T[] items, int index, T newItem)
		{
			var testTarget = new ListBuilder<T>().WithItems(items)
												 .Build();

			testTarget[index] = newItem;

			var expected = items.Concat(new[] { newItem })
								.ToList();
			Assert.Equal(expected, testTarget);
		}

		public static readonly TestData IndexSetAtEndTestData = new[]
			{
				new object[] { new string[] { "Foo", "Bar", "Baz" }, 3, "Qux" },
				new object[] { new object[] { 1, 2, 3, 4, 5 }, 5, 87 }
			};

		[Theory]
		[InlineData(-1)]
		[InlineData(-10)]
		[InlineData(2)]
		[InlineData(5)]
		public void ExceptionIsThrownWhenInvalidIndexIsSpecifiedOnSet(int invalidIndex)
		{
			var testTarget = new List<int>();

			Assert.Throws<IndexOutOfRangeException>(() => testTarget[invalidIndex] = 42);
		}

		[Theory]
		[MemberData("ClearTestData")]
		public void ClearEmptiesTheTestTarget<T>(T[] items)
		{
			var testTarget = new ListBuilder<T>().WithItems(items)
												 .Build();

			testTarget.Clear();

			Assert.Empty(testTarget);
		}

		public static readonly TestData ClearTestData = new[]
			{
				new object[] { new object[] { 1, 2, 3, 4, } },
				new object[] { new object[] { } },
				new object[] { new string[] { "Hello", "There" } }
			};

		[Fact]
		public void TestTargetMustNotHoldAReferenceToItemsAfterCallingClear()
		{
			var item = new object();
			var weakReferenceToItem = new WeakReference<object>(item);
			var testTarget = new ListBuilder<object>().WithItems(item)
													  .Build();

			testTarget.Clear();

			item = null;
			GC.Collect();
			object retrievedItem = null;
			Assert.False(weakReferenceToItem.TryGetTarget(out retrievedItem));
			Assert.Null(retrievedItem);
		}
    }
}
