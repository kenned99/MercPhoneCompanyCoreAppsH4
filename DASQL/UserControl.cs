using Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Interfaces;

namespace DASQL
{
    public class DASQL : Interfaces.IUserControl
    {
        public MySqlConnection dbConnect()
        {
            MySqlConnection cnn;

            string connectionString = "server=localhost;database=mpc;uid=root;pwd=password;";
            cnn = new MySqlConnection(connectionString);
            cnn.Open();

            //Test user
            // string sqlString = "INSERT INTO Users (FullName, RoleId, Adress, Adress2, Email, PhoneNo, City, PostCode, Birthday, Password) VALUES ('Kenneth Jensen', 2, 'Merkurvej 25', '1 17', 'Kenneth_jensen_99@hotmail.com', '+45 42686768', 'Viborg', '8800','1999-02-09', 'Password1');";
            // MySqlCommand sql = new MySqlCommand(sqlString, cnn);
            // sql.ExecuteNonQuery();
            return cnn;
        }

        public bool AddUser(User user, bool IsEmployee)
        {
            if (!IsEmployee) return false;

            string newNumber = GenerateNumber();

            MySqlConnection conn = dbConnect();
            string command = "INSERT INTO Users(FullName, Email, Password, RoleId, City, PhoneNo, Adress, Adress2, Birthday, PostCode) " +
                $"VALUES ('{user.FullName}', '{user.Email}', '{user.Password}', '{(int)user.RoleId}', '{user.City}', '{newNumber}', '{user.Adress}', '{user.Adress2}', '{(DateTime)user.Birthday}', '{user.PostCode}')";
            int result = new MySqlCommand(command, conn).ExecuteNonQuery();
            conn.Close();

            if (result != 0) return true;
            return false;
        }

        public string GenerateNumber()
        {
            Random rnd = new Random();

            string number = $"{rnd.Next(10000000, 99999999)}";
            while (!ValidateNumber(number))
            {
                number = $"{rnd.Next(10000000, 9999999)}";
            }
            return number;
        }

        public bool ValidateNumber(string number)
        {
            if (number.Length < 8 || number.Length > 10)
                return false;

            MySqlConnection conn = dbConnect();
            string command = $"SELECT * FROM Users WHERE PhoneNo = '{number}'";

            using (var reader = new MySqlCommand(command, conn).ExecuteReader())
            {
                if (reader.HasRows)
                {
                    conn.Close();
                    return false;
                }
            }
            conn.Close();
            return true;
        }

        public List<Call> GetPhoneRec(User user, bool allTimeHistory, bool IsEmployee)
        {
            List<Call> output = new List<Call>();
           if (user.RoleId == 1) return new List<Call>();
            MySqlConnection conn = dbConnect();
            string command = $"SELECT * FROM custcall WHERE CustId = '{user.Id}'";
            //string command = $"SELECT * FROM custcall";

            if (allTimeHistory == false)
                command += $" AND StartTime >= '{DateTime.Now.AddDays(-7)}'";

            using (var reader = new MySqlCommand(command, conn).ExecuteReader())
            {
                while (reader.Read())
                {
                    output.Add(new Call()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        CustId = int.Parse(reader["CustId"].ToString()),
                        StartTime = DateTime.Parse(reader["StartTime"].ToString()),
                        EndTime = DateTime.Parse(reader["EndTime"].ToString()),
                        PhoneNo = reader["PhoneNo"].ToString()
                    });
                }
            }
            conn.Close();
            return output;
        }

        public bool Login(string email, string password)
        {
            MySqlConnection conn = dbConnect();
            string command = $"SELECT * FROM Users WHERE Email = '{email}' AND Password = '{password}'";
            using (var reader = new MySqlCommand(command, conn).ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.HasRows)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public User SelectAnUser(string name, string key, bool isEmployee)
        {
            MySqlConnection conn = dbConnect();
            string command = $"SELECT * FROM users WHERE FullName = '{name}'";

            using (var reader = new MySqlCommand(command, conn).ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.HasRows) return new User();
                    var user = new User()
                    {
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = "",
                        RoleId = (int)reader["RoleId"],
                        Adress = reader["adress"].ToString(),
                        Adress2 = reader["adress2"].ToString(),
                        City = reader["city"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        PostCode = reader["PostCode"].ToString(),
                        Birthday = DateTime.Now,
                        CallList = new List<Call>()
                    };
                    return user;
                }
                return new User();
            }
        }
    }
}