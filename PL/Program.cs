using System;
using DASQL;
using Models;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {            
            
            UserControl userControl = new UserControl();
            userControl.dbConnect();
        }

    }
}
