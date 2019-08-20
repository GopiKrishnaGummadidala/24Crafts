using System.Web;
using System.Web.Mvc;

namespace Jupiter._24Crafts.Client.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
