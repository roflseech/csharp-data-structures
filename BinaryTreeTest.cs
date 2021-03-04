using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    class BinaryTreeTest
    {
        static Dictionary<string, Func<string[], bool>> InitCommands(BinaryTree<int> tree, out Dictionary<string, string> commandsDescriptions)
        {
            var result = new Dictionary<string, Func<string[], bool>>(StringComparer.OrdinalIgnoreCase);
            commandsDescriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            result.Add("Add", (string[] s) => {
                if (s.Length == 0)
                {
                    Console.WriteLine("Not enough arguments");
                    return false;
                }
                try
                {
                    foreach (var a in s)
                    {
                        if(!tree.Add(Int32.Parse(a)))
                        {
                            Console.WriteLine($"Failed adding {a}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return true;
            });
            commandsDescriptions.Add("Add", "[value1] [value2]...");

            result.Add("Remove", (string[] s) => {
                if (s.Length == 0)
                {
                    Console.WriteLine("Not enough arguments");
                    return false;
                }

                for (int i = 1; i <= s.Length; i++)
                {
                    try
                    {
                        tree.Remove(Int32.Parse(s[^i]));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                }

                return true;
            });
            commandsDescriptions.Add("Remove", "[value1] [value2]...");

            result.Add("Print", (string[] s) => {
                if (s.Length == 0)
                {
                    Console.WriteLine("Not enough arguments");
                }
                foreach(var j in s)
                {
                    IEnumerable<int> enumerable = null;
                    if (j == "width") enumerable = tree.InWidthTraversal();
                    else if (j == "depthleft") enumerable = tree.InDepthLeftTraversal();
                    else if (j == "depthright") enumerable = tree.InDepthRightTraversal();
                    if(enumerable != null)
                    {
                        foreach (var i in enumerable)
                        {
                            Console.Write($"{i} ");
                        }
                        Console.WriteLine();
                    }
                }
                return false;
            });
            commandsDescriptions.Add("Print", "[width/depthleft/depthright] [width/depthleft/depthright]...");
            return result;
        }
        public static void Run()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            Dictionary<string, string> commandsArgs;
            var commands = InitCommands(tree, out commandsArgs);
            string command;
            do
            {
                Console.Clear();
                Console.WriteLine("List:");
                tree.PrintTree();
                Console.WriteLine();

                Console.WriteLine("Commands:");
                foreach (var i in commands.Keys)
                {
                    Console.WriteLine($"{i} {commandsArgs[i]}");
                }
                Console.WriteLine("Stop");

                Console.WriteLine();
                Console.Write("Input command: ");
                command = Console.ReadLine();
                var splitCommand = command.Split(' ');
                try
                {
                    if (command.ToLower() != "stop" && !commands[splitCommand[0]](splitCommand[1..^0]))
                    {
                        Console.Write("Press enter to continue...");
                        Console.ReadLine();
                    }
                }
                catch
                {
                    Console.WriteLine("Can't recognize command");
                    Console.Write("Press enter to continue...");
                    Console.ReadLine();
                }
            } while (command.ToLower() != "stop");
        }
    }
}
