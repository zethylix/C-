using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore;

public class TodoContext(DbContextOptions options):DbContext(options)
{
    
        public DbSet<Todo> Todos { get; set; }
        protected override void OnModelCreating( ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Todo>(builder =>
                {
                    builder.ToTable("Items");
                    builder.HasKey(x => x.Id);
                    builder.Property(x => x.Id)
                        .HasColumnName("Id")
                        .UseIdentityColumn()
                        .ValueGeneratedOnAdd();

                    builder.Property(x => x.Title)
                        .HasColumnName("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    builder.Property(x => x.Done)
                        .HasColumnName("Done").IsRequired();


                }
            );
        }
        
    
}