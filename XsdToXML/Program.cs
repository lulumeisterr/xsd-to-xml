using br.com.dev.xsd.Application.Configuration;
using br.com.dev.xsd.Application.Services.XsdServices.Interface;
using br.com.dev.xsd.Services.XsdServices.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// Adicione os serviços MVC
builder.Services.AddControllers().AddJsonOptions(x => { x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Classes de negocio
builder.Services.AddScoped<IReadXSDService, ReadXSDService>();
var app = builder.Build();

app.UseRouting();
app.UseTokenValidationMiddleware("49DE246A-26E0-425D-A147-E62EDAA8D96C");
app.MapControllers();
app.UseSwaggerUI();
app.UseSwagger(x => x.SerializeAsV2 = true);
app.Run();