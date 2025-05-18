
using RegistroEmpresas.Domain.RegistroEmpresarial.Entities;
using RegistroEmpresas.Domain.RegistroEmpresarial.Integration;
using RegistroEmpresas.Domain.RegistroEmpresarial.Interfaces.Repositories;
using RegistroEmpresas.Domain.RegistroEmpresarial.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.AppServices
{
    public class RegistrarEmpresaService : IRegistrarEmpresaService
    {
        private readonly IPessoaJuridicaRepository _empresaRepository;
        private readonly IContabilidadeAdapter _contabilidadeAdapter;

        public RegistrarEmpresaService(IPessoaJuridicaRepository empresaRepository, IContabilidadeAdapter contabilidadeAdapter)
        {
            _empresaRepository = empresaRepository;
            _contabilidadeAdapter = contabilidadeAdapter;
        }

        public void Registrar(PessoaJuridica empresa)
        {
            _empresaRepository.Adicionar(empresa);
            _contabilidadeAdapter.EnviarDadosEmpresa(empresa);
        }
    }
}
