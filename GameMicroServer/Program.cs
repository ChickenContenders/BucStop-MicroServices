using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
builder.Services.AddHttpClient<Micro.PongClient>(client =>
{
    var baseAddress = new Uri(configuration.GetValue<string>("Pong"));

    client.BaseAddress = baseAddress;
});

builder.Services.AddHttpClient<Micro.TetrisClient>(client =>
{
    // "Tetris" is the name of the route on the appsettings.json file
    var baseAddress = new Uri(configuration.GetValue<string>("Tetris"));

    client.BaseAddress = baseAddress;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
