using System;
using System.Reflection;
using CommandLineInterface;

namespace NestedCommands
{
    class Program
    {
        [Command(nameof(Branch))]
        [Description("en-US", "Hello world")]
        public Branch Branch;

        [Command(nameof(Commit))]
        public Commit Commit;

        [Parameter("v", "version")]
        public string Version;

        static void Main(string[] args)
        {
            var program = ArgumentParser.Parse<Program>(args);
            if (!string.IsNullOrEmpty(program.Version))
            {
                Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Version);
            }
        }
    }
}
