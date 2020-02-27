using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandLineInterface
{
    public class OptionsDefinition
    {
        public Type Type { get; }

        public List<CommandDefinition> Commands { get; } = new List<CommandDefinition>();

        public List<object> PositionalParameters { get; } = new List<object>();

        public List<object> NamedParameters { get; } = new List<object>();

        public OptionsDefinition(Type type)
        {
            Type = type;
        }

        public object CreateInstance() => Activator.CreateInstance(Type);

        public object Parse(params string[] args)
        {
            var instance = CreateInstance();

            // Search Commands
            if (args.Length > 0)
            {
                foreach (var command in Commands)
                {
                    if (args[0] == command.Name)
                    {
                        var c = command.Options.Parse(args.Skip(1).ToArray());
                        SetValue(instance, c, command.Member);
                    }
                }

            }

            return instance;
        }

        private void SetValue(object obj, object value, MemberInfo member)
        {
            if (member is PropertyInfo property)
            {
                property.SetValue(obj, value);
            }

            if (member is FieldInfo field)
            {
                field.SetValue(obj, value);
            }
        }

        public static OptionsDefinition GetDefinition<T>() => GetDefinition(typeof(T));

        public static OptionsDefinition GetDefinition(Type type)
        {
            var arguments = new OptionsDefinition(type);

            // Ensure that the type has a default constructor.
            if (type.GetConstructor(Array.Empty<Type>()) == null)
            {
                throw new Exception("Parse types must have a default constructor.");
            }

            // Get commands
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (TryGetCommand(field, out var command))
                {
                    var options = GetDefinition(field.FieldType);
                    arguments.Commands.Add(new CommandDefinition(command.Name, options, field));
                }
            }


            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (TryGetCommand(property, out var command))
                {
                    var options = GetDefinition(property.PropertyType);
                    arguments.Commands.Add(new CommandDefinition(command.Name, options, property));
                }
            }

            return arguments;
        }

        private static bool TryGetCommand(MemberInfo info, out CommandAttribute attribute)
        {
            attribute = info.GetCustomAttribute<CommandAttribute>();
            return attribute != null;
        }
    }
}
