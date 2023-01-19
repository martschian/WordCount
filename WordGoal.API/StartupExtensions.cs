using Microsoft.EntityFrameworkCore;
using WordGoal.API.Data;

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

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddAutoMapper(
                        AppDomain.CurrentDomain.GetAssemblies());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
    }
}
