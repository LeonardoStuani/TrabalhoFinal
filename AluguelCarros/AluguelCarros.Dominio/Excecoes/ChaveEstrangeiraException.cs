using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Excecoes
{
    public class ChaveEstrangeiraException : DominioException
    {
        public ChaveEstrangeiraException() : base("Este registro não pode ser apagado pois possui dependências")
        {
        }
    }
}
