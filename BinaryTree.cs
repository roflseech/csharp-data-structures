using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node _root;
        public BinaryTree()
        {

        }
        public bool Add(T value)
        {
            if (value == null) throw new ArgumentNullException();

            if (_root == null) _root = new Node(null, value);
            else
            {
                Node current = _root;
                do
                {
                    var comparisonResult = current.Value.CompareTo(value);
                    if (comparisonResult == 1)
                    {
                        if (current.Left == null)
                        {
                            current._left = new Node(current, value);
                            return true;
                        }
                        current = current.Left;
                    }
                    else if (comparisonResult == -1)
                    {
                        if (current.Right == null)
                        {
                            current._right = new Node(current, value);
                            return true;
                        }
                        current = current.Right;
                    }
                    else
                    {
                        return false;
                    }
                } while (current != null);
            }
            return false;
        }
        public Node Find(T value)
        {
            if (value == null) throw new ArgumentNullException();

            if (_root == null) _root = new Node(null, value);
            else
            {
                Node current = _root;
                do
                {
                    var comparisonResult = current.Value.CompareTo(value);
                    if (comparisonResult == 1)
                    {
                        current = current.Left;
                    }
                    else if (comparisonResult == -1)
                    {
                        current = current.Right;
                    }
                    else
                    {
                        return current;
                    }
                } while (current != null);
            }
            return null;
        }
        public void Remove(T value)
        {
            if (value == null) throw new ArgumentNullException();
            Node node = Find(value);
            Remove(node);
        }
        public void Remove(Node node)
        {
            if (node == null) throw new Exception("Attempt to delete non existing node");

            int childrenCount = node.ChildrenCount;
            if (childrenCount == 0)
            {
                if (node.Parent != null)
                {
                    if (node.Parent.Left == node) node.Parent._left = null;
                    else node.Parent._right = null;
                }
                else
                {
                    _root = null;
                }
            }
            else if (childrenCount == 1)
            {
                Node notNullChild = node.Left != null ? node.Left : node.Right;
                if (node.Parent != null)
                {
                    if (node.Parent.Left == node) node.Parent._left = notNullChild;
                    else node.Parent._right = notNullChild;
                    notNullChild._parent = node.Parent;
                }
                else
                {
                    _root = notNullChild;
                    _root._parent = null;
                }
            }
            else if (childrenCount == 2)
            {
                Node maxInLeftBranch = FindMax(node.Left);
                node._value = maxInLeftBranch.Value;
                Remove(maxInLeftBranch);
                node = maxInLeftBranch;
            }
            node.Invalidate();
        }
        public Node FindMax(Node node)
        {
            Node result = node;
            while (result.Right != null)
            {
                result = result.Right;
            }
            return result;
        }
        public IEnumerable<T> InDepthRightTraversal()
        {
            Stack<Node> unprocessedNodes = new Stack<Node>();
            unprocessedNodes.Push(_root);
            while (unprocessedNodes.Count != 0)
            {
                Node current = unprocessedNodes.Pop();
                if (current.Right != null) unprocessedNodes.Push(current.Left);
                if (current.Left != null) unprocessedNodes.Push(current.Right);

                yield return current.Value;
            }
        }
        public IEnumerable<T> InDepthLeftTraversal()
        {
            Stack<Node> unprocessedNodes = new Stack<Node>();
            unprocessedNodes.Push(_root);
            while (unprocessedNodes.Count != 0)
            {
                Node current = unprocessedNodes.Pop();
                if (current.Right != null) unprocessedNodes.Push(current.Right);
                if (current.Left != null) unprocessedNodes.Push(current.Left);

                yield return current.Value;
            }
        }
        public IEnumerable<T> InWidthTraversal()
        {
            Queue<Node> unprocessedNodes = new Queue<Node>();
            unprocessedNodes.Enqueue(_root);
            while (unprocessedNodes.Count != 0)
            {
                Node current = unprocessedNodes.Dequeue();

                if (current.Left != null) unprocessedNodes.Enqueue(current.Left);
                if (current.Right != null) unprocessedNodes.Enqueue(current.Right);

                yield return current.Value;
            }
        }
        public void PrintTree()
        {
            if(_root == null)
            {
                Console.WriteLine("Empty");
                return;
            }
            Queue<Node> unprocessedNodes = new Queue<Node>();
            unprocessedNodes.Enqueue(_root);
            int depth = 1;
            int countOnThisLevel = 1;
            int countOnNextLevel = 0;
            Console.Write(new string(' ', 50 / depth / 2));

            while (unprocessedNodes.Count != 0)
            {
                string gap = new string(' ', 50 / depth);
                Node current = unprocessedNodes.Dequeue();

                if (current.Left != null)
                {
                    unprocessedNodes.Enqueue(current.Left);
                    countOnNextLevel++;
                }
                if (current.Right != null)
                {
                    unprocessedNodes.Enqueue(current.Right);
                    countOnNextLevel++;
                }
                Console.Write($"{current.Value}" + gap);
                countOnThisLevel--;
                if(countOnThisLevel == 0)
                {
                    countOnThisLevel = countOnNextLevel;
                    countOnNextLevel = 0;
                    depth++;
                    Console.WriteLine();
                    Console.Write(new string(' ', 50 / depth/2));
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return InWidthTraversal().GetEnumerator();
        }


        public class Node
        {
            internal Node _left;
            internal Node _right;
            internal Node _parent;
            internal T _value;
            public T Value => _value;
            public Node(Node parent, T value)
            {
                _value = value;
                _left = null;
                _right = null;
                _parent = parent;
            }
            public Node Left => _left;
            public Node Right => _right;
            public Node Parent => _parent;
            public int ChildrenCount
            {
                get
                {
                    int count = 0;
                    count += _left == null ? 0 : 1;
                    count += _right == null ? 0 : 1;
                    return count;
                }
            }
            public void Invalidate()
            {
                _left = null;
                _right = null;
                _parent = null;
            }
        }
    }
}
