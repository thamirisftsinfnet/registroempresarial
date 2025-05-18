using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.RegistroEmpresarial.Integration
{
    public class SistemaContabil : ISistemaContabil
    {
        public void RegistrarEmpresa(string cnpj, string razaoSocial, decimal capitalSocial)
        {
            //Console.WriteLine($"[Contabilidade] Empresa registrada: {razaoSocial}, CNPJ: {cnpj}, Capital: {capitalSocial:C}");
        }
    }
}
