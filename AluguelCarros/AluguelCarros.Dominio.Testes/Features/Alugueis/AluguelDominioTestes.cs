using AluguelCarros.Comuns.Testes.Base;
using AluguelCarros.Dominio.Enums;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Alugueis;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Testes.Features.Alugueis
{
    [TestFixture]
    public class AluguelDominioTestes
    {
        Aluguel _aluguel;

        [SetUp]
        public void SetUp()
        {
            _aluguel = ConstrutorObjeto.ObterAluguel();
        }

        [Test]
        public void Dominio_Aluguel_Validar_EsperadoOK()
        {
            Action action = () => _aluguel.Validar();

            action.Should().NotThrow();
        }


        [Test]
        public void Dominio_Aluguel_Validar_CarroInvalido_EsperadoExcecao()
        {
            _aluguel.Carro= null;

            Action action = () => _aluguel.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Aluguel_Validar_ClienteInvalido_EsperadoExcecao()
        {
            _aluguel.Cliente = null;

            Action action = () => _aluguel.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Aluguel_Validar_DataDevolucaoInvalida_EsperadoExcecao()
        {
            _aluguel.DataDevolucao =DateTime.Now.AddDays(-1);
            _aluguel.DataEntrada = DateTime.Now.AddDays(1);

            Action action = () => _aluguel.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Aluguel_Validar_PagamentoInvalido_EsperadoExcecao()
        {
            _aluguel.Pagamento = EPagamento.NaoDefinido;

            Action action = () => _aluguel.Validar();

            action.Should().Throw<DominioException>();
        }
        [Test]
        public void Dominio_Aluguel_Validar_ValorTotalInvalido_EsperadoExcecao()
        {
            _aluguel.ValorTotal = 0;

            Action action = () => _aluguel.Validar();

            action.Should().Throw<DominioException>();
        }
        [Test]
        public void Dominio_Aluguel_Validar_HorasInvalido_EsperadoExcecao()
        {
            _aluguel.HorasTotal = 0;

            Action action = () => _aluguel.Validar();

            action.Should().Throw<DominioException>();
        }
        [Test]
        public void Dominio_Aluguel_Validar_StatusInvalido_EsperadoExcecao()
        {
            _aluguel.Status = EStatus.NaoDefinido;

            Action action = () => _aluguel.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Aluguel_CalcularValorTotal_EsperadoOK()
        {
            _aluguel.ValorTotal = 0;
            _aluguel.DataDevolucao = DateTime.Now;
            _aluguel.DataEntrada = DateTime.Now.AddDays(-2);

            _aluguel.CalcularValorTotal();

            _aluguel.ValorTotal.Should().BeGreaterThan(0);
        }

    }
}
