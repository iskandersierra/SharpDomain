using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Common.Logging;

namespace SharpDomain.Client
{
    public class CommandSendingLoop
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private static readonly Regex ParamRegex = new Regex(@"(\""(?<param>[^\""]*)\"")|(\'(?<param>[^\""]*)\')|(?<param>[^\""\s]+)", RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private readonly Dictionary<string, ICommandInfo> ExactCommands;
        private readonly Dictionary<string, ICommandInfo[]> Commands;
        private static readonly IEqualityComparer<string> Comparer = StringComparer.InvariantCultureIgnoreCase;

        public CommandSendingLoop(IEnumerable<ICommandSendingProvider> commandProviders)
        {
            var allCommands = commandProviders.SelectMany(p => p.Commands).ToList();
            var groupedByExactName = allCommands
                .GroupBy(c => string.Format("{0}.{1}", c.Category, c.Name), Comparer)
                .ToDictionary(c => c.Key, c => c.ToList(), Comparer);
            var duplicateExactNames = groupedByExactName.Where(p => p.Value.Count > 1).ToArray();
            if (duplicateExactNames.Any())
            {
                foreach (var pair in duplicateExactNames)
                {
                    Log.ErrorFormat(string.Format("Ambiguous commands found: {0}", pair.Key));
                }
                throw new ConfigurationErrorsException("Duplicate commands found");
            }
            ExactCommands = groupedByExactName.ToDictionary(c => c.Key, c => c.Value.Single(), Comparer);

            Commands = allCommands
                .GroupBy(c => c.Name, Comparer)
                .ToDictionary(c => c.Key, c => c.ToArray(), Comparer);
        }

        public void Run()
        {
            Log.Info("Command sending loop started.");
            while (true)
            {
                Console.Write(">>> ");
                var line = Console.ReadLine();
                if (line == null) break;
                if (string.IsNullOrWhiteSpace(line)) continue;

                var values = ParamRegex.Matches(line).Cast<Match>().Select(e => e.Value).ToArray();
                if (values.Length == 0)
                {
                    Console.WriteLine("Input not recognized");
                    continue;
                }
                var commandName = values[0];
                values = values.Skip(1).ToArray();

                if (Comparer.Equals(commandName, "help"))
                {
                    PrintHelp(values);
                    continue;
                }

                var commands = FindCommands(commandName, values);
                ICommandInfo command;
                if (commands.Length == 1)
                {
                    command = commands[0];
                }
                else if (commands.Length == 0)
                {
                    Console.WriteLine("There is no command found for name {0}", commandName);
                    continue;
                }
                else
                {
                    Console.WriteLine("Command is ambiguous. Which are you referring to?");
                    for (int i = 0; i < commands.Length; i++)
                    {
                        Console.WriteLine("{0}.- {1}.{2} : {3}", i+1, commands[i].Category, commands[i].Name, commands[i].Description);
                    }
                    Console.Write("Enter a command number (or other than number to skip): ");
                    var numberLine = Console.ReadLine();
                    if (numberLine == null) break;
                    int number;
                    if (!int.TryParse(numberLine, out number)) continue;
                    if (number < 1 || number > commands.Length) continue;
                    command = commands[number];
                }

                try
                {
                    var result = command.Execute(values);
                    if (result == RunCommandResult.BreakLoop) break;
                    //if (result == RunCommandResult.Continue) continue;
                }
                catch (Exception ex)
                {
                    while (ex is TargetInvocationException && ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    Log.Error(ex.Message, ex);
                }
            }

            Log.Info("Command sending loop finished.");
        }

        private void PrintHelp(string[] values)
        {
            if (values == null || values.Length == 0)
            {
                foreach (var pair in ExactCommands.OrderBy(e => e.Key))
                {
                    Console.WriteLine("{0} - {1}" + "", pair.Key, pair.Value.LongDescription);
                }
            }
            else
            {
                foreach (var pair in ExactCommands.Where(e => values.Any(w => e.Key.ToLower().Contains(w.ToLower()))).OrderBy(e => e.Key))
                {
                    Console.WriteLine("{0} - {1}" + "", pair.Key, pair.Value.LongDescription);
                    foreach (var parameter in pair.Value.Parameters)
                    {
                        Console.WriteLine("    {0}: {1}", parameter.Name, parameter.Description);
                    }
                }
            }
        }

        private ICommandInfo[] FindCommands(string commandName, string[] values)
        {
            ICommandInfo commandInfo;
            if (ExactCommands.TryGetValue(commandName, out commandInfo))
            {
                if (commandInfo.Parameters.Count != values.Length)
                    return new ICommandInfo[0];
                return new[] {commandInfo,};
            }

            ICommandInfo[] result;
            if (Commands.TryGetValue(commandName, out result))
            {
                result = result.Where(c => c.Parameters.Count == values.Length).ToArray();
                return result;
            }

            return new ICommandInfo[0];
        }
    }
}
