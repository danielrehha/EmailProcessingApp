// <auto-generated />
using System;
using EmailProcessingApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmailProcessingApp.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220607001418_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EmailProcessingApp.Domain.Models.EmailData", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Attributes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Key");

                    b.ToTable("EmailData");
                });

            modelBuilder.Entity("EmailProcessingApp.Domain.Models.ResponseEmail", b =>
                {
                    b.Property<Guid>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("EmailBody")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<bool>("IsSent")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Key");

                    b.ToTable("SendEmails");
                });
#pragma warning restore 612, 618
        }
    }
}
