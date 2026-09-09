var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// To use the HTTP Client we have to inject the HTTPClient Factory inside the Program.cs so that it can create the HttpClient for us. The HTTPClientFactory is a factory that can be used to create HttpClient instances in an efficient way.
// Before we build this application we need to inject the Http Builder in here
// This was done during the project of API consumption

builder.Services.AddHttpClient();   // This was added to inject the HTTPClientFactory into the application so that we can use it in our controllers to make HTTP calls to the Web API.

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
