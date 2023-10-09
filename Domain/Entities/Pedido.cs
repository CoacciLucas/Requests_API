using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public enum Status { Criado, Cancelado, Concluido }
    public class Pedido
    {
        public Pedido(string idUsuario)
        {
            Id = Guid.NewGuid().ToString();
            IdUsuario = idUsuario;
            Status = Status.Criado;
            Validar();
        }
        protected Pedido() { }
        public string Id { get; private set; }
        public string IdUsuario { get; private set; }
        private readonly List<Item> _itens = new List<Item>();
        public IReadOnlyCollection<Item> Itens => _itens;
        public Status Status { get; private set; }
        public decimal ValorTotal { get; private set; }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (_itens.Any(x => x.ProdutoId == produto.Id))
                throw new InvalidOperationException("O produto ja existe na lista");
            Validar();
            _itens.Add(new Item(produto, this, quantidade));

            ValorTotal += produto.Valor * quantidade;
        }
        public void RemoverItem(string idItem)
        {
            var item = _itens.FirstOrDefault(x => x.Id == idItem);
            if (item == null)
                throw new InvalidOperationException("Item nao encontrado");
            _itens.Remove(item);
        }

        public void Validar()
        {
            ValidarStatus(Status);
            ValidarValorTotal(ValorTotal);
        }
        public void ValidarStatus(Status status)
        {
            bool success = Enum.IsDefined(typeof(Status), status);
            if (!success)
                throw new InvalidOperationException("Status invalido!");
            
        }

        public void ValidarValorTotal(decimal valorTotal)
        {
            if (valorTotal < 0)
                throw new InvalidOperationException("Valor total deve ser maior que ou igual a 0");
        }
    }
}