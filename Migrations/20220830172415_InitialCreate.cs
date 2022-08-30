using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MatrizPlanificacion.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    EstadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    tipoEstado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.EstadoId);
                });

            migrationBuilder.CreateTable(
                name: "Etapas",
                columns: table => new
                {
                    EtapaId = table.Column<Guid>(type: "uuid", nullable: false),
                    tipoEtapa = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etapas", x => x.EtapaId);
                });

            migrationBuilder.CreateTable(
                name: "PlantaUnidadAreas",
                columns: table => new
                {
                    PlantaUnidadAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    PadreId = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    tipo = table.Column<char>(type: "character(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantaUnidadAreas", x => x.PlantaUnidadAreaId);
                    table.ForeignKey(
                        name: "FK_PlantaUnidadAreas_PlantaUnidadAreas_PadreId",
                        column: x => x.PadreId,
                        principalTable: "PlantaUnidadAreas",
                        principalColumn: "PlantaUnidadAreaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcedimientoContrataciones",
                columns: table => new
                {
                    ProcedimientoContratacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    tipoProcedimiento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedimientoContrataciones", x => x.ProcedimientoContratacionId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_PlantaUnidadAreas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "PlantaUnidadAreas",
                        principalColumn: "PlantaUnidadAreaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    AlertaDSPPPId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcesoCompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcionAlerta = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.AlertaDSPPPId);
                });

            migrationBuilder.CreateTable(
                name: "Contractuales",
                columns: table => new
                {
                    ContractualId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrecontractualId = table.Column<Guid>(type: "uuid", nullable: false),
                    fechaSuscripcion = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaFinalizacion = table.Column<DateOnly>(type: "date", nullable: false),
                    rucOferente = table.Column<int>(type: "integer", nullable: false),
                    nombreProveedor = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractuales", x => x.ContractualId);
                });

            migrationBuilder.CreateTable(
                name: "Observaciones",
                columns: table => new
                {
                    ObservacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcesoId = table.Column<Guid>(type: "uuid", nullable: false),
                    fechaObsservacion = table.Column<DateOnly>(type: "date", nullable: false),
                    descripcionObservacion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observaciones", x => x.ObservacionId);
                });

            migrationBuilder.CreateTable(
                name: "Precontractuales",
                columns: table => new
                {
                    PrecontractualId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreparatoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractualId = table.Column<Guid>(type: "uuid", nullable: false),
                    fechaAdjudicacion = table.Column<DateOnly>(type: "date", nullable: false),
                    valorAdjudicado = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    administradorContrato = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precontractuales", x => x.PrecontractualId);
                    table.ForeignKey(
                        name: "FK_Precontractuales_Contractuales_ContractualId",
                        column: x => x.ContractualId,
                        principalTable: "Contractuales",
                        principalColumn: "ContractualId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Preparatorias",
                columns: table => new
                {
                    PreparatoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcesoId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrecontractualId = table.Column<Guid>(type: "uuid", nullable: false),
                    fechaProgramada = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaSolicitud = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaRespuesta = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaMesa = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaEmision = table.Column<DateOnly>(type: "date", nullable: false),
                    valorCertificado = table.Column<decimal>(type: "numeric", nullable: false),
                    fechaReal = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaAutorizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fechaPublicacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preparatorias", x => x.PreparatoriaId);
                    table.ForeignKey(
                        name: "FK_Preparatorias_Precontractuales_PrecontractualId",
                        column: x => x.PrecontractualId,
                        principalTable: "Precontractuales",
                        principalColumn: "PrecontractualId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcesoCompras",
                columns: table => new
                {
                    ProcesoCompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcedimientoId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreparatoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EtapaId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantaId = table.Column<Guid>(type: "uuid", nullable: false),
                    numProceso = table.Column<int>(type: "integer", nullable: false),
                    cpc = table.Column<long>(type: "bigint", nullable: false),
                    grupoGasto = table.Column<int>(type: "integer", nullable: false),
                    itemPresup = table.Column<long>(type: "bigint", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    total = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    cuatrimestre = table.Column<int>(type: "integer", nullable: false),
                    mesPlanificado = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcesoCompras", x => x.ProcesoCompraId);
                    table.ForeignKey(
                        name: "FK_ProcesoCompras_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "EstadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcesoCompras_Etapas_EtapaId",
                        column: x => x.EtapaId,
                        principalTable: "Etapas",
                        principalColumn: "EtapaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcesoCompras_PlantaUnidadAreas_PlantaId",
                        column: x => x.PlantaId,
                        principalTable: "PlantaUnidadAreas",
                        principalColumn: "PlantaUnidadAreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcesoCompras_Preparatorias_PreparatoriaId",
                        column: x => x.PreparatoriaId,
                        principalTable: "Preparatorias",
                        principalColumn: "PreparatoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcesoCompras_ProcedimientoContrataciones_ProcedimientoId",
                        column: x => x.ProcedimientoId,
                        principalTable: "ProcedimientoContrataciones",
                        principalColumn: "ProcedimientoContratacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_ProcesoCompraId",
                table: "Alertas",
                column: "ProcesoCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AreaId",
                table: "AspNetUsers",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contractuales_PrecontractualId",
                table: "Contractuales",
                column: "PrecontractualId");

            migrationBuilder.CreateIndex(
                name: "IX_Observaciones_ProcesoId",
                table: "Observaciones",
                column: "ProcesoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantaUnidadAreas_PadreId",
                table: "PlantaUnidadAreas",
                column: "PadreId");

            migrationBuilder.CreateIndex(
                name: "IX_Precontractuales_ContractualId",
                table: "Precontractuales",
                column: "ContractualId");

            migrationBuilder.CreateIndex(
                name: "IX_Precontractuales_PreparatoriaId",
                table: "Precontractuales",
                column: "PreparatoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Preparatorias_PrecontractualId",
                table: "Preparatorias",
                column: "PrecontractualId");

            migrationBuilder.CreateIndex(
                name: "IX_Preparatorias_ProcesoId",
                table: "Preparatorias",
                column: "ProcesoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcesoCompras_EstadoId",
                table: "ProcesoCompras",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesoCompras_EtapaId",
                table: "ProcesoCompras",
                column: "EtapaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesoCompras_PlantaId",
                table: "ProcesoCompras",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesoCompras_PreparatoriaId",
                table: "ProcesoCompras",
                column: "PreparatoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesoCompras_ProcedimientoId",
                table: "ProcesoCompras",
                column: "ProcedimientoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alertas_ProcesoCompras_ProcesoCompraId",
                table: "Alertas",
                column: "ProcesoCompraId",
                principalTable: "ProcesoCompras",
                principalColumn: "ProcesoCompraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractuales_Precontractuales_PrecontractualId",
                table: "Contractuales",
                column: "PrecontractualId",
                principalTable: "Precontractuales",
                principalColumn: "PrecontractualId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Observaciones_ProcesoCompras_ProcesoId",
                table: "Observaciones",
                column: "ProcesoId",
                principalTable: "ProcesoCompras",
                principalColumn: "ProcesoCompraId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Precontractuales_Preparatorias_PreparatoriaId",
                table: "Precontractuales",
                column: "PreparatoriaId",
                principalTable: "Preparatorias",
                principalColumn: "PreparatoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Preparatorias_ProcesoCompras_ProcesoId",
                table: "Preparatorias",
                column: "ProcesoId",
                principalTable: "ProcesoCompras",
                principalColumn: "ProcesoCompraId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preparatorias_ProcesoCompras_ProcesoId",
                table: "Preparatorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractuales_Precontractuales_PrecontractualId",
                table: "Contractuales");

            migrationBuilder.DropForeignKey(
                name: "FK_Preparatorias_Precontractuales_PrecontractualId",
                table: "Preparatorias");

            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Observaciones");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProcesoCompras");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Etapas");

            migrationBuilder.DropTable(
                name: "PlantaUnidadAreas");

            migrationBuilder.DropTable(
                name: "ProcedimientoContrataciones");

            migrationBuilder.DropTable(
                name: "Precontractuales");

            migrationBuilder.DropTable(
                name: "Contractuales");

            migrationBuilder.DropTable(
                name: "Preparatorias");
        }
    }
}
