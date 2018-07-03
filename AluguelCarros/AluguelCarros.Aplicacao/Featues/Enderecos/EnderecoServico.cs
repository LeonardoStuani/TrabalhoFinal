using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Contratos.Enderecos;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Enderecos;

namespace AluguelCarros.Aplicacao.Featues.Enderecos
{
    public class EnderecoServico : IEnderecoServico
    {
        private IEnderecoRepositorio _enderecoRepositorio;

        private IClienteRepositorio _clienteRepositorio;

        public EnderecoServico(IEnderecoRepositorio enderecoRepositorio, IClienteRepositorio clienteRepositorio)
        {
            _enderecoRepositorio = enderecoRepositorio;
            _clienteRepositorio = clienteRepositorio;
        }
        public Endereco Adicionar(Endereco entidade)
        {
            entidade.Validar();

            return _enderecoRepositorio.Adicionar(entidade);
        }

        public Endereco BuscarPor(long id)
        {
            if (id < 1)
                throw new IdentificadorInvalido();

            return _enderecoRepositorio.BuscarPor(id);
        }

        public List<Endereco> BuscarTudo()
        {
            return _enderecoRepositorio.BuscarTudo();
        }

        public void Deletar(Endereco entidade)
        {
            var clienteDependecia = _clienteRepositorio.BuscarPorEndereco(entidade.Id);

            if (clienteDependecia == true)
                throw new ChaveEstrangeiraException();

            _enderecoRepositorio.Deletar(entidade);
        }

        public void Editar(Endereco entidade)
        {
            _enderecoRepositorio.Editar(entidade);
        }
    }
}
