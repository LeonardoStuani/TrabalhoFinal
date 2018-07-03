using AluguelCarros.Dominio.Features.Carros;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Contexto.Configuracoes
{
    public class CarroConfiguracao : EntityTypeConfiguration<Carro>
    {
        public CarroConfiguracao()
        {
            ToTable("Carro");
            HasKey(c => c.Id);

            Property(c => c.Marca).HasMaxLength(50).IsRequired();
            Property(c => c.Modelo).HasMaxLength(50).IsRequired();
            Property(c => c.Placa).HasMaxLength(50).IsRequired();
            Property(c => c.Ano).HasColumnType("int").IsRequired();
            //Property(c => c.PrecoPorHora).HasColumnType("decimal(18, 2)").IsRequired();
        }
    }
}
