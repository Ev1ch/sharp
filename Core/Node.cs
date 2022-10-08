namespace Core.Structures
{
    public class Node<T>
    {
        public T Content;
        public Node<T>? Previous;
        public Node<T>? Next;

        public Node(T content)
        {
            Content = content;
        }
    }
}
