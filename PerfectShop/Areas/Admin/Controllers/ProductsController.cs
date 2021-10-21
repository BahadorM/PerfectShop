using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.IO;
using Utilities;

namespace PerfectShop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private UnitOfWork _db = new UnitOfWork();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(_db.ProductRepositoryBase.Get());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = _db.ProductRepositoryBase.GetByID(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.Groups = _db.ProductGroupsRepositoryBase.Get();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductTitle,ShortDescrioption,Text,Price,ImageName,CreateDate")] Products products,List<int> SelectedGroup, HttpPostedFileBase ImgUp,string tags)
        {

            if (ModelState.IsValid)
            {
                if (SelectedGroup == null)
                {
                    ViewBag.errorSelectedeGroup = true;
                    ViewBag.Groups = _db.ProductGroupsRepositoryBase.Get();
                    return View(products);
                }
                products.ImageName = "no-image.jpg";
                if (ImgUp != null)
                {
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                    ImgUp.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                    ImageResizer imageResizer = new ImageResizer();
                    imageResizer.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName), Server.MapPath("/Images/ProductImages/Thumb/" + products.ImageName));

                }
                products.CreateDate = DateTime.Now;
                _db.ProductRepositoryBase.Insert(products);
                _db.Save();

                var productId = _db.ProductRepositoryBase.Get().OrderByDescending(c => c.ProductID).Take(1).Select(g => g.ProductID).SingleOrDefault();

                foreach (var item in SelectedGroup)
                {
                    ProductSelectGroup productSelectGroup = new ProductSelectGroup {
                        GroupID = item,
                        ProdctID = productId

                    };
                    _db.SelectedGroupRepositoryBase.Insert(productSelectGroup);
                }

                if (!string.IsNullOrEmpty(tags))
                {
                    string[] tag = tags.Split(',');
                    foreach (var item in tag)
                    {
                        ProductTag Ptag = new ProductTag { ProductID = productId, Tag = item.Trim() };
                        _db.productTagRepositoryBase.Insert(Ptag);
                    }
                }

                _db.Save();

                return RedirectToAction("Index");
            }

            ViewBag.Groups = _db.ProductGroupsRepositoryBase.Get();
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = _db.ProductRepositoryBase.GetByID(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupsChecked = _db.SelectedGroupRepositoryBase.Get(s => s.ProdctID == id);
            ViewBag.Groups = _db.ProductGroupsRepositoryBase.Get();
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductTitle,ShortDescrioption,Text,Price,ImageName,CreateDate")] Products products,List<int> SelectedGroup, HttpPostedFileBase ImgUp,string tags)
        {
            if (SelectedGroup == null)
            {
                ViewBag.errorSelectedeGroup = true;
                ViewBag.Groups = _db.ProductGroupsRepositoryBase.Get();
                return View(products);
            }
            if (ModelState.IsValid)
            {

                if (SelectedGroup == null)
                {
                    ViewBag.errorSelectedeGroup = true;
                    return View(products);
                }
                var selectGroup = _db.SelectedGroupRepositoryBase.Get(g => g.ProdctID == products.ProductID);
                foreach (var item in selectGroup)
                {
                    _db.SelectedGroupRepositoryBase.Delete(item.PG_ID);
                }

                foreach (var item in SelectedGroup)
                {
                    ProductSelectGroup SGroup = new ProductSelectGroup() { GroupID = item, ProdctID = products.ProductID };
                    _db.SelectedGroupRepositoryBase.Insert(SGroup);
                }
                var tag = _db.productTagRepositoryBase.Get(t => t.ProductID == products.ProductID);
                foreach (var item in tag)
                {
                    _db.productTagRepositoryBase.Delete(item);
                }

                if (!string.IsNullOrEmpty(tags))
                {
                    string[] _tags = tags.Split(',');
                    foreach (var item in _tags)
                    {
                        ProductTag tagg = new ProductTag() { ProductID = products.ProductID, Tag = item };
                        _db.productTagRepositoryBase.Insert(tagg);
                    }
                }

                if (ImgUp != null)
                {
                    string lastPic = products.ImageName;
                    products.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                    ImgUp.SaveAs(Server.MapPath("/Images/ProductImages/" + products.ImageName));
                    ImageResizer image = new ImageResizer();
                    image.Resize(Server.MapPath("/Images/ProductImages/" + products.ImageName), Server.MapPath("/Images/ProductImages/Thumb/" + products.ImageName));
                    if (lastPic != "no-image.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumb/" + lastPic));
                        System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + lastPic));
                    }
                }
               



                _db.ProductRepositoryBase.Update(products);



                _db.Save();

                return RedirectToAction("Index");
            }
            ViewBag.GroupsChecked = _db.SelectedGroupRepositoryBase.Get(s => s.ProdctID == products.ProductID);
            ViewBag.Groups = _db.ProductGroupsRepositoryBase.Get();
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = _db.ProductRepositoryBase.GetByID(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var ProductTag = _db.productTagRepositoryBase.Get(t => t.ProductID == id);
            if (ProductTag != null)
            {
                foreach (var item in ProductTag)
                {
                    _db.productTagRepositoryBase.Delete(item);
                }
            }
            var selectGroup = _db.SelectedGroupRepositoryBase.Get(g => g.ProdctID == id);
            foreach (var item in selectGroup)
            {
                _db.SelectedGroupRepositoryBase.Delete(item);
            }

            Products product = _db.ProductRepositoryBase.GetByID(id);


            if (product.ImageName!= "no-image.jpg")
            {
                System.IO.File.Delete(Server.MapPath("/Images/ProductImages/" + product.ImageName));
                System.IO.File.Delete(Server.MapPath("/Images/ProductImages/Thumb/" + product.ImageName));
            }

            _db.ProductRepositoryBase.Delete(product);
            _db.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
