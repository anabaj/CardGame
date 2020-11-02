# CardGame

### CardGame is .NET Core 3.1 console application written in C# using Visual Studio 2019

### To check if .NET Core 3.1 SDK is installed:
	dotnet --list-sdks

### To install .NET Core 3.1 SDK
	https://dotnet.microsoft.com/download

### Compile and run CardGame from command line

Navigate to root folder (folder containing CardGame.sln solution):

- Start game with default game rules (40 cards, 2 players and suits not enabled):

	  dotnet run --project CardGame

- Start game with one or more specific game rules. 
- If suits are not enabled, omit --suit argument. If enabled suits are added in order from weakest to strongest. 
- Default rules: 40 cards, 2 players and no suits.
	
	  dotnet run --project CardGame --size 52 --players 4 --suits spade diamond heart club
	
	  dotnet run --project CardGame --suits spade diamond heart club
 
	  dotnet run --project CardGame --size 80 --players 4

	  dotnet run --project CardGame

### Compile and run Unit Tests from command line

Navigate to root folder:

	dotnet test CardGameTest --logger "console;verbosity=detailed"
