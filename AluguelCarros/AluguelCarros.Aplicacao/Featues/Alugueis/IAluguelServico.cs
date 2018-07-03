using AluguelCarros.Dominio.Contratos.Base;
using AluguelCarros.Dominio.Features.Alugueis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Aplicacao.Featues.Alugueis
{
    public interface IAluguelServico : IServico<Aluguel>
    {
        void FecharAluguel(Aluguel aluguel);
    }
}
