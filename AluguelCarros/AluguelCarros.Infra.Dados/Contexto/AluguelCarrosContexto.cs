using AluguelCarros.Dominio.Features.Alugueis;
using AluguelCarros.Dominio.Features.Carros;
using AluguelCarros.Dominio.Features.Clientes;
using AluguelCarros.Dominio.Features.Enderecos;
using AluguelCarros.Infra.Dados.Contexto.Configuracoes;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluguelCarros.Infra.Dados.Contexto
{
    public class AluguelCarrosContexto : DbContext
    {
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Carro> Carro { get; set; }
        public DbSet<Aluguel> Aluguel { get; set; }
        public AluguelCarrosContexto() : base("AluguelCarrosDB")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AluguelConfiguracao());
            modelBuilder.Configurations.Add(new CarroConfiguracao());
            modelBuilder.Configurations.Add(new ClienteConfiguracao());
            modelBuilder.Configurations.Add(new EnderecoConfiguracao());
            base.OnModelCreating(modelBuilder);
        }
    }
}
