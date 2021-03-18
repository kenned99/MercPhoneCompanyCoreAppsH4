using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IBusinessLogic
    {
        bool AddUser(User user, bool IsEmployee);
        bool Login(string email, string password);
        List<Call> GetPhoneRec(User user, bool allTimeHistory, bool IsEmployee);
        User SelectAnUser(string name, string key, bool IsEmployee);
    }
}
