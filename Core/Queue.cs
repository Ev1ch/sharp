using System.Collections;

namespace Core.Collections
{
    public class Queue<Type> : ICollection
    {
        protected readonly Structures.LinkedList<Type> items = new();
        
        public int Count => items.Count;

        public bool IsSynchronized => false;

        public object SyncRoot { get; set; } = new();

        public delegate void ChangeHandler(Queue<Type> sender);
        
        public delegate void EnqueueHandler(Queue<Type> sender, Type item);

        public delegate void DequeueHandler(Queue<Type> sender, Type item);

        public event ChangeHandler OnChanged = delegate { };

        public event EnqueueHandler OnEnqueued = delegate { };

        public event DequeueHandler OnDequeued = delegate { };

        protected virtual void ChangedHandler()
        {
            OnChanged(this);
        }

        protected virtual void EnqueuedHandler(Type item)
        {
            OnEnqueued(this, item);
            OnChanged(this);
        }

        protected virtual void DequeuedHandler(Type item)
        {
            OnDequeued(this, item);
            OnChanged(this);
        }

        public void Clear()
        {
            items.Clear();
            ChangedHandler();
        }

        public bool Contains(Type item)
        {
            return items.Contains(item);
        }

        public Type Dequeue()
        {
            Structures.LinkedList<Type>.Node<Type>? head = items.First;

            if (head == null)
            {
                throw new Exception();
            }

            Type item = head.Content;
            items.RemoveFirst();

            DequeuedHandler(item);

            return item;
        }

        public void Enqueue(Type item)
        {
            items.AddLast(item);
            EnqueuedHandler(item);
        }

        public Type Peek()
        {
            Structures.LinkedList<Type>.Node<Type>? head = items.First;

            if (head is null)
            {
                throw new Exception();
            }

            Type item = head.Content;

            return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)items).GetEnumerator();
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return ((IEnumerable<Type>)items).GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            items.CopyTo(array, index);
        }
    }
}
