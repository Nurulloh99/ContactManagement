
using ContactSystem.Server.Configurations;
using ContactSystem.Server.Endpoints;
using ContactSystem.Server.Middlewares;

namespace ContactSystem.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.ConfigureSerilog();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            builder.Configuration();
            builder.Services.ConfigureDependecies();






            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            //app.MapControllers();
            app.MapUserEndpoints();
            app.MapContactEndpoints();

            app.Run();
        }
    }
}
