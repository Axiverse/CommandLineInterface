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
    }
}
