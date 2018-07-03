using AluguelCarros.Comuns.Testes.Base;
using AluguelCarros.Dominio.Contratos.Carros;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Carros;
using AluguelCarros.Infra.Dados.Contexto;
using AluguelCarros.Infra.Dados.Features.Carros;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Testes.Features.Carros
{
    [TestFixture]
    public class CarroInfraDataTestes
    {

        Carro _carro;
        private AluguelCarrosContexto _context;
        private ICarroRepositorio _repositorio;

        [SetUp]
        public void SetUp()
        {
            _context = new AluguelCarrosContexto();
            _repositorio = new CarroRepositorio();
            _carro = ConstrutorObjeto.ObterCarro();

            Database.SetInitializer(new InicializadorBanco<AluguelCarrosContexto>());
            _context.Database.Initialize(true);
        }

        [Test]
        public void InfraDados_Carro_Add_EsperadoOK()
        {
            var cliente = _repositorio.Adicionar(_carro);

            var carroBuscado = _context.Carro.Find(cliente.Id);

            cliente.Id.Should().Be(carroBuscado.Id);
        }

        [Test]
        public void InfraDados_Carro_Delete_EsperadoOK()
        {
            var carroInserido = _repositorio.Adicionar(_carro);

            _repositorio.Deletar(carroInserido);

            var deletado = _context.Carro.Find(carroInserido.Id);

            deletado.Should().BeNull();
        }

        [Test]
        public void InfraDados_Carro_Editar_EsperadoOK()
        {
            _carro = _context.Carro.Find(1);
            _carro.Marca = "EDITADO";

            _repositorio.Editar(_carro);

            var marca = _context.Carro.Find(_carro.Id);

            marca.Marca.Should().Be(_carro.Marca);
        }

        [Test]
        public void InfraDados_Carro_Buscar_EsperadoOK()
        {
            _carro.Id = 1;

            var cliente = _repositorio.BuscarPor(_carro.Id);

            cliente.Should().NotBeNull();
            cliente.Id.Should().Be(_carro.Id);
        }

        [Test]
        public void InfraDados_Carro_BuscarTodos_EsperadoOK()
        {
            var lista = _repositorio.BuscarTudo();

            lista.First().Id.Should().Be(1);
            lista.Should().Should().NotBeNull();
            lista.Count.Should().BeGreaterThan(0);
        }
    }
}
