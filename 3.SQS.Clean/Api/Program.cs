using Api.DI;
using App.DI;
using Persistence.PG.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPostgresql(options => {
    options.ConnectionString = builder.Configuration.GetConnectionString("Customers");
});

builder.Services.AddAppServices();

builder.Services.AddControllers();

builder.Host.AddSerilog();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
