using RegistroEmpresas.Domain.RegistroEmpresarial.Entities;
using RegistroEmpresas.Domain.RegistroEmpresarial.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.RegistroEmpresarial.Services
{
    public class ParticipacaoSocietariaService : IParticipacaoSocietariaService
    {

        public void ValidarParticipacoes(List<ParticipacaoSocietaria> participacoes, decimal capitalSocial)
        {
            if (participacoes == null || !participacoes.Any())
                throw new InvalidOperationException("A empresa deve ter pelo menos um sócio.");

            if (capitalSocial <= 0)
                throw new InvalidOperationException("O capital social deve ser maior que zero.");

            // Verifica se existem sócios com participações duplicadas (mesmo ID)
            var sociosDuplicados = participacoes
                .GroupBy(p => p.Socio.Id)
                .Where(g => g.Count() > 1)
                .ToList();

            if (sociosDuplicados.Any())
                throw new InvalidOperationException("Não é permitido ter sócios duplicados no quadro societário.");

            // Verifica se todos os sócios têm participação positiva
            foreach (var participacao in participacoes)
            {
                if (participacao.ValorParticipacao <= 0)
                    throw new InvalidOperationException($"O sócio '{participacao.Socio.Nome}' possui valor de participação inválido.");
            }

            // Verifica se a soma das participações é igual ao capital social
            var somaParticipacoes = participacoes.Sum(p => p.ValorParticipacao);

            if (somaParticipacoes != capitalSocial)
                throw new InvalidOperationException(
                    $"A soma das participações dos sócios ({somaParticipacoes:C}) deve ser igual ao capital social da empresa ({capitalSocial:C}).");


            // Verifica se a data de entrada é anterior à data de saída (quando existir)
            foreach (var participacao in participacoes)
            {
                if (participacao.DataSaida.HasValue &&
                    participacao.DataSaida.Value < participacao.DataEntrada)
                {
                    throw new InvalidOperationException($"O sócio '{participacao.Socio.Nome}' possui data de saída anterior à data de entrada.");
                }
            }
        }
    }
}
