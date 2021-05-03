﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ventas.domain.Base;

namespace ventas.domain
{
    public class ProductoCompuesto : Producto
    {

        private List<Producto> Productos { get; set; }


        public ProductoCompuesto(string id, string nombre, decimal precio, List<Producto> productos) : base(id, nombre, 0, precio)
        {
            Productos = productos;
            getCosto();
        }

        private ProductoCompuesto()
        {
        }

        public override string RegistrarSalida(int cantidad)
        {
            foreach (var item in Productos)
            {
                item.RegistrarSalida(cantidad);
            }

            return $"Nueva salida: {Nombre}, cantidad:{cantidad}, costo:{Costo}, precio:{Precio}";

        }

        public override decimal getCosto()
        {
            decimal total = 0;
            foreach (var item in Productos)
            {
                total += item.getCosto();
            }
            this.Costo = total;
            return total;
        }
    }
}
