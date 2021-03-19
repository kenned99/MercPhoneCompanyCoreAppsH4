using System;
using Models;
using System.Threading;
using System.Collections.Generic;

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
            user.Id = 3;
            user.Password = "password1";
            user.Email = "kenneth_jensen_99@hotmail.com";
            user.FullName = "Kenneth Jensen";
            user.PostCode = "8800";
            user.City = "Viborg";
            user.Adress = "Merkurvej 25";
            user.Adress2 = "1 17";
            user.RoleId = 2;
            user.isEmployee = true;
            user.Birthday = DateTime.Now;

            string key = "";
            bool allTimeHistory = true;

            Console.WriteLine($"Logged on? {bl.Login(user.Email, user.Password)}\n");

            bl.AddUser(user, user.isEmployee);

            Console.WriteLine($"{user.FullName}'s call log:");
            List<Call> PhoneList = bl.GetPhoneRec(user, user.isEmployee, allTimeHistory);

            foreach (Call call in PhoneList)
            {
                Console.WriteLine($"CallId: {call.Id}");
                Console.WriteLine($"Customer Id: {call.CustId}");
                Console.WriteLine($"Start time: {call.StartTime}");
                Console.WriteLine($"End time: {call.EndTime}");
                Console.WriteLine($"Caller phone no.: {call.PhoneNo}\n");
            }

            //Thread.Sleep(2000);            

            User user1 = bl.SelectAnUser(user.FullName, key, user.isEmployee);

            Console.WriteLine($"New created user: {user1.FullName}");

            Console.ReadLine();
        }
    }
}
