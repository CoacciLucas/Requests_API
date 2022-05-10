using Application.Commands;
using Application.Interfaces;
using Domain.Entities;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly Context _context;

        public UsuarioService(Context context)
        {
            _context = context;
            _usuarioRepository = new UsuarioRepository(_context);
        }
        public async Task<List<VisualizarUsuario>> GetAll()
        {
            var usuariosParaVisualizacao = new List<VisualizarUsuario>();
            var usuarios = await _context.Usuarios.ToListAsync();

            foreach (var usuario in usuarios)
                usuariosParaVisualizacao.Add(new VisualizarUsuario(usuario.Nome, usuario.Email, usuario.Cpf, usuario.DataNascimento));

            return usuariosParaVisualizacao;
        }
        public async Task Add(CadastrarUsuario usuarioCommand)
        {
            ValidarUsuario(usuarioCommand);
            var usuario = new Usuario(usuarioCommand.Nome, usuarioCommand.Email, usuarioCommand.Cpf, usuarioCommand.DataNascimento);
            await _usuarioRepository.Create(usuario);
        }
        public async Task<VisualizarUsuario> Get(Guid id)
        {
            var usuario = await _usuarioRepository.Get(id);
            var viewUsuario = new VisualizarUsuario(usuario.Nome, usuario.Email, usuario.Cpf, usuario.DataNascimento);

            return viewUsuario;
        }
        public async Task Update(Guid id, AtualizarUsuario usuarioCommand)
        {
            if (!UsuarioExists(id))
                throw new ArgumentNullException("Usuário não foi encontrado");

            var usuario = await _usuarioRepository.Get(id);
            usuario.DefinirNome(usuarioCommand.Nome);
            usuario.DefinirCpf(usuarioCommand.Cpf);
            usuario.DefinirDataNascimento(usuarioCommand.DataNascimento);
            usuario.DefinirAtivo(usuarioCommand.Ativo);

            await _usuarioRepository.Update(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var usuario = await _usuarioRepository.Get(id);

            await _usuarioRepository.Delete(usuario);
            await _context.SaveChangesAsync();
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
        private void ValidarUsuario(CadastrarUsuario usuario)
        {
            ValidarCpf(usuario.Cpf);
            ValidarEmail(usuario.Email);
            ValidarNome(usuario.Nome);
            /*ValidarAtivo(usuario.Ativo);*/
            ValidarDataNascimento(usuario.DataNascimento);
        }
        private void ValidarEmail(string email)
        {

            int indexArr = email.IndexOf('@');
            if (indexArr > 0)
            {
                int indexDot = email.IndexOf('.', indexArr);
                if (indexDot - 1 > indexArr)
                {
                    if (indexDot + 1 < email.Length)
                    {
                        string indexDot2 = email.Substring(indexDot + 1, 1);
                        if (indexDot2 != ".")
                        {

                        }
                        else
                        {
                            throw new InvalidOperationException("Email Inválido");
                        }

                    }
                    else
                    {
                        throw new InvalidOperationException("Email Inválido");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Email Inválido");
                }
            }
            else
                throw new InvalidOperationException("Email Inválido");

        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome) || nome.Length > 60)
                throw new InvalidOperationException("Nome Inválido");

        }

        private void ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                throw new InvalidOperationException("CPF Inválido");
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            digito = Regex.Replace(digito, "[^0-9]", "");
        }

        private void ValidarAtivo(bool ativo)
        {
            if (ativo != true && ativo != false)
                throw new InvalidOperationException("O ativo deve ser somente true ou false!");

        }

        public void ValidarDataNascimento(DateTime date)
        {
            if (!BeAValidDate(date) && !NotBeAFutureDate(date))
                throw new InvalidOperationException("Data de nascimento inválida!");
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }

        private bool NotBeAFutureDate(DateTime date)
        {
            return !(date.Date > DateTime.Now.Date);
        }

    }
}
