using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class VisualizarProduto
    {
        public VisualizarProduto(string descricao, decimal valor, int quantidadeNoEstoque)
        {
            Descricao = descricao;
            Valor = valor;
            QuantidadeNoEstoque = quantidadeNoEstoque;
        }

        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public int QuantidadeNoEstoque { get; private set; }
    }
}
