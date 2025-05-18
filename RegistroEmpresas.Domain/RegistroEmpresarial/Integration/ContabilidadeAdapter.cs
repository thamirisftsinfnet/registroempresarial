using RegistroEmpresas.Domain.RegistroEmpresarial.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.RegistroEmpresarial.Integration
{
    public class ContabilidadeAdapter : IContabilidadeAdapter
    {
        private readonly ISistemaContabil _sistemaContabil;
        public ContabilidadeAdapter()
        {
            _sistemaContabil = new SistemaContabil();
        }

        public void EnviarDadosEmpresa(PessoaJuridica empresa)
        {
            _sistemaContabil.RegistrarEmpresa(
                empresa.Cnpj,
                empresa.Nome,
                empresa.CapitalSocial
            );
        }
    }
}
