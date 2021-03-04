using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    class LinkedList<T> : IEnumerable<T>
    {
        Node _first;
        Node _last;
        int _count;

        public LinkedList()
        {
            _first = null;
            _last = null;
            _count = 0;
        }
        public int Count => _count;
        public void AddLast(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            Node newNode = new Node(value);
            if(_last != null)
            {
                _last._next = newNode;
                newNode._prev = _last;
                _last = newNode;
            }
            else
            {
                _last = newNode;
                _first = newNode;
            }
            _count++;
        }
        public void AddFirst(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            Node newNode = new Node(value);
            if (_first != null)
            {
                _first._prev = newNode;
                newNode._next = _first;
                _first = newNode;
            }
            else
            {
                _last = newNode;
                _first = newNode;
            }
            _count++;
        }
        public void AddAt(int index, T value)
        {
            int newIndex = index - 1;
            if(newIndex == -1)
            {
                AddFirst(value);
            }
            else if(index == Count)
            {
                AddLast(value);
            }
            else
            {
                AddAfter(newIndex, value);
            }
        }
        public void AddAfter(int index, T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            Node afterNode = FindByIndex(index);
            Node newNode = new Node(value);
            AddAfter(afterNode, newNode);
        }
        public void AddAfter(int index, Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            Node afterNode = FindByIndex(index);
            
            AddAfter(afterNode, node);
        }
        public void AddAfter(T insertAfter, T inserted)
        {
            if (insertAfter == null || inserted == null)
            {
                throw new ArgumentNullException();
            }
            Node afterNode = Find(insertAfter);
            Node newNode = new Node(inserted);
            if (afterNode != null) AddAfter(afterNode, newNode);
            else throw new Exception($"Can't find {inserted}");
        }
        public void AddAfter(Node insertAfter, Node inserted)
        {
            if (insertAfter == null || inserted == null)
            {
                throw new ArgumentNullException();
            }
            Node oldNext = insertAfter._next;
            insertAfter._next = inserted;
            inserted._prev = insertAfter;
            if (oldNext != null)
            {
                inserted._next = oldNext;
                oldNext._prev = inserted;
            }
            else
            {
                _last = inserted;
            }
            _count++;
        }
        public void RemoveAt(int index)
        {
            Node valueNode = FindByIndex(index);
            Remove(valueNode);
        }
        public void Remove(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            Node valueNode = Find(value);
            if (valueNode != null) Remove(valueNode);
            else throw new Exception($"Can't find {value}");
        }
        public void Remove(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
            if (node != _first && node != _last)
            {
                node._prev._next = node._next;
                node._next._prev = node._prev;
            }
            else
            {
                if (node == _first)
                {
                    _first = _first._next;
                    if (_first != null) _first._prev = null;
                }
                if (node == _last)
                {
                    _last = _last._prev;
                    if (_last != null) _last._next = null;
                }
            }
            node.Invalidate();
            _count++;
        }
        public Node Find(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            Node current = _first;
            while (current != null)
            {
                if (current.Value.Equals(value)) return current;
                current = current.Next;
            }
            return null;
        }
        public Node FindByIndex(int index)
        {
            if (Count == 0)
            {
                throw new Exception($"Index {index} is invalid");
            }
            if (index < 0 || index >= Count)
            {
                throw new Exception($"Index {index} out of range [0:{Count - 1}]");
            }
            Node current = _first;
            for (int i = 0; i < index; i++) current = current.Next;
            return current;
        }
        public void PrintList()
        {
            if(Count == 0)
            {
                Console.WriteLine($"Empty");
                return;
            }
            int i = 0;
            foreach(var item in this)
            {
                Console.WriteLine($"[{i++}] {item}");
            }
        }
        public void Swap(Node a, Node b)
        {
            if (a == null || b == null)
            {
                throw new ArgumentNullException();
            }
            T tmp = a.Value;
            a.Value = b.Value;
            b.Value = tmp;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            Node current = _first;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
        public IEnumerable<T> ReversedEnumerator()
        {
            Node current = _last;
            while (current != null)
            {
                yield return current.Value;
                current = current.Prev;
            }
        }

        public class Node
        {
            internal Node _next;
            internal Node _prev;
            public T Value { get; set; }
            public Node(T value)
            {
                Value = value;
                _next = null;
                _prev = null;
            }
            public Node Next => _next;
            public Node Prev => _prev;
            internal void Invalidate()
            {
                _next = null;
                _prev = null;
            }
        }
    }
}
