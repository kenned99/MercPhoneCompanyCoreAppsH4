using System;
using DASQL;
using Models;
using DAJSON;

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
            DAJSON.MongoDB mongoDB = new DAJSON.MongoDB();
            User user = new User();
            // user.Id = 1;
            user.Password = "ris";
            user.Email = "kenneth_jensen_99@hotmail.com";
            user.FullName = "tis";
            user.PhoneNo = "123";
            user.RoleId = 1;


            mongoDB.MongoDBConnect();

            Console.WriteLine(mongoDB.Login(user.Email, user.Password));

            mongoDB.AddUser(user);
            Console.ReadLine();
        }
    }
}
