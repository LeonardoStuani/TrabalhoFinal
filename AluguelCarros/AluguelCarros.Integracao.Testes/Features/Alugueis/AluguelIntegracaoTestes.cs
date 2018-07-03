using AluguelCarros.Aplicacao.Featues.Alugueis;
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

namespace AluguelCarros.Integracao.Testes.Features.Alugueis
{
    [TestFixture]
    public class AluguelIntegracaoTestes
    {

        Aluguel _aluguel;
        private AluguelCarrosContexto _context;
        private IAluguelRepositorio _aluguelRepositorio;
        private IAluguelServico _aluguelServico;

        [SetUp]
        public void SetUp()
        {
            _context = new AluguelCarrosContexto();
            _aluguelRepositorio = new AluguelRepositorio();
            _aluguelServico = new AluguelServico(_aluguelRepositorio);
            _aluguel = ConstrutorObjeto.ObterAluguel();

            Database.SetInitializer(new InicializadorBanco<AluguelCarrosContexto>());
            _context.Database.Initialize(true);
        }

        [Test]
        public void Integracao_Aluguel_Add_EsperadoOK()
        {
            var aluguel = _aluguelServico.Adicionar(_aluguel);

            var aluguelBuscado = _aluguelServico.BuscarPor(aluguel.Id);

            aluguel.Id.Should().Be(aluguelBuscado.Id);
        }

        [Test]
        public void Integracao_Aluguel_Delete_EsperadoOK()
        {
            var aluguelInserido = _aluguelServico.Adicionar(_aluguel);

            _aluguelServico.Deletar(aluguelInserido);

            var deletado = _aluguelServico.BuscarPor(aluguelInserido.Id);

            deletado.Should().BeNull();
        }

        [Test]
        public void Integracao_Aluguel_Editar_EsperadoOK()
        {
            _aluguel = _aluguelServico.BuscarPor(1);
            _aluguel.HorasTotal = 15.99;

            _aluguelServico.Editar(_aluguel);

            var aluguel = _aluguelServico.BuscarPor(_aluguel.Id);

            aluguel.HorasTotal.Should().Be(_aluguel.HorasTotal);
        }

        [Test]
        public void Integracao_Aluguel_Buscar_EsperadoOK()
        {
            _aluguel.Id = 1;

            var aluguel = _aluguelServico.BuscarPor(_aluguel.Id);

            aluguel.Should().NotBeNull();
            aluguel.Id.Should().Be(_aluguel.Id);
        }

        [Test]
        public void Integracao_Aluguel_Buscar_EsperadoExcecao()
        {
            _aluguel.Id = 0;

            Action action =()=> _aluguelServico.BuscarPor(_aluguel.Id);

            action.Should().Throw<IdentificadorInvalido>();
        }

        [Test]
        public void Integracao_Aluguel_BuscarTodos_EsperadoOK()
        {
            var lista = _aluguelServico.BuscarTudo();

            lista.First().Id.Should().Be(1);
            lista.Should().Should().NotBeNull();
            lista.Count.Should().BeGreaterThan(0);
        }

    }
}
