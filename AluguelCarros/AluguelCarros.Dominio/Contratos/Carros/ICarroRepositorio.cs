using AluguelCarros.Dominio.Contratos.Base;
using AluguelCarros.Dominio.Features.Carros;
using AluguelCarros.Dominio.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Contratos.Carros
{
    public interface ICarroRepositorio : IRepositorio<Carro>
    {
    }
}
