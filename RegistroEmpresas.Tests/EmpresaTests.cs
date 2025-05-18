using NUnit.Framework;
using RegistroEmpresas.Domain.Entities;
using RegistroEmpresas.Domain.RegistroEmpresarial.Entities;
using RegistroEmpresas.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class PessoaJuridicaTests
{
    private PessoaFisica socio1;
    private ParticipacaoSocietaria participacao1;

    [SetUp]
    public void Setup()
    {
        var endereco = new Endereco("12345-678", "Rua A", "10", "Centro", "CidadeX", "UF");
        socio1 = new PessoaFisica(1, "Maria", "11111111111",  endereco, new DateTime(1985, 5, 5));
        participacao1 = new ParticipacaoSocietaria(socio1, 5000m, DateTime.Today);
    }

    [Test]
    public void CriarPessoaJuridica_ComParametrosValidos_DeveCriarObjeto()
    {
        var endereco = new Endereco("12345-678", "Rua A", "10", "Centro", "CidadeX", "UF");
        var PessoaJuridica = new PessoaJuridica(1, "PessoaJuridicaX", endereco, "12345678000199", "FantasiaX", "desenvolvimento de aplicativos", 1, 10000m);

        Assert.AreEqual("PessoaJuridicaX", PessoaJuridica.Nome);
        Assert.AreEqual("12345678000199", PessoaJuridica.Cnpj);
        Assert.AreEqual("FantasiaX", PessoaJuridica.NomeFantasia);
        Assert.AreEqual(1, PessoaJuridica.CodigoNaturezaJuridica);
        Assert.AreEqual(10000m, PessoaJuridica.CapitalSocial);
        Assert.AreEqual(endereco, PessoaJuridica.Endereco);
        Assert.IsEmpty(PessoaJuridica.QuadroSocietario);
    }

    [Test]
    public void CriarPessoaJuridica_ComCnpjNuloOuVazio_DeveLancarException()
    {
        var endereco = new Endereco("12345-678", "Rua A", "10", "Centro", "CidadeX", "UF");

        Assert.Throws<ArgumentException>(() =>
            new PessoaJuridica(1, "PessoaJuridicaX", endereco, null, "FantasiaX","desenvolvimento de aplicativos", 1, 10000m), "CNPJ é obrigatório");

    }

    [Test]
    public void CriarPessoaJuridica_ComCapitalSocialZeroOuMenor_DeveLancarException()
    {
        var endereco = new Endereco("12345-678", "Rua A", "10", "Centro", "CidadeX", "UF");

        Assert.Throws<ArgumentException>(() =>
            new PessoaJuridica(1, "PessoaJuridicaX", endereco, "12345678000199", "FantasiaX", "desenvolvimento de aplicativos", 1, 0m),"Capital social deve ser maior que zero.");

        Assert.Throws<ArgumentException>(() =>
            new PessoaJuridica(1, "PessoaJuridicaX", endereco, "12345678000199", "FantasiaX", "desenvolvimento de aplicativos", 1, -100m), "Capital social deve ser maior que zero.");
    }

    [Test]
    public void AdicionarSocio_AdicionaParticipacaoAoQuadro()
    {
        var endereco = new Endereco("12345-678", "Rua A", "10", "Centro", "CidadeX", "UF");
        var PessoaJuridica = new PessoaJuridica(1, "PessoaJuridicaX", endereco, "12345678000199", "FantasiaX", "desenvolvimento de aplicativos", 1, 10000m);

        PessoaJuridica.AdicionarSocio(participacao1);

        Assert.AreEqual(1, PessoaJuridica.QuadroSocietario.Count);
        Assert.AreEqual(participacao1, PessoaJuridica.QuadroSocietario.First());
    }

    [Test]
    public void RemoverSocio_RemoveParticipacaoDoQuadro()
    {
        var endereco = new Endereco("12345-678", "Rua A", "10", "Centro", "CidadeX", "UF");
        var PessoaJuridica = new PessoaJuridica(1, "PessoaJuridicaX", endereco, "12345678000199", "FantasiaX", "desenvolvimento de aplicativos", 1, 10000m);

        PessoaJuridica.AdicionarSocio(participacao1);
        PessoaJuridica.RemoverSocio(socio1);

        Assert.IsEmpty(PessoaJuridica.QuadroSocietario);
    }

    [Test]
    public void TotalParticipacoes_RetornaSomaDosValores()
    {
        var endereco = new Endereco("12345-678", "Rua A", "10", "Centro", "CidadeX", "UF");
        var PessoaJuridica = new PessoaJuridica(1, "PessoaJuridicaX", endereco, "12345678000199", "FantasiaX", "desenvolvimento de aplicativos", 1, 10000m);

        var socio2 = new PessoaFisica(2, "João",  "22222222222", endereco, new DateTime(1990, 1, 1));
        var participacao2 = new ParticipacaoSocietaria(socio2, 2500m, DateTime.Today);

        PessoaJuridica.AdicionarSocio(participacao1);
        PessoaJuridica.AdicionarSocio(participacao2);

        Assert.AreEqual(7500m, PessoaJuridica.TotalParticipacoes());
    }
}
