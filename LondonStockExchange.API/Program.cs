using LondonStockExchange.API.Configuration;
using LondonStockExchange.API.Data;
using LondonStockExchange.API.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Application services
builder.Services.AddSignalR();
builder.Services.AddApplicationServices();

// MVC Controllers
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed the database with initial data in development environment
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            DataSeeder.SeedData(dbContext);
        }
        catch (Exception ex)
        {
            // Log exceptions that occur during the seeding process.
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
}

// HTTP request pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map controller actions to endpoints
app.MapControllers();
// Map SignalR hubs to endpoints
app.MapHub<TradeNotificationHub>("/tradenotificationhub");

app.Run();