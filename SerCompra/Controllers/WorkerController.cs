using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SerCompra.Controllers
{
    [Authorize]
    public class WorkerController : Controller
    {
        // GET
        public IActionResult WorkerIndex()
        {
            return View();
        }
    }
}