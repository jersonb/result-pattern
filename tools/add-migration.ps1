[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $name
)


dotnet ef migrations add $name -c StoreExampleDataContext -s ..\StoreExample.Api\StoreExample.Api.csproj -p .\StoreExample.Data\StoreExample.Data.csproj