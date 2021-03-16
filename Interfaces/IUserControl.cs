using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;

namespace Interfaces
{
    public interface IUserControl 
    {
        bool CreateUser(User user, bool IsEmployee);
        bool Login(string email, string password);
        string findNumber(string userName, bool IsEmployee);
        List<Call> GetPhoneRec(User user, bool allTimeHistory, bool IsEmployee);
        SqlConnection dbConnect();
    }
}
