param (
	[Parameter(Mandatory=$true)]
	[String]$MigrationName
)

dotnet ef migrations add $MigrationName -p .\src\Infrastructure\Infrastructure.csproj -s .\src\WebApi\WebApi.csproj