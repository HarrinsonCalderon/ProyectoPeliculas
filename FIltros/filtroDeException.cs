using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.FIltros
{

    public class filtroDeException:ExceptionFilterAttribute
    {
        //filtro para guardar los errores de la aplicacion
        private readonly ILogger<filtroDeException> logger;

        public filtroDeException(ILogger<filtroDeException> logger) {
            this.logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception,context.Exception.Message);
            base.OnException(context);
        }

    }

}
