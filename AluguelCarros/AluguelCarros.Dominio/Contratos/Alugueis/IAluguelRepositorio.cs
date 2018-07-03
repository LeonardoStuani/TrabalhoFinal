using AluguelCarros.Dominio.Contratos.Base;
using AluguelCarros.Dominio.Features.Alugueis;
using AluguelCarros.Dominio.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Contratos.Alugueis
{
    public interface IAluguelRepositorio : IRepositorio<Aluguel>
    {
        bool BuscarPorCliente(long clienteID);
        bool BuscarPorCarro(long carroID);
    }
}
