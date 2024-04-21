using Microsoft.AspNetCore.Mvc;
using PetEsperanca.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

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
    


app.MapGet("/usuario", ( [FromServices]AppDbContext context) => {
    return Results.Ok(context.Users.ToList());
});

app.Run();

