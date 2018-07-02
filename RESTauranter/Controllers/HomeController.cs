using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTauranter.Models;
using Microsoft.EntityFrameworkCore;

namespace RESTauranter.Controllers
{
    public class HomeController : Controller
    {
        private RestContext _context;
 
        public HomeController(RestContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HttpPost]
        [Route("add_review")]
        public IActionResult AddReview(Review newreview)
        {
            Review test = newreview;
            if(ModelState.IsValid)
            {
                if(newreview.date_of_visit > DateTime.Now){
                    ViewBag.error = "Cannot review a future visit";
                    return RedirectToAction("Index");
                }
                _context.Add(newreview);
                _context.SaveChanges();
                return RedirectToAction("Reviews");
            }
            return View("Index", newreview);
        }

        [HttpGet]
        [HttpPost]
        [Route("reviews")]
        public IActionResult Reviews()
        {
            List<Review> Allreviews = _context.Reviews.OrderByDescending(review => review.created_at).ToList();
            ViewBag.reviews = Allreviews;
            return View();
        }
    }
}
