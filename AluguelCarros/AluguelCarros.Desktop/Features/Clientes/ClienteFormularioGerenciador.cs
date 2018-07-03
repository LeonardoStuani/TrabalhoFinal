using AluguelCarros.Aplicacao.Featues.Clientes;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using AluguelCarros.Infra.Dados.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelCarros.Desktop.Features.Clientes
{
    public class ClienteFormularioGerenciador : FormularioGerenciador
    {
        private IClienteRepositorio clienteRepositorio;
        private IClienteServico clienteServico;
        private IAluguelRepositorio aluguelRepositorio;
        private ClienteUserControl userControl;
        private ClienteDialog clienteDialog;

        public ClienteFormularioGerenciador()
        {
            clienteRepositorio = new ClienteRepositorio();
            aluguelRepositorio = new AluguelRepositorio();
            clienteServico = new ClienteServico(clienteRepositorio, aluguelRepositorio);
            userControl = new ClienteUserControl();
        }
        public override void Adicionar()
        {
            clienteDialog = new ClienteDialog("Novo cliente");
            clienteDialog.ShowDialog();

            AtualizarLista();
        }

        public override void AtualizarLista()
        {
            userControl.AtualizarLista();
        }

        public override UserControl CarregarListagem()
        {
            userControl = new ClienteUserControl();

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
                        selecionado = clienteServico.BuscarPor(selecionado.Id);
                        clienteServico.Deletar(selecionado);
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
                clienteDialog = new ClienteDialog("Editar Cliente", selecionado);
                clienteDialog.ShowDialog();
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
