using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace PerfectShop.Areas.Admin.Controllers
{
    public class ProductGroupsController : Controller
    {
        private UnitOfWork _db = new UnitOfWork();

        // GET: Admin/ProductGroups
        public ActionResult Index()
        {

            return View(_db.ProductGroupsRepositoryBase.Get(u => u.ParentID == null));
        }

        // GET: Admin/ProductGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = _db.ProductGroupsRepositoryBase.GetByID(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // GET: Admin/ProductGroups/Create
        public ActionResult Create(int? id)
        {

            return PartialView(new ProductGroup() { ParentID = id });
        }

        // POST: Admin/ProductGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle,ParentID")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                _db.ProductGroupsRepositoryBase.Insert(productGroup);
                _db.Save();
                return PartialView("ListGroup", _db.ProductGroupsRepositoryBase.Get(u => u.ParentID == null));
                
            }

            return View(productGroup);
        }

        // GET: Admin/ProductGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = _db.ProductGroupsRepositoryBase.GetByID(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // POST: Admin/ProductGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupTitle,ParentID")] ProductGroup productGroup)
        {
            if (ModelState.IsValid)
            {
                _db.ProductGroupsRepositoryBase.Update(productGroup);
                _db.Save();
                return PartialView("ListGroup", _db.ProductGroupsRepositoryBase.Get(u => u.ParentID == null));
            }
            return View(productGroup);
        }

        // GET: Admin/ProductGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = _db.ProductGroupsRepositoryBase.GetByID(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // POST: Admin/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductGroup productGroup = _db.ProductGroupsRepositoryBase.GetByID(id);
            _db.ProductGroupsRepositoryBase.Delete(productGroup);
            _db.Save();
            return PartialView("ListGroup", _db.ProductGroupsRepositoryBase.Get(u => u.ParentID == null));
        }

        public ActionResult ListGroup()
        {
            var productGroups = _db.ProductGroupsRepositoryBase.Get(u => u.ParentID==null);
            return PartialView(productGroups);
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
