﻿using ImpressioApi_.Domain.Interfaces.Repositories;
using ImpressioApi_.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ImpressioApi_.Infrastructure.Data.Contexts
{
    public class ImpressioDbContext : DbContext, IUnitOfWork
    {
        public virtual DbSet<UsuarioModel> Usuarios { get; set; }
        public virtual DbSet<ObraArteModel> ObrasArte { get; set; }
        public virtual DbSet<ObraFavoritadaModel> ObrasFavoritadas { get; set; }

        public ImpressioDbContext(DbContextOptions<ImpressioDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("t_usuario");
                entity.HasKey(u => u.IdUsuario).HasName("pk_usuario");
                entity.Property(u => u.IdUsuario).ValueGeneratedOnAdd().HasColumnName("id_usuario");
                entity.Property(u => u.EmailUsuario).HasColumnName("email_usuario").HasMaxLength(100).IsRequired(true);
                entity.Property(u => u.Senha).HasColumnName("senha").IsRequired(true);
                entity.Property(u => u.DataNascimento).HasColumnName("data_nascimento").IsRequired(true);
                entity.Property(u => u.Apelido).HasColumnName("apelido").HasMaxLength(30).IsRequired(false);
                entity.Property(u => u.NomeUsuario).HasColumnName("nome_usuario").HasMaxLength(30).IsRequired(false);
                entity.Property(u => u.BiografiaUsuario).HasColumnName("biografia_usuario").HasMaxLength(120).IsRequired(false);
                entity.Property(u => u.ImagemUsuario).HasColumnName("imagem_usuario").IsRequired(false);
                entity.Property(u => u.Publico).HasColumnName("publico").HasDefaultValue(true).IsRequired(true);
            });

            modelBuilder.Entity<ObraArteModel>(entity =>
            {
                entity.ToTable("t_obra_arte");
                entity.HasKey(o => o.IdObraArte).HasName("pk_obra_arte");
                entity.Property(o => o.IdObraArte).ValueGeneratedOnAdd().HasColumnName("id_obra_arte");
                entity.Property(o => o.ImagemObraArte).HasColumnName("imagem_obra_arte").IsRequired(true);
                entity.Property(o => o.DescricaoObraArte).HasColumnName("descricao_obra_arte").HasMaxLength(170).IsRequired(false);
                entity.Property(o => o.Publico).HasColumnName("publico").HasDefaultValue(true).IsRequired(true);
                entity.Property(o => o.IdUsuario).HasColumnName("id_usuario").IsRequired(true);

                entity.HasOne(o => o.Usuario).WithMany(u => u.ObrasArte).HasForeignKey(o => o.IdUsuario).HasConstraintName("fk_usuario_obras_arte").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ObraFavoritadaModel>(entity =>
            {
                entity.ToTable("t_obra_favoritada");
                entity.HasKey(f => f.IdObraFavoritada).HasName("pk_obra_favoritada");
                entity.Property(f => f.IdObraFavoritada).ValueGeneratedOnAdd().HasColumnName("id_obra_favoritada");
                entity.Property(f => f.IdObraArte).HasColumnName("id_obra_arte").IsRequired(true);
                entity.Property(f => f.IdUsuario).HasColumnName("id_usuario").IsRequired(true);

                entity.HasOne(f => f.ObraArte).WithMany(o => o.UsuariosFavoritaram).HasForeignKey(f => f.IdObraArte).HasConstraintName("fk_obra_favoritada_obra_arte").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.Usuario).WithMany(u => u.ObrasFavoritadas).HasForeignKey(f => f.IdUsuario).HasConstraintName("fk_obra_favoritada_usuario").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ObraVotoModel>(entity =>
            {
                entity.ToTable("t_obra_voto");
                entity.HasKey(v => v.IdObraVoto).HasName("pk_obra_voto");
                entity.Property(v => v.IdObraVoto).ValueGeneratedOnAdd().HasColumnName("id_obra_voto");
                entity.Property(v => v.IdObraArte).HasColumnName("id_obra_arte").IsRequired(true);
                entity.Property(v => v.IdUsuario).HasColumnName("id_usuario").IsRequired(true);
                entity.Property(v => v.Voto).HasColumnName("voto").IsRequired(true);

                entity.HasOne(v => v.ObraArte).WithMany(o => o.Votos).HasForeignKey(v => v.IdObraArte).HasConstraintName("fk_obra_voto_obra_arte").OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(v => v.Usuario).WithMany(u => u.Votos).HasForeignKey(v => v.IdUsuario).HasConstraintName("fk_obra_voto_usuario").OnDelete(DeleteBehavior.Cascade);
            });


            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
