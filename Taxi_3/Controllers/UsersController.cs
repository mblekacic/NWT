using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi_3.Models;

namespace Taxi_3.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;
                if (isUser("Admin"))
                {
                    ViewBag.Role = "admin";
                }
                else if (isUser("Driver"))
                {
                    ViewBag.Role = "driver";
                }
                else
                {
                    ViewBag.Role = "passenger";
                }
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged In";
            }
            return View();
        }
        public Boolean isUser(string role)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new
               UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == role)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}