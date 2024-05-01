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

app.MapPost("/comment" , ([FromBody] User user, [FromServices] AppDbContext context) => {
    
});

app.MapGet("/comment/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {

});

app.MapPut("/comment/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) => {

});

app.MapDelete("/comment/{id}", ([FromRoute] int id, [FromServices] AppDbContext context) =>{

});

//Fim das rotas de Comment

//Incio das rotas de Events

app.MapPost("/event" , async ([FromBody] Ong ong, [FromServices] AppDbContext context) => {
     try
    {
        context.Ong.Add(ong);
        context.SaveChanges();
        return Results.Created($"/event/{ong.Id}", ong);
    }
    catch (Exception ex)
    {
        
        throw new Exception($"Erro ao criar evento: {ex.Message}");
    }
    
});

app.MapGet("/event/{nome}", ([FromRoute] string nome, [FromServices] AppDbContext context) => {

    var evento = context.Eventos.FirstOrDefault(e => e.Nome == nome);

    if (evento == null)
    {
        return Results.NotFound();
    }

        return Results.Ok(evento);
    
});

app.MapPatch("/event/{id}", async ([FromRoute] Guid id, [FromBody] Evento updatedEvent, [FromServices] AppDbContext context) =>
{
    var evento = await context.Eventos.FindAsync(id);
    
    if (evento == null)
    {
        return Results.NotFound();
    }

    evento.Nome = updatedEvent.Nome;
    evento.DataInicio = updatedEvent.DataInicio;
    evento.Objetivo = updatedEvent.Objetivo;
    evento.ValorDesejado = updatedEvent.ValorDesejado;
    evento.ValorAlcancado = updatedEvent.ValorAlcancado;
    evento.NumeroDeDoacoes = updatedEvent.NumeroDeDoacao;

    await context.SaveChangesAsync();

    return Results.Ok(evento);
});


app.MapPut("/event/{id}", async ([FromRoute] int id, [FromBody] Evento updatedEvent, [FromServices] AppDbContext context) =>
{
    var evento = await context.Eventos.FindAsync(id);

    if (evento == null)
    {
        return Results.NotFound();
    }

    evento.Nome = updatedEvent.Nome;
    evento.DataInicio = updatedEvent.DataInicio;
    evento.Objetivo = updatedEvent.Objetivo;
    evento.ValorDesejado = updatedEvent.ValorDesejado;
    evento.ValorAlcancado = updatedEvent.ValorAlcancado;
    evento.NumeroDeDoacoes = updatedEvent.NumeroDeDoacao;

    await context.SaveChangesAsync();

    return Results.Ok(evento);
});

app.MapDelete("/event/{id}", async ([FromRoute] int id, [FromServices] AppDbContext context) =>
{
    var evento = await context.Eventos.FindAsync(id);

    if (evento == null)
    {
        return Results.NotFound();
    }

    context.Eventos.Remove(evento);
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
