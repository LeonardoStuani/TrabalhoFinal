using AluguelCarros.Aplicacao.Featues.Carros;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Carros;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using AluguelCarros.Infra.Dados.Features.Carros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelCarros.Desktop.Features.Carros
{
    public class CarroFormularioGerenciador : FormularioGerenciador
    {
        private ICarroRepositorio carroRepositorio;
        private IAluguelRepositorio aluguelRepositorio;
        private ICarroServico carroServico;
        private CarroDialog carroDialog;
        private CarroUserControl userControl;

        public CarroFormularioGerenciador()
        {
            carroRepositorio = new CarroRepositorio();
            aluguelRepositorio = new AluguelRepositorio();
            carroServico = new CarroServico(carroRepositorio, aluguelRepositorio);
        }
        public override void Adicionar()
        {
            carroDialog = new CarroDialog("Novo Carro");
            carroDialog.ShowDialog();

            AtualizarLista();
        }

        public override void AtualizarLista()
        {
            userControl.AtualizarLista();
        }

        public override UserControl CarregarListagem()
        {
            userControl = new CarroUserControl();

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
                        selecionado = carroServico.BuscarPor(selecionado.Id);
                        carroServico.Deletar(selecionado);
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
                carroDialog = new CarroDialog("Editar carro", selecionado);
                carroDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nenhum item selecionado");
            }

            AtualizarLista();
        }

        public override void EditarAluguel()
        {
            throw new NotImplementedException();
        }
    }
}
