
using RegistroEmpresas.Application.DTOs;
using RegistroEmpresas.Application.Interfaces;
using RegistroEmpresas.Application.Mappers;
using RegistroEmpresas.Domain.Entities;
using RegistroEmpresas.Domain.RegistroEmpresarial.Entities;
using RegistroEmpresas.Domain.RegistroEmpresarial.Interfaces.Repositories;
using RegistroEmpresas.Domain.RegistroEmpresarial.Interfaces.Services;
using RegistroEmpresas.Domain.RegistroEmpresarial.Services;
using RegistroEmpresas.Domain.ValueObjects;
using RegistroEmpresas.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Application.AppServices
{
    public class EmpresaAppService
    {
        private readonly IPessoaJuridicaRepository _empresaRepository;
        private readonly IParticipacaoSocietariaService _participacaoService;

        public EmpresaAppService( )
        {
            _empresaRepository = new PessoaJuridicaRepository();
            _participacaoService = new ParticipacaoSocietariaService();
        }

        public void CriarEmpresa(PessoaJuridicaDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var enderecoEmpresa = new Endereco(
                dto.Endereco.Logradouro,
                dto.Endereco.Numero,
                dto.Endereco.Bairro,
                dto.Endereco.Cidade,
                dto.Endereco.Estado,
                dto.Endereco.Cep
            );

            var socios = new List<ParticipacaoSocietaria>();

            foreach (var s in dto.Socios)
            {

                var endereco = new Endereco(
                    s.Endereco.Logradouro,
                    s.Endereco.Numero,
                    s.Endereco.Bairro,
                    s.Endereco.Cidade,
                    s.Endereco.Estado,
                    s.Endereco.Cep
                );
                var socio = new PessoaFisica(
                    id: IdGenerator.GerarId(),
                    nome: s.Nome,
                    cpf: s.Cpf,
                    endereco: endereco,
                    dataNascimento: s.DataNascimento ?? throw new ArgumentException("Data de nascimento é obrigatória para pessoa física.")
                );

                var participacao = new ParticipacaoSocietaria(
                    socio,
                    s.Participacao,
                    s.DataEntrada,
                    s.DataSaida
                );

                socios.Add(participacao);
            }
            _participacaoService.ValidarParticipacoes(socios, dto.CapitalSocial);
            var empresa = PessoaJuridica.CriarEmpresa(
                id: IdGenerator.GerarId(),
                nome: dto.Nome,
                endereco: enderecoEmpresa,
                cnpj: dto.Cnpj,
                nomeFantasia: dto.NomeFantasia,
                objetoSocial: dto.ObjetoSocial,
                codigoNaturezaJuridica: dto.CodigoNaturezaJuridica,
                capitalSocial: dto.CapitalSocial, _participacaoService,
                socios: socios
            );

            _empresaRepository.Adicionar(empresa);
        }
    }

}
