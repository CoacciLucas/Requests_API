using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.PedidoCmd
{
    public class VisualizarPedido
    {
        public VisualizarPedido(Guid idUsuario, string descricao, List<Produto> items, Status status, decimal valorTotal)
        {
            IdUsuario = idUsuario;
            Descricao = descricao;
            Items = items;
            Status = status;
            ValorTotal = valorTotal;
        }

        public Guid IdUsuario { get; set; }
        public string Descricao { get; set; }   
        public List<Produto> Items { get; set; }
        public Status Status { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
