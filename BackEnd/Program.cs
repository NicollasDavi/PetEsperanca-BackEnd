using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PetEsperanca.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet Esperança API", Version = "v2.4.1" });
});



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Esperança API V2.4.1");
    c.RoutePrefix = string.Empty;
});

// Início das rotas de Ong
app.MapGet("/list/ong", async ([FromServices] AppDbContext context) =>
{
    var ongs = await context.Ong.ToListAsync();
    return Results.Ok(ongs);
});

app.MapPost("/ong", async ([FromBody] Ong ong, [FromServices] AppDbContext context) =>
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

app.MapGet("/ong/{id}", async ([FromRoute] Guid id, [FromServices] AppDbContext context) => {
    var ong = await context.Ong.FindAsync(id);

     if (ong == null)
    {
        return Results.NotFound("ONG não encontrada.");
    }

    try
    {
        return Results.Ok(ong);
    }
    catch (Exception)
    {
        return Results.Problem("Erro ao encontrar ONG.");
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

app.MapPost("/user", async ([FromBody] User user, [FromServices] AppDbContext context) =>
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


// Início das rotas de Voluntario

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

    return Results.Ok("Voluntário deletado.");
});




app.UseCors("AllowAll");
app.Run();
