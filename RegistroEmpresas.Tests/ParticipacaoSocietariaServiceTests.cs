using NUnit.Framework;
using RegistroEmpresas.Domain.Entities;
using RegistroEmpresas.Domain.RegistroEmpresarial.Entities;
using RegistroEmpresas.Domain.RegistroEmpresarial.Services;
using RegistroEmpresas.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class ParticipacaoSocietariaServiceTests
{
    private ParticipacaoSocietariaService _service;
    private PessoaFisica _socio1;
    private PessoaFisica _socio2;

    [SetUp]
    public void Setup()
    {
        _service = new ParticipacaoSocietariaService();
        var endereco = new Endereco("12345-678", "Rua A", "10", "Centro", "CidadeX", "UF");
        _socio1 = new PessoaFisica(1, "Sócio 1", "11111111111", endereco, new DateTime(1980, 1, 1));
        _socio2 = new PessoaFisica(2, "Sócio 2",  "22222222222", endereco,  new DateTime(1985, 1, 1));
    }

    [Test]
    public void ValidarParticipacoes_ComListaNula_DeveLancarException()
    {
        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(null, 1000m), "A empresa deve ter pelo menos um sócio.");
    }

    [Test]
    public void ValidarParticipacoes_ComListaVazia_DeveLancarException()
    {
        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(new List<ParticipacaoSocietaria>(), 1000m), "A empresa deve ter pelo menos um sócio.");
    }

    [Test]
    public void ValidarParticipacoes_ComCapitalSocialZeroOuMenor_DeveLancarException()
    {
        var participacoes = new List<ParticipacaoSocietaria>
        {
            new ParticipacaoSocietaria(_socio1, 1000m, DateTime.Today)
        };

        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, 0m), "O capital social deve ser maior que zero.");
        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, -10m), "O capital social deve ser maior que zero.");
    }

    [Test]
    public void ValidarParticipacoes_ComSociosDuplicados_DeveLancarException()
    {
        var participacoes = new List<ParticipacaoSocietaria>
        {
            new ParticipacaoSocietaria(_socio1, 500m, DateTime.Today),
            new ParticipacaoSocietaria(_socio1, 500m, DateTime.Today)
        };

        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, 1000m),"Não é permitido ter sócios duplicados no quadro societário.");
    }

    [Test]
    public void ValidarParticipacoes_ComParticipacaoZeroOuNegativa_DeveLancarException()
    {
        var participacoes = new List<ParticipacaoSocietaria>
        {
            new ParticipacaoSocietaria(_socio1, 0m, DateTime.Today),
            new ParticipacaoSocietaria(_socio2, 1000m, DateTime.Today)
        };

        var ex = Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, 1000m), $"O sócio '{_socio1.Nome}' possui valor de participação inválido.");
        StringAssert.Contains("Sócio 1", ex.Message);
    }

    [Test]
    public void ValidarParticipacoes_ComSomaParticipacoesDiferenteDoCapital_DeveLancarException()
    {
        var participacoes = new List<ParticipacaoSocietaria>
        {
            new ParticipacaoSocietaria(_socio1, 400m, DateTime.Today),
            new ParticipacaoSocietaria(_socio2, 400m, DateTime.Today)
        };
        var somaParticipacoes = participacoes.Sum(p => p.ValorParticipacao);
        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, 100m), $"A soma das participações dos sócios ({somaParticipacoes:C}) deve ser igual ao capital social da empresa ({100m:C}).");
    }

    [Test]
    public void ValidarParticipacoes_ComDataSaidaAnteriorDataEntrada_DeveLancarException()
    {
        var participacoes = new List<ParticipacaoSocietaria>
        {
            new ParticipacaoSocietaria(_socio1, 1000m, DateTime.Today, DateTime.Today.AddDays(-1))
        };

        var ex = Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, 1000m),$"O sócio '{_socio1.Nome}' possui data de saída anterior à data de entrada.");
        StringAssert.Contains("Sócio 1", ex.Message);
    }

    [Test]
    public void ValidarParticipacoes_ComParametrosValidos_NaoDeveLancarException()
    {
        var participacoes = new List<ParticipacaoSocietaria>
        {
            new ParticipacaoSocietaria(_socio1, 500m, DateTime.Today),
            new ParticipacaoSocietaria(_socio2, 500m, DateTime.Today)
        };

        Assert.DoesNotThrow(() => _service.ValidarParticipacoes(participacoes, 1000m));
    }
}
