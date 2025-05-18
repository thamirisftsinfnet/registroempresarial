using RegistroEmpresas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.RegistroEmpresarial.Entities
{
    public class ParticipacaoSocietaria
    {
        public Pessoa Socio { get; }
        public decimal ValorParticipacao { get; }
        public DateTime DataEntrada { get; }
        public DateTime? DataSaida { get; }

        public bool Ativo => DataSaida == null;

        public ParticipacaoSocietaria(Pessoa socio, decimal valorParticipacao, DateTime dataEntrada, DateTime? dataSaida = null)
        {
            Socio = socio ?? throw new ArgumentNullException(nameof(socio));
            ValorParticipacao = valorParticipacao;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
        }
    }
}
