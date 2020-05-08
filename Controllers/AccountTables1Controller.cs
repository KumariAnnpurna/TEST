using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Test.Models;

namespace Test.Controllers
{
    
        public class AccountTable1Controller : Controller
        {
        private readonly Context _context;

        public AccountTable1Controller(Context context)
        {
            _context = context;
        }

        // GET: Admin1
        public IActionResult AccountTable1()
            {

                var cookie = Request.Cookies["Accountinfo"];
                if (cookie != null)
                {
                    return RedirectToAction("Index", "AccountTables");
                }
                AccountTable account = new AccountTable();
                return View();
            }
            [HttpPost]
            public IActionResult AccountTable1(AccountTable account)
            {
            //var User = _context.AccountTables.Where(m => m.UserName == account.UserName && m.Password == account.Password).FirstOrDefault();
            Admin adm = new Admin();
            if (adm.UserName == "admin" && adm.Password == "1234")
                {
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("Admininfo", JsonConvert.SerializeObject(account), options);
                return RedirectToAction("Index", "AccountTables");
                }
                else
                {
                    ViewBag.Message = "UserName or Password is invalid,Please check once";
                    return View(account);

                }
            }

            public IActionResult Logout()
            {
                if (Request.Cookies["Admininfo"] != null)
                {
                    Response.Cookies.Delete("Admininfo");
                    return RedirectToAction("AccountTable1");
                }
                return RedirectToAction("AccountTable1", "AccountTable1");
            }

        }
    }
