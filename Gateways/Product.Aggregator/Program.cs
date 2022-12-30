using Product.Aggregator.Client;
using Product.Aggregator.Service;

namespace Product.Aggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Injection
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<CatalogClient>();
            builder.Services.AddScoped<StockClient>();

            // HttpClientFactory preconfig injection
            builder.Services.AddHttpClient<CatalogClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("CatalogAPI"));
            });
            builder.Services.AddHttpClient<StockClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("StockAPI"));
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