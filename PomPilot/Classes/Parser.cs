using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomPilot.Classes
{
    /*  Parser knows how to look at an entered string and recognize valid, potential commands. It doesn't know what do with 
        those commands (actions are handled elsewhere), but it knows a valid command when it sees it.
        
        In order for something to be handed off to other classes to potentially be executed, the Parser needs to know about the command.
    
        It does NOT know what modifiers are valid for commands; it will pass a list of potential modifier on as-is so they can be used elsewhere.
    */
    public class Parser
    {
        // Last valid command the parser was able to parse; null if there was no valid command in last input recieved to parsse
        public string LastCommand { get; private set; } = "";

        // List of any modifiers (unvalidated) that came w/ last valid command; null if none or if last command invalid
        public List<string> LastPotentialModifiers { get; private set; } = new List<string>();

        // Collection of strings representing valid commands based on application features -- will update frequently as new features are added
        private Dictionary<string, string> validCommands = new Dictionary<string, string> {
            { "exit", "exit" }, { "leave", "exit" }, { "close", "exit" }, { "quit","exit" }, // Exit Application
            { "start", "start" }, { "work", "start" }, { "go", "start" }, // Start Pomodoro
            { "status", "status" }, { "state", "status" }, // Get status of current timed session (Pomodoro or Break)
            { "stop", "stop" }, {"cancel", "stop"},

            // TODO - Implement additional "standard" Pomodoro commands
            // Amend Pomodoro
            // Pause Pomodoro
            // Repeat Last Pomodoro
            // Take a break
        };

        public bool TryParse(string input)
        {
            LastCommand = "";
            LastPotentialModifiers.Clear();
            bool isParsed = false;

            if (!String.IsNullOrEmpty(input))
            {
                input = input.ToLower();

                // TODO - Need to recognize items in quotes and not split - will be used for goals like "Finish this PR"
                // TODO - special characters allowed will be ""  -  --  and #    figure out a way to dump anything else that isn't valid
                List<string> inputAsList = input.Split(" ", (StringSplitOptions.RemoveEmptyEntries)).ToList();

                // Commands must be the first word of the input to be valid
                if (validCommands.ContainsKey(inputAsList[0]))
                {
                    // It's a valid command, set last command to value, remove from potential modifiers and publish to potential modifers
                    LastCommand = validCommands[inputAsList[0]];
                    inputAsList.RemoveAt(0);
                    LastPotentialModifiers = inputAsList.ToList();
                    isParsed = true;
                }
            }
            return isParsed;
        }

        // Print piped list of modifiers; empty string if none found
        public string PrintModifiers()
        {
            string output = "";
            if (LastPotentialModifiers.Count() > 0)
            {
                output += LastPotentialModifiers[0];
            }
            for (int i = 1; i < LastPotentialModifiers.Count(); i++)
            {
                output += "|" + LastPotentialModifiers[i];
            }
            return output;
        }
    }
}
