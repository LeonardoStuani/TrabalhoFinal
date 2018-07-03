using AluguelCarros.Aplicacao.Featues.Carros;
using AluguelCarros.Dominio.Contratos.Alugueis;
using AluguelCarros.Dominio.Contratos.Carros;
using AluguelCarros.Dominio.Features.Carros;
using AluguelCarros.Infra.Dados.Features.Alugueis;
using AluguelCarros.Infra.Dados.Features.Carros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelCarros.Desktop.Features.Carros
{
    public partial class CarroDialog : Form
    {
        Carro _carro;
        private ICarroRepositorio carroRepositorio;
        private IAluguelRepositorio aluguelRepositorio;
        private ICarroServico carroServico;
        public CarroDialog(string titulo, Carro carro = null)
        {
            InitializeComponent();
            carroRepositorio = new CarroRepositorio();
            aluguelRepositorio = new AluguelRepositorio();
            carroServico = new CarroServico(carroRepositorio, aluguelRepositorio);
            this.Text = titulo;

            _carro = carro;

            if(_carro != null)
            InserirTextos();
        }

        private void InserirTextos()
        {
            DateTime data = DateTime.Now;
            data.AddYears(-_carro.Ano);
            txtAno.Value = data;
            txtMarca.Text = _carro.Marca;
            txtModelo.Text = _carro.Modelo;
            txtPlaca.Text = _carro.Placa;
            txtPrecoPorHora.Value = Convert.ToDecimal(_carro.PrecoPorHora);
        }

        private Carro ConverterParaObjeto(long id)
        {
            Carro carro = null;

            if (id > 0)
                carro = carroServico.BuscarPor(id);
            else
                carro = new Carro();

            carro.Id = id;
            carro.Ano = txtAno.Value.Year;
            carro.Marca = txtMarca.Text;
            carro.Modelo = txtModelo.Text;
            carro.Placa = txtPlaca.Text;
            carro.PrecoPorHora = Convert.ToDouble( txtPrecoPorHora.Value);

            return carro;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_carro == null)
                    carroServico.Adicionar(ConverterParaObjeto(0));
                else
                    carroServico.Editar(ConverterParaObjeto(_carro.Id));
            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
