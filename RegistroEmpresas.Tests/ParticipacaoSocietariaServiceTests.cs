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
        _socio1 = new PessoaFisica(1, "S�cio 1", "11111111111", endereco, new DateTime(1980, 1, 1));
        _socio2 = new PessoaFisica(2, "S�cio 2",  "22222222222", endereco,  new DateTime(1985, 1, 1));
    }

    [Test]
    public void ValidarParticipacoes_ComListaNula_DeveLancarException()
    {
        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(null, 1000m), "A empresa deve ter pelo menos um s�cio.");
    }

    [Test]
    public void ValidarParticipacoes_ComListaVazia_DeveLancarException()
    {
        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(new List<ParticipacaoSocietaria>(), 1000m), "A empresa deve ter pelo menos um s�cio.");
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

        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, 1000m),"N�o � permitido ter s�cios duplicados no quadro societ�rio.");
    }

    [Test]
    public void ValidarParticipacoes_ComParticipacaoZeroOuNegativa_DeveLancarException()
    {
        var participacoes = new List<ParticipacaoSocietaria>
        {
            new ParticipacaoSocietaria(_socio1, 0m, DateTime.Today),
            new ParticipacaoSocietaria(_socio2, 1000m, DateTime.Today)
        };

        var ex = Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, 1000m), $"O s�cio '{_socio1.Nome}' possui valor de participa��o inv�lido.");
        StringAssert.Contains("S�cio 1", ex.Message);
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
        Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, 100m), $"A soma das participa��es dos s�cios ({somaParticipacoes:C}) deve ser igual ao capital social da empresa ({100m:C}).");
    }

    [Test]
    public void ValidarParticipacoes_ComDataSaidaAnteriorDataEntrada_DeveLancarException()
    {
        var participacoes = new List<ParticipacaoSocietaria>
        {
            new ParticipacaoSocietaria(_socio1, 1000m, DateTime.Today, DateTime.Today.AddDays(-1))
        };

        var ex = Assert.Throws<InvalidOperationException>(() => _service.ValidarParticipacoes(participacoes, 1000m),$"O s�cio '{_socio1.Nome}' possui data de sa�da anterior � data de entrada.");
        StringAssert.Contains("S�cio 1", ex.Message);
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
