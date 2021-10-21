using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using ViewModels;

namespace PerfectShop.Controllers
{
    public class ShopCartController : Controller
    {
        // GET: ShopCart
        UnitOfWork db = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }

        List<ShowOrderViewModel> GetOrderList()
        {

            List<ViewModels.ShowOrderViewModel> list = new List<ViewModels.ShowOrderViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<ViewModels.ShopCartViewModel> ListItem = Session["ShopCart"] as List<ViewModels.ShopCartViewModel>;

                foreach (var item in ListItem)
                {
                    var product = db.ProductRepositoryBase.Get(p => p.ProductID == item.productID).Select(p => new { p.Price, p.ImageName, p.ProductTitle, }).Single();
                    list.Add(new ViewModels.ShowOrderViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.productID,
                        ImageName = product.ImageName,
                        Price = product.Price,
                        Title = product.ProductTitle,
                        Sum = item.Count * product.Price
                    });
                }
            }
            return list;
        }
        public ActionResult Order()
        {
            return PartialView(GetOrderList());
        }

        public ActionResult CommandOrder(int id, int count)
        {
            List<ShopCartViewModel> listItem = Session["ShopCart"] as List<ShopCartViewModel>;
            int index = listItem.FindIndex(p => p.productID == id);
            if (count==0)
            {
                listItem.RemoveAt(index);
            }
            else
            {
                listItem[index].Count = count;
            }


            return PartialView("Order", GetOrderList());
        }


        public ActionResult Payment()
        {
            int UserID = db.userRepository.GetUserByUserName(User.Identity.Name).UserID;

            Orders orders = new Orders() {
                Date = DateTime.Now,
                UserID = UserID,
                IsFinally = false
            };

            db.OrdersRepositoryBase.Insert(orders);

            var listOrderProducts = GetOrderList();
            foreach (var item in listOrderProducts)
            {
                db.OrderDetailsRepositoryBase.Insert(new OrderDetails()
                {
                    OrderID = orders.OrderID,
                    ProductID = item.ProductID,
                    Price = item.Price,
                    Count = item.Count
                }); 
            }

            db.Save();



            return null;
        }


    }
}