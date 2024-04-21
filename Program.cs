using Microsoft.AspNetCore.Mvc;
using PetEsperanca.Models;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<AppDbContext>();

app.MapPost("/cadastro", ([FromBody]User user, [FromServices]AppDbContext context) =>{
   
    try
    {
    context.Users.Add(user);
    context.SaveChanges();
    return Results.Created("", user);
    }
    catch (Exception)
    {
        
        throw new Exception("Erro");
    }
});
    


app.MapGet("/", () => "Hello World!");

app.Run();

