using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ViewModels;

namespace PerfectShop.Controllers
{
    public class ShopController : ApiController
    {
        // GET: api/Shop
        public int Get()
        {
            List<ViewModels.ShopCartViewModel> list = new List<ShopCartViewModel>();
            var _sessions = HttpContext.Current.Session;
            if (_sessions["ShopCart"] != null)
            {
                list = _sessions["ShopCart"] as List<ShopCartViewModel>;
            }

            return list.Sum(l => l.Count);
        }

        // GET: api/Shop/5
        public int Get(int id)
        {
            List<ViewModels.ShopCartViewModel> list = new List<ShopCartViewModel>();
            var _sessions = HttpContext.Current.Session;
            if (_sessions["ShopCart"] != null)
            {
                list = _sessions["ShopCart"] as List<ShopCartViewModel>;
            }
            if (list.Any(l => l.productID == id)) 
            {
                int index = list.FindIndex(l => l.productID == id);
                list[index].Count += 1;
            }
            else
            {
                list.Add(new ShopCartViewModel() { productID=id, Count=1 });
            }
            _sessions["ShopCart"] = list;
            return Get();
        }

    }
}
