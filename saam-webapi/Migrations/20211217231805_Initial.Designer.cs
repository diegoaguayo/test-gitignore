// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using saam_webapi;

namespace saam_webapi.Migrations
{
    [DbContext(typeof(SAAMDbContext))]
    [Migration("20211217231805_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("saam_webapi.Entities.Cartola", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EspecialidadId")
                        .HasColumnType("int");

                    b.Property<int>("ListaId")
                        .HasColumnType("int");

                    b.Property<int>("Posicion")
                        .HasColumnType("int");

                    b.Property<int>("Puerta")
                        .HasColumnType("int");

                    b.Property<string>("Rut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadId");

                    b.HasIndex("ListaId");

                    b.ToTable("Cartolas");
                });

            modelBuilder.Entity("saam_webapi.Entities.Especialidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Origen")
                        .HasColumnType("int");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Especialidades");
                });

            modelBuilder.Entity("saam_webapi.Entities.Faena", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Origen")
                        .HasColumnType("int");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faenas");
                });

            modelBuilder.Entity("saam_webapi.Entities.HistorialRefresco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Creado")
                        .HasColumnType("int");

                    b.Property<int>("Editado")
                        .HasColumnType("int");

                    b.Property<int>("Eliminado")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Maestro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HistorialesR");
                });

            modelBuilder.Entity("saam_webapi.Entities.Inasistencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Dias")
                        .HasColumnType("int");

                    b.Property<int>("EspecialidadId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Termino")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadId");

                    b.ToTable("Inasistencias");
                });

            modelBuilder.Entity("saam_webapi.Entities.Lista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Nlista")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Listas");
                });

            modelBuilder.Entity("saam_webapi.Entities.Lugar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Origen")
                        .HasColumnType("int");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lugares");
                });

            modelBuilder.Entity("saam_webapi.Entities.Maximoturno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EspecialidadId")
                        .HasColumnType("int");

                    b.Property<string>("Saldo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipocontratoId")
                        .HasColumnType("int");

                    b.Property<int>("Valor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadId");

                    b.HasIndex("TipocontratoId");

                    b.ToTable("Maximoturnos");
                });

            modelBuilder.Entity("saam_webapi.Entities.Tipocontrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tipocontratos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "B",
                            Terminal = "Nominacion_ITI"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "R",
                            Terminal = "Nominacion_ITI"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "E",
                            Terminal = "Nominacion_ITI"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Q",
                            Terminal = "Nominacion_ITI"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "B",
                            Terminal = "Nominacion_ATI"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "R",
                            Terminal = "Nominacion_ATI"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "E",
                            Terminal = "Nominacion_ATI"
                        },
                        new
                        {
                            Id = 8,
                            Nombre = "Q",
                            Terminal = "Nominacion_ATI"
                        },
                        new
                        {
                            Id = 9,
                            Nombre = "M",
                            Terminal = "Nominacion_ATI"
                        },
                        new
                        {
                            Id = 10,
                            Nombre = "B",
                            Terminal = "Nominacion_STI"
                        },
                        new
                        {
                            Id = 11,
                            Nombre = "R",
                            Terminal = "Nominacion_STI"
                        },
                        new
                        {
                            Id = 12,
                            Nombre = "E",
                            Terminal = "Nominacion_STI"
                        },
                        new
                        {
                            Id = 13,
                            Nombre = "Q",
                            Terminal = "Nominacion_STI"
                        },
                        new
                        {
                            Id = 14,
                            Nombre = "B",
                            Terminal = "Nominacion_SVTI"
                        },
                        new
                        {
                            Id = 15,
                            Nombre = "R",
                            Terminal = "Nominacion_SVTI"
                        },
                        new
                        {
                            Id = 16,
                            Nombre = "E",
                            Terminal = "Nominacion_SVTI"
                        },
                        new
                        {
                            Id = 17,
                            Nombre = "Q",
                            Terminal = "Nominacion_SVTI"
                        });
                });

            modelBuilder.Entity("saam_webapi.Entities.Trabajador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EspecialidadId")
                        .HasColumnType("int");

                    b.Property<string>("Nombres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Papellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sapellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Terminal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipocontratoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadId");

                    b.HasIndex("TipocontratoId");

                    b.ToTable("Trabajadores");
                });

            modelBuilder.Entity("saam_webapi.Entities.Cartola", b =>
                {
                    b.HasOne("saam_webapi.Entities.Especialidad", "Especialidad")
                        .WithMany()
                        .HasForeignKey("EspecialidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("saam_webapi.Entities.Lista", "Lista")
                        .WithMany()
                        .HasForeignKey("ListaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidad");

                    b.Navigation("Lista");
                });

            modelBuilder.Entity("saam_webapi.Entities.Inasistencia", b =>
                {
                    b.HasOne("saam_webapi.Entities.Especialidad", "Especialidad")
                        .WithMany()
                        .HasForeignKey("EspecialidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidad");
                });

            modelBuilder.Entity("saam_webapi.Entities.Maximoturno", b =>
                {
                    b.HasOne("saam_webapi.Entities.Especialidad", "Especialidad")
                        .WithMany()
                        .HasForeignKey("EspecialidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("saam_webapi.Entities.Tipocontrato", "Tipocontrato")
                        .WithMany()
                        .HasForeignKey("TipocontratoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidad");

                    b.Navigation("Tipocontrato");
                });

            modelBuilder.Entity("saam_webapi.Entities.Trabajador", b =>
                {
                    b.HasOne("saam_webapi.Entities.Especialidad", "Especialidad")
                        .WithMany()
                        .HasForeignKey("EspecialidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("saam_webapi.Entities.Tipocontrato", "Tipocontrato")
                        .WithMany()
                        .HasForeignKey("TipocontratoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidad");

                    b.Navigation("Tipocontrato");
                });
#pragma warning restore 612, 618
        }
    }
}
