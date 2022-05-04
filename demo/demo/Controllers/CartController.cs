using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;

namespace demo.Controllers
{
    public class CartController : Controller
    {
        List<ClotheItem> cart = new List<ClotheItem>();
        LapTrinhWebEntities db = new LapTrinhWebEntities();

        public ActionResult Index()
        {
            // Tạo đối tượng Session để truyeefnduwx liệu cho view
            List<ClotheItem> cart = Session["cart"] as List<ClotheItem>;
            return View(cart); // Truyền Session với tên (key): cart
        }
        // GET: Cart
        public RedirectToRouteResult AddToCart(int id)
        {
            // Nếu giỏ hàng chưa được khởi tạo
            if (Session["cart"] == null)
            {
                // Khởi tạo Session["cart"] là 1 List<ClotheItem>
                Session["cart"] = new List<ClotheItem>();
            }
            // Gán qua biến cart cho dễ code
            cart = Session["cart"] as List<ClotheItem>;
            // Kiểm tra xem sách khách đang chọn đã có trong giỏ hàng chưa ?
            // Nếu chưa có trong giỏ hàng
            if (cart.FirstOrDefault(m => m.ClotheId == id) == null)
            {
                // Tìm sách theo id
                Clothe book = db.Clothes.Find(id);
                // Tạo ra 1 sách chọn (ClotheItem) mới
                ClotheItem newItem = new ClotheItem(id, book.ClotheName, 1, (double)book.Price, book.Imgcover);
                // Thêm ClotheItem vào giỏ
                cart.Add(newItem);
            }
            // Nếu sách đã có trong giỏ hàng
            else
            {
                // Không thêm vào giỏ nữa mà tăng số lượng lên.
                ClotheItem ClotheItem = cart.FirstOrDefault(m => m.ClotheId == id);
                ClotheItem.Quantity++;
            }
            // Action này sẽ chuyển hướng về trang danh mục để khách chọn tiếp
            // có thể thay bằng lệnh return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("index", "Product");
        }

        public RedirectToRouteResult UpdateQuantity(int? BookId, int newQuantity)
        {
            // gán Session cho biến cart cho dễ code
            List<ClotheItem> cart = Session["cart"] as List<ClotheItem>;
            //tìm ClotheItem muốn sửa và gọi là itemUpdate
            ClotheItem itemUpdate = cart.FirstOrDefault(m => m.ClotheId == BookId);
            // Nếu itemUpdate không null
            if (itemUpdate != null)
            {
                itemUpdate.Quantity = newQuantity; //gán số lượng mới
            }
            // Quay về trang danh mục chọn sách
            return RedirectToAction("Index", "cart");
        }

        // Xóa sách chọn có mã id trong giỏ hàng
        public RedirectToRouteResult RemoveCart(int id)
        {
            List<ClotheItem> cart = Session["cart"] as List<ClotheItem>;
            // Tìm sách có BookId = id và gọi là itemDelete
            ClotheItem itemDelete = cart.FirstOrDefault(m => m.ClotheId == id);
            if (itemDelete != null)
            {
                cart.Remove(itemDelete);
            }
            // Quay về trang danh mục chọn sách
            return RedirectToAction("Index", "cart");
        }

        public ActionResult CheckOut()
        {
            if (Session["idUser"] == null)
            {
                ViewBag.error = "do login";
                return RedirectToAction("Login","Account");
            } else
            {
                return View();
            }

        }
    }
}