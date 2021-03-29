using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using GameLib.Model.Exception;
using GameLib.Model.DTOs;
using Microsoft.Extensions.Logging;

namespace GameLib.API.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger _logger;

        public ExceptionHandlerFilter(
            IHostEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider,
            ILogger<ExceptionHandlerFilter> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (typeof(UserFriendlyException).IsAssignableFrom(context.Exception.GetType()) ||
                _hostingEnvironment.IsDevelopment())
            {
                context.Result = new JsonResult(new MessageApiResult{ 
                    Success = false,
                    Message = context.Exception.Message 
                });
                context.HttpContext.Response.StatusCode = 400;
                _logger.LogError("Erro {Exception} em {path}", context.Exception, context.HttpContext.Request.Path);
                return;
            }
            else
            {
                context.Result = new JsonResult(new MessageApiResult{
                    Success = false,
                    Message = "Ocorreu um erro interno, entre em contato com o suporte" 
                });
                context.HttpContext.Response.StatusCode = 500;
                _logger.LogError("Erro {Exception} em {path}", context.Exception, context.HttpContext.Request.Path);
                return;
            }

        }
    }
}