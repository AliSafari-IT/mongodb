// See https://aka.ms/new-console-template for more information
using MongoDB.Bson;

public class Item
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    // quantity
    public int Quantity { get; set; }

}