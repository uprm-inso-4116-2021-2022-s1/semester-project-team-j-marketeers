﻿using Marketeers.Models;
using Marketeers.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketeers.Controllers
{
    public class DriverLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(DriverModel user)
        {
            SecurityService security = new SecurityService();

            if (security.IsValid(user))
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
