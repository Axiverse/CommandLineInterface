using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineInterface.Tests
{
    [TestFixture]
    public class OptionsDefinitionTests
    {
        class WithCommands
        {
            [Command("InnerCommand1")]
            public object Command1;

            [Command("InnerCommand2")]
            public object Command2;
        }

        [Test]
        public void Parse_SucceedsWithCommands()
        {
            var definition = OptionsDefinition.GetDefinition<WithCommands>();

            var parsed = (WithCommands)definition.Parse("InnerCommand1");

            Assert.That(parsed.Command1, Is.Not.Null);
            Assert.That(parsed.Command2, Is.Null);
        }

        class WithNestedCommands
        {
            [Command("OuterCommand1")]
            public WithCommands Command1;

            [Command("OuterCommand2")]
            public WithCommands Command2;
        }

        [Test]
        public void Parse_SucceedsWithNestedCommands()
        {
            var definition = OptionsDefinition.GetDefinition<WithNestedCommands>();

            var parsed = (WithNestedCommands)definition.Parse("OuterCommand2", "InnerCommand1");

            Assert.That(parsed.Command1, Is.Null);
            Assert.That(parsed.Command2, Is.Not.Null);
            Assert.That(parsed.Command2.Command1, Is.Not.Null);
            Assert.That(parsed.Command2.Command2, Is.Null);
        }

        class WithParameters
        {
            [Parameter("v", "version")]
            public string Version;

            [Parameter("i", "include")]
            public string Include;
        }

        [Test]
        public void Parse_SucceedsWithParameters()
        {
            var definition = OptionsDefinition.GetDefinition<WithParameters>();

            var parsed = (WithParameters)definition.Parse("--version=hello", "--include", "sys.obj");

            Assert.That(parsed.Version, Is.EqualTo("hello"));
            Assert.That(parsed.Include, Is.EqualTo("sys.obj"));
        }

        class WithNestedParameters
        {
            [Command("OuterCommand1")]
            public WithParameters Command1;

            [Command("OuterCommand2")]
            public WithParameters Command2;
        }

        [Test]
        public void Parse_SucceedsWithNestedParameters()
        {
            var definition = OptionsDefinition.GetDefinition<WithNestedParameters>();

            var parsed = (WithNestedParameters)definition.Parse("OuterCommand2", "--version=hello", "--include", "sys.obj");

            Assert.That(parsed.Command1, Is.Null);
            Assert.That(parsed.Command2, Is.Not.Null);
            Assert.That(parsed.Command2.Version, Is.EqualTo("hello"));
            Assert.That(parsed.Command2.Include, Is.EqualTo("sys.obj"));
        }
    }
}
