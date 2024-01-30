using LetsTravelCoolPlaces.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSingleton<GlobalExceptionsHandler>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<ITemperatureService, TemperatureService>();
// SERILOG
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionsHandler>();
app.UseAuthorization();

app.MapControllers();

app.Run();