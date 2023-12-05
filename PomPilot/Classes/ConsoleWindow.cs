using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomPilot.Classes
{
    // The ConsoleWindow will handle all aspects of user input and user-facing, non-file output, console settings, styling.
    public class ConsoleWindow
    {
        public void Run()
        {
            configureConsole();
            
            // Get a tomato prompt
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            const string POM = "\uD83C\uDF45: ";

            Parser parser = new Parser();
            Orchestrator orchestrator = new Orchestrator();

            do
            {
                Console.Write("$ ");
                string inputToParse = Console.ReadLine();
                if (parser.TryParse(inputToParse))
                {
                    // TODO - remove ack completely once things are flushed out
#if DEBUG
                    Console.WriteLine($"{POM}Heard command \"{parser.LastCommand}\"" +
                    ((parser.LastPotentialModifiers.Count() > 0) ? $" with modifiers \"{parser.PrintModifiers()}\"" : ""));
#endif
                    orchestrator.TryCommand(parser.LastCommand, parser.LastPotentialModifiers);

                    if (orchestrator.LastResponseToConsole != null)
                    {
                        Console.WriteLine($"{POM}{orchestrator.LastResponseToConsole}\n");
                    }
                }
                else
                {
                    // Didn't find a valid command
                    // FUTURE - add logging and more intelligent suggestions
                    Console.WriteLine($"{POM}I didn't understand \"{inputToParse}\". Please try again.\n");
                }
            } while (true);
        }

        private void configureConsole()
        {
            Console.Title = "PomPilot";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
        }
    }
}
