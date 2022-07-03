using DebitoCredito.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DebitoCredito.Infraestructure.Data
{
    /// <summary>
    /// Clase con abstraccion para acceso a base de datos
    /// </summary>
    public partial class DebitoCreditoDBContext : DbContext
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="options">opciones de configuracion repositorio</param>
        public DebitoCreditoDBContext(DbContextOptions<DebitoCreditoDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Movimientos> Movimientos { get; set; }
        /// <summary>
        /// Permite realizar el mapeo de las entidades con la base de datos
        /// </summary>
        /// <param name="modelBuilder">modelo de configuracion a mapear</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity("DebitoCredito.Domain.Entidades.Cliente", b =>
            {
                b.Property<int>("ClienteId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Contrasena")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("Estado")
                    .HasColumnType("bit");

                b.Property<int>("PersonaId")
                    .HasColumnType("int");

                b.HasKey("ClienteId");

                b.HasIndex("PersonaId");

                b.ToTable("Cliente");
            });


            modelBuilder.Entity("DebitoCredito.Domain.Entidades.Cuenta", b =>
            {
                b.Property<int>("CuentaId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ClienteId")
                    .HasColumnType("int");

                b.Property<bool>("Estado")
                    .HasColumnType("bit");

                b.Property<int>("NumeroCuenta")
                    .HasColumnType("int");

                b.Property<decimal>("SaldoInicial")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("Tipo")
                    .HasColumnType("varchar(256)");

                b.Property<decimal>("MontoDiario")
                  .HasColumnType("decimal(18,2)");

                b.HasKey("CuentaId");

                b.HasIndex("ClienteId");

                b.ToTable("Cuenta");
            });

            modelBuilder.Entity("DebitoCredito.Domain.Entidades.Movimientos", b =>
            {
                b.Property<int>("MovimientoId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("CuentaId")
                    .HasColumnType("int");

                b.Property<DateTime>("FechaMovimiento")
                    .HasColumnType("DateTime");

                b.Property<decimal>("Saldo")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("TipoMovimiento")
                    .HasColumnType("varchar(50)");

                b.Property<decimal>("Valor")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("MovimientoId");

                b.HasIndex("CuentaId");

                b.ToTable("Movimientos");
            });

            modelBuilder.Entity("DebitoCredito.Domain.Entidades.Persona", b =>
            {
                b.Property<int>("PersonaId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Direccion")
                    .HasColumnType("varchar(256)");

                b.Property<string>("Edad")
                    .HasColumnType("varchar(10)");

                b.Property<string>("Genero")
                    .HasColumnType("varchar(15)");

                b.Property<string>("Identificacion")
                    .HasColumnType("varchar(15)");

                b.Property<string>("Nombre")
                    .HasColumnType("varchar(256)");

                b.Property<string>("Telefono")
                    .HasColumnType("varchar(256)");

                b.HasKey("PersonaId");

                b.ToTable("Persona");
            });

            modelBuilder.Entity("DebitoCredito.Domain.Entidades.Cliente", b =>
            {
                b.HasOne("DebitoCredito.Domain.Entidades.Persona", "Persona")
                    .WithMany()
                    .HasForeignKey("PersonaId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Persona");
            });

            modelBuilder.Entity("DebitoCredito.Domain.Entidades.Cuenta", b =>
            {
                b.HasOne("DebitoCredito.Domain.Entidades.Cliente", "Cliente")
                    .WithMany()
                    .HasForeignKey("ClienteId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Cliente");
            });

            modelBuilder.Entity("DebitoCredito.Domain.Entidades.Movimientos", b =>
            {
                b.HasOne("DebitoCredito.Domain.Entidades.Cuenta", "Cuenta")
                    .WithMany()
                    .HasForeignKey("CuentaId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Cuenta");
            });
        }
    }
}
