using System.Text.Json;
WebApplication app = WebApplication.Create();

app.Urls.Add("http://localhost:1337");
app.Urls.Add("http://*:1337");

List<Breads> breads = new();

breads.Add(new() { name = "Baguette", desc = "Cruncy long bread, sometimes soft inside", rating = 7 });
breads.Add(new() { name = "Lingonberrybread", desc = "Lingonberrytaste, soft but a bit hard sometimes", rating = 6 });
breads.Add(new() { name = "Tomatobread", desc = "Tomatotaste, soft", rating = 8 });

app.MapGet("/", () =>
{
    return "Add 'breads' in url";
});
app.MapGet("/breads/", () =>
{
    return (breads);
});
app.MapGet("/breads/{num}", (double num) =>
{
    if (num < breads.Count && num >= 0)
    {
        return Results.Ok(breads[(int)num]);
    }
    return Results.NotFound();
});
app.MapPost("/breads/", (Breads b) =>
{
    if (breads.Find(x => x.name == b.name) != null)
    {
        return Results.BadRequest();
    }

    breads.Add(b);
    Console.WriteLine("Added {b.name}");
    return Results.Ok();
});

app.Run();