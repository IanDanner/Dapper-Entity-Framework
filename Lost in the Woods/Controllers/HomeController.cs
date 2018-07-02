using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DapperApp.Factory;
using Lost_in_the_Woods.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lost_in_the_Woods.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrailFactory trailFactory;
        public HomeController()
        {
            //Instantiate a trailFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            trailFactory = new TrailFactory();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            //We can call upon the methods of the trailFactory directly now.
            ViewBag.alltrails = trailFactory.FindAll();
            return View();
        }

        [HttpGet]
        [Route("add_trail")]
        public IActionResult AddTrail()
        {
            return View();
        }

        [HttpGet]
        [HttpPost]
        [Route("create_trail")]
        public IActionResult CreateTrail(Trail newtrail)
        {
            if(ModelState.IsValid)
            {
                trailFactory.Add(newtrail);
                return RedirectToAction("Index");
            }
            return View("AddTrail");
        }

        [HttpGet]
        [Route("trail_info/{Id}")]
        public IActionResult TrailInfo(int Id)
        {
            ViewBag.trail = trailFactory.FindByID(Id);
            return View();
        }
    }
}
