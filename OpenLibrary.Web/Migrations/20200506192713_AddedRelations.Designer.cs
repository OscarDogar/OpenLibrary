﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenLibrary.Web.Data;

namespace OpenLibrary.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200506192713_AddedRelations")]
    partial class AddedRelations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OpenLibrary.Web.Data.Entities.AuthorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("OpenLibrary.Web.Data.Entities.DocumentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("DocumentLanguageId");

                    b.Property<string>("DocumentPath");

                    b.Property<int?>("GenderId");

                    b.Property<int>("PagesNumber")
                        .HasMaxLength(80);

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int?>("TypeOfDocumentId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DocumentLanguageId");

                    b.HasIndex("GenderId");

                    b.HasIndex("TypeOfDocumentId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("OpenLibrary.Web.Data.Entities.DocumentLanguageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("DocumentLanguages");
                });

            modelBuilder.Entity("OpenLibrary.Web.Data.Entities.GenderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("OpenLibrary.Web.Data.Entities.TypeOfDocumentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TypeOfDocuments");
                });

            modelBuilder.Entity("OpenLibrary.Web.Data.Entities.DocumentEntity", b =>
                {
                    b.HasOne("OpenLibrary.Web.Data.Entities.AuthorEntity", "Author")
                        .WithMany("Documents")
                        .HasForeignKey("AuthorId");

                    b.HasOne("OpenLibrary.Web.Data.Entities.DocumentLanguageEntity", "DocumentLanguage")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentLanguageId");

                    b.HasOne("OpenLibrary.Web.Data.Entities.GenderEntity", "Gender")
                        .WithMany("Documents")
                        .HasForeignKey("GenderId");

                    b.HasOne("OpenLibrary.Web.Data.Entities.TypeOfDocumentEntity", "TypeOfDocument")
                        .WithMany("Documents")
                        .HasForeignKey("TypeOfDocumentId");
                });
#pragma warning restore 612, 618
        }
    }
}
