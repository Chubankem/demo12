using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;

namespace demo.Controllers
{
    public class ReviewController : Controller
    {
        LapTrinhWebEntities db = new LapTrinhWebEntities();

        // GET: Review
        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Review review)
        {
            int orderId = int.Parse(TempData["orderId"].ToString());

            Order order = db.Orders.Include("OrderItems").Where(x => x.OrderID == orderId).First();

            return View();
        }

    }
}