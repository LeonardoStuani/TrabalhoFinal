using AluguelCarros.Aplicacao.Featues.Alugueis;
using AluguelCarros.Aplicacao.Featues.Carros;
using AluguelCarros.Aplicacao.Featues.Clientes;
using AluguelCarros.Desktop.Features.Carros;
using AluguelCarros.Desktop.Features.Clientes;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Carros;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Enums;
using AluguelCarros.Dominio.Features.Alugueis;
using AluguelCarros.Dominio.Features.Carros;
using AluguelCarros.Dominio.Features.Clientes;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using AluguelCarros.Infra.Dados.Features.Carros;
using AluguelCarros.Infra.Dados.Features.Clientes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelCarros.Desktop.Features.Alugueis
{
    public partial class AluguelDialog : Form
    {
        private IClienteRepositorio clienteRepositorio;
        private IClienteServico clienteServico;
        private IAluguelRepositorio aluguelRepositorio;
        private IAluguelServico aluguelServico;
        private ICarroRepositorio carroRepositorio;
        private ICarroServico carroServico;
       
        Aluguel _aluguel;
        public AluguelDialog(string titulo, Aluguel aluguel = null)
        {
            clienteRepositorio = new ClienteRepositorio();
            aluguelRepositorio = new AluguelRepositorio();
            clienteServico = new ClienteServico(clienteRepositorio, aluguelRepositorio);
            carroRepositorio = new CarroRepositorio();
            carroServico = new CarroServico(carroRepositorio, aluguelRepositorio);
            aluguelServico = new AluguelServico(aluguelRepositorio);
            InitializeComponent();
            this.Text = titulo;

            _aluguel = aluguel;

            if (_aluguel != null)
                PopularTextos();

            PopularListaDeClientes();
            PopularListaDeCarros();
            PopularEnum();
        }

        private void PopularTextos()
        {
            txtDataEmtrada.Value = _aluguel.DataEntrada;
        }

        public void PopularListaDeClientes()
        {
            var lista = clienteServico.BuscarTudo();

            if (lista == null) return;

            listBoxClientes.Items.Clear();

            foreach (var item in lista)
            {
                listBoxClientes.Items.Add(item);
            }

        }
        public void PopularListaDeCarros()
        {
            var lista = carroServico.BuscarTudo();

            if (lista == null) return;

            listBoxCarros.Items.Clear();

            foreach (var item in lista)
            {
                listBoxCarros.Items.Add(item);
            }
        }

        public void PopularEnum()
        {
            comboBoxStatus.DataSource = Enum.GetNames(typeof(EStatus));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_aluguel == null)
                    aluguelServico.Adicionar(ConverterParaObjeto(0));
                else
                    aluguelServico.Editar(ConverterParaObjeto(_aluguel.Id));
            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show(ex.Message);
            }
        }

        private Aluguel ConverterParaObjeto(long id)
        {
            Aluguel aluguel = null;

            if (id > 0)
                aluguel = aluguelServico.BuscarPor(id);
            else
                aluguel = new Aluguel();

            aluguel.Status = VerificarStatus();
            aluguel.DataEntrada = DateTime.Now;

            var carro = listBoxCarros.SelectedItem as Carro;
            aluguel.Carro = carroServico.BuscarPor(carro.Id);

            var cliente = listBoxClientes.SelectedItem as Cliente;
            aluguel.Cliente = clienteServico.BuscarPor(cliente.Id);

                return aluguel;
        }

        private EStatus VerificarStatus()
        {
            if (comboBoxStatus.SelectedItem.Equals("Alugado"))
                return EStatus.Alugado;

            if (comboBoxStatus.SelectedItem.Equals("Disponivel"))
                return EStatus.Disponivel;

            if (comboBoxStatus.SelectedItem.Equals("Manutencao"))
                return EStatus.Manutencao;
            else return EStatus.NaoDefinido;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClienteDialog clienteDialog = new ClienteDialog("Novo cliente");
            clienteDialog.ShowDialog();

            PopularListaDeClientes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarroDialog carroDialog = new CarroDialog("Novo Carro");
            carroDialog.ShowDialog();

            PopularListaDeCarros();
        }
    }
}
