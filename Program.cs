using System;
using System.Collections;

namespace DataStructures
{
    class Program
    {
        static void TestSort()
        {
            string input;
            do
            {
                Console.Write("Input array(Stop to exit): ");
                input = Console.ReadLine();
                if(input.ToLower() != "stop")
                {
                    try
                    {
                        var inputSplit = input.Split(' ');
                        int[] array = new int[inputSplit.Length];
                        int index = 0;
                        foreach (var i in inputSplit)
                        {
                            array[index] = Int32.Parse(i);
                            index++;
                        }
                        InsertionSort.Sort(array);
                        Console.WriteLine("Sorted");
                        foreach (var i in array)
                        {
                            Console.Write($"{i} ");
                        }
                        Console.WriteLine();
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("Wrong argument");
                    }
                }
            } while (input.ToLower() != "stop");
            
        }
        static void Main(string[] args)
        {
            string command;
            do
            {
                Console.Clear();
                Console.WriteLine("1 - linked list test, 2 - binary tree test, 3 - sort test, 4 - exit");
                Console.Write("Input: ");
                command = Console.ReadLine();
                Console.Clear();
                if (command == "1") LinkedListTest.Run();
                else if (command == "2") BinaryTreeTest.Run();
                else if (command == "3") TestSort();
            } while (command != "4");
            
            
            TestSort();
        }
    }
}
