using FatSecretDotNet.Authentication;
using FatSecretDotNet;
using FatSecretDotNet.RequestObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace NutritionService
{
    internal class Program
    {
        public static async Task Main(String[] args)
        {
            Console.WriteLine("Main Method");
            
            ////var credentials = new FatSecretCredentials()
            ////{
            ////    ClientId = "c8fa10b9e80d4a8883c7b65279318073",
            ////    ClientSecret = "42a4cf7d58b847599b71c9eddb38618e",
            ////    Scope = "basic", // basic or premier
            ////};

            ////var client = new FatSecretClient(credentials);

            ////var foodSearchRequest = new FoodsSearchRequest()
            ////{
            ////    SearchExpression = "Apples",
            ////    MaxResults = 25, //optional
            ////    PageNumber = 1   //optional
            ////};

            ////var foods = await client.FoodsSearchAsync(foodSearchRequest);

            ////var request = new RecipesSearchRequest()
            ////{
            ////    SearchExpression = "Chicken",
            ////    MaxResults = 25, //optional
            ////    PageNumber = 1   //optional
            ////};

            ////var response = await client.RecipesSearchAsync(request);

            CallMongo();
        }

        private static void CallMongo()
        {
            const string connectionUri = "mongodb+srv://homerchap:DdGE0YIi338KQ9vn@nutrition-studio.vpvplkz.mongodb.net/?retryWrites=true&w=majority&appName=nutrition-studio";

            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Create a new client and connect to the server
            var client = new MongoClient(settings);

            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");

                var database = client.GetDatabase("nutrition-studio");

                // Select collection (sort of like a SQL "table")
                var collection = database.GetCollection<Users>("users");

                var users = collection.AsQueryable().OrderBy(x => x.Name);

                var filter = Builders<Users>.Filter
                    .Eq(user => user.Name, "John");

                var user = collection.Find(filter).FirstOrDefault();

                var document = new Users
                {
                    Name = "John",
                    Age = 30,
                    Sex = "Male",
                };

                // Creates instructions to update the "name" field of the first document
                // that matches the filter
                var update = Builders<Users>.Update
                    .Set(restaurant => restaurant, users);

                //// Insert document into collection
                //collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
