using System.Text.Json.Serialization;
using ApiFunkos.Factories;
using ApiFunkos.Repositories;
using ApiFunkos.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );
});

builder.Services.AddSingleton<IFunkoRepository, FunkoRepository>();

var app = builder.Build();

var repository = app.Services.GetRequiredService<IFunkoRepository>();

await FunkoFactory.Seed(repository);

app.MapFunkoRoutes();

app.Run();