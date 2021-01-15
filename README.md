

dotnet migration add
dotnet ef dotnet ef --startup-project src/server/Api/Api.csproj  migrations add addTunnelEntity -p src/server/Infrastructure/Infrastructure.csproj --verbose 


dotnet ef database update --startup-project src/server/Api/Api.csproj --project src/server/Infrastructure/Infrastructure.csproj