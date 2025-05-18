using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Application.DTOs
{
    public class PessoaFisicaDto : PessoaDto
    {
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
