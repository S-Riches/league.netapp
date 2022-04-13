
using League.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// adds service that manages https clients so that there isnt a shortage of ports
builder.Services.AddHttpClient();
// tells the application to register the service, implements iriotservice, the object that i creates inherits the methods and anything defined in the interface.
builder.Services.AddScoped<IRiotService, RiotService>();

builder.Services.AddControllers();
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
