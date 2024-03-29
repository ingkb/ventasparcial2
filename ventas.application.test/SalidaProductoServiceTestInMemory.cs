﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ventas.domain;
using ventas.infrastructure;

namespace ventas.application.test
{
    [TestFixture]
    class SalidaProductoServiceTestInMemory
    {
        public ventasContext _dbContext;
        public SalidaProductoService _salidaService;//SUT - Objeto bajo prueba

        //se ejecuta una vez por cada prueba //hace parte del Arrange
        [OneTimeSetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            //Arrange
            var optionsSqlite = new DbContextOptionsBuilder<ventasContext>()
           .UseSqlite(connection)
           .Options;

            connection.Open();

            _dbContext = new ventasContext(optionsSqlite);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _salidaService = new SalidaProductoService(
                new UnitOfWork(_dbContext),
                new ProductoRepository(_dbContext));

            ProductoSimple pan = (ProductoSimple)ProductoMother.ProductoPan("005");
            pan.RegistrarEntrada(10);

            ProductoSimple salchicha = (ProductoSimple)ProductoMother.ProductoSalchicha("006");
            salchicha.RegistrarEntrada(10);

            ProductoCompuesto perro = new("004", "Perro", 5000, new List<Producto> { pan, salchicha });

            //_dbContext.Productos.Add(pan);
            _dbContext.Productos.Add(salchicha);
            _dbContext.Productos.Add(perro);
            _dbContext.SaveChanges();

        }

        [Test]
        public void SalidaProductoSimpleTest()
        {
            var response = _salidaService.Ejecutar(new SalidaProductoRequest("001", 3));
            Assert.AreEqual("Nueva salida: Pan, cantidad:3, costo_total:$ 500,00, precio_total:$ 0,00", response.Mensaje);
        }

        [Test]
        public void SalidaProductoCompuestoTest()
        {
            var response = _salidaService.Ejecutar(new SalidaProductoRequest("004", 4));
            Assert.AreEqual("Nueva salida: Perro, cantidad:4, costo_total:$ 1.500,00, precio_total:$ 20.000,00", response.Mensaje);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}
