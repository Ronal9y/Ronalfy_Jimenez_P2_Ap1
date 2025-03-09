global using Microsoft.EntityFrameworkCore;
using Ronalfy_Jimenez_P2_Ap1.Components;
using Ronalfy_Jimenez_P2_Ap1.DAL;
using Ronalfy_Jimenez_P2_Ap1.Services;
using Ronalfy_Jimenez_P2_Ap1.Models;

namespace Ronalfy_Jimenez_P2_Ap1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
        builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));

        builder.Services.AddBlazorBootstrap();

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
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
