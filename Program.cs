using System.Diagnostics;
using Honeycomb.OpenTelemetry;
using honeycomb_odd.Data;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SchoolContextSQLite")));

// --- OpenTelemetry Code
var options = builder.Configuration.GetSection("Honeycomb").Get<HoneycombOptions>();
builder.Services.AddOpenTelemetryTracing(o => {
    o
    .AddSource(ActivityHelper.SourceName)
    .AddAspNetCoreInstrumentation()
    .AddEntityFrameworkCoreInstrumentation(o => {
        o.SetDbStatementForStoredProcedure = true;
        o.SetDbStatementForText = true;
    })
    .AddHoneycomb(options);
});
// -- OpenTelemetry Code

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SchoolContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

public class ActivityHelper
{
    public const string SourceName = "odd-demo";
    public static ActivitySource Source = new ActivitySource(SourceName);
}