using AluguelCarros.Dominio.Contratos.Enderecos;
using AluguelCarros.Dominio.Features.Enderecos;
using AluguelCarros.Infra.Dados.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Features.Enderecos
{
    public class EnderecoRepositorio : IEnderecoRepositorio
    {
        public AluguelCarrosContexto _contexto;
        public EnderecoRepositorio()
        {
            _contexto = new AluguelCarrosContexto();
        }

        public Endereco Adicionar(Endereco entidade)
        {
            entidade = _contexto.Endereco.Add(entidade);

            _contexto.SaveChanges();

            return entidade;
        }

        public Endereco BuscarPor(long id)
        {
            var endereco = _contexto.Endereco
                 .Where(p => p.Id == id)
                 .FirstOrDefault();

            return endereco;
        }

        public List<Endereco> BuscarTudo()
        {
            return _contexto.Endereco.ToList();
        }

        public void Deletar(Endereco entidade)
        {
            _contexto.Endereco.Remove(entidade);

            _contexto.SaveChanges();
        }

        public void Editar(Endereco entidade)
        {
            _contexto.SaveChanges();
        }
    }
}
