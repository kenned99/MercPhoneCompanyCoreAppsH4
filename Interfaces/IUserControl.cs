using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;

namespace Interfaces
{
    public interface IUserControl 
    {
        bool AddUser(User user, bool IsEmployee);
        bool Login(string email, string password);
        List<Call> GetPhoneRec(User user, bool allTimeHistory, bool IsEmployee);
        User SelectAnUser(string name, string key, bool IsEmployee);
    }
}
