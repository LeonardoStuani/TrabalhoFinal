using AluguelCarros.Dominio.Features.Alugueis;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Contexto.Configuracoes
{
    public class AluguelConfiguracao : EntityTypeConfiguration<Aluguel>
    {
        public AluguelConfiguracao()
        {
            ToTable("Aluguel");

            HasKey(a => a.Id);

            Property(a => a.ValorTotal).HasColumnType("float").IsOptional();
            Property(a => a.Status).HasColumnType("int").IsOptional();
            Property(a => a.Pagamento).HasColumnType("int").IsOptional();
            Property(a => a.HorasTotal).HasColumnType("float").IsOptional();
            Property(a => a.DataDevolucao).HasColumnType("datetime").IsOptional();
            Property(a => a.DataEntrada).HasColumnType("datetime").IsOptional();

            HasRequired(a => a.Carro);
            HasRequired(a => a.Cliente);
        }
    }
}
