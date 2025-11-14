var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.WebHost.UseUrls("http://localhost:5216");

Taulut taulut = new Taulut();

app.MapGet("/", () => "Hello World!");


app.MapPost("/henkilot", (Ihmiset henkilo) =>
{
    taulut.LisaaHenkilo(henkilo.Nimi, henkilo.Puhelin, henkilo.Id);
    return Results.Ok("Henkilö lisätty!");
});

app.MapPost("/lemmikit", (Elaimet Lemmikki) =>
{
    taulut.LisaaLemmikki(Lemmikki.Nimi, Lemmikki.Rotu, Lemmikki.OmistajaId);
    return Results.Ok("Lemmikki lisätty!");
});

app.MapGet("/puhelin/{lemmikinNimi}", (string lemmikinNimi) =>
{
    var numero = taulut.NaytaPuhelin(lemmikinNimi);
    return numero is null ? Results.NotFound("Omistajaa ei löytynyt")
    : Results.Ok(numero);
});



app.Run();
