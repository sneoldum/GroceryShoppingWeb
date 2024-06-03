using Entitiy.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ShoppingWebUI.Services;

public class BalanceService
{
    private IMongoCollection<BalanceModel> balanceCollection;

    public bool testConnection()
    {

        const string connectionUri = "mongodb+srv://balance:balance@nobet.xfefuau.mongodb.net/?retryWrites=true&w=majority";

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

    public BalanceService()
    {
        var client = new MongoClient("mongodb+srv://user:user@nobet.xfefuau.mongodb.net/?retryWrites=true&w=majority");


        var database = client.GetDatabase("nobet");
        balanceCollection = database.GetCollection<BalanceModel>("Balance");
    }

    //returns all of the Users from db
    public List<BalanceModel> Get()
    {
        return balanceCollection.Find(model => true).ToList();
    }

    //returns user by unique username
    public BalanceModel GetOneByUsername(String username)
    {
        return balanceCollection.Find(model => model.Username == username).FirstOrDefault();
    }

    //creates a user from scratch, Id field must be null in order to create new one otherwise it will try to update an existing one
    public string Create(BalanceModel balanceModel)
    {
        balanceCollection.InsertOne(balanceModel);
        return balanceModel.Id.ToString();
    }

    //updates a user, usermongomodel has to contain a valid user id
    public string Update(BalanceModel balanceModel)
    {
        balanceCollection.ReplaceOne(model => model.Id == balanceModel.Id, balanceModel);
        return balanceModel.Id.ToString();
    }

    //deletes user by its id
    public void Delete(ObjectId id)
    {
        balanceCollection.DeleteOne(model => model.Id == id);
    }
}
