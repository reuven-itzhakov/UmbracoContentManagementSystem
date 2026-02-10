1. installed Umbraco using: dotnet new install Umbraco.Templates
2. created Umbraco project using: dotnet new umbraco -n UmbracoContentManagementSystem --development-database-type SQLite
3. The root issue is that Umbraco couldn’t open the SQLite database file, so boot failed. The warning came from SQLite error 14, which happens when the resolved DB path is invalid or the file/parent folder doesn’t exist or isn’t accessible.

In this project, the connection string uses |DataDirectory|/Umbraco.sqlite.db, but DataDirectory wasn’t reliably set early enough for Umbraco’s startup. That meant it was trying to open a DB file at a non‑existent or wrong location. The missing media folder was a second, separate failure that also crashed startup on the first run.

I addressed both by:

Creating the expected folders (Data and media) at startup.
Forcing the connection string to an absolute path so SQLite can always open the file.
If you still see the warning, the new console output will tell us the exact path Umbraco is attempting to use.

```csharp
tring dataDirectory = Path.Combine(contentRoot, "umbraco", "Data");
string mediaDirectory = Path.Combine(contentRoot, "wwwroot", "media");
string sqlitePath = Path.Combine(dataDirectory, "Umbraco.sqlite.db");

Directory.CreateDirectory(dataDirectory);
Directory.CreateDirectory(mediaDirectory);

if (!File.Exists(sqlitePath))
{
    using FileStream _ = File.Create(sqlitePath);
}

AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
builder.Configuration["ConnectionStrings:umbracoDbDSN"] =
    $"Data Source={sqlitePath};Cache=Shared;Foreign Keys=True;Pooling=True";
```

4. added templates in the backoffice
5. added Document Types in the backoffice
6. added page in the content tab and published it