using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SharpDomain.Client
{
    public abstract class ClientCommandProvider : ICommandSendingProvider
    {
        private IReadOnlyCollection<ICommandInfo> _commands;
        private static readonly ConcurrentDictionary<Type, Func<string, object>> ConvertFuncCache = new ConcurrentDictionary<Type, Func<string, object>>();

        protected ClientCommandProvider()
        {
            InitializeCommandsCollection();
        }

        public IReadOnlyCollection<ICommandInfo> Commands { get { return _commands; } }

        private void InitializeCommandsCollection()
        {
            IList<ICommandInfo> commands = new List<ICommandInfo>();

            var type = this.GetType();
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var categoryAttr = type.GetCustomAttribute<CategoryAttribute>();
            var category = categoryAttr != null ? categoryAttr.Category : type.Name;

            foreach (var method in methods)
            {
                var cmd = CreateCommandInfoFromMethod(category, method);
                if (cmd != null)
                    commands.Add(cmd);
            }

            _commands = new ReadOnlyCollection<ICommandInfo>(commands);
        }

        private ICommandInfo CreateCommandInfoFromMethod(string category, MethodInfo method)
        {
            if (method.ReturnType != typeof(void) && method.ReturnType != typeof(RunCommandResult))
                return null;

            var name = method.Name;
            var shortDescription = GetShortDescription(method);
            var longDescription = GetLongDescription(method);
            var parameters = GetParameters(method);
            var executeFunc = GetExecuteFunc(method, parameters);

            var cmd = new CommandInfo(name, shortDescription, longDescription, category, executeFunc, parameters);
            return cmd;
        }

        private Func<string[], RunCommandResult> GetExecuteFunc(
            MethodInfo method,
            ICommandParameterInfo[] parameters)
        {
            Func<string[], object[]> convertFunc = values => 
                values.Select((v, i) => parameters[i].Convert(v)).ToArray();

            if (method.ReturnType == typeof (void))
            {
                return values =>
                {
                    method.Invoke(this, convertFunc(values));
                    return RunCommandResult.Continue;
                };
            }
            else
            {
                return values => (RunCommandResult)method.Invoke(this, convertFunc(values));
            }
        }

        private Func<string, object> GetConvertFunc(Type type)
        {
            return ConvertFuncCache.GetOrAdd(type, CreateConvertFunc(type));
        }

        private Func<string, object> CreateConvertFunc(Type type)
        {
            var converter = TypeDescriptor.GetConverter(type);
            Expression<Func<string, object>> expr = value => converter.ConvertFromString(value);
            var func = expr.Compile();
            return func;
        }

        private ICommandParameterInfo[] GetParameters(MethodInfo method)
        {
            var parameters = method.GetParameters().Select(GetParameter).ToArray();
            return parameters;
        }

        private ICommandParameterInfo GetParameter(ParameterInfo param)
        {
            var name = param.Name;
            var description = GetDescription(param);
            bool isOptional = false;
            var convertFunc = GetConvertFunc(param.ParameterType);

            var info = new CommandParameterInfo(name, description, isOptional, convertFunc);
            return info;
        }

        private string GetShortDescription(MethodInfo method)
        {
            var attr = method.GetCustomAttribute<DisplayNameAttribute>();
            if (attr != null)
                return attr.DisplayName;
            return null;
        }

        private string GetLongDescription(MethodInfo method)
        {
            var attr = method.GetCustomAttribute<DescriptionAttribute>();
            if (attr != null)
                return attr.Description;
            return null;
        }

        private string GetDescription(ParameterInfo parameter)
        {
            var attr = parameter.GetCustomAttribute<DescriptionAttribute>();
            if (attr != null)
                return attr.Description;
            return null;
        }

        abstract class CommandItemInfo : ICommandItemInfo
        {
            protected CommandItemInfo(string name, string description)
            {
                Name = name;
                Description = description;
            }

            public string Name { get; private set; }
            public string Description { get; private set; }
        }

        class CommandInfo : CommandItemInfo, ICommandInfo
        {
            private readonly Func<string[], RunCommandResult> _executeFunc;

            public CommandInfo(
                string name,
                string description,
                string longDescription,
                string category,
                Func<string[], RunCommandResult> executeFunc,
                IEnumerable<ICommandParameterInfo> parameters)
                : base(name, description)
            {
                _executeFunc = executeFunc;
                LongDescription = longDescription;
                Category = category;
                Parameters = new ReadOnlyCollection<ICommandParameterInfo>(parameters.ToList());
            }

            public string Category { get; private set; }
            public string LongDescription { get; private set; }
            public IReadOnlyCollection<ICommandParameterInfo> Parameters { get; private set; }
            public RunCommandResult Execute(string[] args)
            {
                return _executeFunc(args);
            }
        }

        class CommandParameterInfo : CommandItemInfo, ICommandParameterInfo
        {
            private readonly Func<string, object> _convertFunc;

            public CommandParameterInfo(
                string name,
                string description,
                bool isOptional,
                Func<string, object> convertFunc)
                : base(name, description)
            {
                _convertFunc = convertFunc;
                IsOptional = isOptional;
            }

            public bool IsOptional { get; private set; }
            public object Convert(string value)
            {
                return _convertFunc(value);
            }
        }
    }
}