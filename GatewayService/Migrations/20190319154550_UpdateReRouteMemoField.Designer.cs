﻿// <auto-generated />
using System;
using GatewayService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GatewayService.Migrations
{
    [DbContext(typeof(ConnectDbContext))]
    [Migration("20190319154550_UpdateReRouteMemoField")]
    partial class UpdateReRouteMemoField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GatewayService.Repository.ReRoute", b =>
                {
                    b.Property<int>("ReRouteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthenticationOptions")
                        .HasMaxLength(300);

                    b.Property<string>("CacheOptions")
                        .HasMaxLength(200);

                    b.Property<string>("DelegatingHandlers")
                        .HasMaxLength(200);

                    b.Property<string>("DownstreamHostAndPorts")
                        .HasMaxLength(500);

                    b.Property<string>("DownstreamPathTemplate")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("DownstreamScheme")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("InfoStatus");

                    b.Property<int?>("ItemId");

                    b.Property<string>("LoadBalancerOptions")
                        .HasMaxLength(500);

                    b.Property<string>("Memo");

                    b.Property<int?>("Priority");

                    b.Property<string>("QoSOptions")
                        .HasMaxLength(200);

                    b.Property<string>("RequestIdKey")
                        .HasMaxLength(100);

                    b.Property<string>("ServiceName")
                        .HasMaxLength(100);

                    b.Property<string>("UpstreamHost")
                        .HasMaxLength(100);

                    b.Property<string>("UpstreamHttpMethod")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UpstreamPathTemplate")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("ReRouteId");

                    b.ToTable("ReRoutes");
                });
#pragma warning restore 612, 618
        }
    }
}
