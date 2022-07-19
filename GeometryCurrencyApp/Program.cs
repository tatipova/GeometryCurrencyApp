using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using GeometryCurrencyApp.CircleApiFeature.Models;
using GeometryCurrencyApp.CircleApiFeature.Services;
using GeometryCurrencyApp.CircleApiFeature.Repository;

var builder = WebApplication.CreateBuilder();

builder.Services.AddControllers();
builder.Services.AddOptions();

string logPath = builder.Configuration.GetValue<string>("Logging:Path");

builder.Services.AddSingleton<ILogger>(new GeometryCurrencyApp.Logger(logPath));

builder.Services.Configure<ApiSettingsModel>(GeometryCurrencyApp.SettingsHelper.InitConfiguration("apiconfiguration.json"));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();

builder.Services.AddScoped<ICurrencyContext, CBRCurrencyContext>();

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                Message = e.Value.Errors.First().ErrorMessage
            })
            .FirstOrDefault();

        return new BadRequestObjectResult(errors);
    };
});

var app = builder.Build();


app.UseMiddleware<GeometryCurrencyApp.LoggingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
