using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace MultiNinja.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProcessorProgresses",
                columns: table => new
                {
                    ProcessorName = table.Column<string>(type: "varchar(255)", nullable: false),
                    LastProcessedPosition = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorProgresses", x => x.ProcessorName);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Streams",
                columns: table => new
                {
                    StreamId = table.Column<Guid>(type: "char(36)", nullable: false),
                    EntityType = table.Column<string>(type: "longtext", nullable: false),
                    EntityId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streams", x => x.StreamId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "char(36)", nullable: false),
                    StreamId = table.Column<Guid>(type: "char(36)", nullable: false),
                    EntityType = table.Column<string>(type: "longtext", nullable: false),
                    EventTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TypeName = table.Column<string>(type: "longtext", nullable: false),
                    SerializedEvent = table.Column<string>(type: "longtext", nullable: false),
                    Version = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Position = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.UniqueConstraint("AK_Events_Position", x => x.Position);
                    table.ForeignKey(
                        name: "FK_Events_Streams_StreamId",
                        column: x => x.StreamId,
                        principalTable: "Streams",
                        principalColumn: "StreamId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StreamId",
                table: "Events",
                column: "StreamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "ProcessorProgresses");

            migrationBuilder.DropTable(
                name: "Streams");
        }
    }
}
