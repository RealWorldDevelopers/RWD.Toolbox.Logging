﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

// TODO NOT USED

namespace RWD.Toolbox.Logging.Exceptions.Middleware
{
   // based on Microsoft's standard exception middleware found here:
   // https://github.com/aspnet/Diagnostics/tree/dev/src/
   //          Microsoft.AspNetCore.Diagnostics/ExceptionHandler
   //  public static class CustomExceptionMiddlewareExtensions
   //  {
   //public static IApplicationBuilder UseCustomExceptionHandler(
   //    this IApplicationBuilder builder, string product, string layer,
   //    string errorHandlingPath)
   //{
   //   var bldr = builder.UseMiddleware<CustomExceptionHandlerMiddleware>(product, layer, 
   //      Options.Create(new ExceptionHandlerOptions
   //   {
   //      ExceptionHandlingPath = new PathString(errorHandlingPath)
   //   }));

   //   return bldr;
   //}
   // }
}
