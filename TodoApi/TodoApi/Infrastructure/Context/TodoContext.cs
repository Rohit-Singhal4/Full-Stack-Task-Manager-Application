using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Models;

namespace TodoApi.Infrastructure.Context;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; } = null!; // ! indicates that todo items cannot be null
    public DbSet<Group> Groups { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TodoItem>().HasKey(i => i.Id);
        modelBuilder.Entity<TodoItem>().HasOne(todoItem => todoItem.Group)
            .WithMany(group => group.Items)
            .HasForeignKey(todoItem => todoItem.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Group>().HasKey(i => i.Id);
    }
}

