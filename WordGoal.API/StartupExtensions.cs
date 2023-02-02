using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WordGoal.Data;

namespace WordGoal.API
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<WordGoalAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WordGoalAPIContext") 
                ?? throw new InvalidOperationException("Connection string 'WordGoalAPIContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers(configure =>
            {
                configure.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddXmlDataContractSerializerFormatters();

            builder.Services.AddAutoMapper(
                        AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IWordGoalRepository, WordGoalRepository>(); 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WordGoal API",
                    Description = "An ASP.NET Core Web API for managing projects, notes, and log entry items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            
            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<WordGoalAPIContext>();
                    if (context != null)
                    {
                        await context.Database.EnsureDeletedAsync();
                        await context.Database.MigrateAsync();
                        await SeedData.InitAsync(context);
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
        }
    }
}
