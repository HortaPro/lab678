using System.Collections.Generic;
using System.Collections;

namespace lab6.Stack
{

    public class Stack<T>:IEnumerable<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;

        public void Enqueue(T value)
        {
            var node = new Node<T>(value);
            if (_head == null)
            {
                _head = node;
            } else
            {
                node.Previous = _tail;
            }
            _tail = node;
        }

        public T? Dequeue()
        {
            if (_tail == null)
                return default;

            var node = _tail;
            _tail = _tail.Previous;
            if (_tail == null)
                _head = null;
            
            return node.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _tail;
            while (current != null && current.Value != null)
            {
                yield return current.Value;
                current = current.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
}
