using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public enum Status { Criado, Cancelado, Concluido }
    public class Pedido
    {
        public Pedido(Guid idUsuario, string descricao, Status status, decimal valorTotal)
        {
            Id = Guid.NewGuid();
            IdUsuario = idUsuario;
            Descricao = descricao;
            Itens = new List<Produto>();
            Status = status;
            ValorTotal = valorTotal;
        }
        protected Pedido() { }
        public Guid Id { get; private set; }
        public Guid IdUsuario { get; private set; }
        public string Descricao { get; private set; }
        public List<Produto> Itens { get; private set; }
        public Status Status { get; private set; }
        public decimal ValorTotal { get; private set; }

        public void ValidarPedido()
        {

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