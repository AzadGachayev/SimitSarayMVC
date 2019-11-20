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

    public class AdminMenuCategoriesController : Controller
    {
        private SimitSarayDB db = new SimitSarayDB();



        // GET: SimitAdmin/AdminMenuCategories
        public ActionResult Index()
        {
            return View(db.MenuCategory.ToList());
        }

        // GET: SimitAdmin/AdminMenuCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = db.MenuCategory.Find(id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // GET: SimitAdmin/AdminMenuCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimitAdmin/AdminMenuCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryName,CategoryPhoto")] MenuCategory menuCategory,HttpPostedFileBase Photo )
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Photo.ContentType.ToLower() == "image/jpg" ||
                        Photo.ContentType.ToLower() == "image/jpeg" ||
                        Photo.ContentType.ToLower() == "image/gif" ||
                        Photo.ContentType.ToLower() == "image/png" ||
                        Photo.ContentType.ToLower() == "image/jfif"
                        )
                    {

                        WebImage img = new WebImage(Photo.InputStream);
                        FileInfo imgInfo = new FileInfo(Photo.FileName);
                        string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                        img.Save("~/Public/uploads/CategoryPhoto/" + FileName);
                        menuCategory.CategoryPhoto = "/Public/uploads/CategoryPhoto/" + FileName;
                    }

                }
                db.MenuCategory.Add(menuCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menuCategory);
        }

        // GET: SimitAdmin/AdminMenuCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = db.MenuCategory.Find(id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // POST: SimitAdmin/AdminMenuCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryName,CategoryPhoto")] MenuCategory menuCategory,int? id,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                MenuCategory selectedCategory = db.MenuCategory.SingleOrDefault(mg => mg.ID == id);
                if (Photo != null)
                {
                    if( Photo.ContentType.ToLower()=="image/jpg" ||
                        Photo.ContentType.ToLower() == "image/jpeg" ||
                        Photo.ContentType.ToLower()=="image/gif" ||
                        Photo.ContentType.ToLower() == "image/png" ||
                        Photo.ContentType.ToLower() == "image/jfif"
                        )
                    {

                        if (System.IO.File.Exists(Server.MapPath(menuCategory.CategoryPhoto)))
                    {
                        System.IO.File.Delete(Server.MapPath(menuCategory.CategoryPhoto));
                    }

                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo imgInfo = new FileInfo(Photo.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/CategoryPhoto/" + FileName);
                    selectedCategory.CategoryPhoto = "/Public/uploads/CategoryPhoto/" + FileName;
                }

                }

                selectedCategory.CategoryName=menuCategory.CategoryName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuCategory);
        }

        // GET: SimitAdmin/AdminMenuCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuCategory menuCategory = db.MenuCategory.Find(id);
            if (menuCategory == null)
            {
                return HttpNotFound();
            }
            return View(menuCategory);
        }

        // POST: SimitAdmin/AdminMenuCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuCategory menuCategory = db.MenuCategory.Find(id);
            db.MenuCategory.Remove(menuCategory);
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
