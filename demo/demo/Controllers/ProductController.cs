using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demo.Models;
using System.Dynamic;

namespace demo.Controllers
{
    public class ProductController : Controller
    {
        private LapTrinhWebEntities db = new LapTrinhWebEntities();
        public int temp = 0;
        // GET: Product
        public ActionResult Index(int? temp)
        {
            temp = temp ?? 0;
            dynamic dy = new ExpandoObject();
            dy.ClotheList = GetClothes();
            dy.TypeList = GetTypes();
            dy.temp = temp;
            return View(dy);
        }
        public List<Models.Type> GetTypes()
        {
            List<Models.Type> LType = db.Types.ToList();
            return LType;
        }
        public List<Clothe> GetClothes()
        {
            List<Clothe> LClothe = db.Clothes.ToList();
            return LClothe;
        }

        public Cart GetCart()
        {
            Cart cart = Session ["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddtoCart(int id)
        {
            var pro = db.Clothes.SingleOrDefault(s => s.ClotheID == id);
            if(pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowtoCart", "Product"); 
        }

        public ActionResult ShowtoCart()
        {
            if (Session["Cart"] == null)
                return View();
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clothe clothe = db.Clothes.Find(id);
            if (clothe == null)
            {
                return HttpNotFound();
            }
            return View(clothe);
        }

        public ActionResult SearchProduct(string SearchString)
        {
            var books = from s in db.Clothes
                        select s;
            if (!String.IsNullOrEmpty(SearchString))
            {
                books = books.Where(s => s.ClotheName.Contains(SearchString));
            }
            return View(books.ToList());
        }

        public List<Models.Wishlist> GetWishList()
        {
            List<Models.Wishlist> LWishList = db.Wishlists.ToList();
            return LWishList;
        }
        public ActionResult WishList()
        {
            if (Session["idUser"] == null)
            {
                ViewBag.error = "do login";
                return RedirectToAction("Login", "Account");
            }
            else
            {

                dynamic dy = new ExpandoObject();
                dy.ClotheList = GetClothes();
                dy.WishList = GetWishList();
                return View(dy);
            }          
        }
        public ActionResult AddToWishList(string userID , int clotheID)
        {
            if (Session["idUser"] == null)
            {
                ViewBag.error = "do login";
                return RedirectToAction("Login", "Account");
            }
            Wishlist wishlist = new Wishlist();
            int temp = db.Wishlists.Count();
            wishlist.UserID = Convert.ToInt32(userID);
            wishlist.ClotheID = clotheID;
            wishlist.WishistID = temp;
            if (ModelState.IsValid)
            {
                for (int i = 0; i < db.Users.Count()-1; i++)
                {
                    var checkUser = db.Wishlists.FirstOrDefault(s => s.UserID == wishlist.UserID);
                    if (checkUser != null)
                    {
                        
                        var checkClothe = db.Wishlists.FirstOrDefault(s => s.ClotheID == wishlist.ClotheID);
                        if (checkClothe != null)
                        {
                            return RedirectToAction("Details", "Product", new { id = wishlist.ClotheID});
                        }
                    }
                }

                db.Wishlists.Add(wishlist);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Product", new { id = wishlist.ClotheID});
        }

        public ActionResult DeleteWishList(int id)
        {
            Wishlist product = db.Wishlists.Find(id);
            for(int i = id;i<db.Wishlists.Count()-1;i++)
            {
                //asdfadgsfghzdfgeedfasdadfGzdfzhzdfgsdfDAadDfsrgdgznxcvbsf
            }
            db.Wishlists.Remove(product);
            db.SaveChanges();
            return RedirectToAction("WishList", "Product");
        }
        public RedirectToRouteResult GetClotheItem()
        {
            return RedirectToAction("WishList", "Product");
        }

        public ActionResult Costume(int? temp)
        {
            temp = temp ?? 0;
            dynamic dy = new ExpandoObject();
            dy.ClotheList = GetClothes();
            dy.TypeList = GetTypes();
            dy.temp = temp;
            return View(dy);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
