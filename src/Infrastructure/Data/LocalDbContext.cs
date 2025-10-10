using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class LocalDbContext(DbContextOptions<LocalDbContext> options) : DbContext(options)
{
    public DbSet<Saison> Saisons { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<Joueur> Joueurs { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<JoueurEquipe> JoueurEquipes { get; set; }
    public DbSet<JoueurSaison> JoueurSaisons { get; set; }
    public DbSet<JoueurPosition> JoueurPositions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration de JoueurEquipe (table de liaison many-to-many)
        modelBuilder.Entity<JoueurEquipe>()
            .HasKey(je => new { je.JoueurId, je.EquipeId });

        modelBuilder.Entity<JoueurEquipe>()
            .HasOne(je => je.Joueur)
            .WithMany(j => j.JoueurEquipes)
            .HasForeignKey(je => je.JoueurId);

        modelBuilder.Entity<JoueurEquipe>()
            .HasOne(je => je.Equipe)
            .WithMany(e => e.JoueurEquipes)
            .HasForeignKey(je => je.EquipeId);

        // Configuration de JoueurSaison (table de liaison many-to-many)
        modelBuilder.Entity<JoueurSaison>()
            .HasKey(js => new { js.JoueurId, js.SaisonId });

        modelBuilder.Entity<JoueurSaison>()
            .HasOne(js => js.Joueur)
            .WithMany(j => j.JoueurSaisons)
            .HasForeignKey(js => js.JoueurId);

        modelBuilder.Entity<JoueurSaison>()
            .HasOne(js => js.Saison)
            .WithMany(s => s.JoueurSaisons)
            .HasForeignKey(js => js.SaisonId);

        // Configuration de la relation Match-Saison
        modelBuilder.Entity<Match>()
            .HasOne(m => m.Saison)
            .WithMany(s => s.Matches)
            .HasForeignKey(m => m.SaisonId);

        // Configuration de JoueurPosition (table de liaison many-to-many)
        modelBuilder.Entity<JoueurPosition>()
            .HasKey(jp => new { jp.JoueurId, jp.PositionId });

        modelBuilder.Entity<JoueurPosition>()
            .HasOne(jp => jp.Joueur)
            .WithMany(j => j.JoueurPositions)
            .HasForeignKey(jp => jp.JoueurId);

        modelBuilder.Entity<JoueurPosition>()
            .HasOne(jp => jp.Position)
            .WithMany()
            .HasForeignKey(jp => jp.PositionId);

        // Data Seeding - Positions par défaut
        modelBuilder.Entity<Position>().HasData(
            new Position { Id = 1, Nom = "Attaquant" },
            new Position { Id = 2, Nom = "Defenseur" },
            new Position { Id = 3, Nom = "Gardien" }
        );
    }
}