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
        // ong.Name = newOng.Name ?? ong.Name;
        // ong.Cnpj = newOng.Cnpj ?? ong.Cnpj;
        // ong.Tel = newOng.Tel ?? ong.Tel;
        // ong.Cpf = newOng.Cpf ?? ong.Cpf;
        // ong.Email = newOng.Email ?? ong.Email;
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

app.MapPost("/comment/{ongId}", ([FromRoute] Guid ongId, [FromBody] Comentario comment, [FromServices] AppDbContext context) =>
{
    try
    {
        Ong? ong = context.Ong.FirstOrDefault(x => x.Id == ongId);
        if (ong == null)
        {
            return Results.NotFound("ONG não encontrada.");
        }

        comment.OngId = ongId;
        context.Comment.Add(comment);
        context.SaveChanges();
        return Results.Created("", comment);
    }
    catch (Exception)
    {
        throw new Exception("Erro ao adicionar o comentário.");
    }
});


app.MapGet("/comments/{ongId}", ([FromRoute] Guid ongId, [FromServices] AppDbContext context) => {
    var comments = context.Comment.Where(x => x.OngId == ongId).ToList();
    return Results.Ok(comments);
});

app.MapGet("/comment/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {
    var comment = context.Comment.FirstOrDefault(x => x.Id == id);
    if (comment == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(comment);
});




//Fim das rotas de Comment

//Incio das rotas de Events


app.MapPost("/event/cadastrar", async ([FromBody] Event evento, [FromServices] AppDbContext context) => {
    try {
        Ong ong = await context.Ong.FindAsync(evento.ONGid);
        if (ong == null) {
            return Results.NotFound("ONG não encontrada.");
        }
        context.Event.Add(evento);

        ong.Eventos.Add(evento);

        await context.SaveChangesAsync();

        return Results.Created($"/event/{evento.Id}", evento);
    } catch (Exception ex) {
        throw new Exception($"Erro ao criar evento: {ex.Message}");
    }
});



app.MapGet("/event/buscar/{nome}", ([FromRoute] string nome, [FromServices] AppDbContext context) => {
    var eventos = context.Evento.Where(e => e.Nome.ToLower() == nome.ToLower()).ToList();

    if (eventos.Count == 0) {
        return Results.NotFound($"Nenhum evento encontrado com o nome '{nome}'.");
    }

    return Results.Ok(eventos);
});



app.MapPatch("/event/atualizar/{id}", async ([FromRoute] Guid id, [FromBody] Evento updatedEvent, [FromServices] AppDbContext context) => {
    try {
        var evento = context.Evento.Find(id);
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
    } catch (Exception ex) {
        return Results.BadRequest($"Erro ao atualizar evento: {ex.Message}");
    }
});



app.MapPut("/event/alterar/{id}", async ([FromRoute] int id, [FromBody] Evento updatedEvent, [FromServices] AppDbContext context) => {
    try {
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
    } catch (Exception ex) {
        return Results.BadRequest($"Erro ao atualizar evento: {ex.Message}");
    }
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

app.MapPost("/voluntario/{id}/{ongid}", ([FromRoute] string id, [FromRoute] string ongid, [FromServices] AppDbContext context) => {
    try {
        Ong ong = context.Ong.Find(ongId);
        if (ong == null) {
            return Results.NotFound("Ong não encontrada");
        }
        Voluntario voluntario = context.User.Find(id);
        if (voluntario == null) {
            return Results.NotFound("Voluntário não encontrado");
        }
        if (ong.Voluntarios.Any(x => x.Id == voluntario.Id)) {
            return Results.BadRequest("O voluntário já pertence a essa Ong");
        }
        ong.Voluntarios.Add(voluntario);
        context.SaveChanges();
        return Results.Created($"/voluntario/{voluntario.Id}", voluntario);
    } catch (Exception ex) {
        return Results.BadRequest($"Erro ao adicionar voluntário: {ex.Message}");
    }
});

app.MapGet("/voluntario/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) => {
    var voluntario = await context.Voluntario.FindAsync(id);
    return voluntario != null ? Results.Ok(voluntario) : Results.NotFound("Voluntário não encontrado");
});

app.MapPatch("/voluntario/atualizar/{id}", async ([FromRoute] Guid id, [FromBody] Voluntario voluntarioAtualizado, [FromServices] AppDbContext context) => {
    var voluntario = await context.Voluntario.FindAsync(id);
    
    if (voluntario == null) {
        return Results.NotFound("Voluntário não encontrado!");
    }

    voluntario.userId = voluntarioAtualizado.userId ?? voluntario.userId;
    voluntario.OngId = voluntarioAtualizado.OngId ?? voluntario.OngId;
    voluntario.HorasTrabalhadas = voluntarioAtualizado.HorasTrabalhadas ?? voluntario.HorasTrabalhadas;

    await context.SaveChangesAsync();

    return Results.Ok(voluntario);
});


app.MapPut("/voluntario/alterar/completo/{id}", async ([FromRoute] int id, [FromBody] Voluntario voluntarioAtualizado, [FromServices] AppDbContext context) => {
    var voluntario = await context.Voluntario.FindAsync(id);
    
    if (voluntario == null) {
        return Results.NotFound("Voluntário não encontrado!");
    }
    
    voluntario.userId = voluntarioAtualizado.userId;
    voluntario.OngId = voluntarioAtualizado.OngId;
    voluntario.HorasTrabalhadas = voluntarioAtualizado.HorasTrabalhadas;
    voluntario.voluntarioId = voluntarioAtualizado.voluntarioId;
    
    await context.SaveChangesAsync();
    
    return Results.Ok(voluntario);
});



app.MapDelete("/voluntario/deletar/{id}", async ([FromRoute] int id, [FromServices] AppDbContext context) => {
    var voluntario = await context.Voluntario.FindAsync(id);
    
    if (voluntario == null) {
        return Results.NotFound("Voluntário não encontrado!");
    }
    
    context.Voluntario.Remove(voluntario);
    
    await context.SaveChangesAsync();
    
    return Results.Ok("Voluntário deletado!");
});




app.Run();
