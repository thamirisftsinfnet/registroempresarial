using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Application.DTOs
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public EnderecoDto Endereco { get; set; }
        public string TipoPessoa { get; set; } // "F" = Física, "J" = Jurídica
    }
}
