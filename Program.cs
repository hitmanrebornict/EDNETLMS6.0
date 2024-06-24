using EDNETLMS.Data;
using EDNETLMS.Pages;
using EDNETLMS.Pages.Services;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRadzenComponents();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<EDNETLMSUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddDefaultTokenProviders()
	.AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);

    options.LoginPath = "/Login";  //set the login page.
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});


builder.Services.AddScoped<CreateLeadPageServices>();
builder.Services.AddScoped<EditLeadPageServices>();
builder.Services.AddScoped<DialogCreateLeadCatchUpStatusServices>();
builder.Services.AddScoped<DialogLeadCatchUpStatusPageServices>();
builder.Services.AddScoped<GlobalData>();
builder.Services.AddScoped<EditLeadCatchUpStatusPageServices>();
builder.Services.AddScoped<ListTablePageServices>();
builder.Services.AddScoped<FollowUpLeadListPageServices>();
builder.Services.AddScoped<IndexServices>();
builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddScoped<MasterDataServices>();

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
	options.AddPolicy("UserRole", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Seed roles here
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }

    // Add more roles as needed
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();

app.Run();