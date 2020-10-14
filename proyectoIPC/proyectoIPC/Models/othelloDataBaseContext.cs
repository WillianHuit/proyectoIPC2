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

        public virtual DbSet<CrearSala> CrearSala { get; set; }
        public virtual DbSet<CrearTorneo> CrearTorneo { get; set; }
        public virtual DbSet<Partida> Partida { get; set; }
        public virtual DbSet<SalaEspera> SalaEspera { get; set; }
        public virtual DbSet<Torneo> Torneo { get; set; }
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
            modelBuilder.Entity<CrearSala>(entity =>
            {
                entity.HasKey(e => e.IdSala)
                    .HasName("PK__crear_sa__C4AEB19C6196771D");

                entity.ToTable("crear_sala");

                entity.Property(e => e.IdSala).HasColumnName("idSala");

                entity.Property(e => e.IdCreador).HasColumnName("idCreador");

                entity.Property(e => e.IdPartida).HasColumnName("idPartida");

                entity.HasOne(d => d.IdCreadorNavigation)
                    .WithMany(p => p.CrearSala)
                    .HasForeignKey(d => d.IdCreador)
                    .HasConstraintName("FK__crear_sal__idCre__619B8048");

                entity.HasOne(d => d.IdPartidaNavigation)
                    .WithMany(p => p.CrearSala)
                    .HasForeignKey(d => d.IdPartida)
                    .HasConstraintName("FK__crear_sal__idPar__628FA481");
            });

            modelBuilder.Entity<CrearTorneo>(entity =>
            {
                entity.HasKey(e => e.IdTorneo)
                    .HasName("PK__crear_to__58BF3C239DD1EC1C");

                entity.ToTable("crear_torneo");

                entity.Property(e => e.IdTorneo).HasColumnName("idTorneo");

                entity.Property(e => e.IdCreador).HasColumnName("idCreador");

                entity.Property(e => e.IdPartida).HasColumnName("idPartida");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCreadorNavigation)
                    .WithMany(p => p.CrearTorneo)
                    .HasForeignKey(d => d.IdCreador)
                    .HasConstraintName("FK__crear_tor__idCre__656C112C");

                entity.HasOne(d => d.IdPartidaNavigation)
                    .WithMany(p => p.CrearTorneo)
                    .HasForeignKey(d => d.IdPartida)
                    .HasConstraintName("FK__crear_tor__idPar__66603565");
            });

            modelBuilder.Entity<Partida>(entity =>
            {
                entity.HasKey(e => e.IdPartida)
                    .HasName("PK__partida__552192F64C472F79");

                entity.ToTable("partida");

                entity.Property(e => e.IdPartida).HasColumnName("idPartida");

                entity.Property(e => e.Ganador).HasColumnName("ganador");

                entity.Property(e => e.JugadorA).HasColumnName("jugadorA");

                entity.Property(e => e.JugadorB).HasColumnName("jugadorB");

                entity.HasOne(d => d.GanadorNavigation)
                    .WithMany(p => p.PartidaGanadorNavigation)
                    .HasForeignKey(d => d.Ganador)
                    .HasConstraintName("FK__partida__ganador__5EBF139D");

                entity.HasOne(d => d.JugadorANavigation)
                    .WithMany(p => p.PartidaJugadorANavigation)
                    .HasForeignKey(d => d.JugadorA)
                    .HasConstraintName("FK__partida__jugador__5CD6CB2B");

                entity.HasOne(d => d.JugadorBNavigation)
                    .WithMany(p => p.PartidaJugadorBNavigation)
                    .HasForeignKey(d => d.JugadorB)
                    .HasConstraintName("FK__partida__jugador__5DCAEF64");
            });

            modelBuilder.Entity<SalaEspera>(entity =>
            {
                entity.ToTable("salaEspera");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EnEspera).HasColumnName("enEspera");

                entity.Property(e => e.IdJugador).HasColumnName("idJugador");

                entity.Property(e => e.NombreTorneo)
                    .HasColumnName("nombreTorneo")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Torneo>(entity =>
            {
                entity.HasKey(e => e.IdPartida)
                    .HasName("PK__torneo__552192F6F0C1A42C");

                entity.ToTable("torneo");

                entity.Property(e => e.IdPartida).HasColumnName("idPartida");

                entity.Property(e => e.Ganador).HasColumnName("ganador");

                entity.Property(e => e.IdTorneo).HasColumnName("idTorneo");

                entity.Property(e => e.JugadorA).HasColumnName("jugadorA");

                entity.Property(e => e.JugadorB).HasColumnName("jugadorB");

                entity.Property(e => e.Ronda).HasColumnName("ronda");

                entity.HasOne(d => d.GanadorNavigation)
                    .WithMany(p => p.TorneoGanadorNavigation)
                    .HasForeignKey(d => d.Ganador)
                    .HasConstraintName("FK__torneo__ganador__6B24EA82");

                entity.HasOne(d => d.IdTorneoNavigation)
                    .WithMany(p => p.Torneo)
                    .HasForeignKey(d => d.IdTorneo)
                    .HasConstraintName("FK__torneo__idTorneo__6C190EBB");

                entity.HasOne(d => d.JugadorANavigation)
                    .WithMany(p => p.TorneoJugadorANavigation)
                    .HasForeignKey(d => d.JugadorA)
                    .HasConstraintName("FK__torneo__jugadorA__693CA210");

                entity.HasOne(d => d.JugadorBNavigation)
                    .WithMany(p => p.TorneoJugadorBNavigation)
                    .HasForeignKey(d => d.JugadorB)
                    .HasConstraintName("FK__torneo__jugadorB__6A30C649");
            });

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
