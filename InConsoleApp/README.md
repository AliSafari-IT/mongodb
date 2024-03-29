# MongoDB C# Driver Quick Start

## **Prerequisites**

1. **MongoDB Running:** Ensure your local MongoDB instance is running on port 27017. If you haven't installed MongoDB yet, download and install it from [https://www.mongodb.com/try/download/community](https://www.mongodb.com/try/download/community).
2. **Project Setup:**
   - **Visual Studio:** Start a new C# Console Application project. I suggest naming it something like "MongoDBTest".
   - **VS Code:** Create a new folder for your project. Open the folder in VS Code, and from the terminal use `dotnet new console` to set up a console application.
3. **MongoDB C# Driver:**
   - **Visual Studio:** In the NuGet Package Manager, install the `MongoDB.Driver` package.
   - **VS Code:** In the terminal, use `dotnet add package MongoDB.Driver`

## **Basic Project Structure**

Create a new C# file (e.g., "Program.cs") and start with the following skeleton code:

```csharp
using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBTest
{
    public class Item
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

    }
}
```

**Inside the `Main` function:**

```csharp
var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("test");
var collection = database.GetCollection<Item>("test_collection");

// Sample Insert
var newItem = new Item { Name = "Test Item", Quantity = 10 };
collection.InsertOne(newItem);
Console.WriteLine("Item inserted with ID: " + newItem.Id);
```

## **Understanding the Code**

1. **Imports:** You include the necessary namespaces for BSON types and the MongoDB driver.
2. **Item Class:** A simple data model to represent items in your collection.
3. **Connection:** Establish a connection to your local MongoDB instance.
4. **Database and Collection:** Retrieve references to your "test" database and "test_collection".
5. **Insert:** Create a new `Item` object and insert it into the collection.

## **Add MongoDB Remote Connection**

1. **Connection:**
   If you haven't installed MongoDB yet, download and install it from [https://www.mongodb.com/try/download/community](https://www.mongodb.com/try/download/community).
   In the terminal from your project root directory, use `dotnet add package MongoDB.Driver` to add the `MongoDB.Driver` package.
2. **Database and Collection:**
   Now that you have a connection to your local MongoDB instance, you can create a reference to your "test" database and "test_collection".

```csharp
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
```

## **Running the Code**

1. **Build and Run:** Execute your project (F5 in Visual Studio or `dotnet run` in VS Code's terminal).
2. **Verify Success:** The console output should indicate the newly inserted item's ID.
3. **Check MongoDB:** You can directly view the database in a tool like MongoDB Compass or in the Atlas interface (if you use Atlas) to confirm the data was inserted.

## **Conclusion**

Happy coding!

## **Questions?**

Please [open an issue](https://github.com/mongodb/mongo-csharp-driver/issues) or [contact us](https://github.com/mongodb/mongo-csharp-driver/issues) if you have any questions.

## **Thanks to**

- [MongoDB Atlas](https://www.mongodb.com/atlas)
- [MongoDB Community](https://www.mongodb.com/community)
- [MongoDB Docs](https://www.mongodb.com/docs/)
- [MongoDB C# Driver](https://www.mongodb.com/docs/driver/)
