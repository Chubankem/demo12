using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;

namespace demo.Controllers
{
    public class OrderController : Controller
    {
        LapTrinhWebEntities db = new LapTrinhWebEntities();
        // GET: Order
        public ActionResult Index()
        {
            int userID = int.Parse(Session["idUser"].ToString());
            List<Order> orders = db.Orders.Where(x => x.UserID == userID).ToList();
            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            //Order order = db.Orders.Include("OrderItems").Where(x => x.OrderID == id).First();

            Order order1 = db.Orders.Find(id);
            List<OrderItem> orderItems = db.OrderItems.Where(x => x.OrderID == id).ToList();
            order1.OrderItems = orderItems;

            return View(order1);
        }

        //// GET: Order/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create()
        {
            List<ClotheItem> cart = Session["cart"] as List<ClotheItem>;
            Order order = new Order();
            order.UserID = int.Parse(Session["idUser"].ToString());
            order.Total = (int)cart.Sum(x => x.Total);
            order.OrderItems = new List<OrderItem>();

            foreach (ClotheItem item in cart)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ClotheID = item.ClotheId,
                    Quantity = item.Quantity
                });
            }

            db.Orders.Add(order);
            db.SaveChanges();
            // TODO: Add insert logic here

            return RedirectToAction("Index");
        }



    }
}
