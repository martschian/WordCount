using Microsoft.EntityFrameworkCore;
using WordGoal.API;
using WordGoal.Data;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WordGoalAPIContext>();

    db.Database.EnsureDeleted();
    db.Database.Migrate();

    try
    {
        await SeedData.InitAsync(db);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        throw;
    }
}

app.Run();

