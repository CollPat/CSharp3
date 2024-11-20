using ToDoList.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();
//musi to byt *builder.Services.AddRazorComponents().AddInteractiveServerComponents();* jinak se nam to rozpadne kdyz to zkusime spustit :)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();
//musi to byt *app.MapRazorComponents<App>().AddInteractiveServerRenderMode();* jinak se nam to rozpadne kdyz to zkusime spustit :)

app.Run();
