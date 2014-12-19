using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Messaging;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSMQConsoleSnoop
{
    class Program
    {
        private static CommandsClass Commands;
        static void Main(string[] args)
        {
            Console.WriteLine(@"Type ""help"" for a list of commands");
            Console.WriteLine(@"Type ""help command"" for help about a command");
            Console.WriteLine(@"Type ""exit"" to finish command processing");

            Commands = new CommandsClass();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                var line = Console.ReadLine();
                if (line == null) break;
                line = line.Trim();
                if (line == "") continue;
                var cmd = line.Contains(" ") ? line.Substring(0, line.IndexOf(' ')) : line;
                var parameters = line.Substring(cmd.Length).Trim();

                var action = InvokeCommand(cmd, parameters);

                switch (action)
                {
                    case CommandLoopAction.Continue:
                        continue;
                    case CommandLoopAction.Exit:
                        return;
                }
            }
        }

        private static CommandLoopAction InvokeCommand(string commandName, string parameters)
        {
            var type = typeof (CommandsClass);
            var method = type.GetMethod(commandName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                if (method.ReturnType == typeof(void) || method.ReturnType == typeof(CommandLoopAction))
                    if (method.GetParameters().Length == 0 ||
                        method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof (string))
                    {
                        var result = method.Invoke(Commands,
                            method.GetParameters().Length == 0 ? new object[0] : new object[] {parameters});
                        if (method.ReturnType == typeof (void))
                            return CommandLoopAction.Continue;
                        return (CommandLoopAction) result;
                    }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Unknown command: {0}", commandName);
                
            return CommandLoopAction.Continue;
        }
    }

    public class CommandsClass
    {
        [Description("Show list of all available commands")]
        public void help(string command = null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var type = typeof(CommandsClass);
            if (string.IsNullOrEmpty(command))
            {
                var methods = type.GetMethods(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var method in methods.OrderBy(m => m.Name.ToLower()))
                {
                    Console.Write("► {0}", method.Name);
                    if (method.GetParameters().Length == 1)
                    {
                        var p = method.GetParameters()[0];
                        if (p.IsOptional)
                            Console.Write(" [{0}]", method.GetParameters()[0].Name);
                        else
                            Console.Write(" {0}", method.GetParameters()[0].Name);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                var method = type.GetMethod(command, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (method != null)
                {
                    if (method.ReturnType == typeof(void) || method.ReturnType == typeof(CommandLoopAction))
                        if (method.GetParameters().Length == 0 ||
                            method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(string))
                        {
                            var descAttr = method.GetCustomAttribute<DescriptionAttribute>(true);
                            if (descAttr != null)
                                Console.WriteLine(descAttr.Description);
                            else
                                Console.WriteLine("No description available for command {0}", command);
                        }
                }
            }
        }

        [Description("Exits this application")]
        public CommandLoopAction exit()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return CommandLoopAction.Exit;
        }
        [Description("Exits this application")]
        public CommandLoopAction x()
        {
            return exit();
        }

        #region [ MSMQ ]

        [Description("List all private MS message queues on current machine")]
        public void msmql()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            var queues = MessageQueue.GetPrivateQueuesByMachine(".");
            foreach (var queue in queues.OrderBy(e => e.Path))
            {
                Console.WriteLine(queue.QueueName);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Total number of private queues: {0}", queues.Length);
        }

        [Description("Delete all private MS message queues on current machine")]
        public void msmqd()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            var queues = MessageQueue.GetPrivateQueuesByMachine(".");
            foreach (var queue in queues.OrderBy(e => e.Path))
            {
                Console.Write("Deleting {0} ... ", queue.QueueName);
                MessageQueue.Delete(".\\" + queue.QueueName);
                Console.WriteLine("ok!");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Total number of deleted private queues: {0}", queues.Length);
        }

        [Description("Creates a private MS message queues on current machine")]
        public void msmqc(string queueName)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            var path = @".\Private$\" + queueName;
            if (MessageQueue.Exists(path))
            {
                Console.WriteLine("Queue {0} already exist", path);
            }
            else
            {
                Console.Write("Creating queue {0} ... ", path);
                MessageQueue.Create(path);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ok!");
            }
        }
        #endregion [ MSMQ ]
    }

    public enum CommandLoopAction
    {
        Continue,
        Exit,
    }
}
