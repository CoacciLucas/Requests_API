using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class ItemPedido
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
