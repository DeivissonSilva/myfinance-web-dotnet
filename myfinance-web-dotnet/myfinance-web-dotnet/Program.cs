using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.App_Code_Clean.Core.DataAcess;
using myfinance_web_dotnet.App_Code_Clean.Core.Services;
using myfinance_web_dotnet.App_Code_Clean.Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPlanoContasServices, PlanoContasServices>();
builder.Services.AddScoped<IHistoricoTransacoesServices, HistoricoTransacoesServices>();

builder.Services.AddScoped<PlanoContasRepository>();
builder.Services.AddScoped<HistoricoTransacoesRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
