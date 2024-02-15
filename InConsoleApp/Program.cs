// See https://aka.ms/new-console-template for more information
using MongoDB.Bson;
using MongoDB.Driver;

Console.WriteLine("Hello, World!");

// Add MongoDb local
var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("test");
var collection = database.GetCollection<Item>("test_collection");

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
var remoteClient = new MongoClient("mongodb+srv://ali:Ali+mongo2024@cluster0.pdbpotw.mongodb.net/");
var databaseRemote = remoteClient.GetDatabase("testRemoteDb");

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

// pause
Console.ReadLine();

