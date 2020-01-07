using Simit_Saray.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Simit_Saray.Areas.SimitAdmin.Controllers
{
    public class AdminReservController : Controller
    {
        SimitSarayDB db = new SimitSarayDB();
        // GET: SimitAdmin/AdminReserv
        public ActionResult Reserv()
        {
            return View(db.Reservation.ToList());
        }
    }
}