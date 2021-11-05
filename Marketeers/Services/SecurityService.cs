using Marketeers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketeers.Services
{
    public class SecurityService
    {
        UserDAO usersDAO = new UserDAO();

        public SecurityService() {}

        //Customer Login
        public bool IsValid(CustomerModel user)
        {
            return usersDAO.FindUserandPass(user);
        }

        //Customer Register
        public bool Verify(CustomerModel user)
        {
            return usersDAO.CheckUsername(user);
        }

        //Driver
        public bool IsValid(DriverModel user)
        {
            return usersDAO.FindUserandPass(user);
        }

        public bool Verify(DriverModel user)
        {
            return usersDAO.CheckUsername(user);
        }

        //Market
        public bool IsValid(MarketModel user)
        {
            return usersDAO.FindUserandPass(user);
        }

        public bool Verify(MarketModel user)
        {
            return usersDAO.CheckUsername(user);
        }
    }
    
}
