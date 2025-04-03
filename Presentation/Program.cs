using Presentation.Middlewares;
using Authentication.Extensions;
using Data.Extensions;
using Business.Extensions;
using Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddLocalAuthentication(builder.Configuration.GetConnectionString("UsersDatabase")!);
builder.Services.AddDataContext(builder.Configuration.GetConnectionString("AlphaPortalDatabase")!);
//builder.Services.AddLocalIdentity(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

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

app.MapHub<NotificationHub>("/notificationHub");

app.Run();
