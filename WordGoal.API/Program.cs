using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WordGoal.API.Data;
using WordGoal.API.Profiles;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WordGoalAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WordGoalAPIContext") ?? throw new InvalidOperationException("Connection string 'WordGoalAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddAutoMapper(
            AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
