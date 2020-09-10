using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace proyectoIPC.Models
{
    public partial class othelloDataBaseContext : DbContext
    {
        public othelloDataBaseContext()
        {
        }

        public othelloDataBaseContext(DbContextOptions<othelloDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SJIQR50; Initial Catalog=othelloDataBase; User ID=seito;Password=sql0720;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasColumnName("apellido")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasColumnName("correo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Enlinea)
                    .HasColumnName("enlinea")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasColumnName("pais")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Usr)
                    .HasColumnName("usr")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
