using Marketeers.Models;
using Marketeers.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketeers.Controllers
{
    public class RegisterController : Controller
    {
        //CustomerIndex
        [Route("/[controller]/CustomerIndex")]
        [HttpGet]
        public IActionResult CustomerIndex()
        {
            return View("CustomerIndex");
        }

        //DriverIndex
        [Route("/[controller]/DriverIndex")]
        [HttpGet]
        public IActionResult DriverIndex()
        {
            return View("DriverIndex");
        }

        //MarketIndex
        [Route("/[controller]/MarketIndex")]
        [HttpGet]
        public IActionResult MarkerIndex()
        {
            return View("MarketIndex");
        }

        //Customer
        [Route("/[controller]/CustomerRegister")]
        [HttpGet]
        public IActionResult CustomerRegister(CustomerModel user)
        {
            SecurityService security = new SecurityService();
            if (security.Verify(user))
            {
                return View("SuccessfulForCustomer", user);
            }
            else
            {
                return View("Failed", user);
            }
        }

        //Driver
        [Route("/[controller]/DriverRegister")]
        [HttpGet]
        public IActionResult DriverRegister(DriverModel user)
        {
            SecurityService security = new SecurityService();
            if (security.Verify(user))
            {
                return View("SuccessfulForDriver", user);
            }
            else
            {
                return View("Failed", user);
            }
        }

        //Market
        [Route("/[controller]/MarketRegister")]
        [HttpGet]
        public IActionResult MarketRegister(MarketModel user)
        {
            SecurityService security = new SecurityService();
            if (security.Verify(user))
            {
                return View("SuccessfulForMarket", user);
            }
            else
            {
                return View("Failed", user);
            }
        }


    }
}
