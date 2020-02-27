using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommandLineInterface
{
    
    public class CommandDefinition
    {
        public string Name { get; }

        public OptionsDefinition Options { get; }

        public MemberInfo Member { get; }

        public CommandDefinition(string name, OptionsDefinition options, MemberInfo member)
        {
            Name = name;
            Options = options;
            Member = member;
        }

        public override string ToString() => $"Command={Name}";
    }
}
