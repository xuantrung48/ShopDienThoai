using Microsoft.AspNetCore.Mvc;

namespace ShopDienThoai.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("Error/{StatusCode}")]
        public IActionResult PageNotFound(int StatusCode)
        {
            ViewBag.ErrorMessage = "Lỗi 404: không tìm thấy trang!";
            return View();
        }

        [Route("Error")]
        public IActionResult Error()
        {
            ViewBag.ErrorMessage = "Lỗi hệ thống";
            return View("Exception");
        }
    }
}