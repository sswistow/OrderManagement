﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderManagement.Infrastucture.Contexts;

namespace OrderManagement.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20190225121222_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderManagement.Domain.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnName("creation_date");

                    b.Property<int>("State")
                        .HasColumnName("state");

                    b.Property<int?>("ThreadId");

                    b.HasKey("Id");

                    b.HasIndex("ThreadId");

                    b.ToTable("order");
                });

            modelBuilder.Entity("OrderManagement.Domain.Models.OrderThread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Interval")
                        .HasColumnName("interval");

                    b.Property<int>("State")
                        .HasColumnName("state");

                    b.HasKey("Id");

                    b.ToTable("thread");
                });

            modelBuilder.Entity("OrderManagement.Domain.Models.Order", b =>
                {
                    b.HasOne("OrderManagement.Domain.Models.OrderThread", "Thread")
                        .WithMany()
                        .HasForeignKey("ThreadId");
                });
#pragma warning restore 612, 618
        }
    }
}
