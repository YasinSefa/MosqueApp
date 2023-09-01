var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// CORS ayarları sadece belirli IP adresine izin veriyor
app.Use((context, next) =>
{
    if (context.Request.Headers["Referer"].ToString().Contains("http://172.16.108.185/arayuz_iki"))
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    }
    return next();
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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