using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public enum Status { Criado, Cancelado, Concluido }
    public class Pedido
    {
        public Pedido(Guid idUsuario)
        {
            Id = Guid.NewGuid();
            IdUsuario = idUsuario;
            Status = Status.Criado;
            /*Chamar o Validar()*/
        }
        protected Pedido() { }
        public Guid Id { get; private set; }
        public Guid IdUsuario { get; private set; }
        private readonly List<Item> _itens = new List<Item>();
        public IReadOnlyCollection<Item> Itens => _itens;
        public Status Status { get; private set; }
        public decimal ValorTotal { get; private set; }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (_itens.Any(x => x.ProdutoId == produto.Id))
                throw new InvalidOperationException("O produto ja existe na lista");

            _itens.Add(new Item(produto, this, quantidade));

            ValorTotal += produto.Valor * quantidade;
        }
        public void RemoverItem(Item item)
        {
            _itens.Remove(item);
        }
    }
}