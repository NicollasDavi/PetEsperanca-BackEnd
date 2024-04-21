using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PetEsperanca.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet Esperança API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Esperança API V1");
    c.RoutePrefix = string.Empty;
});



// Inicio das rotas de Usuario

app.MapGet("/usuario", ([FromServices] AppDbContext context) =>
{
    return Results.Ok(context.Users.ToList());
});

app.MapPost("/cadastro", ([FromBody]User user,[FromServices] AppDbContext context) =>
{
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

app.MapPut("/usuario/atualizar/{id}", ([FromRoute] int id, [FromBody] User newUser, [FromServices] AppDbContext context) =>
{
    User? user = context.Users.FirstOrDefault(user => user.Id == id);

    if (user is null)
    {
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

app.MapDelete("/usuario/delete/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) =>
{
    User? user = context.Users.FirstOrDefault(x => x.Id == id);

    if (user is null)
    {
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

//Fim das rotas de Usuario

app.Run();
