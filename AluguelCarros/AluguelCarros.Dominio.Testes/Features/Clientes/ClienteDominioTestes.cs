using AluguelCarros.Comuns.Testes.Base;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Clientes;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Testes.Features.Clientes
{
    [TestFixture]
    public class ClienteDominioTestes
    {
        Cliente _cliente;

        [SetUp]
        public void SetUp()
        {
            _cliente = ConstrutorObjeto.ObterCliente();
        }

        [Test]
        public void Dominio_Cliente_Validar_EsperadoOK()
        {
            Action action = () => _cliente.Validar();

            action.Should().NotThrow();
        }

        [Test]
        public void Dominio_Cliente_Validar_CPFInvalido_EsperadoExcecao()
        {
            _cliente.CPF = string.Empty;

            Action action = () => _cliente.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Cliente_Validar_EnderecoInvalido_EsperadoExcecao()
        {
            _cliente.Endereco = null;

            Action action = () => _cliente.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Cliente_Validar_NomeCompletoInvalido_EsperadoExcecao()
        {
            _cliente.NomeCompleto = string.Empty;

            Action action = () => _cliente.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Cliente_Validar_TelefoneInvalido_EsperadoExcecao()
        {
            _cliente.Telefone = string.Empty;

            Action action = () => _cliente.Validar();

            action.Should().Throw<DominioException>();
        }

        [Test]
        public void Dominio_Cliente_Validar_DataNascimentoInvalida_EsperadoExcecao()
        {
            _cliente.DataNascimento = DateTime.Now.AddDays(1) ;

            Action action = () => _cliente.Validar();

            action.Should().Throw<DominioException>();
        }
    }
}
