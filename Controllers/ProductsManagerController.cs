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
using TheGioiDienThoai.ViewModels.Product;

namespace TheGioiDienThoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsManagerController : Controller
    {
        private readonly AppDbContext context;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IImageRepository imageRepository;
        public ProductsManagerController(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository, IWebHostEnvironment webHostEnvironment, IImageRepository imageRepository, AppDbContext context)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.imageRepository = imageRepository;
            this.context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Brands = GetBrands();
            ViewBag.Products = productRepository.Get().ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            ViewBag.Brands = GetBrands();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Name = model.Name,
                    BrandId = model.BrandId,
                    CategoryId = model.CategoryId,
                    CPU = model.CPU,
                    Description = model.Description,
                    FrontCamera = model.FrontCamera,
                    OS = model.OS,
                    Price = model.Price,
                    Ram = model.Ram,
                    RearCamera = model.RearCamera,
                    Remain = model.Remain,
                    Rom = model.Rom,
                    Screen = model.Screen
                };

                var createProduct = productRepository.Create(product);

                if (model.ImageFiles != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\products");
                    foreach (var imageFile in model.ImageFiles)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                        var filePath = Path.Combine(uploadFolder, fileName);
                        using var fs = new FileStream(filePath, FileMode.Create);
                        imageFile.CopyTo(fs);

                        imageRepository.Create(new Image()
                        {
                            ImageId = Guid.NewGuid().ToString(),
                            ProductId = createProduct.ProductId,
                            ImageName = fileName
                        });
                    }
                }
                return RedirectToAction("ViewProduct", "Home", new { id = createProduct.ProductId });
            }
            ViewBag.Categories = categoryRepository.Get();
            ViewBag.Brands = brandRepository.Get();
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewBag.Categories = categoryRepository.Get();
            ViewBag.Brands = brandRepository.Get();
            var product = productRepository.Get(id);
            ViewBag.Images = (from e in context.Images
                              where e.ProductId == product.ProductId
                              select e).ToList();
            if (product == null)
            {
                return View("~/Views/Error/PageNotFound.cshtml");
            }
            var editProduct = new EditProductViewModel()
            {
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                CPU = product.CPU,
                Description = product.Description,
                FrontCamera = product.FrontCamera,
                //ImageFileName = product.ImageFileName,
                Name = product.Name,
                OS = product.OS,
                Price = product.Price,
                ProductId = product.ProductId,
                Ram = product.Ram,
                RearCamera = product.RearCamera,
                Remain = product.Remain,
                Rom = product.Rom,
                Screen = product.Screen
            };
            return View(editProduct);
        }
        [HttpPost]
        public IActionResult Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    ProductId = model.ProductId,
                    CPU = model.CPU,
                    Description = model.Description,
                    FrontCamera = model.FrontCamera,
                    Name = model.Name,
                    OS = model.OS,
                    Price = model.Price,
                    Ram = model.Ram,
                    RearCamera = model.RearCamera,
                    Remain = model.Remain,
                    Rom = model.Rom,
                    Screen = model.Screen,
                    BrandId = model.BrandId,
                    CategoryId = model.CategoryId
                };
                var editProduct = productRepository.Edit(product);
                if (model.ImageFiles != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\products");
                    foreach (var imageFile in model.ImageFiles)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                        var filePath = Path.Combine(uploadFolder, fileName);
                        using var fs = new FileStream(filePath, FileMode.Create);
                        imageFile.CopyTo(fs);

                        imageRepository.Create(new Image()
                        {
                            ImageId = Guid.NewGuid().ToString(),
                            ProductId = editProduct.ProductId,
                            ImageName = fileName
                        });
                    }
                }
                if (editProduct != null)
                {
                    return RedirectToAction("Index", "ProductsManager");
                }
            }
            ViewBag.Categories = categoryRepository.Get();
            ViewBag.Brands = brandRepository.Get();
            return View();
        }
        public IActionResult Remove(string id)
        {
            if (productRepository.Remove(id))
                return RedirectToAction("Index", "ProductsManager");
            return View();
        }
        private List<Category> GetCategories()
        {
            return categoryRepository.Get().ToList();
        }
        private List<Brand> GetBrands()
        {
            return brandRepository.Get().ToList();
        }
        public IActionResult RemoveImage(string id, string imgId)
        {
            var fileName = (from e in context.Images
                            where e.ImageId == imgId
                            select e).ToList().FirstOrDefault().ImageName;
            imageRepository.Remove(imgId);
            string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images\\products", fileName);
            System.IO.File.Delete(delFile);
            return RedirectToAction("Edit", new { id });
        }
    }
}