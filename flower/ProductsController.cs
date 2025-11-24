using bonghoa.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace bonghoa.Controllers
{
    public class ProductsController : Controller
    {
        private context db = new context();

        // GET: Products
        public ActionResult Index(decimal? keyword)
        {
            var sp = db.Products.AsQueryable();
            if(keyword != null)
            {
                sp = sp.Where(m=>m.Price==keyword);
            }
            
            return View(sp.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pid,Categoryid,ProdName,MetaTitle,Description,ImagePath,Price")] Product product, HttpPostedFileBase imgfile)
        {
            if (imgfile == null || imgfile.ContentLength == 0) {
                ModelState.AddModelError("ImagePath", "Bạn chưa chọn ảnh!");
            }
            else
            {
                // Lưu ảnh vào server
                var fileName = Path.GetFileName(imgfile.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                imgfile.SaveAs(path);

                // Gán tên file vào model
                product.ImagePath = fileName;

                ModelState["ImagePath"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pid,Categoryid,ProdName,MetaTitle,Description,ImagePath,Price")] Product product, HttpPostedFileBase imgfile)
        {
            if(imgfile != null && imgfile.ContentLength > 0)
            {
                var filename = Path.GetFileName(imgfile.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), filename);
                imgfile.SaveAs(path);
                product.ImagePath = filename;
            }
            else
                product.ImagePath = db.Products.AsNoTracking().FirstOrDefault(p => p.Pid == product.Pid)?.ImagePath;
            ModelState["ImagePath"]?.Errors.Clear();
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                string path = Server.MapPath("~/Images/" + product.ImagePath);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);   // xóa ảnh
                }
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
