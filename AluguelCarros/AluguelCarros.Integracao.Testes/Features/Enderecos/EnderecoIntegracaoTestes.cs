using AluguelCarros.Dominio.Features.Enderecos;
using NUnit.Framework;
using AluguelCarros.Comuns.Testes.Base;
using System;
using FluentAssertions;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Infra.Dados.Contexto;
using AluguelCarros.Infra.Dados.Features.Enderecos;
using System.Data.Entity;
using System.Linq;
using AluguelCarros.Dominio.Contratos.Enderecos;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Aplicacao.Featues.Enderecos;
using AluguelCarros.Infra.Dados.Features.Clientes;

namespace AluguelCarros.Integracao.Dados.Testes.Features.Enderecos
{
    [TestFixture]
    public class EnderecoIntegracaoTestes
    {
        Endereco _endereco;
        private AluguelCarrosContexto _context;
        private IEnderecoRepositorio _enderecoRepositorio;
        private IClienteRepositorio _clienteRepositorio;
        private IEnderecoServico _enderecoServico;

        [SetUp]
        public void SetUp()
        {
            _context = new AluguelCarrosContexto();
            _enderecoRepositorio = new EnderecoRepositorio();
            _clienteRepositorio = new ClienteRepositorio();
            _enderecoServico = new EnderecoServico(_enderecoRepositorio, _clienteRepositorio);

            _endereco = ConstrutorObjeto.ObterEndereco();

            Database.SetInitializer(new InicializadorBanco<AluguelCarrosContexto>());
            _context.Database.Initialize(true);
        }

        [Test]
        public void Integracao_Endereco_Add_EsperadoOK()
        {
            var endereco = _enderecoServico.Adicionar(_endereco);

            var enderecoBuscado = _enderecoServico.BuscarPor(endereco.Id);

            endereco.Id.Should().Be(enderecoBuscado.Id);
        }

        [Test]
        public void Integracao_Endereco_Delete_EsperadoOK()
        {
            var enderecoInserido = _enderecoServico.Adicionar(_endereco);

            _enderecoServico.Deletar(enderecoInserido);

            var deletado = _enderecoServico.BuscarPor(enderecoInserido.Id);

            deletado.Should().BeNull();
        }

        [Test]
        public void Integracao_Endereco_Editar_EsperadoOK()
        {
            _endereco = _enderecoServico.BuscarPor(1);
            _endereco.Rua = "EDITADO";

            _enderecoServico.Editar(_endereco);

            var endereco = _enderecoServico.BuscarPor(_endereco.Id);

            endereco.Rua.Should().Be(_endereco.Rua);
        }

        [Test]
        public void Integracao_Endereco_Buscar_EsperadoOK()
        {
            _endereco.Id = 1;

            var endereco = _enderecoServico.BuscarPor(_endereco.Id);

            endereco.Should().NotBeNull();
            endereco.Id.Should().Be(_endereco.Id);
        }
        [Test]
        public void Integracao_Endereco_Buscar_EsperadoExcecao()
        {
            _endereco.Id = 0;

            Action action = () => _enderecoServico.BuscarPor(_endereco.Id);

            action.Should().Throw<IdentificadorInvalido>();
        }

        [Test]
        public void Integracao_Endereco_Delete_EsperadoExcecao()
        {
            _endereco.Id = 1;

            Action action = () => _enderecoServico.Deletar(_endereco);

            action.Should().Throw<ChaveEstrangeiraException>();
        }
        [Test]
        public void Integracao_Endereco_BuscarTodos_EsperadoOK()
        {
            var lista = _enderecoServico.BuscarTudo();

            lista.First().Id.Should().Be(1);
            lista.Should().Should().NotBeNull();
            lista.Count.Should().BeGreaterThan(0);
        }
    }
}

