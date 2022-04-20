using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public enum Status { Criado, Cancelado, Concluido }
    public class Pedido
    {
        public Guid IdUsuario { get; private set; }
        public List<Produto> Items { get; private set; }
        public Status Status { get; private set; }
        public decimal ValorTotal { get; private set; }
            
    }
}
