using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineInterface
{
    /// <summary>
    /// Represents an argument parser. If a command implements <see cref="IArgumentParser"/> then
    /// it will be given responsibility to parse its own values.
    /// </summary>
    public interface IArgumentParser
    {
        void Parse(params string[] args);
    }
}
