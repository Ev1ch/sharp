using System.Collections;

namespace Core.Structures
{
    public class LinkedList<Type> : ICollection<Type>
    {
        protected Node<Type>? head;

        public int Count { get; private set; } = 0;

        public bool IsReadOnly => false;

        public Node<Type>? First => head;

        public Node<Type>? Last => head?.Previous ?? head;

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
            EqualityComparer<Type> comparer = EqualityComparer<Type>.Default;

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

            Node<Type>? current = head!;

            while (current != null && current.Next != head)
            {
                if (comparer.Equals(current.Content, item))
                {
                    Node<Type> previous = current.Previous!;
                    Node<Type> next = current.Next!;

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
                throw new ArgumentNullException();
            }

            if (index < 0 || index > array.Length)
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
            } while (node != head && node != null);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo((Type[])array, index);
        }

        public IEnumerator<Type> GetEnumerator()
        {
            bool hasEnumerationStarted = false;
            Node<Type>? currentNode = head;

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
    }
}
