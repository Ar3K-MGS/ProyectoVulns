using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace vulns.Models.Db
{
    public partial class CounterVulnsContext : DbContext
    {
        public CounterVulnsContext()
        {
        }

        public CounterVulnsContext(DbContextOptions<CounterVulnsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Vulnerabilidade> Vulnerabilidades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=segtidev;Database=CounterVulns;User=sa;password=Seguridad_netcore123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdClientes);

                entity.Property(e => e.IdClientes).HasColumnName("id_clientes");

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_cliente");
            });

            modelBuilder.Entity<Vulnerabilidade>(entity =>
            {
                entity.HasKey(e => e.IdVulnerabilidades);

                entity.Property(e => e.IdVulnerabilidades).HasColumnName("id_vulnerabilidades");

                entity.Property(e => e.AltasVulnerabilidades).HasColumnName("altas_vulnerabilidades");

                entity.Property(e => e.BajasVulnerabilidades).HasColumnName("bajas_vulnerabilidades");

                entity.Property(e => e.CriticasVulnerabilidades).HasColumnName("criticas_vulnerabilidades");

                entity.Property(e => e.FechaVulnerabilidades)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_vulnerabilidades")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdClienteVulnerabilidades).HasColumnName("id_cliente_vulnerabilidades");

                entity.Property(e => e.MediasVulnerabilidades).HasColumnName("medias_vulnerabilidades");

                entity.HasOne(d => d.IdClienteVulnerabilidadesNavigation)
                    .WithMany(p => p.Vulnerabilidades)
                    .HasForeignKey(d => d.IdClienteVulnerabilidades)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vulnerabilidades_Clientes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
