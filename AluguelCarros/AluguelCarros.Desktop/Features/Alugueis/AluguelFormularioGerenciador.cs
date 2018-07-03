using AluguelCarros.Aplicacao.Featues.Alugueis;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelCarros.Desktop.Features.Alugueis
{
    public class AluguelFormularioGerenciador : FormularioGerenciador
    {
        private  IAluguelRepositorio aluguelRepositorio;
        private IAluguelServico aluguelServico;
        private AluguelDialog aluguelDialog;

        private AluguelUserControl userControl;

        public AluguelFormularioGerenciador()
        {
            aluguelRepositorio = new AluguelRepositorio();
            aluguelServico = new AluguelServico(aluguelRepositorio);
        }
        public override void Adicionar()
        {
            aluguelDialog = new AluguelDialog("Novo Carro");
            aluguelDialog.ShowDialog();

            AtualizarLista();
        }

        public override void AtualizarLista()
        {
            userControl.AtualizarLista();
        }

        public override UserControl CarregarListagem()
        {
            userControl = new AluguelUserControl();

            return userControl;
        }

        public override void Deletar()
        {
            var selecionado = userControl.ObterSelecionado();

            if (selecionado != null)
            {
                try
                {
                    DialogResult result = MessageBox.Show("Tem certeza que deseja excluir o item?", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        selecionado = aluguelServico.BuscarPor(selecionado.Id);
                        aluguelServico.Deletar(selecionado);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Nenhum item selecionado");
            }

            AtualizarLista();
        }

        public override void Editar()
        {
            var selecionado = userControl.ObterSelecionado();

            if (selecionado != null)
            {
                aluguelDialog = new AluguelDialog("Editar carro", selecionado);
                aluguelDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nenhum item selecionado");
            }

            AtualizarLista();
        }

        public override void EditarAluguel()
        {
            var selecionado = userControl.ObterSelecionado();
            AluguelDevolverDialog aluguelDevolverDialog = new AluguelDevolverDialog(selecionado);
            aluguelDevolverDialog.ShowDialog();
        }
    }
}
