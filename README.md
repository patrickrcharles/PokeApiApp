# PokeApiApp

A C# application that interacts with the PokeAPI — fetches Pokemon name, types, stats, and provides a console UI for determining what types
of Pokemon the entered Pokemon is strong and weak against.

To use the app, enter a Pokemon's name and the app will display the Pokemon's type and what types it is strong and weak against.
example output:

```
Enter Pokémon name or type 'exit' to quit

Enter Pokémon name: charizard

charizard : fire, flying
Strong Against: bug, steel, grass, ice, fire, fairy, fighting, ground
Weak Against: rock, fire, water, dragon, ground, steel, electric, ice
```

To exit the app, type 'exit' into the console.

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version matching your project, e.g. .NET 6 or .NET 7)  
- Internet connection (to call PokéAPI endpoints)  

# Setup — Clone & Build
Open a command line / terminal:

# 1. Clone the repo
```git clone https://github.com/patrickrcharles/PokeApiApp.git```

# 2. Switch to app folder
```cd PokeApiApp```

# 3. Restore dependencies
```dotnet restore```

# 4. Build the project (or solution, if present)
```dotnet build ```

# 5. Run the Application
```dotnet run --project src/PokeApiApp.csproj```

# 6. Switch to tests folder
```cd tests```

# 7. run tests
```dotnet test```

