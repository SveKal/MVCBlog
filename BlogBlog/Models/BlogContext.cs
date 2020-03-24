using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlogBlog.Models
{
    public partial class BlogContext : DbContext
    {
        public BlogContext()
        {
        }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlogTable> BlogTable { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost; Database=Blog ;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTable>(entity =>
            {
                entity.HasKey(e => e.BlogId);

                entity.Property(e => e.BlogDateTime).HasColumnType("datetime");

                entity.Property(e => e.BlogEntry).HasMaxLength(2000);

                entity.Property(e => e.BlogHeadline).HasMaxLength(50);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.BlogTable)
                    .HasForeignKey(d => d.CatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlogTable_Categories");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CatId);

                entity.Property(e => e.CatName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
