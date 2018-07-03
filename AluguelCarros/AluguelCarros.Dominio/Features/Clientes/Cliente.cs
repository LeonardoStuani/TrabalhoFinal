using AluguelCarros.Dominio.Contratos.Base;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Features.Clientes
{
    public class Cliente : Entidade
    {
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public Endereco Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public override void Validar()
        {

            if (String.IsNullOrWhiteSpace(NomeCompleto))
                throw new DominioException("Nome inválido!");

            if (String.IsNullOrWhiteSpace(Telefone))
                throw new DominioException("Telefone inválido!");

            if (DataNascimento > DateTime.Now)
                throw new DominioException("Data nascimento inválida!");

            if (Endereco == null)
                throw new DominioException("Endereço inválido!");

            if (String.IsNullOrEmpty(CPF))
                throw new DominioException("CPF inválido!");

        }
        public override string ToString()
        {
            return string.Format("{0}", NomeCompleto);
        }

    }
}