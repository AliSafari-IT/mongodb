// See https://aka.ms/new-console-template for more information
using Library.Enums;
using Library.Models;
using Library.MongoService;
using Library.Tools;
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
    Email = $"a_{count}@exam.com",
    Fullname = "FullName_" + count,
    Role = UserRoleEnum.ADMINISTRATOR,
    CreationDate = DateTime.Now,
    Password = count + "+P8s33npS",
    Gender = (UserGenderEnum)new Random().Next(0, 2),
    Avatar = "https://picsum.photos/200/300?random=" + count,
    Localization = LocalizationEnum.Dutch,

};

// 3 first & 3 last characters
newUser.Username = newUser.Fullname.Substring(0, 3) + newUser.Fullname.Substring(newUser.Fullname.Length - 3, 3);
newUser.UserInfoIsValid = newUser.PasswordChecker(newUser.Password) && new EmailValidator().IsValidEmail(newUser.Email);

var userColName = Environment.GetEnvironmentVariable("USER_COLLECTION");
var userCollection = instance.Database.GetCollection<User>(userColName);
userCollection.InsertOne(newUser);
if (!newUser.UserInfoIsValid)
{
    // Get user reply if he wants to correct the wrong email
    Console.WriteLine( "Enter email:" );
    var email = Console.ReadLine();
    newUser.Email = email;
    newUser.UpdateDate = DateTime.Now;

    // update newUser in db
    userCollection.ReplaceOne(u => u.Id == newUser.Id, newUser);
}
Console.WriteLine( "User inserted with email: " + newUser.Email );
// pause
newUser.ShowInfo(newUser);
newUser.HideConfindentialValues();
