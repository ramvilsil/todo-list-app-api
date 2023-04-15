using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data;

public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<TodoList> TodoLists { get; set; }

    public DbContext() { }

    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}