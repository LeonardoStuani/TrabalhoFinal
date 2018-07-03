using AluguelCarros.Infra.Dados.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Comuns.Testes.Base
{
   public class InicializadorBanco<T> : DropCreateDatabaseAlways<AluguelCarrosContexto>
    {
        protected override void Seed(AluguelCarrosContexto context)
        {
            context.Aluguel.Add(ConstrutorObjeto.ObterAluguel());

            context.Carro.Add(ConstrutorObjeto.ObterCarro());

            context.Cliente.Add(ConstrutorObjeto.ObterCliente());

            context.Endereco.Add(ConstrutorObjeto.ObterEndereco());

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
