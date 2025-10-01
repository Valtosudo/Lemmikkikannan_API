var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Taulut taulut = new Taulut();

app.MapGet("/", () => "Hello World!");



app.Run();
