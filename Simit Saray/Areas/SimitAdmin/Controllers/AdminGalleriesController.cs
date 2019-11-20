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

    public class AdminGalleriesController : Controller
    {
        private SimitSarayDB db = new SimitSarayDB();



        // GET: SimitAdmin/AdminGalleries
        public ActionResult Index()
        {
            return View(db.Gallery.ToList());
        }

        // GET: SimitAdmin/AdminGalleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: SimitAdmin/AdminGalleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimitAdmin/AdminGalleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Header,Description,Photo")] Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                db.Gallery.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // GET: SimitAdmin/AdminGalleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: SimitAdmin/AdminGalleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Header,Description,Photo")] Gallery gallery, int? id, HttpPostedFileBase Photo )
        {
            if (ModelState.IsValid)
            {
                Gallery selectedGallery = db.Gallery.SingleOrDefault(gl => gl.ID == id);
                if (Photo != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedGallery.Photo)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedGallery.Photo));
                    }

                    WebImage img = new WebImage(Photo.InputStream);
                    FileInfo imgInfo = new FileInfo(Photo.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/Gallery/" + FileName);
                    selectedGallery.Photo = "/Public/uploads/Gallery/" + FileName;
                }
                selectedGallery.Description = gallery.Description;
                selectedGallery.Header = gallery.Header;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }



        // GET: SimitAdmin/AdminGalleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: SimitAdmin/AdminGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Gallery.Find(id);
            db.Gallery.Remove(gallery);
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
