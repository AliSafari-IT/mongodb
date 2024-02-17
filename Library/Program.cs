using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    return "Hello World!";
});
app.MapGet("/todo/5", (int page, int pageSize) => page + " _ " + pageSize);

app.Run();
