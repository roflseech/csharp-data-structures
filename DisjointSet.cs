using System;

namespace PDG.DataStructures
{
    /// <summary>
    /// Collection of disjoint sets.
    /// </summary>
    public class DisjointSet
    {
        private int[] _parent;
        private int[] _rank;

        public DisjointSet(int count)
        {
            if (count < 2) throw new ArgumentException("Count can't be less than 2");
            _parent = new int[count];
            _rank = new int[count];
            for (int i = 0; i < count; i++) _parent[i] = i;
        }
        /// <summary>
        /// Unites two subsets, to which elements a and b belong.
        /// </summary>
        public bool Union(int a, int b)
        {
            int aRoot = FindRoot(a);
            int bRoot = FindRoot(b);
            if (aRoot == bRoot) return false;
            if (_rank[a] < _rank[b])
            {
                _parent[aRoot] = bRoot;
            }
            else if (_rank[a] > _rank[b])
            {
                _parent[bRoot] = aRoot;
            }
            else
            {
                _parent[aRoot] = bRoot;
                _rank[bRoot]++;
            }
            return true;
        }
        /// <summary>
        /// Finds root element of the set, to which element i belongs.
        /// </summary>
        public int FindRoot(int i)
        {
            while (_parent[i] != i) i = _parent[i];
            return i;
        }
        /// <summary>
        /// Checks if two elements are in the same set.
        /// </summary>
        public bool Connected(int a, int b)
        {
            int aRoot = FindRoot(a);
            int bRoot = FindRoot(b);

            return aRoot == bRoot;
        }
        /// <summary>
        /// Total count of elements.
        /// </summary>
        public int Count => _parent.Length;
    }
}