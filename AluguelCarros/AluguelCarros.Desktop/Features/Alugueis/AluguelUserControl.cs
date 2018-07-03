using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AluguelCarros.Dominio.Features.Alugueis;
using AluguelCarros.Aplicacao.Featues.Carros;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Carros;
using AluguelCarros.Infra.Dados.Features.Carros;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using AluguelCarros.Aplicacao.Featues.Alugueis;

namespace AluguelCarros.Desktop.Features.Alugueis
{
    public partial class AluguelUserControl : UserControl
    {
        private IAluguelRepositorio aluguelRepositorio;
        private IAluguelServico aluguelServico;
        public AluguelUserControl()
        {
            InitializeComponent();
            aluguelRepositorio = new AluguelRepositorio();
            aluguelServico = new AluguelServico(aluguelRepositorio);
        }

        public Aluguel ObterSelecionado()
        {
            return dgAlugueis.CurrentRow.DataBoundItem as Aluguel;
        }

        public void AtualizarLista()
        {
            var lista = aluguelServico.BuscarTudo();

            if (lista == null) return;

            dgAlugueis.DataSource = null;

            dgAlugueis.DataSource = lista;
        }
    }
}
