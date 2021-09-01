﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(KastelPlannerDbContext))]
    [Migration("20210830132914_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.BeneficiaryWeeklyLogs.BeneficiaryWeeklyLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("BeneficiaryId")
                        .HasColumnType("uuid")
                        .HasColumnName("beneficiary_id");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("day_of_week");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("interval")
                        .HasColumnName("start_time");

                    b.HasKey("Id");

                    b.HasIndex("BeneficiaryId");

                    b.ToTable("weekly_log");
                });

            modelBuilder.Entity("Domain.Labels.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.HasKey("Id");

                    b.ToTable("label");
                });

            modelBuilder.Entity("Domain.Persons.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.HasKey("Id");

                    b.ToTable("person");
                });

            modelBuilder.Entity("Domain.PersonsLabels.PersonLabel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("LabelId")
                        .HasColumnType("uuid")
                        .HasColumnName("label_id");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("LabelId");

                    b.HasIndex("PersonId");

                    b.ToTable("person_label");
                });

            modelBuilder.Entity("Domain.PersonsRoles.PersonRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("RoleId");

                    b.ToTable("person_role");
                });

            modelBuilder.Entity("Domain.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.HasKey("Id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("Domain.Schedules.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<Guid>("VolunteerId")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.Property<Guid>("WeeklyLogId")
                        .HasColumnType("uuid")
                        .HasColumnName("weekly_log_id");

                    b.HasKey("Id");

                    b.HasIndex("VolunteerId");

                    b.HasIndex("WeeklyLogId");

                    b.ToTable("schedule");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid")
                        .HasColumnName("person_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("Domain.BeneficiaryWeeklyLogs.BeneficiaryWeeklyLog", b =>
                {
                    b.HasOne("Domain.Persons.Person", "Person")
                        .WithMany("BeneficiaryWeeklyLogs")
                        .HasForeignKey("BeneficiaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Domain.Labels.Label", b =>
                {
                    b.OwnsOne("Domain.Labels.ValueObjects.LabelName", "LabelName", b1 =>
                        {
                            b1.Property<Guid>("LabelId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.HasKey("LabelId");

                            b1.ToTable("label");

                            b1.WithOwner()
                                .HasForeignKey("LabelId");
                        });

                    b.Navigation("LabelName");
                });

            modelBuilder.Entity("Domain.Persons.Person", b =>
                {
                    b.OwnsOne("Domain.Persons.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .HasColumnType("text")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .HasColumnType("text")
                                .HasColumnName("last_name");

                            b1.HasKey("PersonId");

                            b1.ToTable("person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("Domain.Persons.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Number")
                                .HasColumnType("text")
                                .HasColumnName("phone_number");

                            b1.HasKey("PersonId");

                            b1.ToTable("person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Name");

                    b.Navigation("PhoneNumber");
                });

            modelBuilder.Entity("Domain.PersonsLabels.PersonLabel", b =>
                {
                    b.HasOne("Domain.Labels.Label", "Label")
                        .WithMany("PersonLabels")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Persons.Person", "Person")
                        .WithMany("PersonLabels")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Label");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Domain.PersonsRoles.PersonRole", b =>
                {
                    b.HasOne("Domain.Persons.Person", "Person")
                        .WithMany("PersonRoles")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Roles.Role", "Role")
                        .WithMany("PersonRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Roles.Role", b =>
                {
                    b.OwnsOne("Domain.Roles.ValueObjects.RoleName", "RoleName", b1 =>
                        {
                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("role");

                            b1.HasKey("RoleId");

                            b1.ToTable("role");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.Navigation("RoleName");
                });

            modelBuilder.Entity("Domain.Schedules.Schedule", b =>
                {
                    b.HasOne("Domain.Persons.Person", "Volunteer")
                        .WithMany("Schedules")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.BeneficiaryWeeklyLogs.BeneficiaryWeeklyLog", "BeneficiaryWeeklyLog")
                        .WithMany("Schedules")
                        .HasForeignKey("WeeklyLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Schedules.ValueObjects.Duration", "Duration", b1 =>
                        {
                            b1.Property<Guid>("ScheduleId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Minutes")
                                .HasColumnType("integer")
                                .HasColumnName("duration");

                            b1.HasKey("ScheduleId");

                            b1.ToTable("schedule");

                            b1.WithOwner()
                                .HasForeignKey("ScheduleId");
                        });

                    b.Navigation("BeneficiaryWeeklyLog");

                    b.Navigation("Duration");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.HasOne("Domain.Persons.Person", "Person")
                        .WithOne("User")
                        .HasForeignKey("Domain.Users.User", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Users.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("email");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Domain.BeneficiaryWeeklyLogs.BeneficiaryWeeklyLog", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("Domain.Labels.Label", b =>
                {
                    b.Navigation("PersonLabels");
                });

            modelBuilder.Entity("Domain.Persons.Person", b =>
                {
                    b.Navigation("BeneficiaryWeeklyLogs");

                    b.Navigation("PersonLabels");

                    b.Navigation("PersonRoles");

                    b.Navigation("Schedules");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Roles.Role", b =>
                {
                    b.Navigation("PersonRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
