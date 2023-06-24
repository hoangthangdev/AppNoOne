using AppNoOne.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppNoOne.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("error/{statusCode}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError(int? statusCode)
        {
            if (statusCode == (int)HttpStatusCode.Unauthorized)
            {
                return ViewComponent(MessagePage.COMPONENTNAME,
                    new MessagePage.Message()
                    {
                        title = "Xác thực error",
                        htmlcontent = "Chưa đăng nhập ứng dụng,vui lòng đăng nhập và thử lại",
                        urlredirect = Url.ActionLink("Index", "Account")
                    }
                );
            }
            return View("Error");
        }
    }
}
