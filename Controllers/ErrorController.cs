using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheGioiDienThoai.Controllers
{
    public class ErrorController : Controller
    {
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