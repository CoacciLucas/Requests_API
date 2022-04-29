using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class NomeServices : INomeServices
    {
        public void IsName(string nome)
        {
            if (string.IsNullOrEmpty(nome) || nome.Length > 60)
            {
                throw new InvalidOperationException("Nome Inválido");
            }
        }
    }
}
