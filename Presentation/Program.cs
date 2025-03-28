using Presentation.Middlewares;
using Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddLocalAuthentication(builder.Configuration.GetConnectionString("UsersDatabase")!);

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
