using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class CadastrarProduto
    {
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public decimal Ativo { get; private set; }
        public decimal QuantidadeNoEstoque { get; private set; }
    }
}
