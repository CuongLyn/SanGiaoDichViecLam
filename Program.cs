using Microsoft.EntityFrameworkCore;
using BTL.Data;
using BTL.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDistributedMemoryCache();
// Add services to the container.
builder.Services.AddControllersWithViews();
//Add session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian tồn tại của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddScoped<INhaTuyenDungRepository, NhaTuyenDungRepository>();
builder.Services.AddScoped<ICongViecRepository, CongViecRepository>();
builder.Services.AddScoped<IUngVienRepository, UngVienRepository>();
builder.Services.AddScoped<IUngTuyenRepository, UngTuyenRepository>();
builder.Services.AddScoped<IThongBaoRepository, ThongBaoRepository>();




var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



//Use session
app.UseSession();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
