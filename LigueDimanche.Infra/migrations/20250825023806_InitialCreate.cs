using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LigueDimanche.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Saisons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saisons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDeNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Positions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaisonJoueurs",
                columns: table => new
                {
                    SaisonId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EstTempsPlein = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaisonJoueurs", x => new { x.SaisonId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SaisonJoueurs_Saisons_SaisonId",
                        column: x => x.SaisonId,
                        principalTable: "Saisons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaisonJoueurs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipeMembres",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipeMembres", x => new { x.UserId, x.EquipeId });
                    table.ForeignKey(
                        name: "FK_EquipeMembres_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaisonId = table.Column<int>(type: "int", nullable: false),
                    Equipe1Id = table.Column<int>(type: "int", nullable: false),
                    Equipe2Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Equipes_Equipe1Id",
                        column: x => x.Equipe1Id,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Equipes_Equipe2Id",
                        column: x => x.Equipe2Id,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Saisons_SaisonId",
                        column: x => x.SaisonId,
                        principalTable: "Saisons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipeMembres_EquipeId",
                table: "EquipeMembres",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_MatchId",
                table: "Equipes",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Equipe1Id",
                table: "Matches",
                column: "Equipe1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Equipe2Id",
                table: "Matches",
                column: "Equipe2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_SaisonId",
                table: "Matches",
                column: "SaisonId");

            migrationBuilder.CreateIndex(
                name: "IX_SaisonJoueurs_UserId",
                table: "SaisonJoueurs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipeMembres_Equipes_EquipeId",
                table: "EquipeMembres",
                column: "EquipeId",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipes_Matches_MatchId",
                table: "Equipes",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Equipes_Equipe1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Equipes_Equipe2Id",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "EquipeMembres");

            migrationBuilder.DropTable(
                name: "SaisonJoueurs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Equipes");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Saisons");
        }
    }
}
