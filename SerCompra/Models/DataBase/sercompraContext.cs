using Microsoft.EntityFrameworkCore;

namespace SerCompra.Models.DataBase
{
    public partial class SercompraContext : DbContext
    {
        public SercompraContext()
        {
        }

        public SercompraContext(DbContextOptions<SercompraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administradors { get; set; }
        public virtual DbSet<Bienservicio> Bienservicios { get; set; }
        public virtual DbSet<BienservicoCompra> BienservicoCompras { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<Cotizacion> Cotizacions { get; set; }
        public virtual DbSet<CotizacionBienservicio> CotizacionBienservicios { get; set; }
        public virtual DbSet<Documentacion> Documentacions { get; set; }
        public virtual DbSet<FuncionarioAreaCompra> FuncionarioAreaCompras { get; set; }
        public virtual DbSet<Proveedor> Proveedors { get; set; }
        public virtual DbSet<Solicitudproveedor> Solicitudproveedors { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => new { e.IdAdministrador, e.UsuarioIdUsuario })
                    .HasName("PRIMARY");

                entity.ToTable("administrador");

                entity.HasIndex(e => e.UsuarioIdUsuario, "fk_Administrador_Usuario1_idx");

                entity.HasIndex(e => e.IdAdministrador, "idAdministrador_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdAdministrador)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idAdministrador");

                entity.Property(e => e.UsuarioIdUsuario).HasColumnName("Usuario_idUsuario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Notificaciones)
                    .IsRequired()
                    .HasColumnType("mediumtext");

                entity.HasOne(d => d.UsuarioIdUsuarioNavigation)
                    .WithMany(p => p.Administradors)
                    .HasForeignKey(d => d.UsuarioIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Administrador_Usuario1");
            });

            modelBuilder.Entity<Bienservicio>(entity =>
            {
                entity.HasKey(e => new { e.IdBienServicio, e.ProveedorIdProveedor })
                    .HasName("PRIMARY");

                entity.ToTable("bienservicio");

                entity.HasIndex(e => e.ProveedorIdProveedor, "fk_BienServicio_Proveedor1_idx");

                entity.HasIndex(e => e.IdBienServicio, "idBienServicio_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdBienServicio)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idBienServicio");

                entity.Property(e => e.ProveedorIdProveedor).HasColumnName("Proveedor_idProveedor");

                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("mediumtext");

                entity.HasOne(d => d.ProveedorIdProveedorNavigation)
                    .WithMany(p => p.Bienservicios)
                    .HasPrincipalKey(p => p.IdProveedor)
                    .HasForeignKey(d => d.ProveedorIdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_BienServicio_Proveedor1");
            });

            modelBuilder.Entity<BienservicoCompra>(entity =>
            {
                entity.HasKey(e => new
                    {
                        e.IdBienServicoCompra, e.CompraIdCompra, e.CompraFuncionarioAreaComprasIdFuncionarioAreaCompras,
                        e.BienServicioIdBienServicio, e.BienServicioProveedorIdProveedor
                    })
                    .HasName("PRIMARY");

                entity.ToTable("bienservico-compra");

                entity.HasIndex(e => new { e.BienServicioIdBienServicio, e.BienServicioProveedorIdProveedor },
                    "fk_BienServico-Compra_BienServicio1_idx");

                entity.HasIndex(e => new { e.CompraIdCompra, e.CompraFuncionarioAreaComprasIdFuncionarioAreaCompras },
                    "fk_BienServico-Compra_Compra1_idx");

                entity.HasIndex(e => e.IdBienServicoCompra, "idBienServico-Compra_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdBienServicoCompra)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idBienServico-Compra");

                entity.Property(e => e.CompraIdCompra).HasColumnName("Compra_idCompra");

                entity.Property(e => e.CompraFuncionarioAreaComprasIdFuncionarioAreaCompras)
                    .HasColumnName("Compra_Funcionario_Area_Compras_idFuncionario_Area_Compras");

                entity.Property(e => e.BienServicioIdBienServicio).HasColumnName("BienServicio_idBienServicio");

                entity.Property(e => e.BienServicioProveedorIdProveedor)
                    .HasColumnName("BienServicio_Proveedor_idProveedor");

                entity.HasOne(d => d.BienServicio)
                    .WithMany(p => p.BienservicoCompras)
                    .HasForeignKey(d => new { d.BienServicioIdBienServicio, d.BienServicioProveedorIdProveedor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_BienServico-Compra_BienServicio1");

                entity.HasOne(d => d.Compra)
                    .WithMany(p => p.BienservicoCompras)
                    .HasForeignKey(d => new
                        { d.CompraIdCompra, d.CompraFuncionarioAreaComprasIdFuncionarioAreaCompras })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_BienServico-Compra_Compra1");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => new { e.IdCompra, e.FuncionarioAreaComprasIdFuncionarioAreaCompras })
                    .HasName("PRIMARY");

                entity.ToTable("compra");

                entity.HasIndex(e => e.FuncionarioAreaComprasIdFuncionarioAreaCompras,
                    "fk_Compra_Funcionario_Area_Compras1_idx");

                entity.HasIndex(e => e.IdCompra, "idCompra_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdCompra)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idCompra");

                entity.Property(e => e.FuncionarioAreaComprasIdFuncionarioAreaCompras)
                    .HasColumnName("Funcionario_Area_Compras_idFuncionario_Area_Compras");

                entity.HasOne(d => d.FuncionarioAreaComprasIdFuncionarioAreaComprasNavigation)
                    .WithMany(p => p.Compras)
                    .HasPrincipalKey(p => p.IdFuncionarioAreaCompras)
                    .HasForeignKey(d => d.FuncionarioAreaComprasIdFuncionarioAreaCompras)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Compra_Funcionario_Area_Compras1");
            });

            modelBuilder.Entity<Cotizacion>(entity =>
            {
                entity.HasKey(e => e.IdCotizacion)
                    .HasName("PRIMARY");

                entity.ToTable("cotizacion");

                entity.HasIndex(e => e.IdCotizacion, "idCotizacion_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdCotizacion).HasColumnName("idCotizacion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Fecha).HasColumnType("date");
            });

            modelBuilder.Entity<CotizacionBienservicio>(entity =>
            {
                entity.HasKey(e => new
                    {
                        e.IdCotizacionBienServicio, e.BienServicioIdBienServicio, e.BienServicioProveedorIdProveedor,
                        e.CotizacionIdCotizacion
                    })
                    .HasName("PRIMARY");

                entity.ToTable("cotizacion-bienservicio");

                entity.HasIndex(e => new { e.BienServicioIdBienServicio, e.BienServicioProveedorIdProveedor },
                    "fk_Cotizacion-BienServicio_BienServicio1_idx");

                entity.HasIndex(e => e.CotizacionIdCotizacion, "fk_Cotizacion-BienServicio_Cotizacion1_idx");

                entity.HasIndex(e => e.IdCotizacionBienServicio, "idCotizacion-BienServicio_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdCotizacionBienServicio)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idCotizacion-BienServicio");

                entity.Property(e => e.BienServicioIdBienServicio).HasColumnName("BienServicio_idBienServicio");

                entity.Property(e => e.BienServicioProveedorIdProveedor)
                    .HasColumnName("BienServicio_Proveedor_idProveedor");

                entity.Property(e => e.CotizacionIdCotizacion).HasColumnName("Cotizacion_idCotizacion");

                entity.HasOne(d => d.CotizacionIdCotizacionNavigation)
                    .WithMany(p => p.CotizacionBienservicios)
                    .HasForeignKey(d => d.CotizacionIdCotizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Cotizacion-BienServicio_Cotizacion1");

                entity.HasOne(d => d.BienServicio)
                    .WithMany(p => p.CotizacionBienservicios)
                    .HasForeignKey(d => new { d.BienServicioIdBienServicio, d.BienServicioProveedorIdProveedor })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Cotizacion-BienServicio_BienServicio1");
            });

            modelBuilder.Entity<Documentacion>(entity =>
            {
                entity.HasKey(e => e.IdDocumentacion)
                    .HasName("PRIMARY");

                entity.ToTable("documentacion");

                entity.HasIndex(e => e.IdDocumentacion, "idDocumentacion_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdDocumentacion).HasColumnName("idDocumentacion");

                entity.Property(e => e.CamaraComercio)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasComment("ertificado de cámara y comercio ");

                entity.Property(e => e.EstadosFinancieros)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasComment("Estados Financieros del último año cuando (cuando aplique)");

                entity.Property(e => e.FotocopiaCedula)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasComment(
                        "Fotocopia  de  la  Cédula  de  Ciudadanía  o  Extranjería  del  Representante  Legal  o Contratista ");

                entity.Property(e => e.LicenciasPermisos)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasComment(
                        "Llicencias,Decretos,    Resoluciones,    Acuerdos, acreditaciones                                                                                                               o permisos deautoridades de control (según aplique).");

                entity.Property(e => e.OtrosDocumentos)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasComment("OTROS  DOCUMENTOS  REQUERIDOS  SEGÚN  ACTIVIDAD  ECONÓMICA    DE  LA EMPRESA");

                entity.Property(e => e.Rut)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("RUT");
            });

            modelBuilder.Entity<FuncionarioAreaCompra>(entity =>
            {
                entity.HasKey(e => new { e.IdFuncionarioAreaCompras, e.UsuarioIdUsuario })
                    .HasName("PRIMARY");

                entity.ToTable("funcionario_area_compras");

                entity.HasIndex(e => e.UsuarioIdUsuario, "fk_Funcionario_Area_Compras_Usuario1_idx");

                entity.HasIndex(e => e.IdFuncionarioAreaCompras, "idFuncionario_Area_Compras_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdFuncionarioAreaCompras)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idFuncionario_Area_Compras");

                entity.Property(e => e.UsuarioIdUsuario).HasColumnName("Usuario_idUsuario");

                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.NombreTrabajador)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Nombre_Trabajador");

                entity.Property(e => e.Notificaciones)
                    .IsRequired()
                    .HasColumnType("mediumtext");

                entity.HasOne(d => d.UsuarioIdUsuarioNavigation)
                    .WithMany(p => p.FuncionarioAreaCompras)
                    .HasForeignKey(d => d.UsuarioIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Funcionario_Area_Compras_Usuario1");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => new { e.IdProveedor, e.DocumentacionIdDocumentacion, e.UsuarioIdUsuario })
                    .HasName("PRIMARY");

                entity.ToTable("proveedor");

                entity.HasIndex(e => e.DocumentacionIdDocumentacion, "fk_Proveedor_Documentacion1_idx");

                entity.HasIndex(e => e.UsuarioIdUsuario, "fk_Proveedor_Usuario1_idx");

                entity.HasIndex(e => e.IdProveedor, "idProveedor_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdProveedor)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idProveedor")
                    .HasComment("Numero para identificar al proveedor");

                entity.Property(e => e.DocumentacionIdDocumentacion).HasColumnName("Documentacion_idDocumentacion");

                entity.Property(e => e.UsuarioIdUsuario).HasColumnName("Usuario_idUsuario");

                entity.Property(e => e.CiudadMunicipio)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("Ciudad o municipio del proveedor");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasComment("Direccion donde encontrar al proveedor");

                entity.Property(e => e.NombreProveedor)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasComment("NOMBRE DE LA FIRMA CONTRATISTA O PROVEEDOR");

                entity.Property(e => e.NombreRepresentante)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("NOMBRE DEL REPRESENTANTE LEGAL CONTRATISTA O PROVEEDOR");

                entity.Property(e => e.Notificaciones)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasComment("Notificaciones que le llegan al proveedor");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasComment("Telefono de contacto del proveedor");

                entity.HasOne(d => d.DocumentacionIdDocumentacionNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.DocumentacionIdDocumentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Proveedor_Documentacion1");

                entity.HasOne(d => d.UsuarioIdUsuarioNavigation)
                    .WithMany(p => p.Proveedors)
                    .HasForeignKey(d => d.UsuarioIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Proveedor_Usuario1");
            });

            modelBuilder.Entity<Solicitudproveedor>(entity =>
            {
                entity.HasKey(e => new
                    {
                        e.IdSolicitudProveedor, e.FuncionarioAreaComprasIdFuncionarioAreaCompras,
                        e.ProveedorIdProveedor, e.ProveedorDocumentacionIdDocumentacion
                    })
                    .HasName("PRIMARY");

                entity.ToTable("solicitudproveedor");

                entity.HasIndex(e => e.FuncionarioAreaComprasIdFuncionarioAreaCompras,
                    "fk_SolicitudProveedor_Funcionario_Area_Compras1_idx");

                entity.HasIndex(e => new { e.ProveedorIdProveedor, e.ProveedorDocumentacionIdDocumentacion },
                    "fk_SolicitudProveedor_Proveedor1_idx");

                entity.HasIndex(e => e.IdSolicitudProveedor, "idSolicitudProveedor_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdSolicitudProveedor)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idSolicitudProveedor")
                    .HasComment("Id de la solicitud");

                entity.Property(e => e.FuncionarioAreaComprasIdFuncionarioAreaCompras)
                    .HasColumnName("Funcionario_Area_Compras_idFuncionario_Area_Compras");

                entity.Property(e => e.ProveedorIdProveedor).HasColumnName("Proveedor_idProveedor");

                entity.Property(e => e.ProveedorDocumentacionIdDocumentacion)
                    .HasColumnName("Proveedor_Documentacion_idDocumentacion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasComment("Solicitud aceptada, rechazada, en espera");

                entity.HasOne(d => d.FuncionarioAreaComprasIdFuncionarioAreaComprasNavigation)
                    .WithMany(p => p.Solicitudproveedors)
                    .HasPrincipalKey(p => p.IdFuncionarioAreaCompras)
                    .HasForeignKey(d => d.FuncionarioAreaComprasIdFuncionarioAreaCompras)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_SolicitudProveedor_Funcionario_Area_Compras1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.IdUsuario, "idUsuario_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.RecoveryToken)
                    .HasMaxLength(200)
                    .HasColumnName("recoveryToken");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}