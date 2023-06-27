namespace lab6.Stack
{
    public class Node<T>
    {
        public T? Value { get; set; }
        public Node<T>? Previous { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }
}
