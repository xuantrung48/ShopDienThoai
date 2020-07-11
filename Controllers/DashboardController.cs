using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly ICarouselImageRepository carouselImageRepository;
        public DashboardController(IWebHostEnvironment webHostEnvironment, AppDbContext context, ICarouselImageRepository carouselImageRepository)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
            this.carouselImageRepository = carouselImageRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AppSetting()
        {
            var appSetting = (from s in context.AppSettings
                               select new AppSettingViewModel()
                               {
                                   Icon = s.Icon,
                                   Logo = s.Logo,
                                   ShortDesc = s.ShortDesc,
                                   Title = s.Title,
                                   DefaultRoleId = s.DefaultRoleId
                               }).ToList().FirstOrDefault();
            ViewBag.Roles = (from r in context.Roles select r).ToList();
            return View(appSetting);
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
                setting.DefaultRoleId = model.DefaultRoleId;
                context.SaveChanges();
                return RedirectToAction("AppSetting");
            }
            return View();
        }
        public IActionResult RemoveCarouselImage(int id)
        {
            _ = carouselImageRepository.Remove(id);
            return RedirectToAction("EditCarousel");
        }
        [HttpGet]
        public IActionResult EditCarousel()
        {
            ViewBag.Images = carouselImageRepository.Get();
            return View();
        }
        [HttpPost]
        public IActionResult EditCarousel(EditCarouselViewModel model)
        {
            if (model.ImageFiles != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\carousel");
                foreach (var imageFile in model.ImageFiles)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using var fs = new FileStream(filePath, FileMode.Create);
                    imageFile.CopyTo(fs);

                    carouselImageRepository.Create(new CarouselImage()
                    {
                        Name = fileName
                    });
                }
            }
            return RedirectToAction("EditCarousel");
        }
    }
}
