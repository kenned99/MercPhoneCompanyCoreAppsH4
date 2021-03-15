using Models;
using System;
using System.Collections.Generic;

namespace DASQL
{
    public class UserControl : Interfaces.IUserControl
    {
        public bool CreateUser(User user, bool IsEmployee)
        {
            throw new NotImplementedException();
        }
        public string findNumber(string userName, bool IsEmployee)
        {
            throw new NotImplementedException();
        }
        public List<Call> GetPhoneRec(User user, bool allTimeHistory, bool IsEmployee)
        {
            throw new NotImplementedException();
        }
        public bool Login(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
