using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.Models
{
    public class ClotheItem
    {
        int clotheId;
        string name;        // Tiêu đề
        int quantity;        // số lượng
        double price;        // Giá bán
        string coverPage;    // Hình bìa        
        double total;        // Thành tiền
        // Propeties
        public int ClotheId { get => clotheId; set => clotheId = value; }
        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }
        public string CoverPage { get => coverPage; set => coverPage = value; }
        // Tổng = đơn giá * số lượng
        public double Total { get => price * Quantity; }
        // Constructor
        public ClotheItem() { }
        public ClotheItem(int id, string title, int quality, double price, string img)
        {
            this.clotheId = id;
            this.name = title;
            this.quantity = quality;
            this.price = price;
            this.coverPage = img;
            this.total = quality * price;
        }
    }
}