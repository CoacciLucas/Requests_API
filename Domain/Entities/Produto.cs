using System;

namespace Domain.Entities
{
    public class Produto
    {
        public Produto(string descricao, decimal valor, int quantidadeNoEstoque)
        {
            Id = Guid.NewGuid();
            Descricao = descricao;
            Valor = valor;
            Ativo = true;
            QuantidadeNoEstoque = quantidadeNoEstoque;
        }
        protected Produto() { }
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public bool Ativo { get; private set; }
        public int QuantidadeNoEstoque { get; private set; }

        public void DefinirDescricao(string descricao)
        {
            Descricao = descricao;
        }
        public void DefinirValor(decimal valor)
        {
            Valor = valor;
        }
        public void DefinirAtivo(bool ativo)
        {
            Ativo = ativo;
        }
        public void DefinirQuantidadeNoEstoque(int quantidadeNoEstoque)
        {
            QuantidadeNoEstoque = quantidadeNoEstoque;
        }

    }
}
