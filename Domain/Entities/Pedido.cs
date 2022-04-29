using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public enum Status { Criado, Cancelado, Concluido }
    public class Pedido
    {
        public Guid Id { get; private set; }
        public List<Produto> Items { get; private set; }
        public Status Status { get; private set; }
        public decimal ValorTotal { get; private set; }

    }
}