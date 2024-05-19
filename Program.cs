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

// Início das rotas de Ong

app.MapGet("/list/ong", async ([FromServices] AppDbContext context) =>
{
    var ongs = await context.Ong.ToListAsync();
    return Results.Ok(ongs);
});

app.MapPost("/signin", async ([FromBody] Ong ong, [FromServices] AppDbContext context) =>
{
    try
    {
        context.Ong.Add(ong);
        await context.SaveChangesAsync();
        return Results.Created("", ong);
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao cadastrar ONG.");
    }
});

app.MapPatch("/ong/update/{id}", async ([FromRoute] Guid id, [FromBody] Ong newOng, [FromServices] AppDbContext context) =>
{
    var ong = await context.Ong.FindAsync(id);

    if (ong == null)
    {
        return Results.NotFound("ONG não encontrada.");
    }

    try
    {
        ong.OngName = newOng.OngName ?? ong.OngName;
        await context.SaveChangesAsync();
        return Results.Ok(ong);
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao atualizar ONG.");
    }
});

app.MapDelete("/ong/delete/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    var ong = await context.Ong.FindAsync(id);

    if (ong == null)
    {
        return Results.NotFound("ONG não encontrada.");
    }

    try
    {
        context.Ong.Remove(ong);
        await context.SaveChangesAsync();
        return Results.Ok("ONG deletada.");
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao deletar ONG.");
    }
});

// Fim das rotas de Ong

// Início das rotas de User

app.MapPost("/signin/user", async ([FromBody] User user, [FromServices] AppDbContext context) =>
{
    try
    {
        context.User.Add(user);
        await context.SaveChangesAsync();
        return Results.Created("", user);
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao cadastrar usuário.");
    }
});

app.MapGet("/user/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    var user = await context.User.FindAsync(id);
    return user != null ? Results.Ok(user) : Results.NotFound("Usuário não encontrado.");
});

app.MapPatch("/user/{id}", async ([FromRoute] Guid id, [FromBody] User updatedUser, [FromServices] AppDbContext context) =>
{
    try
    {
        var user = await context.User.FindAsync(id);
        if (user == null)
        {
            return Results.NotFound("Usuário não encontrado.");
        }

        user.Name = updatedUser.Name ?? user.Name;
        user.Cpf = updatedUser.Cpf ?? user.Cpf;
        user.Tel = updatedUser.Tel ?? user.Tel;
        user.Email = updatedUser.Email ?? user.Email;

        await context.SaveChangesAsync();

        return Results.Ok(user);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro ao atualizar usuário: {ex.Message}");
    }
});

app.MapDelete("/user/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    var user = await context.User.FindAsync(id);
    if (user == null)
    {
        return Results.NotFound("Usuário não encontrado.");
    }

    context.User.Remove(user);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

// Fim das rotas de User

// Início das rotas de Comment

app.MapPost("/comment/{ongId}", async ([FromRoute] Guid ongId, [FromBody] Comentario comment, [FromServices] AppDbContext context) =>
{
    try
    {
        var ong = await context.Ong.FindAsync(ongId);
        if (ong == null)
        {
            return Results.NotFound("ONG não encontrada.");
        }

        comment.OngId = ongId;
        context.Comment.Add(comment);
        await context.SaveChangesAsync();
        return Results.Created("", comment);
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao adicionar o comentário.");
    }
});

app.MapGet("/comments/{ongId}", async ([FromRoute] Guid ongId, [FromServices] AppDbContext context) =>
{
    var comments = await context.Comment.Where(x => x.OngId == ongId).ToListAsync();
    return Results.Ok(comments);
});

app.MapGet("/comment/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    var comment = await context.Comment.FindAsync(id);
    return comment != null ? Results.Ok(comment) : Results.NotFound("Comentário não encontrado.");
});


app.MapDelete("/comment/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    try
    {
        var comment = await context.Comment.FindAsync(id);
        if (comment == null)
        {
            return Results.NotFound("Comentário não encontrado.");
        }

        context.Comment.Remove(comment);
        await context.SaveChangesAsync();
        return Results.Ok("Comentário deletado com sucesso.");
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao deletar o comentário.");
    }
});

// Fim das rotas de Comment

// Início das rotas de Events

app.MapPost("/event/cadastrar", async ([FromBody] Evento evento, [FromServices] AppDbContext context) =>
{
    try
    {
        var ong = await context.Ong.FindAsync(evento.ONGid);
        if (ong == null)
        {
            return Results.NotFound("ONG não encontrada.");
        }

        context.Evento.Add(evento);
        ong.Eventos.Add(evento);
        await context.SaveChangesAsync();

        return Results.Created($"/event/{evento.Id}", evento);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro ao criar evento: {ex.Message}");
    }
});

