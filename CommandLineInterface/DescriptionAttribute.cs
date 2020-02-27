using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CommandLineInterface
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = true)]
    public class DescriptionAttribute : Attribute
    {
        public CultureInfo Locale { get; }

        public string Description { get; }

        public DescriptionAttribute(string locale, string description)
        {
            Locale = CultureInfo.GetCultureInfo(locale);
            Description = description;
        }

        public DescriptionAttribute(string locale, params string[] descriptionLines)
            : this(locale, string.Join(Environment.NewLine, descriptionLines))
        {
            
        }
    }
}
