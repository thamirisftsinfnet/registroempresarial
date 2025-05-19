using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.Contabilidade.Integration
{
    public interface ISistemaContabil
    {
        void RegistrarEmpresa(string cnpj, string razaoSocial, decimal capitalSocial);
    }
}
