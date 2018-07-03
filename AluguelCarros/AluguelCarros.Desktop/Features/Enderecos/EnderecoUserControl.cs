using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AluguelCarros.Dominio.Features.Enderecos;
using AluguelCarros.Dominio.Contratos.Enderecos;
using AluguelCarros.Aplicacao.Featues.Enderecos;
using AluguelCarros.Infra.Dados.Features.Enderecos;
using AluguelCarros.Dominio.Contratos.Clientes;

namespace AluguelCarros.Desktop.Features.Enderecos
{
    public partial class EnderecoUserControl : UserControl
    {
        private IEnderecoRepositorio enderecoRepositorio;
        private IClienteRepositorio clienteRepositorio;
        private IEnderecoServico enderecoServico;

        public EnderecoUserControl()
        {
            InitializeComponent();
            enderecoRepositorio = new EnderecoRepositorio();
            enderecoServico = new EnderecoServico(enderecoRepositorio, clienteRepositorio);
            dgEnderecos.AutoGenerateColumns = true;
            this.AtualizarLista();
        }

        public Endereco ObterSelecionado()
        {
            return dgEnderecos.CurrentRow.DataBoundItem as Endereco;
        }

        public void AtualizarLista()
        {
            dgEnderecos.DataSource = null;

            var lista = enderecoServico.BuscarTudo();

            if (lista == null) return;

            dgEnderecos.DataSource = lista;
        }
    }
}
