using Microsoft.EntityFrameworkCore;
using WordGoal.API;
using WordGoal.Data;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

await app.ResetDatabaseAsync();

app.Run();

