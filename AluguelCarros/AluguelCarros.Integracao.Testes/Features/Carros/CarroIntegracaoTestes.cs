using AluguelCarros.Aplicacao.Featues.Carros;
using AluguelCarros.Comuns.Testes.Base;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Carros;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Carros;
using AluguelCarros.Infra.Dados.Contexto;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using AluguelCarros.Infra.Dados.Features.Carros;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Integracao.Testes.Features.Carros
{
    [TestFixture]
    public class CarroIntegracaoTestes
    {

        Carro _carro;
        private AluguelCarrosContexto _context;
        private ICarroRepositorio _carroRepositorio;
        private ICarroServico _carroServico;
        private IAluguelRepositorio _aluguelRepositorio;

        [SetUp]
        public void SetUp()
        {
            _context = new AluguelCarrosContexto();
            _carroRepositorio = new CarroRepositorio();
            _aluguelRepositorio = new AluguelRepositorio();
            _carroServico = new CarroServico(_carroRepositorio, _aluguelRepositorio);
            _carro = ConstrutorObjeto.ObterCarro();

            Database.SetInitializer(new InicializadorBanco<AluguelCarrosContexto>());
            _context.Database.Initialize(true);
        }

        [Test]
        public void Integracao_Carro_Add_EsperadoOK()
        {
            var cliente = _carroServico.Adicionar(_carro);

            var carroBuscado = _carroServico.BuscarPor(cliente.Id);

            cliente.Id.Should().Be(carroBuscado.Id);
        }

        [Test]
        public void Integracao_Carro_Delete_EsperadoOK()
        {
            var carroInserido = _carroServico.Adicionar(_carro);

            _carroServico.Deletar(carroInserido);

            var deletado = _carroServico.BuscarPor(carroInserido.Id);

            deletado.Should().BeNull();
        }

        [Test]
        public void Integracao_Carro_Delete_EsperadoExcecao()
        {
            _carro.Id = 1;

            Action action =()=> _carroServico.Deletar(_carro);

            action.Should().Throw<ChaveEstrangeiraException>();
        }

        [Test]
        public void Integracao_Carro_Editar_EsperadoOK()
        {
            _carro = _carroServico.BuscarPor(1);
            _carro.Marca = "EDITADO";

            _carroServico.Editar(_carro);

            var carro = _carroServico.BuscarPor(_carro.Id);

            carro.Marca.Should().Be(_carro.Marca);
        }

        [Test]
        public void Integracao_Carro_Buscar_EsperadoOK()
        {
            _carro.Id = 1;

            var cliente = _carroServico.BuscarPor(_carro.Id);

            cliente.Should().NotBeNull();
            cliente.Id.Should().Be(_carro.Id);
        }

        [Test]
        public void Integracao_Carro_Buscar_EsperadoExcecao()
        {
            _carro.Id = 0;

            Action action = () => _carroServico.BuscarPor(_carro.Id);

            action.Should().Throw<IdentificadorInvalido>();
        }

        [Test]
        public void Integracao_Carro_BuscarTodos_EsperadoOK()
        {
            var lista = _carroServico.BuscarTudo();

            lista.First().Id.Should().Be(1);
            lista.Should().Should().NotBeNull();
            lista.Count.Should().BeGreaterThan(0);
        }
    }
}
