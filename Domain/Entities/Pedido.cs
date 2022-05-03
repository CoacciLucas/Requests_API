using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public enum Status { Criado, Cancelado, Concluido }
    public class Pedido
    {
        public Pedido(Guid idUsuario, string descricao/*, decimal valorTotal*/)
        {
            Id = Guid.NewGuid();
            IdUsuario = idUsuario;
            Descricao = descricao;
            Status = Status.Criado;
            /* ValorTotal = valorTotal;*/
        }
        protected Pedido() { }
        public Guid Id { get; private set; }
        public Guid IdUsuario { get; private set; }
        public string Descricao { get; private set; }
        private readonly List<Item> _itens = new List<Item>();
        public IReadOnlyCollection<Item> Itens => _itens;
        /*public List<Item> Itens { get; private set; }*/
        public Status Status { get; private set; }
        public decimal ValorTotal { get; private set; }

        public void ValidarPedido()
        {

        }
        public void AdicionarItem(Produto produto, int quantidade)
        {
            _itens.Add(new Item(produto, this, quantidade));
            ValorTotal = ValorTotal + (produto.Valor * quantidade);
        }
        public void ValidarStatus(string status)
        {
            if (status != "Criado" && status != "Cancelado" && status != "Concluido")
                throw new ArgumentNullException("Status invalido!");
        }

        public void ValidarValorTotal(decimal valorTotal)
        {
            if (valorTotal < 0)
                throw new ArgumentNullException("Valor total deve ser maior que ou igual a 0");


        }

    }
}