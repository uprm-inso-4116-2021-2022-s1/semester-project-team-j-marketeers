using Marketeers.Models;
using Marketeers.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketeers.Controllers
{
    public class MarketRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register(MarketModel user)
        {
            SecurityService security = new SecurityService();

            if (security.Verify(user))
            {
                return View("Successful", user);
            }
            else
            {
                return View("Failed", user);
            }
        }
    }
}
