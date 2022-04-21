using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Treap<T> where T : IComparable<T>
    {
        Node _root;
        public void Insert(T key)
        {
            var split = Split(_root, key);
            var t1 = Merge(split.Item1, new Node(key));
            _root = Merge(t1, split.Item2);
        }
        public bool Contains(T key)
        {
            Node result = Find(key);
            return result != null;
        }
        private Node Find(T key)
        {
            Node current = _root;
            while (current != null && key.CompareTo(current.Key) != 0)
            {
                if (key.CompareTo(current.Key) > 0)
                {
                    current = current.Right;
                }
                else if (key.CompareTo(current.Key) < 0)
                {
                    current = current.Left;
                }
            }
            return current;
        }
        public bool TryFindClosest(T key, out T result)
        {
            if(_root == null)
            {
                result = default;
                return false;
            }
            Node lastCandidate = null;

            Node current = _root;
            while (key.CompareTo(current.Key) != 0)
            {
                if (key.CompareTo(current.Key) > 0)
                {
                    if (current.Right != null) current = current.Right;
                    else break;
                }
                else
                {
                    if (current.Left != null)
                    {
                        if(current.Left.Key.CompareTo(key) < 0)
                        {
                            lastCandidate = current;
                        }
                        else
                        {
                            lastCandidate = current.Left;
                        }
                        current = current.Left;
                    }
                    else
                    {
                        lastCandidate = current;
                        break;
                    }
                }
            }
            if (key.CompareTo(current.Key) == 0) lastCandidate = current;
            if (lastCandidate != null)
            {
                result = lastCandidate.Key;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }
        private (Node, Node) Split(Node node, T key)
        {
            if (node == null) return (null, null);
            else if(key.CompareTo(node.Key) > 0)
            {
                var tmp = Split(node.Right, key);
                node.Right = tmp.Item1;
                return (node, tmp.Item2);
            }
            else
            {
                var tmp = Split(node.Left, key);
                node.Left = tmp.Item2;
                return (tmp.Item1, node);
            }
        }
        private Node Merge(Node t1, Node t2)
        {
            if (t2 == null) return t1;
            if (t1 == null) return t2;
            if(t1.Priority > t2.Priority)
            {
                t1.Right = Merge(t1.Right, t2);
                return t1;
            }
            else
            {
                t2.Left = Merge(t1, t2.Left);
                return t2;
            }
        }
        private class Node
        {
            private T _key;
            private int _prioriy;
            private Node _left;
            private Node _right;
            public T Key => _key;
            public int Priority => _prioriy;
            public Node Left
            {
                get => _left;
                set => _left = value;
            }
            public Node Right
            {
                get => _right;
                set => _right = value;
            }
            public Node(T key)
            {
                _key = key;
                _prioriy = new Random().Next();
            }
        }
    }
}
