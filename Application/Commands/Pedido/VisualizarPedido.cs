using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Commands
{
    public class VisualizarPedido
    {
        public VisualizarPedido(Guid id, decimal valorTotal, Status status)
        {
            Id = id;
            ValorTotal = valorTotal;
            Status = status;
            Itens = new List<VisualizarItem>();
        }

        public Guid Id { get; set; }
        public decimal ValorTotal { get; set; }
        public Status Status { get; set; }
        public List<VisualizarItem> Itens { get; set; }

        public void AdicionarItem(Guid id, string descricao, int quantidade)
        {
            Itens.Add(new VisualizarItem(id, descricao, quantidade));
        }


        public class VisualizarItem
        {
            public VisualizarItem(Guid id, string descricao, int quantidade)
            {
                Id = id;
                Descricao = descricao;
                Quantidade = quantidade;

            }
            protected VisualizarItem() { }
            public Guid Id { get; set; }
            public string Descricao { get; set; }
            public int Quantidade { get; set; }
        }
    }
}
