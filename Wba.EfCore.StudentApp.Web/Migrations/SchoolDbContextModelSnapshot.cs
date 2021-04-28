﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wba.EfCore.StudentApp.Web.Data;

namespace Wba.EfCore.StudentApp.Web.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    partial class SchoolDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wba.EfCore.StudentApp.Domain.Entities.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("TeacherId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");

                    b.HasData(
                        new { Id = 1L, TeacherId = 3L, Title = "WBA" },
                        new { Id = 2L, TeacherId = 2L, Title = "WFA" },
                        new { Id = 3L, TeacherId = 1L, Title = "DBS" }
                    );
                });

            modelBuilder.Entity("Wba.EfCore.StudentApp.Domain.Entities.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Firstname");

                    b.Property<string>("Image");

                    b.Property<string>("Lastname");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new { Id = 1L, DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Firstname = "Jimmy", Lastname = "Page" },
                        new { Id = 2L, DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Firstname = "Rory", Lastname = "Gallagher" },
                        new { Id = 3L, DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Firstname = "Pino", Lastname = "Daniele" }
                    );
                });

            modelBuilder.Entity("Wba.EfCore.StudentApp.Domain.Entities.StudentCourses", b =>
                {
                    b.Property<long>("CourseId");

                    b.Property<long>("StudentId");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourses");

                    b.HasData(
                        new { CourseId = 2L, StudentId = 1L },
                        new { CourseId = 1L, StudentId = 1L },
                        new { CourseId = 3L, StudentId = 1L },
                        new { CourseId = 1L, StudentId = 2L },
                        new { CourseId = 2L, StudentId = 2L },
                        new { CourseId = 3L, StudentId = 2L },
                        new { CourseId = 1L, StudentId = 3L },
                        new { CourseId = 2L, StudentId = 3L },
                        new { CourseId = 3L, StudentId = 3L }
                    );
                });

            modelBuilder.Entity("Wba.EfCore.StudentApp.Domain.Entities.Teacher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Lastname");

                    b.HasKey("Id");

                    b.ToTable("Teachers");

                    b.HasData(
                        new { Id = 1L, Firstname = "TopDog", Lastname = "Soete" },
                        new { Id = 2L, Firstname = "Willie", Lastname = "Schokellé" },
                        new { Id = 3L, Firstname = "Sieggie", Lastname = "Derdeyn" }
                    );
                });

            modelBuilder.Entity("Wba.EfCore.StudentApp.Domain.Entities.Course", b =>
                {
                    b.HasOne("Wba.EfCore.StudentApp.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("Wba.EfCore.StudentApp.Domain.Entities.StudentCourses", b =>
                {
                    b.HasOne("Wba.EfCore.StudentApp.Domain.Entities.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wba.EfCore.StudentApp.Domain.Entities.Student", "Student")
                        .WithMany("Courses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
