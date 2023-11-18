using System.Web;
using System.Web.Mvc;

namespace LR2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //обработка исключений
            filters.Add(new HandleErrorAttribute());
        }
    }
}
