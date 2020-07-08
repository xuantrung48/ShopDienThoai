using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheGioiDienThoai.Models.ProductModel;

namespace TheGioiDienThoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BrandsManagerController : Controller
    {
        private readonly IBrandRepository brandRepository;
        public BrandsManagerController(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }
        public IActionResult Index()
        {
            return View(brandRepository.Get());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand newBrand)
        {
            if (ModelState.IsValid)
            {
                brandRepository.Create(newBrand);
                return RedirectToAction("Index", "BrandsManager");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var brand = brandRepository.Get(id);
            if (brand == null)
                return View("~/Views/Error/PageNotFound.cshtml");
            return View(brand);
        }
        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            if (brandRepository.Edit(brand) != null)
            {
                return RedirectToAction("Index", "BrandsManager");
            }
            return View();
        }
        public IActionResult Remove(int id)
        {
            var brand = brandRepository.Get(id);
            if (brand == null)
                return View("~/Views/Error/PageNotFound.cshtml");

            if (brandRepository.Remove(id))
            {
                return RedirectToAction("Index", "BrandsManager");
            }
            return View();
        }
    }
}