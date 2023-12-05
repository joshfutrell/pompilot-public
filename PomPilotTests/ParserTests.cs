using PomPilot.Classes;
namespace PomPilotTests
{
    [TestClass]
    public class ParserTests
    {
        // Happy paths
        [DataRow("exit", true, "exit")]

        // Known failures
        [DataRow("exitx", false, "")]

        // Edge cases
        [DataRow("   ExiT", true, "exit")]
        [DataRow("exit ", true, "exit")]
        [DataRow(null, false, "")]
        [DataRow("", false, "")]

        [DataTestMethod]
        public void ParserFindsCommands(string input, bool expectedSuccess, string expectedCommand)
        {
            //Arrange
            Parser parser = new Parser();
            
            //Act
            bool parseSucceeded = parser.TryParse(input);
            string parseCommand = parser.LastCommand;
            List<string> parseModifiers = parser.LastPotentialModifiers;
            
            //Assess
            Assert.AreEqual(expectedSuccess, parseSucceeded, "A command should be found");
            Assert.AreEqual(expectedCommand, parseCommand, "The correct command should be returned");
        }


        // Happy paths
        [DataRow("start 20", true, "start", new string[] { "20" })]

        // Known failures
        [DataRow("startx 20", false, "", new string[0])]

        // Edge cases
        [DataRow("  start  one* two  - thr ee", true, "start", new string[] { "one*", "two", "-", "thr", "ee" })]

        [DataTestMethod]
        public void ParserFindsCommandsAndModifiers(string input, bool expectedSuccess, string expectedCommand, string[] expectedModifiers)
        {
            //Arrange
            Parser parser = new Parser();
            
            //Act
            bool parseSucceeded = parser.TryParse(input);
            string parseCommand = parser.LastCommand;
            List<string> parseModifiers = parser.LastPotentialModifiers;
            
            //Assert
            Assert.AreEqual(expectedSuccess, parseSucceeded, "A command should be found");
            Assert.AreEqual(expectedCommand, parseCommand, "The correct command should be returned");
            CollectionAssert.AreEqual(expectedModifiers, parseModifiers, "The correct modifiers should be returned");
        }

        //TODO Add test coverage of PrintModifiers(), though this is convenience method atm and might not be used long-term
    }
}