//using Entitiy.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingWebUI.Models;

namespace ShoppingWebUI.Services;

public class UserService
{
    private IMongoCollection<UserMongoModel> userCollection;

    public bool testConnection()
    {

        const string connectionUri = "mongodb://user:user@localhost:8081";

        var settings = MongoClientSettings.FromConnectionString(connectionUri);

        // Set the ServerApi field of the settings object to Stable API version 1
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        // Create a new client and connect to the server
        var client = new MongoClient(settings);

        // Send a ping to confirm a successful connection
        try
        {
            var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }

    }

    public UserService()
    {
        var client = new MongoClient("mongodb://user:user@localhost:27017");


        var database = client.GetDatabase("NoBet");
        userCollection = database.GetCollection<UserMongoModel>("User");
    }

    //returns all of the Users from db
    public List<UserMongoModel> Get() 
    {
        return userCollection.Find(model => true).ToList();
    }

    //returns user by unique username
    public UserMongoModel GetOneByUsername(String username)
    {
        return userCollection.Find(model => model.Username == username).FirstOrDefault();
    }

    //creates a user from scratch, Id field must be null in order to create new one otherwise it will try to update an existing one
    public string Create(UserMongoModel userMongoModel)
    {
        userCollection.InsertOne(userMongoModel);
        return userMongoModel.Id.ToString();
    }

    //updates a user, usermongomodel has to contain a valid user id
    public string Update(UserMongoModel userMongoModel)
    {
        userCollection.ReplaceOne(model => model.Id == userMongoModel.Id, userMongoModel);
        return userMongoModel.Id.ToString();
    }

    //deletes user by its id
    public void Delete(ObjectId id)
    {
        userCollection.DeleteOne(model => model.Id == id);
    }
}