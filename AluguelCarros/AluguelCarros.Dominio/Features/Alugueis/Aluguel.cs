using AluguelCarros.Dominio.Contratos.Base;
using AluguelCarros.Dominio.Enums;
using AluguelCarros.Dominio.Excecoes;
using AluguelCarros.Dominio.Features.Carros;
using AluguelCarros.Dominio.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Dominio.Features.Alugueis
{
    public class Aluguel : Entidade
    {
        public Cliente Cliente { get; set; }
        public Carro Carro { get; set; }
        public double HorasTotal { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataDevolucao { get; set; }
        public double ValorTotal { get; set; }
        public EPagamento Pagamento { get; set; }

        public EStatus Status { get; set; }

        public void CalcularValorTotal()
        {
            HorasTotal = CalcularHorasTotais();
            ValorTotal = Carro.PrecoPorHora * HorasTotal;
        }

        public double CalcularHorasTotais()
        {
            TimeSpan horasTotais = DataDevolucao - DataEntrada;

            HorasTotal = horasTotais.TotalHours;

            return HorasTotal;
        }
        public override void Validar()
        {
            if (Cliente == null)
                throw new DominioException("Cliente inválido");

            if (Carro == null)
                throw new DominioException("Carro inválido");

            if (DataEntrada > DataDevolucao)
                throw new DominioException("DataEntrada inválido");

            if (HorasTotal < 0.01)
                throw new DominioException("HorasTotal inválido");

            if (ValorTotal < 0.01)
                throw new DominioException("ValorTotal inválido");

            if (Pagamento == EPagamento.NaoDefinido)
                throw new DominioException("Pagamento inválido");

            if(Status == EStatus.NaoDefinido)
                throw new DominioException("Status inválido");
        }

        public override string ToString()
        {
            return string.Format("{0}", Carro);
        }
    }
}
