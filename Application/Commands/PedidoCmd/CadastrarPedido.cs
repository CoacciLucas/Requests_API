using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Commands.PedidoCmd
{
    public class CadastrarPedido
    {

        public Guid IdUsuario { get; set; }
        public List<Produto> Itens { get; set; }

    }
}
