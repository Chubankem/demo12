using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.Models
{
    public class CartItem
    {
        public Clothe _shopping_clothe { get; set; }
        public int _shopping_quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add(Clothe _clo,int _quanlity = 1)
        {
            var item = Items.FirstOrDefault(s => s._shopping_clothe.ClotheID == _clo.ClotheID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_clothe = _clo,
                    _shopping_quantity = _quanlity
                });
            }
            else
            {
                item._shopping_quantity += _quanlity;
            }
        }

    }
}