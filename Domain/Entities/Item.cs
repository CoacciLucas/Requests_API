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
            Descricao = produto.Descricao;
            ProdutoId = produto.Id;
            PedidoId = pedido.Id;
            Quantidade = quantidade;
            Validar();
            
        }

        protected Item() { }
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Guid PedidoId { get; private set; }
        public int Quantidade { get; private set; }

        private void Validar()
        {
            ValidarDescricao(Descricao);
            ValidarQuantidade(Quantidade);
        }
        private void ValidarQuantidade(int quantidade)
        {
            if (quantidade < 0)
                throw new InvalidOperationException("Quantidade invalida! (Não pode ser menor que 0)");

        }

        private void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao) || descricao.Length > 200)
                throw new InvalidOperationException("Descricao inválida");
        }
    }
}
