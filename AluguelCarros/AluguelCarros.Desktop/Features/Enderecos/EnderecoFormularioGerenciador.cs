using AluguelCarros.Aplicacao.Featues.Enderecos;
using AluguelCarros.Dominio.Contratos.Clientes;
using AluguelCarros.Dominio.Contratos.Enderecos;
using AluguelCarros.Infra.Dados.Features.Clientes;
using AluguelCarros.Infra.Dados.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelCarros.Desktop.Features.Enderecos
{
    public class EnderecoFormularioGerenciador : FormularioGerenciador
    {
        private EnderecoDialog enderecoDialog;
        private EnderecoUserControl userControl;
        private IEnderecoRepositorio enderecoRepositorio;
        private IClienteRepositorio clienteRepositorio;
        private IEnderecoServico enderecoServico;
        public EnderecoFormularioGerenciador()
        {
            enderecoRepositorio = new EnderecoRepositorio();
            clienteRepositorio = new ClienteRepositorio();
            enderecoServico = new EnderecoServico(enderecoRepositorio, clienteRepositorio);

            userControl = new EnderecoUserControl();
        }
        public override void Adicionar()
        {
            enderecoDialog = new EnderecoDialog("Cadastrar novo Endereço");
            enderecoDialog.ShowDialog();

            AtualizarLista();
        }

        public override void AtualizarLista()
        {
            userControl.AtualizarLista();
        }

        public override UserControl CarregarListagem()
        {
            userControl = new EnderecoUserControl();

            return userControl;
        }

        public override void Deletar()
        {
            var selecionado = userControl.ObterSelecionado();

            if(selecionado != null)
            {
                try
                {
                    DialogResult result = MessageBox.Show("Tem certeza que deseja excluir o item?", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        selecionado = enderecoServico.BuscarPor(selecionado.Id);
                        enderecoServico.Deletar(selecionado);
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
                enderecoDialog = new EnderecoDialog( "Editar Endereco", selecionado);
                enderecoDialog.ShowDialog();
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
