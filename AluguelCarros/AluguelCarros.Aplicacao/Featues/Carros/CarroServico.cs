using System;
using System.Collections.Generic;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Carros;
using AluguelCarros.Dominio.Features.Carros;

namespace AluguelCarros.Aplicacao.Featues.Carros
{
    public class CarroServico : ICarroServico
    {
        private IAluguelRepositorio _aluguelRepositorio;
        private ICarroRepositorio _carroRepositorio;

        public CarroServico(ICarroRepositorio carroRepositorio, IAluguelRepositorio aluguelRepositorio)
        {
            _carroRepositorio = carroRepositorio;
            _aluguelRepositorio = aluguelRepositorio;
        }
        public Carro Adicionar(Carro entidade)
        {
            entidade.Validar();

            return _carroRepositorio.Adicionar(entidade);
        }

        public Carro BuscarPor(long id)
        {
            if (id < 1)
                throw new IdentificadorInvalido();

            return _carroRepositorio.BuscarPor(id);
        }

        public List<Carro> BuscarTudo()
        {
            return _carroRepositorio.BuscarTudo();
        }

        public void Deletar(Carro entidade)
        {
            var aluguel = _aluguelRepositorio.BuscarPorCarro(entidade.Id);

            if (aluguel == true)
                throw new ChaveEstrangeiraException();

            _carroRepositorio.Deletar(entidade);
        }

        public void Editar(Carro entidade)
        {
            entidade.Validar();

            _carroRepositorio.Editar(entidade);
        }
    }
}
