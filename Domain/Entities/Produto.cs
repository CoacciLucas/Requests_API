using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public abstract class Produto
    {
        public string Desc { get; private set; }
        public decimal Valor { get; private set; }
        public decimal Ativo { get; private set; }
        public decimal QuantidadeNoEstoque { get; private set; }

    }
}
