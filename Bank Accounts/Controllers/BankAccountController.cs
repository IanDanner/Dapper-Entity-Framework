using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bank_Accounts.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Bank_Accounts.Controllers
{
    public class BankAccountController : Controller
    {
        private BankContext _context;
        public BankAccountController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("account")]
        public IActionResult Account()
        {   
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if(loggedId == null){
                return RedirectToAction("Index", "LoginReg");
            }
            
            User logged = _context.Users.Include(activity=>activity.Transactions).Where(we => we.id == loggedId).SingleOrDefault();

            if(logged.Transactions != null) {
                logged.Transactions = logged.Transactions.OrderByDescending(transcation => transcation.date_of_transaction).ToList();
            }

            ViewBag.Account = logged;
            return View("Account");
        }

        [HttpGet]
        [HttpPost]
        [Route("transaction")]
        public IActionResult Transaction(double change)
        {
            int? loggedId = HttpContext.Session.GetInt32("loggedId");
            if(loggedId == null){
                return RedirectToAction("Index", "LoginReg");
            }

            User logged = _context.Users.SingleOrDefault(we => we.id == loggedId);             

            if(change == 0){
                TempData["errors"] = "No amount input";
                return RedirectToAction("Account");
            }

            if(logged.balance + change < 0) {
                TempData["errors"] = "Insufficient Funds";
                return RedirectToAction("Account");
            }

            Transaction newTran = new Transaction
                {
                    amount = change,
                    date_of_transaction = DateTime.Now,
                    userId = logged.id,
                    user = logged
                };
                logged.balance += change;
                _context.Transactions.Add(newTran);
                _context.SaveChanges();

            return RedirectToAction("Account");
        }
    }
}
