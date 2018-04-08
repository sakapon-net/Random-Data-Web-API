# Random Data Web API
Provides the JSON Web API to generate random data.  
This Web API supports CORS (Cross-Origin Resource Sharing).

Using PaaS is the simplest way to host this Web API.
For example, if you fork this repository, you can deploy directly the Web API to an Azure Web App by the Microsoft Azure Portal.
In this case, the continuous deployment is configured.

[日本語のドキュメント](docs)

## Random Data
- alphabets
- alphanumerics
- byte sequence
- UUID (GUID)
- time-ordered ID

## Web App
This project is actually the ASP.NET Web app that contains the following:
- Web API
- help page with specification
- test page using jQuery

[randomdata.azurewebsites.net](https://randomdata.azurewebsites.net/) is a sample deployment.

### Development Environment
- .NET Framework 4.5
- ASP.NET Web API 5.2.3
- ASP.NET Web API Help Page 5.2.3
- ASP.NET Web API Cross-Origin Support 5.2.3
- [Blaze 1.1.10](https://github.com/sakapon/Blaze)

### Release Notes
- **v1.0.0** The first release, using ASP.NET MVC.
- **v2.0.6** Use ASP.NET Web API.
