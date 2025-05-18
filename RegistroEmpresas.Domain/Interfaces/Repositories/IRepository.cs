using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Adicionar(T entidade);
        T ObterPorId(int id);
        IEnumerable<T> ObterTodos();
        void Remover(int id);
        void Atualizar(T entidade);
    }
}
