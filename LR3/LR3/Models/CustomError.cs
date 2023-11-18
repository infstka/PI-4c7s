using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LR3.Models
{
    public class CustomError
    {
        // по коду контроллер ErrorController вернет объект типа CustomErrorDetails
        public int code;
        // ссылка с деталями об ошибке
        public ErrorLink linkForDetailes;
        public CustomError(int code, string link)
        {
            this.code = code;
            this.linkForDetailes = new ErrorLink(link + "/api/error/" + code);
        }

        public class ErrorLink
        {
            // details = url (ссылка с кодом)
            public string details;
            public ErrorLink(string details)
            {
                this.details = details;
            }
        }
    }

    // код ошибки и краткое текстовое пояснение
    public class CustomErrorDetails
    {
        public int code;
        public string message;
        public CustomErrorDetails(int code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}