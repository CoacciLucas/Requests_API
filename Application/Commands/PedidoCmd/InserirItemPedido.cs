using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.PedidoCmd
{
    public class InserirItemPedido
    {
        public Guid ProdutoId { get; set; }
        public Guid PedidoId { get; set; }
    }
}
