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

app.MapPost("/signin", ([FromBody] Ong ong, [FromServices] AppDbContext context) =>
{
    try
    {
        context.Ong.Add(ong);
        context.SaveChanges();
        return Results.Created("", ong);
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao salvar a ONG", statusCode: 500);
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
        ong.OngName = newOng.OngName ?? ong.OngName;
        context.SaveChanges();
        return Results.Ok(ong);
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao atualizar a ONG", statusCode: 500);
    }
});

app.MapDelete("/ong/delete/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    Ong? ong = context.Ong.FirstOrDefault(x => x.Id == id);

    if (ong is null)
    {
        return Results.NotFound("Ong não encontrada");
    }

    try
    {
        context.Ong.Remove(ong);
        context.SaveChanges();
        return Results.Ok("Ong deletada da base");
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao deletar a ONG", statusCode: 500);
    }
});

//Fim das rotas de Ong

//Inicio das Rodas de  User

app.MapPost("/create/user" , ([FromBody] User user, [FromServices] AppDbContext context) => {
     try
    {
        context.User.Add(user);
        context.SaveChanges();
        return Results.Created("", user);
    }
    catch (Exception)
    {
        throw new Exception("Erro");
    }
});

app.MapGet("/user/{id}", ([FromRoute] Guid id, [FromServices] AppDbContext context) => {
    var user = context.User.FirstOrDefault(x => x.Id == id);
    if (user == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(user);
});

app.MapPatch("/user/{id}", async([FromRoute] Guid id,[FromBody] User updatedUser, [FromServices] AppDbContext context) => {
    try {
        var user = context.User.Find(id);
        if (user == null) {
            return Results.NotFound();
        }

        user.Name = updatedUser.Name;
        user.Cpf = updatedUser.Cpf;
        user.Tel = updatedUser.Tel;
        user.Email = updatedUser.Email;
     

        await context.SaveChangesAsync();

        return Results.Ok(user);
    } catch (Exception ex) {
        return Results.BadRequest($"Erro ao atualizar usuário: {ex.Message}");
    }
});

app.MapPut("/user/{id}", async ([FromRoute] int id,[FromBody] User updatedUser, [FromServices] AppDbContext context) => {
    try {
        var user = await context.User.FindAsync(id);
        if (user == null) {
            return Results.NotFound();
        }

        user.Name = updatedUser.Name;
        user.Cpf = updatedUser.Cpf;
        user.Tel = updatedUser.Tel;
        user.Email = updatedUser.Email;

        await context.SaveChangesAsync();

        return Results.Ok(user);
    } catch (Exception ex) {
        return Results.BadRequest($"Erro ao atualizar usuário: {ex.Message}");
    }
});

app.MapDelete("/user/{id}", async ([FromRoute] int id, [FromServices] AppDbContext context) =>{
     var user =  context.User.Find(id); 
    if (user == null) {
        return Results.NotFound();
    }
    context.User.Remove(user);
    await context.SaveChangesAsync();
    return Results.NoContent();
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


app.MapPost("/event/cadastrar", async ([FromBody] Evento evento, [FromServices] AppDbContext context) => {
    try {
        Ong? ong = context.Ong.Find(evento.ONGid);
        if (ong == null) {
            return Results.NotFound("ONG não encontrada.");
        }
        context.Evento.Add(evento);

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

// Adiciona um voluntário
// Adiciona um voluntário
app.MapPost("/voluntario/{id}/{ongid}", async ([FromRoute] string id, [FromRoute] string ongid, [FromServices] AppDbContext context) => {
    try {
        var ong = await context.Ong.FindAsync(Guid.Parse(ongid));
        var user = await context.User.FindAsync(Guid.Parse(id));

        if (ong == null || user == null) {
            return Results.NotFound("Ong ou usuário não encontrado");
        }

        if (context.Voluntario.Any(v => v.UserId == Guid.Parse(id) && v.OngId == Guid.Parse(ongid))) {
            return Results.BadRequest("O voluntário já pertence a essa Ong");
        }

        var voluntario = new Voluntario { UserId = Guid.Parse(id), OngId = Guid.Parse(ongid), HorasTrabalhadas = 0 };
        context.Voluntario.Add(voluntario);
        await context.SaveChangesAsync();

        return Results.Created($"/voluntario/{voluntario.VoluntarioId}", voluntario);
    } catch (Exception ex) {
        return Results.BadRequest($"Erro ao adicionar voluntário: {ex.Message}");
    }
});

// Atualiza um voluntário 
app.MapPatch("/voluntario/{id}", async ([FromRoute] Guid id, [FromBody] Voluntario voluntarioAtualizado, [FromServices] AppDbContext context) => {
    var voluntario = await context.Voluntario.FindAsync(id);

    if (voluntario == null) {
        return Results.NotFound("Voluntário não encontrado");
    }

    voluntario.UserId = voluntarioAtualizado.UserId != Guid.Empty ? voluntarioAtualizado.UserId : voluntario.UserId;
    voluntario.OngId = voluntarioAtualizado.OngId != Guid.Empty ? voluntarioAtualizado.OngId : voluntario.OngId;
    voluntario.HorasTrabalhadas = voluntarioAtualizado.HorasTrabalhadas;

    await context.SaveChangesAsync();

    return Results.Ok(voluntario);
});

// Deleta um voluntário
app.MapDelete("/voluntario/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) => {
    var voluntario = await context.Voluntario.FindAsync(id);
    
    if (voluntario == null) {
        return Results.NotFound("Voluntário não encontrado");
    }
    
    context.Voluntario.Remove(voluntario);
    
    await context.SaveChangesAsync();
    
    return Results.Ok("Voluntário deletado");
});


app.Run();
