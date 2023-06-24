using AppNoOne.MiddelWere;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AppNoOne.Controllers 
{
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        [HttpGet]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Index()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "dist/index.html");

            // Trả về file index.html
            return PhysicalFile(filePath, "text/html");
        }
    }
}
