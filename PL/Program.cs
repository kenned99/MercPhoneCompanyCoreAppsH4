using System;
using Models;
using System.Threading;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            //Mysql
            //UserControl userControl = new UserControl();
            //userControl.dbConnect();

            //MongoDB
            Interfaces.IBusinessLogic bl = new BusinessLogic.BL();
            User user = new User();
            // user.Id = 1;
            user.Password = "password1";
            user.Email = "kenneth_jensen_99@hotmail.com";
            user.FullName = "Kenneth Sørnsen";
            user.PhoneNo = "42 68 67 68";
            user.RoleId = 1;
            user.isEmployee = true;
            user.Birthday = DateTime.Now;
            string key = "";

            Console.WriteLine(bl.Login(user.Email, user.Password));

            bl.AddUser(user, user.isEmployee);

            Thread.Sleep(2000);

            User user1 = bl.SelectAnUser(user.FullName, key, user.isEmployee);

            Console.WriteLine(user1.FullName);

            Console.ReadLine();
        }
    }
}
