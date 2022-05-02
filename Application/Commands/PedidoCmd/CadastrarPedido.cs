using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Commands.PedidoCmd
{
    public class CadastrarPedido
    {

        public Guid IdUsuario { get; set; }
        public string Descricao { get; set; }
        public List<Produto> Itens { get; set; }
        public Status Status { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
