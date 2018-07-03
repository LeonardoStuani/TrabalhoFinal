using AluguelCarros.Dominio.Contratos.Base;
using AluguelCarros.Dominio.Features.Clientes;
using AluguelCarros.Dominio.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Contratos.Clientes
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {
        bool BuscarPorEndereco(long enderecoID);
    }
}
