using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineInterface
{
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }

        public DescriptionAttribute(params string[] descriptionLines)
            : this(string.Join(Environment.NewLine, descriptionLines))
        {

        }
    }
}
