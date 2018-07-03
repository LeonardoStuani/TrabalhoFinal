using AluguelCarros.Dominio.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Contexto.Configuracoes
{
    public class ClienteConfiguracao : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguracao()
        {
            ToTable("Cliente");

            HasKey(c => c.Id);

            Property(c => c.NomeCompleto).HasMaxLength(150).IsRequired();
            Property(c => c.CPF).HasMaxLength(20).IsRequired();
            Property(c => c.Telefone).HasMaxLength(20).IsRequired();
            Property(c => c.DataNascimento).HasColumnType("Date").IsRequired();

            HasRequired(c => c.Endereco);
        }
    }
}
