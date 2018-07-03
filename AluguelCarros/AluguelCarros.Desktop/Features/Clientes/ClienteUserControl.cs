using AluguelCarros.Aplicacao.Featues.Clientes;
using AluguelCarros.Infra.Dados.Features.Clientes;
using AluguelCarros.Dominio.Features.Clientes;
using System.Windows.Forms;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Infra.Dados.Features.Alugueis;

namespace AluguelCarros.Desktop.Features.Clientes
{
    public partial class ClienteUserControl : UserControl
    {
        private IClienteRepositorio clienteRepositorio;
        private IClienteServico clienteServico;
        private IAluguelRepositorio aluguelRepositorio;
        public ClienteUserControl()
        {
            InitializeComponent();

            clienteRepositorio = new ClienteRepositorio();
            aluguelRepositorio = new AluguelRepositorio();
            clienteServico = new ClienteServico(clienteRepositorio, aluguelRepositorio);

            AtualizarLista();
        }

        public void AtualizarLista()
        {
            var lista = clienteServico.BuscarTudo();

            if (lista == null) return;

            dgClientes.DataSource = null;

            dgClientes.DataSource = lista;
            
        }

        public  Cliente ObterSelecionado()
        {
            return dgClientes.CurrentRow.DataBoundItem as Cliente;
        }
    }
}
