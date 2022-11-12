using Microsoft.EntityFrameworkCore;
using Montaniarzii.Code.ExtensionMethods;
using Montaniarzii.DataAccess;
using Montaniarzii.DataAccess.Context;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.WebApp.Code;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(BaseService).Assembly);
builder.Services.AddDbContext<MontaniarziiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information);
});
builder.Services.AddAuthentication("MontaniarziiCookies")
       .AddCookie("MontaniarziiCookies", options =>
       {
           options.AccessDeniedPath = new PathString("/Account/Login");
           options.LoginPath = new PathString("/Account/Login");
       });

builder.Services
    .AddScoped<UnitOfWork>();

builder.Services.AddMontaniarziiCurrentUser();
builder.Services.AddPresentation();
builder.Services.AddMontaniarziiBusinessLogic();

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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            @"C:\Users\strat\PozeProiect"),
        RequestPath = ""
    });

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("NotFound", "{*url}",
            new { controller = "CustomError", action = "Error404" });
app.Run();
