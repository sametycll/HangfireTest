using Hangfire;
using HangfireBasicAuthenticationFilter;
using HangfireTest.Context;
using HangfireTest.Jobs;
using HangfireTest.Repositories.WeatherRepositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<DbContext>();
builder.Services.AddTransient<IWeatherRepository, WeatherRepository>();

builder.Services.AddControllersWithViews();

var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");

builder.Services.AddHangfire(x => {
    x.UseSqlServerStorage(hangfireConnectionString);
    RecurringJob.AddOrUpdate<JobTest>(j => j.GetWeather(), "*/1 * * * *");
});

builder.Services.AddHangfireServer();

var app = builder.Build();


if (!app.Environment.IsDevelopment()){
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseHangfireDashboard("/test/jobs", new DashboardOptions
{
    DashboardTitle = "Api Test Hangfire",
    DisplayStorageConnectionString=false,
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter
        {
            User="admin",
            Pass="123"
        }
    }
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
