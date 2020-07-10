using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TheGioiDienThoai.Models;
using TheGioiDienThoai.Models.ProductModel;
using TheGioiDienThoai.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheGioiDienThoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public DashboardController(IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AppSetting()
        {
            var appSettings = (from s in context.AppSettings
                               select new AppSettingViewModel()
                               {
                                   Icon = s.Icon,
                                   Logo = s.Logo,
                                   ShortDesc = s.ShortDesc,
                                   Title = s.Title
                               }).ToList().FirstOrDefault();
            return View(appSettings);
        }
        [HttpPost]
        public IActionResult AppSetting(AppSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var setting = context.AppSettings.FirstOrDefault();
                setting.Title = model.Title;
                setting.ShortDesc = model.ShortDesc;
                if (model.LogoFile != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.LogoFile.FileName)}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using var fs = new FileStream(filePath, FileMode.Create);
                    model.LogoFile.CopyTo(fs);

                    setting.Logo = fileName;
                    if (!string.IsNullOrEmpty(model.Logo))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images", model.Logo);
                        System.IO.File.Delete(delFile);
                    }
                }
                if (model.IconFile != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.IconFile.FileName)}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using var fs = new FileStream(filePath, FileMode.Create);
                    model.IconFile.CopyTo(fs);

                    setting.Icon = fileName;
                    if (!string.IsNullOrEmpty(model.Icon))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images", model.Icon);
                        System.IO.File.Delete(delFile);
                    }
                }
                context.SaveChanges();
                return RedirectToAction("AppSetting");
            }
            return View();
        }
    }
}
