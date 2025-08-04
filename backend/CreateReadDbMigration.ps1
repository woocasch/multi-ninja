param (
	[Parameter(Mandatory=$true)]
	[String]$MigrationName
)

dotnet ef migrations add $MigrationName -p .\src\ReadsDatabase\ReadsDatabase.csproj -c MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore.ReadsContext
