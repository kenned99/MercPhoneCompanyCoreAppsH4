using System;
using MongoDB.Driver;
using Models;
using MongoDB.Bson;

namespace DAJSON
{
    public class MongoDB
    {
        MongoClient cnn = new MongoClient("mongodb+srv://Kenned:Password123@cluster0.dg6zy.mongodb.net/Cluster0?authSource=admin&retryWrites=true&w=majority");

        public MongoClient MongoDBConnect()
        {
            //var client = new MongoClient("mongodb+srv://Kenned:password123@cluster0.dg6zy.mongodb.net/mpc?authSource=admin&retryWrites=true&w=majority");
            var database = cnn.GetDatabase("mpc");

            return cnn;
        }

        //Gets one user
        public User SelectAnUser(string name, string key) 
        {
            User user = new User();
            var database = cnn.GetDatabase("mpc");
            var collection = database.GetCollection<BsonDocument>("User");
            var filter = Builders<BsonDocument>.Filter.Eq(key, name);

            var data = collection.Find(filter).FirstOrDefault();
            user = UserObject(data);

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
        public bool AddUser(User user)
        {

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
                    if (mwx != null && mwx.WriteError.Category == ServerErrorCategory.DuplicateKey)
                    {
                        //hvis oprettet
                        state = true; 
                        return true;
                    }
                    //Hvis ikke oprettet
                    state = false;
                    return false;
                });
            }
            return state;
        }
    }
   }
