# VPX
Simple project for reading information and pass tests using:
 - ASP.NET Core WebAPI 3.x
 - EntityFramework Core 3.x
 - Core DI
 - Angular 8.x
 - Bootstrap
 
## Build 
- Restore NuGet packages 
- Create database using EntityFramework migration:
  - Change connection string:
    - `VPX.DataAccess.Context` > `AppDbFactory`
    - `VPX.Presentation.WebClient` > `appsettings.json`
  - Set `VPX.Presentation.WebClient` as default startup projcet
  - Open `Package Manager Console`
  - Set `VPX.DataAccess` as default project
  - run `Update-Database`
 
 ## Contributing
 * [Contributing Information](CONTRIBUTING.md)

 ## Code of Conduct
 * [Code of Conduct Information](CODE_OF_CONDUCT.md)
