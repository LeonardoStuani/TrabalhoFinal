using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Contratos.Base
{
    public interface IRepositorio<T> where T : Entidade
    {
        T Adicionar(T entidade);

        void Editar(T entidade);

        T BuscarPor(long id);

        List<T> BuscarTudo();

        void Deletar(T entidade);
    }
}
