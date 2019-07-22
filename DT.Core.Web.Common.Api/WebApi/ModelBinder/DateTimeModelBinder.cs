using System;
using System.Globalization;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace DT.Core.Web.Common.Api.WebApi.ModelBinder
{
    public class DateTimeModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            DateTime dateTime;

            ValueProviderResult valueProvider = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProvider == null)
                return false;

            if (string.IsNullOrEmpty(valueProvider.AttemptedValue))
                return false;

            if (DateTime.TryParse(valueProvider.AttemptedValue, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out dateTime))
            {
                bindingContext.Model = dateTime;
                return true;
            }

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalidate date");

            return false;
        }
    }
}
