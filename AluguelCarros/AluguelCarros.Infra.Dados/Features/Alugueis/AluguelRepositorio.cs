using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Features.Alugueis;
using AluguelCarros.Infra.Dados.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Features.Alugueis
{
    public class AluguelRepositorio : IAluguelRepositorio
    {
        public AluguelCarrosContexto _contexto;

        public AluguelRepositorio()
        {
            _contexto = new AluguelCarrosContexto();
        }

        public Aluguel Adicionar(Aluguel entidade)
        {
           entidade = _contexto.Aluguel.Add(entidade);

           _contexto.SaveChanges();

            return entidade;
        }

        public Aluguel BuscarPor(long id)
        {
            var aluguel = (from a in _contexto.Aluguel
                           where a.Id == id
                           select a)
                           .Include("Cliente")
                           .Include("Carro")
                           .FirstOrDefault();

            return aluguel;
        }

        public bool BuscarPorCarro(long carroID)
        {
            var aluguel = (from e in _contexto.Aluguel
                           where e.Carro.Id == carroID
                           select e)
                         .FirstOrDefault();

            if (aluguel == null)
                return false;

            return true;
        }

        public bool BuscarPorCliente(long clienteID)
        {
            var aluguel = (from e in _contexto.Aluguel
                           where e.Cliente.Id == clienteID
                           select e)
                          .FirstOrDefault();

            if (aluguel == null)
                return false;

            return true;
        }

        public List<Aluguel> BuscarTudo()
        {
            var alugueis = (from a in _contexto.Aluguel
                           select a)
                          .Include("Cliente")
                          .Include("Carro")
                          .ToList();

            return alugueis;
        }

        public void Deletar(Aluguel entidade)
        {
            _contexto.Aluguel.Remove(entidade);

            _contexto.SaveChanges();
        }

        public void Editar(Aluguel entidade)
        {
            _contexto.SaveChanges();
        }
    }
}
