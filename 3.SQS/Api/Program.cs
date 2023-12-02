using Api.Config;
using App.DI;
using Messenger.SQS.Config;
using Persistence.PG.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(new PgSettings() { ConnectionString = builder.Configuration.GetConnectionString(PgSettings.Key)! });
builder.Services.AddPostgresql();


builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection(QueueSettings.Key));
builder.Services.AddSqsMessenger();

builder.Services.AddAppServices();
builder.Services.AddMiddlewareServices();

builder.Services.AddApiControllers();

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
