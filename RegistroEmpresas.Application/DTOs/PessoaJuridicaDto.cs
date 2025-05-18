using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Application.DTOs
{
    public class PessoaJuridicaDto : PessoaDto
    {
        public string Cnpj { get; set; }
        public string NomeFantasia { get; set; }
        public string ObjetoSocial { get; set; }
        public int CodigoNaturezaJuridica { get; set; }
        public decimal CapitalSocial { get; set; }
        public List<SocioDto> Socios { get; set; }
    }

    public class SocioDto : PessoaDto
    {
        public string Cpf { get; set; }               
        public DateTime? DataNascimento { get; set; } 

        public decimal Participacao { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
    }
}
