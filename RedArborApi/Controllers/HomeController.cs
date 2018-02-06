using System.Web.Mvc;

namespace RedArbor.WebApi.Controllers
{
    /// <summary>
    /// Se mantiene el proyecto como MVC y WebApi aunque para la prueba se podría haber usado cualquiera de los dos
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
