using Application.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options => { options.LowercaseUrls = true; });

builder.Services.AddSession();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddApplication(builder.Configuration, builder.Environment);

var app = builder.Build();

await PersistenceDatabaseInitializer.Initialize(app.Services, app.Environment);

app.UseHsts();

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
