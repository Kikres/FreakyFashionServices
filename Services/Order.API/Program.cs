using Order.API.Mapper;
using Microsoft.EntityFrameworkCore;
using Order.API.Data;
using Order.API.Repository;
using Order.API.Service;
using Order.API.Client;

namespace Order.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(ApplicationMappings));

            builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Injection
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<OrderRepositroy>();
            builder.Services.AddScoped<CustomerRepositroy>();
            builder.Services.AddScoped<BasketCllient>();

            // HttpClientFactory preconfig injection
            builder.Services.AddHttpClient<BasketCllient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("BasketAPI"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}