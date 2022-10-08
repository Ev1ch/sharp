using System.Collections;

namespace Core.Collections
{
    public class Queue<T> : ICollection
    {
        protected readonly Structures.LinkedList<T> items = new();
        
        public int Count => items.Count;

        public bool IsSynchronized => false;

        public object SyncRoot { get; set; } = new();

        public delegate void ChangeHandler(Queue<T> sender);
        
        public delegate void EnqueueHandler(Queue<T> sender, T item);

        public delegate void DequeueHandler(Queue<T> sender, T item);

        public event ChangeHandler OnChanged = delegate { };

        public event EnqueueHandler OnEnqueued = delegate { };

        public event DequeueHandler OnDequeued = delegate { };

        protected virtual void ChangedHandler()
        {
            OnChanged(this);
        }

        protected virtual void EnqueuedHandler(T item)
        {
            OnEnqueued(this, item);
            OnChanged(this);
        }

        protected virtual void DequeuedHandler(T item)
        {
            OnDequeued(this, item);
            OnChanged(this);
        }

        public void Clear()
        {
            items.Clear();
            ChangedHandler();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public T Dequeue()
        {
            Structures.Node<T>? head = items.First;

            if (head == null)
            {
                throw new Exception();
            }

            T item = head.Content;
            items.RemoveFirst();

            DequeuedHandler(item);

            return item;
        }

        public void Enqueue(T item)
        {
            items.AddLast(item);
            EnqueuedHandler(item);
        }

        public T Peek()
        {
            Structures.Node<T>? head = items.First;

            if (head is null)
            {
                throw new InvalidOperationException();
            }

            T item = head.Content;

            return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)items).GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)items).GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            items.CopyTo(array, index);
        }
    }
}
