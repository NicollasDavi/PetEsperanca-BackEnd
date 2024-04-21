using Microsoft.AspNetCore.Mvc;
using PetEsperanca.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.MapGet("/usuario", ( [FromServices]AppDbContext context) => {
    return Results.Ok(context.Users.ToList());
});


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
    

app.MapPut("/usuario/atualizar/{id}", ([FromRoute] int id , [FromBody] User newUser, [FromServices]AppDbContext context) => {
    User? user = context.Users.FirstOrDefault(user => user.Id == id);

    if (user is null){
        return Results.NotFound("Usuario não encontrado");
    }

    try
    {
        user.Nome = newUser.Nome ?? user.Nome; 
        user.Cnpj = newUser.Cnpj ?? user.Cnpj;
        user.Tel = newUser.Tel ?? user.Tel;
        user.Cpf = newUser.Cpf ?? user.Cpf;
        user.Email = newUser.Email ?? user.Email;
        context.SaveChanges();
        return Results.Ok(user);
    }
    catch (Exception)
    {
        
        throw;
    }
    
});

app.MapDelete("/usuario/delete/{id}",( [FromRoute] int id, [FromServices] AppDbContext context) => {
    User? user = context.Users.FirstOrDefault(x => x.Id == id);

    if(user is null){
        return Results.NotFound("Usuario não encontrado");
    };

    try
    {
        context.Users.Remove(user);
        context.SaveChanges();
        return Results.Ok("Usuario deletado da base");
    }
    catch (Exception)
    {
        
        throw;
    }
});








app.Run();

