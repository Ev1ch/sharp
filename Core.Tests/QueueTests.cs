using System;
using AutoBogus;
using NUnit.Framework;
using FluentAssertions;

using Core.Collections;

namespace Core.Tests
{
    [TestFixture]
    public class QueueTests
    {
        public class Enqueue
        {
            [Test]
            public void EmptyQueue_ReturnItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                Queue<int> subject = new();

                subject.Enqueue(randomItem);

                subject.Peek().Should().Be(randomItem);
            }
        }

        public class Dequeue
        {
            [Test]
            public void EmptyQueue_ThrowException()
            {
                Queue<int> subject = new();

                Action act = () => subject.Dequeue();

                act.Should().Throw<InvalidOperationException>();
            }

            [Test]
            public void NotEmptyQueue_ReturnItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                Queue<int> subject = new();

                subject.Enqueue(randomItem);

                subject.Dequeue().Should().Be(randomItem);
                subject.Count.Should().Be(0);
            }

            [Test]
            public void NotEmptyQueue_ReturnFirstItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                Queue<int> subject = new();

                subject.Enqueue(randomItem);
                subject.Enqueue(AutoFaker.Generate<int>());

                subject.Dequeue().Should().Be(randomItem);
                subject.Count.Should().Be(1);
            }
        }

        public class Peek
        {
            [Test]
            public void EmptyQueue_ThrowException()
            {
                Queue<int> subject = new();

                Action act = () => subject.Peek();

                act.Should().Throw<InvalidOperationException>();
            }

            [Test]
            public void NotEmptyQueue_ReturnFirstItem()
            {
                int randomItem = AutoFaker.Generate<int>();
                Queue<int> subject = new();

                subject.Enqueue(randomItem);
                subject.Enqueue(AutoFaker.Generate<int>());

                subject.Peek().Should().Be(randomItem);
                subject.Count.Should().Be(2);
            }
        }

        public class Clear
        {
            [Test]
            public void NotEmptyQueue_EmptyQueue()
            {
                Queue<int> subject = new();

                subject.Enqueue(AutoFaker.Generate<int>());
                subject.Enqueue(AutoFaker.Generate<int>());
                subject.Clear();

                subject.Count.Should().Be(0);
            }
        }

        public class Contains
        {
            [Test]
            public void EmptyQueue_ReturnFalse()
            {
                Queue<int> subject = new();

                subject.Contains(AutoFaker.Generate<int>()).Should().BeFalse();
            }

            [Test]
            public void QueueWithItem_ReturnTrue()
            {
                int randomItem = AutoFaker.Generate<int>();
                Queue<int> subject = new();

                subject.Enqueue(AutoFaker.Generate<int>());
                subject.Enqueue(randomItem);

                subject.Contains(randomItem).Should().BeTrue();
            }

            [Test]
            public void QueueWithoutItem_ReturnFalse()
            {
                Queue<int> subject = new();

                subject.Enqueue(AutoFaker.Generate<int>());

                subject.Contains(AutoFaker.Generate<int>()).Should().BeFalse();
            }
        }

        public class CopyTo
        {
            [Test]
            public void NullArray_ThrowException()
            {
                Array array = null;
                Queue<int> subject = new();

                Action act = () => subject.CopyTo(array, 0);

                act.Should().Throw<ArgumentNullException>();
            }

            [Test]
            public void TooBigIndex_ThrowException()
            {
                int arraySize = 1;
                Array array = new int[arraySize];
                Queue<int> subject = new();

                Action act = () => subject.CopyTo(array, arraySize + 1);

                act.Should().Throw<IndexOutOfRangeException>();
            }

            [Test]
            public void ToSmallArray_ThrowException()
            {
                int arraySize = 1;
                Array array = new int[arraySize];
                Queue<int> subject = new();

                Action act = () =>
                {
                    subject.Enqueue(AutoFaker.Generate<int>());
                    subject.Enqueue(AutoFaker.Generate<int>());
                    subject.CopyTo(array, 0);
                };

                act.Should().Throw<ArgumentException>();
            }

            [Test]
            public void EmptyList_ReturnArray()
            {
                const int arraySize = 1;
                int[] array = new int[arraySize];
                Queue<int> subject = new();

                subject.CopyTo(array, 0);

                array[0].Should().Be(0);
            }

            [Test]
            public void CorrectArray_ReturnArray()
            {
                const int arraySize = 1;
                int[] array = new int[arraySize];
                int randomItem = AutoFaker.Generate<int>();
                Queue<int> subject = new();

                subject.Enqueue(randomItem);
                subject.CopyTo(array, 0);

                array[0].Should().Be(randomItem);
            }
        }

        public class GetEnumerator
        {
            [Test]
            public void NotEmptyList_ReturnItems()
            {
                const int arraySize = 3;
                int[] array = new int[arraySize];
                Queue<int> subject = new();

                int i = 0;
                while (i < arraySize)
                {
                    int item = AutoFaker.Generate<int>();
                    array[i++] = item;
                    subject.Enqueue(item);
                }

                int j = 0;
                foreach (int item in subject)
                {
                    item.Should().Be(array[j++]);
                }
            }
        }

        public class IsSynchronized
        {
            [Test]
            public void AnyQueue_ReturnFalse()
            {
                Queue<int> subject = new();

                subject.IsSynchronized.Should().BeFalse();
            }
        }

        public class SyncRoot
        {
            [Test]
            public void AnyQueue_ReturnObject()
            {
                Queue<int> subject = new();

                subject.SyncRoot.Should().BeOfType<object>();
            }
        }
    }
}