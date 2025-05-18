using RegistroEmpresas.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.Entities
{
    public class PessoaFisica : Pessoa
    {
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

        public PessoaFisica(int id, string nome, string cpf,  Endereco endereco, DateTime dataNascimento)
        : base(id, nome, endereco)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF é obrigatório.");

            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
        // Implementa o método abstrato da classe base
        public override string ObterDocumento() => Cpf;
    }
}
