﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPNetAPI.Filters;

[AttributeUsage(validOn: AttributeTargets.Method | AttributeTargets.Class)]
public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration.GetValue<string>("ApiKey");

        if (!context.HttpContext.Request.Query.TryGetValue("key", out var providedKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!apiKey!.Equals(providedKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}
