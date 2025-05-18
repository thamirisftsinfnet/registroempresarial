using RegistroEmpresas.Domain.RegistroEmpresarial.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.RegistroEmpresarial.Interfaces.Services
{
    public interface IRegistrarEmpresaService
    {
        void Registrar(PessoaJuridica empresa);
    }
}
