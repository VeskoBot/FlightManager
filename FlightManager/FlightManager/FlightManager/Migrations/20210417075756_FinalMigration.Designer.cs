﻿// <auto-generated />
using System;
using FlightManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlightManager.Migrations
{
    [DbContext(typeof(FlightsManagerDBContext))]
    [Migration("20210417075756_FinalMigration")]
    partial class FinalMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlightManager.Models.Flights", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AirplaneCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("AirplaneType")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ArrivalLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime");

                    b.Property<string>("DepartureLoaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime");

                    b.Property<int>("VacantSpots")
                        .HasColumnType("int");

                    b.Property<int>("VacantSpotsBussiness")
                        .HasColumnType("int");

                    b.HasKey("FlightId");

                    b.ToTable("FlightsSet");
                });

            modelBuilder.Entity("FlightManager.Models.Reservations", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EGN")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Surename")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TicketType")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("FlightId");

                    b.ToTable("ReservationsSet");
                });

            modelBuilder.Entity("FlightManager.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EGN")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserId");

                    b.ToTable("UsersSet");
                });

            modelBuilder.Entity("FlightManager.Models.Reservations", b =>
                {
                    b.HasOne("FlightManager.Models.Flights", "Flight")
                        .WithMany("ReservationsList")
                        .HasForeignKey("FlightId")
                        .HasConstraintName("ForeignKey_Flights_Reservations")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("FlightManager.Models.Flights", b =>
                {
                    b.Navigation("ReservationsList");
                });
#pragma warning restore 612, 618
        }
    }
}
