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
            ValidarProduto();
        }
        protected Produto() { }
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public bool Ativo { get; private set; }
        public int QuantidadeNoEstoque { get; private set; }

        public void ValidarProduto()
        {
            ValidarDescricao(Descricao);
            ValidarValor(Valor);
            ValidarQuantidadeNoEstoque(QuantidadeNoEstoque);
            ValidarAtivo(Ativo);
        }

        private void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao) || descricao.Length > 200)
            {
                throw new InvalidOperationException("Descricao invalida!");
            }
        }

        private void ValidarValor(decimal valor)
        {
            if (valor <= 0 || valor.Equals(null))
            {
                throw new InvalidOperationException("Valor invalido!");
            }
        }

        private void ValidarAtivo(bool ativo)
        {
            if (ativo != true && ativo != false)
                throw new InvalidOperationException("O ativo deve ser apenas true or false!");
        }

        private void ValidarQuantidadeNoEstoque(int quantidadeNoEstoque)
        {
            if (!(quantidadeNoEstoque % 1 == 0) || quantidadeNoEstoque < 0)
            {
                throw new InvalidOperationException("Quantidade no estoque invalida!");
            }
        }

        public void DefinirDescricao(string descricao)
        {
            Descricao = descricao;
            ValidarDescricao(Descricao);
        }
        public void DefinirValor(decimal valor)
        {
            Valor = valor;
            ValidarValor(valor);
        }
        public void DefinirAtivo(bool ativo)
        {
            Ativo = ativo;
            ValidarAtivo(ativo);
        }
        public void DefinirQuantidadeNoEstoque(int quantidadeNoEstoque)
        {
            QuantidadeNoEstoque = quantidadeNoEstoque;
            ValidarQuantidadeNoEstoque(QuantidadeNoEstoque);
        }

    }
}
