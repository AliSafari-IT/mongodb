// See https://aka.ms/new-console-template for more information
using Library.Enums;
using Library.Models;
using Library.MongoService;
using MongoDB.Bson;
using MongoDB.Driver;

Console.WriteLine("Hello, World!");

// Add MongoDb local
//var client = new MongoClient("mongodb://localhost:27017");
//var database = client.GetDatabase("test");
//var collection = database.GetCollection<Item>("test_collection");
var instance = Connection.Instance;
var colName = Environment.GetEnvironmentVariable("COLLECTION");
var collection = instance.Database.GetCollection<Item>(colName);

// Sample Insert
var guid = Guid.NewGuid().ToString();
var randItemName = "Test Item_"+ guid;
var newItem = new Item { Name = randItemName, Quantity = 10 };
collection.InsertOne(newItem);
Console.WriteLine("Item inserted with ID: " + newItem.Id);
// Count all documents
long count = await collection.CountDocumentsAsync(new BsonDocument()); 
Console.WriteLine("Document Count: " + count);

// Add to mongodb remote
var url = Environment.GetEnvironmentVariable("REMOTE_CONNSTR");
MongoClient remoteClient = new(url);

var databaseRemote = remoteClient.GetDatabase("test");

var remoteCollection = databaseRemote.GetCollection<Item>("remoteCollection");
remoteCollection.InsertOne(newItem);
Console.WriteLine("Add to Remote DB.Collection");
// Count all documents
long countRemote = await remoteCollection.CountDocumentsAsync(new BsonDocument());

if (countRemote == 0)
{
    Console.WriteLine("No Data Found in the Collection {0}", remoteCollection.CollectionNamespace);
}
else
{
    Console.WriteLine($"Total documents found in {remoteCollection.CollectionNamespace} is {countRemote} items.");
}


var newUser = new User {
    Email = $"a_{guid}@..com",
    Fullname = "FullName_" + count,
    // Add a random number from 1 to 10000 for the Id
    //Id =  new Random().Next(1, 100000),
    Password = "1cX/65476513",
    Role = UserRoleEnum.ADMINISTRATOR,

    
};

var userColName = Environment.GetEnvironmentVariable("USER_COLLECTION");
var userCollection = instance.Database.GetCollection<User>(userColName);
userCollection.InsertOne(newUser);
if (!newUser.UserInfoIsValid)
{
    // Get user reply if he wants to correct the wrong email
    Console.WriteLine( "Enter email:" );
    var email = Console.ReadLine();
    newUser.Email = email;
    // update newUser in db
    userCollection.ReplaceOne(u => u.Id == newUser.Id, newUser);
}
Console.WriteLine( "User inserted with email: " + newUser.Email );
// pause
Console.ReadLine();