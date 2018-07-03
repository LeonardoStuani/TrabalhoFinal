using AluguelCarros.Desktop.Features.Alugueis;
using AluguelCarros.Desktop.Features.Carros;
using AluguelCarros.Desktop.Features.Clientes;
using AluguelCarros.Desktop.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelCarros.Desktop
{
    public partial class Main : Form
    {
        FormularioGerenciador _formularioGerenciador;
        public Main()
        {
            InitializeComponent();
            _formularioGerenciador = new ClienteFormularioGerenciador();
            UploadForms(_formularioGerenciador);
            btnFinalizar.Visible = false;
        }

        private void UploadForms(FormularioGerenciador formularioGerenciador)
        {
            _formularioGerenciador = formularioGerenciador;

            UserControl _userControl = _formularioGerenciador.CarregarListagem();

            _userControl.Dock = DockStyle.Fill;

            panel.Controls.Clear();

            panel.Controls.Add(_userControl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _formularioGerenciador.Adicionar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _formularioGerenciador.Editar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _formularioGerenciador.Deletar();
        }

        private void endereçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnFinalizar.Visible = false;
            _formularioGerenciador = new EnderecoFormularioGerenciador();
            UploadForms(_formularioGerenciador);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnFinalizar.Visible = false;
            _formularioGerenciador = new ClienteFormularioGerenciador();
            UploadForms(_formularioGerenciador);
        }

        private void carrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnFinalizar.Visible = false;
            _formularioGerenciador = new CarroFormularioGerenciador();
            UploadForms(_formularioGerenciador);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _formularioGerenciador.EditarAluguel();
        }

        private void aluguéisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnFinalizar.Visible = true;

            _formularioGerenciador = new AluguelFormularioGerenciador();
            UploadForms(_formularioGerenciador);
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
