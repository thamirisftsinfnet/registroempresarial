using RegistroEmpresas.Domain.RegistroEmpresarial.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.RegistroEmpresarial.Integration
{
    public interface IContabilidadeAdapter
    {
        void EnviarDadosEmpresa(PessoaJuridica empresa);
    }
}
