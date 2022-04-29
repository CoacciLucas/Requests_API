using System;

namespace Domain
{
    public class Produto
    {
        public Produto(string descricao, decimal valor, int quantidadeNoEstoque)
        {
            IdProduto = Guid.NewGuid();
            Descricao = descricao;
            Valor = valor;
            Ativo = true;
            QuantidadeNoEstoque = quantidadeNoEstoque;
            ValidarProduto();
        }
        protected Produto() { }
        public Guid IdProduto { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public bool Ativo { get; private set; }
        public int QuantidadeNoEstoque { get; private set; }

        public void ValidarProduto()
        {
            ValidarDescricao(Descricao);
            ValidarValor(Valor);
            ValidarQuantidadeNoEstoque(QuantidadeNoEstoque);
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

        private void ValidarQuantidadeNoEstoque(int quantidadeNoEstoque)
        {
            if (!(quantidadeNoEstoque % 1 == 0) || quantidadeNoEstoque < 0)
            {
                throw new InvalidOperationException("Quantidade no estoque invalida!");
            }
        }


    }
}
