namespace Main
{
    internal class Program
    {
        private static void PrintQueue(Core.Collections.Queue<int> queue)
        {
            Console.WriteLine("Queue:");
            foreach (var item in queue)
            {
                Console.Write("{0} ", item);
            }
            Console.Write("\n");
        }

        public static void Main(string[] args)
        {
            Core.Collections.Queue<int> queue = new();
            int enqueuesNumber = 0;
            int dequeuesNumber = 0;
            int changesNumber = 0;

            queue.OnEnqueued += (sender, item) => { enqueuesNumber++; };
            queue.OnDequeued += (sender,item) => { dequeuesNumber++; };
            queue.OnChanged += (sender) => { changesNumber++; };

            Console.WriteLine("Adding 1 in collection");
            queue.Enqueue(1);
            Console.WriteLine("Adding 2 in collection");
            queue.Enqueue(2);
            Console.WriteLine("Adding 3 in collection");
            queue.Enqueue(3);

            PrintQueue(queue);

            int[] array = new int[queue.Count];
            queue.CopyTo(array, 0);
            Console.WriteLine("Copied array: {0}", String.Join(", ", array));

            Console.WriteLine("Collection contains 2, doesn't it? {0}", queue.Contains(2));
            Console.WriteLine("Collection contains 10, doesn't it? {0}", queue.Contains(10));

            Console.WriteLine("The first item to be peeked is: {0}", queue.Peek());

            Console.WriteLine("Item is dequeued now");
            queue.Dequeue();

            PrintQueue(queue);

            Console.WriteLine("Collection size is: {0}", queue.Count);

            Console.WriteLine("Enques number: {0}", enqueuesNumber); 
            Console.WriteLine("Dequeues number: {0}", dequeuesNumber);
            Console.WriteLine("Changes number: {0}", changesNumber);
        }
    }
}