using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineInterface
{
    public static class ArgumentParser
    {
        public static T Parse<T>(params string[] args)
            where T : new()
        {
            var parser = new ArgumentParser<T>();
            return parser.Parse(args);
        }
    }
}
