using System.Collections;

namespace Core.Structures
{
    public class LinkedList<Type> : ICollection<Type>
    {
        protected Node<Type>? head;

        public int Count { get; private set; } = 0;

        public bool IsReadOnly => false;

        public Node<Type>? First()
        {
            return head;
        }

        public Node<Type>? Last()
        {
            return head?.Previous;
        }

        public void Clear()
        {
            head = null;
            Count = 0;
        }

        public void AddFirst(Type item)
        {
            Node<Type> node = new(item);

            if (head == null)
            {
                head = node;
                Count++;
                return;
            }

            node.Next = head;
            node.Previous = head.Previous;
            head.Previous = node;
            head = node;

            Count++;
        }

        public void AddLast(Type item)
        {
            Node<Type> node = new(item);

            if (head == null)
            {
                head = node;
                Count++;
                return;
            }

            if (head.Next == null)
            {
                CirculeNodes(head, node);
                Count++;
                return;
            }


            head.Previous!.Next = node;
            node.Previous = head.Previous;
            node.Next = head;
            head.Previous = node;

            Count++;
        }

        public bool Contains(Type item)
        {
            EqualityComparer<Type> comparer = EqualityComparer<Type>.Default;

            if (head == null)
            {
                return false;
            }

            if (head.Next == null)
            {
                return comparer.Equals(head.Content, item);
            }

            Node<Type> current = head.Next;

            while (current != head)
            {
                if (comparer.Equals(current!.Content, item))
                {
                    return true;
                }

                current = current!.Next!;
            }

            return false;
        }

        public void RemoveFirst()
        {
            if (head == null)
            {
                throw new NullReferenceException();
            }

            if (head.Next == null)
            {
                head = null;
                Count--;
                return;
            }

            if (head.Next == head.Previous)
            {
                head = head.Next;
                RemoveConnections(head);
                Count--;
                return;
            }

            Node<Type> next = head.Next;
            Node<Type> previous = head.Previous!;

            next.Previous = previous;
            previous.Next = next;
            head = next;

            Count--;
        }

        public void RemoveLast()
        {
            if (head == null)
            {
                throw new NullReferenceException();
            }

            if (head.Next == null)
            {
                head = null;
                Count--;
                return;
            }

            Node<Type> last = head.Previous!;
            head.Previous = last.Previous;
            last.Previous!.Next = head;

            Count--;
        }

        public void Add(Type item)
        {
            AddLast(item);
        }

        public bool Remove(Type item)
        {

            if (head == null)
            {
                throw new NullReferenceException();
            }

            EqualityComparer<Type> comparer = EqualityComparer<Type>.Default;

            if (comparer.Equals(head.Content, item))
            {
                head = null;
                Count--;
                return true;
            }

            Node<Type> current = head;

            while (current.Next != null)
            {
                Node<Type> next = current.Next;

                if (comparer.Equals(next.Content, item))
                {
                    if (next.Next != null)
                    {
                        next = next.Next;
                        next.Previous = current;
                    }
                    else
                    {
                        next = head;
                        head.Previous = current;
                    }

                    Count--;

                    return true;
                }

                current = next;
            }

            return false;
        }

        protected void RemoveConnections(Node<Type> node)
        {
            node.Next = null;
            node.Previous = null;
        }

        protected void CirculeNodes(Node<Type> a, Node<Type> b)
        {
            a.Next = b;
            a.Previous = b;
            b.Next = a;
            b.Previous = a;
        }

        public void CopyTo(Type[] array, int index)
        {
            if (array == null)
            {
                throw new NullReferenceException();
            }

            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index > array.Length)
            {
                throw new IndexOutOfRangeException();
            }

            if (array.Length - index < Count)
            {
                throw new ArgumentException();
            }

            Node<Type>? node = head;

            if (node == null)
            {
                return;
            }

            do
            {
                array[index++] = node!.Content;
                node = node.Next;
            } while (node != head);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo((Type[])array, index);
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Node<Type>
        {
            public Type Content;
            public Node<Type>? Previous;
            public Node<Type>? Next;

            public Node(Type content)
            {
                Content = content;
            }
        }

        public class Enumerator : IEnumerator<Type>, IDisposable
        {
            private readonly LinkedList<Type> list;
            private Node<Type>? currentNode;
            private bool hasEnumerationStarted = false;

            public Type Current => currentNode!.Content;
            object? IEnumerator.Current => Current;

            public Enumerator(LinkedList<Type> list)
            {
                this.list = list;
                currentNode = list.head;
            }

            public void Reset()
            {
                hasEnumerationStarted = false;
                currentNode = list.head;
            }

            public bool MoveNext()
            {
                if (!hasEnumerationStarted)
                {
                    currentNode = list.head;
                    hasEnumerationStarted = true;
                    return currentNode != null;
                }

                currentNode = currentNode!.Next;

                return currentNode != list.head;
            }

            public void Dispose()
            {
                
            }
        }
    }
}
