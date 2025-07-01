using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Namespace
{
    [Authorize]
    public class ProdutosController : Controller
    {

        public ActionResult index()
        {
            return View();
        }
    }
}
