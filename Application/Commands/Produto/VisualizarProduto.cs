using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class VisualizarProduto
    {
        public VisualizarProduto(Guid id, string descricao, decimal valor, int quantidadeNoEstoque)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            QuantidadeNoEstoque = quantidadeNoEstoque;
        }

        public Guid Id{ get; set; }  
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public int QuantidadeNoEstoque { get; private set; }
    }
}
