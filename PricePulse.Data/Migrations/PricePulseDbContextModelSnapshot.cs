﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PricePulse.Data;

#nullable disable

namespace PricePulse.Data.Migrations
{
    [DbContext(typeof(PricePulseDbContext))]
    partial class PricePulseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PricePulse.Core.Entities.ConsumerPriceIndexEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConsumerPriceIndexSeriesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateRetrieved")
                        .HasColumnType("datetime2");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("SeriesId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerPriceIndexSeriesId", "Year", "Month")
                        .IsUnique();

                    b.ToTable("ConsumerPriceIndexEntries");
                });

            modelBuilder.Entity("PricePulse.Core.Entities.ConsumerPriceIndexSeries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Frequency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApparelItem")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnergyItem")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFoodItem")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHousingItem")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMedicalItem")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSeasonallyAdjusted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTransportationItem")
                        .HasColumnType("bit");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SeriesId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SeriesTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitOfMeasure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId")
                        .IsUnique();

                    b.ToTable("ConsumerPriceIndexSeries");
                });

            modelBuilder.Entity("PricePulse.Core.Entities.ConsumerPriceIndexEntry", b =>
                {
                    b.HasOne("PricePulse.Core.Entities.ConsumerPriceIndexSeries", "ConsumerPriceIndexSeries")
                        .WithMany("ConsumerPriceIndexEntries")
                        .HasForeignKey("ConsumerPriceIndexSeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConsumerPriceIndexSeries");
                });

            modelBuilder.Entity("PricePulse.Core.Entities.ConsumerPriceIndexSeries", b =>
                {
                    b.Navigation("ConsumerPriceIndexEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
