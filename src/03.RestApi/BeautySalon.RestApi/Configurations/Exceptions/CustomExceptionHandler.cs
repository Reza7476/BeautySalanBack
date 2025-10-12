using BeautySalon.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Text.Json;

namespace BeautySalon.RestApi.Configurations.Exceptions;

public static class CustomExceptionHandler
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        var environment = app.ApplicationServices
            .GetRequiredService<IWebHostEnvironment>();

        var jsonOptions = app.ApplicationServices
            .GetService<IOptions<JsonOptions>>()?.Value.SerializerOptions ?? new JsonSerializerOptions();

        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()?.Error;

                var result = new ExceptionErrorDto();
                result.StatusCode = context.Response.StatusCode;

                const string genericProductionError = "Something went wrong please try again later.";

                if (environment.IsDevelopment())
                {
                    // حالت توسعه: نمایش کامل جزئیات
                    result.Error = exception?.GetType().Name.Replace("Exception", string.Empty);
                    result.Description = exception?.ToString();
                }
                else
                {
                    // حالت Production
                    if (exception is CustomException)
                    {
                        result.Error = exception.GetType().Name.Replace("Exception", string.Empty);
                        result.Description = exception?.Message; // فقط پیام کوتاه
                    }
                    else
                    {
                        result.Error = "UnknownError";
                        result.Description = genericProductionError; // پیام عمومی برای کاربر
                    }
                }

                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(JsonSerializer.Serialize(result, jsonOptions));
            });
        });

        if (environment.IsDevelopment())
        {
            app.UseHsts(); // فقط در توسعه HSTS فعال است
        }

        return app;
    }
}
