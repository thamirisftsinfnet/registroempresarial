using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.ValueObjects
{
    public class Endereco
    {

        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Complemento { get; set; }
        public Endereco(
        string cep,
        string logradouro,
        string numero,
        string bairro,
        string municipio,
        string uf,
        string complemento = null)
        {
            Cep = cep ?? throw new ArgumentNullException(nameof(cep));
            Logradouro = logradouro ?? throw new ArgumentNullException(nameof(logradouro));
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
            Bairro = bairro ?? throw new ArgumentNullException(nameof(bairro));
            Municipio = municipio ?? throw new ArgumentNullException(nameof(municipio));
            Uf = uf ?? throw new ArgumentNullException(nameof(uf));
            Complemento = complemento; // opcional
        }
    }
}
