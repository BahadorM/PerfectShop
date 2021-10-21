using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace PerfectShop.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ShowProduct()
        {
            return PartialView(db.ProductRepositoryBase.Get().Take(6));
        }
        public ActionResult Products()
        {
            return View(db.ProductRepositoryBase.Get());
        }
        public ActionResult SingleProductPage(int? id)
        {
            if (id == null)
            {
                HttpNotFound();
            }

            return View(db.ProductRepositoryBase.GetByID(id));
        }
    }
}