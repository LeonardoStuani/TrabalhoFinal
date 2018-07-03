using AluguelCarros.Aplicacao.Featues.Clientes;
using AluguelCarros.Aplicacao.Featues.Enderecos;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Contratos.Enderecos;
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

namespace AluguelCarros.Desktop.Features.Enderecos
{
    public partial class EnderecoDialog : Form
    {
        Endereco _endereco;
        private IEnderecoRepositorio enderecoRepositorio;
        private IClienteRepositorio clienteRepositorio;
        private IEnderecoServico enderecoServico;
        public EnderecoDialog(string titulo, Endereco endereco = null)
        {
            InitializeComponent();

            enderecoRepositorio = new EnderecoRepositorio();
            clienteRepositorio = new ClienteRepositorio();
            enderecoServico = new EnderecoServico(enderecoRepositorio, clienteRepositorio);
            this.Text = titulo;

            _endereco = endereco;

            if (_endereco != null)
                InserirTextos();
        }

        private void InserirTextos()
        {
            txtBairro.Text = _endereco.Bairro;
            txtCep.Text = _endereco.Cep;
            txtCidade.Text = _endereco.Cidade;
            txtRua.Text = _endereco.Rua;
            txtNumber.Value = _endereco.Numero;
        }

        public Endereco ConverterParaObjeto(long id)
        {
            Endereco endereco = null;

            if (id > 0)
                endereco = enderecoServico.BuscarPor(id);
            else
                endereco = new Endereco();

            endereco.Id = id;
            endereco.Rua = txtRua.Text;
            endereco.Bairro = txtBairro.Text;
            endereco.Cep = txtCep.Text;
            endereco.Cidade = txtCidade.Text;
            endereco.Numero = Convert.ToInt32(txtNumber.Text);
            endereco.Estado = cmbEstado.SelectedItem.ToString();
            return endereco;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_endereco == null)
                    enderecoServico.Adicionar(ConverterParaObjeto(0));
                else
                    enderecoServico.Editar(ConverterParaObjeto(_endereco.Id));

            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
