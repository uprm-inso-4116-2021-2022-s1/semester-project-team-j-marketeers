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

        public SecurityService()
        {

        }

        public bool IsValid(UserModel user)
        {
            return usersDAO.FindUserandPass(user);
        }
    }

    
}
