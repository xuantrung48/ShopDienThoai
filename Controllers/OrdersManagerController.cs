using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheGioiDienThoai.Models;
using TheGioiDienThoai.Models.OrderModel;
using TheGioiDienThoai.Models.ProductModel;
using TheGioiDienThoai.Models.UserModel;
using TheGioiDienThoai.ViewModels.Order;

namespace TheGioiDienThoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersManagerController : Controller
    {
        private readonly AppDbContext context;
        private readonly List<OrderDetailViewModel> orders;

        public OrdersManagerController(AppDbContext context)
        {
            this.context = context;
            orders = (from o in context.Orders
                         join c in context.Customers on o.CustomerId equals c.CustomerId
                         join u in context.Users on c.UserId equals u.Id
                         join d in context.OrderDetails on o.OrderId equals d.OrderId
                         join p in context.Products on d.ProductId equals p.ProductId
                         select new OrderDetailViewModel()
                         {
                             CustomerAddress = c.Address,
                             CustomerName = c.CustomerName,
                             CustomerPhoneNumber = c.PhoneNumber,
                             OrderId = o.OrderId,
                             OrderStatus = o.Status,
                             OrderTime = o.OrderTime,
                             CompleteTime = o.CompleteTime,
                             ProductId = d.ProductId,
                             ProductName = p.Name,
                             ProductPrice = p.Price,
                             UserId = u.Id,
                             Note = o.Note
                         }).ToList();
        }
        public IActionResult PendingOrders()
        {
            var pendingOrders = (from o in orders where o.OrderStatus == OrderStatus.Pending select o).ToList();
            return View(pendingOrders);
        }
        public IActionResult ProcessingOrders()
        {
            var processingOrders = (from o in orders where o.OrderStatus == OrderStatus.Processing select o).ToList();
            return View(processingOrders);
        }
        public IActionResult CompletedOrders()
        {
            var completedOrders = (from o in orders where o.OrderStatus == OrderStatus.Completed select o).ToList();
            return View(completedOrders);
        }
        public IActionResult CanceledOrders()
        {
            var canceledOrders = (from o in orders where o.OrderStatus == OrderStatus.Canceled select o).ToList();
            return View(canceledOrders);
        }
        [HttpGet]
        public IActionResult Edit(string id, string backAction)
        {
            var order = (from o in orders where o.OrderId == id select o).ToList().FirstOrDefault();
            ViewBag.BackAction = backAction;
            return View(order);
        }
        [HttpPost]
        public IActionResult Edit(OrderDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = context.Orders.Find(model.OrderId);
                if ((order.Status == OrderStatus.Canceled) && (model.OrderStatus == OrderStatus.Pending || model.OrderStatus == OrderStatus.Processing || order.Status == OrderStatus.Completed))
                {
                    context.Products.Find(model.ProductId).Remain -= 1;
                }
                if ((order.Status == OrderStatus.Pending || order.Status == OrderStatus.Processing || order.Status == OrderStatus.Completed) && (model.OrderStatus == OrderStatus.Canceled))
                {
                    context.Products.Find(model.ProductId).Remain += 1;
                }
                order.Status = model.OrderStatus;
                if (model.OrderStatus == OrderStatus.Completed || model.OrderStatus == OrderStatus.Canceled)
                {
                    order.CompleteTime = DateTime.Now;
                }
                order.Note = model.Note;
                var orderDetail = (from d in context.OrderDetails where d.OrderId == model.OrderId select d).ToList().FirstOrDefault();
                orderDetail.Price = model.ProductPrice;
                var customer = context.Customers.Find(order.CustomerId);
                customer.Address = model.CustomerAddress;
                customer.CustomerName = model.CustomerName;
                customer.PhoneNumber = model.CustomerPhoneNumber;
                context.SaveChanges();
                return RedirectToAction("Edit", "OrdersManager", new { id = model.OrderId });
            }
            return View();
        }
    }
}