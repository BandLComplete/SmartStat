// <auto-generated />
using System;
using EntryPoint.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntryPoint.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220111183724_Recreate")]
    partial class Recreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("EntryPoint.Database.PracticeDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("Length")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan?>("Notification")
                        .HasColumnType("TEXT");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Users")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Practices");
                });

            modelBuilder.Entity("EntryPoint.Database.StatDb", b =>
                {
                    b.Property<string>("UserDateName")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit")
                        .HasColumnType("TEXT");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserDateName");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("EntryPoint.Database.UserDb", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
