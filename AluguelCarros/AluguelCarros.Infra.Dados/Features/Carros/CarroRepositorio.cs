using AluguelCarros.Dominio.Contratos.Carros;
using AluguelCarros.Dominio.Features.Carros;
using AluguelCarros.Infra.Dados.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Features.Carros
{
    public class CarroRepositorio : ICarroRepositorio
    {
        public AluguelCarrosContexto _contexto;

        public CarroRepositorio()
        {
            _contexto = new AluguelCarrosContexto();
        }

        public Carro Adicionar(Carro entidade)
        {
            entidade =_contexto.Carro.Add(entidade);

            _contexto.SaveChanges();

            return entidade;
        }

        public Carro BuscarPor(long id)
        {
            return _contexto.Carro.Find(id);
        }

        public List<Carro> BuscarTudo()
        {
            return _contexto.Carro.ToList();
        }

        public void Deletar(Carro entidade)
        {
            _contexto.Carro.Remove(entidade);

            _contexto.SaveChanges();
        }

        public void Editar(Carro entidade)
        {
            _contexto.SaveChanges();
        }
    }
}
