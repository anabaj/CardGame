# CardGame

## Run CardGame from command line

Navigate to root folder:

- Start game with default game rules (40 cards, 2 players and suits not enabled):

	  dotnet run --project CardGame

- Start game with one or more specific game rules: 
	
	  dotnet run --project CardGame --size 80 --players 4 --suits true
	
	  dotnet run --project CardGame --suits true
 
	  dotnet run --project CardGame --size 80 --players 4

## Run Unit Tests from command line

Navigate to root folder:

	dotnet test CardGameTest --logger "console;verbosity=detailed"
