﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBanco1
{
    public class MyContext : DbContext
    {
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<CajaDeAhorro> cajas { get; set; }
        public DbSet<PlazoFijo> pfs { get; set; }
        public DbSet<TarjetaDeCredito> tarjetas { get; set; }
        public DbSet<Pago> pagos { get; set; }
        public DbSet<Movimiento> movimientos { get; set; }

        public MyContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Properties.Resources.connectionStr);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //          USUARIO


            //nombre de la tabla
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .HasKey(u => u.id);
            //propiedades de los datos
            modelBuilder.Entity<Usuario>(
                usr =>
                {
                    usr.Property(u => u.dni).HasColumnType("int");
                    usr.Property(u => u.dni).IsRequired(true);
                    usr.Property(u => u.nombre).HasColumnType("varchar(50)");
                    usr.Property(u => u.apellido).HasColumnType("varchar(50)");
                    usr.Property(u => u.mail).HasColumnType("varchar(512)");
                    usr.Property(u => u.password).HasColumnType("varchar(50)");
                    usr.Property(u => u.intentosFallidos).HasColumnType("int");
                    usr.Property(u => u.intentosFallidos).IsRequired(true);
                    usr.Property(u => u.bloqueado).HasColumnType("bit");
                    usr.Property(u => u.esAdmin).HasColumnType("bit");
                });

            //          CAJA DE AHORRO

            modelBuilder.Entity<CajaDeAhorro>()
                .ToTable("Cajas")
                .HasKey(c => c.id);
            modelBuilder.Entity<CajaDeAhorro>(
                cajas =>
                {
                    cajas.Property(c => c.cbu).HasColumnType("int");
                    cajas.Property(c => c.cbu).IsRequired(true);
                    cajas.Property(c => c.saldo).HasColumnType("float");
                    cajas.Property(c => c.saldo).IsRequired(true);
                });

            //          MOVIMIENTO

            modelBuilder.Entity<Movimiento>()
                .ToTable("Movimientos")
                .HasKey(m => m.id);
            modelBuilder.Entity<Movimiento>(
                mov =>
                {
                    mov.Property(m => m.idCaja).HasColumnType("int");
                    mov.Property(m => m.idCaja).IsRequired(true);
                    mov.Property(m => m.detalle).HasColumnType("varchar(100)");
                    mov.Property(m => m.monto).HasColumnType("float");
                    mov.Property(m => m.fecha).HasColumnType("date");
                });


            //          PAGO

            modelBuilder.Entity<Pago>()
                .ToTable("Pagos")
                .HasKey(p => p.id);
            modelBuilder.Entity<Pago>(
                pago =>
                {
                    pago.Property(p => p.idUsuario).HasColumnType("int");
                    pago.Property(p => p.idUsuario).IsRequired(true);
                    pago.Property(p => p.nombre).HasColumnType("varchar(100)");
                    pago.Property(p => p.monto).HasColumnType("float");
                    pago.Property(p => p.pagado).HasColumnType("bit");
                    pago.Property(p => p.metodo).HasColumnType("varchar(50)");
                });

            //          PLAZO FIJO

            modelBuilder.Entity<PlazoFijo>()
                .ToTable("Pfs")
                .HasKey(p => p.id);
            modelBuilder.Entity<PlazoFijo>(
                pf =>
                {
                    pf.Property(p => p.idTitular).HasColumnType("int");
                    pf.Property(p => p.idTitular).IsRequired(true);
                    pf.Property(p => p.monto).HasColumnType("float");
                    pf.Property(p => p.fechaIni).HasColumnType("date");
                    pf.Property(p => p.fechaFin).HasColumnType("date");
                    pf.Property(p => p.tasa).HasColumnType("float");
                    pf.Property(p => p.pagado).HasColumnType("bit");
                    pf.Property(p => p.cbu).HasColumnType("varchar(50)");
                });

            //          TARJETA DE CREDITO

            modelBuilder.Entity<TarjetaDeCredito>()
                .ToTable("Tarjetas")
                .HasKey(t => t.id);
            modelBuilder.Entity<TarjetaDeCredito>(
                tarj =>
                {
                    tarj.Property(t => t.idTitular).HasColumnType("int");
                    tarj.Property(t => t.idTitular).IsRequired(true);
                    tarj.Property(t => t.numero).HasColumnType("int");
                    tarj.Property(t => t.codigoV).HasColumnType("int");
                    tarj.Property(t => t.limite).HasColumnType("float");
                    tarj.Property(t => t.consumos).HasColumnType("float");
                });

                  

            //Ignoro, no agrego Banco a la base de datos
            modelBuilder.Ignore<Banco>();


            modelBuilder.Entity<TarjetaDeCredito>()
                        .HasOne(t => t.titular)
                        .WithMany(u => u.tarjetas)
                        .HasForeignKey(t => t.idTitular);
            modelBuilder.Entity<PlazoFijo>()
                        .HasOne(pfs => pfs.titular)
                        .WithMany(u => u.pfs)
                        .HasForeignKey(pfs => pfs.idTitular);
            modelBuilder.Entity<Pago>()
                        .HasOne(p => p.user)
                        .WithMany(u => u.pagos)
                        .HasForeignKey(p => p.idUsuario);
            modelBuilder.Entity<Movimiento>()
                        .HasOne(m => m.caja)
                        .WithMany(c => c.movimientos)
                        .HasForeignKey(m => m.idCaja);
            modelBuilder.Entity<CajaDeAhorro>()
                        .HasMany(c => c.titulares)
                        .WithMany(u => u.cajas)
                        .UsingEntity<UsuarioCaja>(

                euc => euc.HasOne(uc => uc.user)
                            .WithMany(u => u.userCaja)
                            .HasForeignKey(uc => uc.idUsuario),
                euc => euc.HasOne(uc => uc.caja)
                            .WithMany(c => c.userCaja)
                            .HasForeignKey(uc => uc.idCaja),
                euc => euc.HasKey(k => new { k.idCaja, k.idUsuario })
                );



            // Carga de datos iniciales

            modelBuilder.Entity<Usuario>().HasData(
                new { id = 1, dni = 123, nombre = "admin", apellido = "admin", mail = "admin@admin.com", password = "123", intentosFallidos = 0, bloqueado = false, esAdmin = true },
                new { id = 2, dni = 36522030, nombre = "Matias", apellido = "Gomez", mail = "matias_gomez05@hotmail.com", password = "123", intentosFallidos = 0, bloqueado = false, esAdmin = false },
                new { id = 3, dni = 41883064, nombre = "Juan", apellido = "Saavedra", mail = "juansaavedra@gmail.com", password = "123", intentosFallidos = 0, bloqueado = false, esAdmin = false }
            );
            modelBuilder.Entity<UsuarioCaja>().HasData(
                new { id = 1, idUsuario = 1, idCaja = 1 },
                new { id = 2, idUsuario = 1, idCaja = 2 },
                new { id = 3, idUsuario = 2, idCaja = 2 },
                new { id = 4, idUsuario = 2, idCaja = 3 },
                new { id = 5, idUsuario = 3, idCaja = 4 },
                new { id = 6, idUsuario = 2, idCaja = 5 }
            );
            modelBuilder.Entity<CajaDeAhorro>().HasData(
                new { id = 1, cbu = 7000001, saldo = 20000.0 },
                new { id = 2, cbu = 7000002, saldo = 15000.0 },
                new { id = 3, cbu = 7000003, saldo = 10000.0 },
                new { id = 4, cbu = 7000004, saldo = 50000.0 },
                new { id = 5, cbu = 7000005, saldo = 45200.0 }
            );

            modelBuilder.Entity<Movimiento>().HasData(
                new { id = 1, idCaja = 1, detalle = "Abona Luz", monto = 2000.0, fecha = DateTime.Now },
                new { id = 2, idCaja = 1, detalle = "Abona Gas", monto = 5500.0, fecha = DateTime.Now },
                new { id = 3, idCaja = 2, detalle = "Extraccion", monto = 3000.0, fecha = DateTime.Now },
                new { id = 4, idCaja = 3, detalle = "Extraccion", monto = 3000.0, fecha = DateTime.Now },
                new { id = 5, idCaja = 4, detalle = "Nuevo pago : varios.", monto = 3000.0, fecha = DateTime.Now },
                new { id = 6, idCaja = 5, detalle = "Pago tarjeta", monto = 3000.0, fecha = DateTime.Now }
            );
            modelBuilder.Entity<Pago>().HasData(
                new { id = 1, idUsuario = 1, nombre = "Luz", monto = 2000.0, pagado = true, metodo = "Tarjeta de Credito" },
                new { id = 2, idUsuario = 2, nombre = "Gas", monto = 5500.0, pagado = true, metodo = "Caja de Ahorro" },
                new { id = 3, idUsuario = 2, nombre = "Abono movil", monto = 1000.0, pagado = false, metodo = "" },
                new { id = 4, idUsuario = 3, nombre = "Telecentro", monto = 5500.0, pagado = false, metodo = "" },
                new { id = 5, idUsuario = 3, nombre = "Luz", monto = 3000.0, pagado = false, metodo = "" }
            );
            modelBuilder.Entity<PlazoFijo>().HasData(
                new { id = 1, idTitular = 2, monto = 2000.0, fechaIni = DateTime.Parse("22-10-2022"), fechaFin = DateTime.Now, tasa = 70.4, pagado = false, cbu = 7000001 },
                new { id = 2, idTitular = 2, monto = 5000.0, fechaIni = DateTime.Now, fechaFin = DateTime.Now.AddDays(30), tasa = 70.5, pagado = false, cbu = 7000002 }
            );
            modelBuilder.Entity<TarjetaDeCredito>().HasData(
                new { id = 1, idTitular = 1, numero = 12312, codigoV = 10001, limite = 5000.0, consumos = 550.0 },
                new { id = 2, idTitular = 2, numero = 45645, codigoV = 10002, limite = 7000.0, consumos = 1000.0 },
                new { id = 3, idTitular = 2, numero = 78978, codigoV = 10002, limite = 3000.0, consumos = 0.0 },
                new { id = 4, idTitular = 3, numero = 14785, codigoV = 10002, limite = 6000.0, consumos = 0.0 },
                new { id = 5, idTitular = 3, numero = 36985, codigoV = 10002, limite = 2000.0, consumos = 0.0 }
            );

        }

    }
}
