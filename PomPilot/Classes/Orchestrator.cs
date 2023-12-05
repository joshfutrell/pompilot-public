using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomPilot.Classes
{
    //  Orchestrator takes a parsed, valid command and potential modifiers and decides what objects need to be created and actions need to be taken.
    //  Modifier logic will generally not live here and will be passed on to other classes to handle.
 
 
    public class Orchestrator
    {
        public string LastCommand { get; private set; } = "";
        public List<string> LastModifiers { get; private set; } = new List<string>(); 
        public bool wasLastCommandExecuted { get; private set; }
        public string LastResponseToConsole { get; private set; } = "";
        public Pomodoro ActivePomdoro { get; private set; }
        private Configuration Config { get; } = new Configuration();

        // TODO - make commands classes so I can use polymorphism and clean up this method below
        public bool TryCommand(string command, List<string> modifiers)
        {
            wasLastCommandExecuted = false;
            LastResponseToConsole = null;
            LastCommand = command;
            LastModifiers = modifiers.ToList();

            switch (LastCommand)
            {
                case "exit":
                    Environment.Exit(0);
                    break;
                case "start":
                    if (ActivePomdoro == null)
                    {
                        ActivePomdoro = new Pomodoro(LastModifiers);
                        LastResponseToConsole = $"Pomodoro for {ActivePomdoro.TargetDuration} created.";
                        wasLastCommandExecuted = true;
                    }
                    else
                    {
                        LastResponseToConsole = "A Pomodoro is already running. Can't create a new one.";
                    }
                    break;

                case "status":
                    if (ActivePomdoro != null)
                    {
                        LastResponseToConsole = $"You have {ActivePomdoro.TimeRemaining(DateTime.Now).ToString(@"hh\:mm\:ss")} left.";
                        wasLastCommandExecuted = true;
                    } 
                    else
                    {
                        LastResponseToConsole = "No session is actively running.";
                        wasLastCommandExecuted = true;
                    }
                    break;

                case "stop":
                    if (ActivePomdoro != null)
                    {
                        LastResponseToConsole = $"Your session will be stopped. There was {ActivePomdoro.TimeRemaining(DateTime.Now).ToString(@"hh\:mm\:ss")} left.";
                        ActivePomdoro = null;
                        wasLastCommandExecuted = true;
                    }
                    else
                    {
                        LastResponseToConsole = "No session is actively running.";
                        wasLastCommandExecuted = true;
                    }
                    break;
                default:
                    break;
            }
            return wasLastCommandExecuted;
        }
    }
}
