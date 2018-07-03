using AluguelCarros.Dominio.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Contexto.Configuracoes
{
    public class EnderecoConfiguracao : EntityTypeConfiguration<Endereco>
    {
        public EnderecoConfiguracao()
        {
            ToTable("Endereco");

            HasKey(e => e.Id);

            Property(e => e.Numero).HasColumnType("int").IsRequired();
            Property(e => e.Rua).HasMaxLength(150).IsRequired();
            Property(e => e.Cep).HasMaxLength(10).IsRequired();
            Property(e => e.Estado).HasMaxLength(2).IsRequired();
            Property(e => e.Bairro).HasMaxLength(150).IsRequired();
            Property(e => e.Cidade).HasMaxLength(150).IsRequired();
        }
    }
}
