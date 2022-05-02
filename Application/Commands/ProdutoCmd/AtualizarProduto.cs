using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ProdutoCmd
{
    public class AtualizarProduto
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeNoEstoque { get; set; }
        public bool Ativo { get; set; }
    }
}
