using authcontroller.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace authcontroller
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TransactionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}