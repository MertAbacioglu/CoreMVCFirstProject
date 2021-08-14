using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Tools
{
    [Serializable]
    public class Cart
    {
        [JsonProperty("_myCart")]
        Dictionary<int, CartItem> _myCart;
        public Cart()
        {
            _myCart = new Dictionary<int, CartItem>();
        }

        [JsonProperty("MyCart")]
        public List<CartItem> MyCart
        {
            get
            {
                return _myCart.Values.ToList();
            }
        }

        public void SepeteEkle(CartItem item)
        {
            if (_myCart.ContainsKey(item.ID))
            {
                _myCart[item.ID].Amount += 1;
                return;
            }
            _myCart.Add(item.ID, item);
        }

        public void SepettenSil(int id)
        {
            if (_myCart[id].Amount > 1)
            {
                _myCart[id].Amount -= 1;
            }
            _myCart.Remove(id);
        }
        [JsonProperty("TotalPrice")]
        public decimal? TotalPrice
        {
            get
            {
                return _myCart.Sum(x => x.Value.SubTotal);
            }
        }
    }
    
}
