var builder = WebApplication.CreateBuilder(args);

// Logging minimalista
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

Console.WriteLine("⚠️ App.Build() completado");

app.MapGet("/", () => "¡Backend funcionando!");
app.MapGet("/api/test", () => new { message = "Test OK", timestamp = DateTime.UtcNow });

Console.WriteLine("⚠️ MapGet completado");

app.Run();

Console.WriteLine("⚠️ App.Run() completado - esto no debería verse");
