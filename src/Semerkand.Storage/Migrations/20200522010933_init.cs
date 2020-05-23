using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Semerkand.Storage.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: true),
                    LastName = table.Column<string>(maxLength: 64, nullable: true),
                    FullName = table.Column<string>(maxLength: 64, nullable: true),
                    TCKN = table.Column<string>(maxLength: 11, nullable: true),
                    OgrenciId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonemTips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonemTips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FakulteTurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    Tur = table.Column<string>(nullable: true),
                    TurEn = table.Column<string>(nullable: true),
                    YokasId = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FakulteTurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KayitNedens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    Kod = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KayitNedens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgrenimDurums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    Kod = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenimDurums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgrenimTurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenimTurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                name: "ApiLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestTime = table.Column<DateTime>(nullable: false),
                    ResponseMillis = table.Column<long>(nullable: false),
                    StatusCode = table.Column<int>(nullable: false),
                    Method = table.Column<string>(nullable: false),
                    Path = table.Column<string>(maxLength: 2048, nullable: false),
                    QueryString = table.Column<string>(maxLength: 2048, nullable: true),
                    RequestBody = table.Column<string>(maxLength: 256, nullable: true),
                    ResponseBody = table.Column<string>(maxLength: 256, nullable: true),
                    IPAddress = table.Column<string>(maxLength: 45, nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiLogs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    When = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    OwnerUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenants_AspNetUsers_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    LastPageVisited = table.Column<string>(nullable: true),
                    IsNavOpen = table.Column<bool>(nullable: false),
                    IsNavMinified = table.Column<bool>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DonemTipId = table.Column<int>(nullable: false),
                    Ad = table.Column<string>(nullable: true),
                    KisaAd = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    KisaAdEn = table.Column<string>(nullable: true),
                    BasTarih = table.Column<DateTime>(nullable: false),
                    BitTarih = table.Column<DateTime>(nullable: false),
                    Durum = table.Column<bool>(nullable: false),
                    YokSisDurum = table.Column<bool>(nullable: false),
                    YilTip = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donems_DonemTips_DonemTipId",
                        column: x => x.DonemTipId,
                        principalTable: "DonemTips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fakultes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    Kod = table.Column<int>(nullable: false),
                    UniversiteId = table.Column<int>(nullable: false),
                    FakulteTurId = table.Column<int>(nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    EPosta = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Adres = table.Column<string>(nullable: true),
                    Adres2 = table.Column<string>(nullable: true),
                    Faks = table.Column<string>(nullable: true),
                    Web = table.Column<string>(nullable: true),
                    IlceId = table.Column<int>(nullable: false),
                    OgrenimTurId = table.Column<int>(nullable: false),
                    OgrenimSure = table.Column<int>(nullable: false),
                    IlKod = table.Column<int>(nullable: false),
                    Tip = table.Column<int>(nullable: false),
                    DiplomaAd = table.Column<string>(nullable: true),
                    IsBologna = table.Column<bool>(nullable: false),
                    BolognaIcerikTR = table.Column<string>(nullable: true),
                    BolognaIcerikEN = table.Column<string>(nullable: true),
                    BirimID = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakultes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fakultes_FakulteTurs_FakulteTurId",
                        column: x => x.FakulteTurId,
                        principalTable: "FakulteTurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fakultes_OgrenimTurs_OgrenimTurId",
                        column: x => x.OgrenimTurId,
                        principalTable: "OgrenimTurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fakultes_Universites_UniversiteId",
                        column: x => x.UniversiteId,
                        principalTable: "Universites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bolums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 400, nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    Kod = table.Column<int>(nullable: false),
                    AdEn = table.Column<string>(nullable: true),
                    OsymKod = table.Column<string>(nullable: true),
                    OgrenimTurId = table.Column<int>(nullable: false),
                    FakulteId = table.Column<int>(nullable: false),
                    OgrenimSure = table.Column<int>(nullable: false),
                    Durum = table.Column<bool>(nullable: false),
                    DiplomaAd = table.Column<string>(nullable: true),
                    IsBologna = table.Column<bool>(nullable: false),
                    DiplomaAdEn = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bolums_Fakultes_FakulteId",
                        column: x => x.FakulteId,
                        principalTable: "Fakultes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bolums_OgrenimTurs_OgrenimTurId",
                        column: x => x.OgrenimTurId,
                        principalTable: "OgrenimTurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    BolumId = table.Column<int>(nullable: false),
                    Kod = table.Column<int>(nullable: false),
                    AdEn = table.Column<string>(nullable: true),
                    KisaAd = table.Column<string>(nullable: true),
                    OptKod = table.Column<string>(nullable: true),
                    AnaBolum = table.Column<int>(nullable: false),
                    ProgramTipId = table.Column<int>(nullable: false),
                    ProgramTurId = table.Column<int>(nullable: false),
                    Adres = table.Column<string>(nullable: true),
                    Iletisim = table.Column<string>(nullable: true),
                    FakulteId = table.Column<int>(nullable: false),
                    IsHazırlık = table.Column<bool>(nullable: false),
                    AzamiSure = table.Column<int>(nullable: false),
                    NormalSure = table.Column<int>(nullable: false),
                    OsymKod = table.Column<string>(nullable: true),
                    OsymTur = table.Column<string>(nullable: true),
                    StajDurum = table.Column<bool>(nullable: false),
                    HarcTutar = table.Column<decimal>(nullable: false),
                    GenelKon = table.Column<int>(nullable: false),
                    BarajNot = table.Column<string>(nullable: true),
                    OsymKodBurslu = table.Column<string>(nullable: true),
                    OsymKodYariBurslu = table.Column<string>(nullable: true),
                    TuikKod = table.Column<int>(nullable: false),
                    Dil = table.Column<int>(nullable: false),
                    DiplomaAd = table.Column<string>(nullable: true),
                    IsBologna = table.Column<bool>(nullable: false),
                    BrnOgrOsymKod = table.Column<string>(nullable: true),
                    OsymKodCeyrekBurs = table.Column<string>(nullable: true),
                    DiplomaAdEn = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programs_Bolums_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Harcs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
                    DonemId = table.Column<int>(nullable: false),
                    NormalSure = table.Column<int>(nullable: false),
                    IlkUzatma = table.Column<int>(nullable: false),
                    TakipYillar = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Harcs_Donems_DonemId",
                        column: x => x.DonemId,
                        principalTable: "Donems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Harcs_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mufredats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    Yil = table.Column<int>(nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    BasTarih = table.Column<DateTime>(nullable: false),
                    BitTarih = table.Column<DateTime>(nullable: false),
                    Durum = table.Column<int>(nullable: false),
                    Aktif = table.Column<bool>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
                    KararTarih = table.Column<DateTime>(nullable: false),
                    KararAcik = table.Column<string>(nullable: true),
                    ProgDersGec = table.Column<int>(nullable: false),
                    AraSinavEo = table.Column<int>(nullable: false),
                    YariSonuSinavEo = table.Column<int>(nullable: false),
                    GecmeNot = table.Column<int>(nullable: false),
                    FinalBaraj = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mufredats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mufredats_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenci",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    OgrNo = table.Column<string>(nullable: true),
                    TCKN = table.Column<string>(maxLength: 11, nullable: true),
                    FakulteId = table.Column<int>(nullable: false),
                    BolumId = table.Column<int>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
                    KayitNedenId = table.Column<int>(nullable: false),
                    OgrenimDurumId = table.Column<int>(nullable: false),
                    Durum = table.Column<bool>(nullable: false),
                    AskerDurum = table.Column<string>(nullable: true),
                    KayitTarih = table.Column<DateTime>(nullable: false),
                    AyrilTarih = table.Column<DateTime>(nullable: true),
                    AnaOgrNo = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ogrenci_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrenci_Bolums_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrenci_Fakultes_FakulteId",
                        column: x => x.FakulteId,
                        principalTable: "Fakultes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrenci_KayitNedens_KayitNedenId",
                        column: x => x.KayitNedenId,
                        principalTable: "KayitNedens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrenci_OgrenimDurums_OgrenimDurumId",
                        column: x => x.OgrenimDurumId,
                        principalTable: "OgrenimDurums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrenci_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Derss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    Kod = table.Column<string>(nullable: true),
                    DonemId = table.Column<int>(nullable: false),
                    MufredatId = table.Column<int>(nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    Akts = table.Column<int>(nullable: false),
                    GecmeNotu = table.Column<int>(nullable: false),
                    OptikKod = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    UygSaat = table.Column<int>(nullable: false),
                    LabSaat = table.Column<int>(nullable: false),
                    TeoSaat = table.Column<int>(nullable: false),
                    Kredi = table.Column<double>(nullable: false),
                    Durum = table.Column<bool>(nullable: false),
                    Zorunlu = table.Column<int>(nullable: false),
                    Sinif = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Derss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Derss_Donems_DonemId",
                        column: x => x.DonemId,
                        principalTable: "Donems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Derss_Mufredats_MufredatId",
                        column: x => x.MufredatId,
                        principalTable: "Mufredats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DersAcilans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    Kod = table.Column<string>(nullable: true),
                    DersId = table.Column<int>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
                    DonemId = table.Column<int>(nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    Akts = table.Column<int>(nullable: false),
                    GecmeNotu = table.Column<int>(nullable: false),
                    OptikKod = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    UygSaat = table.Column<int>(nullable: false),
                    LabSaat = table.Column<int>(nullable: false),
                    TeoSaat = table.Column<int>(nullable: false),
                    Kredi = table.Column<double>(nullable: false),
                    Durum = table.Column<bool>(nullable: false),
                    Zorunlu = table.Column<int>(nullable: false),
                    Sinif = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersAcilans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DersAcilans_Derss_DersId",
                        column: x => x.DersId,
                        principalTable: "Derss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersAcilans_Donems_DonemId",
                        column: x => x.DonemId,
                        principalTable: "Donems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersAcilans_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiLogs_ApplicationUserId",
                table: "ApiLogs",
                column: "ApplicationUserId");

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bolums_FakulteId",
                table: "Bolums",
                column: "FakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolums_OgrenimTurId",
                table: "Bolums",
                column: "OgrenimTurId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_DersId",
                table: "DersAcilans",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_DonemId",
                table: "DersAcilans",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_ProgramId",
                table: "DersAcilans",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_DonemId",
                table: "Derss",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_MufredatId",
                table: "Derss",
                column: "MufredatId");

            migrationBuilder.CreateIndex(
                name: "IX_Donems_DonemTipId",
                table: "Donems",
                column: "DonemTipId");

            migrationBuilder.CreateIndex(
                name: "IX_Fakultes_FakulteTurId",
                table: "Fakultes",
                column: "FakulteTurId");

            migrationBuilder.CreateIndex(
                name: "IX_Fakultes_OgrenimTurId",
                table: "Fakultes",
                column: "OgrenimTurId");

            migrationBuilder.CreateIndex(
                name: "IX_Fakultes_UniversiteId",
                table: "Fakultes",
                column: "UniversiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Harcs_DonemId",
                table: "Harcs",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_Harcs_ProgramId",
                table: "Harcs",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_TimeStamp",
                table: "Logs",
                column: "TimeStamp");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserID",
                table: "Messages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Mufredats_ProgramId",
                table: "Mufredats",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_ApplicationUserId",
                table: "Ogrenci",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_BolumId",
                table: "Ogrenci",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_FakulteId",
                table: "Ogrenci",
                column: "FakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_KayitNedenId",
                table: "Ogrenci",
                column: "KayitNedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_OgrenimDurumId",
                table: "Ogrenci",
                column: "OgrenimDurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenci_ProgramId",
                table: "Ogrenci",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_BolumId",
                table: "Programs",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_OwnerUserId",
                table: "Tenants",
                column: "OwnerUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLogs");

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
                name: "DersAcilans");

            migrationBuilder.DropTable(
                name: "Harcs");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Ogrenci");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Derss");

            migrationBuilder.DropTable(
                name: "KayitNedens");

            migrationBuilder.DropTable(
                name: "OgrenimDurums");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Donems");

            migrationBuilder.DropTable(
                name: "Mufredats");

            migrationBuilder.DropTable(
                name: "DonemTips");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Bolums");

            migrationBuilder.DropTable(
                name: "Fakultes");

            migrationBuilder.DropTable(
                name: "FakulteTurs");

            migrationBuilder.DropTable(
                name: "OgrenimTurs");

            migrationBuilder.DropTable(
                name: "Universites");
        }
    }
}
