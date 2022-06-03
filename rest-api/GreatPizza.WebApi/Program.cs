using GreatPizza.Infrastructure;
using GreatPizza.WebApi.Mappers;
using GreatPizza.WebApi.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
builder.Services.AddSwaggerGen(config => config.SwaggerDoc("v1", new() { Title = "Great Pizza", Version = "v1" }));
builder.Services.AddSingleton<ToppingMapper>();
builder.Services.AddSingleton<PizzaMapper>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "Great Pizza v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.UseMiddleware<ExceptionHandler>();
app.Run();
