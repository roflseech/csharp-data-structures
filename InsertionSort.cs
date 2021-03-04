using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public static class InsertionSort
    {
        public static void Sort<T> (T[] array) where T : IComparable
        {
            for(int i = 1; i < array.Length; i++)
            {
                T current = array[i];
                int j = i - 1;
                for (; j >= 0 && current.CompareTo(array[j]) == -1; j--)
                {
                    array[j + 1] = array[j];
                }
                if(j != i-1) array[j+1] = current;
            }
        }
    }
}
