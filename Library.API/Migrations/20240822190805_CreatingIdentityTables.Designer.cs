﻿// <auto-generated />
using System;
using Library.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.API.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20240822190805_CreatingIdentityTables")]
    partial class CreatingIdentityTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Library.Domain.Models.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Author");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"),
                            Birthday = new DateOnly(1947, 9, 21),
                            Country = "USA",
                            FirstName = "Stephen",
                            LastName = "King"
                        },
                        new
                        {
                            Id = new Guid("ac9417d1-a277-4974-b41c-25abde25bf38"),
                            Birthday = new DateOnly(1799, 5, 26),
                            Country = "Russia",
                            FirstName = "Alexander",
                            LastName = "Pushkin"
                        },
                        new
                        {
                            Id = new Guid("944936e4-5167-41fe-8373-0540f319c3d3"),
                            Birthday = new DateOnly(1882, 11, 3),
                            Country = "Belarus",
                            FirstName = "Yakub",
                            LastName = "Kolas"
                        });
                });

            modelBuilder.Entity("Library.Domain.Models.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ReceiveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f86f763e-7f80-491d-ac29-ec93cf0048e0"),
                            AuthorId = new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"),
                            GenreId = new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"),
                            ISBN = "978-3-4534-3577-3",
                            Name = "It",
                            ReceiveTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(5957),
                            ReturnTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(5976),
                            Title = "Stephen King’s terrifying, classic #1 New York Times bestseller."
                        },
                        new
                        {
                            Id = new Guid("bd304d0c-66b5-4873-bd22-c83f804ca7b7"),
                            AuthorId = new Guid("2fc63da3-b9ed-480d-9543-a31037599bbb"),
                            GenreId = new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"),
                            ISBN = "978-2-2264-9274-6",
                            Name = "Holly",
                            ReceiveTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(5989),
                            ReturnTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(5990),
                            Title = "Holly Gibney, one of Stephen King’s most compelling and resourceful characters, returns in this chilling novel to solve the gruesome truth behind multiple disappearances in a midwestern town."
                        },
                        new
                        {
                            Id = new Guid("4dccb0be-1de4-4659-80d2-0cf971a0d599"),
                            AuthorId = new Guid("ac9417d1-a277-4974-b41c-25abde25bf38"),
                            GenreId = new Guid("3898242b-a685-4d49-bd06-8c7e34e14d7c"),
                            ISBN = "978-0-4608-7595-0",
                            Name = "Eugene Onegin",
                            ReceiveTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(6000),
                            ReturnTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(6002),
                            Title = "Eugene Onegin is the master work of the poet whom Russians regard as the fountainhead of their literature."
                        },
                        new
                        {
                            Id = new Guid("2c463c56-21eb-4aae-8c2b-a87bcda80256"),
                            AuthorId = new Guid("944936e4-5167-41fe-8373-0540f319c3d3"),
                            GenreId = new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"),
                            ISBN = "978-9-8515-5288-3",
                            Name = "The New Land",
                            ReceiveTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(6011),
                            ReturnTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(6013),
                            Title = "The first Belarusian lyric-epic work."
                        },
                        new
                        {
                            Id = new Guid("130fa159-70c1-42fb-8f31-5907f04b20e2"),
                            AuthorId = new Guid("944936e4-5167-41fe-8373-0540f319c3d3"),
                            GenreId = new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"),
                            ISBN = "978-9-8588-1435-9",
                            Name = "Heritage",
                            ReceiveTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(6022),
                            ReturnTime = new DateTime(2024, 8, 22, 22, 8, 5, 149, DateTimeKind.Local).AddTicks(6023),
                            Title = "The book of People's Poet of Belarus Yanka Kupala includes verses and poems that give an idea of the main stages of his creative path, the ideological, thematic and genre richness of his poetry, the peculiarities of his artistic skill."
                        });
                });

            modelBuilder.Entity("Library.Domain.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2330f6c1-3212-44fd-b231-c9ee4ffe196e"),
                            Name = "Novel"
                        },
                        new
                        {
                            Id = new Guid("3898242b-a685-4d49-bd06-8c7e34e14d7c"),
                            Name = "Novel in verse"
                        },
                        new
                        {
                            Id = new Guid("27e0db66-79ef-448d-b20c-af2c1a769e6b"),
                            Name = "Poem"
                        });
                });

            modelBuilder.Entity("Library.Domain.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Library.Domain.Models.Book", b =>
                {
                    b.HasOne("Library.Domain.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Domain.Models.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Author");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Library.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Library.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Library.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Domain.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Library.Domain.Models.Genre", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
