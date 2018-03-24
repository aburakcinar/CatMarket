using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace FishMarket.WebApi.Utils.MultipartHelper
{
    /// <summary>
    /// This code is obtained from the link below
    /// <see cref="https://github.com/aspnet/Docs/blob/master/aspnetcore/mvc/models/file-uploads/sample/FileUploadSample/Filters/DisableFormValueModelBindingAttribute.cs"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var formValueProviderFactory = context.ValueProviderFactories
                .OfType<FormValueProviderFactory>()
                .FirstOrDefault();
            if (formValueProviderFactory != null)
            {
                context.ValueProviderFactories.Remove(formValueProviderFactory);
            }

            var jqueryFormValueProviderFactory = context.ValueProviderFactories
                .OfType<JQueryFormValueProviderFactory>()
                .FirstOrDefault();
            if (jqueryFormValueProviderFactory != null)
            {
                context.ValueProviderFactories.Remove(jqueryFormValueProviderFactory);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
