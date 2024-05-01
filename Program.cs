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

app.MapPut("/ong/update/{id}", ([FromRoute] Guid id, [FromBody] Ong newOng, [FromServices] AppDbContext context) =>
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

app.MapPost("/comment" , ([FromBody] Comentario comentario, [FromServices] AppDbContext context) => {
    try
    {
        context.Comment.Add(comentario);
        context.SaveChanges();
        return Results.Created("", comentario);
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
        return StatusCodes(404, "Não encontrado");
    }
});

app.MapGet("/comment/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {
    
});

app.MapPut("/comment/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {

});

app.MapDelete("/comment/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) =>{

});

//Fim das rotas de Comment

//Incio das rotas de Events

// Esta rota permite criar um novo evento (ONG) com base nos dados fornecidos no corpo da requisição.
// Adiciona a ONG ao banco de dados, salva as alterações e retorna o evento criado com uma URL de referência.
app.MapPost("/event", async ([FromBody] Ong ong, [FromServices] AppDbContext context) => {
    try {
        context.Ong.Add(ong);
        context.SaveChanges();
        return Results.Created($"/event/{ong.Id}", ong);
    } catch (Exception ex) {
        throw new Exception($"Erro ao criar evento: {ex.Message}");
    }
});

// Esta rota permite buscar um evento pelo nome.
// Se o evento com o nome fornecido for encontrado, ele é retornado na resposta. Caso contrário, retorna 404 Not Found.
app.MapGet("/event/{nome}", ([FromRoute] string nome, [FromServices] AppDbContext context) => {
    var evento = context.Evento.FirstOrDefault(e => e.Nome == nome);
    if (evento == null) {
        return Results.NotFound();
    }
    return Results.Ok(evento);
});

// Esta rota permite atualizar um evento existente com base no ID fornecido.
// Se o evento for encontrado, suas propriedades são atualizadas com os dados fornecidos no corpo da requisição.
// As alterações são salvas no banco de dados e o evento atualizado é retornado na resposta.
app.MapPatch("/event/{id}", async ([FromRoute] Guid id, [FromBody] Evento updatedEvent, [FromServices] AppDbContext context) => {
    var evento = await context.Evento.FindAsync(id);
    if (evento == null) {
        return Results.NotFound();
    }
    evento.Nome = updatedEvent.Nome;
    evento.DataInicio = updatedEvent.DataInicio;
    evento.Objetivo = updatedEvent.Objetivo;
    evento.ValorDesejado = updatedEvent.ValorDesejado;
    evento.ValorAlcancado = updatedEvent.ValorAlcancado;
    evento.NumeroDeDoacao = updatedEvent.NumeroDeDoacao;
    await context.SaveChangesAsync();
    return Results.Ok(evento);
});

// Esta rota permite substituir um evento existente com base no ID fornecido.
// Se o evento for encontrado, todas as suas propriedades são substituídas com os dados fornecidos no corpo da requisição.
// As alterações são salvas no banco de dados e o evento atualizado é retornado na resposta.
app.MapPut("/event/{id}", async ([FromRoute] int id, [FromBody] Evento updatedEvent, [FromServices] AppDbContext context) => {
    var evento = await context.Evento.FindAsync(id);
    if (evento == null) {
        return Results.NotFound();
    }
    evento.Nome = updatedEvent.Nome;
    evento.DataInicio = updatedEvent.DataInicio;
    evento.Objetivo = updatedEvent.Objetivo;
    evento.ValorDesejado = updatedEvent.ValorDesejado;
    evento.ValorAlcancado = updatedEvent.ValorAlcancado;
    evento.NumeroDeDoacao = updatedEvent.NumeroDeDoacao;
    await context.SaveChangesAsync();
    return Results.Ok(evento);
});

// Esta rota permite excluir um evento existente com base no ID fornecido.
// Se o evento for encontrado, ele é removido do banco de dados e as alterações são salvas.
// Retorna uma resposta 204 No Content para indicar que a operação foi concluída com sucesso.
app.MapDelete("/event/{id}", async ([FromRoute] int id, [FromServices] AppDbContext context) => {
    var evento = await context.Evento.FindAsync(id);
    if (evento == null) {
        return Results.NotFound();
    }
    context.Evento.Remove(evento);
    await context.SaveChangesAsync();
    return Results.NoContent();
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
