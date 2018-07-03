using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Features.Clientes;
using AluguelCarros.Infra.Dados.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Features.Clientes
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        public AluguelCarrosContexto _contexto;

        public ClienteRepositorio()
        {
            _contexto = new AluguelCarrosContexto();
        }

        public Cliente Adicionar(Cliente entidade)
        {
            entidade = _contexto.Cliente.Add(entidade);

            _contexto.SaveChanges();

            return entidade;
        }

        public Cliente BuscarPor(long id)
        {
            var aluguel = (from a in _contexto.Cliente
                           where a.Id == id
                           select a)
                            .Include("Endereco")
                            .FirstOrDefault();
            return aluguel;
        }

        public bool BuscarPorEndereco(long enderecoID)
        {
            var cliente = (from e in _contexto.Cliente
                           where e.Endereco.Id == enderecoID
                           select e)
                           .FirstOrDefault();

            if (cliente == null)
                return false;

            return true;
        }

        public List<Cliente> BuscarTudo()
        {
            var alugueis = (from a in _contexto.Cliente
                           select a)
                            .Include("Endereco")
                           .ToList();
            return alugueis;
        }

        public void Deletar(Cliente entidade)
        {
            _contexto.Cliente.Remove(entidade);

            _contexto.SaveChanges();
        }

        public void Editar(Cliente entidade)
        {
            _contexto.SaveChanges();
        }
    }
}
