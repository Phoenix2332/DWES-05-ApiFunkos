using System.Text;
using ApiFunkos.Config;
using ApiFunkos.Entity;
using ApiFunkos.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static System.Console;

OutputEncoding = Encoding.UTF8;
Title = "ApiFunkos";

try {
    var builder = WebApplication.CreateBuilder(args);
    FunkoConfig.Init(args);

    builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

    builder.Services.AddControllers();

    if (FunkoConfig.RepositoryName.Equals("SQLite", StringComparison.OrdinalIgnoreCase)) {
        Directory.CreateDirectory(FunkoConfig.DataFolder);
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(FunkoConfig.DbConnection));
    }
    else {
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(FunkoConfig.DbConnection));
    }

    builder.Services.AddDependencies();

    var app = builder.Build();

    await InitializeDatabaseAsync(app.Services);

    MostrarCabecera();

    app.UseHttpsRedirection();
    app.MapControllers();

    await app.RunAsync();
}
catch (Exception) {
    // ignored
}

return;

static async Task InitializeDatabaseAsync(IServiceProvider provider) {
    using var scope = provider.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

static void MostrarCabecera() {
    WriteLine();
    WriteLine("┌────────────────────────────────────────────────┐");
    WriteLine("│         ApiFunkos - Servidor Web Activo        │");
    WriteLine("└────────────────────────────────────────────────┘");
    WriteLine();
    WriteLine($" Entorno / API:  {FunkoConfig.ApiName}");
    WriteLine($" Repositorio:    {FunkoConfig.RepositoryName}");
    WriteLine($" Ruta Datos:     {FunkoConfig.DataFolder}");
    WriteLine();
}