app.MapGet("/event/buscar/{nome}", async ([FromRoute] string nome, [FromServices] AppDbContext context) =>
{
    var eventos = await context.Evento.Where(e => e.Nome.ToLower() == nome.ToLower()).ToListAsync();
    return eventos.Any() ? Results.Ok(eventos) : Results.NotFound($"Nenhum evento encontrado com o nome '{nome}'.");
});

app.MapPatch("/event/atualizar/{id}", async ([FromRoute] Guid id, [FromBody] Evento updatedEvent, [FromServices] AppDbContext context) =>
{
    try
    {
        var evento = await context.Evento.FindAsync(id);
        if (evento == null)
        {
            return Results.NotFound("Evento não encontrado.");
        }

        evento.Nome = updatedEvent.Nome;
        evento.DataInicio = updatedEvent.DataInicio;
        evento.Objetivo = updatedEvent.Objetivo;
        evento.ValorDesejado = updatedEvent.ValorDesejado;
        evento.ValorAlcancado = updatedEvent.ValorAlcancado;
        evento.NumeroDeDoacao = updatedEvent.NumeroDeDoacao;

        await context.SaveChangesAsync();

        return Results.Ok(evento);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro ao atualizar evento: {ex.Message}");
    }
});

app.MapDelete("/event/deletar/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    var evento = await context.Evento.FindAsync(id);
    if (evento == null)
    {
        return Results.NotFound("Evento não encontrado.");
    }

    context.Evento.Remove(evento);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

// Fim das rotas de Events

// Início das rotas de Voluntario

app.MapPost("/voluntario/{id}/{ongid}", async ([FromRoute] Guid id, [FromRoute] Guid ongid, [FromServices] AppDbContext context) =>
{
    try
    {
        var ong = await context.Ong.FindAsync(ongid);
        if (ong == null)
        {
            return Results.NotFound("ONG não encontrada.");
        }

        var voluntario = await context.User.FindAsync(id);
        if (voluntario == null)
        {
            return Results.NotFound("Voluntário não encontrado.");
        }

        if (ong.Voluntarios.Any(x => x.voluntarioId == voluntario.Id))
        {
            return Results.BadRequest("O voluntário já pertence a essa ONG.");
        }

        
        await context.SaveChangesAsync();
        return Results.Created($"/voluntario/{voluntario.Id}", voluntario);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro ao adicionar voluntário: {ex.Message}");
    }
});

app.MapGet("/voluntario/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    var voluntario = await context.Voluntario.FindAsync(id);
    return voluntario != null ? Results.Ok(voluntario) : Results.NotFound("Voluntário não encontrado.");
});

app.MapPatch("/voluntario/atualizar/{id}", async ([FromRoute] Guid id, [FromBody] Voluntario voluntarioAtualizado, [FromServices] AppDbContext context) =>
{
    var voluntario = await context.Voluntario.FindAsync(id);
    if (voluntario == null)
    {
        return Results.NotFound("Voluntário não encontrado.");
    }

    voluntario.userId = voluntarioAtualizado.userId ?? voluntario.userId;
    voluntario.OngId = voluntarioAtualizado.OngId ?? voluntario.OngId;
    voluntario.HorasTrabalhadas = voluntarioAtualizado.HorasTrabalhadas + voluntario.HorasTrabalhadas;

    await context.SaveChangesAsync();

    return Results.Ok(voluntario);
});

app.MapPut("/voluntario/alterar/completo/{id}", async ([FromRoute] Guid id, [FromBody] Voluntario voluntarioAtualizado, [FromServices] AppDbContext context) =>
{
    var voluntario = await context.Voluntario.FindAsync(id);
    if (voluntario == null)
    {
        return Results.NotFound("Voluntário não encontrado.");
    }

    voluntario.userId = voluntarioAtualizado.userId;
    voluntario.OngId = voluntarioAtualizado.OngId;
    voluntario.HorasTrabalhadas = voluntarioAtualizado.HorasTrabalhadas;

    await context.SaveChangesAsync();

    return Results.Ok(voluntario);
});

app.MapDelete("/voluntario/deletar/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) =>
{
    var voluntario = await context.Voluntario.FindAsync(id);
    if (voluntario == null)
    {
        return Results.NotFound("Voluntário não encontrado.");
    }

    context.Voluntario.Remove(voluntario);
    await context.SaveChangesAsync();

    return Results.Ok("Voluntário deletado.");
});

//endpoint de teste ViaCep

app.MapGet("/test-viacep/{cep}", async ([FromRoute] string cep, [FromServices] ViaCEPService viaCEPService) =>
{
    try
    {
        var address = await viaCEPService.GetAddressByCepAsync(cep);
        return Results.Ok(address);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro ao buscar o endereço: {ex.Message}");
    }
});


app.Run();
