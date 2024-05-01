using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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



// Inicio das rotas de Ong

app.MapGet("/list/ong", ([FromServices] AppDbContext context) =>
{
    return Results.Ok(context.Ong.ToList());
});

app.MapPost("/signin", ([FromBody]Ong ong,[FromServices] AppDbContext context) =>
{
    try
    {
        context.Ong.Add(ong);
        context.SaveChanges();
        return Results.Created("", ong);
    }
    catch (Exception)
    {
        throw new Exception("Erro");
    }
});

app.MapPatch("/ong/update/{id}", ([FromRoute] Guid id, [FromBody] Ong newOng, [FromServices] AppDbContext context) =>
{
    Ong? ong = context.Ong.FirstOrDefault(x => x.Id == id);

    if (ong is null)
    {
        return Results.NotFound("Ong não encontrada");
    }

    try
    {
        ong.Name = newOng.Name ?? ong.Name;
        ong.Cnpj = newOng.Cnpj ?? ong.Cnpj;
        ong.Tel = newOng.Tel ?? ong.Tel;
        ong.Cpf = newOng.Cpf ?? ong.Cpf;
        ong.Email = newOng.Email ?? ong.Email;
        ong.OngName = newOng.OngName ?? ong.OngName;
        context.SaveChanges();
        return Results.Ok(ong);
    }
    catch (Exception)
    {
        throw;
    }
});

app.MapDelete("/ong/delete/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    Ong? ong = context.Ong.FirstOrDefault(x => x.Id == id);

    if (ong is null)
    {
        return Results.NotFound("Ong não encontrada");
    };

    try
    {
        context.Ong.Remove(ong);
        context.SaveChanges();
        return Results.Ok("Ong deletada da base");
    }
    catch (Exception)
    {
        throw;
    }
});

//Fim das rotas de Ong

//Inicio das Rodas de  User

app.MapPost("/sigin/user" , ([FromBody] User user, [FromServices] AppDbContext context) => {
    
});

app.MapGet("/user/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {

});

app.MapPatch("/user/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {

});

app.MapPut("/user/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) => {

});

app.MapDelete("/user/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) =>{

});

//Fim das Rotas de User

//Inicio das rotas de Comment

app.MapPost("/comment" , ([FromBody] Comentario comment, [FromServices] AppDbContext context) => {
    try
    {
        context.Comment.Add(comment);
        context.SaveChanges();
        return Results.Created("", comment);
    }
    catch (Exception)
    {
        throw new Exception("Erro");
    }
});

app.MapGet("/comments/{ongId}", ([FromRoute] Guid ongId, [FromServices] AppDbContext context) => {
    Comentario? comment = context.Comment.FirstOrDefault(x => x.OngId == ongId);
    try
    {
        return Results.Ok(context.Comment.ToList());
    }
    catch (Exception)
    {
        throw new Exception("Erro");
    }
});

app.MapGet("/comment/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {
    Comentario? comment = context.Comment.FirstOrDefault(x => x.Id == id);
    try
    {
        return Results.Ok(context.Comment.FirstOrDefault());
    }
    catch (System.Exception)
    {
        
        throw new Exception("Comentario não encontrado");
    }
});

app.MapDelete("/comment/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) =>{
    Comentario? comment = context.Comment.FirstOrDefault(x => x.Id == id);
    try
    {
        context.Comment.Remove(comment);
        context.SaveChanges();
        return Results.Created("", comment);
    }
    catch (System.Exception)
    {
        throw new Exception("Comentario não encontrado");
    }
});

//Fim das rotas de Comment

//Incio das rotas de Events

app.MapPost("/event" , ([FromBody] User user, [FromServices] AppDbContext context) => {
    
});

app.MapGet("/event/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {

});

app.MapPatch("/event/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {

});

app.MapPut("/event/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) => {

});

app.MapDelete("/event/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) =>{

});

//Fim das rotas de Events

//Incio das rotas de Voluntario

app.MapPost("/voluntario" , ([FromBody] User user, [FromServices] AppDbContext context) => {
    
});

app.MapGet("/voluntario/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {

});

app.MapPatch("/voluntario/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {

});

app.MapPut("/voluntario/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) => {

});

app.MapDelete("/voluntario/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) =>{

});

//Fim das rotas de Voluntario


app.Run();
