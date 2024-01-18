﻿// <auto-generated />
using System;
using FMail.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FMail.Migrations
{
    [DbContext(typeof(SmtpDbContext))]
    [Migration("20240118012324_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("FMail.Data.SmtpAttachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentType")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Contents")
                        .HasColumnType("BLOB");

                    b.Property<Guid?>("SmtpMessageId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SmtpMessageId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("FMail.Data.SmtpMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Body")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RawContent")
                        .HasColumnType("BLOB");

                    b.Property<string>("Subject")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("FMail.Data.SmtpAttachment", b =>
                {
                    b.HasOne("FMail.Data.SmtpMessage", null)
                        .WithMany("Attachments")
                        .HasForeignKey("SmtpMessageId");
                });

            modelBuilder.Entity("FMail.Data.SmtpMessage", b =>
                {
                    b.OwnsMany("FMail.Data.SmtpAddress", "Bcc", b1 =>
                        {
                            b1.Property<Guid>("SmtpMessageId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Address")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .HasColumnType("TEXT");

                            b1.HasKey("SmtpMessageId", "Id");

                            b1.ToTable("Messages");

                            b1.ToJson("Bcc");

                            b1.WithOwner()
                                .HasForeignKey("SmtpMessageId");
                        });

                    b.OwnsMany("FMail.Data.SmtpAddress", "Cc", b1 =>
                        {
                            b1.Property<Guid>("SmtpMessageId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Address")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .HasColumnType("TEXT");

                            b1.HasKey("SmtpMessageId", "Id");

                            b1.ToTable("Messages");

                            b1.ToJson("Cc");

                            b1.WithOwner()
                                .HasForeignKey("SmtpMessageId");
                        });

                    b.OwnsMany("FMail.Data.SmtpAddress", "From", b1 =>
                        {
                            b1.Property<Guid>("SmtpMessageId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAddOrUpdate()
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Address")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .HasColumnType("TEXT");

                            b1.HasKey("SmtpMessageId", "Id");

                            b1.ToTable("Messages");

                            b1.ToJson("From");

                            b1.WithOwner()
                                .HasForeignKey("SmtpMessageId");
                        });

                    b.OwnsMany("FMail.Data.SmtpAddress", "To", b1 =>
                        {
                            b1.Property<Guid>("SmtpMessageId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Address")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .HasColumnType("TEXT");

                            b1.HasKey("SmtpMessageId", "Id");

                            b1.ToTable("Messages");

                            b1.ToJson("To");

                            b1.WithOwner()
                                .HasForeignKey("SmtpMessageId");
                        });

                    b.Navigation("Bcc");

                    b.Navigation("Cc");

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("FMail.Data.SmtpMessage", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}