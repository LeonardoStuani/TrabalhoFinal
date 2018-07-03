using AluguelCarros.Dominio.Contratos.Base;
using AluguelCarros.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Features.Enderecos
{
    public class Endereco : Entidade
    {
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public int Numero { get; set; }

        public override void Validar()
        {

            if (string.IsNullOrEmpty(Cep))
                throw new DominioException("O número do CEP não pode ser vazio");

            if (Cep.Length < 8)
                throw new DominioException("O número do CEP informado é inválido");

            if (string.IsNullOrEmpty(Rua))
                throw new DominioException("A rua não pode ser vazio");


            if (string.IsNullOrEmpty(Cidade))
                throw new DominioException("A cidade não pode ser vazio");


            if (string.IsNullOrEmpty(Estado))
                throw new DominioException("O Estado não pode ser vazio");

            if (Estado.Length != 2)
                throw new DominioException("O Estado informado é inválido");

            if (string.IsNullOrEmpty(Bairro))
                throw new DominioException("O bairro não pode ser vazio");

            if (Numero < 1)
                throw new DominioException("O número não pode ser menor que 1");
        }

        public override string ToString()
        {
            return string.Format("{0}", Cep);
        }
    }
}
