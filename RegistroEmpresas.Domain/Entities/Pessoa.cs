using RegistroEmpresas.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.Entities
{
    public abstract class Pessoa
    {
        public Int32 Id { get; set; }
        public string Nome { get; set; }

        public Endereco Endereco { get; set; }
        public abstract string ObterDocumento();

        protected Pessoa(int id, string nome, Endereco endereco)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            Id = id;
            Nome = nome;
        }
        public void AtualizarEndereco(Endereco novoEndereco)
        {
            Endereco = novoEndereco ?? throw new ArgumentNullException(nameof(novoEndereco));
        }
        
    }
}
