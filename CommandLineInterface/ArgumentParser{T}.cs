using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommandLineInterface
{
    public class ArgumentParser<T>
        where T : new()
    {
        /// <summary>
        /// Gets the <see cref="OptionsDefinition"/> for the base argument type <see cref="T" />.
        /// </summary>
        public OptionsDefinition Definition { get; }

        public ArgumentParser()
        {
            Definition = OptionsDefinition.GetDefinition<T>();
        }

        public T Parse(params string[] args)
        {
            return (T)Definition.Parse(args);
        }
    }
}
