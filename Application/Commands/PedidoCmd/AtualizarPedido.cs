using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.PedidoCmd
{
    public class AtualizarPedido
    {
        public Guid IdUsuario { get; set; }
        public string Descricao { get; set; }
        public List<Produto> Items { get; set; }
        public Status Status { get; set; }
    }
}
