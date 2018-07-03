using AluguelCarros.Aplicacao.Featues.Clientes;
using AluguelCarros.Aplicacao.Featues.Enderecos;
using AluguelCarros.Desktop.Features.Enderecos;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Contratos.Enderecos;
using AluguelCarros.Dominio.Features.Clientes;
using AluguelCarros.Dominio.Features.Enderecos;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using AluguelCarros.Infra.Dados.Features.Clientes;
using AluguelCarros.Infra.Dados.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelCarros.Desktop.Features.Clientes
{
    public partial class ClienteDialog : Form
    {
        Cliente _cliente;
        private IClienteRepositorio clienteRepositorio;
        private IClienteServico clienteServico;
        private IAluguelRepositorio aluguelRepositorio;
        private IEnderecoRepositorio enderecoRepositorio;
                private IEnderecoServico enderecoServico;
        public ClienteDialog(string titulo, Cliente cliente = null)
        {
            InitializeComponent();
            enderecoRepositorio = new EnderecoRepositorio();
            clienteRepositorio = new ClienteRepositorio();
            enderecoServico = new EnderecoServico(enderecoRepositorio, clienteRepositorio);
            aluguelRepositorio = new AluguelRepositorio();
            clienteServico = new ClienteServico(clienteRepositorio, aluguelRepositorio);

            _cliente = cliente;

            if (_cliente != null)
                InserirTextos();

            ListarEndereco();
        }

        private Cliente ConverterParaObjeto(long id)
        {
            Cliente cliente = null;

            if (id > 0)
                cliente = clienteServico.BuscarPor(id);
            else
                cliente = new Cliente();

            cliente.Id = id;
            cliente.NomeCompleto = txtNome.Text;
            cliente.Telefone = txtTelefone.Text;
            cliente.CPF = txtCpf.Text;
            cliente.DataNascimento = dtpDataNascimento.Value;
            cliente.Endereco = listBoxEndereco.SelectedItem as Endereco;

            return cliente;
        }
        private void InserirTextos()
        {
            txtNome.Text = _cliente.NomeCompleto;
            txtTelefone.Text = _cliente.Telefone;
            txtCpf.Text = _cliente.CPF;
            dtpDataNascimento.Value = _cliente.DataNascimento;
        }

        public void ListarEndereco()
        {
            var lista = enderecoServico.BuscarTudo();

            if (lista == null) return;

            listBoxEndereco.Items.Clear();

            foreach (var item in lista)
            {
                listBoxEndereco.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnderecoDialog enderecoDialog = new EnderecoDialog("Adicionar novo");
            enderecoDialog.ShowDialog();

            ListarEndereco();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cliente == null)
                    clienteServico.Adicionar(ConverterParaObjeto(0));
                else
                    clienteServico.Editar(ConverterParaObjeto(_cliente.Id));
            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
