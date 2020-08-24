using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UniLife.Storage.Migrations
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
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Binas",
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
                    table.PrimaryKey("PK_Binas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BursTips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    Kod = table.Column<int>(nullable: false),
                    TutarOran = table.Column<double>(nullable: false),
                    Tutar1Oran2 = table.Column<int>(nullable: false),
                    YoksisKod = table.Column<int>(nullable: false),
                    Engelli = table.Column<bool>(nullable: false),
                    Sehit = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BursTips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CezaTips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    Kod = table.Column<string>(nullable: true),
                    YoksisTip = table.Column<string>(nullable: true),
                    SistemeGiremez = table.Column<bool>(nullable: false),
                    DersKayitIzin = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CezaTips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonemTips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    isActiveDonemTip = table.Column<bool>(nullable: false),
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
                name: "Ils",
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
                    table.PrimaryKey("PK_Ils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KayitNedens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    Kod = table.Column<int>(nullable: false),
                    YoksisKod = table.Column<int>(nullable: false),
                    YoksisStatusKod = table.Column<int>(nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    KisaAdEn = table.Column<string>(nullable: true),
                    YuzdeOn = table.Column<bool>(nullable: false),
                    DersKayitKuralDisi = table.Column<bool>(nullable: false),
                    OzelOgr = table.Column<bool>(nullable: false),
                    HarcTahakkukEtmez = table.Column<bool>(nullable: false),
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
                    OgrenciDurum = table.Column<string>(nullable: true),
                    Aktif = table.Column<bool>(nullable: false),
                    YoksisKod = table.Column<int>(nullable: false),
                    YoksisStatuKod = table.Column<int>(nullable: false),
                    AdEn = table.Column<string>(nullable: true),
                    KisaAd = table.Column<string>(nullable: true),
                    KisaAdEn = table.Column<string>(nullable: true),
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
                name: "ProgramTur",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramTur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SinavTips",
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
                    table.PrimaryKey("PK_SinavTips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SinavTurs",
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
                    table.PrimaryKey("PK_SinavTurs", x => x.Id);
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
                name: "YabancıBasvurus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ulke = table.Column<string>(nullable: true),
                    Sehir = table.Column<string>(nullable: true),
                    Ad = table.Column<string>(nullable: true),
                    Soyad = table.Column<string>(nullable: true),
                    BabaAd = table.Column<string>(nullable: true),
                    AnaAd = table.Column<string>(nullable: true),
                    DogumYer = table.Column<string>(nullable: true),
                    DogumTarih = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Cinsiyet = table.Column<string>(nullable: true),
                    PassportNo = table.Column<string>(nullable: true),
                    YasaUlke = table.Column<string>(nullable: true),
                    IletisimBilgisi = table.Column<string>(nullable: true),
                    Adres = table.Column<string>(nullable: true),
                    LiseMezuniyetAlan = table.Column<string>(nullable: true),
                    LiseDurum = table.Column<string>(nullable: true),
                    LiseMezunNot = table.Column<decimal>(nullable: false),
                    Tercih1 = table.Column<string>(nullable: true),
                    Tercih2 = table.Column<string>(nullable: true),
                    Tercih3 = table.Column<string>(nullable: true),
                    Tercih4 = table.Column<string>(nullable: true),
                    Tercih5 = table.Column<string>(nullable: true),
                    Accept = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YabancıBasvurus", x => x.Id);
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
                name: "Akademisyens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    Soyad = table.Column<string>(nullable: true),
                    OgrtNo = table.Column<string>(nullable: true),
                    TCKN = table.Column<string>(maxLength: 11, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Eimg = table.Column<string>(nullable: true),
                    Durum = table.Column<bool>(nullable: false),
                    KayitTarih = table.Column<DateTime>(nullable: true),
                    AyrilTarih = table.Column<DateTime>(nullable: true),
                    Diller = table.Column<string>(nullable: true),
                    EgitimBilg = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Telefon2 = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Akademisyens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Akademisyens_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
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
                name: "Askerliks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Durum = table.Column<int>(nullable: false),
                    Tecil = table.Column<DateTime>(nullable: true),
                    Terhis = table.Column<DateTime>(nullable: true),
                    Alinis = table.Column<DateTime>(nullable: true),
                    Islem = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Askerliks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Askerliks_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Osyms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    OsymTip = table.Column<string>(nullable: true),
                    Yil = table.Column<string>(nullable: true),
                    Puan = table.Column<string>(nullable: true),
                    PuanTuru = table.Column<string>(nullable: true),
                    TercihSirasi = table.Column<int>(nullable: false),
                    BolumKodu = table.Column<string>(nullable: true),
                    PuanYuzde = table.Column<string>(nullable: true),
                    BasariSira = table.Column<int>(nullable: false),
                    BasariPuan = table.Column<double>(nullable: false),
                    OkulBirinci = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Osyms_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    Soyad = table.Column<string>(nullable: true),
                    PersNo = table.Column<string>(nullable: true),
                    TCKN = table.Column<string>(maxLength: 11, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Eimg = table.Column<string>(nullable: true),
                    Durum = table.Column<bool>(nullable: false),
                    KayitTarih = table.Column<DateTime>(nullable: true),
                    AyrilTarih = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personels_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
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
                name: "Dersliks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    BinaId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersliks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dersliks_Binas_BinaId",
                        column: x => x.BinaId,
                        principalTable: "Binas",
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
                    Yil = table.Column<int>(nullable: false),
                    Ad = table.Column<string>(nullable: true),
                    KisaAd = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    KisaAdEn = table.Column<string>(nullable: true),
                    BasTarih = table.Column<DateTime>(nullable: true),
                    BitTarih = table.Column<DateTime>(nullable: true),
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
                name: "Nufuss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Adres = table.Column<string>(nullable: true),
                    IlId = table.Column<int>(nullable: false),
                    Ilce = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Banka = table.Column<string>(nullable: true),
                    Sube = table.Column<string>(nullable: true),
                    SubeKod = table.Column<string>(nullable: true),
                    HesapNo = table.Column<string>(nullable: true),
                    Iban = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nufuss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nufuss_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nufuss_Ils_IlId",
                        column: x => x.IlId,
                        principalTable: "Ils",
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
                    Kod = table.Column<string>(nullable: true),
                    UniversiteId = table.Column<int>(nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    AdEnKisa = table.Column<string>(nullable: true),
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
                    IsBologna = table.Column<bool>(nullable: false),
                    BolognaIcerikTR = table.Column<string>(nullable: true),
                    BolognaIcerikEN = table.Column<string>(nullable: true),
                    BirimID = table.Column<int>(nullable: false),
                    Durum = table.Column<bool>(nullable: false),
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
                name: "AkademikTakvims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 600, nullable: false),
                    Kod = table.Column<int>(nullable: false),
                    DonemId = table.Column<int>(nullable: false),
                    FakulteId = table.Column<int>(nullable: true),
                    BasTarih = table.Column<DateTime>(nullable: true),
                    BitTarih = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkademikTakvims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AkademikTakvims_Donems_DonemId",
                        column: x => x.DonemId,
                        principalTable: "Donems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AkademikTakvims_Fakultes_FakulteId",
                        column: x => x.FakulteId,
                        principalTable: "Fakultes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bolums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 400, nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    Kod = table.Column<string>(nullable: true),
                    AdEn = table.Column<string>(nullable: true),
                    KisaAdEn = table.Column<string>(nullable: true),
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
                    FakulteId = table.Column<int>(nullable: false),
                    ProgramTurId = table.Column<int>(nullable: false),
                    Kod = table.Column<int>(nullable: false),
                    AdEn = table.Column<string>(nullable: true),
                    KisaAd = table.Column<string>(nullable: true),
                    KisaAdEn = table.Column<string>(nullable: true),
                    OptKod = table.Column<string>(nullable: true),
                    AnaBolum = table.Column<int>(nullable: false),
                    Adres = table.Column<string>(nullable: true),
                    Iletisim = table.Column<string>(nullable: true),
                    IsHazirlik = table.Column<bool>(nullable: false),
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
                    YoksisBirimKod = table.Column<int>(nullable: false),
                    MezuniyetUnvan = table.Column<string>(nullable: true),
                    Durum = table.Column<bool>(nullable: false),
                    Yillik = table.Column<bool>(nullable: false),
                    Yerleske = table.Column<string>(nullable: true),
                    YoksisDurum = table.Column<bool>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Programs_Fakultes_FakulteId",
                        column: x => x.FakulteId,
                        principalTable: "Fakultes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Programs_ProgramTur_ProgramTurId",
                        column: x => x.ProgramTurId,
                        principalTable: "ProgramTur",
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
                name: "Derss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    Kod = table.Column<string>(nullable: true),
                    DonemId = table.Column<int>(nullable: false),
                    MufredatId = table.Column<int>(nullable: false),
                    FakulteId = table.Column<int>(nullable: false),
                    BolumId = table.Column<int>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
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
                    Zorunlu = table.Column<bool>(nullable: false),
                    SecmeliKodu = table.Column<string>(nullable: true),
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
                        name: "FK_Derss_Bolums_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Derss_Donems_DonemId",
                        column: x => x.DonemId,
                        principalTable: "Donems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Derss_Fakultes_FakulteId",
                        column: x => x.FakulteId,
                        principalTable: "Fakultes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Derss_Mufredats_MufredatId",
                        column: x => x.MufredatId,
                        principalTable: "Mufredats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Derss_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ogrencis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<Guid>(nullable: false),
                    Ad = table.Column<string>(maxLength: 300, nullable: false),
                    Soyad = table.Column<string>(nullable: true),
                    OgrNo = table.Column<long>(nullable: false),
                    TCKN = table.Column<string>(maxLength: 11, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FakulteId = table.Column<int>(nullable: false),
                    BolumId = table.Column<int>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
                    MufredatId = table.Column<int>(nullable: false),
                    Eimg = table.Column<string>(nullable: true),
                    KayitNedenId = table.Column<int>(nullable: false),
                    OgrenimDurumId = table.Column<int>(nullable: false),
                    OgrenimTurId = table.Column<int>(nullable: false),
                    Durum = table.Column<bool>(nullable: false),
                    DanismanId = table.Column<int>(nullable: true),
                    DanismanIkiId = table.Column<int>(nullable: true),
                    DnmSnfGecBilgi = table.Column<string>(nullable: true),
                    AskerDurum = table.Column<string>(nullable: true),
                    KayitTarih = table.Column<DateTime>(nullable: true),
                    AyrilTarih = table.Column<DateTime>(nullable: true),
                    AnaOgrNo = table.Column<string>(nullable: true),
                    Sinif = table.Column<int>(nullable: false),
                    GerekenTopUcret = table.Column<decimal>(nullable: false),
                    OdenenTopUcret = table.Column<decimal>(nullable: false),
                    GenelBakiye = table.Column<decimal>(nullable: false),
                    IsMale = table.Column<bool>(nullable: false),
                    Adres = table.Column<string>(nullable: true),
                    BilgNotu = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrencis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ogrencis_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrencis_Bolums_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrencis_Akademisyens_DanismanId",
                        column: x => x.DanismanId,
                        principalTable: "Akademisyens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrencis_Akademisyens_DanismanIkiId",
                        column: x => x.DanismanIkiId,
                        principalTable: "Akademisyens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogrencis_Fakultes_FakulteId",
                        column: x => x.FakulteId,
                        principalTable: "Fakultes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrencis_KayitNedens_KayitNedenId",
                        column: x => x.KayitNedenId,
                        principalTable: "KayitNedens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrencis_Mufredats_MufredatId",
                        column: x => x.MufredatId,
                        principalTable: "Mufredats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrencis_OgrenimDurums_OgrenimDurumId",
                        column: x => x.OgrenimDurumId,
                        principalTable: "OgrenimDurums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrencis_OgrenimTurs_OgrenimTurId",
                        column: x => x.OgrenimTurId,
                        principalTable: "OgrenimTurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ogrencis_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
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
                    FakulteId = table.Column<int>(nullable: false),
                    BolumId = table.Column<int>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
                    MufredatId = table.Column<int>(nullable: false),
                    Sube = table.Column<int>(nullable: false),
                    AkademisyenId = table.Column<int>(nullable: true),
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
                    Zorunlu = table.Column<bool>(nullable: false),
                    SecmeliKodu = table.Column<string>(nullable: true),
                    Sinif = table.Column<int>(nullable: false),
                    ODTekrar = table.Column<int>(nullable: true),
                    ADKayit = table.Column<int>(nullable: true),
                    TopKont = table.Column<int>(nullable: false),
                    BolDisKont = table.Column<int>(nullable: false),
                    AltKont = table.Column<int>(nullable: false),
                    BolDisKota = table.Column<int>(nullable: false),
                    AltKota = table.Column<int>(nullable: false),
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
                        name: "FK_DersAcilans_Akademisyens_AkademisyenId",
                        column: x => x.AkademisyenId,
                        principalTable: "Akademisyens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DersAcilans_Bolums_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_DersAcilans_Fakultes_FakulteId",
                        column: x => x.FakulteId,
                        principalTable: "Fakultes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersAcilans_Mufredats_MufredatId",
                        column: x => x.MufredatId,
                        principalTable: "Mufredats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersAcilans_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrCezas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(nullable: false),
                    CezaTarih = table.Column<DateTime>(nullable: true),
                    CezaAd = table.Column<string>(nullable: true),
                    CezaDesc = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrCezas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrCezas_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrDondurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(nullable: false),
                    DondTarih = table.Column<DateTime>(nullable: true),
                    IsDond = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrDondurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrDondurs_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrenciDigers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(nullable: false),
                    GelUniv = table.Column<string>(nullable: true),
                    GelBolum = table.Column<string>(nullable: true),
                    GelBirim = table.Column<string>(nullable: true),
                    DondTarih = table.Column<DateTime>(nullable: true),
                    IsDond = table.Column<bool>(nullable: false),
                    CezaTarih = table.Column<DateTime>(nullable: true),
                    CezaAd = table.Column<string>(nullable: true),
                    CezaDesc = table.Column<string>(nullable: true),
                    StajTarihBas = table.Column<DateTime>(nullable: true),
                    StajTarihSon = table.Column<DateTime>(nullable: true),
                    StajSirket = table.Column<string>(nullable: true),
                    TezKonu = table.Column<string>(nullable: true),
                    TezTarih = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciDigers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrenciDigers_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrGeciss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(nullable: false),
                    GelUniv = table.Column<string>(nullable: true),
                    GelBolum = table.Column<string>(nullable: true),
                    GelBirim = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrGeciss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrGeciss_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrHarcs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(nullable: false),
                    DonemId = table.Column<int>(nullable: false),
                    Tipi = table.Column<string>(nullable: true),
                    Turu = table.Column<string>(nullable: true),
                    Tutar = table.Column<double>(nullable: false),
                    IadeTutar = table.Column<double>(nullable: false),
                    TahakkukTarih = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrHarcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrHarcs_Donems_DonemId",
                        column: x => x.DonemId,
                        principalTable: "Donems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgrHarcs_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrStajs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(nullable: false),
                    StajTarihBas = table.Column<DateTime>(nullable: true),
                    StajTarihSon = table.Column<DateTime>(nullable: true),
                    StajSirket = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrStajs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrStajs_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OgrTezs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OgrenciId = table.Column<int>(nullable: false),
                    TezKonu = table.Column<string>(nullable: true),
                    TezTarih = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrTezs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OgrTezs_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DersKayits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DersAcilanId = table.Column<int>(nullable: false),
                    OgrenciId = table.Column<int>(nullable: false),
                    DersYerineSecilenId = table.Column<int>(nullable: true),
                    DersYerineSecilenAd = table.Column<string>(nullable: true),
                    Ort = table.Column<double>(nullable: false),
                    Carpan = table.Column<double>(nullable: false),
                    SonucDurum = table.Column<int>(nullable: false),
                    Sonuc = table.Column<int>(nullable: false),
                    HBN = table.Column<int>(nullable: false),
                    TSkor = table.Column<double>(nullable: false),
                    HarfNot = table.Column<string>(nullable: true),
                    GecDurum = table.Column<bool>(nullable: false),
                    AlTip = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersKayits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DersKayits_DersAcilans_DersAcilanId",
                        column: x => x.DersAcilanId,
                        principalTable: "DersAcilans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersKayits_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DerslikRezervs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Subject = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    IsAllDay = table.Column<bool>(nullable: true),
                    CategoryColor = table.Column<string>(nullable: true),
                    RecurrenceRule = table.Column<string>(nullable: true),
                    RecurrenceID = table.Column<int>(nullable: true),
                    RecurrenceException = table.Column<string>(nullable: true),
                    StartTimezone = table.Column<string>(nullable: true),
                    EndTimezone = table.Column<string>(nullable: true),
                    DersAcilanId = table.Column<int>(nullable: false),
                    IsSinav = table.Column<bool>(nullable: false),
                    DerslikId = table.Column<int>(nullable: false),
                    IsBlock = table.Column<bool>(nullable: false),
                    ElementType = table.Column<string>(nullable: true),
                    StartTimeValue = table.Column<DateTime>(nullable: false),
                    EndTimeValue = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerslikRezervs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DerslikRezervs_DersAcilans_DersAcilanId",
                        column: x => x.DersAcilanId,
                        principalTable: "DersAcilans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DerslikRezervs_Dersliks_DerslikId",
                        column: x => x.DerslikId,
                        principalTable: "Dersliks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sinavs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(nullable: true),
                    DersAcilanId = table.Column<int>(nullable: false),
                    SinavTipId = table.Column<int>(nullable: false),
                    SinavTurId = table.Column<int>(nullable: false),
                    SablonAd = table.Column<string>(nullable: true),
                    EtkiOran = table.Column<int>(maxLength: 100, nullable: false),
                    IsKilit = table.Column<bool>(nullable: false),
                    Tarih = table.Column<DateTime>(nullable: true),
                    TarihIlan = table.Column<bool>(nullable: false),
                    KisaAd = table.Column<string>(nullable: true),
                    MazeretiSinavId = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinavs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sinavs_DersAcilans_DersAcilanId",
                        column: x => x.DersAcilanId,
                        principalTable: "DersAcilans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sinavs_SinavTips_SinavTipId",
                        column: x => x.SinavTipId,
                        principalTable: "SinavTips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sinavs_SinavTurs_SinavTurId",
                        column: x => x.SinavTurId,
                        principalTable: "SinavTurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SinavKayits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SinavId = table.Column<int>(nullable: false),
                    OgrenciId = table.Column<int>(nullable: false),
                    OgrNot = table.Column<double>(nullable: false),
                    MazeretiSinavKayitId = table.Column<long>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinavKayits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinavKayits_Ogrencis_OgrenciId",
                        column: x => x.OgrenciId,
                        principalTable: "Ogrencis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SinavKayits_Sinavs_SinavId",
                        column: x => x.SinavId,
                        principalTable: "Sinavs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkademikTakvims_DonemId",
                table: "AkademikTakvims",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_AkademikTakvims_FakulteId",
                table: "AkademikTakvims",
                column: "FakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_Akademisyens_ApplicationUserId",
                table: "Akademisyens",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiLogs_ApplicationUserId",
                table: "ApiLogs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Askerliks_ApplicationUserId",
                table: "Askerliks",
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
                name: "IX_DersAcilans_AkademisyenId",
                table: "DersAcilans",
                column: "AkademisyenId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_BolumId",
                table: "DersAcilans",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_DersId",
                table: "DersAcilans",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_DonemId",
                table: "DersAcilans",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_FakulteId",
                table: "DersAcilans",
                column: "FakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_MufredatId",
                table: "DersAcilans",
                column: "MufredatId");

            migrationBuilder.CreateIndex(
                name: "IX_DersAcilans_ProgramId",
                table: "DersAcilans",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_DersKayits_DersAcilanId",
                table: "DersKayits",
                column: "DersAcilanId");

            migrationBuilder.CreateIndex(
                name: "IX_DersKayits_OgrenciId",
                table: "DersKayits",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_DerslikRezervs_DersAcilanId",
                table: "DerslikRezervs",
                column: "DersAcilanId");

            migrationBuilder.CreateIndex(
                name: "IX_DerslikRezervs_DerslikId",
                table: "DerslikRezervs",
                column: "DerslikId");

            migrationBuilder.CreateIndex(
                name: "IX_Dersliks_BinaId",
                table: "Dersliks",
                column: "BinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_BolumId",
                table: "Derss",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_DonemId",
                table: "Derss",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_FakulteId",
                table: "Derss",
                column: "FakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_MufredatId",
                table: "Derss",
                column: "MufredatId");

            migrationBuilder.CreateIndex(
                name: "IX_Derss_ProgramId",
                table: "Derss",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Donems_DonemTipId",
                table: "Donems",
                column: "DonemTipId");

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
                name: "IX_Nufuss_ApplicationUserId",
                table: "Nufuss",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Nufuss_IlId",
                table: "Nufuss",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrCezas_OgrenciId",
                table: "OgrCezas",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrDondurs_OgrenciId",
                table: "OgrDondurs",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDigers_OgrenciId",
                table: "OgrenciDigers",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_ApplicationUserId",
                table: "Ogrencis",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_BolumId",
                table: "Ogrencis",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_DanismanId",
                table: "Ogrencis",
                column: "DanismanId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_DanismanIkiId",
                table: "Ogrencis",
                column: "DanismanIkiId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_FakulteId",
                table: "Ogrencis",
                column: "FakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_KayitNedenId",
                table: "Ogrencis",
                column: "KayitNedenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_MufredatId",
                table: "Ogrencis",
                column: "MufredatId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_OgrenimDurumId",
                table: "Ogrencis",
                column: "OgrenimDurumId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_OgrenimTurId",
                table: "Ogrencis",
                column: "OgrenimTurId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrencis_ProgramId",
                table: "Ogrencis",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrGeciss_OgrenciId",
                table: "OgrGeciss",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrHarcs_DonemId",
                table: "OgrHarcs",
                column: "DonemId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrHarcs_OgrenciId",
                table: "OgrHarcs",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrStajs_OgrenciId",
                table: "OgrStajs",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrTezs_OgrenciId",
                table: "OgrTezs",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_Osyms_ApplicationUserId",
                table: "Osyms",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Personels_ApplicationUserId",
                table: "Personels",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_BolumId",
                table: "Programs",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_FakulteId",
                table: "Programs",
                column: "FakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ProgramTurId",
                table: "Programs",
                column: "ProgramTurId");

            migrationBuilder.CreateIndex(
                name: "IX_SinavKayits_OgrenciId",
                table: "SinavKayits",
                column: "OgrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_SinavKayits_SinavId",
                table: "SinavKayits",
                column: "SinavId");

            migrationBuilder.CreateIndex(
                name: "IX_Sinavs_DersAcilanId",
                table: "Sinavs",
                column: "DersAcilanId");

            migrationBuilder.CreateIndex(
                name: "IX_Sinavs_SinavTipId",
                table: "Sinavs",
                column: "SinavTipId");

            migrationBuilder.CreateIndex(
                name: "IX_Sinavs_SinavTurId",
                table: "Sinavs",
                column: "SinavTurId");

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
                name: "AkademikTakvims");

            migrationBuilder.DropTable(
                name: "ApiLogs");

            migrationBuilder.DropTable(
                name: "Askerliks");

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
                name: "BursTips");

            migrationBuilder.DropTable(
                name: "CezaTips");

            migrationBuilder.DropTable(
                name: "DersKayits");

            migrationBuilder.DropTable(
                name: "DerslikRezervs");

            migrationBuilder.DropTable(
                name: "FakulteTurs");

            migrationBuilder.DropTable(
                name: "Harcs");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Nufuss");

            migrationBuilder.DropTable(
                name: "OgrCezas");

            migrationBuilder.DropTable(
                name: "OgrDondurs");

            migrationBuilder.DropTable(
                name: "OgrenciDigers");

            migrationBuilder.DropTable(
                name: "OgrGeciss");

            migrationBuilder.DropTable(
                name: "OgrHarcs");

            migrationBuilder.DropTable(
                name: "OgrStajs");

            migrationBuilder.DropTable(
                name: "OgrTezs");

            migrationBuilder.DropTable(
                name: "Osyms");

            migrationBuilder.DropTable(
                name: "Personels");

            migrationBuilder.DropTable(
                name: "SinavKayits");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "YabancıBasvurus");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Dersliks");

            migrationBuilder.DropTable(
                name: "Ils");

            migrationBuilder.DropTable(
                name: "Ogrencis");

            migrationBuilder.DropTable(
                name: "Sinavs");

            migrationBuilder.DropTable(
                name: "Binas");

            migrationBuilder.DropTable(
                name: "KayitNedens");

            migrationBuilder.DropTable(
                name: "OgrenimDurums");

            migrationBuilder.DropTable(
                name: "DersAcilans");

            migrationBuilder.DropTable(
                name: "SinavTips");

            migrationBuilder.DropTable(
                name: "SinavTurs");

            migrationBuilder.DropTable(
                name: "Akademisyens");

            migrationBuilder.DropTable(
                name: "Derss");

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
                name: "ProgramTur");

            migrationBuilder.DropTable(
                name: "Fakultes");

            migrationBuilder.DropTable(
                name: "OgrenimTurs");

            migrationBuilder.DropTable(
                name: "Universites");
        }
    }
}
