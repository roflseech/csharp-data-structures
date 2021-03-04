using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    class LinkedListTest
    {
        static Dictionary<string, Func<string[], bool>> InitCommands(LinkedList<string> list, out Dictionary<string, string> commandsDescriptions)
        {
            var result = new Dictionary<string, Func<string[], bool>>(StringComparer.OrdinalIgnoreCase);
            commandsDescriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            result.Add("AddLast", (string[] s) => {
                if (s.Length == 0)
                {
                    Console.WriteLine("Not enough arguments");
                    return false;
                }
                try
                {
                    foreach (var a in s) list.AddLast(a);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return true;
            });
            commandsDescriptions.Add("AddLast", "[value1] [value2]...");

            result.Add("AddFirst", (string[] s) => {
                if (s.Length == 0)
                {
                    Console.WriteLine("Not enough arguments");
                    return false;
                }
                try
                {
                    for (int i = 1; i <= s.Length; i++) list.AddFirst(s[^i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return true;
            });
            commandsDescriptions.Add("AddFirst", "[value1] [value2]...");

            result.Add("AddAt", (string[] s) => {
                if (s.Length < 2)
                {
                    Console.WriteLine("Wrong arguments count");
                    return false;
                }
                int index;
                if (!Int32.TryParse(s[0], out index))
                {
                    Console.WriteLine($"Wrong index {index}");
                    return false;
                }

                try
                {
                    for (int i = 1; i <= s.Length - 1; i++) list.AddAt(index, s[^i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return true;
            });
            commandsDescriptions.Add("AddAt", "[index] [value1] [value2]...");

            result.Add("RemoveAt", (string[] s) => {
                if (s.Length != 1)
                {
                    Console.WriteLine("Wrong arguments count");
                    return false;
                }
                int index;
                if (!Int32.TryParse(s[0], out index))
                {
                    Console.WriteLine("Wrong Argument");
                    return false;
                }
                try
                {
                    list.RemoveAt(index);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return true;
            });
            commandsDescriptions.Add("RemoveAt", "[index]");
            return result;
        }
        public static void Run()
        {
            LinkedList<string> list = new LinkedList<string>();
            Dictionary<string, string> commandsArgs;
            var commands = InitCommands(list, out commandsArgs);
            string command;
            do
            {
                Console.Clear();
                Console.WriteLine("List:");
                list.PrintList();
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
