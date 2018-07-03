using AluguelCarros.Dominio.Contratos.Base;
using AluguelCarros.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Features.Carros
{
    public class Carro : Entidade
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double PrecoPorHora { get; set; }

        public override void Validar()
        {
            if (String.IsNullOrEmpty(Placa))
                throw new DominioException("Placa inválida");
            if (String.IsNullOrEmpty(Marca))
                throw new DominioException("Marca inválida");
            if (String.IsNullOrEmpty(Modelo))
                throw new DominioException("Modelo inválido");
            if (Ano < 1950)
                throw new DominioException("Ano inválido");
            if (Ano > DateTime.Now.Year)
                throw new DominioException("Ano inválido");
            if (PrecoPorHora < 1)
                throw new DominioException("Preco Por Hora inválido");
        }

        public override string ToString()
        {
            return string.Format("{0}", Placa);
        }
    }
}
