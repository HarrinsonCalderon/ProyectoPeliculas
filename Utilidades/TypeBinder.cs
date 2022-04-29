using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Utilidades
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombreProdiedad = bindingContext.ModelName;
            var valor = bindingContext.ValueProvider.GetValue(nombreProdiedad);
            if (valor==ValueProviderResult.None) {
                return Task.CompletedTask;
            }
            try {
                var valorDeserializado = JsonConvert.DeserializeObject<T>(valor.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);

            }
            catch   {
                bindingContext.ModelState.TryAddModelError(nombreProdiedad,"El valor dado no es del tipo adecuado");
            }
            return Task.CompletedTask;
        }
    }
}
