using Simit_Saray.Models;
using Simit_Saray.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Simit_Saray.Controllers
{
    public class HomeController : Controller
    {
        SimitSarayDB DB = new SimitSarayDB();
        public ActionResult Index()
        {
            var defaultModel = new DefaultViewModel
            {
                EsasSehife = DB.HomeHeader.FirstOrDefault(),
                NavBackground = DB.HomeNavbar.FirstOrDefault(),
                KateqoriyaMenyu=DB.MenuCategory.ToList(),
                homeHeader=DB.HomeHeader.FirstOrDefault(),
                Kontakt = DB.Contact.FirstOrDefault(),
                Haqqinda = DB.About.FirstOrDefault(),
            };
            return View(defaultModel);
        }
        public ActionResult Reservation()
        {
            

            return PartialView();
        }
        [HttpPost]
        public JsonResult Reservation(Reservation res,Profil user)
        {

            if (user.FullName != null)
            {
                
                    int profilId = 0;
                    Profil selectedProfil = DB.Profil.FirstOrDefault(pr => pr.Phone == user.Phone);
                    if (selectedProfil == null)
                    {
                        Profil profile = DB.Profil.Add(new Profil
                        {
                            Phone = user.Phone,
                            Email = user.Email,
                            FullName = user.FullName
                        });
                        DB.SaveChanges();
                        profilId = profile.ID;
                    }
                    else
                    {
                        profilId = selectedProfil.ID;
                    }
                    DB.Reservation.Add(new Reservation
                    {
                        UserID = profilId,
                        ComeDate = res.ComeDate,
                        ComingHours = res.ComingHours,
                        Amount = res.Amount,
                    });
                    DB.SaveChanges();
                try
                {
                  
                        var senderEmail = new MailAddress(user.Email, user.FullName);
                        var receiverEmail = new MailAddress("azad.gachayev@gmail.com", "Azad");
                        var password = "mediterranean92";
                        var sub = "Reservasiya";
                        var body = res.Amount+ " neferlik";
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                    }
                
                catch (Exception)
                {
                    ViewBag.Error = "Some Error";
                }

            }

            else
            {
                return Json(new
                {
                    status = 404,
                    errorFullName = true,
                    fullname = "Zəhmət olmasa adınızı qeyd edin!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (user.Phone == null)
            {
                return Json(new
                {
                    status = 404,
                    errorPhone = true,
                    phone = "Telefon nömrəsini yazın!"
                }, JsonRequestBehavior.AllowGet);
             
            }
            return Json(new{ 
                message="success",
                status=200,
                response="",
                error=false
            },JsonRequestBehavior.AllowGet);
        }
        public ActionResult Menyu()
        {
            var defaultModel = new DefaultViewModel
            {
                homeHeader = DB.HomeHeader.FirstOrDefault(),
                Menyum = DB.Menyu.ToList(),
                NavBackground = DB.HomeNavbar.FirstOrDefault(),
                KateqoriyaMenyu = DB.MenuCategory.ToList(),
            };
            return View(defaultModel);
        }

        public ActionResult Blog()
        {
            var defaultModel = new DefaultViewModel
            {
                homeHeader = DB.HomeHeader.FirstOrDefault(),
                BloqSehife = DB.BlogItem.OrderByDescending(bl=>bl.CreateDate).ToList(),
                NavBackground = DB.HomeNavbar.FirstOrDefault(),

            };
            return View(defaultModel);
        }
        public ActionResult Gallery()
        {
            var defaultModel = new DefaultViewModel
            {
                NavBackground = DB.HomeNavbar.FirstOrDefault(),
                homeHeader = DB.HomeHeader.FirstOrDefault(),
                Qalereya = DB.Gallery.ToList(),
                Qalereya2 = DB.Gallery2.ToList(),

            };

            return View(defaultModel);
        }

       

        public ActionResult Contact()
        {
            var defaultModel = new DefaultViewModel
            {
                homeHeader = DB.HomeHeader.FirstOrDefault(),
                NavBackground = DB.HomeNavbar.FirstOrDefault(),
                Kontakt = DB.Contact.FirstOrDefault(),
            };
           

            return View(defaultModel);
        }
        public ActionResult About()
        {
            var defaultModel = new DefaultViewModel
            {
                NavBackground = DB.HomeNavbar.FirstOrDefault(),
                homeHeader = DB.HomeHeader.FirstOrDefault(),
                Haqqinda = DB.About.FirstOrDefault(),

            };

            return View(defaultModel);
        }
        public ActionResult BlogItem(int? id) 
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            BlogItem blogItm = DB.BlogItem.FirstOrDefault(bl => bl.ID == id);
            if (blogItm == null)
            {
                return HttpNotFound();
            }
            var defaultModel = new DefaultViewModel
            {
                NavBackground = DB.HomeNavbar.FirstOrDefault(),
                homeHeader = DB.HomeHeader.FirstOrDefault(),
                blogDetail = blogItm
            };

            return View(defaultModel);
        }

    }
}