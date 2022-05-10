﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            /*Validar se o produto já existe na lista de itens*/
            if (_itens.Any(x => x.ProdutoId == produto.Id))
                throw new InvalidOperationException("O produto ja existe na lista");

            var item = new Item(produto, this, quantidade);
            _itens.Add(item);
            ValorTotal += produto.Valor * quantidade;
        }
        public void Delete(Item item)
        {
            _itens.Remove(item);
        }
    }
}