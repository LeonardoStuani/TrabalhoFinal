using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelCarros.Desktop
{
   public abstract class FormularioGerenciador
    {
        public abstract void Adicionar();

        public abstract void EditarAluguel();

        public abstract UserControl CarregarListagem();

        public abstract void Deletar();

        public abstract void Editar();

        public abstract void AtualizarLista();
    }
}
