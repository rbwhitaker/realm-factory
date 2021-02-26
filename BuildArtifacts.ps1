$version = $args[0];

if($version -eq $null) { $version = Read-Host "Version number"; }

dotnet pack -c Release "-p:PackageVersion=$version" .\RealmFactoryCore\RealmFactory.Core.csproj
mv -Force ".\RealmFactoryCore\bin\Release\RealmFactory.Core.$version.nupkg" .

dotnet build -c Release .\RealmFactoryContentManager\RealmFactory.ContentPipeline.csproj
nuget pack ./RealmFactoryContentManager/RealmFactory.ContentPipeline.nuspec -Version $version
