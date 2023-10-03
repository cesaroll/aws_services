using Customers.Api.Config.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPostgreSql(builder.Configuration);

builder.Services.AddAllServices();

builder.Services.AddControllersWithValidation();

builder.Host.AddSerilog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddMiddleware();

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