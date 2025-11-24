using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using traicay.Models;

namespace traicay.Controllers
{
    public class SanPhamsController : Controller
    {
        private context db = new context();

        // GET: SanPhams
        public ActionResult gethsx()
        {
            return View(db.HangSanXuats.ToList());
        }
        public ActionResult Index(string tenhang)
        {
            var sanPhams = db.SanPhams.Include(s => s.HangSanXuat);
            
            if(tenhang != null)
            {
                sanPhams = sanPhams.Where(m=>m.TenSP.Contains(tenhang));
                if(!sanPhams.Any())
                {
                    ModelState.AddModelError("tenhang", "Không tìm thấy sản phẩm");
                }
            }
            return View(sanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,HinhAnh,MaHang")] SanPham sanPham, HttpPostedFileBase imgfile)
        {
            var check = db.SanPhams.FirstOrDefault(x => x.MaSP == sanPham.MaSP);
            if (check != null)
            {
                ModelState.AddModelError("MaSP", "Mã sản phẩm đã tồn tại");
            }

            if (imgfile != null && imgfile.ContentLength > 0)
            {
                var filename = Path.GetFileName(imgfile.FileName);
                var path = Path.Combine(Server.MapPath("~/images"), filename);
                imgfile.SaveAs(path);
                sanPham.HinhAnh = filename;
                ModelState["Hinhanh"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", sanPham.MaHang);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", sanPham.MaHang);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,HinhAnh,MaHang")] SanPham sanPham, HttpPostedFileBase imgfile)
        {
            if(imgfile!=null && imgfile.ContentLength > 0)
            {
                var filename = Path.GetFileName(imgfile.FileName);
                var path = Path.Combine(Server.MapPath("~/images"), filename);
                imgfile.SaveAs(path);
                sanPham.HinhAnh = filename;
                ModelState["Hinhanh"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", sanPham.MaHang);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
