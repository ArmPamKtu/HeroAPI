﻿// <auto-generated />
using System;
using Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Db.Migrations
{
    [DbContext(typeof(HeroDbContext))]
    partial class HeroDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Db.Entities.Feat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("FromGuid");

                    b.Property<string>("Reason");

                    b.Property<Guid>("ToUserGuid");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.ToTable("Feat");
                });

            modelBuilder.Entity("Db.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsInStore");

                    b.Property<bool>("IsOrderable");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Db.Entities.ProductVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cost");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<Guid>("ProductId");

                    b.Property<bool>("SoftDelete");

                    b.Property<string>("UrlImg");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVersion");
                });

            modelBuilder.Entity("Db.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Db.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserGuid")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsStoreManager");

                    b.HasKey("UserGuid");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Db.Entities.ProductVersion", b =>
                {
                    b.HasOne("Db.Entities.Product", "Product")
                        .WithMany("ProductVersion")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
