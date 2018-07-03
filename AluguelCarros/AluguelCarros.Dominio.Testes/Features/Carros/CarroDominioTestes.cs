using AluguelCarros.Comuns.Testes.Base;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Carros;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Testes.Features.Carros
{
    [TestFixture]
    public class CarroDominioTestes
    {
        Carro _carro;

        [SetUp]
        public void SetUp()
        {
            _carro = ConstrutorObjeto.ObterCarro();
        }

        [Test]
        public void Dominio_Carro_Validar_EsperadoOK()
        {
            Action action = () => _carro.Validar();

            action.Should().NotThrow();
        }
        

        [Test]
        public void Dominio_Carro_Validar_AnoInvalido_EsperadoExcecao()
        {
            _carro.Ano = 0;

            Action action = () => _carro.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Carro_Validar_AnoInvalido_MaiorQueDataAtual_EsperadoExcecao()
        {
            _carro.Ano = DateTime.Now.AddYears(1).Year;

            Action action = () => _carro.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Carro_Validar_MarcaInvalida_EsperadoExcecao()
        {
            _carro.Marca = string.Empty;

            Action action = () => _carro.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Carro_Validar_ModeloInvalido_EsperadoExcecao()
        {
            _carro.Modelo = string.Empty;

            Action action = () => _carro.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Carro_Validar_PlacaInvalida_EsperadoExcecao()
        {
            _carro.Placa = string.Empty;

            Action action = () => _carro.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Carro_Validar_PrecoPorHoraInvalido_EsperadoExcecao()
        {
            _carro.PrecoPorHora = 0;

            Action action = () => _carro.Validar();

            action.Should().Throw<DominioException>();
        }
    }
}
