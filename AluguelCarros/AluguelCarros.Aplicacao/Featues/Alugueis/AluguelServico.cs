using System;
using System.Collections.Generic;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Alugueis;

namespace AluguelCarros.Aplicacao.Featues.Alugueis
{
    public class AluguelServico : IAluguelServico
    {
        private IAluguelRepositorio _aluguelRepositorio;
        public AluguelServico(IAluguelRepositorio aluguelRepositorio)
        {
            _aluguelRepositorio = aluguelRepositorio;
        }

        public Aluguel Adicionar(Aluguel entidade)
        {
            return _aluguelRepositorio.Adicionar(entidade);
        }

        public Aluguel BuscarPor(long id)
        {
            if (id < 1)
                throw new IdentificadorInvalido();

            return _aluguelRepositorio.BuscarPor(id);
        }

        public List<Aluguel> BuscarTudo()
        {
            return _aluguelRepositorio.BuscarTudo();
        }

        public void Deletar(Aluguel entidade)
        {
            _aluguelRepositorio.Deletar(entidade);
        }

        public void Editar(Aluguel entidade)
        {
            entidade.Validar();

            _aluguelRepositorio.Editar(entidade);
        }

        public void FecharAluguel(Aluguel aluguel)
        {
            aluguel.Validar();
            aluguel.CalcularValorTotal();

            _aluguelRepositorio.Editar(aluguel);
        }
    }
}
