using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopDienThoai.Models;
using ShopDienThoai.Models.OrderModel;
using ShopDienThoai.Models.ProductModel;
using ShopDienThoai.Models.UserModel;
using ShopDienThoai.ViewModels.Order;
using System;
using System.Linq;

namespace ShopDienThoai.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBrandRepository brandRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly AppDbContext context;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public OrderController(IProductRepository productRepository, IBrandRepository brandRepository,
            ICategoryRepository categoryRepository, AppDbContext context, SignInManager<User> signInManager,
            UserManager<User> userManager, IOrderRepository orderRepository, ICustomerRepository customerRepository,
            IOrderDetailRepository orderDetailRepository)
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
        }

        public IActionResult Buy(string id)
        {
            if (signInManager.IsSignedIn(User))
                return RedirectToAction("UserBuy", new { ProductId = id });
            ViewBag.ProductId = id;
            ViewBag.Product = (from p in context.Products where p.IsDeleted == false && p.ProductId == id select p)
                .ToList().FirstOrDefault();
            return View();
        }

        [HttpGet]
        public IActionResult Order(string ProductId)
        {
            var product = (from p in context.Products where p.IsDeleted == false && p.ProductId == ProductId select p)
                .ToList().FirstOrDefault();
            if (product.Remain == 0)
                return RedirectToAction("Index", "Home");
            var customer = new CustomerViewModel();
            if (signInManager.IsSignedIn(User))
            {
                var currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
                customer = new CustomerViewModel
                {
                    Name = currentUser.Name,
                    PhoneNumber = currentUser.PhoneNumber,
                    Address = currentUser.Address,
                    ProductId = ProductId
                };
            }

            ViewBag.Product = product;
            return View(customer);
        }

        [HttpPost]
        public IActionResult Order(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                if (signInManager.IsSignedIn(User))
                    user = userManager.FindByNameAsync(User.Identity.Name).Result;
                var product =
                    (from p in context.Products where p.IsDeleted == false && p.ProductId == model.ProductId select p)
                    .ToList().FirstOrDefault();
                var customer = new Customer
                {
                    CustomerName = model.Name,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    UserId = signInManager.IsSignedIn(User) ? user.Id : null
                };
                var createCustomer = customerRepository.Create(customer);
                var order = new Order
                {
                    CustomerId = createCustomer.CustomerId,
                    OrderTime = DateTime.Now,
                    Status = OrderStatus.Pending
                };
                var createOrder = orderRepository.Create(order);
                var orderDetail = new OrderDetail
                {
                    OrderId = createOrder.OrderId,
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = 1
                };
                var createOrderDetail = orderDetailRepository.Create(orderDetail);
                context.Products.Find(model.ProductId).Remain -= 1;
                context.SaveChanges();
                return RedirectToAction("OrderDetail", new { id = createOrder.OrderId });
            }

            return View();
        }

        public IActionResult OrderDetail(string id)
        {
            var orderDetail = (from o in context.Orders
                               where o.OrderId == id
                               join d in context.OrderDetails on o.OrderId equals d.OrderId
                               join c in context.Customers on o.CustomerId equals c.CustomerId
                               join p in context.Products on d.ProductId equals p.ProductId
                               select new OrderDetailViewModel
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
                                   OrderStatus = o.Status,
                                   Note = o.Note
                               }).ToList().FirstOrDefault();
            ViewBag.OrderId = id;
            ViewBag.UserOrder = (from u in context.Users
                                 join c in context.Customers on u.Id equals c.UserId
                                 join o in context.Orders on c.CustomerId equals o.CustomerId
                                 where o.OrderId == id
                                 select u).ToList().FirstOrDefault();
            return View(orderDetail);
        }

        public IActionResult Cancel(string id)
        {
            var order = (from o in context.Orders where o.OrderId == id select o).ToList().FirstOrDefault();
            if (order.Status != OrderStatus.Pending) return RedirectToAction("Index", "Home");
            var userOrder = (from u in context.Users
                             join c in context.Customers on u.Id equals c.UserId
                             join o in context.Orders on c.CustomerId equals o.CustomerId
                             where o.OrderId == id
                             select u).ToList().FirstOrDefault();

            var currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
            if (userOrder.Id == currentUser.Id)
            {
                var editOrder = context.Orders.Find(id);
                editOrder.Status = OrderStatus.Canceled;
                editOrder.CompleteTime = DateTime.Now;
                var productId = (from p in context.Products
                                 join d in context.OrderDetails on p.ProductId equals d.ProductId
                                 join o in context.Orders on d.OrderId equals o.OrderId
                                 where d.OrderId == id
                                 select p).ToList().FirstOrDefault().ProductId;
                context.Products.Find(productId).Remain += 1;
                context.SaveChanges();
                return RedirectToAction("Orders", "Account");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult CheckOrder()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var order = (from o in context.Orders where o.OrderId == id select o).ToList().FirstOrDefault();
            if (order == null)
                return RedirectToAction("Index", "Home");
            var userOrder = (from u in context.Users
                             join c in context.Customers on u.Id equals c.UserId
                             join o in context.Orders on c.CustomerId equals o.CustomerId
                             where o.OrderId == id
                             select u).ToList().FirstOrDefault();
            if (!signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            var currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
            if (currentUser.Id != userOrder.Id)
                return RedirectToAction("Index", "Home");

            var orderDetail = (from o in context.Orders
                               where o.OrderId == id
                               join d in context.OrderDetails on o.OrderId equals d.OrderId
                               join c in context.Customers on o.CustomerId equals c.CustomerId
                               join p in context.Products on d.ProductId equals p.ProductId
                               select new OrderDetailViewModel
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
                               }).ToList().FirstOrDefault();
            return View(orderDetail);
        }

        [HttpPost]
        public IActionResult Edit(OrderDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = (from c in context.Customers
                                join o in context.Orders on c.CustomerId equals o.CustomerId
                                where o.OrderId == model.OrderId
                                select c).ToList().FirstOrDefault();
                customer.Address = model.CustomerAddress;
                customer.PhoneNumber = model.CustomerPhoneNumber;
                customer.CustomerName = model.CustomerName;
                context.SaveChanges();
                return RedirectToAction("OrderDetail", "Order", new { id = model.OrderId });
            }

            return View();
        }
    }
}