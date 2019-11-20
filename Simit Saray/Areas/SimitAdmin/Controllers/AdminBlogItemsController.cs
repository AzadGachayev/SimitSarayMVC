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

    public class AdminBlogItemsController : Controller
    {
        private SimitSarayDB db = new SimitSarayDB();

        // GET: SimitAdmin/AdminBlogItems
        public ActionResult Index()
        {
            return View(db.BlogItem.ToList());
        }

        // GET: SimitAdmin/AdminBlogItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogItem blogItem = db.BlogItem.Find(id);
            if (blogItem == null)
            {
                return HttpNotFound();
            }
            return View(blogItem);
        }

        // GET: SimitAdmin/AdminBlogItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SimitAdmin/AdminBlogItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Photo,Header,Description,CreateDate,Photo,AuthorName")] BlogItem blogItem,HttpPostedFileBase Image,DateTime? mainDate)
        {
          
            if (ModelState.IsValid)
            {
                if (mainDate == null)
                {
                    blogItem.CreateDate = DateTime.Now;
                }
                else
                {
                    blogItem.CreateDate = (DateTime)mainDate;
                }
                if (Image != null)
                {
                 
                    WebImage img = new WebImage(Image.InputStream);
                    FileInfo imgInfo = new FileInfo(Image.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/BlogPhoto/" + FileName);
                    blogItem.Photo = "/Public/uploads/BlogPhoto/" + FileName;

                }
                db.BlogItem.Add(blogItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                return View(blogItem);

        }

        // GET: SimitAdmin/AdminBlogItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogItem blogItem = db.BlogItem.Find(id);
            if (blogItem == null)
            {
                return HttpNotFound();
            }
            return View(blogItem);
        }

        // POST: SimitAdmin/AdminBlogItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Photo,Header,Description,CreateDate,AuthorName,Photo")] BlogItem blogItem,DateTime? mainDate,HttpPostedFileBase Image,int id)
        {
            if (ModelState.IsValid)
            {
                if (mainDate == null)
                {
                    blogItem.CreateDate = DateTime.Now;
                }
                else
                {
                    blogItem.CreateDate = (DateTime)mainDate;
                }
                BlogItem selectedBlog = db.BlogItem.SingleOrDefault(nvb => nvb.ID == id);
                if (Image!= null)
                {
                    if (System.IO.File.Exists(Server.MapPath(selectedBlog.Photo)))
                    {
                        System.IO.File.Delete(Server.MapPath(selectedBlog.Photo));
                    }

                    WebImage img = new WebImage(Image.InputStream);
                    FileInfo imgInfo = new FileInfo(Image.FileName);
                    string FileName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    img.Save("~/Public/uploads/BlogPhoto/" + FileName);
                    selectedBlog.Photo= "/Public/uploads/BlogPhoto/" + FileName;

                }
                selectedBlog.Description = blogItem.Description;
                selectedBlog.CreateDate = blogItem.CreateDate;
                selectedBlog.Header = blogItem.Header;
                selectedBlog.AuthorName = blogItem.AuthorName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogItem);
        }

        // GET: SimitAdmin/AdminBlogItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogItem blogItem = db.BlogItem.Find(id);
            if (blogItem == null)
            {
                return HttpNotFound();
            }
            return View(blogItem);
        }

        // POST: SimitAdmin/AdminBlogItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogItem blogItem = db.BlogItem.Find(id);
            db.BlogItem.Remove(blogItem);
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
