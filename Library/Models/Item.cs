using MongoDB.Bson;
namespace Library.MongoService;
public class Item
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // quantity
    public int Quantity { get; set; }

}