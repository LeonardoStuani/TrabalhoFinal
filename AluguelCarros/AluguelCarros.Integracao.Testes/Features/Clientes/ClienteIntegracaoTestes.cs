using AluguelCarros.Aplicacao.Featues.Clientes;
using AluguelCarros.Comuns.Testes.Base;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Clientes;
using AluguelCarros.Infra.Dados.Contexto;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using AluguelCarros.Infra.Dados.Features.Clientes;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Integracao.Dados.Testes.Features.Clientes
{
    [TestFixture]
    public class ClienteIntegracaoTestes
    {
        Cliente _cliente;
        private AluguelCarrosContexto _context;
        private IAluguelRepositorio _aluguelRepositorio;
        private IClienteRepositorio _clienteRepositorio;
        private IClienteServico _clienteServico;

        [SetUp]
        public void SetUp()
        {
            _context = new AluguelCarrosContexto();
            _aluguelRepositorio = new AluguelRepositorio();
            _clienteRepositorio = new ClienteRepositorio();
            _clienteServico = new ClienteServico(_clienteRepositorio, _aluguelRepositorio);

            _cliente = ConstrutorObjeto.ObterCliente();

            Database.SetInitializer(new InicializadorBanco<AluguelCarrosContexto>());
            _context.Database.Initialize(true);
        }

        [Test]
        public void Integracao_Cliente_Add_EsperadoOK()
        {
            var cliente = _clienteServico.Adicionar(_cliente);

            var clienteBuscado = _clienteServico.BuscarPor(cliente.Id);

            cliente.Id.Should().Be(clienteBuscado.Id);
        }

        [Test]
        public void Integracao_Cliente_Delete_EsperadoOK()
        {
            var clienteInserido = _clienteServico.Adicionar(_cliente);

            _clienteServico.Deletar(clienteInserido);

            var deletado = _clienteServico.BuscarPor(clienteInserido.Id);

            deletado.Should().BeNull();
        }
        [Test]
        public void Integracao_Cliente_Delete_EsperadoExcecao()
        {
            _cliente.Id = 1;

            Action action = () => _clienteServico.Deletar(_cliente);

            action.Should().Throw<ChaveEstrangeiraException>();
        }

        [Test]
        public void Integracao_Cliente_Buscar_EsperadoExcecao()
        {
            _cliente.Id = 0;

            Action action = () => _clienteServico.BuscarPor(_cliente.Id);

            action.Should().Throw<IdentificadorInvalido>();
        }
        [Test]
        public void Integracao_Cliente_Editar_EsperadoOK()
        {
            _cliente = _clienteServico.BuscarPor(1);
            _cliente.CPF = "EDITADO";

            _clienteServico.Editar(_cliente);

            var cliente = _clienteServico.BuscarPor(_cliente.Id);

            cliente.CPF.Should().Be(_cliente.CPF);
        }

        [Test]
        public void Integracao_Cliente_Buscar_EsperadoOK()
        {
            _cliente.Id = 1;

            var endereco = _clienteServico.BuscarPor(_cliente.Id);

            endereco.Should().NotBeNull();
            endereco.Id.Should().Be(_cliente.Id);
        }

        [Test]
        public void Integracao_Cliente_BuscarTodos_EsperadoOK()
        {
            var lista = _clienteServico.BuscarTudo();

            lista.First().Id.Should().Be(1);
            lista.Should().Should().NotBeNull();
            lista.Count.Should().BeGreaterThan(0);
        }
    }
}
