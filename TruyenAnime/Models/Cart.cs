using System.Collections.Generic;
using System.Linq;

namespace TruyenAnime.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(Product product, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product != null && i.Product.Id == product.Id);
            if (item == null)
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.Product != null && i.Product.Id == productId);
        }

        public void UpdateItem(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product != null && i.Product.Id == productId);
            if (item != null)
            { 
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    RemoveItem(productId);
                }
            }
        }

        public decimal GetTotal()
        {
            return Items.Where(i => i.Product != null).Sum(i => i.Product.Price * i.Quantity);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
