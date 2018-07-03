using AluguelCarros.Aplicacao.Featues.Alugueis;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Enums;
using AluguelCarros.Dominio.Features.Alugueis;
using AluguelCarros.Infra.Dados.Features.Alugueis;
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
    public partial class AluguelDevolverDialog : Form
    {
        Aluguel _aluguel;
        private IAluguelRepositorio aluguelRepositorio;
        private IAluguelServico aluguelServico;
        public AluguelDevolverDialog(Aluguel aluguel)
        {
            InitializeComponent();

            PupularEnums();
            aluguelRepositorio = new AluguelRepositorio();
            aluguelServico = new AluguelServico(aluguelRepositorio);
            _aluguel = aluguel;

            dtpDataDevolucao.Value = DateTime.Now;
        }

        private void PupularEnums()
        {
            comboBoxPagamento.DataSource = Enum.GetNames(typeof(EPagamento));
            comboBoxStatus.DataSource = Enum.GetNames(typeof(EStatus));
        }

        private void AluguelDevolverDialog_Load(object sender, EventArgs e)
        {
            _aluguel.CalcularValorTotal();
            txtHorasTotais.Text =  _aluguel.HorasTotal.ToString();
            txtValorTotal.Text = "Valor Total: " + _aluguel.ValorTotal;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                aluguelServico.FecharAluguel(ConverterParaObjeto(_aluguel.Id));
            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show(ex.Message);
            }
        }

        public Aluguel ConverterParaObjeto(long id)
        {
            var aluguel = aluguelServico.BuscarPor(id);

            aluguel.DataDevolucao = dtpDataDevolucao.Value;
            aluguel.HorasTotal =Convert.ToDouble( txtHorasTotais.Text);
            aluguel.Pagamento = VerificarPagamento();
            aluguel.Status = EStatus.Disponivel;

            return aluguel;
        }
        
        private EPagamento VerificarPagamento()
        {
            if (comboBoxPagamento.SelectedItem.Equals("CartaoCredito"))
                return EPagamento.CartaoCredito;
            if (comboBoxPagamento.SelectedItem.Equals("CartaoDebito"))
                return EPagamento.CartaoDebito;
            if (comboBoxPagamento.SelectedItem.Equals("Cheque"))
                return EPagamento.Cheque;
            if (comboBoxPagamento.SelectedItem.Equals("Dinheiro"))
                return EPagamento.Dinheiro;
            else 
                return EPagamento.NaoDefinido;
        }
    }
}
