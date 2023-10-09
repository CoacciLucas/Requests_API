using Amazon.DynamoDBv2.DataModel;
using System;

namespace Application.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string DataNascimento { get; set; }
        public string DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
