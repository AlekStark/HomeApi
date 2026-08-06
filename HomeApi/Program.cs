using HomeApi.Configuration;
using Microsoft.OpenApi;

namespace HomeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("HomeOptions.json", optional: true, reloadOnChange: true);
            builder.Services.Configure<HomeOptions>(builder.Configuration.GetSection("HomeOptions"));
            //builder.Configuration.AddJsonFile("HomeOptions.json", optional: false, reloadOnChange: true);
            // Add services to the container.

            builder.Services.AddControllers(); 
            
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeAPI", Version = "v1" });
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            //var config = app.Services.GetRequiredService<IConfiguration>();
            /*private IConfiguration Configuration
            { get; } = new ConfigurationBuilder()
            .AddJsonFile("HomeOptions.json")
            .Build();*/

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1");
                    // Раскомментируйте, чтобы убрать /swagger из URL:
                    // options.RoutePrefix = string.Empty; 
                });
                app.MapOpenApi();

            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
