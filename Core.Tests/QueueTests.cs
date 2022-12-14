using System;
using System.Collections;
using AutoBogus;
using NUnit.Framework;
using FluentAssertions;

using Core.Collections;
using System.Reflection.Metadata;

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

                act.Should().Throw<InvalidOperationException>().WithMessage("Can not dequeue: collection is empty");
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

                act.Should().Throw<InvalidOperationException>().WithMessage("Can not peek: collection is empty");
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

                act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'array')");
            }

            [Test]
            public void TooBigIndex_ThrowException()
            {
                int arraySize = 1;
                Array array = new int[arraySize];
                Queue<int> subject = new();

                Action act = () => subject.CopyTo(array, arraySize + 1);

                act.Should().Throw<IndexOutOfRangeException>().WithMessage("Can not copy: index is out of range");
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

                act.Should().Throw<ArgumentException>().WithMessage("Can not copy: invalid index");
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

        public class OnEnqueued
        {
            [Test]
            public void ItemAdded_FireEvent()
            {
                int randomItem = AutoFaker.Generate<int>();
                Queue<int> subject = new();
                var monitoredSubject = subject.Monitor();

                subject.Enqueue(randomItem);

                monitoredSubject.Should().Raise("OnEnqueued").WithSender(subject).WithArgs<int>((arg) => arg == randomItem);
            }
        }

        public class OnDequeued
        {
            [Test]
            public void ItemRemoved_FireEvent()
            {
                int randomItem = AutoFaker.Generate<int>();
                Queue<int> subject = new();
                var monitoredSubject = subject.Monitor();

                subject.Enqueue(randomItem);
                subject.Dequeue();

                monitoredSubject.Should().Raise("OnDequeued").WithSender(subject).WithArgs<int>((arg) => arg == randomItem);
            }
        }

        public class OnChanged
        {
            [Test]
            public void ItemAdded_FireEvent()
            {
                Queue<int> subject = new();
                var monitoredSubject = subject.Monitor();

                subject.Enqueue(AutoFaker.Generate<int>());

                monitoredSubject.Should().Raise("OnChanged").WithSender(subject);
            }
 
            [Test]
            public void ItemRemoved_FireEvent()
            {
                Queue<int> subject = new();
                var monitoredSubject = subject.Monitor();

                subject.Enqueue(AutoFaker.Generate<int>());
                subject.Dequeue();

                monitoredSubject.Should().Raise("OnDequeued").WithSender(subject);
            }
        }

        public class GetEnumeratorWithoutGeneric
        {
            [Test]
            public void NotEmptyQueue_ReturnItems()
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

                ((IEnumerable)subject).Should().BeEquivalentTo(array);
            }
        }

        public class GetEnumeratorWithGeneric
        {
            [Test]
            public void NotEmptyQueue_ReturnItems()
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

                ((System.Collections.Generic.IEnumerable<int>)subject).Should().Equal(array);
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
            public void AnyQueue_ReturnNull()
            {
                Queue<int> subject = new();

                subject.SyncRoot.Should().BeNull();
            }
        }
    }
}