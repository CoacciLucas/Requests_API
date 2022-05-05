using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Item
    {
        public Item(Produto produto, Pedido pedido, int quantidade)
        {
            Id = Guid.NewGuid();
            ProdutoId = produto.Id;
            PedidoId = pedido.Id;
            Quantidade = quantidade;
            ProdutoId = produto.Id;
            PedidoId = pedido.Id;
        }
        protected Item() { }
        public Guid Id { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Guid PedidoId { get; private set; }
        public int Quantidade { get; private set; } 
/*      public Produto Produto { get; private set; }
        public Pedido Pedido { get; private set; }*/
    }
}
