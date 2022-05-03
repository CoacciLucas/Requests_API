using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.PedidoCmd
{
    public class VisualizarPedido
    {
        public VisualizarPedido(Pedido pedido)
        {
            this.pedido = pedido;
        }

        public Pedido pedido { get; set; }
    }
}
