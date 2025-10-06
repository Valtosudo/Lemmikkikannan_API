var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Taulut taulut = new Taulut();

app.MapGet("/", () => "Hello World!");


app.MapPost("/henkilot", (Ihmiset henkilo) =>
{
    taulut.LisaaHenkilo(henkilo.Nimi, henkilo.Puhelin);
    return Results.Ok("Henkilö lisätty!");
});

app.MapPost("/Lemmikit", (Elaimet Lemmikki) =>
{
    taulut.LisaaLemmikki(Lemmikki.Nimi, Lemmikki.Rotu, Lemmikki.OmistajaNimi);
    return Results.Ok("Lemmikki lisätty!");
});

app.MapGet("/puhelin/{lemmikinNimi}", (string lemmikinNimi) =>
{
    var numero = taulut.NaytaPuhelin(lemmikinNimi);
    return numero is null ? Results.NotFound("Omistajaa ei löytynyt")
    : Results.Ok(numero);
});

app.Run();
