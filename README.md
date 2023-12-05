# PomPilot
## Background
- Console appliction to manage and facilitate Pomodoro productivity sessions

## Valid Commands/Modifiers
- **start** - starts an active Pomodoro
	- **#** - sets the duration of the Pomodor to # minutes (will default to 25 if absent)
	- **$"x"** - starts music playlist with search term X
	- Example: start 20 $"steve miller band"
- **status** - displays the status of an active Pomodoro
- **stop** - stops an active Pomodoro
- **exit** - closes the application

## Roadmap
- Refactor parser / command logic
- Build alert for when timed sessions complete (i.e. flash icon in tray?)
- Config file
- Break functionality
- Tags/goals
- Rank session productivity / add comments
- File logging file File IO to .csv
- Visualization of past Pomodoro activity via Grafana or a JavaScript visualization library
- Pause timed sessions
- Amend/copy last session
- Alexa integration (music, timers)
- Run from command lined / any terminal
- Create installer
