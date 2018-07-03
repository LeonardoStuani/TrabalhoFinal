using AluguelCarros.Comuns.Testes.Base;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Clientes;
using AluguelCarros.Infra.Dados.Contexto;
using AluguelCarros.Infra.Dados.Features.Clientes;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Testes.Features.Clientes
{
    [TestFixture]
    public class ClienteInfraDadosTestes
    {
        Cliente _cliente;
        private AluguelCarrosContexto _context;
        private IClienteRepositorio _repositorio;

        [SetUp]
        public void SetUp()
        {
            _context = new AluguelCarrosContexto();
            _repositorio = new ClienteRepositorio();
            _cliente = ConstrutorObjeto.ObterCliente();

            Database.SetInitializer(new InicializadorBanco<AluguelCarrosContexto>());
            _context.Database.Initialize(true);
        }

        [Test]
        public void InfraDados_Cliente_Add_EsperadoOK()
        {
            var cliente = _repositorio.Adicionar(_cliente);

            var clienteBuscado = _context.Cliente.Find(cliente.Id);

            cliente.Id.Should().Be(clienteBuscado.Id);
        }

        [Test]
        public void InfraDados_Cliente_Delete_EsperadoOK()
        {
            var enderecoInserido = _repositorio.Adicionar(_cliente);

            _repositorio.Deletar(enderecoInserido);

            var deletado = _context.Cliente.Find(enderecoInserido.Id);

            deletado.Should().BeNull();
        }

        [Test]
        public void InfraDados_Cliente_Editar_EsperadoOK()
        {
            _cliente = _context.Cliente.Find(1);
            _cliente.NomeCompleto = "EDITADO";

            _repositorio.Editar(_cliente);

            var cliente = _context.Cliente.Find(_cliente.Id);

            cliente.NomeCompleto.Should().Be(_cliente.NomeCompleto);
        }

        [Test]
        public void InfraDados_Cliente_Buscar_EsperadoOK()
        {
            _cliente.Id = 1;

            var cliente = _repositorio.BuscarPor(_cliente.Id);

            cliente.Should().NotBeNull();
            cliente.Id.Should().Be(_cliente.Id);
        }

        [Test]
        public void InfraDados_Cliente_BuscarPorEndereco_EsperadoVerdadeiro()
        {
            _cliente.Endereco.Id = 1;

            var cliente = _repositorio.BuscarPorEndereco(_cliente.Endereco.Id);

            cliente.Should().BeTrue();
        }

        [Test]
        public void InfraDados_Cliente_BuscarPorEndereco_EsperadoFalso()
        {
            _cliente.Endereco.Id = 1456;

            var cliente = _repositorio.BuscarPorEndereco(_cliente.Endereco.Id);

            cliente.Should().BeFalse();
        }

        [Test]
        public void InfraDados_Cliente_BuscarTodos_EsperadoOK()
        {
            var lista = _repositorio.BuscarTudo();

            lista.First().Id.Should().Be(1);
            lista.Should().Should().NotBeNull();
            lista.Count.Should().BeGreaterThan(0);
        }
    }
}
