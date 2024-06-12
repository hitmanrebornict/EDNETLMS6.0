using EDNETLMS.Data;
using EDNETLMS.Pages;
using EDNETLMS.Pages.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRadzenComponents();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CreateLeadPageServices>();
builder.Services.AddScoped<EditLeadPageServices>();
builder.Services.AddScoped<DialogCreateLeadCatchUpStatusServices>();
builder.Services.AddScoped<DialogLeadCatchUpStatusPageServices>();
builder.Services.AddScoped<GlobalData>();
builder.Services.AddScoped<EditLeadCatchUpStatusPageServices>();
builder.Services.AddScoped<ListTablePageServices>();
builder.Services.AddScoped<FollowUpLeadListPageServices>();
builder.Services.AddScoped<IndexServices>();
builder.Services.AddScoped<FollowUpLeadListPage>();
builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddScoped<MasterDataServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
