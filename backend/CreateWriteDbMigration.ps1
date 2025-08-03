param (
	[Parameter(Mandatory=$true)]
	[String]$MigrationName
)

dotnet ef migrations add $MigrationName -p .\src\WritesDatabase\WritesDatabase.csproj -c MultiNinja.Backend.Infrastructure.WritesRepository.EfCore.WriteContext
