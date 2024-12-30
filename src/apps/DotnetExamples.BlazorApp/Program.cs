using System.Reflection;
using Microsoft.AspNetCore.DataProtection;

namespace DotnetExamples.BlazorApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDataProtection().SetApplicationName(Assembly.GetCallingAssembly().FullName!);
        
        builder.Services
            .AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            })
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<Components.App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}