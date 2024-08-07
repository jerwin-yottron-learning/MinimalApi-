using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoApp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDBContext>(opt => opt.UseInMemoryDatabase("Todolist"));

var app = builder.Build();


app.MapGet("/todoitems", async (TodoDBContext db) =>
    await db.Todos.ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, TodoDBContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    return todo != null ? Results.Ok(todo) : Results.NotFound();
});

app.MapPost("/todoitems", async (TodoModel todo, TodoDBContext db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todo.TaskId}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, TodoModel inputTodos, TodoDBContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo == null) return Results.NotFound();

    todo.TaskTitle = inputTodos.TaskTitle;
    todo.TaskDescription = inputTodos.TaskDescription;
    todo.IsTaskCompleted = inputTodos.IsTaskCompleted;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDBContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo == null) return Results.NotFound();

    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

