using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AluguelCarros.Dominio.Contratos.Carros;
using AluguelCarros.Aplicacao.Featues.Carros;
using AluguelCarros.Infra.Dados.Features.Carros;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using AluguelCarros.Dominio.Features.Carros;

namespace AluguelCarros.Desktop.Features.Carros
{
    public partial class CarroUserControl : UserControl
    {
        private ICarroRepositorio carroRepositorio;
        private IAluguelRepositorio aluguelRepositorio;
        private ICarroServico carroServico;
        public CarroUserControl()
        {
            InitializeComponent();

            carroRepositorio = new CarroRepositorio();
            aluguelRepositorio = new AluguelRepositorio();
            carroServico = new CarroServico(carroRepositorio, aluguelRepositorio);
            AtualizarLista();
        }

        public void AtualizarLista()
        {
            var lista = carroServico.BuscarTudo();

            if (lista == null) return;

            dgCarros.DataSource = null;

            dgCarros.DataSource = lista;
        }

        public Carro ObterSelecionado()
        {
            return dgCarros.CurrentRow.DataBoundItem as Carro;
        }
    }
}
