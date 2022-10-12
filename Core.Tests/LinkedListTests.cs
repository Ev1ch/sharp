using System;
using AutoBogus;
using NUnit.Framework;
using FluentAssertions;

using Core.Structures;

namespace Core.Tests
{
    [TestFixture]
    public class LinkedListTests
    {
        public class First
        {
            [Test]
            public void EmptyList_ReturnNull()
            {
                LinkedList<int> subject = new();

                subject.First.Should().BeNull();
            }

            [Test]
            public void NotEmptyList_ReturnItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(randomItem);

                subject.First.Content.Should().Be(randomItem);
            }
        }

        public class Last
        {
            [Test]
            public void EmptyList_ReturnNull()
            {
                LinkedList<int> subject = new();

                subject.Last.Should().BeNull();
            }

            [Test]
            public void NotEmptyList_ReturnItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(randomItem);

                subject.Last.Content.Should().Be(randomItem);
            }
        }

        public class Clear
        {
            [Test]
            public void NotEmptyList_ReturnNull()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();
                subject.Add(randomItem);

                subject.Clear();

                subject.Last.Should().BeNull();
                subject.Count.Should().Be(0);
            }
        }

        public class Add
        {
            [Test]
            public void NotEmptyList_ReturnItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(AutoFaker.Generate<int>());
                subject.Add(randomItem);

                subject.Last.Content.Should().Be(randomItem);
            }
        }

        public class AddFirst
        {
            [Test]
            public void NotEmptyList_ReturnItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.AddFirst(AutoFaker.Generate<int>());
                subject.AddFirst(randomItem);

                subject.First.Content.Should().Be(randomItem);
            }
        }

        public class AddLast
        {
            [Test]
            public void NotEmptyList_ReturnItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.AddLast(AutoFaker.Generate<int>());
                subject.AddLast(randomItem);

                subject.Last.Content.Should().Be(randomItem);
            }
        }

        public class Count
        {
            [Test]
            public void EmptyList_ReturnZero()
            {
                LinkedList<int> subject = new();

                subject.Count.Should().Be(0);
            }

            [Test]
            public void NotEmptyList_ReturnSize()
            {
                LinkedList<int> subject = new();

                subject.Add(AutoFaker.Generate<int>());

                subject.Count.Should().Be(1);
            }
        }

        public class IsReadOnly
        {
            [Test]
            public void IsReadOnly_AnyList_ReturnFalse()
            {
                LinkedList<int> subject = new();

                subject.IsReadOnly.Should().BeFalse();
            }
        }

        public class Contains
        {
            [Test]
            public void EmptyList_ReturnFalse()
            {
                LinkedList<int> subject = new();

                subject.Contains(AutoFaker.Generate<int>()).Should().BeFalse();
            }

            [Test]
            public void ItemNotInList_ReturnFalse()
            {
                LinkedList<int> subject = new();

                subject.Add(AutoFaker.Generate<int>());
                subject.Add(AutoFaker.Generate<int>());

                subject.Contains(AutoFaker.Generate<int>()).Should().BeFalse();
            }

            [Test]
            public void ItemInList_ReturnTrue()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(AutoFaker.Generate<int>());
                subject.Add(AutoFaker.Generate<int>());
                subject.Add(randomItem);

                subject.Contains(randomItem).Should().BeTrue();
            }
        }

        public class RemoveFirst
        {
            [Test]
            public void EmptyList_ThrowException()
            {
                LinkedList<int> subject = new();

                Action act = () => subject.RemoveFirst();

                act.Should().Throw<InvalidOperationException>();
            }

            [Test]
            public void NotEmptyListWithOneItem_RemoveItem()
            {
                LinkedList<int> subject = new();

                subject.Add(AutoFaker.Generate<int>());
                subject.RemoveFirst();

                subject.First.Should().BeNull();
            }

            [Test]
            public void NotEmptyListWithCirculedItems_RemoveItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(AutoFaker.Generate<int>());
                subject.Add(randomItem);
                subject.RemoveFirst();

                subject.First.Content.Should().Be(randomItem);
            }

            [Test]
            public void NotEmptyListWithSomeItems_RemoveItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(AutoFaker.Generate<int>());
                subject.Add(randomItem);
                subject.Add(AutoFaker.Generate<int>());
                subject.RemoveFirst();

                subject.First.Content.Should().Be(randomItem);
            }
        }

        public class RemoveLast
        {
            [Test]
            public void EmptyList_ThrowException()
            {
                LinkedList<int> subject = new();

                Action act = () => subject.RemoveLast();

                act.Should().Throw<InvalidOperationException>();
            }

            [Test]
            public void NotEmptyList_RemoveItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(randomItem);
                subject.Add(AutoFaker.Generate<int>());
                subject.RemoveLast();

                subject.Last.Content.Should().Be(randomItem);
            }

            [Test]
            public void NotEmptyListWithOneItem_RemoveItem()
            {
                Random random = new();
                LinkedList<int> subject = new();

                subject.Add(random.Next());
                subject.RemoveLast();

                subject.Last.Should().BeNull();
            }
        }

        public class Remove
        {
            [Test]
            public void Remove_NotEmptyListWithSomeSameItems_RemoveOnlyFirstItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(randomItem);
                subject.Add(randomItem);

                subject.Remove(randomItem).Should().BeTrue();
                subject.First.Content.Should().Be(randomItem);
            }

            [Test]
            public void NotEmptyListWithoutSuchItem_NotRemoveAnyItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(randomItem);

                subject.Remove(AutoFaker.Generate<int>()).Should().BeFalse();
                subject.First.Content.Should().Be(randomItem);
            }

            [Test]
            public void NotEmptyListWithOneItem_RemoveItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(randomItem);
                subject.Remove(randomItem);

                subject.First.Should().BeNull();
            }

            [Test]
            public void EmptyList_NotRemoveAnyItem()
            {
                LinkedList<int> subject = new();

                subject.Remove(AutoFaker.Generate<int>()).Should().BeFalse();
            }

            [Test]
            public void NotEmptyListWithSomeItems_RemoveItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(AutoFaker.Generate<int>());
                subject.Add(randomItem);
                subject.Add(AutoFaker.Generate<int>());
                subject.Remove(randomItem);

                subject.Count.Should().Be(2);
            }
        }

        public class CopyTo
        {
            [Test]
            public void NullArray_ThrowException()
            {
                Array array = null;
                LinkedList<int> subject = new();

                Action act = () => subject.CopyTo(array, 0);

                act.Should().Throw<ArgumentNullException>();
            }

            [Test]
            public void TooBigIndex_ThrowException()
            {
                int arraySize = 1;
                Array array = new int[arraySize];
                LinkedList<int> subject = new();

                Action act = () => subject.CopyTo(array, arraySize + 1);

                act.Should().Throw<IndexOutOfRangeException>();
            }

            [Test]
            public void ToSmallArray_ThrowException()
            {
                int arraySize = 1;
                Array array = new int[arraySize];
                LinkedList<int> subject = new();

                Action act = () =>
                {
                    subject.Add(AutoFaker.Generate<int>());
                    subject.Add(AutoFaker.Generate<int>());
                    subject.CopyTo(array, 0);
                };

                act.Should().Throw<ArgumentException>();
            }

            [Test]
            public void EmptyList_ReturnArray()
            {
                const int arraySize = 1;
                int[] array = new int[arraySize];
                LinkedList<int> subject = new();

                subject.CopyTo(array, 0);

                array[0].Should().Be(0);
            }

            [Test]
            public void CorrectArray_ReturnArray()
            {
                const int arraySize = 1;
                int[] array = new int[arraySize];
                int randomItem = AutoFaker.Generate<int>();
                LinkedList<int> subject = new();

                subject.Add(randomItem);
                subject.CopyTo(array, 0);

                array[0].Should().Be(randomItem);
            }
        }

        public class GetEnumeratorWithGeneric
        {
            [Test]
            public void NotEmptyQueue_ReturnItems()
            {
                const int arraySize = 3;
                int[] array = new int[arraySize];
                LinkedList<int> subject = new();

                int i = 0;
                while (i < arraySize)
                {
                    int item = AutoFaker.Generate<int>();
                    array[i++] = item;
                    subject.Add(item);
                }

                ((System.Collections.Generic.IEnumerable<int>)subject).Should().Equal(array);
            }
        }
    }
}