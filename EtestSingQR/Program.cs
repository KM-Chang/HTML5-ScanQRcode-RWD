using EtestSingQR.Services;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    //�쥻�O JsonNamingPolicy.CamelCase�A�j���Y��r��p�g�A�]��null������ˤ��ন�p�g
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    //���\�򥻩ԤB�^��Τ�������r������r��
                    options.JsonSerializerOptions.Encoder =
                        System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
                });

// �z�L builder.Services �N�A�ȵ��U�J DI �e��
builder.Services.AddSingleton<FunService>();
builder.Services.AddScoped<ILoginUserService, LoginUserTestService>();
builder.Services.AddScoped<IScanQRService, ScanQRTestService>();
builder.Services.AddTransient<IImgVerifService, ImgVerifViewService>();

//�]�wAJXA��}�����@
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //�]�w����������~��Middlewares (ExceptionHandler)
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

// �Ұ� ASP.NET Core ���ε{��
app.Run();
