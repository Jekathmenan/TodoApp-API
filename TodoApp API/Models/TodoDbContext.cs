using Microsoft.EntityFrameworkCore;

namespace TodoApp_API.Models
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>()
                .ToTable("todolist")
                .HasKey(t => t.Id);

            modelBuilder.Entity<TodoItem>()
                .ToTable("todoitem")
                .HasKey(t => t.Id);

            // Configure the one-to-many relationship
            /*modelBuilder.Entity<TodoItem>()
                .HasOne(l => l.Items)
                .HasForeignKey(ti => ti.TodoListId)
                .OnDelete(DeleteBehavior.Cascade)*/

            modelBuilder.Entity<TodoList>()
            .HasMany(l => l.Items)
            .WithOne() // no navigation property back
            .HasForeignKey(i => i.TodoListId);


            /*modelBuilder.Entity<TodoList>()
                .HasMany<TodoItem>(l => l.Items);*/
        }

        public DbSet<TodoList> TodoLists { get; set; } = null!;
        public DbSet<TodoItem> TodoItems { get; set; } = null!;

    }
}
