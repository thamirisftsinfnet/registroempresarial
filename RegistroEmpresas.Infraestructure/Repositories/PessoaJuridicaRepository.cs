using RegistroEmpresas.Domain.RegistroEmpresarial.Entities;
using RegistroEmpresas.Domain.RegistroEmpresarial.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Infraestructure.Repositories
{
    public class PessoaJuridicaRepository : IPessoaJuridicaRepository
    {
        private readonly Dictionary<int, PessoaJuridica> _empresas = new Dictionary<int, PessoaJuridica>();
        private int _proximoId = 1;

        public void Adicionar(PessoaJuridica empresa)
        {
            if (empresa == null)
                throw new ArgumentNullException(nameof(empresa));

            empresa.Id = _proximoId++;
            _empresas[empresa.Id] = empresa;
        }

        public PessoaJuridica ObterPorId(int id)
        {
            _empresas.TryGetValue(id, out var empresa);
            return empresa;
        }

        public IEnumerable<PessoaJuridica> ObterTodos()
        {
            return _empresas.Values.ToList();
        }

        public void Remover(int id)
        {
            _empresas.Remove(id);
        }

        public void Atualizar(PessoaJuridica empresa)
        {
            if (empresa == null)
                throw new ArgumentNullException(nameof(empresa));

            if (!_empresas.ContainsKey(empresa.Id))
                throw new InvalidOperationException("PessoaJuridica não encontrada.");

            _empresas[empresa.Id] = empresa;
        }

    }
}
