using AluguelCarros.Dominio.Features.Enderecos;
using NUnit.Framework;
using AluguelCarros.Comuns.Testes.Base;
using System;
using FluentAssertions;
using AluguelCarros.Dominio.Excecoes;

namespace AluguelCarros.Dominio.Testes.Features.Enderecos
{
    [TestFixture]
    public class EnderecoDominioTestes
    {
        Endereco _endereco;

        [SetUp]
        public void SetUp()
        {
            _endereco = ConstrutorObjeto.ObterEndereco();
        }

        [Test]
        public void Dominio_Endereco_Validar_EsperadoOK()
        {
            Action action = () => _endereco.Validar();

            action.Should().NotThrow();
        }

        [Test]
        public void Dominio_Endereco_Validar_CEPVazio_EsperadoExcecao()
        {
            _endereco.Cep = string.Empty;

            Action action = () => _endereco.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Endereco_Validar_CEPInvalido_EsperadoExcecao()
        {
            _endereco.Cep = "132";

            Action action = () => _endereco.Validar();

            action.Should().Throw<DominioException>();
        }
        [Test]
        public void Dominio_Endereco_Validar_RuaInvalida_EsperadoExcecao()
        {
            _endereco.Rua = string.Empty;

            Action action = () => _endereco.Validar();

            action.Should().Throw<DominioException>();
        }
        [Test]
        public void Dominio_Endereco_Validar_BairroInvalido_EsperadoExcecao()
        {
            _endereco.Bairro = string.Empty;

            Action action = () => _endereco.Validar();

            action.Should().Throw<DominioException>();
        }
        [Test]
        public void Dominio_Endereco_Validar_CidadeInvalida_EsperadoExcecao()
        {
            _endereco.Cidade = string.Empty;

            Action action = () => _endereco.Validar();

            action.Should().Throw<DominioException>();
        }
        [Test]
        public void Dominio_Endereco_Validar_NumeroInvalido_EsperadoExcecao()
        {
            _endereco.Numero = 0;

            Action action = () => _endereco.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Endereco_Validar_EstadoInvalido_EsperadoExcecao()
        {
            _endereco.Estado = "s";

            Action action = () => _endereco.Validar();

            action.Should().Throw<DominioException>();
        }
        [Test]
        public void Dominio_Endereco_Validar_EstadoVazio_EsperadoExcecao()
        {
            _endereco.Estado = string.Empty;

            Action action = () => _endereco.Validar();

            action.Should().Throw<DominioException>();
        }
    }
}

