# PokeApiApp

A C# application that interacts with the PokéAPI — fetches Pokémon name, types, stats, and provides a console UI for determining what types
of Pokemon the entered Pokemon is strong and weak against.

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version matching your project, e.g. .NET 6 or .NET 7)  
- Internet connection (to call PokéAPI endpoints)  

# Setup — Clone & Build
Open a command line / terminal:

# 1. Clone the repo
git clone https://github.com/patrickrcharles/PokeApiApp.git
cd PokeApiApp

# 2. Restore dependencies
dotnet restore

# 3. Build the project (or solution, if present)
dotnet build 

# 4. Run the Application
dotnet run --project src/PokeApiApp.csproj

# 5 Run Tests
cd tests
dotnet test

