using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheGioiDienThoai.Models;
using TheGioiDienThoai.Models.UserModel;
using TheGioiDienThoai.ViewModels;
using TheGioiDienThoai.ViewModels.Order;
using TheGioiDienThoai.ViewModels.User;

namespace TheGioiDienThoai.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserRepository userRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext context;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment, IUserRepository userRepository, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
            this.userRepository = userRepository;
            this.roleManager = roleManager;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                User currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
                var userViewModel = new UserViewModel()
                {
                    UserId = currentUser.Id,
                    Address = currentUser.Address,
                    Email = currentUser.Email,
                    Gender = currentUser.Gender,
                    Name = currentUser.Name,
                    ProfilePicture = currentUser.ProfilePicture
                };
                return View(userViewModel);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public IActionResult Index(UserViewModel model)
        {
            if (signInManager.IsSignedIn(User))
            {
                if (ModelState.IsValid)
                {
                    User currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
                    var user = new User()
                    {
                        Email = currentUser.Email,
                        UserName = currentUser.Email,
                        Address = currentUser.Address,
                        Name = currentUser.Name,
                        PhoneNumber = currentUser.PhoneNumber,
                        Gender = currentUser.Gender,
                        Id = currentUser.Id,
                        ProfilePicture = currentUser.ProfilePicture
                    };
                    if (model.ImageFile != null)
                    {
                        string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\users");
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                        var filePath = Path.Combine(uploadFolder, fileName);
                        using var fs = new FileStream(filePath, FileMode.Create);
                        model.ImageFile.CopyTo(fs);

                        currentUser.ProfilePicture = fileName;
                        if (!string.IsNullOrEmpty(model.ProfilePicture))
                        {
                            string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images\\users", model.ProfilePicture);
                            System.IO.File.Delete(delFile);
                        }
                    }

                    var editUser = userRepository.Edit(user);
                    if (editUser != null)
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Account");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Sai email hoặc mật khẩu!");
                    //if (!string.IsNullOrEmpty(model.ReturnUrl))
                    //    return Redirect(model.ReturnUrl);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Address = model.Address,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                };
                if (model.ImageFile != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\users");
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using var fs = new FileStream(filePath, FileMode.Create);
                    model.ImageFile.CopyTo(fs);
                    user.ProfilePicture = fileName;
                }
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult LoginAndBuy(string id)
        {
            if (signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Account");
            }
            ViewBag.ProductId = id;
            ViewBag.Product = (from p in context.Products where p.ProductId == id select p).ToList().FirstOrDefault();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAndBuy(LoginAndBuyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserBuy", "Order", new { model.ProductId });
                }
                else
                {
                    ModelState.AddModelError("", "Sai email hoặc mật khẩu!");
                    //if (!string.IsNullOrEmpty(model.ReturnUrl))
                    //    return Redirect(model.ReturnUrl);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult RegisterAndBuy(string id)
        {
            if (signInManager.IsSignedIn(User))
            {
                RedirectToAction("UserBuy", "Order", new { id });
            }
            ViewBag.ProductId = id;
            ViewBag.Product = (from p in context.Products where p.ProductId == id select p).ToList().FirstOrDefault();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAndBuy(RegisterAndBuyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Address = model.Address,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                };
                if (model.ImageFile != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\users");
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using var fs = new FileStream(filePath, FileMode.Create);
                    model.ImageFile.CopyTo(fs);
                    user.ProfilePicture = fileName;
                }
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("UserBuy", "Order", new { model.ProductId });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Orders()
        {
            if (signInManager.IsSignedIn(User))
            {
                var userId = userManager.FindByNameAsync(User.Identity.Name).Result.Id;

                var orders = (from o in context.Orders 
                              join d in context.OrderDetails on o.OrderId equals d.OrderId
                              join c in context.Customers on o.CustomerId equals c.CustomerId
                              join p in context.Products on d.ProductId equals p.ProductId
                              join u in context.Users on c.UserId equals u.Id
                              where u.Id == userId
                              select new OrderDetailViewModel()
                              {
                                  OrderId = o.OrderId,
                                  ProductId = p.ProductId,
                                  CustomerAddress = c.Address,
                                  CustomerName = c.CustomerName,
                                  CustomerPhoneNumber = c.PhoneNumber,
                                  OrderTime = o.OrderTime,
                                  CompleteTime = o.CompleteTime,
                                  ProductName = p.Name,
                                  ProductPrice = p.Price,
                                  OrderStatus = o.Status
                              }).ToList();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}