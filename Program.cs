using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PetEsperanca.Models;
using PetEsperanca.Services;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet Esperança API", Version = "v1" });
});

builder.Services.AddHttpClient<ViaCEPService>();
builder.Services.AddScoped<ViaCEPService>();


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

app.MapDelete("/comment/delete{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) =>{
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


app.MapPost("/event/cadastrar", async ([FromBody] Ong ong, [FromServices] AppDbContext context) => {
    try {
        context.Ong.Add(ong);
        context.SaveChanges();
        return Results.Created($"/event/{ong.Id}", ong);
    } catch (Exception ex) {
        throw new Exception($"Erro ao criar evento: {ex.Message}");
    }
});


app.MapGet("/event/buscar{nome}", ([FromRoute] string nome, [FromServices] AppDbContext context) => {
    var evento = context.Evento.FirstOrDefault(e => e.Nome == nome);
    if (evento == null) {
        return Results.NotFound();
    }
    return Results.Ok(evento);
});



app.MapPatch("/event/atualizar{id}", async ([FromRoute] Guid id, [FromBody] Evento updatedEvent, [FromServices] AppDbContext context) => {
    var evento =  context.Evento.Find(id);
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


app.MapPut("/event/alterar{id}", async ([FromRoute] int id, [FromBody] Evento updatedEvent, [FromServices] AppDbContext context) => {
    var evento = await context.Evento.FindAsync(id); //tirar
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



app.MapDelete("/event/deletar{id}", async ([FromRoute] int id, [FromServices] AppDbContext context) => {
    var evento =  context.Evento.Find(id); 
    if (evento == null) {
        return Results.NotFound();
    }
    context.Evento.Remove(evento);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

//Fim das rotas de Events

//Incio das rotas de Voluntario

app.MapPost("/voluntario/cadastrar" , ([FromBody] User user, [FromServices] AppDbContext context) => {
     try {
        Voluntario voluntario = new Voluntario
            {
                userId = "12345",
                OngId = "67890",
                voluntarioId = "abcde"
            };
        context.Voluntario.Add(voluntario);
        context.SaveChanges();

        return Results.Created($"/volutario/{voluntario.voluntarioId}", voluntario);
    } catch (Exception ex) {
        throw new Exception($"Erro ao mostra voluntario: {ex.Message}");
    }
});

app.MapGet("/voluntario/buscar{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {
    Voluntario? voluntario = context.Voluntario.Find(id);
    if (voluntario is null)
    {
        return Results.NotFound("Voluntario nao encontrado!");
    }
    return Results.Ok(voluntario);

});

app.MapPatch("/voluntario/atualizar/{id}", ([FromRoute] Guid id, [FromBody] Voluntario voluntarioAtualizado, [FromServices] AppDbContext context) => {
    
    Voluntario? voluntario = context.Voluntario.FirstOrDefault(v => v.voluntarioId == id.ToString());

    
    if (voluntario == null) {
        return Results.NotFound("Voluntário não encontrado!");
    }

    if (voluntarioAtualizado.userId != null) {
        voluntario.userId = voluntarioAtualizado.userId;
    }
    if (voluntarioAtualizado.OngId != null) {
        voluntario.OngId = voluntarioAtualizado.OngId;
    }
    if (voluntarioAtualizado.HorasTrabalhadas != null) {
        voluntario.HorasTrabalhadas = voluntarioAtualizado.HorasTrabalhadas;
    }

    // Salvar as alterações
    context.SaveChanges();

    // Retornar o recurso atualizado
    return Results.Ok(voluntario);
});


app.MapPut("/voluntario/alterar/completo/{id}", ([FromRoute] int id, [FromBody] Voluntario voluntarioAtualizado, [FromServices] AppDbContext context) => {
    
    Voluntario? voluntario = context.Voluntario.FirstOrDefault(v => v.voluntarioId == id.ToString());
    
    if (voluntario == null) {
        return Results.NotFound("Voluntário não encontrado!");
    }
    
    voluntario.userId = voluntarioAtualizado.userId;
    voluntario.OngId = voluntarioAtualizado.OngId;
    voluntario.HorasTrabalhadas = voluntarioAtualizado.HorasTrabalhadas;
    voluntario.voluntarioId = voluntarioAtualizado.voluntarioId;
    
    context.SaveChanges();
    
    return Results.Ok(voluntario);
});


app.MapDelete("/voluntario/deletar{id}", ([FromRoute] int id, [FromServices] AppDbContext context) =>{
    Voluntario? voluntario = context.Voluntario.Find(id);
    if (voluntario is null)
    {
        return Results.NotFound("voluntario não encontrado!");
    }
    context.Voluntario.Remove(voluntario);
    context.SaveChanges();
    return Results.Ok("Produto deletado!");
});

//Fim das rotas de Voluntario


app.Run();
