using System;
using MongoDB.Driver;
using Models;
using MongoDB.Bson;
using Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAJSON
{
    public class MongoDB : IUserControl
    {
        MongoClient cnn = new MongoClient("mongodb+srv://Kenned:Password123@cluster0.dg6zy.mongodb.net/Cluster0?authSource=admin&retryWrites=true&w=majority");

        public MongoClient MongoDBConnect()
        {
            //var client = new MongoClient("mongodb+srv://Kenned:password123@cluster0.dg6zy.mongodb.net/mpc?authSource=admin&retryWrites=true&w=majority");
            var database = cnn.GetDatabase("mpc");

            return cnn;
        }

        //Gets one user
        public User SelectAnUser(string name, string key, bool isEmployee)
        {
            User user = new User();
            if (isEmployee)
            {
                var database = cnn.GetDatabase("mpc");
                var collection = database.GetCollection<BsonDocument>("User");
                var filter = Builders<BsonDocument>.Filter.Eq(key, name);

                var data = collection.Find(filter).FirstOrDefault();
                user = UserObject(data);
            }
            return user;
        }

        //Create objects
        public User UserObject(BsonDocument Tname)
        {
            User user = new User();
            user.FullName = (string)Tname["name"];
            user.Adress = (string)Tname["Adress"];
            user.Password = (string)Tname["Password"];
            user.PhoneNo = (string)Tname["PhoneNo"];
            user.RoleId = (int)Tname["RoleId"];

            return user;
        }

        //login
        public User Login(string email, string password)
        {
            User user = new User();
            var database = cnn.GetDatabase("mpc");
            var collection = database.GetCollection<BsonDocument>("Users");
            var filterEmail = Builders<BsonDocument>.Filter.Eq("email", email);
            var filterPassword = Builders<BsonDocument>.Filter.Eq("password", password);
            var data = collection.Find(filterEmail & filterPassword).FirstOrDefault();

            return user;
        }

        //Create user
        public bool AddUser(User user, bool isEmployee)
        {
            int number = 0;
            void CreateNewNumber()
            {
                var generator = new RandomGenerator();
                var New8Number = generator.RandomNumber(10000000, 99999999);
            }

            CreateNewNumber();
            while (number == 0) 
            {
                CreateNewNumber();
            }

            var database = cnn.GetDatabase("mpc");
            var collection = database.GetCollection<BsonDocument>("Users");
            bool state = true;
            var document = new BsonDocument
            {
                { "FullName", user.FullName},
                { "RoleId", user.RoleId.ToString()},
                { "PhoneNo", user.PhoneNo},
                { "Password", user.Password},
                { "Email", user.Email},
            };
            try
            {
                collection.InsertOne(document);
            }
            catch (AggregateException aggEx)
            {
                aggEx.Handle(x =>
                {
                    var mwx = x as MongoWriteException;
                    if (mwx != null && mwx.WriteError.Category == ServerErrorCategory.DuplicateKey) //  tjekker om rec findes
                    {
                        //  added
                        state = false;
                        return false;
                    }
                    //  not created
                    state = false;
                    return false;
                });
            }
            return state;
        }

        bool IUserControl.Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public List<Call> GetPhoneRec(User user, bool allTimeHistory, bool IsEmployee)
        {
            throw new NotImplementedException();
        }
        public class RandomGenerator
        {
            private readonly Random _random = new Random();

            public int RandomNumber(int min, int max)
            {
                return _random.Next(min, max);
            }
        }
    }
}
