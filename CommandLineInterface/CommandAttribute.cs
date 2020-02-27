using System;

namespace CommandLineInterface
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public string Name { get; }

        public CommandAttribute(string name)
        {
            Name = name;
        }
    }
}
