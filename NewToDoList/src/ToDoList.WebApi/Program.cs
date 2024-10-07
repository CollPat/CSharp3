var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello world!");

app.MapGet("/NewPath", () => "Zdravím svet!");

app.Run();
