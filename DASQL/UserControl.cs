using Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Interfaces;

namespace DASQL
{
    public class UserControl : Interfaces.IUserControl
    {
        public MySqlConnection dbConnect()
        {
            MySqlConnection cnn;

            string connectionString = "server=localhost;database=mpc;uid=root;pwd=password;";
            cnn = new MySqlConnection(connectionString);
            cnn.Open();
            Console.WriteLine("Database connection open");

            string sqlString = "INSERT INTO Users (FullName, RoleId, Adress, Adress2, Email, PhoneNo, City, PostCode, Birthday, Password) VALUES ('Kenneth Jensen', 2, 'Merkurvej 25', '1 17', 'Kenneth_jensen_99@hotmail.com', '+45 42686768', 'Viborg', '8800','1999-02-09', 'Password1');";
            MySqlCommand sql = new MySqlCommand(sqlString, cnn);
            sql.ExecuteNonQuery();
            return cnn;
        }
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
            List<Call> output = new List<Call>();
            if (user.RoleId == 1) return new List<Call>();
            MySqlConnection conn = dbConnect();
            string command = $"SELECT * FROM custcall WHERE ID = '{user.Id}'";

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
            throw new NotImplementedException();
        }

        SqlConnection IUserControl.dbConnect()
        {
            throw new NotImplementedException();
        }
    }
}
