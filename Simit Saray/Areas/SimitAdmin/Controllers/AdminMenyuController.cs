using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Simit_Saray.Models;

namespace Simit_Saray.Areas.SimitAdmin.Controllers
{
    [AuthorizationFilterController]

    public class AdminMenyuController : Controller
    {
        private SimitSarayDB db = new SimitSarayDB();



        // GET: SimitAdmin/AdminMenyu
        public ActionResult Index()
        {
            var menyu = db.Menyu.Include(m => m.MenuCategory);
            return View(menyu.ToList());
        }

        // GET: SimitAdmin/AdminMenyu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menyu menyu = db.Menyu.Find(id);
            if (menyu == null)
            {
                return HttpNotFound();
            }
            return View(menyu);
        }

        // GET: SimitAdmin/AdminMenyu/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.MenuCategory, "ID", "CategoryName");
            return View();
        }

        // POST: SimitAdmin/AdminMenyu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Ingridient,Photo,CategoryID")] Menyu menyu,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo imgInfo = new FileInfo(Photo.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/Menu/" + FileName);
                    menyu.Photo = "/Public/uploads/Menu/" + FileName;
                }
                db.Menyu.Add(menyu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.MenuCategory, "ID", "CategoryName", menyu.CategoryID);
            return View(menyu);
        }

        // GET: SimitAdmin/AdminMenyu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menyu menyu = db.Menyu.Find(id);
            if (menyu == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.MenuCategory, "ID", "CategoryName", menyu.CategoryID);
            return View(menyu);
        }

        // POST: SimitAdmin/AdminMenyu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Ingridient,Photo,CategoryID")] Menyu menyu,int? id, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                Menyu selectedMenu = db.Menyu.SingleOrDefault(mn => mn.ID == id);
                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedMenu.Photo)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedMenu.Photo));
                    }
                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo imgInfo = new FileInfo(Photo.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/Menu/" + FileName);
                    selectedMenu.Photo = "/Public/uploads/Menu/" + FileName;
                }
                selectedMenu.Name = menyu.Name;
                selectedMenu.Price = menyu.Price;
                selectedMenu.Ingridient = menyu.Ingridient;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.MenuCategory, "ID", "CategoryName", menyu.CategoryID);
            return View(menyu);
        }

        // GET: SimitAdmin/AdminMenyu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menyu menyu = db.Menyu.Find(id);
            if (menyu == null)
            {
                return HttpNotFound();
            }
            return View(menyu);
        }

        // POST: SimitAdmin/AdminMenyu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menyu menyu = db.Menyu.Find(id);
            db.Menyu.Remove(menyu);
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
