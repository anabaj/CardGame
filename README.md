# CardGame

### CardGame is .NET Core console application written in C# using Visual Studio 2019

### Compile and run CardGame from command line

Navigate to root folder:

- Start game with default game rules (40 cards, 2 players and suits not enabled):

	  dotnet run --project CardGame

- Start game with one or more specific game rules. If suits are not enabled, omit --suit argument. If enabled suits are added in order from weakest to strongest. 
	
	  dotnet run --project CardGame --size 52 --players 4 --suits spade diamond heart club
	
	  dotnet run --project CardGame --suits spade diamond heart club
 
	  dotnet run --project CardGame --size 80 --players 4

### Compile and run Unit Tests from command line

Navigate to root folder:

	dotnet test CardGameTest --logger "console;verbosity=detailed"
