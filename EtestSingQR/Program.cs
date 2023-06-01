using EtestSingQR.Services;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    //原本是 JsonNamingPolicy.CamelCase，強制頭文字轉小寫，設為null維持原樣不轉成小寫
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    //允許基本拉丁英文及中日韓文字維持原字元
                    options.JsonSerializerOptions.Encoder =
                        System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
                });

// 透過 builder.Services 將服務註冊入 DI 容器
builder.Services.AddSingleton<FunService>();
builder.Services.AddScoped<ILoginUserService, LoginUserTestService>();
builder.Services.AddScoped<IScanQRService, ScanQRTestService>();
builder.Services.AddTransient<IImgVerifService, ImgVerifViewService>();

//設定AJXA跨腳本防護
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //設定捕捉全域錯誤的Middlewares (ExceptionHandler)
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
//app.MapControllerRoute(
//    name: "ErrorRoute",
//    pattern: "Error/{*ErrNoMsg}",
//    defaults:new { controller = "Home", action = "Error" });

// 啟動 ASP.NET Core 應用程式
app.Run();
