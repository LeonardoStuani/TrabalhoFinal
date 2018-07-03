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

namespace AluguelCarros.Infra.Dados.Testes.Features.Enderecos
{
    [TestFixture]
    public class EnderecoInfraDadosTestes
    {
        Endereco _endereco;
        private AluguelCarrosContexto _context;
        private IEnderecoRepositorio _repositorio;

        [SetUp]
        public void SetUp()
        {
            _context = new AluguelCarrosContexto();
            _repositorio = new EnderecoRepositorio();
            _endereco = ConstrutorObjeto.ObterEndereco();

            Database.SetInitializer(new InicializadorBanco<AluguelCarrosContexto>());
            _context.Database.Initialize(true);
        }

        [Test]
        public void InfraDados_Endereco_Add_EsperadoOK()
        {
            var endereco = _repositorio.Adicionar(_endereco);

            var enderecoBuscado = _context.Endereco.Find(endereco.Id);

            endereco.Id.Should().Be(enderecoBuscado.Id);
        }

        [Test]
        public void InfraDados_Endereco_Delete_EsperadoOK()
        {
            var enderecoInserido = _repositorio.Adicionar(_endereco);

            _repositorio.Deletar(enderecoInserido);

            var deletado = _context.Endereco.Find(enderecoInserido.Id);

            deletado.Should().BeNull();
        }

        [Test]
        public void InfraDados_Endereco_Editar_EsperadoOK()
        {
            _endereco = _context.Endereco.Find(1);
            _endereco.Rua = "EDITADO";

            _repositorio.Editar(_endereco);

            var endereco = _context.Endereco.Find(_endereco.Id);

            endereco.Rua.Should().Be(_endereco.Rua);
        }

        [Test]
        public void InfraDados_Endereco_Buscar_EsperadoOK()
        {
            _endereco.Id = 1;

            var endereco = _repositorio.BuscarPor(_endereco.Id);

            endereco.Should().NotBeNull();
            endereco.Id.Should().Be(_endereco.Id);
        }

        [Test]
        public void InfraDados_Endereco_BuscarTodos_EsperadoOK()
        {
            var lista = _repositorio.BuscarTudo();

            lista.First().Id.Should().Be(1);
            lista.Should().Should().NotBeNull();
            lista.Count.Should().BeGreaterThan(0);
        }
    }
}

