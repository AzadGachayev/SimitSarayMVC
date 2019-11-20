using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Simit_Saray.Models;

namespace Simit_Saray.Areas.SimitAdmin.Controllers
{
    [AuthorizationFilterController]
    public class AdminReservationsController : Controller
    {
        private SimitSarayDB db = new SimitSarayDB();

        // GET: SimitAdmin/AdminReservations
        public async Task<ActionResult> Index()
        {
            var reservation = db.Reservation.Include(r => r.Profil);
            return View(await reservation.ToListAsync());
        }

        // GET: SimitAdmin/AdminReservations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: SimitAdmin/AdminReservations/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Profil, "ID", "FullName");
            return View();
        }

        // POST: SimitAdmin/AdminReservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,UserID,Amount,ComeDate,ComingHours")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservation.Add(reservation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Profil, "ID", "FullName", reservation.UserID);
            return View(reservation);
        }

        // GET: SimitAdmin/AdminReservations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Profil, "ID", "FullName", reservation.UserID);
            return View(reservation);
        }

        // POST: SimitAdmin/AdminReservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,UserID,Amount,ComeDate,ComingHours")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Profil, "ID", "FullName", reservation.UserID);
            return View(reservation);
        }

        // GET: SimitAdmin/AdminReservations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: SimitAdmin/AdminReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reservation reservation = await db.Reservation.FindAsync(id);
            db.Reservation.Remove(reservation);
            await db.SaveChangesAsync();
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
