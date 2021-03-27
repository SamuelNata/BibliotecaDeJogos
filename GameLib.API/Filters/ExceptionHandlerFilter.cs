using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using GameLib.Model.Exception;

namespace GameLib.API.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public ExceptionHandlerFilter(
            IHostEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if(typeof(UserFriendlyException).IsAssignableFrom(context.Exception.GetType())){
                context.Result = new JsonResult(new { Erro = true, Message = context.Exception.Message });
            }

            if (_hostingEnvironment.IsDevelopment())
            {
                if(typeof(UserFriendlyException).IsAssignableFrom(context.Exception.GetType()))
                {
                    context.Result = new JsonResult(new { Erro = true, Message = context.Exception.Message });
                }
            }
            else
            {
                if(typeof(UserFriendlyException).IsAssignableFrom(context.Exception.GetType()))
                {
                    context.Result = new JsonResult(new { Erro = true, Message = "Ocorreu um erro interno, entre em contato com o suporte" });
                }
            }

        }
    }
}