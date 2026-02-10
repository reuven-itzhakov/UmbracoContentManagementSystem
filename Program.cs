// Application startup and Umbraco bootstrapping.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Ensure local SQLite and media folders exist before Umbraco boots.
string contentRoot = builder.Environment.ContentRootPath;
string dataDirectory = Path.Combine(contentRoot, "umbraco", "Data");
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

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
