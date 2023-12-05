using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PomPilot.Classes
{
    public class Pomodoro : TimedSession
    {
        private Configuration Config { get; set; } = new Configuration();

        public Pomodoro(List<string> potentialModifiers)
        {
            if (potentialModifiers == null || potentialModifiers.Count == 0)
            {
                Start(Config.DefaultPomodoroDuration);
            }
            else
            {
                parseDuration(potentialModifiers);
                Start(this.Config.CustomPomodoroDuration);
                parseMusic(potentialModifiers);
            }
        }

        // TODO - add more valid formats like XmYs or X:XX
        // TODO - migrate to parser and determine how best to share results (create Duration class?)
        private void parseDuration(List<string> potentialModifers)
        {
            TimeSpan output = this.Config.DefaultPomodoroDuration;
            foreach (string s in potentialModifers)
            {
                if (int.TryParse(s, out int result))
                {
                    output = new TimeSpan(0, result, 0);
                    break;
                }
            }
            this.Config.CustomPomodoroDuration = output;
        }

        // TODO - migrate to parser and determine how best to share results (create Music class?)
        private void parseMusic(List<string> potentialModifers)
        {
            if (potentialModifers != null && potentialModifers.Count > 0)
                {
                string modifiers = String.Join(" ", potentialModifers);
                MatchCollection musicString = Regex.Matches(modifiers, "\\$\\\"(.*?)\\\"");
                
                if (musicString != null && musicString.Count > 0) {
                    string url = createMusicUrl(musicString[0].ToString());
                    openUrl(url);
                }
            }
        }

        private string createMusicUrl(string musicUrl)
        {
            musicUrl = musicUrl.Substring(2, musicUrl.Length - 3);
            musicUrl = musicUrl.Replace(" ", "+");
            musicUrl = $"https://music.amazon.com/search/{musicUrl}/playlists";
            return musicUrl;
        }

        private void openUrl(string url)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw new IOException();
                }
            }
            catch
            {
                // TODO - add better error handling to message back that music could not be started
            }
        }
    }
}
