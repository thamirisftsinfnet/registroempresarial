using RegistroEmpresas.Domain.Entities;
using RegistroEmpresas.Domain.RegistroEmpresarial.Interfaces.Services;
using RegistroEmpresas.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Domain.RegistroEmpresarial.Entities
{
    public class PessoaJuridica : Pessoa
    {
        public string Cnpj { get; private set; }
        public Int32 CodigoNaturezaJuridica { get; set; }
        public string NomeFantasia { get; set; }
        public string ObjetoSocial { get; set; }
        public Decimal CapitalSocial { get; set; }
        private readonly List<ParticipacaoSocietaria> _quadroSocietario = new List<ParticipacaoSocietaria>();
        public IReadOnlyCollection<ParticipacaoSocietaria> QuadroSocietario => _quadroSocietario.AsReadOnly();
        public PessoaJuridica(
        int id,
        string nome,
        Endereco endereco,
        string cnpj,
        string nomeFantasia,
        string objetoSocial,
        int codigoNaturezaJuridica,
        decimal capitalSocial) : base(id, nome, endereco)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) throw new ArgumentException("CNPJ é obrigatório.");
            if (capitalSocial <= 0) throw new ArgumentException("Capital social deve ser maior que zero.");

            Cnpj = cnpj;
            NomeFantasia = nomeFantasia;
            ObjetoSocial = objetoSocial;
            CodigoNaturezaJuridica = codigoNaturezaJuridica;
            CapitalSocial = capitalSocial;
            Endereco = endereco;
        }
        // Método Factory estático (padrão GRASP Creator)
        public static PessoaJuridica CriarEmpresa(
            int id,
            string nome,
            Endereco endereco,
            string cnpj,
            string nomeFantasia,
            string objetoSocial,
            int codigoNaturezaJuridica,
            decimal capitalSocial,
            IParticipacaoSocietariaService participacaoService,
            List<ParticipacaoSocietaria> socios)
        {
            var sociosList = socios.ToList();

            participacaoService.ValidarParticipacoes(sociosList, capitalSocial);

            var empresa = new PessoaJuridica(
                id,
                nome,
                endereco,
                cnpj,
                nomeFantasia,
                objetoSocial,
                codigoNaturezaJuridica,
                capitalSocial
            );

            // Adiciona os sócios
            foreach (var socio in sociosList)
            {
                empresa.AdicionarSocio(socio);
            }

            return empresa;
        }
        public void AdicionarSocio(ParticipacaoSocietaria socio)
        {
            if (socio == null)
                throw new ArgumentNullException(nameof(socio));

            _quadroSocietario.Add(socio);
        }

        public void RemoverSocio(Pessoa socio)
        {
            _quadroSocietario.RemoveAll(p => p.Socio.Id == socio.Id);
        }

        public decimal TotalParticipacoes() => _quadroSocietario.Sum(p => p.ValorParticipacao);

        public override string ObterDocumento() => Cnpj;

    }
}
