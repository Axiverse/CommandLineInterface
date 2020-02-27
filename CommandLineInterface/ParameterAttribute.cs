using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineInterface
{
    public class ParameterAttribute : Attribute
    {
        public string ShortName { get; }

        public string LongName { get; }

        public bool? Required { get; }

        public ParameterAttribute(string shortName, string longName, bool required = false)
        {
            ShortName = shortName;
            LongName = longName;
            Required = required;
        }
    }
}
