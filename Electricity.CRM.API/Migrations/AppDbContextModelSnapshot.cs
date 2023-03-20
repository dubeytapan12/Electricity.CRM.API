﻿// <auto-generated />
using System;
using Electricity.CRM.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Electricity.CRM.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Electricity.CRM.API.Entity.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TechnologyEnablementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TechnologyEnablementId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.EducationOrCertification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TechnologyEnablementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TechnologyEnablementId");

                    b.ToTable("EducationOrCertification");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.ElectricityBiller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CommercialUserId")
                        .HasColumnType("int");

                    b.Property<string>("ConnectionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FactoryUserId")
                        .HasColumnType("int");

                    b.Property<int?>("FlatUserId")
                        .HasColumnType("int");

                    b.Property<int?>("ResidentialUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommercialUserId");

                    b.HasIndex("FactoryUserId");

                    b.HasIndex("FlatUserId");

                    b.HasIndex("ResidentialUserId");

                    b.ToTable("ElectricityBiller");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.ElectricityUserCommercial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AadharNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ConnectionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FatherName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsUsingNewMeeter")
                        .HasColumnType("bit");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ElectricityUserCommercial");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.ElectricityUserFactory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AadharNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ConnectionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FatherName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsUsingNewMeeter")
                        .HasColumnType("bit");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ElectricityUserFactory");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.ElectricityUserFlat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AadharNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ConnectionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FatherName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsUsingNewMeeter")
                        .HasColumnType("bit");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ElectricityUserFlat");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.ElectricityUserResidential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AadharNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ConnectionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FatherName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsUsingNewMeeter")
                        .HasColumnType("bit");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ElectricityUserResidential");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Experience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExperienceDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TechnologyEnablementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TechnologyEnablementId");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.ForgotPassword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("expiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("ForgotPasswords");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Languages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TechnologyEnablementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TechnologyEnablementId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.Property<string>("StartDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ResumeId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Resume", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Resume");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Skills", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TechnologyEnablementId")
                        .HasColumnType("int");

                    b.Property<string>("skill")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TechnologyEnablementId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.TechnologyEnablement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Background")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobiles")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TechnologyEnablement");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ForgotPasswordToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.UserRefreshTokens", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("UserRefreshToken");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Client", b =>
                {
                    b.HasOne("Electricity.CRM.API.Entity.TechnologyEnablement", "TechnologyEnablement")
                        .WithMany("Clients")
                        .HasForeignKey("TechnologyEnablementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TechnologyEnablement");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.EducationOrCertification", b =>
                {
                    b.HasOne("Electricity.CRM.API.Entity.TechnologyEnablement", "TechnologyEnablement")
                        .WithMany("EducationOrCertifications")
                        .HasForeignKey("TechnologyEnablementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TechnologyEnablement");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.ElectricityBiller", b =>
                {
                    b.HasOne("Electricity.CRM.API.Entity.ElectricityUserCommercial", "ElectricityUserCommercial")
                        .WithMany()
                        .HasForeignKey("CommercialUserId");

                    b.HasOne("Electricity.CRM.API.Entity.ElectricityUserFactory", "ElectricityUserFactory")
                        .WithMany()
                        .HasForeignKey("FactoryUserId");

                    b.HasOne("Electricity.CRM.API.Entity.ElectricityUserFlat", "ElectricityUserFlat")
                        .WithMany()
                        .HasForeignKey("FlatUserId");

                    b.HasOne("Electricity.CRM.API.Entity.ElectricityUserResidential", "ElectricityUserResidential")
                        .WithMany()
                        .HasForeignKey("ResidentialUserId");

                    b.Navigation("ElectricityUserCommercial");

                    b.Navigation("ElectricityUserFactory");

                    b.Navigation("ElectricityUserFlat");

                    b.Navigation("ElectricityUserResidential");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Experience", b =>
                {
                    b.HasOne("Electricity.CRM.API.Entity.TechnologyEnablement", "TechnologyEnablement")
                        .WithMany("Experiencies")
                        .HasForeignKey("TechnologyEnablementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TechnologyEnablement");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.ForgotPassword", b =>
                {
                    b.HasOne("Electricity.CRM.API.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Languages", b =>
                {
                    b.HasOne("Electricity.CRM.API.Entity.TechnologyEnablement", "TechnologyEnablement")
                        .WithMany("Languages")
                        .HasForeignKey("TechnologyEnablementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TechnologyEnablement");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Project", b =>
                {
                    b.HasOne("Electricity.CRM.API.Entity.Resume", "Resume")
                        .WithMany("projects")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resume");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Skills", b =>
                {
                    b.HasOne("Electricity.CRM.API.Entity.TechnologyEnablement", "TechnologyEnablement")
                        .WithMany("Skills")
                        .HasForeignKey("TechnologyEnablementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TechnologyEnablement");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.Resume", b =>
                {
                    b.Navigation("projects");
                });

            modelBuilder.Entity("Electricity.CRM.API.Entity.TechnologyEnablement", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("EducationOrCertifications");

                    b.Navigation("Experiencies");

                    b.Navigation("Languages");

                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
