using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ProdutoCmd
{
    public class CadastrarProduto
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeNoEstoque { get; set; }
    }
}
