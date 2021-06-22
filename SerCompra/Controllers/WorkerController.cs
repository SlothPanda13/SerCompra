using Microsoft.AspNetCore.Mvc;

namespace SerCompra.Controllers
{
    public class WorkerController : Controller
    {
        // GET
        public IActionResult WorkerIndex()
        {
            return View();
        }
    }
}