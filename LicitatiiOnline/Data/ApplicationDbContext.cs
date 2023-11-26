using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LicitatiiOnline.Models.DBObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LicitatiiOnline.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<ImagineProdus> ImagineProdus { get; set; } = null!;
        public virtual DbSet<IstoricLicitatii> IstoricLicitatii { get; set; } = null!;
        public virtual DbSet<Licitatii> Licitatii { get; set; } = null!;
        public virtual DbSet<Oferte> Oferte { get; set; } = null!;
        public virtual DbSet<Produs> Produs { get; set; } = null!;
        public virtual DbSet<Recenzii> Recenzii { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<AspNetRole>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetRoleClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            //modelBuilder.Entity<AspNetUser>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);

            //    entity.HasMany(d => d.Roles)
            //        .WithMany(p => p.Users)
            //        .UsingEntity<Dictionary<string, object>>(
            //            "AspNetUserRole",
            //            l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
            //            r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
            //            j =>
            //            {
            //                j.HasKey("UserId", "RoleId");

            //                j.ToTable("AspNetUserRoles");

            //                j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
            //            });
            //});

            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserLogin>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

            //    entity.Property(e => e.ProviderKey).HasMaxLength(128);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserToken>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

            //    entity.Property(e => e.Name).HasMaxLength(128);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});

            modelBuilder.Entity<ImagineProdus>(entity =>
            {
                entity.HasKey(e => e.IdImagine);

                entity.Property(e => e.IdImagine)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Imagine");

                entity.Property(e => e.CaleImagine).HasColumnName("Cale_Imagine");

                entity.Property(e => e.IdProdus).HasColumnName("ID_Produs");

                entity.HasOne(d => d.IdProdusNavigation)
                    .WithMany(p => p.ImagineProdus)
                    .HasForeignKey(d => d.IdProdus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImagineProdus_Produs");
            });

            modelBuilder.Entity<IstoricLicitatii>(entity =>
            {
                entity.HasKey(e => e.IdIstoric)
                    .HasName("PK__Istoric___AC929D8BEB9F573B");

                entity.ToTable("Istoric_Licitatii");

                entity.Property(e => e.IdIstoric)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Istoric");

                entity.Property(e => e.DataIncheiere)
                    .HasColumnType("datetime")
                    .HasColumnName("Data_Incheiere");

                entity.Property(e => e.IdLicitatie).HasColumnName("ID_Licitatie");

                entity.Property(e => e.PretVandut)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Pret_Vandut");

                entity.Property(e => e.UtilizatorCastigator).HasColumnName("Utilizator_Castigator");

                entity.HasOne(d => d.IdLicitatieNavigation)
                    .WithMany(p => p.IstoricLicitatiis)
                    .HasForeignKey(d => d.IdLicitatie)
                    .HasConstraintName("FK_Licitatie_Istoric");
            });

            modelBuilder.Entity<Licitatii>(entity =>
            {
                entity.HasKey(e => e.IdLicitatie)
                    .HasName("PK__Licitati__CD73ABE5FC2E35F9");

                entity.ToTable("Licitatii");

                entity.Property(e => e.IdLicitatie)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Licitatie");

                entity.Property(e => e.DataLicitatie)
                    .HasColumnType("datetime")
                    .HasColumnName("Data_Licitatie");

                entity.Property(e => e.Nume_Cumparator).HasColumnName("Nume_Cumparator");

                entity.Property(e => e.IdProdus).HasColumnName("ID_Produs");


                entity.Property(e => e.PretActual)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Pret_Actual");

                entity.Property(e => e.Stare).HasMaxLength(20);

                entity.HasOne(d => d.IdProdusNavigation)
                    .WithMany(p => p.Licitatii)
                    .HasForeignKey(d => d.IdProdus)
                    .HasConstraintName("FK_Produs_Licitatie");
            });

            modelBuilder.Entity<Oferte>(entity =>
            {
                entity.HasKey(e => e.IdOferta)
                    .HasName("PK__Oferte__DA770106B78A4301");

                entity.ToTable("Oferte");

                entity.Property(e => e.IdOferta)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Oferta");

                entity.Property(e => e.DataOferire)
                    .HasColumnType("datetime")
                    .HasColumnName("Data_Oferire");

                entity.Property(e => e.IdLicitatie).HasColumnName("ID_Licitatie");

                entity.Property(e => e.Nume_Ofertant).HasColumnName("Nume_Ofertant");


                entity.Property(e => e.SumaOfertata)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Suma_Ofertata");

                entity.HasOne(d => d.IdLicitatieNavigation)
                    .WithMany(p => p.Ofertes)
                    .HasForeignKey(d => d.IdLicitatie)
                    .HasConstraintName("FK_Licitatie_Oferta");
            });

            modelBuilder.Entity<Produs>(entity =>
            {
                entity.HasKey(e => e.IdProdus)
                    .HasName("PK__Produs__C6921B12607911C0");

                entity.Property(e => e.IdProdus)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Produs");

                entity.Property(e => e.Categorie).HasMaxLength(50);

                entity.Property(e => e.DataIncheiereLicitatie)
                    .HasColumnType("datetime")
                    .HasColumnName("Data_Incheiere_Licitatie");

                entity.Property(e => e.NumeProdus)
                    .HasMaxLength(100)
                    .HasColumnName("Nume_Produs");

                entity.Property(e => e.PretPornire)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Pret_Pornire");

                entity.Property(e => e.CaleImagine)
                    .HasMaxLength(5000)
                    .HasColumnName("Cale_Imagine");

                entity.Property(e => e.Stare).HasMaxLength(10);

                entity.Property(e => e.Utilizator_Ofertant_Curent).HasColumnName("Utilizator_Ofertant_Curent");
            });

            modelBuilder.Entity<Recenzii>(entity =>
            {
                entity.HasKey(e => e.IdRecenzie)
                    .HasName("PK__Recenzii__15081335616C0526");

                entity.ToTable("Recenzii");

                entity.Property(e => e.IdRecenzie)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Recenzie");

                entity.Property(e => e.IdUtilizator).HasColumnName("ID_Utilizator");

                entity.Property(e => e.NumeUtilizator)
                    .HasMaxLength(50)
                    .HasColumnName("Nume_Utilizator");

                entity.Property(e => e.TextRecenzie).HasColumnName("Text_Recenzie");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
