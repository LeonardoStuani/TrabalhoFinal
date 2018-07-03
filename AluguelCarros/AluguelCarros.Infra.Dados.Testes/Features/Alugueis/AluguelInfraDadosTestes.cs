using AluguelCarros.Comuns.Testes.Base;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Enums;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Alugueis;
using AluguelCarros.Infra.Dados.Contexto;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Testes.Features.Alugueis
{
    [TestFixture]
    public class AluguelInfraDadosTestes
    {

        Aluguel _aluguel;
        private AluguelCarrosContexto _context;
        private IAluguelRepositorio _repositorio;

        [SetUp]
        public void SetUp()
        {
            _context = new AluguelCarrosContexto();
            _repositorio = new AluguelRepositorio();
            _aluguel = ConstrutorObjeto.ObterAluguel();

            Database.SetInitializer(new InicializadorBanco<AluguelCarrosContexto>());
            _context.Database.Initialize(true);
        }

        [Test]
        public void InfraDados_Aluguel_Add_EsperadoOK()
        {
            var aluguel = _repositorio.Adicionar(_aluguel);

            var aluguelBuscado = _context.Aluguel.Find(aluguel.Id);

            aluguel.Id.Should().Be(aluguelBuscado.Id);
        }

        [Test]
        public void InfraDados_Aluguel_Delete_EsperadoOK()
        {
            var aluguelInserido = _repositorio.Adicionar(_aluguel);

            _repositorio.Deletar(aluguelInserido);

            var deletado = _context.Aluguel.Find(aluguelInserido.Id);

            deletado.Should().BeNull();
        }

        [Test]
        public void InfraDados_Aluguel_Editar_EsperadoOK()
        {
            _aluguel = _context.Aluguel.Find(1);
            _aluguel.HorasTotal = 15.99;

            _repositorio.Editar(_aluguel);

            var aluguel = _context.Aluguel.Find(_aluguel.Id);

            aluguel.HorasTotal.Should().Be(_aluguel.HorasTotal);
        }

        [Test]
        public void InfraDados_Aluguel_Buscar_EsperadoOK()
        {
            _aluguel.Id = 1;

            var aluguel = _repositorio.BuscarPor(_aluguel.Id);

            aluguel.Should().NotBeNull();
            aluguel.Id.Should().Be(_aluguel.Id);
        }

        [Test]
        public void InfraDados_Aluguel_BuscarPorCarro_EsperadoVerdadeiro()
        {
            _aluguel.Carro.Id = 1;

            var aluguel = _repositorio.BuscarPorCarro(_aluguel.Carro.Id);

            aluguel.Should().BeTrue();
        }

        [Test]
        public void InfraDados_Aluguel_BuscarPorCarro_EsperadoFalso()
        {
            _aluguel.Carro.Id = 123;

            var aluguel = _repositorio.BuscarPorCarro(_aluguel.Carro.Id);

            aluguel.Should().BeFalse();
        }

        [Test]
        public void InfraDados_Aluguel_BuscarPorCliente_EsperadoVerdadeiro()
        {
            _aluguel.Cliente.Id = 1;

            var aluguel = _repositorio.BuscarPorCliente(_aluguel.Cliente.Id);

            aluguel.Should().BeTrue();
        }

        [Test]
        public void InfraDados_Aluguel_BuscarPorCliente_EsperadoFalso()
        {
            _aluguel.Cliente.Id = 231;

            var aluguel = _repositorio.BuscarPorCliente(_aluguel.Cliente.Id);

            aluguel.Should().BeFalse();
        }
        [Test]
        public void InfraDados_Aluguel_BuscarTodos_EsperadoOK()
        {
            var lista = _repositorio.BuscarTudo();

            lista.First().Id.Should().Be(1);
            lista.Should().Should().NotBeNull();
            lista.Count.Should().BeGreaterThan(0);
        }

    }
}
