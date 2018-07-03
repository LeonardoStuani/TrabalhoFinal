using System;
using System.Collections.Generic;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Features.Clientes;

namespace AluguelCarros.Aplicacao.Featues.Clientes
{
    public class ClienteServico : IClienteServico
    {
        private IClienteRepositorio _clienteRepositorio;
        private IAluguelRepositorio _aluguelRepositorio;

        public ClienteServico(IClienteRepositorio clienteRepositorio, IAluguelRepositorio aluguelRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _aluguelRepositorio = aluguelRepositorio;
        }
        public Cliente Adicionar(Cliente entidade)
        {
            entidade.Validar();

            return _clienteRepositorio.Adicionar(entidade);
        }

        public Cliente BuscarPor(long id)
        {
            if (id < 1)
                throw new IdentificadorInvalido();

            return _clienteRepositorio.BuscarPor(id);
        }

        public List<Cliente> BuscarTudo()
        {
            return _clienteRepositorio.BuscarTudo();
        }

        public void Deletar(Cliente entidade)
        {
            var aluguel = _aluguelRepositorio.BuscarPorCliente(entidade.Id);

            if (aluguel == true)
                throw new ChaveEstrangeiraException();

            _clienteRepositorio.Deletar(entidade);
        }

        public void Editar(Cliente entidade)
        {
            entidade.Validar();

            _clienteRepositorio.Editar(entidade);
        }
    }
}
