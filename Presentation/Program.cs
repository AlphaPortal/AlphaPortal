using Presentation.Middlewares;
using Authentication.Extensions;
using Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddLocalAuthentication(builder.Configuration.GetConnectionString("UsersDatabase")!);
builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();

app.UseRootRedirect("/admin/dashboard");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultRoles();
app.UseDefaultAdminAccount();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
