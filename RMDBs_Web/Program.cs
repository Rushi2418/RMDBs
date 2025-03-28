using Microsoft.Extensions.DependencyInjection;
using RMDBs_Web;
using RMDBs_Web.Services;
using RMDBs_Web.Services.IServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappeConfig));
builder.Services.AddHttpClient(); // Ensure IHttpClientFactory is registered
builder.Services.AddScoped<IMovieRatingService, MovieRatingService>();
builder.Services.AddScoped<IMovieDeatils, MovieDeatils>();
builder.Services.AddScoped<IActorDeatils, ActorDeatil>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpContextAccessor(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Expire after 30 min
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LogoutPath = "/Auth/Logout";
    });



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
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
