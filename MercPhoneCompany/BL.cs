using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using DASQL;

namespace BusinessLogic
{
    public class BL : IBusinessLogic
    {
       // IUserControl da = new DAJSON.MongoDB(); //MongoDB
        IUserControl da = new DASQL.DASQL(); //MySQL

        public bool AddUser(User user, bool IsEmployee)
        {
            return da.AddUser(user, IsEmployee);
        }
        public List<Call> GetPhoneRec(User user, bool allTimeHistory, bool IsEmployee)
        {
            return da.GetPhoneRec(user, allTimeHistory, IsEmployee);
        }
        public bool Login(string email, string password)
        {
            return da.Login(email, password);
        }
        public User SelectAnUser(string name, string key, bool IsEmployee)
        {
            return da.SelectAnUser(name, key, IsEmployee);
        }
    }
}
