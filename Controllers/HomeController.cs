using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheGioiDienThoai.Models;
using TheGioiDienThoai.Models.OrderModel;
using TheGioiDienThoai.Models.ProductModel;
using TheGioiDienThoai.Models.UserModel;
using TheGioiDienThoai.ViewModels.Order;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheGioiDienThoai.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;
        private readonly IProductRepository productRepository;
        private readonly IBrandRepository brandRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IOrderRepository orderRepository;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly ICarouselImageRepository carouselImageRepository;
        public HomeController(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, AppDbContext context, SignInManager<User> signInManager, UserManager<User> userManager, IOrderRepository orderRepository, ICustomerRepository customerRepository, IOrderDetailRepository orderDetailRepository, ICarouselImageRepository carouselImageRepository)
        {
            this.context = context;
            this.productRepository = productRepository;
            this.brandRepository = brandRepository;
            this.categoryRepository = categoryRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.carouselImageRepository = carouselImageRepository;
        }
        public IActionResult Index()
        {
            ViewBag.Brands = brandRepository.Get().ToList();
            ViewBag.Categories = categoryRepository.Get().ToList();
            ViewBag.Products = productRepository.Get().ToList();
            ViewBag.CarouselImages = carouselImageRepository.Get().ToList();
            return View();
        }
        public IActionResult ViewProduct(string id)
        {
            var product = productRepository.Get(id);
            if (product == null)
            {
                ViewBag.ErrorMessage = "Không tìm thấy sản phẩm!";
                return View("~/Views/Error/PageNotFound.cshtml");
            }
            ViewBag.Images = (from e in context.Images
                              where e.ProductId == product.ProductId
                              select e).ToList();
            var relatedProducts = (from p in context.Products
                                   where p.CategoryId == product.CategoryId ||
                                   p.BrandId == product.BrandId
                                   select p).ToList();
            relatedProducts.Remove(context.Products.Find(id));
            ViewBag.Brands = brandRepository.Get().ToList();
            ViewBag.Categories = categoryRepository.Get().ToList();
            ViewBag.RelatedProducts = relatedProducts.Take(6);
            return View(product);
        }
        public IActionResult Search(int categoryId, int brandId, string keyWord, int minPrice, int maxPrice, string sortByPrice, int page = 1)
        {
            string key = string.Empty;
            if (keyWord != null)
                key = keyWord.ToLower();
            var products = (from p in context.Products where p.Remain > 0
                            join c in context.Categories on p.CategoryId equals c.CategoryId
                            join b in context.Brands on p.BrandId equals b.BrandId
                            where ((p.FrontCamera.ToLower().Contains(key) ||
                             p.RearCamera.ToLower().Contains(key) ||
                             p.Screen.ToLower().Contains(key)) ||
                             c.Name.ToLower().Contains(key) ||
                             b.Name.ToLower().Contains(key) ||
                             p.Name.ToLower().Contains(key) ||
                             p.CPU.ToLower().Contains(key))
                            select p).ToList();

            if (categoryId == 0 && brandId != 0)
                products = (from p in products
                            where (p.BrandId == brandId)
                            select p).ToList();
            
            if (categoryId != 0 && brandId == 0)
                products = (from p in products
                            where (p.CategoryId == categoryId)
                            select p).ToList();
            
            if (categoryId != 0 && brandId != 0)
                products = (from p in products
                            where (p.CategoryId == categoryId) &&
                            (p.BrandId == brandId)
                            select p).ToList();

            if (minPrice != 0 && maxPrice != 0)
                products = (from p in products
                            where (p.Price >= minPrice) &&
                            (p.Price <= maxPrice)
                            select p).ToList();

            if (sortByPrice == "desc")
                products = products.OrderByDescending(x => x.Price).ToList();

            if (sortByPrice == "asc")
                products = products.OrderBy(x => x.Price).ToList();

            ViewBag.Categories = (from c in context.Categories select c).ToList();
            ViewBag.Brands = (from b in context.Brands select b).ToList();
            ViewBag.CategoryId = categoryId;
            ViewBag.BrandId = brandId;
            ViewBag.KeyWord = keyWord;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.Sort = sortByPrice;
            ViewBag.Count = products.Count;
            ViewBag.Page = page;
            return View(products.Skip(page * 12 - 12).Take(12).ToList());
        }
        public IActionResult Category(int id, int page = 1)
        {
            var products = (from p in context.Products where p.CategoryId == id && p.Remain > 0 select p).ToList();
            ViewBag.Categories = (from c in context.Categories select c).ToList();
            ViewBag.Brands = (from b in context.Brands select b).ToList();
            var categories = (from c in context.Categories where c.CategoryId == id select c).ToList();
            if (categories.Count != 0)
                ViewBag.title = categories.FirstOrDefault().Name;
            else
                ViewBag.title = "Không có danh mục!";

            ViewBag.CategoryId = id;
            ViewBag.Count = products.Count;
            ViewBag.Page = page;
            return View(products.Skip(page * 12 - 12).Take(12).ToList());
        }
    }
}
