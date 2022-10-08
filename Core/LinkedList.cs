using System.Collections;

namespace Core.Structures
{
    public class LinkedList<T> : ICollection<T>
    {
        protected Node<T>? head;

        public int Count { get; private set; } = 0;

        public bool IsReadOnly => false;

        public Node<T>? First => head;

        public Node<T>? Last => head?.Previous ?? head;

        public void Clear()
        {
            head = null;
            Count = 0;
        }

        public void AddFirst(T item)
        {
            Node<T> node = new(item);

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

        public void AddLast(T item)
        {
            Node<T> node = new(item);

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

        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            if (head == null)
            {
                return false;
            }

            if (head.Next == null)
            {
                return comparer.Equals(head.Content, item);
            }

            Node<T> current = head.Next;

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
                throw new InvalidOperationException();
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

            Node<T> next = head.Next;
            Node<T> previous = head.Previous!;

            next.Previous = previous;
            previous.Next = next;
            head = next;

            Count--;
        }

        public void RemoveLast()
        {
            if (head == null)
            {
                throw new InvalidOperationException();
            }

            if (head.Next == null)
            {
                head = null;
                Count--;
                return;
            }

            Node<T> last = head.Previous!;
            head.Previous = last.Previous;
            last.Previous!.Next = head;

            Count--;
        }

        public void Add(T item)
        {
            AddLast(item);
        }

        public bool Remove(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            if (Count == 0)
            {
                return false;
            }

            if (Count == 1 && comparer.Equals(head!.Content, item))
            {
                head = null;
                Count--;
                return true;
            }

            Node<T>? current = head!;

            while (current != null && current.Next != head)
            {
                if (comparer.Equals(current.Content, item))
                {
                    Node<T> previous = current.Previous!;
                    Node<T> next = current.Next!;

                    if (previous.Previous == current && previous.Next == current)
                    {
                        previous.Previous = null;
                        previous.Next = null;
                        Count--;

                        return true;
                    }

                    previous.Next = next;
                    next.Previous = previous;
                    Count--;

                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        protected void RemoveConnections(Node<T> node)
        {
            node.Next = null;
            node.Previous = null;
        }

        protected void CirculeNodes(Node<T> a, Node<T> b)
        {
            a.Next = b;
            a.Previous = b;
            b.Next = a;
            b.Previous = a;
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0 || index > array.Length)
            {
                throw new IndexOutOfRangeException();
            }

            if (array.Length - index < Count)
            {
                throw new ArgumentException("Invalid index");
            }

            Node<T>? node = head;

            if (node == null)
            {
                return;
            }

            do
            {
                array[index++] = node!.Content;
                node = node.Next;
            } while (node != head && node != null);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo((T[])array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            bool hasEnumerationStarted = false;
            Node<T>? currentNode = head;

            while (currentNode != null && (hasEnumerationStarted && currentNode != head || !hasEnumerationStarted))
            {
                hasEnumerationStarted = true;
                yield return currentNode!.Content;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
