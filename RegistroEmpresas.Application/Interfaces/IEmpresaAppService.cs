using RegistroEmpresas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Application.Interfaces
{
    public interface IEmpresaAppService
    {
        int CriarEmpresa(PessoaJuridicaDto empresaDto);
        PessoaJuridicaDto ObterEmpresa(int id);
        IEnumerable<PessoaJuridicaDto> ListarEmpresas();
        void RemoverEmpresa(int id);
    }
}
