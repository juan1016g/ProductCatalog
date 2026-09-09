using Application;
using Delamujer.ProductCatalog.Middlewares;
using Infrastructure;
using Microsoft.OpenApi;
using System.Reflection;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Catalog API",
        Version = "v1",
        Description = "Bienvenido a la API del Catálogo de Productos.\n\n" +
                      "Esta interfaz permite consultar y gestionar el inventario aplicando **Clean Architecture** y **CQRS**.\n\n" +
                      "[Ver repositorio en GitHub](https://github.com/juan1016g/ProductCatalog)"
    });

    // Cargar comentarios XML del proyecto WebAPI (Controladores)
    var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
    c.IncludeXmlComments(apiXmlPath);

    // Cargar comentarios XML del proyecto Application (DTOs y Commands)
    var appXmlFile = "Application.xml";
    var appXmlPath = Path.Combine(AppContext.BaseDirectory, appXmlFile);
    if (File.Exists(appXmlPath))
    {
        c.IncludeXmlComments(appXmlPath);
    }
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Registrar las dependencias de nuestras capas (Clean Architecture)
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

var app = builder.Build();

// Conectar el Middleware de Excepciones (Debe ir al inicio del pipeline)
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();