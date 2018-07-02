using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wedding_Planner.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Wedding_Planner.Controllers
{
    public class WeddingPlannerController : Controller
    {
        private WeddingContext _context;
        public WeddingPlannerController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {   
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if(loggedId == null){
                return RedirectToAction("Index", "LoginReg");
            }
            
            User logged = _context.Users.Include(make=>make.CreatedWeddings).Include(go=>go.Attending).ThenInclude(where=>where.weddings).Where(we => we.id == loggedId).SingleOrDefault();
            List<Wedding> weddings = _context.Weddings.Include(people=>people.Guests).ThenInclude(who=>who.users).OrderByDescending(time=>time.date_of_wedding).ToList();

            foreach(Wedding wed in weddings){
                if(wed.date_of_wedding.Date < DateTime.Now.Date){
                    int wedNum = wed.id;
                    DeleteWedding(wedNum);          
                    return RedirectToAction("Dashboard");
                }
            }
            
            weddings = _context.Weddings.Include(people=>people.Guests).ThenInclude(who=>who.users).OrderByDescending(time=>time.date_of_wedding).ToList();

            ViewBag.Weddings = weddings;
            ViewBag.Logged = logged;
            
            return View();
        }

        [HttpGet]
        [Route("wedding/{weddingId}")]
        public IActionResult WeddingInfo(int weddingId)
        {   
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if(loggedId == null){
                return RedirectToAction("Index", "LoginReg");
            }
            
            Wedding wedding = _context.Weddings.Include(people=>people.Guests).ThenInclude(who=>who.users).SingleOrDefault(which=>which.id == weddingId);

            ViewBag.Wedding = wedding;
            ViewBag.stuff = "AIzaSyAyNKE0jDnE4_l8Kmyfs53ourak8DRtA9U";
            return View();
        }

        [HttpGet]
        [Route("new_wedding")]
        public IActionResult NewWedding()
        {   
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if(loggedId == null){
                return RedirectToAction("Index", "LoginReg");
            }

            return View();
        }

        [HttpGet]
        [HttpPost]
        [Route("create_wedding")]
        public IActionResult CreateWedding(WeddingCreate info)
        {
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if(loggedId == null){
                return RedirectToAction("Index", "LoginReg");
            }
            
            if(ModelState.IsValid)
            {
                User logged = _context.Users.SingleOrDefault(we => we.id == loggedId);             

                Wedding newWed = new Wedding
                {
                    bride = info.bride,
                    groom = info.groom,
                    date_of_wedding = info.date_of_wedding,
                    weddingLocation = info.weddingLocation,
                    userId = logged.id,
                    user = logged
                };
                _context.Weddings.Add(newWed);
                _context.SaveChanges();
                
                Guest newGuest = new Guest
                {
                    weddingsId = newWed.id,
                    weddings = newWed,
                    usersId = logged.id,
                    users = logged,
                };
                _context.Guests.Add(newGuest);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("NewWedding", info);
        }

        [HttpGet]
        [Route("delete_wedding/{weddingId}")]
        public IActionResult DeleteWedding(int weddingId)
        {   
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if(loggedId == null){
                return RedirectToAction("Index", "LoginReg");
            }

            Wedding wedding = _context.Weddings.SingleOrDefault(del=>del.id == weddingId);

            List<Guest> guests = _context.Guests.Where(we => we.weddingsId == weddingId).ToList();
            foreach(Guest gues in guests){
                _context.Guests.Remove(gues);
                _context.SaveChanges();
            }

            _context.Weddings.Remove(wedding);
            _context.SaveChanges();            

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("join_wedding/{weddingId}")]
        public IActionResult JoinWedding(int weddingId)
        {   
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if(loggedId == null){
                return RedirectToAction("Index", "LoginReg");
            }

            User logged = _context.Users.SingleOrDefault(we => we.id == loggedId);
            Wedding wedding = _context.Weddings.SingleOrDefault(del=>del.id == weddingId);                                

            Guest imGuest = new Guest
            {
                weddingsId = wedding.id,
                weddings = wedding,
                usersId = logged.id,
                users = logged,
            };
            _context.Guests.Add(imGuest);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("leave_wedding/{weddingId}")]
        public IActionResult LeaveWedding(int weddingId)
        {   
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if(loggedId == null){
                return RedirectToAction("Index", "LoginReg");
            }
            
            Guest logged = _context.Guests.SingleOrDefault(we => we.usersId == loggedId && we.weddingsId == weddingId);
            _context.Guests.Remove(logged);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }
    }
}
