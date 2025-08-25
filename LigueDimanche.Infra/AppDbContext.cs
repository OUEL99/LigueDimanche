using LigueDimanche.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LigueDimanche.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) {}

        // DbSets pour toutes les entités
        public DbSet<User> Users { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Saison> Saisons { get; set; }
        public DbSet<EquipeMembre> EquipeMembres { get; set; }
        public DbSet<SaisonJoueur> SaisonJoueurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de la relation Many-to-Many User <-> Equipe via EquipeMembre
            modelBuilder.Entity<EquipeMembre>()
                .HasKey(em => new { em.UserId, em.EquipeId });

            modelBuilder.Entity<EquipeMembre>()
                .HasOne(em => em.User)
                .WithMany(u => u.Equipes)
                .HasForeignKey(em => em.UserId);

            modelBuilder.Entity<EquipeMembre>()
                .HasOne(em => em.Equipe)
                .WithMany(e => e.Membres)
                .HasForeignKey(em => em.EquipeId);

            // Configuration de la relation Many-to-Many User <-> Saison via SaisonJoueur
            modelBuilder.Entity<SaisonJoueur>()
                .HasKey(sj => new { sj.SaisonId, sj.UserId });

            modelBuilder.Entity<SaisonJoueur>()
                .HasOne(sj => sj.Saison)
                .WithMany(s => s.Joueurs)
                .HasForeignKey(sj => sj.SaisonId);

            modelBuilder.Entity<SaisonJoueur>()
                .HasOne(sj => sj.User)
                .WithMany(u => u.Saisons)
                .HasForeignKey(sj => sj.UserId);

            // Configuration des relations Match
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Saison)
                .WithMany()
                .HasForeignKey(m => m.SaisonId);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Equipe1)
                .WithMany()
                .HasForeignKey(m => m.Equipe1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Equipe2)
                .WithMany()
                .HasForeignKey(m => m.Equipe2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration des contraintes sur User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.Nom)
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Prenom)
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Telephone)
                .HasMaxLength(20);

            // Configuration des contraintes sur Equipe
            modelBuilder.Entity<Equipe>()
                .Property(e => e.Nom)
                .HasMaxLength(100);
        }
    }
}