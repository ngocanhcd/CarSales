using System;
using DAL_CarSales;

namespace BLL_CarSales
{
    public class UserBLL
    {
        private UserDAL userDAL;

        public UserBLL()
        {
            userDAL = new UserDAL();
        }

        public bool AuthenticateUser(string username, string password)
        {
            return userDAL.Login(username, password);
        }
    }
}
